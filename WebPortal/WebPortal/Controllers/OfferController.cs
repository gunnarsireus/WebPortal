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
    public class OfferController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            Account account = base.GetLoginAccount(false);
            if (account == null || !OfferOperations.CanRender(account))
            {
                return base.ToLoginPage();
            }
            return View();
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult ListOffers(int draw, int start, int length, int customerid)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IQueryable<Offer> query = OfferOperations.TryList(account, context);

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
                    IList<Offer> dbms = query.OrderBy(n => n.showfrom).ThenBy(n => n.showuntil).Skip(start).Take(length).ToList();
                    int recordsTotal = context.Offers.Count();

                    // Compose view models
                    IList<UIOffer_List> uims = new List<UIOffer_List>();
                    long now = DateUtils.TimeStamp;
                    foreach (Offer dbm in dbms)
                    {
                        uims.Add(new UIOffer_List(dbm, now));
                    }
                    return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, data = uims });
                }
                catch (Exception e)
                {
                    base.HandleException("ListOffers", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult ReadOffer(int id)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Offer dbm = OfferOperations.TryRead(account, context, id);
                    return Json(new UIOffer_CRU(dbm));
                }
                catch (Exception e)
                {
                    base.HandleException("ReadOffer", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult CreateOffer(UIOffer_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Offer model = uim.CreateModel(account);
                    OfferOperations.TryCreate(account, context, model);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("CreateOffer", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult UpdateOffer(UIOffer_CRU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Offer dbm = OfferOperations.TryRead(account, context, uim.id);
                    dbm = uim.UpdateModel(dbm, account);
                    OfferOperations.TryUpdate(account, context, dbm);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("UpdateOffer", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult DeleteOffer(int id)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    OfferOperations.TryDelete(account, context, id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("DeleteOffer", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }
    }
}
