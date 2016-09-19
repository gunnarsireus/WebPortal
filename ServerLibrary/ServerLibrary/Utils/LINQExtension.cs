using System.Collections.Generic;
using ServerLibrary.Model;

namespace ServerLibrary.Utils
{
    public static class LINQExtension
    {
        public static Issue CollidesWith(this IEnumerable<Issue> issues, Issue issue)
        {
            foreach (Issue source in issues)
            {
                if (issue != source && issue.CollidesWith(source))
                {
                    return source;
                }
            }
            return null;
        }
    }
}