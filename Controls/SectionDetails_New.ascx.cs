using System;

namespace Controls
{
    public partial class Controls_SectionDetails_New : System.Web.UI.UserControl
    {
        //expose values
        public string Year
        {
            get { return ddYear.SelectedItem.Text; }
        }

        public string Semester
        {
            get { return ddSemester.SelectedItem.Text; }
        }

        public string Department
        {
            get { return ddDepartment.SelectedItem.Text; }
        }

        public string CatalogNumber
        {
            get { return txtCatalogNumber.Text; }
        }

        public string SectionNumber
        {
            get { return txtSectionNumber.Text; }
        }

        public string DateStart
        {
            get { return txtTermStartDate.Text; }
        }

        public string DateEnd
        {
            get { return txtTermEndDate.Text; }
        }

        public string Capacity
        {
            get { return txtCapacity.Text; }
        }

        public int DepartmentsID
        {
            get { return int.Parse(ddDepartment.SelectedValue); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // FIX for hiding calendar when a date is selected
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "hideCalendar", "function hideCalendar(cb) { cb.hide(); }", true);
        }
    }
}