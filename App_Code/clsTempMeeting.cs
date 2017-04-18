using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public class ClsTempMeeting
{
    public ClsTempMeeting(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _meetingId;

    public int MeetingId
    {
        get { return _meetingId; }
        set { _meetingId = value; }
    }

    private string _campus;

    public string Campus
    {
        get { return _campus; }
        set { _campus = value; }
    }

    private string _building;

    public string Building
    {
        get { return _building; }
        set { _building = value; }
    }

    private string _room;

    public string Room
    {
        get { return _room; }
        set { _room = value; }
    }

    private string _startTime;

    public string StartTime
    {
        get { return _startTime; }
        set { _startTime = value; }
    }

    private string _endTime;

    public string EndTime
    {
        get { return _endTime; }
        set { _endTime = value; }
    }

    private string _days;

    public string Days
    {
        get { return _days; }
        set { _days = value; }
    }

    private Guid _sectionId;

    public Guid SectionId
    {
        get { return _sectionId; }
        set { _sectionId = value; }
    }

    private string _meetType;

    public string MeetType
    {
        get { return _meetType; }
        set { _meetType = value; }
    }

    public void AddRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "TempMeeting_Add";
        cmd.Parameters.Add("@Campus", SqlDbType.VarChar, 50).Value = _campus;
        cmd.Parameters.Add("@Room", SqlDbType.VarChar, 50).Value = _room;
        cmd.Parameters.Add("@StartTime", SqlDbType.VarChar, 50).Value = _startTime;
        cmd.Parameters.Add("@EndTime", SqlDbType.VarChar, 50).Value = _endTime;
        cmd.Parameters.Add("@Days", SqlDbType.VarChar, 7).Value = _days;
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Value = _sectionId;
        cmd.Parameters.Add("@MeetingID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@MeetType", SqlDbType.VarChar, 25).Value = _meetType;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _meetingId = (int)cmd.Parameters["@MeetingID"].Value;
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
        cmd.CommandText = "TempMeeting_Delete";
        cmd.Parameters.Add("@MeetingID", SqlDbType.Int).Value = _meetingId;
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
        cmd.CommandText = "TempMeeting_Fill";
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Value = _sectionId;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "TempMeeting");
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
        cmd.CommandText = "TempMeeting_Get";
        cmd.Parameters.Add("@MeetingID", SqlDbType.Int, 4).Value = _meetingId;
        cmd.Parameters.Add("@Campus", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Room", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@StartTime", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@EndTime", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Days", SqlDbType.VarChar, 7).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@MeetType", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _campus = (string)cmd.Parameters["@Campus"].Value;
            _room = (string)cmd.Parameters["@Room"].Value;
            _startTime = (string)cmd.Parameters["@StartTime"].Value;
            _endTime = (string)cmd.Parameters["@EndTime"].Value;
            _days = (string)cmd.Parameters["@Days"].Value;
            _sectionId = (Guid)cmd.Parameters["@SectionID"].Value;
            _meetType = (string)cmd.Parameters["@MeetType"].Value;
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
        cmd.CommandText = "TempMeeting_Update";
        cmd.Parameters.Add("@MeetingID", SqlDbType.Int, 4).Value = _meetingId;
        cmd.Parameters.Add("@Campus", SqlDbType.VarChar, 50).Value = _campus;
        cmd.Parameters.Add("@Room", SqlDbType.VarChar, 50).Value = _room;
        cmd.Parameters.Add("@StartTime", SqlDbType.VarChar, 50).Value = _startTime;
        cmd.Parameters.Add("@EndTime", SqlDbType.VarChar, 50).Value = _endTime;
        cmd.Parameters.Add("@Days", SqlDbType.VarChar, 7).Value = _days;
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Value = _sectionId;
        cmd.Parameters.Add("@MeetType", SqlDbType.VarChar, 25).Value = _meetType;
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