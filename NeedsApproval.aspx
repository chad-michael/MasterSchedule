<%@ Page Language="C#" Theme="Default" MasterPageFile="~/public/Default.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="NeedsApproval.aspx.cs" Inherits="NeedsApproval"
    Title="Master Schedule - Division Chair Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        //script here.
    </script>

    <h1>Pending Schedule Changes</h1>
    <div id="hidethis">
        <asp:GridView Width="100%" SkinID="GridView" ID="gvNeedsApproval" runat="server"
            AutoGenerateColumns="False" DataKeyNames="LogID" DataSourceID="SqlDataSourceNeedsApproval"
            OnRowCommand="gvNeedsApproval_RowCommand" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="LOGID" runat="server" Value='<%#Eval("LogID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DateSubmitted" HeaderText="Submitted" SortExpression="DateSubmitted" />
                <asp:BoundField DataField="SubmittedBy" HeaderText="By User" SortExpression="SubmittedBy" />
                <asp:TemplateField HeaderText="Course">
                    <ItemTemplate>
                        <asp:HyperLink Style='<%#ViewCourseCss(Eval("SectionsID")) %>' ID="lnkViewCourse"
                            runat="server" NavigateUrl='<%#Eval("SectionsID", "~/ViewSection.aspx?SectionsID={0}") %>'
                            CssClass="magnifier"><%#Eval("Term") %>&nbsp;<%#Eval("CourseNumber") %>&nbsp;<%#Eval("SectionNumber") %></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Change" HeaderText="Change" SortExpression="Change" HtmlEncode="false" />
                <asp:TemplateField ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# String.Format("~/ChangeEdit.aspx?changelogid={0}&returnurl=NeedsApproval.aspx", Eval("LogID")) %>'
                            runat="server">
                            Edit
                            <asp:Image ID="Image1" ImageUrl="~/images/application_edit.png" BorderStyle="none"
                                runat="server" />
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnApprove" runat="server" CssClass="approve">Approve</asp:LinkButton>

                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalbg"
                            CancelControlID="btnCloseConfirm" DropShadow="true" PopupControlID="DivChairComments"
                            TargetControlID="btnApprove">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="DivChairComments" runat="server" CssClass="modalwindow" Width="300">
                            <asp:UpdatePanel ID="courseUpdateInfo" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox TextMode="MultiLine" ID="DivChairCommentsTextBox" runat="server" Height="164px" Width="303px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button runat="server" ID="btnApproveChange"
                                CommandName="approve" CommandArgument='<%#Eval("LogID") %>' Text="Approve" />
                            &nbsp;
                            <asp:Button ID="btnCloseConfirm" runat="server" CausesValidation="false" Text="Cancel" />
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnIgnore" runat="server" CssClass="ignore">Deny</asp:LinkButton>

                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalbg"
                            CancelControlID="btnCloseConfirm2" DropShadow="true" PopupControlID="DivChairDeny"
                            TargetControlID="btnIgnore">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="DivChairDeny" runat="server" CssClass="modalwindow" Width="300">
                            <asp:UpdatePanel ID="DenialComments" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox TextMode="MultiLine" ID="DivChairDenyTextBox" runat="server" Height="164px" Width="303px"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button runat="server" ID="btnDenyChange"
                                CommandName="ignore" CommandArgument='<%#Eval("LogID") %>' Text="Deny" />
                            &nbsp;
                            <asp:Button ID="btnCloseConfirm2" runat="server" CausesValidation="false" Text="Cancel" />
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                There are no pending changes that need your approval.
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <asp:SqlDataSource ID="SqlDataSourceNeedsApproval" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
        SelectCommand="ChangeLog_Fill_ChairByStatus" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="DeltaID" SessionField="deltaid" Type="String" />
            <asp:Parameter DefaultValue="pending" Name="Status" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>