using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsTempExistingFaculty
{
    public ClsTempExistingFaculty(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _facultyId;

    public int FacultyId
    {
        get { return _facultyId; }
        set { _facultyId = value; }
    }

    private string _facultyName;

    public string FacultyName
    {
        get { return _facultyName; }
        set { _facultyName = value; }
    }

    private Guid _sectionId;

    public Guid SectionId
    {
        get { return _sectionId; }
        set { _sectionId = value; }
    }

    private int _deleted;

    public int Deleted
    {
        get { return _deleted; }
        set { _deleted = value; }
    }

    private int _recordId;

    public int RecordId
    {
        get { return _recordId; }
        set { _recordId = value; }
    }

    private int _qsSectionsId;

    public int QsSectionsId
    {
        get { return _qsSectionsId; }
        set { _qsSectionsId = value; }
    }

    public void AddRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "TempExistingFaculty_Add";
        cmd.Parameters.Add("@FacultyName", SqlDbType.VarChar, 128).Value = _facultyName;
        cmd.Parameters.Add("@SectionID", SqlDbType.Int).Value = QsSectionsId;
        cmd.Parameters.Add("@FacultyID", SqlDbType.Int).Value = FacultyId;
        cmd.Parameters.Add("@NewSectionID", SqlDbType.UniqueIdentifier).Value = SectionId;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _facultyId = (int)cmd.Parameters["@FacultyID"].Value;
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
        cmd.CommandText = "TempExistingFaculty_Delete";
        cmd.Parameters.Add("@FacultyID", SqlDbType.Int).Value = _facultyId;
        cmd.Parameters.Add("@SectionsID", SqlDbType.Int).Value = _qsSectionsId;
        cmd.Parameters.Add("@NewSectionID", SqlDbType.UniqueIdentifier).Value = SectionId;
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
        cmd.CommandText = "TempExistingFaculty_Fill";
        cmd.Parameters.Add("@SectionID", SqlDbType.Int).Value = _qsSectionsId;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "TempFaculty");
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
        cmd.CommandText = "TempExistingFaculty_Get";
        cmd.Parameters.Add("@FacultyID", SqlDbType.Int, 4).Value = _facultyId;
        cmd.Parameters.Add("@FacultyName", SqlDbType.VarChar, 128).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@SectionID", SqlDbType.UniqueIdentifier, 16).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _facultyName = (string)cmd.Parameters["@FacultyName"].Value;
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
        cmd.CommandText = "TempExistingFaculty_Update";
        cmd.Parameters.Add("@FacultyID", SqlDbType.Int, 4).Value = _facultyId;
        cmd.Parameters.Add("@FacultyName", SqlDbType.VarChar, 128).Value = _facultyName;
        cmd.Parameters.Add("@SectionID", SqlDbType.Int, 16).Value = _qsSectionsId;
        cmd.Parameters.Add("@Deleted", SqlDbType.Int).Value = _deleted;
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

    public void FlagRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "Update TempExistingFaculty set Deleted = 1 where RecordID =" + _recordId;
        try
        {
            _conn.Open();
            cmd.ExecuteNonQuery();
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