using System;
using System.Web.Mvc;

using ServerLibrary.Model;
using ServerLibrary.Operations;

using WebPortal.UIModel;

namespace WebPortal.Controllers
{
    public class LoginController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            if (!LoginOperations.CanRender(Account.EMPTY_ACCOUNT))
            {
                return base.ToLoginPage();
            }
            return View();
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult ValidateAccount(UIAccount_V uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account dbm = LoginOperations.TryRead(Account.EMPTY_ACCOUNT, context, uim.email);
                    dbm = LoginOperations.TryValidate(dbm, context, uim.password);
                    if (dbm == null)
                    {
                        status.SetError("Felaktig användare eller lösenord");
                    }
                    else
                    {
                        SetLoginAccount(dbm);
                        status.nextURL = "/Home";
                    }
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("ValidateAccount", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult ForgotAccount(UIAccount_F uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account dbm = uim.CreateModel();
                    Account requester = LoginOperations.TryRead(Account.EMPTY_ACCOUNT, context, dbm.email);
                    LoginOperations.TrySendPIN(requester, context, Account.EMPTY_ACCOUNT);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("ForgotAccount", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }
    }
}
