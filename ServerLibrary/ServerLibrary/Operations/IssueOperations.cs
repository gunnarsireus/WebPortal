using System;
using System.Linq;
using System.Collections.Generic;
using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace ServerLibrary.Operations
{
    public static class IssueOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            return requester.IsAtLeastResident();
        }

        public static void TryCreate(Account requester, DataContext context, Issue issue)
        {
            if (requester.IsAtMostCustomer() && issue.customerid != requester.customerid)
            {
                throw new ServerAuthorizeException("Du har inte behörighet att skapa ärende");
            }
            issue.Validate();

            Issue busy = context.Issues.CollidesWith(issue);
            if (busy != null)
            {
                string t0 = DateUtils.ConvertToTimeString(busy.startdate);
                string t1 = DateUtils.ConvertToTimeString(busy.enddate);
                throw new ServerConflictException("Tilldelad redan bokad på ärende " + busy.id + " (" + t0 + "-" + t1 + ")");
            }

            context.Issues.Add(issue);
        }

        public static IQueryable<Issue> TryList(Account requester, DataContext context)
        {
            IQueryable<Issue> query = context.Issues.AsQueryable<Issue>();
            if (requester.IsAtMostResident())
            {
                return query.Where(i => i.customerid == requester.customerid && (i.residentid == requester.id || i.areatype == Issue.AREATYPE_COMMON));
            }
            if (requester.IsAtMostCustomer())
            {
                return query.Where(i => i.customerid == requester.customerid);
            }
            if (requester.IsAtMostTechnician())
            {
                IList<int> mycustomers = context.Customers.Where(c => c.technicianid == requester.id).Select(c => c.id).ToList();
                return query.Where(i => i.assignedid == requester.id || mycustomers.Contains(i.customerid));    
            }
            if (requester.IsAtLeastManagement())
            {
                return query;
            }
            throw new ServerAuthorizeException("Du har inte behörighet att lista ärenden");
        }

        public static Issue TryRead(Account requester, DataContext context, int id)
        {
            Issue dbentity = GetDBEntity(context, id);
            if (requester.IsAtMostCustomer() && dbentity.customerid != requester.customerid)
            {
                throw new ServerAuthorizeException("Du har inte behörighet för ärende");
            }
            return dbentity;
        }

        public static Issue TryUpdate(Account requester, DataContext context, Issue dbentity)
        {
            if (requester.IsAtMostCustomer() && dbentity.customerid != requester.customerid)
            {
                throw new ServerAuthorizeException("Du har inte behörighet att uppdatera ärende");
            }
            if (requester.IsResident() && dbentity.areatype == Issue.AREATYPE_APARTMENT && requester.id != dbentity.residentid)
            {
                throw new ServerAuthorizeException("Du har inte behörighet att uppdatera ärende");
            }
            dbentity.Validate();

            Issue busy = context.Issues.CollidesWith(dbentity);
            if (busy != null)
            {
                string t0 = DateUtils.ConvertToTimeString(busy.startdate);
                string t1 = DateUtils.ConvertToTimeString(busy.enddate);
                throw new ServerConflictException("Tilldelad redan bokad på ärende " + busy.id + " (" + t0 + "-" + t1 + ")");
            }

            context.Entry(dbentity).State = System.Data.Entity.EntityState.Modified;
            return dbentity;
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att radera ärende");
            }
            Issue dbentity = GetDBEntity(context, id);
            if (dbentity.status < Issue.__STATUS_ARCHIVED)
            {
                throw new ServerConflictException("Ärendet kan inte raderas i detta status");
            }
            // Do deletion
            context.Issues.Remove(dbentity);
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Deleted;
        }

        public static void TryDeleteReaders(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att uppdatera ärende");
            }
            context.IssueReaders.RemoveRange(context.IssueReaders.Where(i => i.issueid == id));
        }

        private static Issue GetDBEntity(DataContext context, int id)
        {
            Issue dbentity = context.Issues.Find(id);
            if (dbentity == null)
            {
                throw new ServerDBEntityException("Databasen innehåller ej ärende med id " + id);
            }
            return dbentity;
        }
    }
}
