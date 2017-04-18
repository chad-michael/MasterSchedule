using System;
using System.Web.UI.WebControls;

namespace Users.Users
{
    public partial class UsersUsersCurrentUsers : System.Web.UI.UserControl
    {
        private ClsProcessUsers _processUsers;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._processUsers = new ClsProcessUsers("MasterSchedule");
            _processUsers.SqlError += new ClsProcessUsers.SqlErrorEventHandler(clsProcessUsers_SqlError);
            CurrentUsers.DataSource = _processUsers.FillDsJoined();
            CurrentUsers.DataBind();
        }

        protected void clsProcessUsers_SqlError(object sender, EventArgs e)
        {
            Response.Write(_processUsers.LastError);
        }

        protected void User_Remove(object sender, EventArgs e)
        {
            _processUsers.Id = (int.Parse(((ImageButton)sender).CommandArgument));
            _processUsers.DeleteRecord();
            CurrentUsers.DataSource = _processUsers.FillDsJoined();
            CurrentUsers.DataBind();
        }
    }
}