using System;

namespace Controls.Shared
{
    public partial class Controls_Shared_SiteWidget : System.Web.UI.UserControl
    {
        private string _cssClassDefault = "SiteWidget";

        public string CssClass
        {
            get { return WidgetWrapper.CssClass; }
            set
            {
                _cssClassDefault = value;
                WidgetWrapper.CssClass = value;
            }
        }

        public string ObjectId
        {
            get { return hiddenObjectId.Value; }
            set { hiddenObjectId.Value = value; }
        }

        private bool _showImage;

        public bool ShowImage
        {
            get { return _showImage; }
            set { _showImage = value; }
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
            if (!IsDesignMode) { btnStartEdit.Text = ""; }
            WidgetWrapper.CssClass = _cssClassDefault;
        }

        public bool IsDesignMode
        {
            get
            {
                bool ret = false;
                if (Page.User.IsInRole("Administrators"))
                {
                    if (Request.QueryString["designmode"] != null)
                    {
                        ret = Request.QueryString["designmode"] == "true";
                    }
                }
                return true;
            }
        }
    }
}