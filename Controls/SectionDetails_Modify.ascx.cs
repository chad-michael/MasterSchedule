using System;

namespace Controls
{
    public partial class Controls_SectionDetails_Modify : System.Web.UI.UserControl
    {
        //expose values
        public string Year
        {
            get { return ddYear.SelectedItem.Text; }
            set { ddYear.SelectedItem.Text = value; }
        }

        public string Semester
        {
            get { return ddSemester.SelectedItem.Text; }
            set { ddSemester.SelectedItem.Text = value; }
        }

        public string Department
        {
            get { return ddDepartment.SelectedItem.Text; }
            set { ddDepartment.SelectedItem.Text = value; }
        }

        public string CatalogNumber
        {
            get { return txtCatalogNumber.Text; }
            set { txtCatalogNumber.Text = value; }
        }

        public string SectionNumber
        {
            get { return txtSectionNumber.Text; }
            set { txtSectionNumber.Text = value; }
        }

        public string DateStart
        {
            get { return txtTermStartDate.Text; }
            set { txtTermStartDate.Text = value; }
        }

        public string DateEnd
        {
            get { return txtTermEndDate.Text; }
            set { txtTermEndDate.Text = value; }
        }

        public string Capacity
        {
            get { return txtCapacity.Text; }
            set { txtCapacity.Text = value; }
        }

        public int DepartmentsID
        {
            get { return int.Parse(ddDepartment.SelectedValue); }
            set { ddDepartment.SelectedValue = value.ToString(); }
        }

        private ClsChangeLog log = new ClsChangeLog("MasterSchedule");
        private int SectionID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            int.TryParse(Request["SectionsID"], out SectionID);
            if (SectionID <= 0)
                Response.Redirect("~/?Error=SectionDetails.ascx+had+no+SectionID");

            // FIX for hiding calendar when a date is selected
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "hideCalendar", "function hideCalendar(cb) { cb.hide(); }", true);

            if (!IsPostBack)
            {
                ClsSections section = new ClsSections("MasterSchedule");
                section.SectionsId = SectionID;
                section.GetRecord();

                ClsTerms term = new ClsTerms("MasterSchedule");
                term.TermsId = section.TermsId;
                term.GetRecord();

                ClsCourses course = new ClsCourses("MasterSchedule");
                course.CoursesId = section.CoursesId;
                course.GetRecord();

                ClsDepartments department = new ClsDepartments("MasterSchedule");
                department.DepartmentsId = course.DepartmentsId;
                department.GetRecord();

                ClsSemesters semester = new ClsSemesters("MasterSchedule");
                semester.SemestersId = term.SemestersId;
                semester.GetRecord();
            }
        }
    }
}