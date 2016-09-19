using System;
using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations // see IOperation
{
    public static class NewsOperations
    {
        public static bool CanRender(Account requester)
        {
            return requester.IsAtLeastManagement();
        }

        public static void TryCreate(Account requester, DataContext context, News news)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att skapa nyhet");
            }
            news.Validate();
            context.News.Add(news);
        }

        public static IQueryable<News> TryList(Account requester, DataContext context)
        {
            IQueryable<News> query = context.News.AsQueryable<News>();
            if (requester.IsAtMostCustomer())
            {
                query = query.Where(n => n.customerid == Customer.CUSTOMER_ANY || n.customerid == requester.customerid);
            }
            return query;
        }

        public static News TryRead(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet för nyhet");
            }
            return GetDBEntity(context, id);
        }

        public static News TryUpdate(Account requester, DataContext context, News dbentity)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att uppdatera nyhet");
            }
            dbentity.Validate();
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Modified;
            // If updated, remove all "has read" records
            context.NewsReaders.RemoveRange(context.NewsReaders.Where(n => n.newsid == dbentity.id));
            return dbentity;
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att radera nyhet");
            }
            // Do deletion
            News dbentity = GetDBEntity(context, id);
            context.News.Remove(dbentity);
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Deleted;
        }

        private static News GetDBEntity(DataContext context, int id)
        {
            News dbentity = context.News.Find(id);
            if (dbentity == null)
            {
                throw new ServerDBEntityException("Databasen innehåller ej nyhet med id " + id);
            }
            return dbentity;
        }
    }
}
