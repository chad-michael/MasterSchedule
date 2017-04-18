using System;

namespace Controls.Shared
{
    public partial class Controls_Users_StatusBanner : System.Web.UI.UserControl
    {
        private string _status;

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                if (_status == "Issues")
                {
                    panelBannerBorder.BorderColor = System.Drawing.Color.Red;
                }
                else if (_status == "Not Tested")
                {
                    panelBannerBorder.BorderColor = System.Drawing.Color.Yellow;
                }
                else if (_status == "Approved")
                {
                    panelBannerBorder.BorderColor = System.Drawing.Color.Green;
                }
                else
                {
                    panelBannerBorder.BorderColor = System.Drawing.Color.DarkGray;
                }
            }
        }

        private string _banner;

        public string Banner
        {
            get { return "Current task status is <strong>" + _banner + "</strong>"; }
            set
            {
                _banner = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}