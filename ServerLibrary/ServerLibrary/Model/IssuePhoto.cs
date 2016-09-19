using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServerLibrary.Utils;

namespace ServerLibrary.Model
{
    [Table("issuephotos")]
    public class IssuePhoto : Validatable
    {
        public const int ISSUEPHOTO_ANY = 0;

        // Database constraints
        public const int MINLEN_CAPTION   = 0;
        public const int MAXLEN_CAPTION   = 45;
        public const int MINLEN_OSVERSION = 0;
        public const int MAXLEN_OSVERSION = 45;

        [Key]
        public int    id          { get; set; }
        public int    issueid     { get; set; }
        public string caption     { get; set; }
        public int    rotation    { get; set; }
        public int    platform    { get; set; }
        public string osversion   { get; set; }
        public string image       { get; set; }
        public int    createdby   { get; set; }
        public long   createddate { get; set; }

        public IssuePhoto()
        {
            this.id          = 0;
            this.issueid     = 0;
            this.caption     = "";
            this.rotation    = 0;
            this.platform    = 0;
            this.osversion   = "";
            this.image       = "";
            this.createdby   = 0;
            this.createddate = 0;
        }
    
        public override void Validate()
        {
            caption   = ValidateRange(MINLEN_CAPTION, caption, MAXLEN_CAPTION, "Felaktig rubrik");
            osversion = ValidateRange(MINLEN_OSVERSION, osversion, MAXLEN_OSVERSION, "Felaktig OS-version");
        }
    }
}