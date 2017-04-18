<%@ Page Language="C#" MasterPageFile="~/public/Default.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="AddUsers.aspx.cs" Inherits="Users.Users_AddUsers" Title="Untitled Page" %>

<%@ Register Src="Users/ProcessUsers.ascx" TagName="ProcessUsers" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span style="color: #0000ff; text-decoration: underline">
        <uc1:ProcessUsers ID="ProcessUsers1" runat="server" />
    </span>
</asp:Content>