﻿using ServerLibrary.Model;
using ServerLibrary.Utils;

namespace WebPortal.UIModel
{
    public class UIOffer_CRU
    {
        public int    id            { get; set; }
        public int    customerid    { get; set; }
        public int    category      { get; set; }
        public string headline      { get; set; }
        public string message       { get; set; }
        public string author        { get; set; }
        public string sent          { get; set; }
        public string showfromdate  { get; set; }
        public string showfromtime  { get; set; }
        public string showuntildate { get; set; }
        public string showuntiltime { get; set; }

        public UIOffer_CRU()
        {
        }

        public UIOffer_CRU(Offer model)
        {
            this.id            = model.id;
            this.customerid    = model.customerid;
            this.category      = model.category;
            this.headline      = model.headline;
            this.message       = model.message;
            this.author        = model.author;
            this.sent          = DateUtils.ConvertToDateTimeString(model.sent);
            this.showfromdate  = DateUtils.ConvertToDateString(model.showfrom);
            this.showfromtime  = DateUtils.ConvertToTimeString(model.showfrom);
            this.showuntildate = DateUtils.ConvertToDateString(model.showuntil);
            this.showuntiltime = DateUtils.ConvertToTimeString(model.showuntil);
        }

        public Offer CreateModel(Account author)
        {
            return UpdateModel(new Offer(), author);
        }

        public Offer UpdateModel(Offer model, Account author)
        {
            model.customerid = this.customerid;
            model.category   = this.category;
            model.headline   = this.headline;
            model.message    = this.message;
            model.author     = author.Name;
            model.sent       = DateUtils.TimeStamp;
            model.showfrom   = DateUtils.ConvertToTimeStamp(this.showfromdate  + " " + this.showfromtime);
            model.showuntil  = DateUtils.ConvertToTimeStamp(this.showuntildate + " " + this.showuntiltime);
            return model;
        }
    }
}