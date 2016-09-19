using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

using ServerLibrary.Model;
using ServerLibrary.Utils;
using ServerLibrary.Operations;
using ServerLibrary.Collections;

using WebPortal.UIModel;

namespace WebPortal.Controllers
{
    public class IssueFeedbackController : BaseController
    {
        /* CRUDL operations */

        [HttpPost]
        public ActionResult CreateIssueFeedback(UIIssueFeedback_C uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IssueFeedback model = uim.CreateModel(account);
                    IssueFeedbackOperations.TryCreate(account, context, model);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("CreateIssueFeedback", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }
    }
}
