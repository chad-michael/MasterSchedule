using System;
using System.Web.UI.WebControls;

namespace Controls
{
    public partial class Controls_CrossLinked_Modify : System.Web.UI.UserControl
    {
        private ClsTempLink link;

        protected void Page_Load(object sender, EventArgs e)
        {
            link = new ClsTempLink("MasterSchedule");
            link.SectionId = (Guid)Session["NewSectionID"];
            if (!IsPostBack)
            {
                //  MultiView1.ActiveViewIndex = 0;
                SectionID.Value = Session["NewSectionID"].ToString();
            }
        }

        //protected void lnkAddRecord_Click(object sender, EventArgs e) {
        //    MultiView1.ActiveViewIndex = 1;
        //}
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCatalogNumber.Text = "";
            txtDepartment.Text = "";
            txtSectionNumber.Text = "";
            rblAction.SelectedIndex = -1;
            // MultiView1.ActiveViewIndex = 0;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            link.Course = txtCatalogNumber.Text;
            link.Department = txtDepartment.Text;
            link.Section = txtSectionNumber.Text;
            link.Action = rblAction.SelectedValue.ToString();
            link.AddRecord();
            gvXLink.DataBind();
            txtCatalogNumber.Text = "";
            txtDepartment.Text = "";
            txtSectionNumber.Text = "";
            rblAction.SelectedIndex = -1;
        }

        protected void gvXLink_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deletexlink")
            {
                link.LinkId = int.Parse(e.CommandArgument.ToString());
                link.DeleteRecord();
                gvXLink.DataBind();
            }
        }
    }
}