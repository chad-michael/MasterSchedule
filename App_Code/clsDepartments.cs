using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsDepartments
{
    public ClsDepartments(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _departmentsId;

    public int DepartmentsId
    {
        get { return _departmentsId; }
        set { _departmentsId = value; }
    }

    private string _departmentCode;

    public string DepartmentCode
    {
        get { return _departmentCode; }
        set { _departmentCode = value; }
    }

    private string _departmentDesc;

    public string DepartmentDesc
    {
        get { return _departmentDesc + " ( " + _departmentCode + " ) "; }
        set { _departmentDesc = value; }
    }

    private int _divisionsId;

    public int DivisionsId
    {
        get { return _divisionsId; }
        set { _divisionsId = value; }
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Departments_Get";
        cmd.Parameters.Add("@DepartmentsID", SqlDbType.Int, 4).Value = _departmentsId;
        cmd.Parameters.Add("@DepartmentCode", SqlDbType.Char, 10).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@DepartmentDesc", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@DivisionsID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _departmentCode = (string)cmd.Parameters["@DepartmentCode"].Value;
            _departmentDesc = (string)cmd.Parameters["@DepartmentDesc"].Value;
            _divisionsId = (int)cmd.Parameters["@DivisionsID"].Value;
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