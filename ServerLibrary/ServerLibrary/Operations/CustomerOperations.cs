using System;
using System.Linq;
using ServerLibrary.Model;

namespace ServerLibrary.Operations
{
    public static class CustomerOperations // see IOperation
    {
        public static bool CanRender(Account requester)
        {
            return requester.IsAtLeastManagement();
        }

        public static void TryCreate(Account requester, DataContext context, Customer customer)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att skapa kund");
            }
            if (context.Customers.Any(c => c.orgnumber == customer.orgnumber))
            {
                throw new ServerConflictException("Kund med samma organisationsnummer finns redan");
            }
            if (customer.vismaref.Length > 0 && context.Customers.Any(c => c.vismaref == customer.vismaref))
            {
                throw new ServerConflictException("Kund med samma Vismakod finns redan");
            }
            customer.Validate();
            context.Customers.Add(customer);
        }

        public static IQueryable<Customer> TryList(Account requester, DataContext context)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att lista kunder");
            }
            return context.Customers.AsQueryable<Customer>();
        }

        public static Customer TryRead(Account requester, DataContext context, int id)
        {
            if (requester.IsAtMostCustomer() && id != requester.customerid)
            {
                throw new ServerAuthorizeException("Du har inte behörighet för kund");
            }
            return GetDBEntity(context, id);
        }

        public static Customer TryUpdate(Account requester, DataContext context, Customer dbentity)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att uppdatera kund");
            }
            if (context.Customers.Any(c => c.orgnumber == dbentity.orgnumber && c.id != dbentity.id))
            {
                throw new ServerConflictException("Kund med samma organisationsnummer finns redan");
            }
            if (dbentity.vismaref.Length > 0 && context.Customers.Any(c => c.vismaref == dbentity.vismaref && c.id != dbentity.id))
            {
                throw new ServerConflictException("Kund med samma Vismakod finns redan");
            }
            dbentity.Validate();
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Modified;
            return dbentity;
        }

        public static void TryDelete(Account requester, DataContext context, int id)
        {
            if (!requester.IsAtLeastManagement())
            {
                throw new ServerAuthorizeException("Du har inte behörighet att radera kund");
            }
            // News             -- delete OK (cascades via weak FK)
            // Offers           -- delete OK (cascades via weak FK)
            // Accounts, Issues -- delete NOT OK
            if (context.Accounts.Any(a => a.customerid == id))
            {
                throw new ServerConflictException("Det finns konton kopplade till kund");
            }
            Issue issue = context.Issues.FirstOrDefault(i => i.customerid == id);
            if (issue != null)
            {
                throw new ServerConflictException("Kund används i ärende " + issue.id);
            }
            // Do deletion
            Customer dbentity = GetDBEntity(context, id);
            context.Customers.Remove(dbentity);
            context.Entry(dbentity).State = System.Data.Entity.EntityState.Deleted;
            // Delete associated News (weak FK)
            context.News.RemoveRange(context.News.Where(n => n.customerid == id));
            // Delete associated News (weak FK)
            // TODO: context.Offers.RemoveRange(context.Offers.Where(n => n.customerid == id));
        }

        private static Customer GetDBEntity(DataContext context, int id)
        {
            Customer dbentity = context.Customers.Find(id);
            if (dbentity == null)
            {
                throw new ServerDBEntityException("Databasen innehåller ej kund med id " + id);
            }
            return dbentity;
        }
    }
}
