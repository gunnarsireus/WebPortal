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
    public class IssueClassController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            Account account = base.GetLoginAccount(false);
            if (account == null || !IssueClassOperations.CanRender(account))
            {
                return base.ToLoginPage();
            }
            return View();
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult ListIssueClasses(int draw, int start, int length)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IQueryable<IssueClass> query = IssueClassOperations.TryList(account, context);

                    // Possibly apply filter
                    var searchstring = Request["search[value]"];
                    if (!String.IsNullOrWhiteSpace(searchstring))
                    {
                        query = query.Where(c => c.name.Contains(searchstring));
                    }
                    int recordsFiltered = query.Count();

                    // Execute query
                    IList<IssueClass> dbms = query.OrderBy(c => c.name).Skip(start).Take(length).ToList();
                    int recordsTotal = context.IssueClasses.Count();

                    // Compose view models
                    IList<UIIssueClass_List> uims = new List<UIIssueClass_List>(dbms.Count);
                    foreach (IssueClass dbm in dbms)
                    {
                        uims.Add(new UIIssueClass_List(dbm));
                    }
                    return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, data = uims });
                }
                catch (Exception e)
                {
                    base.HandleException("ListIssueClasses", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult ReadIssueClass(int id)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IssueClass dbm = IssueClassOperations.TryRead(account, context, id);
                    return Json(new UIIssueClass_CRU(dbm));
                }
                catch (Exception e)
                {
                    base.HandleException("ReadIssueClass", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult CreateIssueClass(UIIssueClass_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IssueClass model = uim.CreateModel();
                    IssueClassOperations.TryCreate(account, context, model);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("CreateIssueClass", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult UpdateIssueClass(UIIssueClass_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IssueClass dbm = IssueClassOperations.TryRead(account, context, uim.id);
                    dbm = uim.UpdateModel(dbm);
                    IssueClassOperations.TryUpdate(account, context, dbm);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("UpdateIssueClass", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult DeleteIssueClass(int id)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IssueClassOperations.TryDelete(account, context, id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("DeleteIssueClass", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }


        /* AJAX support */

        [HttpPost]
        public ActionResult GetIssueClasses()
        {
            using (var context = new DataContext())
            {
                try
                {
                    base.GetLoginAccount();
                    IList<CollectionOption> options = IssueCollections.GetIssueClasses(context, CollectionOption.WITH_ANY);
                    return Json(options);
                }
                catch (Exception e)
                {
                    base.HandleException("GetIssueClasses", e);
                    return null;
                }
            }
        }
    }
}