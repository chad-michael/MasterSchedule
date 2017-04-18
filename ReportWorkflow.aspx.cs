using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ReportWorkflow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClsChangeLog log = new ClsChangeLog("MasterSchedule");
        log.LogId = int.Parse(Request.QueryString["changelogid"]);
        DataView view = log.FillDs().Tables[0].DefaultView;
        view.RowFilter = "LogID = " + Request.QueryString["changelogid"];

        this.gvNeedsApproval.DataSource = view;
        this.gvNeedsApproval.DataBind();
    }

    public string ViewCourseCss(object sectionsid)
    {
        int id = (int)sectionsid;
        if (id > 0)
            return "display:block;";
        else
            return "display:none;";
    }
}