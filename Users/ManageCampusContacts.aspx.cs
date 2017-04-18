using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Users
{
    public partial class Users_Users_AddDivChair : System.Web.UI.Page
    {
        private ClsDivisionChairs dc;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAddDivChair_Click(object sender, EventArgs e)
        {
            errorBox.Visible = false;
            try
            {
                //dc.DivisionName = ddlDivision.SelectedItem.Text.ToString();
                //dc.CRPDivisionID = Int32.Parse(ddlDivision.SelectedValue.ToString());
                //dc.IDNO = Int32.Parse(txtIDNO.Text);
                //dc.AddRecord();

                //Response.Redirect("addDivChair.aspx");
                using (MasterScheduleDataContext db = new MasterScheduleDataContext())
                {
                    MeetingCenterContact newContact = new MeetingCenterContact();
                    newContact.MeetingCenter = ddlDivision.SelectedValue;
                    newContact.ContactIDNO = int.Parse(txtIDNO.Text);

                    db.MeetingCenterContacts.InsertOnSubmit(newContact);
                    db.SubmitChanges();
                }
                Response.Redirect("~/Users/managecampuscontacts.aspx");
            }
            catch
            {
                errorBox.Visible = true;
            }
        }

        protected void btnDeleteMe_Click(object sender, EventArgs e)
        {
            Button Sender = (Button)sender;
            string sRowToRemove = Sender.CommandArgument;

            int iRowToRemove = int.Parse(sRowToRemove);

            using (MasterScheduleDataContext db = new MasterScheduleDataContext())
            {
                db.MeetingCenterContacts.DeleteOnSubmit(db.MeetingCenterContacts.Where(X => X.MeetingCenterContactID == iRowToRemove).First());
                db.SubmitChanges();
            }
            Response.Redirect("~/Users/managecampuscontacts.aspx");
        }
    }
}