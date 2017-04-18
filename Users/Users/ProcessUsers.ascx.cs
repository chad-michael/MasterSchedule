using System;
using System.Web.UI.WebControls;

namespace Users.Users
{
    public partial class Controls_ProcessUsers_ascx : System.Web.UI.UserControl
    {
        private ClsProcessUsers _ProcessUsers;

        //Make sure to have an instance of clsProcesses
        private ClsProcesses _instanceProcesses;

        #region "Public Fields"

        public String UserId
        {
            get { return Convert.ToString(txtUserId.Text); }
            set { txtUserId.Text = value.ToString(); }
        }

        #endregion "Public Fields"

        #region "Public Foreign Key Fields"

        public Int32 ProcessId
        {
            get { return Convert.ToInt32(ddlProcessId.SelectedValue); }
            set { ddlProcessId.SelectedValue = value.ToString(); }
        }

        #endregion "Public Foreign Key Fields"

        protected void Page_Load(object sender, EventArgs e)
        {
            this._ProcessUsers = new ClsProcessUsers("MasterSchedule");
            _ProcessUsers.SqlError += new ClsProcessUsers.SqlErrorEventHandler(clsProcessUsers_SqlError);

            _instanceProcesses = new ClsProcesses("MasterSchedule");
        }

        protected void clsProcessUsers_SqlError(object sender, EventArgs e)
        {
            //Do something with the exception here.
        }

        /// <summary>
        /// Gets the data for the ddlProcessId.
        /// </summary>
        /// <remarks>
        /// Change the data text field to display other data from the database.
        ///</remarks>
        protected void ddlProcessId_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DropDownList list = sender as DropDownList;
                //Bind up the list for ddlProcessId
                list.DataSource = _instanceProcesses.FillDs();
                list.DataValueField = "ProcessID";
                list.DataTextField = "ProcessName";
                list.DataBind();
            }
        }

        protected void Form_DataBind()
        {
            //ID = _ProcessUsers.ID;
            ProcessId = _ProcessUsers.ProcessId;
            UserId = _ProcessUsers.UserId;
        }

        protected void UpdateFromForm()
        {
            //_ProcessUsers.ID = ID;
            _ProcessUsers.ProcessId = ProcessId;
            _ProcessUsers.UserId = UserId;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            _ProcessUsers.ProcessId = ProcessId;
            _ProcessUsers.UserId = UserId;
            _ProcessUsers.AddRecord();
        }
    }
}