using System;

namespace Controls.Shared
{
    public partial class Controls_Shared_FormSeperator : System.Web.UI.UserControl
    {
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

        public string BannerText
        {
            get { return BannerLabel.Text; }
            set { BannerLabel.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SetBannerBefore(object sender, EventArgs e)
        {
            this.bannerBefore.Text = BannerBefore;
        }

        protected void SetBannerAfter(object sender, EventArgs e)
        {
            this.bannerAfter.Text = BannerAfter;
        }
    }
}