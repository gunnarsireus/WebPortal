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
    public class CustomerController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            Account account = base.GetLoginAccount(false);
            if (account == null || !CustomerOperations.CanRender(account))
            {
                return base.ToLoginPage();
            }
            return View();
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult ListCustomers(int draw, int start, int length, int managementid, int technicianid, int active)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IQueryable<Customer> query = CustomerOperations.TryList(account, context).Where(c => c.active == active);

                    // Handle filtering
                    if (managementid != Account.ACCOUNT_ANY)
                    {
                        query = query.Where(c => c.managementid == managementid);
                    }
                    if (technicianid != Account.ACCOUNT_ANY)
                    {
                        query = query.Where(c => c.technicianid == technicianid);
                    }

                    // Possibly apply filter
                    var searchstring = Request["search[value]"];
                    if (!String.IsNullOrWhiteSpace(searchstring))
                    {
                        query = query.Where(c =>
                            c.name.Contains(searchstring)      ||
                            c.orgnumber.Contains(searchstring) ||
                            c.address.Contains(searchstring));
                    }
                    int recordsFiltered = query.Count();

                    // Execute query
                    IList<Customer> dbms = query.OrderBy(c => c.name).Skip(start).Take(length).ToList();
                    int recordsTotal = context.Customers.Count();

                    // Compose view models
                    IList<UICustomer_List> uims = new List<UICustomer_List>(dbms.Count);
                    foreach (Customer dbm in dbms)
                    {
                        uims.Add(new UICustomer_List(dbm));
                    }
                    return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, data = uims });
                }
                catch (Exception e)
                {
                    base.HandleException("ListCustomers", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult ReadCustomer(int id)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Customer dbm = CustomerOperations.TryRead(account, context, id);
                    return Json(new UICustomer_CRU(dbm));
                }
                catch (Exception e)
                {
                    base.HandleException("ReadCustomer", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult CreateCustomer(UICustomer_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Customer model = uim.CreateModel();
                    CustomerOperations.TryCreate(account, context, model);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("CreateCustomer", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult UpdateCustomer(UICustomer_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Customer dbm = CustomerOperations.TryRead(account, context, uim.id);
                    dbm = uim.UpdateModel(dbm);
                    CustomerOperations.TryUpdate(account, context, dbm);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("UpdateCustomer", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int id)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    CustomerOperations.TryDelete(account, context, id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("DeleteCustomer", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        /* AJAX support */

        [HttpPost]
        public ActionResult GetAllCompanyCustomers()
        {
            using (var context = new DataContext())
            {
                try
                {
                    base.GetLoginAccount();
                    IList<CollectionOption> options = CustomerCollections.GetAllCustomers(context, Customer.TYPE_COMPANY, CollectionOption.WITH_ANY);
                    return Json(options);
                }
                catch (Exception e)
                {
                    base.HandleException("GetAllCompanyCustomers", e);
                    return null;
                }
            }
        }

        /* Select2 support */

        [HttpGet]
        public ActionResult SearchCustomers(string filter)
        {
            using (var context = new DataContext())
            {
                try
                {
                    IList<Customer> dbms = context.Customers.Where(c => c.active == Customer.ACTIVE).ToList();
                    dbms = dbms.Where(c => c.name.Matches(filter) || c.address.Matches(filter) || c.orgnumber.Matches(filter)).OrderBy(c => c.name).ToList();
                    IList<UICustomer_Search> uims = new List<UICustomer_Search>(dbms.Count);
                    foreach (Customer dbm in dbms)
                    {
                        uims.Add(new UICustomer_Search(dbm));
                    }
                    return Json(uims, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    base.HandleException("SearchCustomers", e);
                    return null;
                }
            }
        }
    }
}
