using System;
using System.Linq;
using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace ServerLibrary.Operations
{
    public static class AccountOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            return false;
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

        public static bool TrySecure(Account requester, DataContext context, Account dbentity, string PIN)
        {
            long now = DateUtils.TimeStamp;
            if (dbentity.IsLocked(now))
            {
                throw new ServerConflictException("Det här kontot är låst till " + DateUtils.ConvertToTimeString(dbentity.lockeduntil));
            }
            dbentity.Validate();
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Modified;
            if (dbentity.PIN != PIN || dbentity.PINvaliduntil < now)
            {
                dbentity.RegisterFailedAttempt(now);
                return false;
            }
            dbentity.SetSuccessfulLogin(now);
            dbentity.active = Account.ACTIVE;
            return true;
        }

        public static void TryResendPIN(Account requester, DataContext context, Account dbentity)
        {
            if (dbentity.IsVerified())
            {
                throw new ServerConflictException("Kontot redan verifierat, prova att logga in");
            }
            if (dbentity.IsVerifiedInactive())
            {
                throw new ServerConflictException("Kontot låst av administratör");
            }

            dbentity.PIN           = CryptoUtils.CreatePINCode();
            dbentity.PINvaliduntil = DateUtils.TimeStamp + Account.PIN_VALID_SECS;
            string result = MailUtils.Instance.Send_PIN(dbentity);
            if (result != null)
            {
                throw new ServerConflictException("Kunde inte skicka e-post, försök igen senare (" + result + ")");
            }
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Modified;
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
