using System;
using System.Web.UI.WebControls;

namespace Controls
{
    public partial class Controls_Coreqs_New : System.Web.UI.UserControl
    {
        private ClsTempCoreq coreq;

        protected void Page_Load(object sender, EventArgs e)
        {
            coreq = new ClsTempCoreq("MasterSchedule");
            coreq.SectionId = (Guid)Session["NewSectionID"];
            if (!IsPostBack)
            {
                //MultiView1.ActiveViewIndex = 0;
                SectionID.Value = Session["NewSectionID"].ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCatalogNumber.Text = "";
            txtDepartment.Text = "";
            txtSectionNumber.Text = "";
            //MultiView1.ActiveViewIndex = 0;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            coreq.Course = txtCatalogNumber.Text;
            coreq.Department = txtDepartment.Text;
            coreq.Section = txtSectionNumber.Text;
            coreq.AddRecord();
            gvCoreq.DataBind();
            txtCatalogNumber.Text = "";
            txtDepartment.Text = "";
            txtSectionNumber.Text = "";
            //MultiView1.ActiveViewIndex = 0;
        }

        protected void gvCoreq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deletecoreq")
            {
                coreq.CoreqId = int.Parse(e.CommandArgument.ToString());
                coreq.DeleteRecord();
                gvCoreq.DataBind();
            }
        }
    }
}