<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Processes.ascx.cs" Inherits="Users.Processes.Controls_Processes_ascx" EnableTheming="true" %>

<asp:Table ID="Processes_View" runat="server">
    <asp:TableRow>
        <asp:TableCell CssClass="FormInputLabels">
            <asp:Label ID="lblProcessID" Text="Process ID" runat="server" />
        </asp:TableCell>
        <asp:TableCell CssClass="FormInputControls">

            <asp:TextBox ID='txtProcessID' runat='server' />
        </asp:TableCell>
        <asp:TableCell CssClass="FormValidatorControls">
            <asp:RequiredFieldValidator runat="server" ID="requiredProcessID" ControlToValidate="txtProcessID"
                ErrorMessage="Process ID is required" Text="*" />
            <asp:CompareValidator Operator="datatypecheck" Type="integer"
                runat="server" ID="compareProcessID" ControlToValidate="txtProcessID"
                ErrorMessage="Process ID is not a valid integer." />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="FormInputLabels">
            <asp:Label ID="lblProcessName" Text="Process Name" runat="server" />
        </asp:TableCell>
        <asp:TableCell CssClass="FormInputControls">

            <asp:TextBox ID='txtProcessName' runat='server' />
        </asp:TableCell>
        <asp:TableCell CssClass="FormValidatorControls">
            <asp:CompareValidator Operator="datatypecheck" Type="string"
                runat="server" ID="compareProcessName" ControlToValidate="txtProcessName"
                ErrorMessage="Process Name is not a valid string." />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell CssClass="FormInputLabels">
            <asp:Label ID="lblProcessGroup" Text="Process Group" runat="server" />
        </asp:TableCell>
        <asp:TableCell CssClass="FormInputControls">

            <asp:TextBox ID='txtProcessGroup' runat='server' />
        </asp:TableCell>
        <asp:TableCell CssClass="FormValidatorControls">
            <asp:CompareValidator Operator="datatypecheck" Type="integer"
                runat="server" ID="compareProcessGroup" ControlToValidate="txtProcessGroup"
                ErrorMessage="Process Group is not a valid integer." />
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