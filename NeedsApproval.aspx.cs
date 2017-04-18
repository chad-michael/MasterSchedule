using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;

public partial class NeedsApproval : System.Web.UI.Page
{
    private ClsChangeLog log;
    private ClsChangeLogProcessing logProcess;
    private ClsChangeLogProcessing nextProcess;

    protected void Page_Load(object sender, EventArgs e)
    {
        log = new ClsChangeLog("MasterSchedule");
        logProcess = new ClsChangeLogProcessing("MasterSchedule");
        nextProcess = new ClsChangeLogProcessing("MasterSchedule");
    }

    protected void gvNeedsApproval_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string divChairComments = string.Empty;
        if (e.CommandName != "Page")
        {
            log.LogId = int.Parse(e.CommandArgument.ToString());
            log.GetRecord();
            log.UpdatedBy = Session["deltaid"].ToString();
            log.DateUpdated = DateTime.Now;

            string to = Helpers.GetDeltaEmail(log.SubmittedBy);
            string admin = ConfigurationManager.AppSettings["EmailAdmin"];

            if (e.CommandName == "approve")
            {
                StringBuilder sb1 = new StringBuilder(log.Change);
                sb1.Append("<ul><br>Division Chair: ");
                foreach (GridViewRow row in gvNeedsApproval.Rows)
                {
                    if (row.FindControl<HiddenField>("LOGID").Value == log.LogId.ToString())
                    {
                        divChairComments = row.FindControl<TextBox>("DivChairCommentsTextBox").Text;
                        if (string.IsNullOrEmpty(divChairComments))
                        {
                            divChairComments = "<b>No comments.</b>";
                        }
                    }
                }
                sb1.Append(divChairComments);
                sb1.Append("</ul>");
                log.Status = "approved";
                log.ProcessGroup = "registars";
                log.Change = sb1.ToString();
                log.UpdateRecord();

                //Add the registars processing information
                this.AddNextProcess();

                if (to != "")
                {
                    Helpers.SendEmail("Schedule change approved", "<br /> Your schedule change has been approved and has been sent to the registration office for processing.<br />Division Chair Comments: " + divChairComments, to);
                }
                StringBuilder sb = new StringBuilder();
                if (log.SectionsId > 0)
                {
                    sb.Append(" to ");
                    ClsSections section = new ClsSections("MasterSchedule");
                    section.SectionsId = log.SectionsId;
                    section.GetRecord();
                    ClsCourses course = new ClsCourses("MasterSchedule");
                    course.CoursesId = section.CoursesId;
                    course.GetRecord();
                    ClsTerms term = new ClsTerms("MasterSchedule");
                    term.TermsId = section.TermsId;
                    term.GetRecord();
                    sb.Append("<br /> Please make the following change to " + term.Term + " " + course.CourseNumber + " " + section.SectionNumber + ":<br/><br/>");
                }
                sb.Append(log.Change);
                Helpers.SendEmail("Schedule change submitted", sb.ToString(), "regis@delta.edu");
            }
            if (e.CommandName == "ignore")
            {
                StringBuilder sb2 = new StringBuilder(log.Change);
                sb2.Append("<ul><br>Division Chair: ");
                foreach (GridViewRow row in gvNeedsApproval.Rows)
                {
                    if (row.FindControl<HiddenField>("LOGID").Value == log.LogId.ToString())
                    {
                        divChairComments = row.FindControl<TextBox>("DivChairDenyTextBox").Text;
                        if (string.IsNullOrEmpty(divChairComments))
                        {
                            divChairComments = "<b>No comments.</b>";
                        }
                    }
                }
                sb2.Append(divChairComments);
                sb2.Append("</ul>");
                log.Status = "ignored";
                log.Change = sb2.ToString();
                log.UpdateRecord();
                if (to != "")
                    SendEmail("Schedule change denied", "Your schedule change has been denied.<br /><br />Comments<br />" + divChairComments, to);
            }
            gvNeedsApproval.DataBind();
        }
    }

    private void AddNextProcess()
    {
        nextProcess.ProcessName = "registars";
        nextProcess.ChangeLogId = log.LogId;
        nextProcess.ActionTaken = "pending";
        nextProcess.ProcessGroup = "registars";
        nextProcess.AddRecord();
    }

    //private void UpdateCurrentProcess(string status) {
    //    logProcess.ChangeLogId = log.LogID;
    //    logProcess.ActionTaken = status;
    //    logProcess.ProcessedBy = log.UpdatedBy;
    //    logProcess.ProcessedOn = DateTime.Now;
    //    logProcess.ProcessGroup = "divisionchair";
    //    logProcess.UpdateRecord();
    //}

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
}