using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public partial class AddCourse : System.Web.UI.Page
{
    public ClsTempSection section;
    private ClsChangeLog log;

    protected void Page_Load(object sender, EventArgs e)
    {
        section = new ClsTempSection("MasterSchedule");
        log = new ClsChangeLog("MasterSchedule");
        Session["usepreview"] = false;
        if (divchairoption.Visible) { Session["usepreview"] = radSubmitOptions.SelectedIndex == 1; }
        if (!IsPostBack)
        {
            divchairoption.Visible = Helpers.UserIsDivisionChair();
            MultiView1.ActiveViewIndex = 0;
            section.AddRecord();
            Session["NewSectionID"] = section.SectionId;
        }
        else
        {
            section.SectionId = (Guid)Session["NewSectionID"];
            section.GetRecord();
        }
    }

    private void log_RecordAdding(object sender, RecordAddEventArgs e)
    {
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        section.DeleteRecord();
        Response.Redirect("~/");
    }

    protected void btnAddNewCourse_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<h4>Add Course:</h4><br/><br/>");
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
            sb.Append("<br/><br/>");
        }
        meetingds.Dispose();

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

        log.SectionsId = 0;
        log.SubmittedBy = Session["deltaid"].ToString();
        log.DepartmentsId = SectionDetails1.DepartmentsID;
        log.Change = sb.ToString();
        log.AddRecord();

        section.DeleteRecord();
        MultiView1.ActiveViewIndex = 1;
        List<ClsChangeLog> logs = new List<ClsChangeLog>();
        logs.Add(log);
        gvPendingChanges.DataSource = logs;
        gvPendingChanges.DataBind();
    }
}