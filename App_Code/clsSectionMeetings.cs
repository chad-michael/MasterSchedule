using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsSectionMeetings
{
    public ClsSectionMeetings(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _sectionMeetingsId = 0;

    public int SectionMeetingsId
    {
        get { return _sectionMeetingsId; }
        set { _sectionMeetingsId = value; }
    }

    private int _sectionsId = 0;

    public int SectionsId
    {
        get { return _sectionsId; }
        set { _sectionsId = value; }
    }

    private int _sectionMeetingsIndex = 1;

    public int SectionMeetingsIndex
    {
        get { return _sectionMeetingsIndex; }
        set { _sectionMeetingsIndex = value; }
    }

    private int _roomsId = 0;

    public int RoomsId
    {
        get { return _roomsId; }
        set { _roomsId = value; }
    }

    private string _meetDays = "";

    public string MeetDays
    {
        get { return _meetDays; }
        set { _meetDays = value; }
    }

    private DateTime _meetStartTime = DateTime.MinValue;

    public DateTime MeetStartTime
    {
        get { return _meetStartTime; }
        set { _meetStartTime = value; }
    }

    private DateTime _meetEndTime = DateTime.MinValue;

    public DateTime MeetEndTime
    {
        get { return _meetEndTime; }
        set { _meetEndTime = value; }
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SectionMeetings_Get";
        cmd.Parameters.Add("@SectionMeetingsID", SqlDbType.Int, 4).Value = _sectionMeetingsId;
        cmd.Parameters.Add("@SectionsID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@SectionMeetingsIndex", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@RoomsID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@MeetDays", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@MeetStartTime", SqlDbType.SmallDateTime, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@MeetEndTime", SqlDbType.SmallDateTime, 4).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _sectionsId = (int)cmd.Parameters["@SectionsID"].Value;
            _sectionMeetingsIndex = (int)cmd.Parameters["@SectionMeetingsIndex"].Value;
            _roomsId = (int)cmd.Parameters["@RoomsID"].Value;
            if (cmd.Parameters["@MeetDays"].Value != DBNull.Value)
                _meetDays = (string)cmd.Parameters["@MeetDays"].Value;
            if (cmd.Parameters["@MeetStartTime"].Value != DBNull.Value)
                _meetStartTime = (DateTime)cmd.Parameters["@MeetStartTime"].Value;
            if (cmd.Parameters["@MeetEndTime"].Value != DBNull.Value)
                _meetEndTime = (DateTime)cmd.Parameters["@MeetEndTime"].Value;
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