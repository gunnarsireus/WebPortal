using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

using ServerLibrary.Model;
using ServerLibrary.Operations;

using WebPortal.UIModel;

namespace WebPortal.Controllers
{
    public class ResidentController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            Account account = base.GetLoginAccount(false);
            if (account == null || !ResidentOperations.CanRender(account))
            {
                return base.ToLoginPage();
            }
            return View();
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult ListResidents(int draw, int start, int length, int customerid, int active)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IQueryable<Account> query = ResidentOperations.TryList(account, context);

                    // Handle filtering
                    query = query.Where(a => a.customerid == customerid && a.active == active);

                    // Possibly apply filter
                    var searchstring = Request["search[value]"];
                    if (!String.IsNullOrWhiteSpace(searchstring))
                    {
                        query = query.Where(a =>
                            a.firstname.Contains(searchstring) ||
                            a.lastname.Contains(searchstring) ||
                            a.email.Contains(searchstring));
                    }
                    int recordsFiltered = query.Count();

                    // Execute query
                    IList<Account> dbms = query.OrderBy(a => a.firstname).ThenBy(a => a.lastname).Skip(start).Take(length).ToList();
                    int recordsTotal = context.Accounts.Count();

                    // Compose view models
                    IList<UIResident_List> uims = new List<UIResident_List>();
                    foreach (Account dbm in dbms)
                    {
                        uims.Add(new UIResident_List(dbm));
                    }
                    return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, data = uims });
                }
                catch (Exception e)
                {
                    base.HandleException("ListResidents", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult ReadResident(int id)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Account dbm = ResidentOperations.TryRead(account, context, id);
                    return Json(new UIResident_CRU(dbm));
                }
                catch (Exception e)
                {
                    base.HandleException("ReadResident", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult CreateResident(UIResident_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Account model = uim.CreateModel();
                    ResidentOperations.TryCreate(account, context, model);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("CreateResident", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult UpdateResident(UIResident_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Account dbm = ResidentOperations.TryRead(account, context, uim.id);
                    dbm = uim.UpdateModel(dbm);
                    ResidentOperations.TryUpdate(account, context, dbm);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("UpdateResident", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult DeleteResident(int id)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    ResidentOperations.TryDelete(account, context, id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("DeleteResident", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult ChangePassword(UIResident_S uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Account dbm = ResidentOperations.TryRead(account, context, uim.id);
                    dbm = uim.UpdateModel(dbm);
                    ResidentOperations.TryUpdate(account, context, dbm);
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

        /* Select2 support */

        [HttpGet]
        public ActionResult SearchResidents(string filter, int customerid)
        {
            IList<UIResident_Search> uims = new List<UIResident_Search>();
            if (customerid != Customer.CUSTOMER_ANY)
            {
                using (var context = new DataContext())
                {
                    try
                    {
                        Account account = base.GetLoginAccount();
                        IQueryable<Account> query = ResidentOperations.TryList(account, context);
                        query = query.Where(a => a.active == Account.ACTIVE && a.customerid == customerid);
                        query = query.Where(a => a.firstname.Contains(filter) || a.lastname.Contains(filter));
                        IList<Account> dbms = query.OrderBy(a => a.firstname).ThenBy(a => a.lastname).ToList();
                        foreach (Account dbm in dbms)
                        {
                            uims.Add(new UIResident_Search(dbm));
                        }
                    }
                    catch (Exception e)
                    {
                        base.HandleException("SearchResidents", e);
                        return null;
                    }
                }
            }
            return Json(uims, JsonRequestBehavior.AllowGet);
        }
    }
}
