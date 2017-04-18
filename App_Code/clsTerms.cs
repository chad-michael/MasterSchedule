using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsTerms
{
    public ClsTerms(string connectionStringName)
    {
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    private SqlConnection _conn;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _termsId;

    public int TermsId
    {
        get { return _termsId; }
        set { _termsId = value; }
    }

    private int _termYear;

    public int TermYear
    {
        get { return _termYear; }
        set { _termYear = value; }
    }

    private int _semestersId;

    public int SemestersId
    {
        get { return _semestersId; }
        set { _semestersId = value; }
    }

    private string _term;

    public string Term
    {
        get { return _term; }
        set { _term = value; }
    }

    private DateTime _termStartDate;

    public DateTime TermStartDate
    {
        get { return _termStartDate; }
        set { _termStartDate = value; }
    }

    private DateTime _termEndDate;

    public DateTime TermEndDate
    {
        get { return _termEndDate; }
        set { _termEndDate = value; }
    }

    private DateTime _termWaitListDate;

    public DateTime TermWaitListDate
    {
        get { return _termWaitListDate; }
        set { _termWaitListDate = value; }
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "Terms_Get";
        cmd.Parameters.Add("@TermsID", SqlDbType.Int, 4).Value = _termsId;
        cmd.Parameters.Add("@TermYear", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@SemestersID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Term", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@TermStartDate", SqlDbType.DateTime, 8).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@TermEndDate", SqlDbType.DateTime, 8).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@TermWaitListDate", SqlDbType.DateTime, 8).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _termYear = (int)cmd.Parameters["@TermYear"].Value;
            _semestersId = (int)cmd.Parameters["@SemestersID"].Value;
            _term = (string)cmd.Parameters["@Term"].Value;

            if (cmd.Parameters["@TermStartDate"].Value != DBNull.Value)
                _termStartDate = (DateTime)cmd.Parameters["@TermStartDate"].Value;

            if (cmd.Parameters["@TermEndDate"].Value != DBNull.Value)
                _termEndDate = (DateTime)cmd.Parameters["@TermEndDate"].Value;

            if (cmd.Parameters["@TermWaitListDate"].Value != DBNull.Value)
                _termWaitListDate = (DateTime)cmd.Parameters["@TermWaitListDate"].Value;
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