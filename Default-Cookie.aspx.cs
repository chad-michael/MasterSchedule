using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultWithCookie : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            pnlDivisionChair.Visible = false;
            if (Helpers.UserIsDivisionChair())
            {
                int NoChanges = Helpers.DivisionPendingChanges();
                if (NoChanges > 0)
                {
                    pnlDivisionChair.Visible = true;
                    lblNoChanges.Text = NoChanges.ToString();
                }
            }

            DataTable termsTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["MasterSchedule"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("List_Terms", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    termsTable.Load(cmd.ExecuteReader());
                }
            }

            ddlTerm.DataTextField = "Term";
            ddlTerm.DataValueField = "TermsID";
            ddlTerm.DataSource = termsTable;
            ddlTerm.DataBind();

            DataTable departmentsTable = new DataTable();
            using (SqlConnection connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["MasterSchedule"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("List_Departments", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    departmentsTable.Load(cmd.ExecuteReader());
                }
            }

            ddDepartment.DataTextField = "DepartmentDesc";
            ddDepartment.DataValueField = "DepartmentsID";
            ddDepartment.DataSource = departmentsTable;
            ddDepartment.DataBind();

            try
            {
                if (!InitalizeFromCookie())
                {
                    InitalizeCookie();
                }
            }
            catch (Exception)
            {
                //empty catch to suppress errors.  there might be somethign wrong with the cookie value,
                //or from selecting from the courses table.
            }
        }
    }

    /// <summary>
    /// Initalizes the cookie.
    /// </summary>
    private void InitalizeCookie()
    {
        HttpCookie cookie = new HttpCookie("SelectedCourse");
        cookie.Values.Add("TermId", "");
        cookie.Values.Add("DepartmentId", "");
        cookie.Values.Add("CourseId", "");
        cookie.Expires = DateTime.Now.AddDays(50);
        Response.Cookies.Add(cookie);
    }

    /// <summary>
    /// Initalizes from cookie.
    /// </summary>
    /// <returns></returns>
    private bool InitalizeFromCookie()
    {
        if (Request.Cookies["SelectedCourse"] != null)
        {
            string termId = Request.Cookies["SelectedCourse"].Values["TermId"];
            string departmentId = Request.Cookies["SelectedCourse"].Values["DepartmentId"];
            string courseId = Request.Cookies["SelectedCourse"].Values["CourseId"];

            ddlTerm.SelectedValue = termId;
            ddDepartment.SelectedValue = departmentId;
            ddCourse.SelectedValue = courseId;

            BindCoursess(int.Parse(departmentId));
            BindScheduleGrid(int.Parse(termId), int.Parse(departmentId), int.Parse(courseId));
            return true;
        }
        return false;
    }

    protected void ddDepartment_DataBound(object sender, EventArgs e)
    {
        ddDepartment.Items.Insert(0, new ListItem("All", "0"));
    }

    protected void ddCourse_DataBound(object sender, EventArgs e)
    {
        ddCourse.Items.Insert(0, new ListItem("All", "0"));
    }

    public string FormatMeetDays(object MeetDays)
    {
        return MeetDays.ToString().Replace(":", "");
    }

    protected void ddDepartment_Changed(object sender, EventArgs e)
    {
        BindCoursess(int.Parse(ddDepartment.SelectedValue));
    }

    protected void ddCourse_Changed(object sender, EventArgs e)
    {
        if (Response.Cookies != null)
        {
            Response.Cookies["SelectedCourse"].Values["TermId"] = ddlTerm.SelectedValue;
            Response.Cookies["SelectedCourse"].Values["DepartmentId"] = ddDepartment.SelectedValue;
            Response.Cookies["SelectedCourse"].Values["CourseId"] = ddCourse.SelectedValue;
        }

        int termId = int.Parse(ddlTerm.SelectedValue);
        int departmentId = int.Parse(ddDepartment.SelectedValue);
        int courseId = int.Parse(ddCourse.SelectedValue);

        BindScheduleGrid(termId, departmentId, courseId);
    }

    /// <summary>
    /// Binds the coursess.
    /// </summary>
    /// <param name="selectedDepartmentId">The selected department id.</param>
    private void BindCoursess(int selectedDepartmentId)
    {
        DataTable coursesTable = new DataTable();
        using (SqlConnection connection =
            new SqlConnection(WebConfigurationManager.ConnectionStrings["MasterSchedule"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("List_Course", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DepartmentsID", selectedDepartmentId));
                connection.Open();
                coursesTable.Load(cmd.ExecuteReader());
            }
        }

        ddCourse.DataTextField = "CourseNumber";
        ddCourse.DataValueField = "CoursesID";
        ddCourse.DataSource = coursesTable;
        ddCourse.DataBind();
    }

    /// <summary>
    /// Binds the schedule grid.
    /// </summary>
    /// <param name="termId">The term id.</param>
    /// <param name="departmentId">The department id.</param>
    /// <param name="courseId">The course id.</param>
    private void BindScheduleGrid(int termId, int departmentId, int courseId)
    {
        DataTable scheduleTable = new DataTable();
        using (SqlConnection connection =
            new SqlConnection(WebConfigurationManager.ConnectionStrings["MasterSchedule"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("MasterSchedule_SearchByTerm", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TermsId", termId));
                cmd.Parameters.Add(new SqlParameter("@DepartmentsID", departmentId));
                cmd.Parameters.Add(new SqlParameter("@CoursesID", courseId));
                connection.Open();
                scheduleTable.Load(cmd.ExecuteReader());
            }
        }

        gvSchedule.DataSource = scheduleTable;
        gvSchedule.DataBind();
    }
}