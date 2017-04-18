<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProcessUsers.ascx.cs" Inherits="Users.Users.Controls_ProcessUsers_ascx" EnableTheming="true" %>

<table id="ProcessUsers_InsertView" runat="server">
    <tr>
        <td class="FormInputLabels">
            <asp:Label ID="lblProcessId" Text="Process Id" runat="server" />
        </td>
        <td class="FormInputControls">
            <asp:DropDownList runat="server"
                ID="ddlProcessId" OnLoad="ddlProcessId_Load">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="FormInputLabels">
            <asp:Label ID="lblUserId" Text="User Id" runat="server" />
        </td>
        <td class="FormInputControls">

            <asp:TextBox ID='txtUserId' Text='<%# Bind("UserId")%>' runat='server' />
        </td>
        <td class="FormValidatorControls">
            <asp:CompareValidator Operator="datatypecheck" Type="string"
                runat="server" ID="compareUserId" ControlToValidate="txtUserId"
                ErrorMessage="User Id is not a valid string." />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td></td>
        <td>
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
        </td>
    </tr>
</table>