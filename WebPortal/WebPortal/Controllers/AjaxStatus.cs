using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.Controllers
{
    public class AjaxStatus
    {
        public bool   success { get; set; }
        public string message { get; set; }
        public string nextURL { get; set; }
        public string data1   { get; set; }
        public string data2   { get; set; }

        public AjaxStatus()
        {
            success = true;
            message = "";
            nextURL = "";
            data1   = "";
            data2   = "";
        }

        public void SetSuccess(string message)
        {
            this.success = true;
            this.message = message;
        }

        public void SetError(string error)
        {
            this.success = false;
            this.message = error;
        }
    }
}
