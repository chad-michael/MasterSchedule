using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ComponentArt.Web.UI;

namespace Shared.Controls
{
    public partial class Controls_LSBGroupContainer : UserControl, ILeftSideBarUserControl
    {
        private bool _dataBound;

        public string Header
        {
            get { return lblHeader.Text; }
            set { lblHeader.Text = value; }
        }

        private DataSet _dataSet = null;

        public DataSet DataSet
        {
            get
            {
                if (_dataSet == null)
                {
                    _dataSet = new DataSet();
                }
                return _dataSet;
            }
            set { _dataSet = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && !_dataBound)
            {
                DataBindMenu();
            }
        }

        public void DataBindMenu()
        {
            if (_dataSet != null)
            {
                Menu1.DataSource = _dataSet;
                Menu1.DataBind();
                _dataBound = true;
            }
        }
    }

    public interface ILeftSideBarUserControl
    {
    }
}