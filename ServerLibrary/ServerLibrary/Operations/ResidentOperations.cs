using System;
using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations
{
    public static class ResidentOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            return requester.IsAtLeastManagement();
        }

        public static void TryCreate(Account requester, DataContext context, Account account)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att skapa konto");
            }
            if (context.Accounts.Any(a => a.email == account.email))
            {
                throw new ServerConflictException("Konto med samma e-postadress finns redan");
            }
            account.Validate();
            context.Accounts.Add(account);
        }

        public static IQueryable<Account> TryList(Account requester, DataContext context)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att lista konton");
            }
            return context.Accounts.Where(a => a.authz <= Account.AUTHZ_CUSTOMER).AsQueryable<Account>();
        }

        public static Account TryRead(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet för konto");
            }
            return GetDBEntity(context, id);
        }

        public static Account TryUpdate(Account requester, DataContext context, Account dbentity)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att uppdatera konto");
            }
            if (context.Accounts.Any(a => a.email == dbentity.email && a.id != dbentity.id))
            {
                throw new ServerConflictException("Konto med samma e-postadress finns redan");
            }
            dbentity.Validate();
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Modified;
            return dbentity;
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att radera konto");
            }
            // Issues -- delete NOT OK
            Issue issue = context.Issues.FirstOrDefault(i => id == i.residentid);
            if (issue != null)
            {
                throw new ServerConflictException("Konto används som boende i ärende " + issue.id);
            }

            issue = context.Issues.FirstOrDefault(i => id == i.assignedid);
            if (issue != null)
            {
                throw new ServerConflictException("Konto används som tilldelad i ärende " + issue.id);
            }   

            IssueMaterial issuematerial = context.IssueMaterials.FirstOrDefault(i => id == i.createdby);
            if (issuematerial != null)
            {
                throw new ServerConflictException("Konto används för material i ärende " + issuematerial.issueid);
            }

            IssuePhoto issuephoto = context.IssuePhotos.FirstOrDefault(i => id == i.createdby);
            if (issuephoto != null)
            {
                throw new ServerConflictException("Konto används för fotografi i ärende " + issuephoto.issueid);
            }

            IssueTime issuetime = context.IssueTimes.FirstOrDefault(i => id == i.createdby);
            if (issuetime != null)
            {
                throw new ServerConflictException("Konto används för tid i ärende " + issuetime.issueid);
            }

            IssueTransition issuetransition = context.IssueTransitions.FirstOrDefault(i => id == i.createdby);
            if (issuetransition != null)
            {
                throw new ServerConflictException("Konto används för historik i ärende " + issuetransition.issueid);
            }

            // Do deletion
            Account dbentity = GetDBEntity(context, id);
            context.Accounts.Remove(dbentity);
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Deleted;
        }

        private static Account GetDBEntity(DataContext context, int id)
        {
            Account dbentity = context.Accounts.Find(id);
            if (dbentity == null)
            {
                throw new ServerDBEntityException("Databasen innehåller ej konto med id " + id);
            }
            return dbentity;
        }
    }
}
