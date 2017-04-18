using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsChangeLogProcessing
{
    public ClsChangeLogProcessing(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _processingId;

    public int ProcessingId
    {
        get { return _processingId; }
        set { _processingId = value; }
    }

    private int _changeLogId;

    public int ChangeLogId
    {
        get { return _changeLogId; }
        set { _changeLogId = value; }
    }

    private string _processName;

    public string ProcessName
    {
        get { return _processName; }
        set { _processName = value; }
    }

    private string _actionTaken;

    public string ActionTaken
    {
        get { return _actionTaken; }
        set { _actionTaken = value; }
    }

    private string _processedBy;

    public string ProcessedBy
    {
        get { return _processedBy; }
        set { _processedBy = value; }
    }

    private DateTime _processedOn;

    public DateTime ProcessedOn
    {
        get { return _processedOn; }
        set { _processedOn = value; }
    }

    private string _processGroup;

    public string ProcessGroup
    {
        get { return _processGroup; }
        set { _processGroup = value; }
    }

    public void AddRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ChangeLogProcessing_Add";
        cmd.Parameters.Add("@ChangeLogId", SqlDbType.Int, 4).Value = _changeLogId;
        cmd.Parameters.Add("@ProcessName", SqlDbType.VarChar, 50).Value = _processName;
        cmd.Parameters.Add("@ActionTaken", SqlDbType.VarChar, 50).Value = _actionTaken;
        cmd.Parameters.Add("@ProcessedBy", SqlDbType.VarChar, 64).Value = System.DBNull.Value;
        cmd.Parameters.Add("@ProcessedOn", SqlDbType.DateTime, 8).Value = System.DBNull.Value;
        cmd.Parameters.Add("@ProcessGroup", SqlDbType.VarChar, 24).Value = _processGroup;
        cmd.Parameters.Add("@ProcessingID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _processingId = (int)cmd.Parameters["@ProcessingID"].Value;
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

    public void DeleteRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ChangeLogProcessing_Delete";
        cmd.Parameters.Add("@ProcessingID", SqlDbType.Int).Value = _processingId;
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
        cmd.CommandText = "ChangeLogProcessing_Fill";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "ChangeLogProcessing");
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
        cmd.CommandText = "ChangeLogProcessing_Get";
        cmd.Parameters.Add("@ProcessingID", SqlDbType.Int, 4).Value = _processingId;
        cmd.Parameters.Add("@ChangeLogId", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@ProcessName", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@ActionTaken", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@ProcessedBy", SqlDbType.VarChar, 64).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@ProcessedOn", SqlDbType.DateTime, 8).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@ProcessGroup", SqlDbType.VarChar, 24).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _changeLogId = (int)cmd.Parameters["@ChangeLogId"].Value;
            _processName = (string)cmd.Parameters["@ProcessName"].Value;
            _actionTaken = (string)cmd.Parameters["@ActionTaken"].Value;
            if (cmd.Parameters["@ProcessedBy"].Value != System.DBNull.Value)
            {
                _processedBy = (string)cmd.Parameters["@ProcessedBy"].Value;
            }
            if (cmd.Parameters["@ProcessedOn"].Value != System.DBNull.Value)
            {
                _processedOn = (DateTime)cmd.Parameters["@ProcessedOn"].Value;
            }
            if (cmd.Parameters["@ProcessGroup"].Value != System.DBNull.Value)
            {
                _processGroup = (string)cmd.Parameters["@ProcessGroup"].Value;
            }
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

    /// <summary>
    /// Set the ChangeLogId and the Process Name before using this method.
    /// </summary>
    public void GetRecord_ByChange_ByProcess()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "ChangeLogProcessing_Get_ByChange_ByProcess";
        cmd.Parameters.Add("@ProcessingID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@ChangeLogId", SqlDbType.Int, 4).Value = _changeLogId;
        cmd.Parameters.Add("@ProcessName", SqlDbType.VarChar, 50).Value = _processName;
        cmd.Parameters.Add("@ActionTaken", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@ProcessedBy", SqlDbType.VarChar, 64).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@ProcessedOn", SqlDbType.DateTime, 8).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@ProcessGroup", SqlDbType.VarChar, 24).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _processingId = (int)cmd.Parameters["@ProcessingID"].Value;
            _changeLogId = (int)cmd.Parameters["@ChangeLogId"].Value;
            _processName = (string)cmd.Parameters["@ProcessName"].Value;
            _actionTaken = (string)cmd.Parameters["@ActionTaken"].Value;
            if (cmd.Parameters["@ProcessedBy"].Value != System.DBNull.Value)
            {
                _processedBy = (string)cmd.Parameters["@ProcessedBy"].Value;
            }
            if (cmd.Parameters["@ProcessedOn"].Value != System.DBNull.Value)
            {
                _processedOn = (DateTime)cmd.Parameters["@ProcessedOn"].Value;
            }
            if (cmd.Parameters["@ProcessGroup"].Value != System.DBNull.Value)
            {
                _processGroup = (string)cmd.Parameters["@ProcessGroup"].Value;
            }
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
        cmd.CommandText = "ChangeLogProcessing_Update";
        cmd.Parameters.Add("@ProcessingID", SqlDbType.Int, 4).Value = _processingId;
        cmd.Parameters.Add("@ChangeLogId", SqlDbType.Int, 4).Value = _changeLogId;
        cmd.Parameters.Add("@ProcessName", SqlDbType.VarChar, 50).Value = _processName;
        cmd.Parameters.Add("@ActionTaken", SqlDbType.VarChar, 50).Value = _actionTaken;
        cmd.Parameters.Add("@ProcessedBy", SqlDbType.VarChar, 64).Value = _processedBy;
        cmd.Parameters.Add("@ProcessedOn", SqlDbType.DateTime, 8).Value = _processedOn;
        cmd.Parameters.Add("@ProcessGroup", SqlDbType.VarChar, 24).Value = _processGroup;
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
}