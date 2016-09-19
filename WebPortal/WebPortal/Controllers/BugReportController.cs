using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using ServerLibrary.Model;
using ServerLibrary.Operations;

using WebPortal.UIModel;
using WebPortal.Utils;

namespace WebPortal.Controllers
{
    public class BugReportController : BaseController
    {
        /* Render */

        public ActionResult Index()
        {
            Account account = base.GetLoginAccount(false);
            if (account == null || !BugReportOperations.CanRender(account))
            {
                return base.ToLoginPage();
            }
            return View();
        }

        /* CRUDL operations */

        [HttpPost]
        public ActionResult CreateBugReport(UIBugReport_C uim)
        {
            AjaxStatus status = new AjaxStatus();
            using (var context = new DataContext())
            {
                try
                {
                    Account account = base.GetLoginAccount();
                    BugReport model = uim.CreateModel();
                    BugReportOperations.TryCreate(account, context, model);
                    SendBugReport(account, model, status);
                }
                catch (Exception e)
                {
                    base.HandleException("CreateBugReport", e);
                    status.SetError(e.Message);
                }
            }
            return Json(status);
        }

        /* Helpers */

        private const string TOKEN   = "2cd56cff01b042fe4f34461422a67f842d5e2f3a";
        private const string GITAPI  = "https://api.github.com/repos/balthazars/renew/issues";
        private const string ACCOUNT = "balthazars";
        private const string LABEL   = "Extern";

        public void SendBugReport(Account requester, BugReport bugreport, AjaxStatus status)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GITAPI);
            request.Headers.Add("Authorization", string.Format("token {0}", TOKEN));
            request.Method      = "POST";
            request.ContentType = "application/json";
            request.Accept      = "application/json";
            request.UserAgent   = ACCOUNT;

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var jsonString = new JavaScriptSerializer().Serialize(new
                    {
                        title  = bugreport.headline,
                        body   = bugreport.description + "\n\n[" + requester.Name + "]",
                        labels = new List<String>(new string[] { LABEL })
                    });
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var response = request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var deserializer = new JavaScriptSerializer();
                    var result       = streamReader.ReadToEnd();
                    var dict         = deserializer.Deserialize<Dictionary<string, object>>(result);
                    status.data1   = "Ärende: " + dict["number"];
                    status.data2   = "Rubrik: " + dict["title"];
                }
            }
            catch (Exception e)
            {
                TraceLog.Instance.LogError("SendBugReport() " + e.ToString());
                throw new ServerConflictException("Kunde inte skapa felanmälan, kontakta oss via e-post");
            }
        }
    }
}
