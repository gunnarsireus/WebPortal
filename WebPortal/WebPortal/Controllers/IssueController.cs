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
    public class IssueController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            Account account = base.GetLoginAccount(false);
            if (account == null || !IssueOperations.CanRender(account))
            {
                return base.ToLoginPage();
            }
            return View();
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult ReadIssue(int id)
        {
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Issue                  dbm         = IssueOperations.TryRead(account, context, id);
                    Customer               customer    = CustomerOperations.TryRead(account, context, dbm.customerid);
                    IList<IssueFeedback>   feedbacks   = IssueFeedbackOperations.TryList(account, context, dbm.id).ToList();
                    IList<IssueTransition> transitions = IssueTransitionOperations.TryList(account, context, dbm.id).ToList();
                    return Json(new UIIssue_R(dbm, customer, feedbacks, transitions));
                }
                catch (Exception e)
                {
                    base.HandleException("ReadIssue", e);
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult CreateIssue(UIIssue_C uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Account account = base.GetLoginAccount();
                        Issue dbm = uim.CreateModel();
                        IssueOperations.TryCreate(account, context, dbm);
                        context.SaveChanges();
                        IssueTransition transition = uim.CreateModel(dbm, account);
                        IssueTransitionOperations.TryCreate(account, context, transition);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        base.HandleException("CreateIssue", e);
                        status.SetError(e.Message);
                    }
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult UpdateIssue(UIIssueProblem_U uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    Issue dbm = IssueOperations.TryRead(account, context, uim.issueid);
                    dbm = uim.UpdateModel(dbm);
                    IssueOperations.TryUpdate(account, context, dbm);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    base.HandleException("UpdateIssue", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        [HttpPost]
        public ActionResult UpdateIssueStatus(UIIssueStatus_U uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Account account = base.GetLoginAccount();
                        Issue dbm = IssueOperations.TryRead(account, context, uim.id);
                        // Create transition
                        IssueTransition transition = uim.CreateModel(dbm, account);
                        IssueTransitionOperations.TryCreate(account, context, transition);
                        // Update model
                        uim.UpdateModel(dbm);
                        IssueOperations.TryUpdate(account, context, dbm);
                        // Mark as unread
                        IssueOperations.TryDeleteReaders(account, context, dbm.id);
                        // TODO: If new status is to archive, move to archived tables
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        base.HandleException("UpdateIssueStatus", e);
                        status.SetError(e.Message);
                    }
                }
            }
            return Json(status);
        }
    }
}