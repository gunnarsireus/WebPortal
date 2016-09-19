using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServerLibrary.Utils;

namespace ServerLibrary.Model
{
    [Table("issueclasses")]
    public class IssueClass : Validatable
    {
        public const int ISSUECLASS_ANY = 0;

        // Database constraints
        public const int MINLEN_NAME = 2;
        public const int MAXLEN_NAME = 45;

        [Key]
        public int    id   { get; set; }
        public string name { get; set; }

        public IssueClass()
        {
            this.id   = 0;
            this.name = "";
        }
    
        public override void Validate()
        {
            name = ValidateRange(MINLEN_NAME, name, MAXLEN_NAME, "Felaktigt namn");
        }
    }
}