using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerLibrary.Model
{
    [Table("issuetransitions")]
    public class IssueTransition : Validatable
    {
        [Key]
        public int    id          { get; set; }
        public int    issueid     { get; set; }
        public int    fromstatus  { get; set; }
        public int    tostatus    { get; set; }
        public int    createdby   { get; set; }
        public string createdname { get; set; }
        public long   createddate { get; set; }

        public IssueTransition()
        {
            this.id          = 0;
            this.issueid     = 0;
            this.fromstatus  = 0;
            this.tostatus    = 0;
            this.createdby   = 0;
            this.createdname = "";
            this.createddate = 0;
        }

        public override void Validate()
        {
            ValidateCondition(issueid != 0,                     "Ogitligt ärendenummer");
            ValidateCondition(Issue.IsValidStatus(fromstatus),  "Ogiltigt från-status");
            ValidateCondition(Issue.IsValidStatus(tostatus),    "Ogiltigt till-status");
            ValidateCondition(createdby != Account.ACCOUNT_ANY, "Ogiltig utförare");
            ValidateGreaterThan(createddate, 0,                 "Ogiltigt datum");
        }
    }
}