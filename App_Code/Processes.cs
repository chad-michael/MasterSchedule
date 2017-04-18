using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsProcesses
{
    public ClsProcesses(string connectionStringName)
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

    private int _processId = int.MinValue;

    public int ProcessId
    {
        get { return _processId; }
        set { _processId = value; }
    }

    private string _processName = string.Empty;

    public string ProcessName
    {
        get { return _processName; }
        set { _processName = value; }
    }

    private int _processGroup = int.MinValue;

    public int ProcessGroup
    {
        get { return _processGroup; }
        set { _processGroup = value; }
    }

    public void AddRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Processes_Add";
        cmd.Parameters.Add("@ProcessName", SqlDbType.VarChar, 50).Value = _processName;
        cmd.Parameters.Add("@ProcessGroup", SqlDbType.Int, 4).Value = _processGroup;
        cmd.Parameters.Add("@ProcessID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _processId = (int)cmd.Parameters["@ProcessID"].Value;
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
        cmd.CommandText = "Processes_Delete";
        cmd.Parameters.Add("@ProcessID", SqlDbType.Int).Value = _processId;
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
        cmd.CommandText = "Processes_Fill";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "Processes");
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

    public List<ClsProcesses> FillList()
    {
        DataSet ds = FillDs();
        return FillList(ds);
    }

    public List<ClsProcesses> FillList(DataSet ds)
    {
        List<ClsProcesses> list = new List<ClsProcesses>();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            ClsProcesses objTemp = new ClsProcesses(_connectionStringName);
            objTemp.ProcessId = (int)row["ProcessID"];
            if (row["ProcessName"] != System.DBNull.Value)
                objTemp.ProcessName = (string)row["ProcessName"];
            if (row["ProcessGroup"] != System.DBNull.Value)
                objTemp.ProcessGroup = (int)row["ProcessGroup"];
            list.Add(objTemp);
        }
        return list;
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Processes_Get";
        cmd.Parameters.Add("@ProcessID", SqlDbType.Int, 4).Value = _processId;
        cmd.Parameters.Add("@ProcessName", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@ProcessGroup", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            if (cmd.Parameters["@ProcessName"].Value != System.DBNull.Value)
            {
                _processName = (string)cmd.Parameters["@ProcessName"].Value;
            }
            if (cmd.Parameters["@ProcessGroup"].Value != System.DBNull.Value)
            {
                _processGroup = (int)cmd.Parameters["@ProcessGroup"].Value;
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
        cmd.CommandText = "Processes_Update";
        cmd.Parameters.Add("@ProcessID", SqlDbType.Int, 4).Value = _processId;
        cmd.Parameters.Add("@ProcessName", SqlDbType.VarChar, 50).Value = _processName;
        cmd.Parameters.Add("@ProcessGroup", SqlDbType.Int, 4).Value = _processGroup;
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