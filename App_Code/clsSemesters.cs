using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsSemesters
{
    public ClsSemesters(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _semestersId;

    public int SemestersId
    {
        get { return _semestersId; }
        set { _semestersId = value; }
    }

    private string _semester;

    public string Semester
    {
        get { return _semester; }
        set { _semester = value; }
    }

    private string _semesterDesc;

    public string SemesterDesc
    {
        get { return _semesterDesc; }
        set { _semesterDesc = value; }
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Semesters_Get";
        cmd.Parameters.Add("@SemestersID", SqlDbType.Int, 4).Value = _semestersId;
        cmd.Parameters.Add("@Semester", SqlDbType.Char, 2).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@SemesterDesc", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _semester = (string)cmd.Parameters["@Semester"].Value;
            _semesterDesc = (string)cmd.Parameters["@SemesterDesc"].Value;
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