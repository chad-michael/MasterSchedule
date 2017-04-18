using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.Controls
{
    public partial class _themes_DeltaCollege_Controls_LandingPage_Spotlight : System.Web.UI.UserControl
    {
        public class SpotlightControl : CompositeControl
        {
            // Fields
            private Image imgSpotlightImage;

            private Literal lblBio;
            private HyperLink lnkReadMore;
            private Literal ltHeaderText;
            private Panel pnlClearFix;
            private Panel pnlOuterPanel;

            // Methods
            public SpotlightControl()
            {
            }

            internal SpotlightControl(string header, string text, string imageUrl, string linkUrl)
            {
                this.Header = header;
                this.Text = text;
                this.ImageUrl = imageUrl;
                this.LinkUrl = linkUrl;
            }

            protected override void CreateChildControls()
            {
                this.pnlOuterPanel = new Panel();
                Literal literal = new Literal();
                literal.Text = "<h3 class=\"h3header\">" + this.Header + "</h3>";
                this.ltHeaderText = literal;
                this.pnlOuterPanel.Controls.Add(this.ltHeaderText);
                this.imgSpotlightImage = new Image();
                this.imgSpotlightImage.ImageUrl = this.ImageUrl;
                this.pnlOuterPanel.Controls.Add(this.imgSpotlightImage);
                this.lblBio = new Literal();
                this.lblBio.Text = "<p>" + this.Text + "</p>";
                this.pnlOuterPanel.Controls.Add(this.lblBio);
                this.lnkReadMore = new HyperLink();
                this.lnkReadMore.Text = "Read More";
                this.lnkReadMore.NavigateUrl = this.LinkUrl;
                this.pnlOuterPanel.Controls.Add(this.lnkReadMore);
                this.pnlClearFix = new Panel();
                this.pnlOuterPanel.Controls.Add(this.pnlClearFix);
                this.Controls.Add(this.pnlOuterPanel);
            }

            protected virtual void PrepareControlForRendering()
            {
                this.pnlOuterPanel.CssClass = "spotlightitem";
                this.pnlClearFix.CssClass = "clearfix";
            }

            protected override void Render(HtmlTextWriter writer)
            {
                this.PrepareControlForRendering();
                base.RenderContents(writer);
            }

            // Properties
            public string Header
            {
                get
                {
                    object obj2 = this.ViewState["Header"];
                    if (obj2 == null)
                    {
                        return string.Empty;
                    }
                    return (string)obj2;
                }
                set
                {
                    this.ViewState["Header"] = value;
                }
            }

            public string ImageUrl
            {
                get
                {
                    object obj2 = this.ViewState["ImageUrl"];
                    if (obj2 == null)
                    {
                        return string.Empty;
                    }
                    return (string)obj2;
                }
                set
                {
                    this.ViewState["ImageUrl"] = value;
                }
            }

            public string LinkUrl
            {
                get
                {
                    object obj2 = this.ViewState["LinkUrl"];
                    if (obj2 == null)
                    {
                        return string.Empty;
                    }
                    return (string)obj2;
                }
                set
                {
                    this.ViewState["LinkUrl"] = value;
                }
            }

            public string Text
            {
                get
                {
                    object obj2 = this.ViewState["Text"];
                    if (obj2 == null)
                    {
                        return string.Empty;
                    }
                    return (string)obj2;
                }
                set
                {
                    this.ViewState["Text"] = this.Context.Server.HtmlEncode(value);
                }
            }
        }

        public class SpotlightList : CompositeDataBoundControl
        {
            // Methods
            protected override void CreateChildControls()
            {
                this.Controls.Clear();
                object obj2 = this.ViewState["_!ItemCount"];
                if (obj2 != null)
                {
                    object[] dataSource = new object[(int)obj2];
                    this.CreateChildControls(dataSource, false);
                    base.ChildControlsCreated = true;
                }
            }

            protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
            {
                return this.CreateControlHierarchy(dataSource, dataBinding);
            }

            private int CreateControlHierarchy(IEnumerable dataSource, bool dataBinding)
            {
                int num = 0;
                foreach (object obj2 in dataSource)
                {
                    if (dataBinding)
                    {
                        this.Controls.Add(this.CreateSpotlight(obj2));
                    }
                    else
                    {
                        this.Controls.Add(this.CreateSpotlight());
                    }
                    num++;
                }
                return num;
            }

            private SpotlightControl CreateSpotlight()
            {
                return new SpotlightControl();
            }

            private SpotlightControl CreateSpotlight(object dataItem)
            {
                if (string.IsNullOrEmpty(this.DataValueField))
                {
                    this.ThrowInvalidDataException("DataValueField");
                }
                if (string.IsNullOrEmpty(this.DataHeaderField))
                {
                    this.ThrowInvalidDataException("DataHeaderField");
                }
                if (string.IsNullOrEmpty(this.DataBioTextField))
                {
                    this.ThrowInvalidDataException("DataBioTextField");
                }
                if (string.IsNullOrEmpty(this.DataImageUrlField))
                {
                    this.ThrowInvalidDataException("DataImageUrlField");
                }
                if (string.IsNullOrEmpty(this.DataRelatedUrlField) && string.IsNullOrEmpty(this.DetailDisplayUrl))
                {
                    throw new Exception("Either DataRelatedUrlField or DetailDisplayUrl should be set");
                }
                if (string.IsNullOrEmpty(this.DataRelatedUrlField) && string.IsNullOrEmpty(this.DataValueField))
                {
                    throw new Exception("You must specify the DataValueField when the DataRelatedUrlField is not set");
                }
                string detailUrl = string.Empty;
                detailUrl = this.DetailDisplayUrl + "?" + this.DetailDisplayUrlQueryStringField + "=" + DataBinder.GetPropertyValue(dataItem, this.DataValueField).ToString();
                if (string.IsNullOrEmpty(this.DataRelatedUrlField))
                {
                    detailUrl = this.ExtractDetailLink(dataItem, detailUrl);
                }
                else
                {
                    object obj2 = DataBinder.GetPropertyValue(dataItem, this.DataRelatedUrlField);
                    if (obj2 == null)
                    {
                        detailUrl = this.DetailDisplayUrl + "?" + this.DetailDisplayUrlQueryStringField + "=" + DataBinder.GetPropertyValue(dataItem, this.DataValueField).ToString();
                    }
                    else
                    {
                        string str2 = (string)obj2;
                        if (string.IsNullOrEmpty(str2))
                        {
                            detailUrl = this.DetailDisplayUrl + "?" + this.DetailDisplayUrlQueryStringField + "=" + DataBinder.GetPropertyValue(dataItem, this.DataValueField).ToString();
                        }
                        else
                        {
                            detailUrl = (string)DataBinder.GetPropertyValue(dataItem, this.DataRelatedUrlField);
                        }
                    }
                }
                string propertyValue = (string)DataBinder.GetPropertyValue(dataItem, this.DataBioTextField);
                string header = (string)DataBinder.GetPropertyValue(dataItem, this.DataHeaderField);
                return new SpotlightControl(header, propertyValue, (string)DataBinder.GetPropertyValue(dataItem, this.DataImageUrlField), detailUrl);
            }

            private string ExtractDetailLink(object dataItem, string detailUrl)
            {
                detailUrl = this.DetailDisplayUrl + "?" + this.DetailDisplayUrlQueryStringField + "=" + DataBinder.GetPropertyValue(dataItem, this.DataValueField).ToString();
                return detailUrl;
            }

            private void ThrowInvalidDataException(string field)
            {
                throw new Exception(field + " can not be null");
            }

            // Properties
            public string DataBioTextField
            {
                get
                {
                    object obj2 = this.ViewState["DataBioTextField"];
                    if (obj2 == null)
                    {
                        return string.Empty;
                    }
                    return (string)obj2;
                }
                set
                {
                    this.ViewState["DataBioTextField"] = value;
                }
            }

            public string DataHeaderField
            {
                get
                {
                    object obj2 = this.ViewState["DataHeaderField"];
                    if (obj2 == null)
                    {
                        return string.Empty;
                    }
                    return (string)obj2;
                }
                set
                {
                    this.ViewState["DataHeaderField"] = value;
                }
            }

            public string DataImageUrlField
            {
                get
                {
                    object obj2 = this.ViewState["DataImageUrlField"];
                    if (obj2 == null)
                    {
                        return string.Empty;
                    }
                    return (string)obj2;
                }
                set
                {
                    this.ViewState["DataImageUrlField"] = value;
                }
            }

            public string DataRelatedUrlField
            {
                get
                {
                    object obj2 = this.ViewState["DataRelatedUrlField"];
                    if (obj2 == null)
                    {
                        return string.Empty;
                    }
                    return (string)obj2;
                }
                set
                {
                    this.ViewState["DataRelatedUrlField"] = value;
                }
            }

            public string DataValueField
            {
                get
                {
                    object obj2 = this.ViewState["DataValueField"];
                    if (obj2 == null)
                    {
                        return string.Empty;
                    }
                    return (string)obj2;
                }
                set
                {
                    this.ViewState["DataValueField"] = value;
                }
            }

            public string DetailDisplayUrl
            {
                get
                {
                    object obj2 = this.ViewState["DetailDisplayUrl"];
                    if (obj2 == null)
                    {
                        return string.Empty;
                    }
                    return (string)obj2;
                }
                set
                {
                    this.ViewState["DetailDisplayUrl"] = value;
                }
            }

            public string DetailDisplayUrlQueryStringField
            {
                get
                {
                    object obj2 = this.ViewState["DetailDisplayUrlQueryStringField"];
                    if (obj2 == null)
                    {
                        return string.Empty;
                    }
                    return (string)obj2;
                }
                set
                {
                    this.ViewState["DetailDisplayUrlQueryStringField"] = value;
                }
            }
        }

        public string LandingPageName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LandingPageName != null)
            {
                if (!Page.IsPostBack)
                {
                    SpotlightList sptList = new SpotlightList();
                    pcSpotlightList.Controls.Add(sptList);
                    sptList.DataSource = LoadRsbSpotlights(this.LandingPageName);
                    //Set up the columns needed.
                    sptList.DataValueField = "ItemID";
                    sptList.DataBioTextField = "teasertext";
                    sptList.DataRelatedUrlField = "LocationURL";
                    sptList.DataImageUrlField = "CustomVC256_1";
                    sptList.DataHeaderField = "title";

                    //Set some defaults in case the URL is empty.
                    sptList.DetailDisplayUrlQueryStringField = "itemid";
                    sptList.DetailDisplayUrl = "http://www.delta.edu/internal/promotions.aspx";

                    //Actually Databind the control.
                    sptList.DataBind();
                }
            }
        }

        public static DataTable LoadRsbSpotlights(string landingPageName)
        {
            List<LandingPageLsbContainer> containers = new List<LandingPageLsbContainer>();
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SB_Public_08ConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand("ChannelData_SpotlightGet", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@LandingPageName", landingPageName));

            SqlDataReader reader = null;
            DataTable results = new DataTable();
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                results.Load(reader);
            }
            catch (SqlException ex)
            {
                //Do something with the exception.
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
                command.Dispose();
            }
            return results;
        }
    }
}