using System;
using System.Web.Mvc;

using ServerLibrary.Model;
using WebPortal.Utils;

namespace WebPortal.Controllers
{
    public class BaseController : Controller
    {
        protected Account GetLoginAccount(bool ajaxcall = true)
        {
            Account account = VerifyLogin(ajaxcall);
            if (account == null && ajaxcall)
            {
                throw new ServerAuthorizeException("Du är inte längre inloggad");
            }
            return account;
        }

        protected void HandleException(string source, Exception e)
        {
            if (e is ServerAuthorizeException)
            {
                Response.StatusCode = 403;
                Response.StatusDescription = e.Message;
                Response.End();
            }
            TraceLog.Instance.LogError(source + ": " + e.Message);
        }

        /* Configuration */

        protected void SetLoginAccount(Account account)
        {
            Session["account"] = account;
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session["account"] = null;
            return ToLoginPage();
        }

        protected ActionResult ToLoginPage()
        {
            return Redirect("~/Login");
        }

        private Account VerifyLogin(bool ajaxcall)
        {
#if DEBUGGO_MCBRAIN
            Account account   = new Account();
            account.email     = "admin@renewservice.se";
            account.active    = Account.ACTIVE;
            account.firstname = "Debuggo";
            account.lastname  = "McBrain";
            account.authz     = Account.AUTHZ_ADMIN;
            account.id        = 1;
#else
            Account account = (Account)Session["account"];
#endif
            if (account != null)
            {
                ViewBag.LoggedInAs          = account.Name;
                ViewBag.Authz               = account.authz;
                ViewBag.IsAdmin             = account.IsAdmin();
                ViewBag.IsManagement        = account.IsManagement();
                ViewBag.IsTechnician        = account.IsTechnician();
                ViewBag.IsCustomer          = account.IsCustomer();
                ViewBag.IsResident          = account.IsResident();
                ViewBag.IsAtLeastAdmin      = account.IsAtLeastAdmin();
                ViewBag.IsAtLeastManagement = account.IsAtLeastManagement();
                ViewBag.IsAtLeastTechnician = account.IsAtLeastTechnician();
                ViewBag.IsAtLeastCustomer   = account.IsAtLeastCustomer();
                ViewBag.IsAtLeastResident   = account.IsAtLeastResident();
                ViewBag.IsAtMostAdmin       = account.IsAtMostAdmin();
                ViewBag.IsAtMostManagement  = account.IsAtMostManagement();
                ViewBag.IsAtMostTechnician  = account.IsAtMostTechnician();
                ViewBag.IsAtMostCustomer    = account.IsAtMostCustomer();
                ViewBag.IsAtMostResident    = account.IsAtMostResident();
                return account;
            }
            if (ajaxcall)
            {
                Response.StatusCode = 401;
                Response.StatusDescription = "Du är inte längre inloggad";
                Response.End();
            }
            return null;
        }
    }
}
