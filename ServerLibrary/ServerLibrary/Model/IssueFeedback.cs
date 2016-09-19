using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServerLibrary.Utils;

namespace ServerLibrary.Model
{
    [Table("issuefeedbacks")]
    public class IssueFeedback : Validatable
    {
        public const int ISSUEFEEDBACK_ANY = 0;

        public const int ACCESSTYPE_ANY     = 0;
        public const int ACCESSTYPE_PRIVATE = 21;
        public const int ACCESSTYPE_PUBLIC  = 22;

        public const string STRING_ACCESSTYPE_PRIVATE = "Privat";
        public const string STRING_ACCESSTYPE_PUBLIC  = "Publik";

        // Database constraints
        public const int MINLEN_DESCRIPTION = 2;
        public const int MAXLEN_DESCRIPTION = 200;

        [Key]
        public int    id           { get; set; }
        public int    issueid      { get; set; }
        public string description  { get; set; }
        public int    createdby    { get; set; }
        public string createdname  { get; set; }
        public long   createddate  { get; set; }
        public int    createdauthz { get; set; }
        public int    accesstype   { get; set; }

        public IssueFeedback()
        {
            this.id           = 0;
            this.issueid      = 0;
            this.description  = "";
            this.createdby    = 0;
            this.createdname  = "";
            this.createddate  = 0;
            this.createdauthz = 0;
            this.accesstype   = ACCESSTYPE_ANY;
        }
   
        public override void Validate()
        {
            description = ValidateRange(MINLEN_DESCRIPTION, description, MAXLEN_DESCRIPTION, "Ogiltig beskrivning");
            ValidateGreaterThan(issueid, 0,                                                  "Ogiltigt ärende");
            ValidateGreaterThan(createddate, 0,                                              "Ogiltigt datum/tid");
            ValidateCondition(Account.IsValidAuthz(createdauthz),                            "Ogiltig behörighet");
            ValidateRange(ACCESSTYPE_PRIVATE, accesstype, ACCESSTYPE_PUBLIC,                 "Ogiltig synlighet");
        }
    }
}