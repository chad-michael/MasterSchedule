using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

public partial class Registar : System.Web.UI.Page
{
    private ClsChangeLog log;

    protected void Page_Load(object sender, EventArgs e)
    {
        log = new ClsChangeLog("MasterSchedule");

        if (!Page.IsPostBack)
        {
            ViewState["rowfilter"] = string.Empty;
            DataSet ds = new DataSet();
            BindGvNeedsApproval(ds);
            BindLists(ds);
        }
    }

    public string rowFilter
    {
        get { return (string)ViewState["rowfilter"]; }
        set { ViewState["rowfilter"] = value; }
    }

    private void BindGvNeedsApproval(DataSet ds)
    {
        try
        {
            ds = log.FillDs_ByProcess(ds, (string)Session["deltaid"], "registars", "pending");

            DataView needsApproval = ds.Tables[0].DefaultView;
            if (!string.IsNullOrEmpty(rowFilter))
            {
                needsApproval.RowFilter = rowFilter;
            }

            gvNeedsApproval.DataSource = needsApproval;
            gvNeedsApproval.DataBind();
        }
        catch { }
    }

    private void BindLists(DataSet ds)
    {
        try
        {
            DataSetHelper dsHelper = new DataSetHelper(ref ds);
            dsHelper.SelectDistinct("Terms", ds.Tables[0], "Term");
            dsHelper.SelectDistinct("Course", ds.Tables[0], "CourseNumber");
            dsHelper.SelectDistinct("SubmittedBy", ds.Tables[0], "SubmittedBy");
            dsHelper.SelectDistinct("DepartmentCode", ds.Tables[0], "DepartmentCode");

            ddlTermFilter.DataSource = ds.Tables["Terms"];
            ddlTermFilter.DataBind();
            ddlTermFilter.Items.Insert(0, new ListItem("All", "All"));

            ddlCourse.DataSource = ds.Tables["Course"];
            ddlCourse.DataBind();
            ddlCourse.Items.Insert(0, new ListItem("All", "All"));

            ddlSubmittedByFilter.DataSource = ds.Tables["SubmittedBy"];
            ddlSubmittedByFilter.DataBind();
            ddlSubmittedByFilter.Items.Insert(0, new ListItem("All", "All"));

            ddlDepartmentFilter.DataSource = ds.Tables["DepartmentCode"];
            ddlDepartmentFilter.DataBind();
            ddlDepartmentFilter.Items.Insert(0, new ListItem("All", "All"));
        }
        catch
        {
            NoDataPanel.Visible = true;
        }
    }

    protected void gvNeedsApproval_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string registrarComments = string.Empty;

        if (e.CommandName != "Page")
        {
            log.LogId = int.Parse(e.CommandArgument.ToString());
            log.GetRecord();

            string to = Helpers.GetDeltaEmail(log.SubmittedBy);
            //Get division chair email address
            string DivChairEmail = log.UpdatedBy.ToString() + "@delta.edu";
            StringBuilder sbCourseInfo = new StringBuilder(string.Empty);

            if (e.CommandName == "approve")
            {
                StringBuilder sb1 = new StringBuilder(log.Change);
                sb1.Append("<ul><br>Registrar: ");
                foreach (GridViewRow row in gvNeedsApproval.Rows)
                {
                    if (row.FindControl<HiddenField>("LOGID").Value == log.LogId.ToString())
                    {
                        registrarComments = row.FindControl<TextBox>("RegisCommentsTextBox").Text;
                        if (string.IsNullOrEmpty(registrarComments))
                        {
                            registrarComments = "<b>No comments.</b>";
                        }
                    }
                }
                sb1.Append(registrarComments);
                sb1.Append("</ul>");

                log.ProcessGroup = "final";
                log.UpdatedBy = User.Identity.Name;
                log.Change = sb1.ToString();
                log.UpdateRecord();

                this.UpdateCurrentProcess("approved");

                //Add the additional processing screens.

                string[] sendTo = GetNextProcessGroups();
                for (int i = 0; i < sendTo.Length; i++)
                {
                    AddProcess(sendTo[i], "last");
                }

                StringBuilder sb = new StringBuilder();
                if (log.SectionsId > 0)
                {
                    ClsSections section = new ClsSections("MasterSchedule");
                    section.SectionsId = log.SectionsId;
                    section.GetRecord();
                    ClsCourses course = new ClsCourses("MasterSchedule");
                    course.CoursesId = section.CoursesId;
                    course.GetRecord();
                    ClsTerms term = new ClsTerms("MasterSchedule");
                    term.TermsId = section.TermsId;
                    term.GetRecord();
                    sbCourseInfo.Append(term.Term + " " + course.CourseNumber + " " + section.SectionNumber + ":<br/><br/>");
                    sb.Append("<br /> Please make the following change to " + term.Term + " " + course.CourseNumber + " " + section.SectionNumber + ":<br/><br/>");
                }
                if (sbCourseInfo.Length > 1)
                {
                    sb.Append("<h4>Add Course:</h4><br/><br/>");
                }
                sb.Append(log.Change);
                sbCourseInfo.Append(log.Change);

                Helpers.SendEmail("Master Schedule change approved", sb.ToString(), GetNextProcessEmails(), log.SectionsId);
                // 3/23/16 - mgm - changed richardzeien to michaelmccloskey
                Helpers.SendEmail("Master Schedule change approved.", "Email sending to: " + GetNextProcessEmails() + "<br />" + sb.ToString(), "michaelmccloskey@delta.edu", log.SectionsId);
                using (MasterScheduleDataContext db = new MasterScheduleDataContext())
                {
                    List<SendMessageOnLogApproval> messagesToSend = db.SendMessageOnLogApprovals.Where(X => X.LogID == log.LogId).ToList();

                    foreach (SendMessageOnLogApproval thisMessage in messagesToSend)
                    {
                        var results = db.GetMeetingCenterEmail(thisMessage.CampusID).ToList();
                        if (results.Count() > 0)
                        {
                            StringBuilder emailsToSend = new StringBuilder();

                            results.ForEach(X => emailsToSend.Append(X.DeltaEmail + ";"));

                            Helpers.SendEmail("Master Schedule change approved", sb.ToString(), emailsToSend.ToString().TrimEnd(";".ToCharArray()), log.SectionsId);
                        }
                        thisMessage.DateCompleted = DateTime.Now;
                        db.SubmitChanges();
                    }
                }

                if (to != "")
                {
                    Helpers.SendEmail("Master Schedule change approved", "Records and Registration has <b>approved</b> your schedule change. <br />Registrar Comments: " + registrarComments + "<br /><br /><b>Course Info:</b><br/>" + sbCourseInfo.ToString(), to, log.SectionsId);
                }

                Helpers.SendEmail("Master Schedule change approved.", "This email will be sent to:  " + DivChairEmail + "Records and Registration has <b>approved</b> the schedule change. <br />Registrar Comments: " + registrarComments + "<br /><br /><b>Course Info:</b><br/>" + sbCourseInfo.ToString(), "webadmin@delta.edu", log.SectionsId);
                if (DivChairEmail != "")
                {
                    /*Commented out for testing.. Cyrus */
                    Helpers.SendEmail("Master Schedule change approved", "Records and Registration has <b>approved</b> the schedule change. <br />Registrar Comments: " + registrarComments + "<br /><br /><b>Course Info:</b><br/>" + sbCourseInfo.ToString(), DivChairEmail, log.SectionsId);
                }
                else
                {
                }
            }
            if (e.CommandName == "ignore")
            {
                StringBuilder sb2 = new StringBuilder(log.Change);
                sb2.Append("<ul><br>Registrar: ");
                foreach (GridViewRow row in gvNeedsApproval.Rows)
                {
                    if (row.FindControl<HiddenField>("LOGID").Value == log.LogId.ToString())
                    {
                        registrarComments = row.FindControl<TextBox>("RegistrarDenyTextBox").Text;
                        if (string.IsNullOrEmpty(registrarComments))
                        {
                            registrarComments = "<b>No comments.</b>";
                        }
                    }
                }
                sb2.Append(registrarComments);
                sb2.Append("</ul>");

                sbCourseInfo.Append(log.Change);
                log.Status = "ignored";
                log.ProcessGroup = "complete";
                log.Change = sb2.ToString();
                log.UpdateRecord();

                this.UpdateCurrentProcess("ignored");

                if (to != "")
                {
                    Helpers.SendEmail("Master Schedule change denied", "Your schedule change has been <b>denied</b> by Records and Registration. <br />Registrar Comments: " + registrarComments + "<br /><br /><b>Course Info:</b><br/>" + sbCourseInfo.ToString(), to, log.SectionsId);
                }

                if (DivChairEmail != "")
                {
                    /*Commented out for testing.. Cyrus */
                    Helpers.SendEmail("Master Schedule change denied", "The schedule change has been <b>denied</b> by Records and Registration. <br />Registrar Comments: " + registrarComments + "<br /><br /><b>Course Info:</b><br/>" + sbCourseInfo.ToString(), DivChairEmail, log.SectionsId);
                }
            }
            DataSet ds = new DataSet();
            rowFilter = string.Empty;
            BindGvNeedsApproval(ds);
            BindLists(ds);
        }
    }

    private string[] GetNextProcessGroups()
    {
        //This will eventually be a function to deterine what items are in the change log.
        // If there are certian items, different groups need to process.
        string[] processNames = new string[] { "bookstore", "roomscheduling" };
        return processNames;
    }

    private string GetNextProcessEmails()
    {
        string admin = ConfigurationManager.AppSettings["EmailAdmin"];
        //This will eventually be a function to deterine what items are in the change log.
        // If there are certian items, different groups need to process.
        string processNames = "roomscheduling@delta.edu;bookstore@delta.edu";
        processNames += (!String.IsNullOrEmpty(admin)) ? ";" + admin : "";
        return processNames;
    }

    private void AddProcess(string processname, string processGroup)
    {
        ClsChangeLogProcessing nextProcess = new ClsChangeLogProcessing("MasterSchedule");
        nextProcess.ProcessName = processname;
        nextProcess.ChangeLogId = log.LogId;
        nextProcess.ActionTaken = "pending";
        nextProcess.ProcessGroup = processGroup;
        nextProcess.AddRecord();
    }

    private void UpdateCurrentProcess(string status)
    {
        ClsChangeLogProcessing curProcess = new ClsChangeLogProcessing("MasterSchedule");
        curProcess.ChangeLogId = log.LogId;
        curProcess.ProcessName = "registars";
        curProcess.GetRecord_ByChange_ByProcess();

        curProcess.ActionTaken = status;
        curProcess.ProcessedBy = log.UpdatedBy;
        curProcess.ProcessedOn = DateTime.Now;
        curProcess.ProcessGroup = "registars";

        curProcess.UpdateRecord();
    }

    private void SendEmail(string subject, string body, string to)
    {
        MailMessage msg = new MailMessage();
        msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
        msg.Subject = subject;
        msg.IsBodyHtml = true;
        msg.Body = body;

        foreach (string address in to.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
        {
            msg.To.Add(new MailAddress(address));
        }

        SmtpClient smtp = new SmtpClient();
        smtp.Host = ConfigurationManager.AppSettings["EmailHost"];
        smtp.Port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
        if (!ConfigurationManager.AppSettings["EmailUsername"].Equals(string.Empty))
        {
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUsername"], ConfigurationManager.AppSettings["EmailPassword"]);
        }
        smtp.Send(msg);
        smtp = null;

        msg.Dispose();
    }

    public string ViewCourseCss(object sectionsid)
    {
        int id = (int)sectionsid;
        if (id > 0)
            return "display:block;";
        else
            return "display:none;";
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        List<string> rowFilters = new List<string>();
        if (ddlTermFilter.SelectedValue != "All")
        {
            rowFilters.Add("Term = '" + ddlTermFilter.SelectedValue + "' ");
        }
        if (ddlCourse.SelectedValue != "All")
        {
            rowFilters.Add("CourseNumber LIKE '" + ddlCourse.SelectedValue + "%' ");
        }
        if (ddlSubmittedByFilter.SelectedValue != "All")
        {
            rowFilters.Add("SubmittedBy = '" + ddlSubmittedByFilter.SelectedValue + "' ");
        }
        if (ddlDepartmentFilter.SelectedValue != "All")
        {
            string DeptCode = ddlDepartmentFilter.SelectedValue.Trim();
            rowFilters.Add("DepartmentCode = '" + DeptCode + "'");
        }
        StringBuilder sb = new StringBuilder();
        string a = "";
        foreach (string filter in rowFilters)
        {
            sb.Append(a);
            sb.Append(filter);
            a = " AND ";
        }
        rowFilter = sb.ToString();
        BindGvNeedsApproval(new DataSet());
    }
}