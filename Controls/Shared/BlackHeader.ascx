<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlackHeader.ascx.cs" Inherits="Controls.Shared.Controls_Shared_BlackHeader" %>
<asp:Panel ID="BannerSection" CssClass="blackheader" runat="server">
    <%--    <asp:Literal ID="bannerBefore" runat="server" Text='<%# BannerBefore %>' />--%>
    <asp:Label ID="Banner" runat="server" ForeColor="#FFFFFF" Font-Size="12pt" Font-Bold="true"
        Text="Default Banner Content" />
    <%--    <asp:Literal ID="bannerAfter" runat="server" Text='<%# BannerAfter %>'></asp:Literal>
    --%>
</asp:Panel>