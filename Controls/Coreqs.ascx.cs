using System;
using System.Web.UI.WebControls;

namespace Controls
{
    public partial class Controls_Coreqs : System.Web.UI.UserControl
    {
        private ClsChangeLog log = new ClsChangeLog("MasterSchedule");

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            log.SectionsId = int.Parse(Request["SectionsID"]);
            log.SubmittedBy = Session["deltaid"].ToString();
            log.Change = ddAction.SelectedValue + " Co-requisite: " + txtDepartment.Text + " - " + txtCourseNumber.Text + " - " + txtSectionNumber.Text;
            log.AddRecord();

            txtCourseNumber.Text = "";
            txtDepartment.Text = "";
            txtSectionNumber.Text = "";

            Helpers.RefreshSortPending((GridView)Helpers.FindControlRecursive(Page.Master, "gvPendingChanges")); //Refresh the pending list
        }

        protected void ddAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSubmit.Text = ddAction.SelectedValue + " Corequisite";
        }
    }
}