using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

using ServerLibrary.Model;
using ServerLibrary.Operations;
using ServerLibrary.Collections;

using WebPortal.UIModel;

namespace WebPortal.Controllers
{
    public class EmployeeController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            Account account = base.GetLoginAccount(false);
            if (account == null || !EmployeeOperations.CanRender(account))
            {
                return base.ToLoginPage();
            }
            return View();
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult ListEmployees(int draw, int start, int length, int authz, int active)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IQueryable<Account> query = EmployeeOperations.TryList(account, context);

                    // Handle filtering
                    query = query.Where(a => a.active == active);
                    if (authz != Account.AUTHZ_ANY)
                    {
                        query = query.Where(a => a.authz == authz);
                    }

                    // Possibly apply filter
                    var searchstring = Request["search[value]"];
                    if (!String.IsNullOrWhiteSpace(searchstring))
                    {
                        query = query.Where(a =>
                            a.firstname.Contains(searchstring) ||
                            a.lastname.Contains(searchstring));
                    }
                    int recordsFiltered = query.Count();

                    // Execute query
                    IList<Account> dbms = query.OrderBy(a => a.firstname).ThenBy(a => a.lastname).Skip(start).Take(length).ToList();
                    int recordsTotal = context.Accounts.Count();

                    // Compose view models
                    IList<UIEmployee_List> uims = new List<UIEmployee_List>();
                    foreach (Account dbm in dbms)
                    {
                        uims.Add(new UIEmployee_List(dbm));
                    }
                    return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, data = uims });
                }
                catch (Exception e)
                {
                    base.HandleException("ListEmployees", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult ReadEmployee(int id)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Account dbm = EmployeeOperations.TryRead(account, context, id);
                    return Json(new UIEmployee_CRU(dbm));
                }
                catch (Exception e)
                {
                    base.HandleException("ReadEmployee", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult CreateEmployee(UIEmployee_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Account model = uim.CreateModel();
                    EmployeeOperations.TryCreate(account, context, model);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("CreateEmployee", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult UpdateEmployee(UIEmployee_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Account dbm = EmployeeOperations.TryRead(account, context, uim.id);
                    dbm = uim.UpdateModel(dbm);
                    EmployeeOperations.TryUpdate(account, context, dbm);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("UpdateEmployee", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    EmployeeOperations.TryDelete(account, context, id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("DeleteEmployee", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult ChangePassword(UIEmployee_S uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Account dbm = EmployeeOperations.TryRead(account, context, uim.id);
                    dbm = uim.UpdateModel(dbm);
                    EmployeeOperations.TryUpdate(account, context, dbm);
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
    }
}
