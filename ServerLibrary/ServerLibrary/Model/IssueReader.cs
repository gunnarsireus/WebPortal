using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerLibrary.Model
{
    [Table("issuereaders")]
    public class IssueReader
    {
        [Key]
        public long id        { get; set; }
        public int  issueid   { get; set; }
        public int  accountid { get; set; }
        public long date      { get; set; }

        public IssueReader()
        {
            this.id        = 0;
            this.issueid   = 0;
            this.accountid = 0;
            this.date      = 0;
        }
    }
}