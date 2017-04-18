using System;

public partial class ChangeEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hiddenObjectId.Value = Request.QueryString["changelogid"];
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.QueryString["returnurl"]);
    }
}