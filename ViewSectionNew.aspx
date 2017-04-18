<%@ Page Language="C#" Theme="Default" MasterPageFile="~/public/Default.master" AutoEventWireup="true" CodeFile="ViewSectionNew.aspx.cs" Inherits="ViewSectionNew
" Title="Untitled Page" %>

<%@ Register Src="~/Controls/SectionDetails.ascx" TagName="SectionDetails" TagPrefix="ms" %>
<%@ Register Src="~/Controls/CrossLinked.ascx" TagName="CrossLinked" TagPrefix="ms" %>
<%@ Register Src="~/Controls/Coreqs.ascx" TagName="Coreqs" TagPrefix="ms" %>
<%@ Register Src="~/Controls/Meetings.ascx" TagName="Meetings" TagPrefix="ms" %>
<%@ Register Src="~/Controls/Faculty.ascx" TagName="Faculty" TagPrefix="ms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
function getList()
{
    __doPostBack('<%=courseUpdateInfo.ClientID%>','');
}

        function PostBack() {
        
         var modal = $find('<%=ModalPopupExtender1.ClientID %>');
            modal.hide();
        
        WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("addCourseButton", "", true, "editsection", "", false, false));
            if(Page_IsValid)
            {
             customPostBack('TargetItem', '');
            }
        }
        
        function customPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;
                theForm.submit();
            }
}

    </script>

    <h1>Course Details</h1>
    <p runat="server" id="divchairoption">
        When I submit this form, I would like to:
        <asp:RadioButtonList ID="radSubmitOptions" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Text="Submit Directly to registration" Value="Direct" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Review before approving to registration" Value="Preview"></asp:ListItem>
        </asp:RadioButtonList>
<cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" PopupControlID="example"
    Position="Bottom" TargetControlID="radSubmitOptions" />
<asp:Panel ID="example" CssClass="exCallout" runat="server">
   <p >
        If you would like to review this form before you submit to registration, choose the review option.
        Assuming that you are the divison chair, the form will be available for your review from the <a href="NeedsApproval.aspx">Chair Review menu</a>.<br />
        You will be able to edit, approve, or ignore the form from that menu.    
   </p>
</asp:Panel>
    </p>
    <%--  Start of Changes  --%>
  
                  <img src="images/Labels/Details.png" />
                <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid" 
                    BorderWidth="1" CssClass="formLayout">
                    <br />
                    <ms:SectionDetails ID="SectionDetails1" runat="server" />
                </asp:Panel>
                <br />
                <img src="images/Labels/Faculty.png" />
                <asp:Panel ID="Panel" runat="server" BorderColor="Black" BorderStyle="Solid" 
                    BorderWidth="1" CssClass="formLayout">
                    <br />
                    <ms:Faculty ID="Faculty1" runat="server" />
                </asp:Panel>
                <br />
                <img src="images/Labels/Meetings.png" />
                <asp:Panel ID="Panel3" runat="server" BorderColor="Black" BorderStyle="Solid" 
                    BorderWidth="1" CssClass="formLayout">
                    <br />
                    <ms:Meetings ID="Meetings1" runat="server" />
                </asp:Panel>
                <br />
                <img src="images/Labels/CrossLinked.png" />
                <asp:Panel ID="Panel4" runat="server" BorderColor="Black" BorderStyle="Solid" 
                    BorderWidth="1" CssClass="formLayout">
                    <br />
                     <ms:CrossLinked ID="CrossLinked1" runat="server" />
                </asp:Panel>
                <br />
                <img src="images/Labels/CoRequisite.png" />
                <asp:Panel ID="Panel5" runat="server" BorderColor="Black" BorderStyle="Solid" 
                    BorderWidth="1" CssClass="formLayout">
                    <br />
                    <ms:Coreqs ID="Coreqs1" runat="server" />
                </asp:Panel>
                 <br />
                <img src="images/Labels/CoRequisite.png" />
                <%--<asp:Panel ID="Panel2" runat="server" BorderColor="Black" BorderStyle="Solid" 
                    BorderWidth="1" CssClass="formLayout">
                    <br />
                    <ms:Coreqs ID="Coreqs1" runat="server" />
                </asp:Panel>--%>
                
<%-- End of Changes    --%>    

 
      
    
    <br />
    <div style="padding: 20px 0">
                    <asp:Button ID="btnAddNewCourse" runat="server" Text="Add Course" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"
                        Text="Cancel" />
                </div>
                <p>
                </p>
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalbg"
                    CancelControlID="btnCloseConfirm" DropShadow="true" PopupControlID="CourseConfirmPanel"
                    TargetControlID="btnAddNewCourse">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="CourseConfirmPanel" runat="server" CssClass="modalwindow" Width="300">
                    <asp:UpdatePanel ID="courseUpdateInfo" runat="server" OnLoad="confirmationData">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="CourseConfirmationLabel" Text=""></asp:Label></ContentTemplate>
                    </asp:UpdatePanel>
                    <input type="button" id="addCourseButton" runat="server" onclick="PostBack();" value="Add Course" />
                    &nbsp;
                    <asp:Button ID="btnCloseConfirm" runat="server" CausesValidation="false" Text="Cancel" />
                </asp:Panel>
                <p>
                </p>
    
   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    <h1>Pending Changes</h1>
    <asp:GridView ID="gvPendingChanges" SkinID="GridView" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="LogID" DataSourceID="SqlDataSourceChanges" HorizontalAlign="Left" Width="100%">
        <Columns>
       <asp:TemplateField ItemStyle-Wrap="false">
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# String.Format("~/ChangeEdit.aspx?changelogid={0}&returnurl=ViewSection.aspx?sectionsid={1}", Eval("LogID"), Request.QueryString["SectionsID"]) %>' runat="server">Edit
                    <asp:Image ID="Image1" ImageUrl="~/images/application_edit.png" BorderStyle="none" runat="server" />
                </asp:HyperLink>
            </ItemTemplate>
                  
            </asp:TemplateField>
         
            <asp:BoundField DataField="DateSubmitted" HeaderText="Date Submitted" SortExpression="DateSubmitted" >
                <ItemStyle HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="SubmittedBy" HeaderText="By User" SortExpression="SubmittedBy" >
                <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Change" HeaderText="Change" SortExpression="Change" HtmlEncode="False" >
                <ItemStyle Width="100%" />
            </asp:BoundField>
        </Columns>
        <EmptyDataTemplate>
            There are no pending changes.
        </EmptyDataTemplate>
    </asp:GridView>--%>
<%--    <asp:SqlDataSource ID="SqlDataSourceChanges" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
        SelectCommand="ChangeLog_Fill_Section" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="SectionsID" QueryStringField="SectionsID"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </ContentTemplate>
    </asp:UpdatePanel>--%>
    
    <div style="padding-bottom:15px;">&nbsp;</div>
    
</asp:Content>

