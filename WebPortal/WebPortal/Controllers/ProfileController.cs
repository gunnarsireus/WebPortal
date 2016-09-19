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
    public class ProfileController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            Account account = base.GetLoginAccount(false);
            if (account == null || !ProfileOperations.CanRender(account))
            {
                return base.ToLoginPage();
            }
            using (DataContext context = new DataContext())
            {
                // Info in database may be newer than in session
                account = context.Accounts.Find(account.id);
                Customer customer = account.customerid == Customer.CUSTOMER_ANY ? null : context.Customers.Find(account.customerid);

                ViewBag.UserName      = account.email;
                ViewBag.CustomerName  = (customer == null) ? "--" : customer.name;
                ViewBag.Authorization = Account.AuthzAsString(account.authz);
                ViewBag.LastLoginTime = DateUtils.ConvertToDateTimeString(account.lastlogin);
                return View();
            }
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult ReadProfile()
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Account dbm = ProfileOperations.TryRead(account, context, account.id);
                    return Json(new UIProfile_RU(dbm));
                }
                catch (Exception e)
                {
                    base.HandleException("ReadProfile", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateProfile(UIProfile_RU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Account dbm = ProfileOperations.TryRead(account, context, account.id);
                    dbm = uim.UpdateModel(dbm);
                    ProfileOperations.TryUpdate(account, context, dbm);
                    context.SaveChanges();
                    base.SetLoginAccount(dbm);
                }
                catch (Exception e)
                {
                    base.HandleException("UpdateProfile", e);
                    status.SetError(e.Message);
                }
                return Json(status);
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(UIProfile_S uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Account dbm = ProfileOperations.TryRead(account, context, account.id);
                    dbm = uim.UpdateModel(dbm);
                    ProfileOperations.TryUpdate(account, context, dbm);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("ChangePassword", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult DeleteProfile()
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    ProfileOperations.TryDelete(account, context, account.id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("DeleteProfile", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }
    }
}
