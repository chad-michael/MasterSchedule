<%@ Page Language="C#" MasterPageFile="~/public/Default.master" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false"
    CodeFile="ChangeEdit.aspx.cs" Inherits="ChangeEdit" Title="Change Log - Edit" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <asp:HiddenField ID="hiddenObjectId" runat="server" />
    <h1>Edit change
    </h1>
    <asp:FormView ID="ContentPlaceholder1" DefaultMode="edit" runat="server" DataKeyNames="LogId" DataSourceID="FrontPageContent1"
        AllowPaging="True">
        <EditItemTemplate>
            <asp:Label ID="WidgetIDLabel1" runat="server" Text='<%# Eval("LogId") %>' Visible="False"></asp:Label><br />
            <table>
                <tr>

                    <td>
                        <FCKeditorV2:FCKeditor Width="500px" Height="300px" ID="FCKeditor1" Value='<%# Bind("Change") %>' ToolbarSet="Basic" runat="server" BasePath="/fckeditor/" AutoDetectLanguage="false">
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                Text="Save Updates"></asp:Button>

            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel Update"></asp:Button>

            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" /><br />
        </EditItemTemplate>
        <ItemTemplate>
            <h4>
                <asp:Label ID="BannerLabel" runat="server" Text='<%# Bind("Banner") %>'></asp:Label><br />
            </h4>
            <p>
                <asp:Label ID="ContentLabel" runat="server" Text='<%# Bind("Content") %>'></asp:Label><br />
            </p>
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                Text="Edit"></asp:LinkButton>
            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                Text="Delete"></asp:LinkButton>
            <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                Text="New"></asp:LinkButton>
        </ItemTemplate>
        <EmptyDataTemplate>
            <asp:Button ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                Text="New"></asp:Button>
        </EmptyDataTemplate>
    </asp:FormView>
    <hr />
    <asp:SqlDataSource ID="FrontPageContent1" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
        SelectCommand="SELECT * FROM [ChangeLog] Where LogID = @LogID"
        UpdateCommand="UPDATE [ChangeLog] SET [Change] = @Change WHERE [LogID] = @LogID">
        <SelectParameters>
            <asp:ControlParameter ControlID="hiddenObjectId" Type="int32" Name="LogID"
                DefaultValue="0" PropertyName="value" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="LogID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Change" Type="String" />
            <asp:Parameter Name="LogID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>