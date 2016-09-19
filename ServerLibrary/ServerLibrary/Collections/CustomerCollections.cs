using System.Linq;
using System.Collections.Generic;

using ServerLibrary.Model;

namespace ServerLibrary.Collections
{
    public static class CustomerCollections
    {
        public static IList<CollectionOption> GetActiveCustomers(DataContext context, int type, int option)
        {
            IList<CollectionOption> options = context.Customers.Where(c => c.active == Account.ACTIVE && (type == Customer.TYPE_ANY) ? true : c.type == type)
                .Select(a => new CollectionOption
                {
                    text     = a.name,
                    value    = a.id.ToString(),
                    selected = false
                }).OrderBy(c => c.text).ToList();
            if (option == CollectionOption.WITH_ANY)
            {
                options.Insert(0, new CollectionOption("--", Customer.CUSTOMER_ANY.ToString()));
            }
            return options;
        }

        public static IList<CollectionOption> GetAllCustomers(DataContext context, int type, int option)
        {
            IList<CollectionOption> options = context.Customers.Where(c => (type == Customer.TYPE_ANY) ? true : c.type == type)
                .Select(a => new CollectionOption
                {
                    text     = a.name,
                    value    = a.id.ToString(),
                    selected = false
                }).OrderBy(c => c.text).ToList();
            if (option == CollectionOption.WITH_ANY)
            {
                options.Insert(0, new CollectionOption("--", Customer.CUSTOMER_ANY.ToString()));
            }
            return options;
        }
    }
}
