<%@ Page Language="C#" MasterPageFile="~/public/Default.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Default.aspx.cs" Inherits="Users.Users_Default" Title="Untitled Page" %>

<%@ Register Src="Users/CurrentUsers.ascx" TagName="CurrentUsers" TagPrefix="uc2" %>

<%@ Register Src="Users/ProcessUsers.ascx" TagName="ProcessUsers" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Add Users">
            <ContentTemplate>
                <uc1:ProcessUsers ID="ProcessUsers1" runat="server" />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Delete Users">
            <ContentTemplate>
                <uc2:CurrentUsers ID="CurrentUsers1" runat="server" />
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>