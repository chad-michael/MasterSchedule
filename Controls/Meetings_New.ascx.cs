using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Controls
{
    public partial class Controls_Meetings_New : System.Web.UI.UserControl
    {
        private ClsTempMeeting meeting;

        protected void Page_Load(object sender, EventArgs e)
        {
            meeting = new ClsTempMeeting("MasterSchedule");
            meeting.SectionId = (Guid)Session["NewSectionID"];

            if (!IsPostBack)
            {
                //MultiView1.ActiveViewIndex = 0;
                SectionID.Value = Session["NewSectionID"].ToString();
                ddAddRoom.Items.Clear();
                ddAddRoom.Items.Add("TBD");
                ddAddRoom.Items.Add("BREC");
            }

            // if(ddAddRoom.Items.Count() == 0)
            //   ddAddRoom.Items.Add("TBD");
        }

        public string FormatMeetDays(object MeetDays)
        {
            return MeetDays.ToString().Replace(":", "");
        }

        //protected void btnUpdateCancel_Click(object sender, EventArgs e) {
        //    MultiView1.ActiveViewIndex = 0;
        //}
        protected void btnAddMeeting_Click(object sender, EventArgs e)
        {
            meeting.Campus = ddAddCampus.SelectedItem.Text;
            meeting.EndTime = txtAddEndTime.Text;
            meeting.Room = ddAddRoom.SelectedItem.Text;
            meeting.StartTime = txtAddStartTime.Text;
            meeting.MeetType = MeetingTypeRadioButtonList.SelectedValue.ToString();
            meeting.Days = "";
            foreach (ListItem item in cblAddDays.Items)
                if (item.Selected) { meeting.Days += item.Value; }
            meeting.AddRecord();

            ddAddCampus.SelectedIndex = 0;
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
        }

        protected void btnAddCancel_Click(object sender, EventArgs e)
        {
            // MultiView1.ActiveViewIndex = 0;
        }

        //protected void lnkAddMeeting_Click(object sender, EventArgs e) {
        //    MultiView1.ActiveViewIndex = 1;
        //}
        protected void gvMeetings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deletemeeting")
            {
                meeting.MeetingId = int.Parse(e.CommandArgument.ToString());
                meeting.DeleteRecord();
                gvMeetings.DataBind();
            }
        }

        protected void ddAddCampus_SelectedIndexChanged(object sender, EventArgs e)
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

            // if(ddAddRoom.Items.Count() == 0)
            //     ddAddRoom.Items.Add("TBD");
        }
    }
}