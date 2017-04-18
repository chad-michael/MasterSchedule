using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class ClsDivisionChairs
{
    public ClsDivisionChairs(string connectionStringName)
    {
        _connectionStringName = connectionStringName;
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
    }

    public ClsDivisionChairs(string connectionStringName, int cRpDivisionId, int iDno, string divisionName)
    {
        _connectionStringName = connectionStringName;
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
        _crpDivisionId = cRpDivisionId;
        _idno = iDno;
        _divisionName = divisionName;
    }

    public ClsDivisionChairs(string connectionStringName, int divisionChairsId)
    {
        _connectionStringName = connectionStringName;
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());
        _divisionChairsId = divisionChairsId;
    }

    #region 'SqlError' event definition code

    /// <summary>
    ///     This represents the delegate method prototype that
    ///     event receivers must implement
    /// </summary>
    public delegate void SqlErrorEventHandler(object sender, System.EventArgs e);

    /// <summary>
    ///     TODO: Describe the purpose of SqlError here
    /// </summary>
    public event SqlErrorEventHandler SqlError;

    /// <summary>
    ///     This is the method that is responsible for notifying
    ///     receivers that the event occurred
    /// </summary>
    protected virtual void OnSqlError(System.EventArgs e)
    {
        if (SqlError != null)
        {
            SqlError(this, e);
        }
    }

    #endregion 'SqlError' event definition code

    private SqlConnection _conn;
    private string _connectionStringName = string.Empty;

    private string _lastError = string.Empty;

    public string LastError
    {
        get { return _lastError; }
    }

    private int _divisionChairsId = int.MinValue;

    public int DivisionChairsId
    {
        get { return _divisionChairsId; }
        set { _divisionChairsId = value; }
    }

    private int _crpDivisionId = int.MinValue;

    public int CrpDivisionId
    {
        get { return _crpDivisionId; }
        set { _crpDivisionId = value; }
    }

    private int _idno = int.MinValue;

    public int Idno
    {
        get { return _idno; }
        set { _idno = value; }
    }

    private string _divisionName = string.Empty;

    public string DivisionName
    {
        get { return _divisionName; }
        set { _divisionName = value; }
    }

    public void AddRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "DivisionChairs_Add";
        cmd.Parameters.Add("@CRPDivisionID", SqlDbType.Int, 4).Value = _crpDivisionId;
        cmd.Parameters.Add("@IDNO", SqlDbType.Int, 4).Value = _idno;
        cmd.Parameters.Add("@DivisionName", SqlDbType.NVarChar, 100).Value = _divisionName;
        cmd.Parameters.Add("@DivisionChairsID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            _divisionChairsId = (int)cmd.Parameters["@DivisionChairsID"].Value;
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message; OnSqlError(new EventArgs());
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
        cmd.CommandText = "DivisionChairs_Delete";
        cmd.Parameters.Add("@DivisionChairsID", SqlDbType.Int).Value = _divisionChairsId;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message; OnSqlError(new EventArgs());
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
        cmd.CommandText = "DivisionChairs_Fill";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            da.Fill(ds, "DivisionChairs");
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

    public List<ClsDivisionChairs> FillList()
    {
        DataSet ds = FillDs();
        return FillList(ds);
    }

    public List<ClsDivisionChairs> FillList(DataSet ds)
    {
        List<ClsDivisionChairs> list = new List<ClsDivisionChairs>();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            ClsDivisionChairs objTemp = new ClsDivisionChairs(_connectionStringName);
            objTemp.DivisionChairsId = (int)row["DivisionChairsID"];
            if (row["CRPDivisionID"] != System.DBNull.Value)
                objTemp.CrpDivisionId = (int)row["CRPDivisionID"];
            if (row["IDNO"] != System.DBNull.Value)
                objTemp.Idno = (int)row["IDNO"];
            if (row["DivisionName"] != System.DBNull.Value)
                objTemp.DivisionName = (string)row["DivisionName"];
            list.Add(objTemp);
        }
        return list;
    }

    public void GetRecord()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "DivisionChairs_Get";
        cmd.Parameters.Add("@DivisionChairsID", SqlDbType.Int, 4).Value = _divisionChairsId;
        cmd.Parameters.Add("@CRPDivisionID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@IDNO", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@DivisionName", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
            if (cmd.Parameters["@CRPDivisionID"].Value != System.DBNull.Value)
            {
                _crpDivisionId = (int)cmd.Parameters["@CRPDivisionID"].Value;
            }
            if (cmd.Parameters["@IDNO"].Value != System.DBNull.Value)
            {
                _idno = (int)cmd.Parameters["@IDNO"].Value;
            }
            if (cmd.Parameters["@DivisionName"].Value != System.DBNull.Value)
            {
                _divisionName = (string)cmd.Parameters["@DivisionName"].Value;
            }
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message; OnSqlError(new EventArgs());
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
        cmd.CommandText = "DivisionChairs_Update";
        cmd.Parameters.Add("@DivisionChairsID", SqlDbType.Int, 4).Value = _divisionChairsId;
        cmd.Parameters.Add("@CRPDivisionID", SqlDbType.Int, 4).Value = _crpDivisionId;
        cmd.Parameters.Add("@IDNO", SqlDbType.Int, 4).Value = _idno;
        cmd.Parameters.Add("@DivisionName", SqlDbType.NVarChar, 100).Value = _divisionName;
        try
        {
            _conn.Open();
            cmd.ExecuteScalar();
        }
        catch (SqlException ex)
        {
            _lastError = ex.Message; OnSqlError(new EventArgs());
        }
        finally
        {
            if (_conn.State.Equals(ConnectionState.Open)) { _conn.Close(); }
        }
        cmd.Dispose();
    }
}