using System;
using System.IO;

namespace Controls.Shared
{
    public partial class Controls_Users_HelpWindow : System.Web.UI.UserControl
    {
        public String BannerText
        {
            get { return BlackHeader1.BannerText; }
            set { BlackHeader1.BannerText = value; }
        }

        private string _helpFile;

        public string HelpFile
        {
            get { return _helpFile; }
            set { _helpFile = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (File.Exists(Server.MapPath("~/HelpFiles/" + HelpFile + ".help")))
            {
                StreamReader rdr = new StreamReader(File.OpenRead(Server.MapPath("~/HelpFiles/" + HelpFile + ".help")));
                lblHelpText.Text = rdr.ReadToEnd();

                rdr.Close();
                rdr.Dispose();
            }
        }
    }
}