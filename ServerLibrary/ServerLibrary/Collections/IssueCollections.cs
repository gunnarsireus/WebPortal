using System.Linq;
using System.Collections.Generic;

using ServerLibrary.Model;

namespace ServerLibrary.Collections
{
    public static class IssueCollections
    {
        public static IList<CollectionOption> GetIssueClasses(DataContext context, int option)
        {
            IList<CollectionOption> options = context.IssueClasses
                .Select(a => new CollectionOption
                {
                    text     = a.name,
                    value    = a.id.ToString(),
                    selected = false
                }).OrderBy(c => c.text).ToList();
            if (option == CollectionOption.WITH_ANY)
            {
                options.Insert(0, new CollectionOption("--", IssueClass.ISSUECLASS_ANY.ToString()));
            }
            return options;
        }
    }
}
