using System.Linq;
using System.Collections.Generic;

using ServerLibrary.Model;

namespace ServerLibrary.Collections
{
    public static class AccountCollections
    {
        public static IList<CollectionOption> GetAccounts(DataContext context, int authzmask, int active, int option)
        {
            IList<CollectionOption> options = context.Accounts.Where(a => (a.authz & authzmask) != 0 && a.active == active)
                .Select(a => new CollectionOption
                {
                    text     = a.firstname + " " + a.lastname,
                    value    = a.id.ToString(),
                    selected = false
                }).OrderBy(c => c.text).ToList();
            if (option == CollectionOption.WITH_ANY)
            {
                options.Insert(0, new CollectionOption("--", Account.ACCOUNT_ANY.ToString()));
            }
            return options;
        }
    }
}
