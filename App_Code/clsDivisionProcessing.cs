using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

/// <summary>
/// Summary description for clsDivisionProcessing
/// </summary>
public class ClsDivisionProcessing
{
    public ClsDivisionProcessing()
    {
    }

    public string GetDepartments(string divisionId)
    {
        List<string> departmentId = new List<string>();
        StringBuilder sb = new StringBuilder();

        CRPDataContext crp = new CRPDataContext();

        var dept = from d in crp.Departments
                   where d.DivisionsID == int.Parse(divisionId)
                   select d.DepartmentsID;
        if (dept != null)
        {
            foreach (int department in dept)
            {
                departmentId.Add("DepartmentsID = " + department.ToString());
            }
        }

        string a = "(";
        foreach (string filter in departmentId)
        {
            sb.Append(a);
            sb.Append(filter);
            a = " OR ";
        }
        sb.Append(")");
        return sb.ToString();
    }

    public DataTable GetDivisions()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn divisionDesc = new DataColumn();
        DataColumn divisionsId = new DataColumn();
        dt.Columns.Add("DivisionDesc");
        dt.Columns.Add("DivisionsID");
        ds.Tables.Add("dt");

        SqlConnection sc = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CRP"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Select * from MasterSchedule_Divisions Order By DivisionDesc", sc);

        try
        {
            sc.Open();
            dt.Load(cmd.ExecuteReader());
        }
        catch
        {
        }
        finally
        {
            if (sc.State == ConnectionState.Open)
            {
                sc.Close();
            }
        }

        return dt;
    }
}