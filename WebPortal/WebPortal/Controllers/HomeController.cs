using System.Web.Mvc;
using ServerLibrary.Model;
using ServerLibrary.Operations;

namespace WebPortal.Controllers
{
    public class HomeController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            Account account = base.GetLoginAccount(false);
            if (account == null || !HomeOperations.CanRender(account))
            {
                return base.ToLoginPage();
            }
            return View();
        }
    }
}
