using System;
using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations
{
    public static class IssueClassOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            return requester.IsAtLeastManagement();
        }

        public static void TryCreate(Account requester, DataContext context, IssueClass issueclass)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att skapa ärendeklass");
            }
            if (context.IssueClasses.Any(i => i.name == issueclass.name))
            {
                throw new ServerConflictException("Ärendeklass med samma namn finns redan");
            }
            issueclass.Validate();
            context.IssueClasses.Add(issueclass);
        }

        public static IQueryable<IssueClass> TryList(Account requester, DataContext context)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att lista ärendeklasser");
            }
            return context.IssueClasses.AsQueryable<IssueClass>();
        }

        public static IssueClass TryRead(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet för ärendeklass");
            }
            return GetDBEntity(context, id);
        }

        public static IssueClass TryUpdate(Account requester, DataContext context, IssueClass dbentity)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att uppdatera ärendeklass");
            }
            if (context.IssueClasses.Any(i => i.name == dbentity.name))
            {
                throw new ServerConflictException("Ärendeklass med samma namn finns redan");
            }
            dbentity.Validate();
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Modified;
            return dbentity;
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att radera ärendeklass");
            }
            // Issues -- delete NOT OK
            Issue issue = context.Issues.FirstOrDefault(i => id == i.issueclassid);
            if (issue != null)
            {
                throw new ServerConflictException("Ärendeklass används i ärende " + issue.id);
            }
            // Do deletion
            IssueClass dbentity = GetDBEntity(context, id);
            context.IssueClasses.Remove(dbentity);
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Deleted;
        }

        private static IssueClass GetDBEntity(DataContext context, int id)
        {
            IssueClass dbentity = context.IssueClasses.Find(id);
            if (dbentity == null)
            {
                throw new ServerDBEntityException("Databasen innehåller ej ärendeklass med id " + id);
            }
            return dbentity;
        }
    }
}
