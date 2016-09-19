using System;
using System.Linq;
using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace ServerLibrary.Operations
{
    public static class RegisterOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            return true;
        }

        public static void TryCreate(Account requester, DataContext context, Account account)
        {
            // If we find an unverified account with same e-mail, probably the
            // same user is trying again, so we let that pass and (re)send PIN.
            Account dbentity = context.Accounts.FirstOrDefault(a => a.email == account.email);
            if (dbentity != null && dbentity.IsVerified())
            {
                throw new ServerConflictException("E-postadressen är upptagen");
            }

            account.Validate();

            Customer customer = context.Customers.Find(account.customerid);
            if (customer == null || customer.IsInactive())
            {
                throw new ServerConflictException("Din förening tillåter inte registering");
            }

            string result = MailUtils.Instance.Send_PIN(account);
            if (result != null)
            {
                throw new ServerConflictException("Kunde inte skicka e-post, försök igen senare (" + result + ")");
            }

            if (dbentity != null)
            {
                context.Accounts.Remove(dbentity);
            }
            context.Accounts.Add(account);
        }

        public static IQueryable<Account> TryList(Account requester, DataContext context)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static Account TryRead(Account requester, DataContext context, string email)
        {
            return GetDBEntity(context, email);
        }

        public static Account TryUpdate(Account requester, DataContext context, Account dbentity)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        private static Account GetDBEntity(DataContext context, string email)
        {
            Account dbentity = context.Accounts.FirstOrDefault(a => a.email == email);
            if (dbentity == null)
            {
                throw new ServerDBEntityException("Databasen innehåller ej kund med e-post " + email);
            }
            return dbentity;
        }
    }
}
