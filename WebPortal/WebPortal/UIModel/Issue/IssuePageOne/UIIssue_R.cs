using System.Collections.Generic;
using ServerLibrary.Model;
using ServerLibrary.Utils;
using WebPortal.Utils;

namespace WebPortal.UIModel
{
    public class UIIssue_R
    {
        public struct IssueFeedbackStruct
        {
            public string description;
            public string accessicon;
            public string name;
            public string date;
            public string time;

            public IssueFeedbackStruct(IssueFeedback feedback)
            {
                this.description = feedback.description;
                this.accessicon  = (feedback.accesstype == IssueFeedback.ACCESSTYPE_PUBLIC) ? "" : Images.PRIVATE();
                this.name        = feedback.createdname;
                this.date        = DateUtils.ConvertToDateString(feedback.createddate);
                this.time        = DateUtils.ConvertToTimeString(feedback.createddate);
            }
        }

        public struct IssueTransitionStruct
        {
            public string fromstatus;
            public string tostatus;
            public string statusicon;
            public string name;
            public string date;
            public string time;

            public IssueTransitionStruct(IssueTransition transition)
            {
                this.fromstatus = Issue.StatusAsString(transition.fromstatus);
                this.tostatus   = Issue.StatusAsString(transition.tostatus);
                this.statusicon = Images.StatusAsImage(transition.tostatus);
                this.name       = transition.createdname;
                this.date       = DateUtils.ConvertToDateString(transition.createddate);
                this.time       = DateUtils.ConvertToTimeString(transition.createddate);
            }
        }

        public struct IssueProblemStruct
        {
            public int    issueid;
            public string name;
            public int    assignedid;
            public string startdatestring;
            public string starttimestring;
            public string enddatestring;
            public string endtimestring;
            public int    issueclassid;
            public int    prio;
            public int    responsible;
            public int    areatype;
            public string description;
            public string customername;

            public IssueProblemStruct(Issue issue, Customer customer)
            {
                this.issueid         = issue.id;
                this.name            = issue.name;
                this.assignedid      = issue.assignedid;
                this.startdatestring = DateUtils.ConvertToDateString(issue.startdate);
                this.starttimestring = DateUtils.ConvertToTimeString(issue.startdate);
                this.enddatestring   = DateUtils.ConvertToDateString(issue.enddate);
                this.endtimestring   = DateUtils.ConvertToTimeString(issue.enddate);
                this.issueclassid    = issue.issueclassid;
                this.prio            = issue.prio;
                this.responsible     = issue.responsible;
                this.areatype        = issue.areatype;
                this.description     = issue.description;
                this.customername    = (customer == null) ? "--" : customer.name;
            }
        }

        /*        
        public int    residentid      { get; set; }
        public int    responsible     { get; set; }
        public string firstname       { get; set; }
        public string lastname        { get; set; }
        public string phone           { get; set; }
        public string email           { get; set; }
        public string address         { get; set; }
        public string floor           { get; set; }
        public string apartment       { get; set; }
        */
        public IssueProblemStruct           problem;
        public IList<IssueFeedbackStruct>   feedbacks;
        public IList<IssueTransitionStruct> transitions;

        public UIIssue_R(Issue issue, Customer customer, IList<IssueFeedback> feedbacks, IList<IssueTransition> transitions)
        {
            this.problem     = new IssueProblemStruct(issue, customer);
            this.feedbacks   = new List<IssueFeedbackStruct>(feedbacks.Count);
            this.transitions = new List<IssueTransitionStruct>(transitions.Count);
            foreach (IssueFeedback feedback in feedbacks)
            {
                this.feedbacks.Add(new IssueFeedbackStruct(feedback));
            }
            foreach (IssueTransition transition in transitions)
            {
                this.transitions.Add(new IssueTransitionStruct(transition));
            }
        }
    }
}