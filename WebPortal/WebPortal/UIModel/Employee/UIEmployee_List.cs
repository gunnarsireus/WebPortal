using System;
using ServerLibrary.Model;
using ServerLibrary.Utils;
using WebPortal.Utils;

namespace WebPortal.UIModel
{
    public class UIEmployee_List
    {
        public int    id        { get; set; }
        public string email     { get; set; }
        public string authz     { get; set; }
        public string firstname { get; set; }
        public string lastname  { get; set; }
        public string lastlogin { get; set; }

        /* Commands */
        public string editcmdlink   { get; set; }
        public string securecmdlink { get; set; }
        public string deletecmdlink { get; set; }

        public UIEmployee_List(Account model)
        {
            this.id            = model.id;
            this.email         = model.email;
            this.authz         = Account.AuthzAsString(model.authz);
            this.firstname     = model.firstname;
            this.lastname      = model.lastname;
            this.lastlogin     = DateUtils.ConvertToDateString(model.lastlogin);
            this.editcmdlink   = Images.EDIT();
            this.securecmdlink = Images.SECURE();
            this.deletecmdlink = Images.DELETE();
        }
    }
}