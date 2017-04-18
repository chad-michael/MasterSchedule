using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsSections
{
    public ClsSections(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _sectionsId;

    public int SectionsId
    {
        get { return _sectionsId; }
        set { _sectionsId = value; }
    }

    private int _coursesId;

    public int CoursesId
    {
        get { return _coursesId; }
        set { _coursesId = value; }
    }

    private string _sectionNumber;

    public string SectionNumber
    {
        get { return _sectionNumber; }
        set { _sectionNumber = value; }
    }

    private int _termsId;

    public int TermsId
    {
        get { return _termsId; }
        set { _termsId = value; }
    }

    private decimal _capacity;

    public decimal Capacity
    {
        get { return _capacity; }
        set { _capacity = value; }
    }

    private DateTime _sectionStartDate;

    public DateTime SectionStartDate
    {
        get { return _sectionStartDate; }
        set { _sectionStartDate = value; }
    }

    private DateTime _sectionEndDate;

    public DateTime SectionEndDate
    {
        get { return _sectionEndDate; }
        set { _sectionEndDate = value; }
    }

    private string _status;

    public string Status
    {
        get { return _status; }
        set { _status = value; }
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Sections_Get";
        cmd.Parameters.Add("@SectionsID", SqlDbType.Int, 4).Value = _sectionsId;
        cmd.Parameters.Add("@CoursesID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@SectionNumber", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@TermsID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Capacity", SqlDbType.Decimal, 9).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@SectionStartDate", SqlDbType.DateTime, 8).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@SectionEndDate", SqlDbType.DateTime, 8).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 2).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _coursesId = (int)cmd.Parameters["@CoursesID"].Value;
            _sectionNumber = (string)cmd.Parameters["@SectionNumber"].Value;
            _termsId = (int)cmd.Parameters["@TermsID"].Value;
            _capacity = (decimal)cmd.Parameters["@Capacity"].Value;
            _sectionStartDate = (DateTime)cmd.Parameters["@SectionStartDate"].Value;
            _sectionEndDate = (DateTime)cmd.Parameters["@SectionEndDate"].Value;
            _status = (string)cmd.Parameters["@Status"].Value;
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