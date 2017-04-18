using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;

namespace Controls
{
    public partial class Controls_ApprovalGrid : System.Web.UI.UserControl
    {
        public string ProcessName
        {
            get { return hProcessName.Value; }
            set { hProcessName.Value = value; }
        }

        public string StatusFilter
        {
            get { return hStatusFilter.Value; }
            set { hStatusFilter.Value = value; }
        }

        public string ScreenTitle
        {
            get { return lblScreenTitle.Text; }
            set { lblScreenTitle.Text = value; lblHeader.Text = "Pending Schedule Changes : " + lblScreenTitle; }
        }

        private bool _isVisible = false;

        public bool IsVisible
        {
            get { return _isVisible; }
        }

        private ClsChangeLog log;
        private ClsChangeLogProcessing logProcess;

        protected void Page_Load(object sender, EventArgs e)
        {
            log = new ClsChangeLog("MasterSchedule");
            logProcess = new ClsChangeLogProcessing("MasterSchedule");
            ClsUserControl userControl = new ClsUserControl("MasterSchedule");
            this._isVisible = userControl.UserInProcess((string)Session["deltaid"], this.ProcessName);
            SetVisibility();
        }

        protected void SetVisibility()
        {
            processScreen.Visible = this._isVisible;
        }

        protected void gvNeedsApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            logProcess.ProcessingId = int.Parse(e.CommandArgument.ToString());
            logProcess.GetRecord();

            string to = Helpers.GetDeltaEmail(log.SubmittedBy);
            if (e.CommandName == "approve")
            {
                //Update the current process information
                this.UpdateCurrentProcess("processed");
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
                    sb.Append("Schedule change for " + term.Term + " " + course.CourseNumber + " " + section.SectionNumber + ":<br/><br/>");
                    sb.Append(" has been processed by " + this.ProcessName);
                    sb.Append(log.Change);
                }
                if (!string.IsNullOrEmpty(to))
                {
                    SendEmail("Schedule change processed", sb.ToString(), to);
                }
            }

            if (e.CommandName == "ignore")
            {
                this.UpdateCurrentProcess("notprocessed");
            }

            Response.Write(log.LastError);
            gvNeedsApproval.DataBind();
        }

        private void UpdateCurrentProcess(string status)
        {
            logProcess.ActionTaken = status;
            logProcess.ProcessedBy = (string)Session["deltaid"];
            logProcess.ProcessedOn = DateTime.Now;
            logProcess.UpdateRecord();
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
    }
}