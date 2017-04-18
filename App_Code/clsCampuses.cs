using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsCampuses
{
    public ClsCampuses(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _campusesId;

    public int CampusesId
    {
        get { return _campusesId; }
        set { _campusesId = value; }
    }

    private string _campusCode;

    public string CampusCode
    {
        get { return _campusCode; }
        set { _campusCode = value; }
    }

    private string _campusName;

    public string CampusName
    {
        get { return _campusName; }
        set { _campusName = value; }
    }

    private string _campusDesc;

    public string CampusDesc
    {
        get { return _campusDesc; }
        set { _campusDesc = value; }
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Campuses_Get";
        cmd.Parameters.Add("@CampusesID", SqlDbType.Int, 4).Value = _campusesId;
        cmd.Parameters.Add("@CampusCode", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@CampusName", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@CampusDesc", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _campusCode = (string)cmd.Parameters["@CampusCode"].Value;
            if (cmd.Parameters["@CampusName"].Value != DBNull.Value)
                _campusName = (string)cmd.Parameters["@CampusName"].Value;
            if (cmd.Parameters["@CampusDesc"].Value != DBNull.Value)
                _campusDesc = (string)cmd.Parameters["@CampusDesc"].Value;
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