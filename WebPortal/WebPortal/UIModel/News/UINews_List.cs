using System;
using ServerLibrary.Model;
using ServerLibrary.Utils;
using WebPortal.Utils;

namespace WebPortal.UIModel
{
    public class UINews_List
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

        public UINews_List(News news, long now)
        {
            this.id            = news.id;
            this.categoryicon  = Images.NewsCategoryAsImage(news.category, news.id.ToString(), News.CategoryAsString(news.category));
            this.headline      = news.headline;
            this.speakicon     = (news.showfrom <= now && now <= news.showuntil) ? Images.SPEAK() : Images.EMPTY;
            this.message       = StringUtils.Shorten(news.message, 100);
            this.showfrom      = DateUtils.ConvertToDateString(news.showfrom);
            this.showuntil     = DateUtils.ConvertToDateString(news.showuntil);
            this.editcmdlink   = Images.EDIT();
            this.deletecmdlink = Images.DELETE();
        }
    }
}
