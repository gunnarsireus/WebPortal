using System;
using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations
{
    public static class TimeTypeOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            return requester.IsAtLeastManagement();
        }

        public static void TryCreate(Account requester, DataContext context, TimeType timetype)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att skapa tidtyp");
            }
            timetype.Validate();
            context.TimeTypes.Add(timetype);
        }

        public static IQueryable<TimeType> TryList(Account requester, DataContext context)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att lista tidtyper");
            }
            return context.TimeTypes.AsQueryable<TimeType>();
        }

        public static TimeType TryRead(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet för tidtyp");
            }
            return GetDBEntity(context, id);
        }

        public static TimeType TryUpdate(Account requester, DataContext context, TimeType dbentity)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att uppdatera tidtyp");
            }
            dbentity.Validate();
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Modified;
            return dbentity;
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att radera tidtyp");
            }
            // Issues -- delete NOT OK
            IssueTime issuetime = context.IssueTimes.FirstOrDefault(i => id == i.timetypeid);
            if (issuetime != null)
            {
                throw new ServerConflictException("Tidtyp används i ärende " + issuetime.issueid);
            }
            // Do deletion
            TimeType dbentity = GetDBEntity(context, id);
            context.TimeTypes.Remove(dbentity);
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Deleted;
        }

        private static TimeType GetDBEntity(DataContext context, int id)
        {
            TimeType dbentity = context.TimeTypes.Find(id);
            if (dbentity == null)
            {
                throw new ServerDBEntityException("Databasen innehåller ej tidtyp med id " + id);
            }
            return dbentity;
        }
    }
}
