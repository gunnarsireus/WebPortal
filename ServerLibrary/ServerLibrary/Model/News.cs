using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServerLibrary.Utils;

namespace ServerLibrary.Model
{
    [Table("news")]
    public class News : Validatable
    {
        // Constants
        public const int MAX_NEWS_FOR_CUSTOMER = 50; // TODO: Implement in xxxOperations!

        public const int NEWS_ANY  = 0;
        public const int NEWS_NONE = 0;

        public const int CATEGORY_ANY     = 0;
        public const int CATEGORY_NONE    = 0;
        public const int CATEGORY_MESSAGE = 11;
        public const int CATEGORY_ECONOMY = 12;
        public const int CATEGORY_REQUEST = 13;
        public const int CATEGORY_CALLING = 14;
        public const int CATEGORY_WARNING = 15;
        public const int CATEGORY_VARIOUS = 19;

        // Strings
        public const string STRING_CATEGORY_MESSAGE = "Meddelande";
        public const string STRING_CATEGORY_ECONOMY = "Ekonomi";
        public const string STRING_CATEGORY_REQUEST = "Förfrågan";
        public const string STRING_CATEGORY_CALLING = "Kallelse";
        public const string STRING_CATEGORY_WARNING = "Varning";
        public const string STRING_CATEGORY_VARIOUS = "Övrigt";

        // Database constraints
        public const int MINLEN_HEADLINE = 3;
        public const int MAXLEN_HEADLINE = 45;
        public const int MINLEN_MESSAGE  = 0;
        public const int MAXLEN_MESSAGE  = 500;
        public const int MINLEN_AUTHOR   = 0;
        public const int MAXLEN_AUTHOR   = 100;

        [Key]
        public int    id         { get; set; }
        public int    customerid { get; set; }
        public int    category   { get; set; }
        public string headline   { get; set; }
        public string message    { get; set; }
        public string author     { get; set; }
        public long   sent       { get; set; }
        public long   showfrom   { get; set; }
        public long   showuntil  { get; set; }

        public News()
        {
            this.id         = 0;
            this.customerid = 0;
            this.category   = 0;
            this.headline   = "";
            this.message    = "";
            this.author     = "";
            this.sent       = 0;
            this.showfrom   = 0;
            this.showuntil  = 0;
        }

        public static string CategoryAsString(int category)
        {
            if (category == CATEGORY_MESSAGE) return STRING_CATEGORY_MESSAGE;
            if (category == CATEGORY_ECONOMY) return STRING_CATEGORY_ECONOMY;
            if (category == CATEGORY_REQUEST) return STRING_CATEGORY_REQUEST;
            if (category == CATEGORY_CALLING) return STRING_CATEGORY_CALLING;
            if (category == CATEGORY_WARNING) return STRING_CATEGORY_WARNING;
            if (category == CATEGORY_VARIOUS) return STRING_CATEGORY_VARIOUS;
            return "";
        }

        public override void Validate()
        {
            ValidateGreaterThan(showfrom, 0, "Felaktigt visas datum/tid");
            ValidateGreaterThan(showuntil, 0, "Felaktigt släcks datum/tid");
            ValidateDateTimePeriod(showfrom, showuntil, "Visas måste komma före släcks");
            ValidateFutureDateTime(showuntil, "Släcks har redan passerat");
            headline = ValidateRange(MINLEN_HEADLINE, headline, MAXLEN_HEADLINE, "Felaktig rubrik");
            message  = ValidateRange(MINLEN_MESSAGE,  message,  MAXLEN_MESSAGE,  "Felaktigt meddelande");
            // Auto-generated info, just cut too long strings
            author = StringUtils.Cutoff(author, MAXLEN_AUTHOR);
        }
    }
}