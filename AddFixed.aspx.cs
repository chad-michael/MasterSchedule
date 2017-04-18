using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public partial class addfixed : System.Web.UI.Page
{
    public ClsTempSection section;
    private ClsChangeLog log;

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
                divchairoption.Visible = Helpers.UserIsDivisionChair();
                mvSubmissionForm.ActiveViewIndex = 0;
                section.AddRecord();

                Response.Redirect(string.Format("~/add.aspx?NewSectionID={0}", section.SectionId.ToString()));
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

        /* Send email upon submission */
        sendNotification();
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
            EmailBody = "Testing - A new course has been submitted, and is awaiting your approval.  Please review the pending additions/changes at https://appstage.delta.edu/masterschedule/NeedsApproval.aspx.";
            Helpers.SendEmail("Testing - Master Schedule Request", EmailBody, "mcloree@delta.edu");
        }
    }

    private string getCourseInfo()
    {
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
        sb.Append("<br/><br/>");

        sb.Append("Faculty<ul>");
        ClsTempFaculty faculty = new ClsTempFaculty("MasterSchedule");
        faculty.SectionId = section.SectionId;
        DataSet facultyds = faculty.FillDs();
        for (int i = 0; i < facultyds.Tables[0].Rows.Count; i++)
            sb.Append("<li>" + facultyds.Tables[0].Rows[i]["FacultyName"].ToString() + "</li>");
        facultyds.Dispose();
        sb.Append("</ul><br/>");

        sb.Append("Meetings<br/><br/>");
        ClsTempMeeting meeting = new ClsTempMeeting("MasterSchedule");
        meeting.SectionId = section.SectionId;
        DataSet meetingds = meeting.FillDs();
        for (int i = 0; i < meetingds.Tables[0].Rows.Count; i++)
        {
            sb.Append("Meeting " + ((int)(i + 1)).ToString());
            sb.Append("<br/>Campus: " + meetingds.Tables[0].Rows[i]["Campus"].ToString());
            sb.Append("<br/>Room: " + meetingds.Tables[0].Rows[i]["Room"].ToString());
            sb.Append("<br/>Start Time: " + meetingds.Tables[0].Rows[i]["StartTime"].ToString());
            sb.Append("<br/>End Time: " + meetingds.Tables[0].Rows[i]["EndTime"].ToString());
            sb.Append("<br/>Days: " + meetingds.Tables[0].Rows[i]["Days"].ToString());
            sb.Append("<br/>Type: " + meetingds.Tables[0].Rows[i]["Type"].ToString());
            sb.Append("<br/><br/>");
        }
        meetingds.Dispose();

        sb.Append("Co-Listed Courses<ul>");
        ClsTempColink colink = new ClsTempColink("MasterSchedule");
        colink.SectionId = section.SectionId;
        DataSet colinkds = colink.FillDs();
        for (int i = 0; i < colinkds.Tables[0].Rows.Count; i++)
            sb.Append("<li>" + colinkds.Tables[0].Rows[i]["Department"].ToString() + " - " + colinkds.Tables[0].Rows[i]["Course"].ToString() + " - " + colinkds.Tables[0].Rows[i]["Section"].ToString() + "</li>");
        colinkds.Dispose();
        sb.Append("</ul><br/>");

        sb.Append("Cross Linked Courses<ul>");
        ClsTempLink link = new ClsTempLink("MasterSchedule");
        link.SectionId = section.SectionId;
        DataSet linkds = link.FillDs();
        for (int i = 0; i < linkds.Tables[0].Rows.Count; i++)
            sb.Append("<li>" + linkds.Tables[0].Rows[i]["Department"].ToString() + " - " + linkds.Tables[0].Rows[i]["Course"].ToString() + " - " + linkds.Tables[0].Rows[i]["Section"].ToString() + "</li>");
        linkds.Dispose();
        sb.Append("</ul><br/>");

        sb.Append("Co-requisite Courses<ul>");
        ClsTempCoreq coreq = new ClsTempCoreq("MasterSchedule");
        coreq.SectionId = section.SectionId;
        DataSet coreqds = coreq.FillDs();
        for (int i = 0; i < coreqds.Tables[0].Rows.Count; i++)
            sb.Append("<li>" + coreqds.Tables[0].Rows[i]["Department"].ToString() + " - " + coreqds.Tables[0].Rows[i]["Course"].ToString() + " - " + coreqds.Tables[0].Rows[i]["Section"].ToString() + "</li>");
        coreqds.Dispose();
        sb.Append("</ul><br/>");

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

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        mvSubmissionForm.ActiveViewIndex = 0;
    }

    protected void confirmationData_Click(object sender, EventArgs e)
    {
        CourseConfirmationLabel.Text = getCourseInfo();
        mvSubmissionForm.ActiveViewIndex = 2;
    }
}