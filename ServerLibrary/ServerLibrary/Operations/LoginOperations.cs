using System;
using System.Linq;
using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace ServerLibrary.Operations
{
    public static class LoginOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            return true;
        }

        public static void TryCreate(Account requester, DataContext context, Account account)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
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

        public static Account TryValidate(Account requester, DataContext context, string password)
        {
            long now = DateUtils.TimeStamp;

            password = StringUtils.Trim(password);
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ServerValidateException("Felaktigt lösenord");
            }

            if (requester.IsInactive())
            {
                throw new ServerConflictException("Kontot inaktiverat");
            }

            if (requester.IsLocked(now))
            {
                throw new ServerConflictException("Det här kontot är låst till " + DateUtils.ConvertToTimeString(requester.lockeduntil));
            }

            if (requester.IsAtMostCustomer())
            {
                Customer customer = context.Customers.Find(requester.customerid);
                if (customer == null || customer.id == Customer.CUSTOMER_ANY || customer.IsInactive())
                {
                    throw new ServerConflictException("Din förening tillåter inte inloggning");
                }
            }

            context.Entry(requester).State = System.Data.Entity.EntityState.Modified;
            if (!CryptoUtils.VerifyMD5Hash(password, requester.password))
            {
                requester.RegisterFailedAttempt(now);
                return null;
            }
            requester.SetSuccessfulLogin(now);
            return requester;
        }

        public static void TrySendPIN(Account requester, DataContext context, Account dbentity)
        {
            if (requester.IsVerifiedInactive())
            {
                throw new ServerConflictException("Kontot låst av administratör");
            }

            requester.password      = "";
            requester.PIN           = CryptoUtils.CreatePINCode();
            requester.PINvaliduntil = DateUtils.TimeStamp + Account.PIN_VALID_SECS;
            string result = MailUtils.Instance.Send_PIN(requester);
            if (result != null)
            {
                throw new ServerConflictException("Kunde inte skicka e-post, försök igen senare (" + result + ")");
            }
            context.Entry(requester).State = System.Data.Entity.EntityState.Modified;
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
                throw new ServerDBEntityException("Felaktig användare eller lösenord");
            }
            return dbentity;
        }
    }
}
