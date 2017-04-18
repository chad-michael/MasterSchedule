<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StatusBanner.ascx.cs" Inherits="Controls.Shared.Controls_Users_StatusBanner" %>
<asp:Panel ID="panelBannerBorder" runat="server" BorderStyle="Solid" BorderWidth="1px">
    <div style="padding: 5px;">
        <asp:Label ID="lblstatusBannerText" runat="server" Text='<%# Banner %>'></asp:Label>
    </div>
</asp:Panel>