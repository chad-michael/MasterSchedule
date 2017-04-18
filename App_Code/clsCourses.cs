using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsCourses
{
    public ClsCourses(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _coursesId;

    public int CoursesId
    {
        get { return _coursesId; }
        set { _coursesId = value; }
    }

    private string _courseNumber;

    public string CourseNumber
    {
        get { return _courseNumber; }
        set { _courseNumber = value; }
    }

    private string _courseTitle;

    public string CourseTitle
    {
        get { return _courseTitle; }
        set { _courseTitle = value; }
    }

    private string _courseDesc;

    public string CourseDesc
    {
        get { return _courseDesc; }
        set { _courseDesc = value; }
    }

    private int _departmentsId;

    public int DepartmentsId
    {
        get { return _departmentsId; }
        set { _departmentsId = value; }
    }

    private decimal _credits;

    public decimal Credits
    {
        get { return _credits; }
        set { _credits = value; }
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Courses_Get";
        cmd.Parameters.Add("@CoursesID", SqlDbType.Int, 4).Value = _coursesId;
        cmd.Parameters.Add("@CourseNumber", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@CourseTitle", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@CourseDesc", SqlDbType.VarChar, 8000).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@DepartmentsID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Credits", SqlDbType.Decimal, 9).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _courseNumber = (string)cmd.Parameters["@CourseNumber"].Value;
            _courseTitle = (string)cmd.Parameters["@CourseTitle"].Value;

            if (cmd.Parameters["@CourseDesc"].Value != DBNull.Value)
                _courseDesc = (string)cmd.Parameters["@CourseDesc"].Value;

            _departmentsId = (int)cmd.Parameters["@DepartmentsID"].Value;
            _credits = (decimal)cmd.Parameters["@Credits"].Value;
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
}