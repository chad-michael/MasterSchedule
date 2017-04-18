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

public partial class ViewSection : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        Control form = (Control)Page.Master.FindControl("form1");
        ScriptManager sm = (ScriptManager)form.FindControl("ScriptManager1");
        sm.RegisterAsyncPostBackControl(gvPendingChanges);
        Session["usepreview"] = false;
        if (divchairoption.Visible) { Session["usepreview"] = radSubmitOptions.SelectedIndex == 1; }
        if (!IsPostBack) {
            divchairoption.Visible = Helpers.UserIsDivisionChair();
            int TabID = 0;
            int.TryParse(Request["TabID"], out TabID);
            TabContainer1.ActiveTabIndex = TabID;
        }

    }


}
