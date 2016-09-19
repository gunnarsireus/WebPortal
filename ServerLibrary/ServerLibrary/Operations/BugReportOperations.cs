using System;
using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations
{
    public static class BugReportOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            return requester.IsAtLeastAdmin();
        }

        public static void TryCreate(Account requester, DataContext context, BugReport bugreport)
        {
            if (!requester.IsAtLeastAdmin())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att skapa felanmälan");
            }
            bugreport.Validate();
        }

        public static IQueryable<Account> TryList(Account requester, DataContext context)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static Account TryRead(Account requester, DataContext context, int id)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static Account TryUpdate(Account requester, DataContext context, Account dbentity)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }
    }
}
