<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProcessGroups.ascx.cs" Inherits="Users.ProcessGroups.Controls_ProcessGroups_ascx" EnableTheming="true" %>

<asp:Table ID="ProcessGroups_View" runat="server">
    <asp:TableRow>
        <asp:TableCell CssClass="FormInputLabels">
            <asp:Label ID="lblProcessGroupId" Text="Process Group Id" runat="server" />
        </asp:TableCell>
        <asp:TableCell CssClass="FormInputControls">

            <asp:TextBox ID='txtProcessGroupId' runat='server' />
        </asp:TableCell>
        <asp:TableCell CssClass="FormValidatorControls">
            <asp:RequiredFieldValidator runat="server" ID="requiredProcessGroupId" ControlToValidate="txtProcessGroupId"
                ErrorMessage="Process Group Id is required" Text="*" />
            <asp:CompareValidator Operator="datatypecheck" Type="integer"
                runat="server" ID="compareProcessGroupId" ControlToValidate="txtProcessGroupId"
                ErrorMessage="Process Group Id is not a valid integer." />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="FormInputLabels">
            <asp:Label ID="lblGroupName" Text="Group Name" runat="server" />
        </asp:TableCell>
        <asp:TableCell CssClass="FormInputControls">

            <asp:TextBox ID='txtGroupName' runat='server' />
        </asp:TableCell>
        <asp:TableCell CssClass="FormValidatorControls">
            <asp:CompareValidator Operator="datatypecheck" Type="string"
                runat="server" ID="compareGroupName" ControlToValidate="txtGroupName"
                ErrorMessage="Group Name is not a valid string." />
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell>
        </asp:TableCell>
        <asp:TableCell>
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>