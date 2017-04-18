using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Controls
{
    public partial class Controls_Faculty_New : System.Web.UI.UserControl
    {
        public ClsTempFaculty faculty;
        private Guid _sectionID;

        public Guid sectionGUID
        {
            get { return _sectionID; }
            set { _sectionID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //SectionID.Value = Session["NewSectionID"].ToString();
            faculty = new ClsTempFaculty("MasterSchedule");
            faculty.SectionId = sectionGUID;
            if (!Page.IsPostBack)
            {
            }
        }

        protected void gvFacultyAssignment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "deletefaculty")
            {
                faculty.FacultyId = int.Parse(e.CommandArgument.ToString());
                faculty.DeleteRecord();
                gvFacultyAssignment.DataBind();
            }
        }

        private DataTable getFaculty()
        {
            if (Cache["facListCache"] != null)
            {
                return (DataTable)Cache["facListCache"];
            }
            else
            {
                SqlConnection dbCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterSchedule"].ConnectionString.ToString());
                SqlCommand sqlCmd = new SqlCommand("List_FacultyUnassigned", dbCon);

                sqlCmd.CommandType = CommandType.StoredProcedure;

                dbCon.Open();
                SqlDataReader rdr = null;
                rdr = sqlCmd.ExecuteReader();
                DataTable t = new DataTable();
                t.Load(rdr);
                rdr.Close();
                dbCon.Close();
                Cache.Insert("facListCache", t, null, DateTime.Now.AddHours(2), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);

                return t;
            }
        }

        protected void btnAddFaculty_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFaculty.Text))
            {
                selectionErrorLabel.Visible = false;
                faculty.FacultyName = txtFaculty.Text;
                faculty.AddRecord();
                txtFaculty.Text = "";
                gvFacultyAssignment.DataBind();
            }
            else
            {
                selectionErrorLabel.Visible = true;
            }
        }
    }
}