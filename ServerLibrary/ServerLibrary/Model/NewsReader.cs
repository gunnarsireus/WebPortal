using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerLibrary.Model
{
    [Table("newsreaders")]
    public class NewsReader
    {
        [Key]
        public long id         { get; set; }
        public int  newsid     { get; set; }
        public int  residentid { get; set; }
        public long date       { get; set; }

        public NewsReader()
        {
            this.id         = 0;
            this.newsid     = 0;
            this.residentid = 0;
            this.date       = 0;
        }
    }
}