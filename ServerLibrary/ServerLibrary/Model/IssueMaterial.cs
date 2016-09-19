using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ServerLibrary.Utils;

namespace ServerLibrary.Model
{
    [Table("issuematerials")]
    public class IssueMaterial : Validatable
    {
        public const int ISSUEMATERIAL_ANY = 0;

        // Database constraints
        public const int MINLEN_DESCRIPTION = 2;
        public const int MAXLEN_DESCRIPTION = 200;

        [Key]
        public int     id          { get; set; }
        public int     issueid     { get; set; }
        public string  description { get; set; }
        public double  amount      { get; set; }
        public int     createdby   { get; set; }
        public long    createddate { get; set; }
        public decimal price       { get; set; }

        public IssueMaterial()
        {
            this.id          = 0;
            this.issueid     = 0;
            this.description = "";
            this.amount      = 0;
            this.createdby   = 0;
            this.createddate = 0;
            this.price       = 0;
        }

        public override void Validate()
        {
            description = ValidateRange(MINLEN_DESCRIPTION, description, MAXLEN_DESCRIPTION, "Felaktig beskrivning");
        }
    }
}