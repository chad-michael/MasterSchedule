using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsProcessGroups
{
    public ClsProcessGroups(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    #region 'SqlError' event definition code

    /// <summary>
    ///     This represents the delegate method prototype that
    ///     event receivers must implement
    /// </summary>
    public delegate void SqlErrorEventHandler(object sender, System.EventArgs e);

    /// <summary>
    ///     TODO: Describe the purpose of SqlError here
    /// </summary>
    public event SqlErrorEventHandler SqlError;

    /// <summary>
    ///     This is the method that is responsible for notifying
    ///     receivers that the event occurred
    /// </summary>
    protected virtual void OnSqlError(System.EventArgs e)
    {
        if (SqlError != null)
        {
            SqlError(this, e);
        }
    }

    #endregion 'SqlError' event definition code

    private SqlConnection _conn;
    private string _connectionStringName = string.Empty;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _processGroupId = int.MinValue;

    public int ProcessGroupId
    {
        get { return _processGroupId; }
        set { _processGroupId = value; }
    }

    private string _groupName = string.Empty;

    public string GroupName
    {
        get { return _groupName; }
        set { _groupName = value; }
    }

    public void AddRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ProcessGroups_Add";
        cmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50).Value = _groupName;
        cmd.Parameters.Add("@ProcessGroupId", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _processGroupId = (int)cmd.Parameters["@ProcessGroupId"].Value;
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message; OnSqlError(new EventArgs());
        }
        finally
        {
            if (_conn.State.Equals(ConnectionState.Open)) { _conn.Close(); }
        }
        cmd.Dispose();
    }

    public void DeleteRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ProcessGroups_Delete";
        cmd.Parameters.Add("@ProcessGroupId", SqlDbType.Int).Value = _processGroupId;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message; OnSqlError(new EventArgs());
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
        cmd.CommandText = "ProcessGroups_Fill";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "ProcessGroups");
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

    public List<ClsProcessGroups> FillList()
    {
        DataSet ds = FillDs();
        return FillList(ds);
    }

    public List<ClsProcessGroups> FillList(DataSet ds)
    {
        List<ClsProcessGroups> list = new List<ClsProcessGroups>();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            ClsProcessGroups objTemp = new ClsProcessGroups(_connectionStringName);
            objTemp.ProcessGroupId = (int)row["ProcessGroupId"];
            if (row["GroupName"] != System.DBNull.Value)
                objTemp.GroupName = (string)row["GroupName"];
            list.Add(objTemp);
        }
        return list;
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ProcessGroups_Get";
        cmd.Parameters.Add("@ProcessGroupId", SqlDbType.Int, 4).Value = _processGroupId;
        cmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            if (cmd.Parameters["@GroupName"].Value != System.DBNull.Value)
            {
                _groupName = (string)cmd.Parameters["@GroupName"].Value;
            }
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message; OnSqlError(new EventArgs());
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
        cmd.CommandText = "ProcessGroups_Update";
        cmd.Parameters.Add("@ProcessGroupId", SqlDbType.Int, 4).Value = _processGroupId;
        cmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50).Value = _groupName;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message; OnSqlError(new EventArgs());
        }
        finally
        {
            if (_conn.State.Equals(ConnectionState.Open)) { _conn.Close(); }
        }
        cmd.Dispose();
    }
}