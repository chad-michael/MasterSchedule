<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FormSeperator.ascx.cs" Inherits="Controls.Shared.Controls_Shared_FormSeperator" %>
<asp:Panel ID="BannerSection" SkinID="FormSeperatorContainer" runat="server">
    <asp:Literal ID="bannerBefore" runat="server" OnLoad="SetBannerBefore" />
    <asp:Label ID="BannerLabel" runat="server"></asp:Label><br />
    <asp:Literal ID="bannerAfter" runat="server" OnLoad="SetBannerAfter"></asp:Literal>
</asp:Panel>