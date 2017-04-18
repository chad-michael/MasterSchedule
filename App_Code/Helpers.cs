using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Helpers
/// </summary>
public static class Helpers
{
    /// <summary>
    /// Returns the Delta Counseling connection string
    /// </summary>
    /// <returns>Data Source=DB1;Initial Catalog=CRP;Integrated Security=True</returns>
    public static string Delta_Counseling()
    {
        return ConfigurationManager.ConnectionStrings["Delta_Counseling"].ConnectionString.ToString();
    }

    /// <summary>
    /// Returns the connection string for DeltaNetSQL.  DeltaNetSQL is used to store the authentication cache for the user.
    /// </summary>
    /// <returns>Data Source=SQL, 20149;Initial Catalog=DeltaNetSQL;Integrated Security=True</returns>
    /// <remarks>
    /// This method is needed for the Gloabl.asax file.
    /// </remarks>
    public static string DeltaNetSql()
    {
        return ConfigurationManager.ConnectionStrings["DeltaNetSQL"].ConnectionString.ToString();
    }

    /// <summary>
    /// Finds a Control recursively. Note finds the first match and exists
    /// </summary>
    /// <returns>Control</returns>
    public static Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id)
            return root;
        foreach (Control ctl in root.Controls)
        {
            Control foundCtl = FindControlRecursive(ctl, id);
            if (foundCtl != null)
                return foundCtl;
        }
        return null;
    }

    /// <summary>
    /// Refreshes a gridview and sorts it by DateSubmitted Asc then moves to the last page.
    /// </summary>
    /// <returns></returns>
    public static void RefreshSortPending(GridView gv)
    {
        gv.DataBind();
        gv.Sort("DateSubmitted", SortDirection.Ascending);
        if (gv.PageCount > 0)
        {
            gv.PageIndex = gv.PageCount - 1;
        }
    }

    /// <summary>
    /// Checks to see if the currently logged in user is a division chair.
    /// </summary>
    /// <returns>bool</returns>
    public static bool UserIsDivisionChair()
    {
        bool value = false;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterSchedule"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "DivisionChair_Check";
        cmd.Parameters.Add("@DeltaID", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["deltaid"].ToString();
        cmd.Parameters.Add("@IsDivisionChair", SqlDbType.Bit, 1).Direction = ParameterDirection.Output;
        try
        {
            conn.Open();
            cmd.ExecuteScalar();
            value = (bool)cmd.Parameters["@IsDivisionChair"].Value;
        }
        catch
        {
            //just keep swimming :-)
        }
        finally
        {
            if (conn.State.Equals(ConnectionState.Open)) { conn.Close(); }
        }
        cmd.Dispose();
        return value;
    }

    /// <summary>
    /// The number of pending changes that a division chair needs to approve.
    /// </summary>
    /// <returns>int</returns>
    public static int DivisionPendingChanges()
    {
        int value = 0;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterSchedule"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "DivisionChair_CountPending";
        cmd.Parameters.Add("@DeltaID", SqlDbType.VarChar, 50).Value = HttpContext.Current.Session["deltaid"].ToString();
        cmd.Parameters.Add("@NoChanges", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        try
        {
            conn.Open();
            cmd.ExecuteScalar();
            value = (int)cmd.Parameters["@NoChanges"].Value;
        }
        catch
        {
            //just keep swimming :-)
        }
        finally
        {
            if (conn.State.Equals(ConnectionState.Open)) { conn.Close(); }
        }
        cmd.Dispose();
        return value;
    }

    public static string GetDeltaEmail(string deltaId)
    {
        string value = "";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterSchedule"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetDeltaEmail";
        cmd.Parameters.Add("@DeltaID", SqlDbType.VarChar, 50).Value = deltaId;
        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
        try
        {
            conn.Open();
            cmd.ExecuteScalar();
            value = (string)cmd.Parameters["@Email"].Value;
        }
        catch
        {
            //just keep swimming :-)
        }
        finally
        {
            if (conn.State.Equals(ConnectionState.Open)) { conn.Close(); }
        }
        cmd.Dispose();
        return value;
    }

    public static string GetDivisionEmail(int sectionId)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterSchedule"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetDivisionEmail";
        cmd.Parameters.Add("@SectionId", SqlDbType.Int, 4).Value = sectionId;
        SqlDataReader rdr = null;
        StringBuilder emails = new StringBuilder();
        try
        {
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                emails.Append((string)rdr[0]);
                emails.Append(";");
            }
        }
        catch (Exception ex)
        {
            //just keep swimming :-)
        }
        finally
        {
            if (conn.State.Equals(ConnectionState.Open)) { conn.Close(); }
            if (rdr != null) { rdr.Dispose(); }
        }
        cmd.Dispose();
        return emails.ToString();
    }

    public static string GetDivisionEmailByDepartmentsId(int departmentsId)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterSchedule"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetDeltaEmailByDepartmentsID";
        cmd.Parameters.Add("@DepartmentsID", SqlDbType.Int, 4).Value = departmentsId;
        SqlDataReader rdr = null;
        StringBuilder emails = new StringBuilder();
        try
        {
            conn.Open();
            rdr = cmd.ExecuteReader();
            string sep = "";
            while (rdr.Read())
            {
                emails.Append(sep);
                emails.Append((string)rdr[0]);
                sep = ";";
            }
        }
        catch (Exception ex)
        {
            //just keep swimming :-)
        }
        finally
        {
            if (conn.State.Equals(ConnectionState.Open)) { conn.Close(); }
            if (rdr != null) { rdr.Dispose(); }
        }
        cmd.Dispose();
        return emails.ToString();
    }

    public static string GetDivisionEmailByDepartmentsId(int departmentsId, string submitter)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterSchedule"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "GetDeltaEmailByDepartmentsID";
        cmd.Parameters.Add("@DepartmentsID", SqlDbType.Int, 4).Value = departmentsId;
        SqlDataReader rdr = null;
        StringBuilder emails = new StringBuilder();
        try
        {
            conn.Open();
            rdr = cmd.ExecuteReader();
            string sep = "";
            submitter = submitter + "@delta.edu";
            while (rdr.Read())
            {
                if (submitter.CompareTo((string)rdr[0]) == 0)
                {
                    emails.Remove(0, emails.Length);
                    emails.Append((string)rdr[0]);
                    break;
                }
                else
                {
                    emails.Append(sep);
                    emails.Append((string)rdr[0]);
                    sep = ";";
                }
            }
        }
        catch (Exception ex)
        {
            //just keep swimming :-)
        }
        finally
        {
            if (conn.State.Equals(ConnectionState.Open)) { conn.Close(); }
            if (rdr != null) { rdr.Dispose(); }
        }
        cmd.Dispose();
        return emails.ToString();
    }

    public static void SendEmail(string subject, string body, string to)
    {
        try
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;

            foreach (string address in to.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address != "jamiller@delta.edu" && address != "ktwilson@delta.edu")
                {
                    msg.To.Add(new MailAddress(address));
                }
            }
            if (msg.To.Count > 0)
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["EmailHost"];
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
                if (!ConfigurationManager.AppSettings["EmailUsername"].Equals(string.Empty))
                {
                    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUsername"], ConfigurationManager.AppSettings["EmailPassword"]);
                }
                smtp.Send(msg);
                smtp = null;
            }
            msg.Dispose();
        }
        catch { }
    }

    public static void SendEmail(string subject, string body, string to, int sectionId)
    {
        try
        {
            if (sectionId > 0)
            {
                body = body + GetDescriptiveEmail(sectionId);
            }
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;

            foreach (string address in to.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                msg.To.Add(new MailAddress(address));
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["EmailHost"];
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
            if (!ConfigurationManager.AppSettings["EmailUsername"].Equals(string.Empty))
            {
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUsername"], ConfigurationManager.AppSettings["EmailPassword"]);
            }
            smtp.Send(msg);
            smtp = null;

            msg.Dispose();
        }
        catch { }
    }

    public static string GetDescriptiveEmail(int sectionId)
    {
        if (sectionId > 0)
        {
            ClsSections section = new ClsSections("MasterSchedule");
            section.SectionsId = sectionId;
            section.GetRecord();

            ClsTerms term = new ClsTerms("MasterSchedule");
            term.TermsId = section.TermsId;
            term.GetRecord();

            ClsCourses course = new ClsCourses("MasterSchedule");
            course.CoursesId = section.CoursesId;
            course.GetRecord();

            ClsDepartments department = new ClsDepartments("MasterSchedule");
            department.DepartmentsId = course.DepartmentsId;
            department.GetRecord();

            ClsSemesters semester = new ClsSemesters("MasterSchedule");
            semester.SemestersId = term.SemestersId;
            semester.GetRecord();

            StringBuilder sb = new StringBuilder();
            sb.Append("<p style='font-weight:bold;'>");
            sb.Append("</p>");
            sb.Append("<br /><br />Orginal Section Detail");
            sb.Append("<table>");
            AppendRow("Department", department.DepartmentCode, sb);
            AppendRow("Course Number", course.CourseNumber, sb);
            AppendRow("Section", section.SectionNumber, sb);
            AppendRow("Section Start", section.SectionStartDate.ToShortDateString(), sb);
            AppendRow("Section End", section.SectionEndDate.ToShortDateString(), sb);
            sb.Append("</table>");
            return sb.ToString();
        }
        else return "";
    }

    private static void AppendRow(string label, string value, StringBuilder sb)
    {
        sb.Append(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", label, value));
    }
}