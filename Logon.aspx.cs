using SunGard.Global.DirectoryServices;
using System;
using System.Drawing;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            //Control c = this.Master.FindControl("sa");
            //ImageButton saButton = c as ImageButton;
            //if (saButton != null) saButton.Enabled = false;

            Page.Form.DefaultButton = this.btnLogon.UniqueID;
        }
    }

    protected void btnLogon_Click(object sender, System.EventArgs e)
    {
        string adPath = System.Configuration.ConfigurationManager.AppSettings["DefaultServer"];
        string domainName = System.Configuration.ConfigurationManager.AppSettings["DefaultDomain"];
        SunGard.Global.DirectoryServices.LdapAuthentication adAuth = new SunGard.Global.DirectoryServices.LdapAuthentication(adPath);
        LogonAuthenticator adAuthenticator = new LogonAuthenticator(adPath, domainName, adAuth);

        Guid cacheGuid = new Guid("00000000-0000-0000-0000-000000000001");

        try
        {
            if (adAuthenticator.DoLogin(txtUserName.Text, txtPassword.Text, cacheGuid))
            {
                Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUserName.Text, false));
            }
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.ForeColor = Color.White;
            lblError.Font.Size = FontUnit.Point(10);
            lblError.Text = ex.Message;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
    }
}