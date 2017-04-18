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
using System.Text;
using System.Collections.Generic;
using AjaxControlToolkit;

public partial class Report : System.Web.UI.Page
{
    private ClsChangeLog _log;

    public string rowFilter
    {
        get { return (string)ViewState["rowfilter"]; }
        set { ViewState["rowfilter"] = value; }
    }

    protected int currentPageNumber = 1;
    private const int PAGE_SIZE = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        _log = new ClsChangeLog("MasterSchedule");
        // FIX for hiding calendar when a date is selected
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "hideCalendar", "function hideCalendar(cb) { cb.hide(); }", true);

        // FIX for css not loading when a calendar is inside of an updatepanel+multiview (+tabcontrol?)
        string url = Page.ClientScript.GetWebResourceUrl(typeof(ClientCssResourceAttribute), "AjaxControlToolkit.Calendar.Calendar.css");
        HtmlLink myHtmlLink = new HtmlLink();
        myHtmlLink.Href = url;
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        Page.Header.Controls.Add(myHtmlLink);
        rowFilter = "Select * From ChangeLog_Report ORDER BY DateSubmitted";

        if (!Page.IsPostBack)
        {
            DataSet ds = _log.FillDs();
            //gvNeedsApproval.DataSource = ds.Tables[0].DefaultView;
            //lblRecordCount.Text = ds.Tables[0].Rows.Count.ToString();
            //gvNeedsApproval.DataBind();

            BindLists(ds);
            CreateFilteredReport();
            currentPageNumber = 1;
        }
    }

    private void BindLists(DataSet ds)
    {
        DataSetHelper dsHelper = new DataSetHelper(ref ds);
        ClsDivisionProcessing dp = new ClsDivisionProcessing();
        dsHelper.SelectDistinct("Terms", ds.Tables[0], "Term");
        dsHelper.SelectDistinct("Course", ds.Tables[0], "CourseNumber");
        dsHelper.SelectDistinct("SubmittedBy", ds.Tables[0], "SubmittedBy");

        ddlTermFilter.DataSource = ds.Tables["Terms"];
        ddlTermFilter.DataBind();
        ddlTermFilter.Items.Insert(0, new ListItem("All", "All"));

        ddlCourse.DataSource = ds.Tables["Course"];
        ddlCourse.DataBind();
        ddlCourse.Items.Insert(0, new ListItem("All", "All"));

        ddlSubmittedByFilter.DataSource = ds.Tables["SubmittedBy"];
        ddlSubmittedByFilter.DataBind();
        ddlSubmittedByFilter.Items.Insert(0, new ListItem("All", "All"));

        ddlDivision.DataSource = dp.GetDivisions();
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, new ListItem("All", "All"));
    }

    public string ViewCourseCss(object sectionsid)
    {
        int id = (int)sectionsid;
        if (id > 0)
            return "display:block;";
        else
            return "display:none;";
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        currentPageNumber = 1;
        CreateFilteredReport();
    }

    private void CreateFilteredReport()
    {
        try
        {
            ClsDivisionProcessing dp = new ClsDivisionProcessing();
            List<string> rowFilters = new List<string>();
            if (ddlTermFilter.SelectedValue != "All")
            {
                rowFilters.Add("Term = '" + ddlTermFilter.SelectedValue + "' ");
            }
            if (ddlCourse.SelectedValue != "All")
            {
                rowFilters.Add("CourseNumber LIKE '" + ddlCourse.SelectedValue.Trim() + "%' ");
            }
            if (ddlSubmittedByFilter.SelectedValue != "All")
            {
                rowFilters.Add("SubmittedBy = '" + ddlSubmittedByFilter.SelectedValue.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(txtFrom.Text))
            {
                DateTime from = DateTime.Parse(txtFrom.Text);
                rowFilters.Add("DateSubmitted >=#" + from.ToShortDateString() + "#");
            }
            if (!string.IsNullOrEmpty(txtTo.Text))
            {
                DateTime to = DateTime.Parse(txtTo.Text);
                to = to.AddDays(1);
                rowFilters.Add("DateSubmitted <= #" + to.ToShortDateString() + "#");
            }
            if (ddlDivision.SelectedValue != "All")
            {
                rowFilters.Add(dp.GetDepartments(ddlDivision.SelectedValue));
            }

            StringBuilder sb = new StringBuilder();
            string a = "";
            foreach (string filter in rowFilters)
            {
                sb.Append(a);
                sb.Append(filter);
                a = " AND ";
            }
            rowFilter = sb.ToString();

            DataSet ds = _log.FillDs();
            DataTable dt = new DataTable();
            dt = ds.Tables[0].Clone();

            DataView view = ds.Tables[0].DefaultView;
            view.RowFilter = rowFilter;

            DataTable cacheTable = view.ToTable();
            int counter = ((currentPageNumber - 1) * PAGE_SIZE);
            for (int i = counter; i < (PAGE_SIZE + counter); i++)
            {
                dt.ImportRow(cacheTable.Rows[i]);

                if (i == (view.Count - 1))
                {
                    break;
                }
            }

            double totalRows = view.Count;

            lblTotalPages.Text = CalculateTotalPages(totalRows).ToString();
            lblCurrentPage.Text = currentPageNumber.ToString();

            if (currentPageNumber == 1)
            {
                Btn_Previous.Enabled = false;

                if (Int32.Parse(lblTotalPages.Text) > 1)
                {
                    Btn_Next.Enabled = true;
                }
                else
                    Btn_Next.Enabled = false;
            }
            else
            {
                Btn_Previous.Enabled = true;

                if (currentPageNumber == Int32.Parse(lblTotalPages.Text))
                    Btn_Next.Enabled = false;
                else Btn_Next.Enabled = true;
            }

            lblRecordCount.Text = view.Count.ToString();
            gvNeedsApproval.DataSource = dt;
            gvNeedsApproval.DataBind();
        }
        catch (Exception ex)
        {
            gvNeedsApproval.DataSource = null;
            gvNeedsApproval.DataBind();
        }
    }

    private int CalculateTotalPages(double totalRows)
    {
        int totalPages = (int)Math.Ceiling(totalRows / PAGE_SIZE);

        return totalPages;
    }

    protected void ChangePage(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Previous":
                currentPageNumber = Int32.Parse(lblCurrentPage.Text) - 1;
                break;

            case "Next":
                currentPageNumber = Int32.Parse(lblCurrentPage.Text) + 1;
                break;
        }

        CreateFilteredReport();
    }
}