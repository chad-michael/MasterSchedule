using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public class ClsTempSection
{
    public ClsTempSection(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private Guid _sectionId;

    public Guid SectionId
    {
        get { return _sectionId; }
        set { _sectionId = value; }
    }

    private DateTime _dateCreated;

    public DateTime DateCreated
    {
        get { return _dateCreated; }
        set { _dateCreated = value; }
    }

    private string _createdBy;

    public string CreatedBy
    {
        get { return _createdBy; }
        set { _createdBy = value; }
    }

    public void AddRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "TempSection_Add";
        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["deltaid"];
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
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

    public void DeleteRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "TempSection_Delete";
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier).Value = _sectionId;
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
        cmd.CommandText = "TempSection_Fill";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "TempSection");
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
        cmd.CommandText = "TempSection_Get";
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Value = _sectionId;
        cmd.Parameters.Add("@DateCreated", SqlDbType.DateTime, 8).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _dateCreated = (DateTime)cmd.Parameters["@DateCreated"].Value;
            _createdBy = (string)cmd.Parameters["@CreatedBy"].Value;
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
        cmd.CommandText = "TempSection_Update";
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Value = _sectionId;
        cmd.Parameters.Add("@DateCreated", SqlDbType.DateTime, 8).Value = _dateCreated;
        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50).Value = _createdBy;
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