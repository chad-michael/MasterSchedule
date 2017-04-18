<%@ Page Language="C#" Title="Logon" MasterPageFile="~/MasterPages/AppCustom.master" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" %>

<asp:Content runat='server' ContentPlaceHolderID="contentMain">
    <table width="100%">
        <tr>
            <td valign="middle" align="center">
                <br />
                <br />
                <table border="2" cellpadding="5" cellspacing="0" width="200">
                    <tr bgcolor="gray">
                        <td colspan="2"><b>Please Login</b><br />
                            <asp:Label ID="lblError" runat="server" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td>Username:</td>
                        <td>
                            <asp:TextBox Width="150" ID="txtUserName" runat="server" />
                            <asp:TextBox ID="TextBox2" runat="server" Style="visibility: hidden; display: none;" /></td>
                    </tr>
                    <tr>
                        <td>Password:</td>
                        <td>
                            <asp:TextBox Width="150" ID="txtPassword" runat="server" TextMode="Password" />
                            <asp:TextBox ID="TextBox1" runat="server" Style="visibility: hidden; display: none;" /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnLogon" runat="server" Text="Log On" OnClick="btnLogon_Click" UseSubmitBehavior="true" TabIndex="0" /></td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>