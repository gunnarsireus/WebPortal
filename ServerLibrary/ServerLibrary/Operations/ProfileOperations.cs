using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations
{
    public static class ProfileOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            return requester.IsAtLeastResident();
        }

        public static void TryCreate(Account requester, DataContext context, Account profile)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static IQueryable<Account> TryList(Account requester, DataContext context)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static Account TryRead(Account requester, DataContext context, int id)
        {
            return GetDBEntity(context, requester.id);
        }

        public static Account TryUpdate(Account requester, DataContext context, Account dbentity)
        {
            if (requester.id != dbentity.id)
            {
                throw new ServerAuthorizeException("Du har inte behörighet att uppdatera annans profil");
            }
            dbentity.Validate();
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Modified;
            return dbentity;
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            // Don't really delete, just make inaccessible
            Account dbentity = GetDBEntity(context, id);
            dbentity.active   = Account.INACTIVE;
            dbentity.password = "inactive";
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Modified;
        }

        private static Account GetDBEntity(DataContext context, int id)
        {
            Account dbentity = context.Accounts.Find(id);
            if (dbentity == null)
            {
                throw new ServerDBEntityException("Databasen innehåller ej profil med id " + id);
            }
            return dbentity;
        }
    }
}
