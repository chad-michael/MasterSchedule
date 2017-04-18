using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

public partial class addnew : System.Web.UI.Page
{
    public ClsTempSection section;
    private ClsChangeLog log;
    private string DivChairEmail;

    protected void Page_Load(object sender, EventArgs e)
    {
        section = new ClsTempSection("MasterSchedule");
        log = new ClsChangeLog("MasterSchedule");
        Session["usepreview"] = false;
        //if (divchairoption.Visible) { Session["usepreview"] = radSubmitOptions.SelectedIndex == 1; }

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["NewSectionID"]))
            {
                // divchairoption.Visible = Helpers.UserIsDivisionChair();
                mvSubmissionForm.ActiveViewIndex = 0;
                section.AddRecord();

                Response.Redirect(string.Format("~/AddNew.aspx?NewSectionID={0}", section.SectionId.ToString()));
                //Session["NewSectionID"] = section.SectionID;
                SectionID.Value = section.SectionId.ToString();
                Faculty1.sectionGUID = section.SectionId;
            }
            else
            {
                Guid tempGuid = new Guid(Request.QueryString["NewSectionID"]);
                section.SectionId = tempGuid;
                Session["NewSectionID"] = section.SectionId;
                Faculty1.sectionGUID = tempGuid;
            }
        }
        else
        {
            section.SectionId = (Guid)Session["NewSectionID"];
            section.GetRecord();
            Guid tempGuid = new Guid(Request.QueryString["NewSectionID"]);
            Faculty1.sectionGUID = tempGuid;
        }
    }

    protected void btnAddNewCourse_Click(object sender, EventArgs e)
    {
        string courseInfo = getCourseInfo();

        log.SectionsId = 0;
        log.SubmittedBy = Session["deltaid"].ToString();
        log.DepartmentsId = SectionDetails1.DepartmentsID;
        log.Change = getCourseInfo();
        log.AddRecord();

        section.DeleteRecord();
        this.mvSubmissionForm.ActiveViewIndex = 1;
        List<ClsChangeLog> logs = new List<ClsChangeLog>();
        logs.Add(log);
        gvPendingChanges.DataSource = logs;
        gvPendingChanges.DataBind();

        using (MasterScheduleDataContext db = new MasterScheduleDataContext())
        {
            foreach (string sLocation in hidRooms.Value.TrimEnd(",".ToCharArray()).Split(",".ToCharArray()).ToList().Distinct())
            {
                SendMessageOnLogApproval thisItem = new SendMessageOnLogApproval();

                thisItem.LogID = log.LogId;
                thisItem.CampusID = sLocation;
                thisItem.DateCreated = DateTime.Now;
                db.SendMessageOnLogApprovals.InsertOnSubmit(thisItem);
            }

            db.SubmitChanges();
        }

        /* Send email upon submission */
        DivChairEmail = Helpers.GetDivisionEmailByDepartmentsId(log.DepartmentsId);
        sendNotification(); // Make sure to re-enable this
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        section.DeleteRecord();
        Response.Redirect("~/");
    }

    protected void sendNotification()
    {
        if (!Helpers.UserIsDivisionChair())
        {
            string EmailBody;
            EmailBody = "A new course has been submitted, and is awaiting your approval.  Please review the pending additions/changes at https://apps.delta.edu/masterschedule/NeedsApproval.aspx.";
            /*Commented out for testing.. Cyrus*/
            Helpers.SendEmail("Master Schedule Request", EmailBody, DivChairEmail);
        }
    }

    private string getCourseInfo()
    {
        int totalDays = DateTime.Parse(SectionDetails1.DateEnd).Subtract(DateTime.Parse(SectionDetails1.DateStart)).Days + 1;
        string beforeDay = "";
        string afterDay = "";

        StringBuilder sb = new StringBuilder();
        sb.Append("Course Details<br/>");
        sb.Append("Year: " + SectionDetails1.Year);
        sb.Append("<br/>Semester: " + SectionDetails1.Semester);
        sb.Append("<br/>Department: " + SectionDetails1.Department);
        sb.Append("<br/>Catalog Number: " + SectionDetails1.CatalogNumber);
        sb.Append("<br/>Section Number: " + SectionDetails1.SectionNumber);
        sb.Append("<br/>Start Date: " + SectionDetails1.DateStart);
        sb.Append("<br/>End Date: " + SectionDetails1.DateEnd);
        sb.Append("<br/>Capacity: " + SectionDetails1.Capacity);
        sb.Append("<br/>Refund Policy: " + RefundPeriodLookup(DateTime.Parse(SectionDetails1.DateStart), DateTime.Parse(SectionDetails1.DateEnd)));
        sb.Append("<br/>Refund Policy is not applicable to non-credit courses.");
        sb.Append("<br/>Days: " + totalDays.ToString());
        sb.Append("<br/><br/>");

        ClsTempFaculty faculty = new ClsTempFaculty("MasterSchedule");
        faculty.SectionId = section.SectionId;
        DataSet facultyds = faculty.FillDs();
        if (facultyds.Tables[0].Rows.Count > 0)
        {
            sb.Append("Faculty<ul>");
            for (int i = 0; i < facultyds.Tables[0].Rows.Count; i++)
                sb.Append("<li>" + facultyds.Tables[0].Rows[i]["FacultyName"].ToString() + "</li>");

            sb.Append("</ul><br/>");
        }
        facultyds.Dispose();

        ClsTempMeeting meeting = new ClsTempMeeting("MasterSchedule");
        meeting.SectionId = section.SectionId;
        DataSet meetingds = meeting.FillDs();
        if (meetingds.Tables[0].Rows.Count > 0)
        {
            sb.Append("Meetings<br/><br/>");
            for (int i = 0; i < meetingds.Tables[0].Rows.Count; i++)
            {
                sb.Append("Meeting " + ((int)(i + 1)).ToString());
                sb.Append("<br/>Location: " + meetingds.Tables[0].Rows[i]["Campus"].ToString());
                sb.Append("<br/>Room: " + meetingds.Tables[0].Rows[i]["Room"].ToString());
                sb.Append("<br/>Start Time: " + meetingds.Tables[0].Rows[i]["StartTime"].ToString());
                sb.Append("<br/>End Time: " + meetingds.Tables[0].Rows[i]["EndTime"].ToString());
                sb.Append("<br/>Days: " + meetingds.Tables[0].Rows[i]["Days"].ToString());
                sb.Append("<br/>Type: " + meetingds.Tables[0].Rows[i]["Type"].ToString());
                sb.Append("<br/><br/>");
            }
        }
        meetingds.Dispose();

        //if (rbliTunesu.SelectedIndex == 0)
        //{
        //    sb.Append("iTunes U<ul><b>");
        //    sb.Append("<li>" + rbliTunesu.SelectedValue.ToString() + "</li>");
        //    sb.Append("</b></ul><br/>");
        //}

        if (chkIsInMyLabs.Checked)
        {
            sb.Append("MyLabsPlus<ul><b>");

            sb.Append("<li> This course is in Pearson eCollege MyLabsPlus.</li>");
            sb.Append("</b></ul><br/>");
        }

        if (ckDirectContentFee.Checked)
        {
            sb.Append("Direct Content Fee<ul><b>");
            sb.Append("<li>Course has a direct content fee.</li>");
            sb.Append("</b></ul><br />");
        }

        ClsTempColink colink = new ClsTempColink("MasterSchedule");
        colink.SectionId = section.SectionId;
        DataSet colinkds = colink.FillDs();
        if (colinkds.Tables[0].Rows.Count > 0)
        {
            sb.Append("Co-Listed Courses<ul>");
            for (int i = 0; i < colinkds.Tables[0].Rows.Count; i++)
                sb.Append("<li>" + colinkds.Tables[0].Rows[i]["Department"].ToString() + " - " + colinkds.Tables[0].Rows[i]["Course"].ToString() + " - " + colinkds.Tables[0].Rows[i]["Section"].ToString() + "</li>");

            sb.Append("</ul><br/>");
        }
        colinkds.Dispose();

        //clsTempLink link = new clsTempLink("MasterSchedule");
        //link.SectionID = section.SectionID;
        //DataSet linkds = link.FillDs();
        //if (linkds.Tables[0].Rows.Count > 0)
        //{
        //    sb.Append("Cross Linked Courses<ul>");
        //    for (int i = 0; i < linkds.Tables[0].Rows.Count; i++)
        //        sb.Append("<li>" + linkds.Tables[0].Rows[i]["Department"].ToString() + " - " + linkds.Tables[0].Rows[i]["Course"].ToString() + " - " + linkds.Tables[0].Rows[i]["Section"].ToString() + "</li>");

        //    sb.Append("</ul><br/>");
        //}
        //linkds.Dispose();

        ClsTempCoreq coreq = new ClsTempCoreq("MasterSchedule");
        coreq.SectionId = section.SectionId;
        DataSet coreqds = coreq.FillDs();
        if (coreqds.Tables[0].Rows.Count > 0)
        {
            sb.Append("Co-requisite/Linked Courses<ul>");
            for (int i = 0; i < coreqds.Tables[0].Rows.Count; i++)
                sb.Append("<li>" + coreqds.Tables[0].Rows[i]["Department"].ToString() + " - " + coreqds.Tables[0].Rows[i]["Course"].ToString() + " - " + coreqds.Tables[0].Rows[i]["Section"].ToString() + "</li>");

            sb.Append("</ul><br/>");
        }
        coreqds.Dispose();

        sb.Append("Comments<ul>");

        string InitiatorComments = string.Empty;

        if (string.IsNullOrEmpty(Comments_New1.Comments))
        {
            InitiatorComments = "No Comments.";
        }
        else
        {
            InitiatorComments = Comments_New1.Comments;
        }
        sb.Append("<br/>Initiator: " + InitiatorComments);
        sb.Append("</ul><br/>");

        return sb.ToString();
    }

    protected string RefundPeriodLookup(DateTime startDate, DateTime endDate)
    {
        string result = "";
        int dateDiff = endDate.Subtract(startDate).Days + 1;

        if (dateDiff > 28)
        {
            //Full Refund Default
            result = "1 - Full Refund";
        }
        else if (dateDiff < 28 && dateDiff > 2)
        {
            //Short-Term Refund
            result = "2 - Short-Term Refund";
        }
        else
        {
            //No refund
            result = "3 - No Refund as of Start Date";
        }

        return result;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        mvSubmissionForm.ActiveViewIndex = 0;
    }

    protected void confirmationData_Click(object sender, EventArgs e)
    {
        CourseConfirmationLabel.Text = getCourseInfo();
        GridView gvMeetings = (GridView)Meetings_New1.FindControl("gvMeetings");
        foreach (GridViewRow thisRow in gvMeetings.Rows)
        {
            hidRooms.Value += thisRow.Cells[0].Text + ",";
        }

        mvSubmissionForm.ActiveViewIndex = 2;
    }
}