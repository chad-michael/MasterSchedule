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
using System.Text;

public partial class ViewSectionNew : System.Web.UI.Page
{
    private ClsChangeLog log = new ClsChangeLog("MasterSchedule");
    private int SectionID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        int.TryParse(Request["SectionsID"], out SectionID);
        if (SectionID <= 0)
            Response.Redirect("~/?Error=SectionDetails.ascx+had+no+SectionID");

        Control form = (Control)Page.Master.FindControl("form1");
        ScriptManager sm = (ScriptManager)form.FindControl("ScriptManager1");
        //  sm.RegisterAsyncPostBackControl(gvPendingChanges);
        Session["usepreview"] = false;
        if (divchairoption.Visible) { Session["usepreview"] = radSubmitOptions.SelectedIndex == 1; }
        if (!IsPostBack)
        {
            divchairoption.Visible = Helpers.UserIsDivisionChair();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //  section.DeleteRecord();
        Response.Redirect("~/");
    }

    private string getCourseInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(SectionDetails1.SaveCourseDetails(SectionID));

        //sb.Append("Faculty<ul>");
        //clsTempFaculty faculty = new clsTempFaculty("MasterSchedule");
        //faculty.SectionID = section.SectionID;
        //DataSet facultyds = faculty.FillDs();
        //for (int i = 0; i < facultyds.Tables[0].Rows.Count; i++)
        //    sb.Append("<li>" + facultyds.Tables[0].Rows[i]["FacultyName"].ToString() + "</li>");
        //facultyds.Dispose();
        //sb.Append("</ul><br/>");

        //sb.Append("Meetings<br/><br/>");
        //clsTempMeeting meeting = new clsTempMeeting("MasterSchedule");
        //meeting.SectionID = section.SectionID;
        //DataSet meetingds = meeting.FillDs();
        //for (int i = 0; i < meetingds.Tables[0].Rows.Count; i++)
        //{
        //    sb.Append("Meeting " + ((int)(i + 1)).ToString());
        //    sb.Append("<br/>Campus: " + meetingds.Tables[0].Rows[i]["Campus"].ToString());
        //    sb.Append("<br/>Room: " + meetingds.Tables[0].Rows[i]["Room"].ToString());
        //    sb.Append("<br/>Start Time: " + meetingds.Tables[0].Rows[i]["StartTime"].ToString());
        //    sb.Append("<br/>End Time: " + meetingds.Tables[0].Rows[i]["EndTime"].ToString());
        //    sb.Append("<br/>Days: " + meetingds.Tables[0].Rows[i]["Days"].ToString());
        //    sb.Append("<br/>Type: " + meetingds.Tables[0].Rows[i]["Type"].ToString());
        //    sb.Append("<br/><br/>");
        //}
        //meetingds.Dispose();

        //sb.Append("Cross Linked Courses<ul>");
        //clsTempLink link = new clsTempLink("MasterSchedule");
        //link.SectionID = section.SectionID;
        //DataSet linkds = link.FillDs();
        //for (int i = 0; i < linkds.Tables[0].Rows.Count; i++)
        //    sb.Append("<li>" + linkds.Tables[0].Rows[i]["Department"].ToString() + " - " + linkds.Tables[0].Rows[i]["Course"].ToString() + " - " + linkds.Tables[0].Rows[i]["Section"].ToString() + "</li>");
        //linkds.Dispose();
        //sb.Append("</ul><br/>");

        //sb.Append("Co-requisite Courses<ul>");
        //clsTempCoreq coreq = new clsTempCoreq("MasterSchedule");
        //coreq.SectionID = section.SectionID;
        //DataSet coreqds = coreq.FillDs();
        //for (int i = 0; i < coreqds.Tables[0].Rows.Count; i++)
        //    sb.Append("<li>" + coreqds.Tables[0].Rows[i]["Department"].ToString() + " - " + coreqds.Tables[0].Rows[i]["Course"].ToString() + " - " + coreqds.Tables[0].Rows[i]["Section"].ToString() + "</li>");
        //coreqds.Dispose();
        //sb.Append("</ul><br/>");

        //sb.Append("Comments<ul>");

        //string InitiatorComments = string.Empty;

        //if (string.IsNullOrEmpty(Comments_New1.Comments))
        //{
        //    InitiatorComments = "No Comments.";
        //}
        //else
        //{
        //    InitiatorComments = Comments_New1.Comments;
        //}
        //sb.Append("<br/>Initiator: " + InitiatorComments);
        //sb.Append("</ul><br/>");

        return sb.ToString();
    }

    protected void confirmationData(Object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            CourseConfirmationLabel.Text = "Section ID: " + SectionID.ToString() + "<br /><br />" + getCourseInfo();
        }
    }

    protected void ProcessModifications()
    {
        CourseConfirmationLabel.Text = "Section ID: " + SectionID.ToString() + "<br /><br />" + getCourseInfo();
    }
}