using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Buildings
{
    public Buildings(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    protected SqlConnection _conn;

    private string _lastError = string.Empty;

    public string GetLastError()
    {
        return _lastError;
    }

    private int _buildingsId;

    public int GetBuildingsId()
    {
        return _buildingsId;
    }

    public void SetBuildingsId(int value)
    {
        _buildingsId = value;
    }

    private string _buildingNumber;

    public string GetBuildingNumber()
    {
        return _buildingNumber;
    }

    public void SetBuildingNumber(string value)
    {
        _buildingNumber = value;
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
            string buildingNum = _buildingNumber.Trim().ToLower();
            if (buildingNum == "inet" || buildingNum == "tnet" || buildingNum == "tele")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public bool CheckIsBrickAndMortar(string building)
    {
        string buildingNum = building.Trim().ToLower();
        if (buildingNum == "inet" || buildingNum == "tnet" || buildingNum == "tele")
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
        SqlCommand cmd = new SqlCommand()
        {
            Connection = _conn,
            CommandType = CommandType.StoredProcedure,
            CommandText = "Buildings_Get"
        };
        cmd.Parameters.Add("@BuildingsID", SqlDbType.Int, 4).Value = _buildingsId;
        cmd.Parameters.Add("@BuildingNumber", SqlDbType.Char, 10).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@CampusesID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _buildingNumber = (string)cmd.Parameters["@BuildingNumber"].Value;
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