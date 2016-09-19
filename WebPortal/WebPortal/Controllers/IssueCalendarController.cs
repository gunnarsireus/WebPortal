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
    public class IssueCalendarController : BaseController
    {
        /* CRUDL operations */

        [HttpPost]
        public ActionResult ListIssues(int customer, int areatype, int status, int prio)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    IQueryable<Issue> query = IssueOperations.TryList(account, context).Where(i => i.status < Issue.__STATUS_ARCHIVED);

                    // Handle filtering
                    if (customer != Customer.CUSTOMER_ANY)
                    {
                        query = query.Where(i => i.customerid == customer);
                    }
                    if (areatype != Issue.AREATYPE_ANY)
                    {
                        query = query.Where(i => i.areatype == areatype);
                    }
                    if (status != Issue.STATUS_ANY)
                    {
                        query = query.Where(i => i.status == status);
                    }
                    if (prio != Issue.PRIO_ANY)
                    {
                        query = query.Where(i => i.prio == prio);
                    }

                    // Execute query
                    IList<Issue> dbms = query.ToList();

                    // Compose view models
                    IList<UIIssueCalendar_List> uims = new List<UIIssueCalendar_List>(dbms.Count);
                    var accounts = context.Accounts.Where(a => a.authz == Account.AUTHZ_TECHNICIAN && a.active == Account.ACTIVE).Select(a => new { a.id, a.firstname, a.lastname }).ToList();
                    foreach (Issue dbm in dbms)
                    {
                        var assignee = accounts.FirstOrDefault(a => a.id == dbm.assignedid);
                        string assigneename = assignee == null ? "--" : assignee.firstname + " " + assignee.lastname;
                        uims.Add(new UIIssueCalendar_List(dbm, assigneename));
                    }

                    return Json(uims);
                }
                catch (Exception e)
                {
                    base.HandleException("ListIssues", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult RescheduleIssue(UIIssueCalendar_U uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Issue dbm = IssueOperations.TryRead(account, context, uim.id);
                    // Update model
                    uim.UpdateModel(dbm);
                    IssueOperations.TryUpdate(account, context, dbm);
                    // Mark as unread
                    IssueOperations.TryDeleteReaders(account, context, dbm.id);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("RescheduleIssue", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }
    }
}
