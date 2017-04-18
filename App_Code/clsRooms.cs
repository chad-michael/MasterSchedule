using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsRooms
{
    public ClsRooms(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string GetLastError()
    {
        return _lastError;
    }

    private int _roomsId;

    public int GetRoomsId()
    {
        return _roomsId;
    }

    public void SetRoomsId(int value)
    {
        _roomsId = value;
    }

    private string _roomNumber;

    public string GetRoomNumber()
    {
        return _roomNumber;
    }

    public void SetRoomNumber(string value)
    {
        _roomNumber = value;
    }

    private int _campusesId;

    public int CampusesId
    {
        get { return _campusesId; }
        set { _campusesId = value; }
    }

    public bool IsBrickMortar
    {
        get
        {
            string roomNum = _roomNumber.Trim().ToLower();
            if (roomNum == "inet" || roomNum == "tnet" || roomNum == "tele")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public bool CheckIsBrickAndMortar(string room)
    {
        string roomNum = room.Trim().ToLower();
        if (roomNum == "inet" || roomNum == "tnet" || roomNum == "tele")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Rooms_Get";
        cmd.Parameters.Add("@RoomsID", SqlDbType.Int, 4).Value = _roomsId;
        cmd.Parameters.Add("@RoomNumber", SqlDbType.Char, 10).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@CampusesID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _roomNumber = (string)cmd.Parameters["@RoomNumber"].Value;
            _campusesId = (int)cmd.Parameters["@CampusesID"].Value;
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