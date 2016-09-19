using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerLibrary.Model
{
    [Table("offerreaders")]
    public class OfferReader
    {
        [Key]
        public long id         { get; set; }
        public int  offerid    { get; set; }
        public int  residentid { get; set; }
        public long date       { get; set; }

        public OfferReader()
        {
            this.id         = 0;
            this.offerid    = 0;
            this.residentid = 0;
            this.date       = 0;
        }
    }
}