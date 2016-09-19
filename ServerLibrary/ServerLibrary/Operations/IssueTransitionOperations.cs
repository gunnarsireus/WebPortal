using System;
using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations
{
    public static class IssueTransitionOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static void TryCreate(Account requester, DataContext context, IssueTransition transition)
        {
            transition.Validate();
            context.IssueTransitions.Add(transition);
        }

        public static IQueryable<IssueTransition> TryList(Account requester, DataContext context, int issueid)
        {
            return context.IssueTransitions.Where(t => t.issueid == issueid).OrderBy(t => t.createddate).AsQueryable<IssueTransition>();
        }

        public static IssueTransition TryRead(Account requester, DataContext context, int id)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static IssueTransition TryUpdate(Account requester, DataContext context, IssueTransition dbentity)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }
    }
}
