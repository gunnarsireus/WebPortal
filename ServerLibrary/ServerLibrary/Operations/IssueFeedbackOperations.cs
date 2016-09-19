using System;
using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations
{
    public static class IssueFeedbackOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static void TryCreate(Account requester, DataContext context, IssueFeedback feedback)
        {
            feedback.Validate();
            context.IssueFeedbacks.Add(feedback);
        }

        public static IQueryable<IssueFeedback> TryList(Account requester, DataContext context, int issueid)
        {
            IQueryable<IssueFeedback> query = context.IssueFeedbacks.Where(t => t.issueid == issueid);
            if (requester.IsAtMostCustomer())
            {
                query = query.Where(i => i.accesstype == IssueFeedback.ACCESSTYPE_PUBLIC || i.createdby == requester.id);
            }
            return query.OrderBy(t => t.createddate).AsQueryable<IssueFeedback>();
        }

        public static IssueFeedback TryRead(Account requester, DataContext context, int id)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static IssueFeedback TryUpdate(Account requester, DataContext context, IssueFeedback dbentity)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }
    }
}
