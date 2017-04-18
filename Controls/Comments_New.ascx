<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Comments_New.ascx.cs" Inherits="Controls.Comments_New" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdkey">Comments
                </td>
                <td colspan="2">
                    <asp:TextBox TextMode="MultiLine" ID="CommentsTextBox" runat="server" Height="182px" Width="460px" Rows="7"></asp:TextBox>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>