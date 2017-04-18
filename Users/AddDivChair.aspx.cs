using System;
using System.Web.UI.WebControls;

namespace Users
{
    public partial class Users_Users_AddDivChair : System.Web.UI.Page
    {
        private ClsDivisionChairs dc;

        protected void Page_Load(object sender, EventArgs e)
        {
            dc = new ClsDivisionChairs("MasterSchedule");

            if (Session["respondToParams"] != null)
            {
                if ((bool)Session["respondToParams"] == true)
                {
                    if (Session["facultyNumber"] != null)
                    {
                        txtIDNO.Text = Session["facultyNumber"].ToString();
                    }

                    if (Session["divisionSelected"] != null)
                    {
                        ddlDivision.SelectedIndex = int.Parse(Session["divisionSelected"].ToString());
                    }

                    Session["respondToParams"] = false;
                }
            }
        }

        protected void btnAddDivChair_Click(object sender, EventArgs e)
        {
            errorBox.Visible = false;
            try
            {
                dc.DivisionName = ddlDivision.SelectedItem.Text.ToString();
                dc.CrpDivisionId = Int32.Parse(ddlDivision.SelectedValue.ToString());
                dc.Idno = Int32.Parse(txtIDNO.Text);
                dc.AddRecord();

                Response.Redirect("addDivChair.aspx");
            }
            catch
            {
                errorBox.Visible = true;
            }
        }

        protected void btnGetFaculty_Click(object sender, EventArgs e)
        {
            RefreshSearchFaculty(0);
        }

        protected void RefreshSearchFaculty(int pageIndex)
        {
            gvStudents.DataSource = FacultyInformation.GetFacultyIdno(txtFirstName.Text, txtLastName.Text, txtUserName.Text);
            gvStudents.PageIndex = pageIndex;
            gvStudents.DataBind();
        }

        protected void gvStudents_PageChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshSearchFaculty(e.NewPageIndex);
        }

        protected void gvStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView Sender = (GridView)sender;
            //Response.Redirect("Default.aspx?studentNumber=" + Sender.Rows[Sender.SelectedIndex].Cells[0].Text + "&" +
            //                                "startingDate=" + txtStartDate.Text + "&" + "endingDate=" + txtEndDate.Text);

            txtIDNO.Text = Sender.Rows[Sender.SelectedIndex].Cells[0].Text;
        }

        protected void btnSelectFaculty_Clicked(object sender, EventArgs e)
        {
            ImageButton Sender = (ImageButton)sender;

            txtIDNO.Text = Sender.CommandArgument;

            Session["respondToParams"] = true;
            Session["facultyNumber"] = Sender.CommandArgument;
            Session["divisionSelected"] = ddlDivision.SelectedIndex.ToString();

            Response.Redirect("AddDivChair.aspx");
        }
    }
}