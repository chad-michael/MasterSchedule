using System;
using System.Text;
using System.Web.UI.WebControls;

namespace Controls
{
    public partial class Controls_Meetings : System.Web.UI.UserControl
    {
        private ClsChangeLog log = new ClsChangeLog("MasterSchedule");
        private int SectionID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            int.TryParse(Request["SectionsID"], out SectionID);
            if (SectionID <= 0)
                Response.Redirect("~/?Error=SectionDetails.ascx+had+no+SectionID");

            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;

                ClsSections section = new ClsSections("MasterSchedule");
                section.SectionsId = SectionID;
                section.GetRecord();
                txtEditTermEndDate.Text = section.SectionEndDate.ToShortDateString();
                txtEditTermStartDate.Text = section.SectionStartDate.ToShortDateString();
            }
        }

        public string FormatMeetDays(object MeetDays)
        {
            return MeetDays.ToString().Replace(":", "");
        }

        protected void btnUpdateMeeting_Click(object sender, EventArgs e)
        {
            DateTime selectdDate = new DateTime();
            selectdDate = DateTime.Parse(txtEditTermStartDate.Text);
            int semStartDate;
            semStartDate = (int)selectdDate.DayOfWeek;

            updateDateError.Visible = false;
            ClsRooms room = new ClsRooms("MasterSchedule");
            //if (semStartDate != cblDays.SelectedIndex && room.CheckIsBrickAndMortar(ddRoom.SelectedItem.Text))
            //{
            //    updateDateError.Visible = true;
            //}
            //else
            //{
            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\"><tr>");
            sb.Append("<td colspan=\"2\"><h4>Change Meeting:</h4></td></tr><tr>");
            sb.Append("<td style=\"padding-left:10px;\">From:</td>");
            sb.Append("<td style=\"padding-left:10px;\">To:</td></tr><tr>");
            sb.Append("<td style=\"padding-left:10px;\">");

            ClsSectionMeetings sm = new ClsSectionMeetings("MasterSchedule");
            sm.SectionMeetingsId = int.Parse(UpdateMeetingID.Value);
            sm.GetRecord();
            ClsSections section = new ClsSections("MasterSchedule");
            section.SectionsId = SectionID;
            section.GetRecord();

            room.RoomsId = sm.RoomsId;
            room.GetRecord();
            ClsCampuses campus = new ClsCampuses("MasterSchedule");
            campus.CampusesId = room.CampusesId;
            campus.GetRecord();

            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<tr><td style=\"padding-right:5px;\">Campus:</td><td " + MarkOrig(campus.CampusCode, ddCampus.SelectedItem.Text) + "</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">Room:</td><td " + MarkOrig(room.RoomNumber, ddRoom.SelectedItem.Text) + "</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">Days:</td><td>" + sm.MeetDays.Replace(":", "") + "</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">Start Time:</td><td>");

            if (sm.MeetStartTime != DateTime.MinValue)
                sb.Append(sm.MeetStartTime.ToShortTimeString());
            sb.Append("</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">End Time:</td><td>");
            if (sm.MeetEndTime != DateTime.MinValue)
                sb.Append(sm.MeetEndTime.ToShortTimeString());
            sb.Append("</td></tr>");

            sb.Append("<tr><td>Section Start:</td><td " + MarkOrig_DateType(section.SectionStartDate, txtEditTermStartDate.Text) + "</td></tr>");
            sb.Append("<tr><td>Section End:</td><td " + MarkOrig_DateType(section.SectionEndDate, txtEditTermEndDate.Text) + "</td></tr>");
            sb.Append("</table>");

            sb.Append("</td>");
            sb.Append("<td style=\"padding-left:10px;\">");

            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<tr><td style=\"padding-right:5px;\">Campus:</td><td " + MarkChanges(campus.CampusCode, ddCampus.SelectedItem.Text) + "</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">Room:</td><td " + MarkChanges(room.RoomNumber, ddRoom.SelectedItem.Text) + "</td></tr>");

            sb.Append("<tr><td style=\"padding-right:5px;\">Days:</td><td>");
            foreach (ListItem item in cblDays.Items)
            {
                if (item.Selected) { if (_meetChanged) { sb.Append("<strong>"); } sb.Append(item.Value); if (_meetChanged) { sb.Append("</strong>"); } }
            }

            sb.Append("</td></tr>");

            sb.Append("<tr><td style=\"padding-right:5px;\">Start Time:</td><td " + MarkChanges_TimeType(sm.MeetStartTime, txtStartTime.Text) + "</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">End Time:</td><td " + MarkChanges_TimeType(sm.MeetEndTime, txtEndTime.Text) + "</td></tr>");

            sb.Append("<tr><td>Start Date:</td><td " + MarkChanges_DateType(section.SectionStartDate, txtEditTermStartDate.Text) + "</td></tr>");
            sb.Append("<tr><td>End Date:</td><td " + MarkChanges_DateType(section.SectionEndDate, txtEditTermEndDate.Text) + "</td></tr>");
            sb.Append("</table>");

            sb.Append("</td></tr></table>");

            log.SectionsId = int.Parse(Request["SectionsID"]);
            log.SubmittedBy = Session["deltaid"].ToString();
            log.Change = sb.ToString();
            log.ProcessGroup = "divsionchairs";
            log.AddRecord();

            Helpers.RefreshSortPending((GridView)Helpers.FindControlRecursive(Page.Master, "gvPendingChanges")); //Refresh the pending list

            MultiView1.ActiveViewIndex = 0;
            UpdateMeetingID.Value = "";
            //}
        }

        protected string MarkOrig(string orig, string newvalue)
        {
            if (orig == newvalue)
            {
                return orig;
            }
            else
            {
                return "class='change'><strong>" + orig + "</strong>";
            }
        }

        protected string MarkChanges(string orig, string newvalue)
        {
            if (orig == newvalue)
            {
                return newvalue;
            }
            else
            {
                return "class='change'><strong>" + newvalue + "</strong>";
            }
        }

        protected string MarkChanges_TimeType(DateTime orig, string newvalue)
        {
            if (!string.IsNullOrEmpty(newvalue))
            {
                DateTime newDate = DateTime.Parse(newvalue);
                if (orig.ToShortTimeString() == newDate.ToShortTimeString())
                {
                    return ">" + newDate.ToShortTimeString();
                }
                else
                {
                    return "class='change'><strong>" + newDate.ToShortTimeString() + "</strong>";
                }
            }
            else
            {
                return ">";
            }
        }

        protected string MarkOrig_TimeType(DateTime orig, string newvalue)
        {
            if (!string.IsNullOrEmpty(newvalue))
            {
                DateTime newDate = DateTime.Parse(newvalue);
                if (orig.ToShortTimeString() == newDate.ToShortTimeString())
                {
                    return ">" + orig.ToShortTimeString();
                }
                else
                {
                    return "class='change'><strong>" + orig.ToShortTimeString() + "</strong>";
                }
            }
            else
            {
                return ">";
            }
        }

        protected string MarkChanges_DateType(DateTime orig, string newvalue)
        {
            if (!string.IsNullOrEmpty(newvalue))
            {
                DateTime newDate = DateTime.Parse(newvalue);
                if (orig.ToShortDateString() == newDate.ToShortDateString())
                {
                    return ">" + newDate.ToShortDateString();
                }
                else
                {
                    return "class='change'><strong>" + newDate.ToShortDateString() + "</strong>";
                }
            }
            else
            {
                return ">";
            }
        }

        protected string MarkOrig_DateType(DateTime orig, string newvalue)
        {
            if (!string.IsNullOrEmpty(newvalue))
            {
                DateTime newDate = DateTime.Parse(newvalue);
                if (orig.ToShortDateString() == newDate.ToShortDateString())
                {
                    return ">" + orig.ToShortDateString();
                }
                else
                {
                    return "class='change'><strong>" + orig.ToShortDateString() + "</strong>";
                }
            }
            else
            {
                return ">";
            }
        }

        protected void btnUpdateCancel_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnAddMeeting_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\"><tr><td valign=\"top\">Add Meeting:</td><td valign=\"top\" style=\"padding-left:10px;\"><table cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<tr><td style=\"padding-right:5px;\">Campus:</td><td>" + ddAddCampus.SelectedItem.Text + "</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">Room:</td><td>" + ddAddRoom.SelectedItem.Text + "</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">Days:</td><td>");
            foreach (ListItem item in cblAddDays.Items)
                if (item.Selected) { if (_meetChanged) { sb.Append("<strong>"); } sb.Append(item.Value); if (_meetChanged) { sb.Append("</strong>"); } }
            sb.Append("</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">Start Time:</td><td>" + txtAddStartTime.Text + "</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">End Time:</td><td>" + txtAddEndTime.Text + "</td></tr>");
            sb.Append("<tr><td>Semester Start:</td><td " + txtAddTermStartDate.Text + "</td></tr>");
            sb.Append("<tr><td>Semester End:</td><td " + txtAddTermEndDate.Text + "</td></tr>");
            sb.Append("</table></td></tr></table>");

            log.SectionsId = int.Parse(Request["SectionsID"]);
            log.SubmittedBy = Session["deltaid"].ToString();
            log.Change = sb.ToString();
            log.ProcessGroup = "divisionchairs";
            log.AddRecord();

            Helpers.RefreshSortPending((GridView)Helpers.FindControlRecursive(Page.Master, "gvPendingChanges")); //Refresh the pending list

            ddAddCampus.SelectedIndex = 0;
            ddAddRoom.DataBind();
            ddAddRoom.SelectedIndex = 0;
            foreach (ListItem item in cblAddDays.Items)
                item.Selected = false;
            txtAddStartTime.Text = "";
            txtAddEndTime.Text = "";
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnAddCancel_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void lnkAddMeeting_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }

        private bool _meetChanged = false;

        protected void gvMeetings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editmeeting")
            {
                UpdateMeetingID.Value = e.CommandArgument.ToString();
                MultiView1.ActiveViewIndex = 1;
                ClsSectionMeetings meeting = new ClsSectionMeetings("MasterSchedule");
                meeting.SectionMeetingsId = int.Parse(e.CommandArgument.ToString());
                meeting.GetRecord();
                ClsRooms room = new ClsRooms("MasterSchedule");
                room.RoomsId = meeting.RoomsId;
                room.GetRecord();

                ddCampus.DataBind();
                ddCampus.SelectedValue = room.CampusesId.ToString();

                ddRoom.DataBind();
                ddRoom.SelectedValue = meeting.RoomsId.ToString();

                //When we switch to the edit view, we are going to disable the validation for the times fields if the course
                //is an inet tnet tele courses.
                //Durring the actual update process, we also check the value so we do not attempt to parse a empty datetime entry
                if (!room.CheckIsBrickAndMortar(ddRoom.SelectedItem.Text))
                {
                    btnUpdateMeeting.ValidationGroup = "";
                }
                else
                {
                    btnUpdateMeeting.ValidationGroup = "editmeeting";
                }

                foreach (ListItem item in cblDays.Items)
                    item.Selected = meeting.MeetDays.ToLower().Contains(item.Value.ToLower());

                if (meeting.MeetStartTime > DateTime.MinValue)
                    txtStartTime.Text = meeting.MeetStartTime.ToShortTimeString();
                else
                    txtStartTime.Text = "";

                if (meeting.MeetEndTime > DateTime.MinValue)
                    txtEndTime.Text = meeting.MeetEndTime.ToShortTimeString();
                else
                    txtEndTime.Text = "";
            }
            if (e.CommandName == "deletemeeting")
            {
                log.SectionsId = int.Parse(Request["SectionsID"]);
                log.SubmittedBy = Session["deltaid"].ToString();
                log.Change = e.CommandArgument.ToString();
                log.AddRecord();

                Helpers.RefreshSortPending((GridView)Helpers.FindControlRecursive(Page.Master, "gvPendingChanges")); //Refresh the pending list
            }
        }

        public string FormatDeleteEntry(object CampusCode, object RoomNumber, object Days, object MeetStartTime, object MeetEndTime)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellpadding=\"0\" cellspacing=\"0\"><tr><td valign=\"top\">Drop Meeting:</td><td valign=\"top\" style=\"padding-left:10px;\"><table cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<tr><td style=\"padding-right:5px;\">Campus:</td><td>" + CampusCode.ToString() + "</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">Room:</td><td>" + RoomNumber.ToString() + "</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">Days:</td><td>" + Days.ToString() + "</td></tr>");
            sb.Append("<tr><td style=\"padding-right:5px;\">Time:</td><td>");

            if (MeetStartTime != DBNull.Value && MeetEndTime != DBNull.Value)
            {
                DateTime st = Convert.ToDateTime(MeetStartTime);
                DateTime et = Convert.ToDateTime(MeetEndTime);
                sb.Append(st.ToShortTimeString() + " - " + et.ToShortTimeString());
            }

            if (MeetStartTime != DBNull.Value && MeetEndTime == DBNull.Value)
            {
                DateTime st = Convert.ToDateTime(MeetStartTime);
                sb.Append(st.ToShortTimeString() + " - ?");
            }

            if (MeetStartTime == DBNull.Value && MeetEndTime != DBNull.Value)
            {
                DateTime et = Convert.ToDateTime(MeetEndTime);
                sb.Append("? - " + et.ToShortTimeString());
            }

            sb.Append("</td></tr></table></td></tr></table>");
            return sb.ToString();
        }

        protected void cblAddDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            _meetChanged = true;
        }

        protected void ddCampus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //When we switch to the edit view, we are going to disable the validation for the times fields if the course
            //is an inet tnet tele courses.
            //Durring the actual update process, we also check the value so we do not attempt to parse a empty datetime entry
            ClsRooms room = new ClsRooms("MasterSchedule");
            if (!room.CheckIsBrickAndMortar(ddRoom.SelectedItem.Text))
            {
                btnUpdateMeeting.ValidationGroup = "";
            }
            else
            {
                btnUpdateMeeting.ValidationGroup = "editmeeting";
            }
        }
    }
}