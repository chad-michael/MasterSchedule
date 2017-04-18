<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrentUsers.ascx.cs" Inherits="Users.Users.UsersUsersCurrentUsers" %>
<asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
<asp:GridView ID="CurrentUsers" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField HeaderText="User Name" DataField="UserId" SortExpression="UserId" />
        <asp:BoundField HeaderText="Process Name" DataField="ProcessName" SortExpression="ProcessName" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:ImageButton BorderStyle="none" ID="ImageButton1" OnClick="User_Remove" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/images/delete.png" runat="server"></asp:ImageButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        No rows selected
    </EmptyDataTemplate>
</asp:GridView>