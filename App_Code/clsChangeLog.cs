using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

public delegate void RecordAddedEventHandler(object sender, RecordAddEventArgs e);

public delegate void RecordAddingEventHandler(object sender, RecordAddEventArgs e);

[Serializable]
public class RecordAddEventArgs : System.EventArgs
{
    /// <summary>
    ///     Use this constructor to initialize the event arguments
    ///     object with the custom event fields
    /// </summary>
    public RecordAddEventArgs(ClsChangeLog changeLog)
    {
        _changeLog = changeLog;
    }

    private ClsChangeLog _changeLog;

    public ClsChangeLog ChangeLog
    {
        get { return _changeLog; }
        set { _changeLog = value; }
    }
}

public class ClsChangeLog
{
    public ClsChangeLog(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    public event RecordAddedEventHandler RecordAdded;

    public event RecordAddingEventHandler RecordAdding;

    protected virtual void OnRecordAdded(RecordAddEventArgs e)
    {
        if (RecordAdded != null)
        {
            RecordAdded(this, e);
        }
    }

    protected virtual void OnRecordAdding(RecordAddEventArgs e)
    {
        if (RecordAdding != null)
        {
            RecordAdding(this, e);
        }
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _logId;

    public int LogId
    {
        get { return _logId; }
        set { _logId = value; }
    }

    private string _submittedBy;

    public string SubmittedBy
    {
        get { return _submittedBy; }
        set { _submittedBy = value; }
    }

    private DateTime _dateSubmitted;

    public DateTime DateSubmitted
    {
        get { return _dateSubmitted; }
        set { _dateSubmitted = value; }
    }

    private int _sectionsId = 0;

    public int SectionsId
    {
        get { return _sectionsId; }
        set { _sectionsId = value; }
    }

    private int _departmentsId = 0;

    public int DepartmentsId
    {
        get { return _departmentsId; }
        set { _departmentsId = value; }
    }

    private string _status;

    public string Status
    {
        get { return _status; }
        set { _status = value; }
    }

    private string _change;

    public string Change
    {
        get { return _change; }
        set { _change = value; }
    }

    private string _updatedBy;

    public string UpdatedBy
    {
        get { return _updatedBy; }
        set { _updatedBy = value; }
    }

    private DateTime _dateUpdated;

    public DateTime DateUpdated
    {
        get { return _dateUpdated; }
        set { _dateUpdated = value; }
    }

    private string _processGroup;

    public string ProcessGroup
    {
        get { return _processGroup; }
        set { _processGroup = value; }
    }

    private void AutoApprove()
    {
        this.GetRecord();

        this._status = "approved";
        this._updatedBy = HttpContext.Current.User.Identity.Name;
        this.ProcessGroup = "registars";
        this._dateUpdated = DateTime.Now;
        this.UpdateRecord();
    }

    public void AddRecord()
    {
        if (_sectionsId > 0)
        {
            ClsSections s = new ClsSections("MasterSchedule");
            s.SectionsId = _sectionsId;
            s.GetRecord();
            ClsCourses c = new ClsCourses("MasterSchedule");
            c.CoursesId = s.CoursesId;
            c.GetRecord();
            _departmentsId = c.DepartmentsId;
        }
        _processGroup = "divisionchairs";

        bool divchair = Helpers.UserIsDivisionChair();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ChangeLog_Add";
        cmd.Parameters.Add("@SubmittedBy", SqlDbType.VarChar, 64).Value = _submittedBy;
        cmd.Parameters.Add("@SectionsID", SqlDbType.Int, 4).Value = _sectionsId;
        cmd.Parameters.Add("@Change", SqlDbType.VarChar, -1).Value = _change;
        cmd.Parameters.Add("@DepartmentsID", SqlDbType.Int, 4).Value = _departmentsId;
        cmd.Parameters.Add("@ProcessGroup", SqlDbType.VarChar, 24).Value = _processGroup;
        cmd.Parameters.Add("@LogID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _logId = (int)cmd.Parameters["@LogID"].Value;
            System.Diagnostics.Debug.WriteLine("the change log has been comitted to the database");

            this._dateSubmitted = DateTime.Now;
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message;
        }
        finally
        {
            if (_conn.State.Equals(ConnectionState.Open)) { _conn.Close(); }
        }
        cmd.Dispose();
        bool usePreview = false;
        if (HttpContext.Current.Session["usepreview"] != null)
        {
            usePreview = (bool)HttpContext.Current.Session["usepreview"];
        }
        //reset the use preview session option.
        HttpContext.Current.Session["usepreview"] = false;
        if (!divchair || usePreview)
        {
            //Don't need to send email to the division chair.
            if (!divchair)
            {
                /* Commented out for testing..  Cyrus */
                Helpers.SendEmail("Master Schedule Form Submitted", GetDescriptiveEmail() + this._change,
                    Helpers.GetDivisionEmail(this._sectionsId));
            }
        }
        else
        {
            AutoApprove();
            ClsChangeLogProcessing nextProcess = new ClsChangeLogProcessing("MasterSchedule");
            nextProcess.ProcessName = "registars";
            nextProcess.ChangeLogId = _logId;
            nextProcess.ActionTaken = "pending";
            nextProcess.ProcessGroup = "registars";
            nextProcess.AddRecord();
            Helpers.SendEmail("Schedule change submitted", GetDescriptiveEmail() + this._change, "regis@delta.edu;webadmin@delta.edu");
        }
    }

    private string GetDescriptiveEmail()
    {
        if (_sectionsId > 0)
        {
            ClsSections section = new ClsSections("MasterSchedule");
            section.SectionsId = this._sectionsId;
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
            sb.Append("<p style='font-weight:bold;'>");
            sb.Append("Change Submitted By: " + this._submittedBy);
            sb.Append("Change Submitted On: " + this._dateSubmitted.ToString());
            sb.Append("</p>");
            sb.Append("<br /><br />Orginal Section Detail");
            sb.Append("<table>");
            AppendRow("Department", department.DepartmentCode, sb);
            AppendRow("Course Number", course.CourseNumber, sb);
            AppendRow("Section", section.SectionNumber, sb);
            AppendRow("Section Start", section.SectionStartDate.ToShortDateString(), sb);
            AppendRow("Section End", section.SectionEndDate.ToShortDateString(), sb);
            sb.Append("</table>");
            return sb.ToString();
        }
        else return "";
    }

    private void AppendRow(string label, string value, StringBuilder sb)
    {
        sb.Append(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", label, value));
    }

    public void DeleteRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ChangeLog_Delete";
        cmd.Parameters.Add("@LogID", SqlDbType.Int).Value = _logId;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message;
        }
        finally
        {
            if (_conn.State.Equals(ConnectionState.Open)) { _conn.Close(); }
        }
        cmd.Dispose();
    }

    public DataSet FillDs()
    {
        DataSet ds = new DataSet();
        return FillDs(ds);
    }

    public DataSet FillDs(DataSet ds)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ChangeLog_Fill";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "ChangeLog");
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message;
            ds = null;
        }
        finally
        {
            if (_conn.State.Equals(ConnectionState.Open)) { _conn.Close(); }
        }
        cmd.Dispose();
        da.Dispose();
        return ds;
    }

    public DataSet FillDs_ByProcess(DataSet ds, string deltaid, string processName, string status)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ChangeLog_Fill_ChairByProcess";
        cmd.Parameters.Add(new SqlParameter("@DeltaID", deltaid));
        cmd.Parameters.Add(new SqlParameter("@ProcessName", processName));
        cmd.Parameters.Add(new SqlParameter("@Status", status));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "ChangeLog_Fill_ChairByProcess");
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message;
            ds = null;
        }
        finally
        {
            if (_conn.State.Equals(ConnectionState.Open)) { _conn.Close(); }
        }
        cmd.Dispose();
        da.Dispose();
        return ds;
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ChangeLog_Get";
        cmd.Parameters.Add("@LogID", SqlDbType.Int, 4).Value = _logId;
        cmd.Parameters.Add("@SubmittedBy", SqlDbType.VarChar, 64).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@DateSubmitted", SqlDbType.DateTime, 8).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@SectionsID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 16).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Change", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 64).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@DateUpdated", SqlDbType.DateTime, 8).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@DepartmentsID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@ProcessGroup", SqlDbType.VarChar, 24).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            if (cmd.Parameters["@SubmittedBy"].Value != System.DBNull.Value)
            {
                _submittedBy = (string)cmd.Parameters["@SubmittedBy"].Value;
            }
            _dateSubmitted = (DateTime)cmd.Parameters["@DateSubmitted"].Value;
            _sectionsId = (int)cmd.Parameters["@SectionsID"].Value;
            _status = (string)cmd.Parameters["@Status"].Value;
            _change = (string)cmd.Parameters["@Change"].Value;
            _departmentsId = (int)cmd.Parameters["@DepartmentsID"].Value;
            _processGroup = (string)cmd.Parameters["@ProcessGroup"].Value;

            if (cmd.Parameters["@UpdatedBy"].Value != DBNull.Value)
                _updatedBy = (string)cmd.Parameters["@UpdatedBy"].Value;
            else
                _updatedBy = "";

            if (cmd.Parameters["@DateUpdated"].Value != DBNull.Value)
                _dateUpdated = (DateTime)cmd.Parameters["@DateUpdated"].Value;
            else
                _dateUpdated = DateTime.MinValue;
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message;
        }
        finally
        {
            if (_conn.State.Equals(ConnectionState.Open)) { _conn.Close(); }
        }
        cmd.Dispose();
    }

    public void UpdateRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ChangeLog_Update";
        cmd.Parameters.Add("@LogID", SqlDbType.Int, 4).Value = _logId;
        cmd.Parameters.Add("@SubmittedBy", SqlDbType.VarChar, 64).Value = _submittedBy;
        cmd.Parameters.Add("@DateSubmitted", SqlDbType.DateTime, 8).Value = _dateSubmitted;
        cmd.Parameters.Add("@SectionsID", SqlDbType.Int, 4).Value = _sectionsId;
        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 16).Value = _status;
        cmd.Parameters.Add("@Change", SqlDbType.VarChar, -1).Value = _change;
        cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 64).Value = _updatedBy;
        cmd.Parameters.Add("@DateUpdated", SqlDbType.DateTime, 8).Value = _dateUpdated;
        cmd.Parameters.Add("@DepartmentsID", SqlDbType.Int, 4).Value = _departmentsId;
        cmd.Parameters.Add("@ProcessGroup", SqlDbType.VarChar, 24).Value = _processGroup;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message;
            throw ex;
        }
        finally
        {
            if (_conn.State.Equals(ConnectionState.Open)) { _conn.Close(); }
        }
        cmd.Dispose();
    }
}