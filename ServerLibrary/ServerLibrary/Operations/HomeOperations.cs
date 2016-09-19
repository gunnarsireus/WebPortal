using System;
using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations
{
    public static class HomeOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            return requester.IsAtLeastResident();
        }

        public static void TryCreate(Account requester, DataContext context, Type type)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static IQueryable<Type> TryList(Account requester, DataContext context)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static Type TryRead(Account requester, DataContext context, int id)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static Type TryUpdate(Account requester, DataContext context, Type dbentity)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            throw new ServerAuthorizeException("Ej tillåtet");
        }
    }
}
