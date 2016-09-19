using System;
using System.Web.Mvc;

using ServerLibrary.Model;
using ServerLibrary.Operations;

using WebPortal.UIModel;

namespace WebPortal.Controllers
{
    public class RegisterController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            if (!RegisterOperations.CanRender(Account.EMPTY_ACCOUNT))
            {
                return base.ToLoginPage();
            }
            return View();
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult CreateAccount(UIAccount_C uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account model = uim.CreateModel();
                    RegisterOperations.TryCreate(Account.EMPTY_ACCOUNT, context, model);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("CreateAccount", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }
    }
}