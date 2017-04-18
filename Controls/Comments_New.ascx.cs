using System;

namespace Controls
{
    public partial class Comments_New : System.Web.UI.UserControl
    {
        public string Comments
        {
            get { return CommentsTextBox.Text; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}