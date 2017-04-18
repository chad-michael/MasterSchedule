using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Controls
{
    public partial class Controls_Meetings_Modify2 : System.Web.UI.UserControl
    {
        private ClsTempMeeting meeting;
        private ClsTempExistingMeeting existingMeeting;

        protected void Page_Load(object sender, EventArgs e)
        {
            meeting = new ClsTempMeeting("MasterSchedule");
            meeting.SectionId = (Guid)Session["NewSectionID"];

            existingMeeting = new ClsTempExistingMeeting("MasterSchedule");
            existingMeeting.SectionId = (Guid)Session["NewSectionID"];

            if (!IsPostBack)
            {
                //MultiView1.ActiveViewIndex = 0;
                SectionID.Value = Session["NewSectionID"].ToString();
                setExistingMeetings();
                //Set CNET as only room listing for CNET Campus
                ddAddRoom.Items.Clear();
                ddAddRoom.Items.Add("TBD");
                ddAddRoom.Items.Add("BREC");

                ddAddBuilding.ClearSelection();
            }
        }

        public string FormatMeetDays(object MeetDays)
        {
            return MeetDays.ToString().Replace(":", "");
        }

        public string FormatTime(object MeetTime)
        {
            string tempstring = string.Empty;
            if (!string.IsNullOrEmpty(MeetTime.ToString()))
            {
                try
                {
                    DateTime temp = DateTime.Parse(MeetTime.ToString());
                    tempstring = temp.ToString("h:mm tt");
                }
                catch
                {
                    tempstring = "";
                }
            }
            return tempstring;
        }

        //protected void btnUpdateCancel_Click(object sender, EventArgs e) {
        //    MultiView1.ActiveViewIndex = 0;
        //}
        protected void btnAddMeeting_Click(object sender, EventArgs e)
        {
            meeting.Campus = ddAddCampus.SelectedItem.Text;
            meeting.EndTime = txtAddEndTime.Text;
            meeting.Building = ddAddBuilding.SelectedItem.Text;
            meeting.Room = ddAddRoom.SelectedItem.Text;
            meeting.StartTime = txtAddStartTime.Text;
            meeting.MeetType = MeetingTypeRadioButtonList.SelectedValue.ToString();
            meeting.Days = "";
            foreach (ListItem item in cblAddDays.Items)
                if (item.Selected) { meeting.Days += item.Value; }
            meeting.AddRecord();

            ddAddCampus.SelectedIndex = 0;

            ddAddBuilding.DataBind();
            ddAddBuilding.SelectedIndex = 0;

            ddAddRoom.DataBind();
            ddAddRoom.SelectedIndex = 0;
            foreach (ListItem item in cblAddDays.Items)
                item.Selected = false;
            txtAddStartTime.Text = "";
            txtAddEndTime.Text = "";
            //MultiView1.ActiveViewIndex = 0;
            gvMeetings.DataBind();

            MeetingTypeRadioButtonList.SelectedIndex = -1;
            ddAddRoom.Items.Clear();
            ddAddRoom.Items.Add("TBD");
            ddAddRoom.Items.Add("BREC");
            ddAddBuilding.Items.Clear();
        }

        protected void btnAddCancel_Click(object sender, EventArgs e)
        {
            // MultiView1.ActiveViewIndex = 0;
        }

        //protected void lnkAddMeeting_Click(object sender, EventArgs e) {
        //    MultiView1.ActiveViewIndex = 1;
        //}

        protected void gvExistingMeetings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "deleteExistingMeeting")
            {
                existingMeeting.RecordId = Int32.Parse(e.CommandArgument.ToString());
                existingMeeting.FlagRecord();
                gvExistingMeeting.DataBind();
            }
        }

        protected void gvMeetings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deletemeeting")
            {
                meeting.MeetingId = int.Parse(e.CommandArgument.ToString());
                meeting.DeleteRecord();
                gvMeetings.DataBind();
            }
        }

        private void setExistingMeetings()
        {
            SqlConnection dbCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterSchedule"].ConnectionString.ToString());
            SqlCommand sqlCmd = new SqlCommand("List_Room", dbCon);

            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT [SectionMeetingsID], [MeetDays], [MeetStartTime], [MeetEndTime], [RoomNumber], [Building], [CampusCode] FROM [SectionMeetings] WHERE [SectionsID] = " + Request.QueryString["SectionsID"];

            dbCon.Open();
            SqlDataReader rdr = null;
            rdr = sqlCmd.ExecuteReader();
            DataTable t = new DataTable();
            t.Load(rdr);
            rdr.Close();
            dbCon.Close();
            if (t.Rows.Count > 0)
            {
                int x = 0;
                while (x < t.Rows.Count)
                {
                    //existingMeeting.SectionID = (Guid)t.Rows[x]["SectionMeetingsID"];
                    existingMeeting.Days = t.Rows[x]["MeetDays"].ToString();
                    existingMeeting.StartTime = t.Rows[x]["MeetStartTime"].ToString();
                    existingMeeting.EndTime = t.Rows[x]["MeetEndTime"].ToString();
                    existingMeeting.Room = t.Rows[x]["RoomNumber"].ToString();
                    existingMeeting.Building = t.Rows[x]["Building"].ToString();
                    existingMeeting.Campus = t.Rows[x]["CampusCode"].ToString();

                    existingMeeting.AddRecord();
                    x += 1;
                }
            }
        }

        protected void ddAddCampus_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ddAddRoom.Items.Clear();
            if (ddAddCampus.SelectedValue == "12")
            {
                ddAddRoom.Items.Add("CNET");
            }
            else if (ddAddCampus.SelectedValue == "24" || ddAddCampus.SelectedValue == "27")
            {
                ddAddRoom.Items.Add("TBD");
            }
            else
            {
                ArrayList newListItem = new ArrayList();
                newListItem.Add("TBD");

                SqlConnection dbCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterSchedule"].ConnectionString.ToString());
                SqlCommand sqlCmd = new SqlCommand("List_Room", dbCon);

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@CampusesID", SqlDbType.Int).Value = Int32.Parse(ddAddCampus.SelectedValue);

                dbCon.Open();
                SqlDataReader rdr = null;
                rdr = sqlCmd.ExecuteReader();
                while (rdr.Read())
                {
                    newListItem.Add(rdr.GetString(1));
                    ddAddRoom.DataSource = newListItem;
                    ddAddRoom.DataBind();
                }
                rdr.Close();
                dbCon.Close();
            }
        }
    }
}