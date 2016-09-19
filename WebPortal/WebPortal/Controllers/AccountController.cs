using System;
using System.Collections.Generic;
using System.Web.Mvc;

using ServerLibrary.Model;
using ServerLibrary.Operations;
using ServerLibrary.Collections;

using WebPortal.UIModel;

namespace WebPortal.Controllers
{
    public class AccountController : BaseController
    {
        /* Security support */

        [HttpPost]
        public ActionResult SecureAccount(UIAccount_S uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account dbm = AccountOperations.TryRead(Account.EMPTY_ACCOUNT, context, uim.email);
                    dbm = uim.UpdateModel(dbm);
                    if (AccountOperations.TrySecure(Account.EMPTY_ACCOUNT, context, dbm, uim.PIN))
                    {
                        SetLoginAccount(dbm);
                        status.nextURL = "/Home";
                    }
                    else
                    {
                        uim.ResetModel(dbm);
                        status.SetError("Felaktig eller för gammal PIN-kod");
                    }
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("SecureAccount", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult ResendPIN(string email)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account dbm = AccountOperations.TryRead(Account.EMPTY_ACCOUNT, context, email);
                    AccountOperations.TryResendPIN(Account.EMPTY_ACCOUNT, context, dbm);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("ResendPIN", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        /* AJAX support */

        [HttpPost]
        public ActionResult GetActiveManagers()
        {
            using (var context = new DataContext())
            {
                try
                {
                    base.GetLoginAccount();
                    IList<CollectionOption> options = AccountCollections.GetAccounts(context, Account.AUTHZ_MANAGEMENT, Account.ACTIVE, CollectionOption.WITH_ANY);
                    return Json(options);
                }
                catch (Exception e)
                {
                    base.HandleException("GetActiveManagers", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult GetActiveTechnicians()
        {
            using (var context = new DataContext())
            {
                try
                {
                    base.GetLoginAccount();
                    IList<CollectionOption> options = AccountCollections.GetAccounts(context, Account.AUTHZ_TECHNICIAN, Account.ACTIVE, CollectionOption.WITH_ANY);
                    return Json(options);
                }
                catch (Exception e)
                {
                    base.HandleException("GetActiveTechnicians", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult GetDefaultAssignee(int customerid)
        {
            using (var context = new DataContext())
            {
                try
                {
                    base.GetLoginAccount();
                    CollectionOption option = new CollectionOption();
                    Customer customer = context.Customers.Find(customerid);
                    if (customer != null)
                    {
                        Account assignedto = context.Accounts.Find(customer.technicianid);
                        option.value = customer.technicianid.ToString();
                        option.text  = (assignedto == null) ? "--" : assignedto.Name;
                    }

                    return Json(option);
                }
                catch (Exception e)
                {
                    base.HandleException("GetDefaultAssignee", e);
                    return null;
                }
            }
        }
    }
}
