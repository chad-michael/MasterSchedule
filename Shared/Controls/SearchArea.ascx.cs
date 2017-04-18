using System;
using System.Web;
using System.Web.UI;

namespace Shared.Controls
{
    public partial class Controls_SearchArea : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack && !string.IsNullOrEmpty(searchbox.Value) &&
                !(searchbox.Value.Equals("Search...", StringComparison.InvariantCultureIgnoreCase)))
            {
                sa_Click(null, null);
            }
        }

        protected void sa_Click(object sender, ImageClickEventArgs e)
        {
            string pageId = "-1";
            if (Request.QueryString["pageid"] != null)
            {
                pageId = Request.QueryString["pageid"];
            }
            else
            {
                pageId = Request.FilePath;
            }
            Response.Redirect(
                String.Format("http://www.delta.edu/internal/SearchResults.aspx?q={0}&cx={1}&cof={2}&source={3}#976",
                    HttpUtility.UrlEncode(searchbox.Value),
                    HttpUtility.UrlEncode("002965079154023670929:hyqgl5a5it0"), HttpUtility.UrlEncode("FORID:11"),
                    pageId), false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}