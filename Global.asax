<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Security.Principal" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        AddressBookManager manager = new AddressBookManager();
        manager.LoadFromDb();
    }

    void Application_End(object sender, EventArgs e)
    {

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        if (Context.User != null)
        {
            Session["deltaid"] = Context.User.Identity.Name.Substring(6);
        }

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends.
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer
        // or SQLServer, the event is not raised.

    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        // 5/27/15 Added to stop error requiring a / at the end with SSO
        if (Request.Path.EndsWith("e"))
        {
            Response.Redirect(Request.Path + "/");
        }

        // 7/7/2015 General Access to Application
        bool appAccess = false;
        try
        {
            string[] allowed = System.Configuration.ConfigurationManager.AppSettings["AppAccess"].Split(new char[] { '|' });
            for (int i = 0; i < allowed.Length; i++)
            {
                if (User.IsInRole(allowed[i].Trim()) || (User.Identity.Name.Substring(6).ToLower() == allowed[i].Trim().ToLower()))
                {
                    appAccess = true;
                }
            }

            if (!appAccess)
            {
                Response.StatusCode = 401;
                Response.End();
            }
        }
        catch (Exception ex)
        {
            return;
        }

        // 7/1/2015 Deny users not in role or have name listed in web.config appSetting "AdminAccess" access to managecampuscontacts.aspx page
        bool subdirectoryAccess = false;
        if (Request.Path.ToLower().Contains("users/managecampuscontacts.aspx"))
        {
            try
            {
                string[] allowed = System.Configuration.ConfigurationManager.AppSettings["AdminAccess"].Split(new char[] { '|' });
                for (int i = 0; i < allowed.Length; i++)
                {
                    if (User.IsInRole(allowed[i].Trim()) || (User.Identity.Name.Substring(6).ToLower() == allowed[i].Trim().ToLower()))
                    {
                        subdirectoryAccess = true;
                    }
                }

                if (!subdirectoryAccess)
                {
                    Response.StatusCode = 401;
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        // Get the Web application configuration.
        System.Configuration.Configuration configuration =
            System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(
            "/MasterSchedule");

        // Get the section.
        System.Web.Configuration.AuthenticationSection authenticationSection =
            (System.Web.Configuration.AuthenticationSection)configuration.GetSection(
            "system.web/authentication");

        System.Web.Configuration.AuthenticationMode currentmode = authenticationSection.Mode;
        if (currentmode == System.Web.Configuration.AuthenticationMode.Forms)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Context.Request.Cookies[cookieName];

            //// Extract the forms authentication cookie
            //string cookieName = FormsAuthentication.FormsCookieName;
            //HttpCookie authCookie = Context.Request.Cookies[cookieName];

            if (null == authCookie)
            {
                // There is no authentication cookie.
                return;
            }
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch (Exception ex)
            {
                // Log exception details (omitted for simplicity)
                throw new Exception("Error Reading cookie");
            }

            if (null == authTicket)
            {
                // Cookie failed to decrypt.
                return;
            }
            // When the ticket was created, the UserData property was assigned a
            // pipe delimited string of group names.

            String cacheGroups;
            String[] groups = null;
            if (!String.IsNullOrEmpty((string)authTicket.UserData))
            {

                groups = LoadRoles(new Guid(authTicket.UserData));
                // Create an Identity object

                GenericIdentity id = new GenericIdentity(authTicket.Name, "LdapAuthentication");

                // This principal will flow throughout the request.
                GenericPrincipal principal = new GenericPrincipal(id, groups);

                // Attach the new principal object to the current HttpContext object
                Context.User = principal;
            }
            else
            {

                GenericIdentity id = new GenericIdentity(authTicket.Name, "LdapAuthentication");
                // This principal will flow throughout the request.
                GenericPrincipal principal = new GenericPrincipal(id, new string[] { "Everyone", "Domain_Users" });

                // Attach the new principal object to the current HttpContext object
                Context.User = principal;
            }
        }
        else
        {
            ///// Commented out for ADFS to work
            //string[] username = Context.User.Identity.Name.Split(new string[] { "\\"},StringSplitOptions.RemoveEmptyEntries );
            //GenericIdentity id = new GenericIdentity(username[1], "LdapAuthentication");
            // This principal will flow throughout the request.
            //GenericPrincipal principal = new GenericPrincipal(id, new string[] { "Everyone", "Domain_Users" });

            // Attach the new principal object to the current HttpContext object
            //Context.User = principal;

        }

    }

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        //clsSecurity.CheckAccess();
    }

    protected string[] LoadRoles(Guid CacheId)
    {
        string roleString = "";
        string[] groups = null;
        if (HttpRuntime.Cache[CacheId.ToString()] == null)
        {
            try
            {
                System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(
                    Helpers.DeltaNetSql());

                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("GetCacheData", sqlCon);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CacheId", CacheId));
                command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Expires", DateTime.Now.AddMinutes(20)));

                sqlCon.Open();
                System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    roleString = reader.GetString(0);
                }

                groups = roleString.Split(new char[] { '|' });
                sqlCon.Close();
                sqlCon.Dispose();
                command.Dispose();

                HttpRuntime.Cache.Add(CacheId.ToString(), roleString,
                   null, DateTime.Now.AddMinutes(20), System.Web.Caching.Cache.NoSlidingExpiration,
                   System.Web.Caching.CacheItemPriority.Normal, null);

            }
            catch (System.Data.SqlClient.SqlException ex)
            {

            }
            finally
            {

            }
        }
        else
        {
            roleString = (string)HttpRuntime.Cache[CacheId.ToString()];
            groups = roleString.Split(new char[] { '|' });

        }

        return groups;

    }
</script>