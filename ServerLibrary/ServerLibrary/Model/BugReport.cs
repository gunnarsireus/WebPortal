using System;

namespace ServerLibrary.Model
{
    public class BugReport : Validatable
    {
        // GitHub (made-up) constraints
        public const int MINLEN_HEADLINE    = 5;
        public const int MAXLEN_HEADLINE    = 50;
        public const int MINLEN_DESCRIPTION = 5;
        public const int MAXLEN_DESCRIPTION = 300;
        
        public string headline    { get; set; }
        public string description { get; set; }

        public BugReport()
        {
            this.headline    = "";
            this.description = "";
        }

        public override void Validate()
        {
            headline    = ValidateRange(MINLEN_HEADLINE,    headline,    MAXLEN_HEADLINE,    "Felaktig rubrik");
            description = ValidateRange(MINLEN_DESCRIPTION, description, MAXLEN_DESCRIPTION, "Felaktig beskrivning");
        }
    }
}