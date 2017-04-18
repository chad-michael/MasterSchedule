<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteWidget.ascx.cs" Inherits="Controls.Shared.Controls_Shared_SiteWidget" %>
<%@ Register Src="BlackHeader.ascx" TagName="BlackHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:HiddenField ID="hiddenObjectId" runat="server" />
<asp:Panel CssClass='<%# CssClass %>' runat="server" ID="WidgetWrapper">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:FormView ID="ContentPlaceholder1ReadOnly" runat="server" DataKeyNames="LogId"
                DataSourceID="FrontPageContent1" AllowPaging="True">
                <ItemTemplate>
                    <asp:Literal ID="bannerBefore" runat="server" Text='<%# BannerBefore %>' />
                    <asp:Label ID="BannerLabel" runat="server" Text='Edit Master Schedule Form'></asp:Label><br />
                    <asp:Literal ID="bannerAfter" runat="server" Text='<%# BannerAfter %>'></asp:Literal>
                    <p>
                        <asp:Label ID="ContentLabel" runat="server" Text='<%# Bind("Change") %>'></asp:Label><br />
                    </p>
                </ItemTemplate>
            </asp:FormView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCloseWindow" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:LinkButton ID="btnStartEdit" runat="server">
        <asp:Image ID="Image1" runat="server" AlternateText="Edit" ImageUrl="~/Images/application_edit.png" />
        Edit
    </asp:LinkButton>&nbsp;&nbsp;&nbsp;
</asp:Panel>
<cc1:ModalPopupExtender Enabled='<%# IsDesignMode %>' ID="ModalPopupExtender1" BackgroundCssClass="modalbg"
    runat="server" OkControlID="btnCloseWindow" PopupControlID="PlaceHolderPanel"
    TargetControlID="btnStartEdit">
</cc1:ModalPopupExtender>

<asp:Panel ID="PlaceHolderPanel" Visible='<%# IsDesignMode %>' runat="server" CssClass="modalwindow">
    <asp:UpdatePanel ID="PlaceHolderUpdatePanel" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <uc1:BlackHeader ID="BlackHeader1" runat="server" BannerText="Modify Content Area" />
            <br />
            <asp:FormView ID="ContentPlaceholder1" DefaultMode="edit" runat="server" DataKeyNames="LogId" DataSourceID="FrontPageContent1"
                AllowPaging="True">
                <EditItemTemplate>
                    <asp:Label ID="WidgetIDLabel1" runat="server" Text='<%# Eval("LogId") %>' Visible="False"></asp:Label><br />
                    <table>
                        <tr>
                            <td>Change:
                            </td>
                            <td>
                                <FCKeditorV2:FCKeditor Width="500px" Height="300px" ID="FCKeditor1" Value='<%# Bind("Change") %>' ToolbarSet="Basic" runat="server" BasePath="/fckeditor/" AutoDetectLanguage="false">
                                </FCKeditorV2:FCKeditor>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update">
                    </asp:LinkButton>
                    <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel">
                    </asp:LinkButton>
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
                    <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                        Text="New"></asp:LinkButton>
                </EmptyDataTemplate>
            </asp:FormView>
            <hr />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btnCloseWindow" runat="server" Text="Close Window" />
</asp:Panel>
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