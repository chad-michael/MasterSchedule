using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

namespace Controls
{
    public partial class Controls_Faculty_Modify : System.Web.UI.UserControl
    {
        public ClsTempFaculty faculty;
        public ClsTempExistingFaculty existingFaculty;
        private Guid _sectionID;
        private StringBuilder sb = new StringBuilder();
        private StringBuilder sb2 = new StringBuilder();
        private bool modifiedFaculty = false;
        private int qsSectionsID;

        public Guid sectionGUID
        {
            get { return _sectionID; }
            set { _sectionID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //SectionID.Value = Session["NewSectionID"].ToString();
            qsSectionsID = Int32.Parse(Request.QueryString["SectionsID"]);
            faculty = new ClsTempFaculty("MasterSchedule");
            faculty.SectionId = sectionGUID;
            existingFaculty = new ClsTempExistingFaculty("MasterSchedule");
            existingFaculty.SectionId = sectionGUID;

            existingFaculty.QsSectionsId = qsSectionsID;

            if (!Page.IsPostBack)
            {
                setExistingFaculty();
            }
        }

        protected void gvFacultyExistingAssignment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "deleteexistingfaculty")
            {
                modifiedFaculty = true;
                existingFaculty.RecordId = Int32.Parse(e.CommandArgument.ToString());
                existingFaculty.FlagRecord();

                gvFacultyExistingAssignments.DataBind();
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

        public string getModifiedFaculty()
        {
            string tempString = string.Empty;
            if (modifiedFaculty)
            {
                tempString = "<hr />" + DeletedFaculty.ToString();
            }

            return tempString;
        }

        public void setExistingFaculty()
        {
            SqlConnection dbCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterSchedule"].ConnectionString.ToString());
            SqlCommand sqlCmd = new SqlCommand("List_FacultyAssignments", dbCon);

            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add("@SectionsID", SqlDbType.Int).Value = qsSectionsID;

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
                    existingFaculty.FacultyName = t.Rows[x]["FullName"].ToString();
                    existingFaculty.FacultyId = Int32.Parse(t.Rows[x]["IDNO"].ToString());
                    existingFaculty.AddRecord();
                    x += 1;
                }
            }
        }
    }
}