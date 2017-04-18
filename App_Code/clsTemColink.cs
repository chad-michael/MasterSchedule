using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsTempColink
{
    public ClsTempColink(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _colinkId;

    public int ColinkId
    {
        get { return _colinkId; }
        set { _colinkId = value; }
    }

    private string _action = "Add";

    public string Action
    {
        get { return _action; }
        set { _action = value; }
    }

    private string _department;

    public string Department
    {
        get { return _department; }
        set { _department = value; }
    }

    private string _course;

    public string Course
    {
        get { return _course; }
        set { _course = value; }
    }

    private string _section;

    public string Section
    {
        get { return _section; }
        set { _section = value; }
    }

    private Guid _sectionId;

    public Guid SectionId
    {
        get { return _sectionId; }
        set { _sectionId = value; }
    }

    public void AddRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "TempColink_Add";
        cmd.Parameters.Add("@Department", SqlDbType.VarChar, 128).Value = _department;
        cmd.Parameters.Add("@Course", SqlDbType.VarChar, 50).Value = _course;
        cmd.Parameters.Add("@Section", SqlDbType.VarChar, 8).Value = _section;
        cmd.Parameters.Add("@Action", SqlDbType.VarChar, 5).Value = _action;
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Value = _sectionId;
        cmd.Parameters.Add("@ColinkID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _colinkId = (int)cmd.Parameters["@ColinkID"].Value;
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
        cmd.CommandText = "TempCoLink_Delete";
        cmd.Parameters.Add("@ColinkID", SqlDbType.Int).Value = _colinkId;
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
        cmd.CommandText = "TempColink_Fill";
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Value = _sectionId;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "TempLink");
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
        cmd.CommandText = "TempColink_Get";
        cmd.Parameters.Add("@ColinkID", SqlDbType.Int, 4).Value = _colinkId;
        cmd.Parameters.Add("@Department", SqlDbType.VarChar, 128).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Course", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Section", SqlDbType.VarChar, 8).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Action", SqlDbType.VarChar, 5).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _department = (string)cmd.Parameters["@Department"].Value;
            _course = (string)cmd.Parameters["@Course"].Value;
            _section = (string)cmd.Parameters["@Section"].Value;
            _action = (string)cmd.Parameters["@Action"].Value;
            _sectionId = (Guid)cmd.Parameters["@SectionID"].Value;
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
        cmd.CommandText = "TempColink_Update";
        cmd.Parameters.Add("@ColinkID", SqlDbType.Int, 4).Value = _colinkId;
        cmd.Parameters.Add("@Department", SqlDbType.VarChar, 128).Value = _department;
        cmd.Parameters.Add("@Course", SqlDbType.VarChar, 50).Value = _course;
        cmd.Parameters.Add("@Section", SqlDbType.VarChar, 8).Value = _section;
        cmd.Parameters.Add("@Action", SqlDbType.VarChar, 5).Value = _action;
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Value = _sectionId;
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