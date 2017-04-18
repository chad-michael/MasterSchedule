<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HelpWindow.ascx.cs" Inherits="Controls.Shared.Controls_Users_HelpWindow" %>
<%@ Register Src="../Shared/BlackHeader.ascx" TagName="BlackHeader" TagPrefix="uc1" %>
<div class="UserControl">
    <uc1:BlackHeader ID="BlackHeader1" BannerText="Help" runat="server" />
    <asp:Label ID="lblHelpText" runat="server" Text=""></asp:Label>
</div>