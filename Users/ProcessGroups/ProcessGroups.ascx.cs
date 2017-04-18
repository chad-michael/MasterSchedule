using System;

namespace Users.ProcessGroups
{
    public partial class Controls_ProcessGroups_ascx : System.Web.UI.UserControl
    {
        private ClsProcessGroups _ProcessGroups;

        #region "Public Fields"

        public Int32 ProcessGroupId
        {
            get { return Convert.ToInt32(txtProcessGroupId.Text); }
            set { txtProcessGroupId.Text = value.ToString(); }
        }

        public String GroupName
        {
            get { return Convert.ToString(txtGroupName.Text); }
            set { txtGroupName.Text = value.ToString(); }
        }

        #endregion "Public Fields"

        protected void Page_Load(object sender, EventArgs e)
        {
            this._ProcessGroups = new ClsProcessGroups("MasterSchedule");
            _ProcessGroups.SqlError += new ClsProcessGroups.SqlErrorEventHandler(clsProcessGroups_SqlError);
        }

        protected void clsProcessGroups_SqlError(object sender, EventArgs e)
        {
            //Do something with the exception here.
        }

        protected void Form_DataBind()
        {
            ProcessGroupId = _ProcessGroups.ProcessGroupId;
            GroupName = _ProcessGroups.GroupName;
        }

        protected void UpdateFromForm()
        {
            _ProcessGroups.ProcessGroupId = ProcessGroupId;
            _ProcessGroups.GroupName = GroupName;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
        }
    }
}