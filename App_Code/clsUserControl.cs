using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

/// <summary>
/// Summary description for clsUserControl
/// </summary>
public class ClsUserControl : UserControl
{
    public ClsUserControl(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    public void Refresh()
    {
    }

    public bool UserInProcess(string username, string processName)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "UserInProcess";
        cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 64).Value = username;
        cmd.Parameters.Add("@ProcessName", SqlDbType.VarChar, 50).Value = processName;
        cmd.Parameters.Add("@IsInProcess", SqlDbType.Bit, 4).Direction = ParameterDirection.Output;

        bool inProcess = false;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            inProcess = (bool)cmd.Parameters["@IsInProcess"].Value;
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

        return inProcess;
    }
}