using System;

namespace Controls.Shared
{
    public partial class Controls_Shared_BlackHeader : System.Web.UI.UserControl
    {
        public string BannerText
        {
            get { return Banner.Text; }
            set { Banner.Text = value; }
        }

        private HeaderStyle _headingStyle = HeaderStyle.H4;

        public HeaderStyle HeadingStyle
        {
            get { return _headingStyle; }
            set { _headingStyle = value; }
        }

        protected string BannerBefore
        {
            get
            {
                return "<" + Enum.GetName(typeof(HeaderStyle), _headingStyle) + ">";
            }
        }

        protected string BannerAfter
        {
            get
            {
                return "</" + Enum.GetName(typeof(HeaderStyle), _headingStyle) + ">";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}