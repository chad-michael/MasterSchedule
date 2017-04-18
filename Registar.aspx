<%@ Page Language="C#" Theme="Default" MasterPageFile="~/public/Default.master" 
AutoEventWireup="true" CodeFile="Registar.aspx.cs" Inherits="Registar" 
Title="Master Schedule - Registars Office" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:HiddenField runat="server" ID="hProcessName" />
<asp:HiddenField runat="server" ID="hStatusFilter" />

    <h1>Pending Schedule Changes - Registar Processing</h1>

    <br />
  <table>
    <tr>
        <td>
            Term:
        </td>
        <td>
            <asp:DropDownList ID="ddlTermFilter" runat="server" DataTextField="Term" DataValueField="Term">
            </asp:DropDownList>
        </td>
 
        <td>
            Submitted By:
        </td>
        <td>
            <asp:DropDownList ID="ddlSubmittedByFilter" runat="server" DataValueField="SubmittedBy" DataTextField="SubmittedBy">
            </asp:DropDownList>
        </td>

        <td>
            Course:
        </td>
        <td>
            <asp:DropDownList ID="ddlCourse" runat="server" DataValueField="CourseNumber" DataTextField="CourseNumber">
            </asp:DropDownList>
        </td>
     <td>
            Department: 
        </td>
        <td>
        <asp:DropDownList ID="ddlDepartmentFilter" runat="server" DataValueField="DepartmentCode" DataTextField="DepartmentCode">
            </asp:DropDownList>
        </td>
       
        <td>
            <asp:Button ID="btnFilter" runat="server" Text="Filter Results" 
                onclick="btnFilter_Click" />
        </td>
    </tr>
  </table>
  
    <asp:GridView Width="100%" SkinID="GridView" ID="gvNeedsApproval" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="LogID" OnRowCommand="gvNeedsApproval_RowCommand" 
    AllowPaging="False" AllowSorting="False">
        <Columns>
        <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="LOGID" runat="server" Value='<%#Eval("LogID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:BoundField DataField="DateSubmitted" HeaderText="Submitted" SortExpression="DateSubmitted" />
            <asp:BoundField DataField="SubmittedBy" HeaderText="By User" SortExpression="SubmittedBy" />
            <asp:BoundField DataField="UpdatedBy"    HeaderText="Approved By" SortExpression="UpdatedBy" />
            <asp:TemplateField HeaderText="Course">
                <ItemTemplate>
                    <asp:HyperLink style='<%#ViewCourseCss(Eval("SectionsID")) %>' ID="lnkViewCourse" runat="server" NavigateUrl='<%#Eval("SectionsID", "~/ViewSection.aspx?SectionsID={0}") %>' CssClass="magnifier"><%#Eval("Term") %>&nbsp;<%#Eval("CourseNumber") %>&nbsp;<%#Eval("SectionNumber") %></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Change" HeaderText="Change" SortExpression="Change" HtmlEncode="false" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnApprove" runat="server" CssClass="approve" >Process</asp:LinkButton>
               
               <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalbg"
                            CancelControlID="btnCloseConfirm2" DropShadow="true" PopupControlID="DivChairComments"
                            TargetControlID="btnApprove">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="DivChairComments" runat="server" CssClass="modalwindow" Width="300">
                            <asp:UpdatePanel ID="courseUpdateInfo" runat="server" >
                                <ContentTemplate>
                                    <asp:TextBox TextMode="MultiLine" ID="RegisCommentsTextBox" runat="server" Height="164px" Width="303px"></asp:TextBox>
            
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button runat="server" ID="btnApproveChange" 
                                CommandName="approve" CommandArgument='<%#Eval("LogID") %>' Text="Process" />
                            &nbsp;
                            <asp:Button ID="btnCloseConfirm2" runat="server" CausesValidation="false" Text="Cancel" />
                        </asp:Panel>
               
                </ItemTemplate>
            </asp:TemplateField><asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnIgnore" runat="server" CssClass="ignore" >Denied</asp:LinkButton>
                
                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalbg"
                            CancelControlID="btnCloseConfirm" DropShadow="true" PopupControlID="DivChairDeny"
                            TargetControlID="btnIgnore">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="DivChairDeny" runat="server" CssClass="modalwindow" Width="300">
                            <asp:UpdatePanel ID="DenialComments" runat="server" >
                                <ContentTemplate>
                                    <asp:TextBox TextMode="MultiLine" ID="RegistrarDenyTextBox" runat="server" Height="164px" Width="303px"></asp:TextBox>
                               
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button runat="server" ID="btnDenyChange" 
                                CommandName="ignore" CommandArgument='<%#Eval("LogID") %>' Text="Deny" />
                            &nbsp;
                            <asp:Button ID="btnCloseConfirm" runat="server" CausesValidation="false" Text="Cancel" />
                        </asp:Panel>
                
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            There are no pending changes that need your approval.
        </EmptyDataTemplate>
    </asp:GridView>
    <br />
      <%--<asp:SqlDataSource ID="SqlDataSourceNeedsApproval" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
        SelectCommand="ChangeLog_Fill_ChairByProcess" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="DeltaID" SessionField="deltaid" Type="String" />
            <asp:ControlParameter ControlID="hProcessName" Name="ProcessName" PropertyName="Value"
                Type="String" />
            <asp:ControlParameter ControlID="hStatusFilter" DefaultValue="pending" Name="Status"
                PropertyName="Value" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>--%>
    <asp:Panel ID="NoDataPanel" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#C00000"
        HorizontalAlign="Center" Visible="False">
        <br />
        Unable to load data. If you are not a registrar, you do not have access to this
        data.</asp:Panel>
</asp:Content>

