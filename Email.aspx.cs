using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

public partial class EmailTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
        //try {
        //MailMessage msg = new MailMessage();
        //msg.From = new MailAddress("test@delta.com");
        //msg.Subject = "Test";
        //msg.IsBodyHtml = true;
        //msg.Body = "Test";
        //msg.To.Add(new MailAddress("richardzeien@delta.edu"));

        //if (msg.To.Count > 0) {
        //  SmtpClient smtp = new SmtpClient();
        //smtp.Host = "smtp.delta.edu";
        //smtp.Port = 25;

        //smtp.Send(msg);
        //smtp = null;
        //}
        //msg.Dispose();
        //}
        //catch { }
        SendEmail("Test", "Test", "michaelmccloskey@delta.edu");
        SendEmail("Test", "Test", "richardzeien@delta.edu");
        SendEmail("Test", "Test", "richardzeien@delta.edu");
        Response.Write("Test");
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
}