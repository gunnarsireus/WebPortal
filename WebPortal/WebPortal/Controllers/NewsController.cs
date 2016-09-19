using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

using ServerLibrary.Model;
using ServerLibrary.Operations;
using ServerLibrary.Utils;

using WebPortal.UIModel;

namespace WebPortal.Controllers
{
    public class NewsController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            Account account = base.GetLoginAccount(false);
            if (account == null || !NewsOperations.CanRender(account))
            {
                return base.ToLoginPage();
            }
            return View();
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult ListNews(int draw, int start, int length, int customerid)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IQueryable<News> query = NewsOperations.TryList(account, context);

                    // Handle filtering (Customer.CUSTOMER_ANY means news is targeted to all customers)
                    query = query.Where(a => a.customerid == customerid);

                    // Possibly apply filter
                    var searchstring = Request["search[value]"];
                    if (!String.IsNullOrWhiteSpace(searchstring))
                    {
                        query = query.Where(n => n.headline.Contains(searchstring));
                    }
                    int recordsFiltered = query.Count();

                    // Execute query
                    IList<News> dbms = query.OrderBy(n => n.showfrom).ThenBy(n => n.showuntil).Skip(start).Take(length).ToList();
                    int recordsTotal = context.News.Count();

                    // Compose view models
                    IList<UINews_List> uims = new List<UINews_List>();
                    long now = DateUtils.TimeStamp;
                    foreach (News dbm in dbms)
                    {
                        uims.Add(new UINews_List(dbm, now));
                    }
                    return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, data = uims });
                }
                catch (Exception e)
                {
                    base.HandleException("ListNews", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult ReadNews(int id)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    News dbm = NewsOperations.TryRead(account, context, id);
                    return Json(new UINews_CRU(dbm));
                }
                catch (Exception e)
                {
                    base.HandleException("ReadNews", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult CreateNews(UINews_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    News model = uim.CreateModel(account);
                    NewsOperations.TryCreate(account, context, model);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("CreateNews", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult UpdateNews(UINews_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    News dbm = NewsOperations.TryRead(account, context, uim.id);
                    dbm = uim.UpdateModel(dbm, account);
                    NewsOperations.TryUpdate(account, context, dbm);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("UpdateNews", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult DeleteNews(int id)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    NewsOperations.TryDelete(account, context, id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("DeleteNews", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }
    }
}
