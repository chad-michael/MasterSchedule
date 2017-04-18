using System;

namespace Users.Processes
{
    public partial class Controls_Processes_ascx : System.Web.UI.UserControl
    {
        private ClsProcesses _Processes;

        #region "Public Fields"

        public Int32 ProcessID
        {
            get { return Convert.ToInt32(txtProcessID.Text); }
            set { txtProcessID.Text = value.ToString(); }
        }

        public String ProcessName
        {
            get { return Convert.ToString(txtProcessName.Text); }
            set { txtProcessName.Text = value.ToString(); }
        }

        public Int32 ProcessGroup
        {
            get { return Convert.ToInt32(txtProcessGroup.Text); }
            set { txtProcessGroup.Text = value.ToString(); }
        }

        #endregion "Public Fields"

        protected void Page_Load(object sender, EventArgs e)
        {
            this._Processes = new ClsProcesses("MasterSchedule");
            _Processes.SqlError += new ClsProcesses.SqlErrorEventHandler(clsProcesses_SqlError);
        }

        protected void clsProcesses_SqlError(object sender, EventArgs e)
        {
            //Do something with the exception here.
        }

        protected void Form_DataBind()
        {
            ProcessID = _Processes.ProcessId;
            ProcessName = _Processes.ProcessName;
            ProcessGroup = _Processes.ProcessGroup;
        }

        protected void UpdateFromForm()
        {
            _Processes.ProcessId = ProcessID;
            _Processes.ProcessName = ProcessName;
            _Processes.ProcessGroup = ProcessGroup;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
        }
    }
}