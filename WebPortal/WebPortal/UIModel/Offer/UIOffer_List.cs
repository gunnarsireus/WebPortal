using System;
using ServerLibrary.Model;
using ServerLibrary.Utils;
using WebPortal.Utils;

namespace WebPortal.UIModel
{
    public class UIOffer_List
    {
        public string headline     { get; set; }
        public string categoryicon { get; set; }
        public string speakicon    { get; set; }
        public string showfrom     { get; set; }
        public string showuntil    { get; set; }

        /* Hidden */
        public long   id      { get; set; }
        public string message { get; set; }

        /* Commands */
        public string editcmdlink   { get; set; }
        public string deletecmdlink { get; set; }

        public UIOffer_List(Offer offer, long now)
        {
            this.id            = offer.id;
            this.categoryicon  = Images.OfferCategoryAsImage(offer.category, offer.id.ToString(), Offer.CategoryAsString(offer.category));
            this.headline      = offer.headline;
            this.speakicon     = (offer.showfrom <= now && now <= offer.showuntil) ? Images.SPEAK() : Images.EMPTY;
            this.message       = StringUtils.Shorten(offer.message, 100);
            this.showfrom      = DateUtils.ConvertToDateString(offer.showfrom);
            this.showuntil     = DateUtils.ConvertToDateString(offer.showuntil);
            this.editcmdlink   = Images.EDIT();
            this.deletecmdlink = Images.DELETE();
        }
    }
}
