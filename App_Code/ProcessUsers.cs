using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsProcessUsers
{
    public ClsProcessUsers(string connectionStringName)
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

    private int _id = int.MinValue;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    private int _processId = int.MinValue;

    public int ProcessId
    {
        get { return _processId; }
        set { _processId = value; }
    }

    private string _userId = string.Empty;

    public string UserId
    {
        get { return _userId; }
        set { _userId = value; }
    }

    public void AddRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ProcessUsers_Add";
        cmd.Parameters.Add("@ProcessId", SqlDbType.Int, 4).Value = _processId;
        cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 64).Value = _userId;
        cmd.Parameters.Add("@ID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _id = (int)cmd.Parameters["@ID"].Value;
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
        cmd.CommandText = "ProcessUsers_Delete";
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _id;
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
        cmd.CommandText = "ProcessUsers_Fill";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "ProcessUsers");
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

    public DataSet FillDsJoined()
    {
        DataSet ds = new DataSet();
        return FillDsJoined(ds);
    }

    public DataSet FillDsJoined(DataSet ds)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ProcessUsers_FillJoined";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "ProcessUsers");
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message;
            OnSqlError(new EventArgs());
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

    public List<ClsProcessUsers> FillList()
    {
        DataSet ds = FillDs();
        return FillList(ds);
    }

    public List<ClsProcessUsers> FillList(DataSet ds)
    {
        List<ClsProcessUsers> list = new List<ClsProcessUsers>();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            ClsProcessUsers objTemp = new ClsProcessUsers(_connectionStringName);
            objTemp.Id = (int)row["ID"];
            objTemp.ProcessId = (int)row["ProcessId"];
            if (row["UserId"] != System.DBNull.Value)
                objTemp.UserId = (string)row["UserId"];
            list.Add(objTemp);
        }
        return list;
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ProcessUsers_Get";
        cmd.Parameters.Add("@ID", SqlDbType.Int, 4).Value = _id;
        cmd.Parameters.Add("@ProcessId", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 64).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _processId = (int)cmd.Parameters["@ProcessId"].Value;
            if (cmd.Parameters["@UserId"].Value != System.DBNull.Value)
            {
                _userId = (string)cmd.Parameters["@UserId"].Value;
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
        cmd.CommandText = "ProcessUsers_Update";
        cmd.Parameters.Add("@ID", SqlDbType.Int, 4).Value = _id;
        cmd.Parameters.Add("@ProcessId", SqlDbType.Int, 4).Value = _processId;
        cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 64).Value = _userId;
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