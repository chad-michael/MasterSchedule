using System;
using System.Text;
using System.Web.UI.WebControls;

namespace Controls
{
    public partial class Controls_Faculty : System.Web.UI.UserControl
    {
        private StringBuilder sb = new StringBuilder();
        private ClsChangeLog log = new ClsChangeLog("MasterSchedule");
        private bool modifiedFaculty = false;

        public ClsTempFaculty faculty;
        private Guid _sectionID;

        public Guid sectionGUID
        {
            get { return _sectionID; }
            set { _sectionID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            faculty = new ClsTempFaculty("MasterSchedule");
            faculty.SectionId = sectionGUID;
            if (!Page.IsPostBack)
            {
            }
        }

        protected void lnkAddFaculty_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void gvFacultyAssignment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "deletefaculty")
            {
                //log.SectionsID = int.Parse(Request["SectionsID"]);
                //log.SubmittedBy = Session["deltaid"].ToString();
                sb.Append("<li>");
                sb.Append("Drop Faculty: " + e.CommandArgument.ToString());
                sb.Append("</li>");
                modifiedFaculty = true;
                //log.AddRecord();
                Helpers.RefreshSortPending((GridView)Helpers.FindControlRecursive(Page.Master, "gvPendingChanges")); //Refresh the pending list
            }
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnAddFaculty_Click(object sender, EventArgs e)
        {
            sb.Append("<li>");
            sb.Append("<h4>Add Faculty:</h4> ");
            sb.Append(lbFacultyUnassigned.SelectedItem.Text);
            sb.Append("</li>");
            modifiedFaculty = true;
            lbFacultyUnassigned.SelectedIndex = -1;

            //log.SectionsID = int.Parse(Request["SectionsID"]);
            //log.SubmittedBy = Session["deltaid"].ToString();
            //log.Change = sb.ToString();
            //log.AddRecord();

            Helpers.RefreshSortPending((GridView)Helpers.FindControlRecursive(Page.Master, "gvPendingChanges")); //Refresh the pending list
        }

        public string getModifiedFaculty()
        {
            string FacultyList;
            FacultyList = string.Empty;

            if (modifiedFaculty)
            {
                FacultyList += sb.ToString();
            }

            return FacultyList;
        }
    }
}