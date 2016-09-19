using System;
using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations // see IOperation
{
    public static class OfferOperations
    {
        public static bool CanRender(Account requester)
        {
            return requester.IsAtLeastManagement();
        }

        public static void TryCreate(Account requester, DataContext context, Offer offer)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att skapa erbjudande");
            }
            offer.Validate();
            context.Offers.Add(offer);
        }

        public static IQueryable<Offer> TryList(Account requester, DataContext context)
        {
            IQueryable<Offer> query = context.Offers.AsQueryable<Offer>();
            if (requester.IsAtMostCustomer())
            {
                query = query.Where(o => o.customerid == Customer.CUSTOMER_ANY || o.customerid == requester.customerid);
            }
            return query;
        }

        public static Offer TryRead(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet för erbjudande");
            }
            return GetDBEntity(context, id);
        }

        public static Offer TryUpdate(Account requester, DataContext context, Offer dbentity)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att uppdatera erbjudande");
            }
            dbentity.Validate();
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Modified;
            // If updated, remove all "has read" records
            context.OfferReaders.RemoveRange(context.OfferReaders.Where(o => o.offerid == dbentity.id));
            return dbentity;
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att radera erbjudande");
            }
            // Do deletion
            Offer dbentity = GetDBEntity(context, id);
            context.Offers.Remove(dbentity);
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Deleted;
        }

        private static Offer GetDBEntity(DataContext context, int id)
        {
            Offer dbentity = context.Offers.Find(id);
            if (dbentity == null)
            {
                throw new ServerDBEntityException("Databasen innehåller ej erbjudande med id " + id);
            }
            return dbentity;
        }
    }
}
