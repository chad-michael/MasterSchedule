using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Faculty : System.Web.UI.UserControl
{
    private ClsChangeLog log = new ClsChangeLog("MasterSchedule");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
        }
    }

    protected void lnkAddFaculty_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    protected void gvFacultyAssignment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "deletefaculty")
        {
            log.SectionsId = int.Parse(Request["SectionsID"]);
            log.SubmittedBy = Session["deltaid"].ToString();
            log.Change = "Drop Faculty: " + e.CommandArgument.ToString();
            log.AddRecord();

            Helpers.RefreshSortPending((GridView)Helpers.FindControlRecursive(Page.Master, "gvPendingChanges")); //Refresh the pending list
        }
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    protected void btnAddFaculty_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Add Faculty: ");
        ArrayList items = new ArrayList();
        foreach (ListItem item in lbFacultyUnassigned.Items)
        {
            if (item.Selected)
            {
                items.Add(item);
            }
        }
        if (items.Count == 1)
        {
            ListItem item = (ListItem)items[0];
            sb.Append(item.Text);
            lbFacultyUnassigned.Items.Remove(item);
        }
        else
        {
            sb.Append("<ol>");
            foreach (ListItem item in items)
            {
                sb.Append("<li>" + item.Text + "</li>");
                lbFacultyUnassigned.Items.Remove(item);
            }
            sb.Append("</ol>");
        }

        if (txtFacultyName.Text != "")
        {
            sb.Append(" " + txtFacultyName.Text);
        }

        lbFacultyUnassigned.SelectedIndex = -1;

        log.SectionsId = int.Parse(Request["SectionsID"]);
        log.SubmittedBy = Session["deltaid"].ToString();
        log.Change = sb.ToString();
        log.AddRecord();

        Helpers.RefreshSortPending((GridView)Helpers.FindControlRecursive(Page.Master, "gvPendingChanges")); //Refresh the pending list

        //reset txt box
        txtFacultyName.Text = "";
    }
}