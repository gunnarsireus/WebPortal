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
    public class TimeTypeController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            Account account = base.GetLoginAccount(false);
            if (account == null || !TimeTypeOperations.CanRender(account))
            {
                return base.ToLoginPage();
            }
            return View();
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult ListTimeTypes(int draw, int start, int length)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IQueryable<TimeType> query = TimeTypeOperations.TryList(account, context);

                    // Possibly apply filter
                    var searchstring = Request["search[value]"];
                    if (!String.IsNullOrWhiteSpace(searchstring))
                    {
                        query = query.Where(c => c.name.Contains(searchstring));
                    }
                    int recordsFiltered = query.Count();

                    // Execute query
                    IList<TimeType> dbms = query.OrderBy(c => c.code).Skip(start).Take(length).ToList();
                    int recordsTotal = context.TimeTypes.Count();

                    // Compose view models
                    IList<UITimeType_List> uims = new List<UITimeType_List>(dbms.Count);
                    foreach (TimeType dbm in dbms)
                    {
                        uims.Add(new UITimeType_List(dbm));
                    }
                    return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, data = uims });
                }
                catch (Exception e)
                {
                    base.HandleException("ListTimeTypes", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult ReadTimeType(int id)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    TimeType dbm = TimeTypeOperations.TryRead(account, context, id);
                    return Json(new UITimeType_RU(dbm));
                }
                catch (Exception e)
                {
                    base.HandleException("ReadTimeType", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult SyncTimeTypes()
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    // Fake code:
                    context.TimeTypes.RemoveRange(context.TimeTypes.ToList());
                    IList<TimeType> addons = new List<TimeType>(10);
                    addons.Add(new TimeType { code = "003", name = "Arbetskostnad",                unit = "Tim", description = "Vardagar 07-19", active = TimeType.ACTIVE });
                    addons.Add(new TimeType { code = "008", name = "Materialkostnad",              unit = "Kr",  description = "Per enhet", active = TimeType.ACTIVE });
                    addons.Add(new TimeType { code = "011", name = "Förvaltare",                   unit = "Tim", description = "Timdebitering ex raster", active = TimeType.ACTIVE });
                    addons.Add(new TimeType { code = "020", name = "Miljö- och deponeringsavgift", unit = "Kr",  description = "Vardagar 07-19", active = TimeType.ACTIVE });
                    addons.Add(new TimeType { code = "022", name = "Tippavgift",                   unit = "Kr",  description = "Sörab Väsby/Smedby", active = TimeType.INACTIVE });
                    addons.Add(new TimeType { code = "021", name = "Underentreprenör",             unit = "Kr",  description = "Fast pris", active = TimeType.ACTIVE });
                    addons.Add(new TimeType { code = "030", name = "Felsökning",                   unit = "Tim", description = "Vardagar 07-19", active = TimeType.ACTIVE });
                    addons.Add(new TimeType { code = "033", name = "Jour",                         unit = "Tim", description = "Helger och kvällar", active = TimeType.ACTIVE });
                    addons.Add(new TimeType { code = "036", name = "Besiktning",                   unit = "St",  description = "Per tillfälle", active = TimeType.ACTIVE });
                    addons.Add(new TimeType { code = "037", name = "Arbetsmaskin/Traktor",         unit = "Tim", description = "Alla tider", active = TimeType.ACTIVE });
                    context.TimeTypes.AddRange(addons);
                    // TODO:
                    //TimeType model = uim.CreateModel();
                    //TimeTypeOperations.TryCreate(account, context, model);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("CreateTimeType", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult UpdateTimeType(UITimeType_RU uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    TimeType dbm = TimeTypeOperations.TryRead(account, context, uim.id);
                    dbm = uim.UpdateModel(dbm);
                    TimeTypeOperations.TryUpdate(account, context, dbm);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("UpdateTimeType", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult DeleteTimeType(int id)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    TimeTypeOperations.TryDelete(account, context, id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("DeleteTimeType", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }
    }
}
