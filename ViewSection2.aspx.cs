using System;
using System.Data;
using System.Web.UI;
using System.Text;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Linq;

public partial class viewsection2 : System.Web.UI.Page
{
    private ClsChangeLog log = new ClsChangeLog("MasterSchedule");
    private int SectionsID = 0;
    private Guid NewSectionID;
    private string DivChairEmail;
    public ClsTempSection section;

    protected void Page_Load(object sender, EventArgs e)
    {
        section = new ClsTempSection("MasterSchedule");

        int.TryParse(Request["SectionsID"], out SectionsID);
        if (SectionsID <= 0)
            Response.Redirect("~/?Error=SectionDetails.ascx+had+no+SectionID");

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["NewSectionID"]))
            {
                section.AddRecord();

                Response.Redirect(string.Format("~/ViewSection2.aspx?NewSectionID={0}&SectionsID={1}", section.SectionId.ToString(), Request.QueryString["SectionsID"]));
                SectionID.Value = section.SectionId.ToString();
                Faculty1.sectionGUID = section.SectionId;
            }
            else
            {
                Guid tempGuid = new Guid(Request.QueryString["NewSectionID"]); ;
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

        Control form = (Control)Page.Master.FindControl("form1");
        ScriptManager sm = (ScriptManager)form.FindControl("ScriptManager1");
        //  sm.RegisterAsyncPostBackControl(gvPendingChanges);
        Session["usepreview"] = false;
        //if (divchairoption.Visible) { Session["usepreview"] = radSubmitOptions.SelectedIndex == 1; }
        //if (!IsPostBack)
        //{
        //    divchairoption.Visible = Helpers.UserIsDivisionChair();

        //}
    }

    protected void btnAddNewCourse_Click(object sender, EventArgs e)
    {
        string courseInfo = getCourseInfo();

        try
        {
            log.SectionsId = Int32.Parse(Request.QueryString["SectionsID"]);
        }
        catch
        {
            log.SectionsId = 0;
        }
        log.SubmittedBy = Session["deltaid"].ToString();
        log.DepartmentsId = SectionDetails1.GetDepartmentsID();
        log.Change = getCourseInfo();
        log.AddRecord();

        // section.DeleteRecord();
        // this.mvSubmissionForm.ActiveViewIndex = 1;
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

        DivChairEmail = Helpers.GetDivisionEmailByDepartmentsId(log.DepartmentsId, Session["deltaid"].ToString());
        /* Send email upon submission */
        sendNotification(Session["deltaid"].ToString());

        Response.Redirect("~/RequestSubmitted.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //section.DeleteRecord();
        Response.Redirect("~/");
    }

    protected void sendNotification(string submitter)
    {
        submitter = submitter + "@delta.edu";

        if (!string.IsNullOrEmpty(DivChairEmail))
        {
            if (submitter.CompareTo(DivChairEmail) != 0)
            {
                string EmailBody;
                EmailBody = "A course has been modified, and is awaiting your approval.  Please review the pending additions/changes at https://apps.delta.edu/masterschedule/NeedsApproval.aspx.";
                Helpers.SendEmail("Master Schedule Course Modified", EmailBody, DivChairEmail); //DivChairEmail
            }
        }
    }

    private string getCourseInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(SectionDetails1.SaveCourseDetails(SectionsID));
        //list pre-existing meetings
        ClsTempExistingMeeting preexistingMeeting = new ClsTempExistingMeeting("MasterSchedule");
        DataSet preexistingMeetingds = new DataSet();
        preexistingMeeting.SectionId = section.SectionId;
        preexistingMeetingds = preexistingMeeting.FillDsPre();

        sb.Append("Original Meeting Times:<br/>");

        if (preexistingMeetingds.Tables[0].Rows.Count != 0)
        {
            for (int i = 0; i < preexistingMeetingds.Tables[0].Rows.Count; i++)
            {
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Meeting: " + ((int)(i + 1)).ToString());
                sb.Append("<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Campus: " + preexistingMeetingds.Tables[0].Rows[i]["Campus"].ToString());
                sb.Append("<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Room: " + preexistingMeetingds.Tables[0].Rows[i]["Room"].ToString());
                sb.Append("<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Start Time: " + FormatTime(preexistingMeetingds.Tables[0].Rows[i]["StartTime"].ToString()));
                sb.Append("<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;End Time: " + FormatTime(preexistingMeetingds.Tables[0].Rows[i]["EndTime"].ToString()));
                sb.Append("<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Days: " + preexistingMeetingds.Tables[0].Rows[i]["Days"].ToString());
                sb.Append("<br/><br/>");
            }
        }
        preexistingMeetingds.Dispose();

        if (cbDropCourse.Checked)
        {
            sb.Append("Cancel Section<br>");
            sb.Append("<br /><b>Please cancel the selected course section.</b><br /><br />");
            //
            sb.Append(string.Format("<br /><b>Cancel Reason: &nbsp;</b>{0}<br /><br />",
                (ddlDropReason.SelectedValue == "Other Category" ? ddlDropReason.SelectedValue + " - " + txtOtherReason.Text : ddlDropReason.SelectedValue)
                ));
        }
        else
        {
            ClsTempFaculty faculty = new ClsTempFaculty("MasterSchedule");
            faculty.SectionId = Faculty1.sectionGUID;
            DataSet facultyds = faculty.FillDs();

            ClsTempExistingFaculty existingFaculty = new ClsTempExistingFaculty("MasterSchedule");
            existingFaculty.QsSectionsId = SectionsID;
            DataSet exitingFacultyds = existingFaculty.FillDs();

            if (facultyds.Tables[0].Rows.Count > 0 || exitingFacultyds.Tables[0].Rows.Count > 0)
            {
                sb.Append("Faculty<ul><b>");
            }
            for (int i = 0; i < facultyds.Tables[0].Rows.Count; i++)
                sb.Append("<li>Add Faculty: " + facultyds.Tables[0].Rows[i]["FacultyName"].ToString() + "</li>");

            facultyds.Dispose();

            if (exitingFacultyds.Tables[0].Rows.Count > 0)
            {
                sb.Append("<hr />");
                for (int i = 0; i < exitingFacultyds.Tables[0].Rows.Count; i++)
                    sb.Append("<li>Delete Faculty: " + exitingFacultyds.Tables[0].Rows[i]["FacultyName"].ToString() + "</li>");
            }
            exitingFacultyds.Dispose();

            sb.Append("</b></ul><br/>");

            ClsTempMeeting meeting = new ClsTempMeeting("MasterSchedule");
            meeting.SectionId = section.SectionId;
            DataSet meetingds = meeting.FillDs();

            ClsTempExistingMeeting existingMeeting = new ClsTempExistingMeeting("MasterSchedule");
            DataSet existingMeetingds = new DataSet();
            existingMeeting.SectionId = section.SectionId;
            existingMeetingds = existingMeeting.FillDs();

            if (meetingds.Tables[0].Rows.Count > 0 || existingMeetingds.Tables[0].Rows.Count > 0)
            {
                sb.Append("Meetings<br/><br/>");
            }

            if (meetingds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < meetingds.Tables[0].Rows.Count; i++)
                {
                    sb.Append("<b>Add Meeting: " + ((int)(i + 1)).ToString());
                    sb.Append("<br/>Campus: " + meetingds.Tables[0].Rows[i]["Campus"].ToString());
                    sb.Append("<br/>Room: " + meetingds.Tables[0].Rows[i]["Room"].ToString());
                    sb.Append("<br/>Start Time: " + meetingds.Tables[0].Rows[i]["StartTime"].ToString());
                    sb.Append("<br/>End Time: " + meetingds.Tables[0].Rows[i]["EndTime"].ToString());
                    sb.Append("<br/>Days: " + meetingds.Tables[0].Rows[i]["Days"].ToString());
                    sb.Append("<br/>Type: " + meetingds.Tables[0].Rows[i]["Type"].ToString());
                    sb.Append("</b><br/><br/>");
                }
            }
            meetingds.Dispose();

            if (existingMeetingds.Tables[0].Rows.Count != 0)
            {
                sb.Append("<b>-----------------------------------------------------</b><br/>");
                for (int i = 0; i < existingMeetingds.Tables[0].Rows.Count; i++)
                {
                    sb.Append("<b>Drop Meeting: " + ((int)(i + 1)).ToString());
                    sb.Append("<br/>Campus: " + existingMeetingds.Tables[0].Rows[i]["Campus"].ToString());
                    sb.Append("<br/>Room: " + existingMeetingds.Tables[0].Rows[i]["Room"].ToString());
                    sb.Append("<br/>Start Time: " + FormatTime(existingMeetingds.Tables[0].Rows[i]["StartTime"].ToString()));
                    sb.Append("<br/>End Time: " + FormatTime(existingMeetingds.Tables[0].Rows[i]["EndTime"].ToString()));
                    sb.Append("<br/>Days: " + existingMeetingds.Tables[0].Rows[i]["Days"].ToString());
                    sb.Append("</b><br/><br/>");
                }
            }
            existingMeetingds.Dispose();

            //if (rbliTunesu.SelectedIndex == 0 || rbliTunesu.SelectedIndex == 1) {
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

            if (radDirectContentFee.SelectedIndex < 2)
            {
                sb.Append("Direct Content Fee<ul><b>");
                sb.Append(string.Concat("<li>", radDirectContentFee.SelectedValue, "</li>"));
                sb.Append("</b></ul><br />");
            }

            ClsTempColink colink = new ClsTempColink("MasterSchedule");
            colink.SectionId = section.SectionId;
            DataSet colinkds = colink.FillDs();
            if (colinkds.Tables[0].Rows.Count > 0)
            {
                sb.Append("Co-Listed Courses<ul><b>");
                for (int i = 0; i < colinkds.Tables[0].Rows.Count; i++)
                    sb.Append("<li>" + colinkds.Tables[0].Rows[i]["Action"].ToString() + " : " + colinkds.Tables[0].Rows[i]["Department"].ToString() + " - " + colinkds.Tables[0].Rows[i]["Course"].ToString() + " - " + colinkds.Tables[0].Rows[i]["Section"].ToString() + "</li>");
                sb.Append("</b></ul><br/>");
            }
            colinkds.Dispose();

            //clsTempLink link = new clsTempLink("MasterSchedule");
            //link.SectionID = section.SectionID;
            //DataSet linkds = link.FillDs();
            //if (linkds.Tables[0].Rows.Count > 0)
            //{
            //    sb.Append("Cross Linked Courses<ul><b>");
            //    for (int i = 0; i < linkds.Tables[0].Rows.Count; i++)
            //        sb.Append("<li>" + linkds.Tables[0].Rows[i]["Action"].ToString() + " : " + linkds.Tables[0].Rows[i]["Department"].ToString() + " - " + linkds.Tables[0].Rows[i]["Course"].ToString() + " - " + linkds.Tables[0].Rows[i]["Section"].ToString() + "</li>");

            //    sb.Append("</b></ul><br/>");
            //}
            //linkds.Dispose();

            ClsTempCoreq coreq = new ClsTempCoreq("MasterSchedule");
            coreq.SectionId = section.SectionId;
            DataSet coreqds = coreq.FillDs();
            if (coreqds.Tables[0].Rows.Count > 0)
            {
                sb.Append("Co-requisite/Linked Courses<ul><b>");
                for (int i = 0; i < coreqds.Tables[0].Rows.Count; i++)
                    sb.Append("<li>" + coreqds.Tables[0].Rows[i]["Action"].ToString() + " : " + coreqds.Tables[0].Rows[i]["Department"].ToString() + " - " + coreqds.Tables[0].Rows[i]["Course"].ToString() + " - " + coreqds.Tables[0].Rows[i]["Section"].ToString() + "</li>");
                sb.Append("</b></ul><br/>");
            }
            coreqds.Dispose();
        }

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
        CourseConfirmationLabel.Text = "Section ID: " + SectionsID.ToString() + "<br /><br />" + getCourseInfo();
        /* Delete Temp Records */
        GridView gvMeetings = (GridView)Meetings_New1.FindControl("gvExistingMeeting");
        foreach (GridViewRow thisRow in gvMeetings.Rows)
        {
            hidRooms.Value += thisRow.Cells[0].Text + ",";
        }

        GridView gvMeetings2 = (GridView)Meetings_New1.FindControl("gvMeetings");
        foreach (GridViewRow thisRow in gvMeetings2.Rows)
        {
            hidRooms.Value += thisRow.Cells[0].Text + ",";
        }

        mvSubmissionForm.ActiveViewIndex = 2;
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        bool displayPanels = false;
        if (cbDropCourse.Checked)
        {
            displayPanels = false;
            pnlOtherInformation.Visible = true;
        }
        else
        {
            displayPanels = true;
            pnlOtherInformation.Visible = false;
        }

        pnlHeaderColist.Visible = displayPanels;
        pnlHeaderCoreq.Visible = displayPanels;
        //     pnlHeaderCrosslink.Visible = displayPanels;
        pnlHeaderFaculty.Visible = displayPanels;
        pnlHeaderMeetings.Visible = displayPanels;
        pnlMeetings.Visible = displayPanels;
        pnlFaculty.Visible = displayPanels;
        pnlCoreq.Visible = displayPanels;
        //  pnlCrosslink.Visible = displayPanels;
        pnlColist.Visible = displayPanels;
    }

    protected string FormatTime(string MeetTime)
    {
        string tempstring = string.Empty;
        if (!string.IsNullOrEmpty(MeetTime))
        {
            try
            {
                DateTime temp = DateTime.Parse(MeetTime);
                tempstring = temp.ToString("h:mm tt");
            }
            catch
            {
                tempstring = "";
            }
        }
        return tempstring;
    }

    protected void ddlDropReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList Sender = (DropDownList)sender;

        if (Sender.SelectedValue == "Other Category")
        {
            txtOtherReason.Enabled = true;
        }
        else
        {
            txtOtherReason.Enabled = false;
        }
    }
}