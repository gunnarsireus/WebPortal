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
    public class IssueListController : BaseController
    {
        /* CRUDL operations */

        [HttpPost]
        public ActionResult ListIssues(int draw, int start, int length, int customer, int areatype, int status, int prio)
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

                    // Possibly apply filter
                    var searchstring = Request["search[value]"];
                    if (!String.IsNullOrWhiteSpace(searchstring))
                    {
                        query = query.Where(i =>
                            i.id.ToString().StartsWith(searchstring) ||
                            i.name.Matches(searchstring)             ||
                            i.address.Matches(searchstring));
                    }
                    int recordsFiltered = query.Count();

                    // Execute query
                    IList<Issue> dbms = query.OrderBy(i => i.id).Skip(start).Take(length).ToList();
                    int recordsTotal = context.Issues.Count();

                    // Compose view models
                    IList<UIIssueList_List> uims = new List<UIIssueList_List>(dbms.Count);
                    var accounts = context.Accounts.Where(a => a.authz == Account.AUTHZ_TECHNICIAN && a.active == Account.ACTIVE).Select(a => new { a.id, a.firstname, a.lastname }).ToList();
                    foreach (Issue dbm in dbms)
                    {
                        var assignee = accounts.FirstOrDefault(a => a.id == dbm.assignedid);
                        string assigneename = assignee == null ? "--" : assignee.firstname + " " + assignee.lastname;
                        uims.Add(new UIIssueList_List(dbm, assigneename));
                    }

                    return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, data = uims });
                }
                catch (Exception e)
                {
                    base.HandleException("ListIssues", e);
                    return null;
                }
            }
        }
    }
}
