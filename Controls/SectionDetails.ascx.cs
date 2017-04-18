using AjaxControlToolkit;
using System;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Controls
{
    public partial class Controls_SectionDetails : System.Web.UI.UserControl
    {
        private ClsChangeLog log = new ClsChangeLog("MasterSchedule");
        private int SectionID = 0;
        private bool DetailsChanged = false;

        //expose values
        public string Year
        {
            get { return lblYear.Text; }
            set { lblYear.Text = value; }
        }

        public string Semester
        {
            get { return lblCourseSemester.Text; }
            set { lblCourseSemester.Text = value; }
        }

        public string Department
        {
            get { return lblCourseDepartment.Text; }
            set { lblCourseDepartment.Text = value; }
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

        private int _departmentsID;

        public int DepartmentsID
        {
            get { return _departmentsID; }
            set { _departmentsID = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int.TryParse(Request["SectionsID"], out SectionID);
            if (SectionID <= 0)
                Response.Redirect("~/?Error=SectionDetails.ascx+had+no+SectionID");

            // FIX for hiding calendar when a date is selected
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "hideCalendar", "function hideCalendar(cb) { cb.hide(); }", true);

            // FIX for css not loading when a calendar is inside of an updatepanel+multiview (+tabcontrol?)
            string url = Page.ClientScript.GetWebResourceUrl(typeof(ClientCssResourceAttribute), "AjaxControlToolkit.Calendar.Calendar.css");
            HtmlLink myHtmlLink = new HtmlLink();
            myHtmlLink.Href = url;
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            Page.Header.Controls.Add(myHtmlLink);

            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 1;

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
                // DepartmentsID = course.DepartmentsID;

                ClsSemesters semester = new ClsSemesters("MasterSchedule");
                semester.SemestersId = term.SemestersId;
                semester.GetRecord();

                lblCatalogNumber.Text = course.CourseNumber;
                lblDepartment.Text = department.DepartmentDesc;
                lblSectionNumber.Text = section.SectionNumber;
                lblSemester.Text = semester.SemesterDesc;
                //lblTermEndDate.Text = section.SectionEndDate.ToShortDateString();
                //lblTermStartDate.Text = section.SectionStartDate.ToShortDateString();
                lblTermYear.Text = term.TermYear.ToString();
                lblCapacity.Text = String.Format(section.Capacity.ToString(), "{0:0}");

                txtCatalogNumber.Text = course.CourseNumber;
                txtSectionNumber.Text = section.SectionNumber;
                txtTermEndDate.Text = section.SectionEndDate.ToShortDateString();
                txtTermStartDate.Text = section.SectionStartDate.ToShortDateString();
                txtCapacity.Text = String.Format(section.Capacity.ToString(), "{0:0}");

                //ddYear.DataBind();
                Year = term.TermYear.ToString();

                //ddSemester.DataBind();
                Semester = semester.SemesterDesc;

                //ddDepartment.DataBind();
                Department = department.DepartmentDesc;

                hdfDepartmentID.Value = course.DepartmentsId.ToString();
            }
        }

        public int GetDepartmentsID()
        {
            return Int32.Parse(hdfDepartmentID.Value);
        }

        protected void lnkEditCourseDetails_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        public StringBuilder SaveCourseDetails(int SectionID)
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

            StringBuilder sb = new StringBuilder();

            int oldDays = section.SectionEndDate.Subtract(section.SectionStartDate).Days + 1;
            int newDays = DateTime.Parse(txtTermEndDate.Text).Subtract(DateTime.Parse(txtTermStartDate.Text)).Days + 1;
            string beforeDay = "";
            string afterDay = "";

            if (oldDays != newDays)
            {
                beforeDay = "<strong>";
                afterDay = "</strong>";
            }

            sb.Append("<table><tr>");
            sb.Append("<td><strong>Change Course:</strong></td>");
            sb.Append("<td><strong>From:</strong></td>");
            sb.Append("<td><strong>To:</strong></td>");
            sb.Append("</tr><tr>");
            sb.Append("<td></td>");
            sb.Append("<td>");

            sb.Append("<table>");
            sb.Append("<tr><td>Year:</td><td><strong>" + Year + "</strong></td></tr>");
            sb.Append("<tr><td>Semester:</td><td><strong>" + Semester + "</strong></td></tr>");
            sb.Append("<tr><td>Department:</td><td><strong>" + Department + "</strong></td></tr>");
            sb.Append("<tr><td>Catalog Number:</td><td><strong>" + MarkOrig(course.CourseNumber, txtCatalogNumber.Text) + "</strong></td></tr>");
            sb.Append("<tr><td>Section Number:</td><td><strong>" + MarkOrig(section.SectionNumber, txtSectionNumber.Text) + "</strong></td></tr>");
            sb.Append("<tr><td>Semester Start:</td><td><strong>" + MarkOrig_DateType(section.SectionStartDate, txtTermStartDate.Text) + "</strong></td></tr>");
            sb.Append("<tr><td>Semester End:</td><td><strong>" + MarkOrig_DateType(section.SectionEndDate, txtTermEndDate.Text) + "</strong></td></tr>");
            sb.Append("<tr><td>Capacity:</td><td><strong>" + MarkOrig(section.Capacity.ToString(), (txtCapacity.Text)) + "</strong></td></tr>");
            if (GetCourseSections.GetCreditType(term.Term.Replace("-", "/"), section.SectionNumber, course.CourseNumber) != "2")
            {
                sb.Append("<tr><td>Refund Policy:</td><td><strong>" + MarkOrig_RefundPeriod(DateTime.Parse(txtTermStartDate.Text), section.SectionStartDate, DateTime.Parse(txtTermEndDate.Text), section.SectionEndDate) + "</strong></td></tr>");
                sb.Append("<tr><td>Days:</td><td>" + beforeDay + oldDays.ToString() + afterDay + "</td></tr>");
            }
            //Days
            sb.Append("</table>");

            sb.Append("</td>");
            sb.Append("<td>");

            sb.Append("<table>");
            sb.Append("<tr><td>Year:</td><td><strong>" + Year + "</strong></td></tr>");
            sb.Append("<tr><td>Semester:</td><td><strong>" + Semester + "</strong></td></tr>");
            sb.Append("<tr><td>Department:</td><td><strong>" + Department + "</strong></td></tr>");
            sb.Append("<tr><td>Catalog Number:</td><td><strong>" + MarkChanges(course.CourseNumber, txtCatalogNumber.Text) + "</strong></td></tr>");
            sb.Append("<tr><td>Section Number:</td><td><strong>" + MarkChanges(section.SectionNumber, txtSectionNumber.Text) + "</strong></td></tr>");
            sb.Append("<tr><td>Semester Start:</td><td><strong>" + MarkChanges_DateType(section.SectionStartDate, txtTermStartDate.Text) + "</strong></td></tr>");
            sb.Append("<tr><td>Semester End:</td><td><strong>" + MarkChanges_DateType(section.SectionEndDate, txtTermEndDate.Text) + "</strong></td></tr>");
            sb.Append("<tr><td>Capacity:</td><td><strong>" + MarkChanges(section.Capacity.ToString(), (txtCapacity.Text)) + "</strong></td></tr>");
            if (GetCourseSections.GetCreditType(term.Term.Replace("-", "/"), section.SectionNumber, course.CourseNumber) != "2")
            {
                sb.Append("<tr><td>Refund Policy:</td><td><strong>" + MarkChange_RefundPeriod(DateTime.Parse(txtTermStartDate.Text), section.SectionStartDate, DateTime.Parse(txtTermEndDate.Text), section.SectionEndDate) + "</strong></td></tr>");
                sb.Append("<tr><td>Days:</td><td>" + beforeDay + newDays.ToString() + afterDay + "</td></tr>");
            }
            sb.Append("</table>");

            sb.Append("</td>");
            sb.Append("</tr></table>");
            sb.Append("<table><tr><td>");
            sb.Append(txtShortNote.Text);
            sb.Append("</td></tr></table>");

            return sb;

            //log.SectionsID = int.Parse(Request["SectionsID"]);
            //log.SubmittedBy = Session["deltaid"].ToString();

            //log.Change = sb.ToString();
            //log.AddRecord();

            //Helpers.RefreshSortPending((GridView)Helpers.FindControlRecursive(Page.Master, "gvPendingChanges")); //Refresh the pending list
            // MultiView1.ActiveViewIndex = 0;
        }

        protected string MarkOrig(string orig, string newvalue)
        {
            if (orig == newvalue)
            {
                return ">" + orig;
            }
            else
            {
                return "class='change'><strong>" + orig + "</strong>";
                DetailsChanged = true;
            }
        }

        protected string MarkOrig_RefundPeriod(DateTime newStartDate, DateTime oldStartDate, DateTime newEndDate, DateTime oldEndDate)
        {
            string refundPeriodOld = RefundPeriodLookup(oldStartDate, oldEndDate);
            string refundPeriodNew = RefundPeriodLookup(newStartDate, newEndDate);

            if (refundPeriodOld == refundPeriodNew)
            {
                return ">" + refundPeriodNew;
            }
            else
            {
                DetailsChanged = true;
                return "class='change'><strong>" + refundPeriodOld + "</strong>";
            }
        }

        protected string RefundPeriodLookup(DateTime startDate, DateTime endDate)
        {
            string result = "";
            int dateDiff = endDate.Subtract(startDate).Days + 1;

            if (dateDiff > 28)
            {
                //Full Refund Default
                result = "1 - Full Refund";
            }
            else if (dateDiff < 28 && dateDiff > 2)
            {
                //Short-Term Refund
                result = "2 - Short-Term Refund";
            }
            else
            {
                //No refund
                result = "3 - No Refund as of Start Date";
            }

            return result;
        }

        protected string MarkChanges(string orig, string newvalue)
        {
            if (orig == newvalue)
            {
                return ">" + newvalue;
            }
            else
            {
                return "class='change'><strong>" + newvalue + "</strong>";
                DetailsChanged = true;
            }
        }

        protected string MarkChanges_DateType(DateTime orig, string newvalue)
        {
            DateTime newDate = DateTime.Parse(newvalue);
            if (orig == newDate)
            {
                return ">" + newDate.ToShortDateString();
            }
            else
            {
                return "class='change'><strong>" + newDate.ToShortDateString() + "</strong>";
                DetailsChanged = true;
            }
        }

        protected string MarkChange_RefundPeriod(DateTime newStartDate, DateTime oldStartDate, DateTime newEndDate, DateTime oldEndDate)
        {
            string refundPeriodOld = RefundPeriodLookup(oldStartDate, oldEndDate);
            string refundPeriodNew = RefundPeriodLookup(newStartDate, newEndDate);

            if (refundPeriodOld == refundPeriodNew)
            {
                return ">" + refundPeriodNew;
            }
            else
            {
                return "class='change'><strong>" + refundPeriodNew + "</strong>";
                DetailsChanged = true;
            }
        }

        protected string MarkOrig_DateType(DateTime orig, string newvalue)
        {
            DateTime newDate = DateTime.Parse(newvalue);
            if (orig == newDate)
            {
                return ">" + orig.ToShortDateString();
            }
            else
            {
                return "class='change'><strong>" + orig.ToShortDateString() + "</strong>";
                DetailsChanged = true;
            }
        }

        protected void btnDeleteCourse_Click(object sender, EventArgs e)
        {
            ClsSections section = new ClsSections("MasterSchedule");
            section.SectionsId = int.Parse(Request["SectionsID"]);
            section.GetRecord();

            ClsTerms term = new ClsTerms("MasterSchedule");
            term.TermsId = section.TermsId;
            term.GetRecord();

            ClsCourses course = new ClsCourses("MasterSchedule");
            course.CoursesId = section.CoursesId;
            course.GetRecord();

            log.SectionsId = int.Parse(Request["SectionsID"]);
            log.SubmittedBy = Session["deltaid"].ToString();
            log.Change = "Drop Course: " + term.Term + " " + course.CourseNumber + " sec " + section.SectionNumber;
            log.AddRecord();

            Helpers.RefreshSortPending((GridView)Helpers.FindControlRecursive(Page.Master, "gvPendingChanges")); //Refresh the pending list
        }
    }
}