using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerLibrary.Model
{
    [Table("issuetimes")]
    public class IssueTime : Validatable
    {
        public const int ISSUETIME_ANY = 0;

        [Key]
        public int  id          { get; set; }
        public int  issueid     { get; set; }
        public int  timetypeid  { get; set; }
        public long starttime   { get; set; }
        public long endtime     { get; set; }
        public int  createdby   { get; set; }
        public long createddate { get; set; }

        public IssueTime()
        {
            this.id          = 0;
            this.issueid     = 0;
            this.timetypeid  = 0;
            this.starttime   = 0;
            this.endtime     = 0;
            this.createdby   = 0;
            this.createddate = 0;
        }

        public override void Validate()
        {
            ValidateCondition(timetypeid != TimeType.TIMETYPE_ANY, "Tidtyp måste anges");
            ValidateGreaterThan(starttime, 0, "Felaktig starttid");
            ValidateGreaterThan(endtime,   0, "Felaktig sluttid");
            ValidateDateTimePeriod(starttime, endtime, "Starttid kan inte vara senare än sluttid");
        }
    }
}