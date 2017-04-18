<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Coreqs.ascx.cs" Inherits="Controls.Controls_Coreqs" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td>Action</td>
                <td>Department</td>
                <td>Course Number</td>
                <td>Section</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddAction" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddAction_SelectedIndexChanged">
                        <asp:ListItem Value="Add">Add</asp:ListItem>
                        <asp:ListItem Value="Drop">Drop</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtDepartment" runat="server" Width="150" ValidationGroup="coreqs"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtCourseNumber" runat="server" Width="150" ValidationGroup="coreqs"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtSectionNumber" runat="server" Width="100" ValidationGroup="coreqs"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Add Corequisite" OnClick="btnSubmit_Click" ValidationGroup="coreqs" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>