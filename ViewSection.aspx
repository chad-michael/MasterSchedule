<%@ Page Language="C#" Theme="Default" MasterPageFile="~/public/Default.master" AutoEventWireup="true" CodeFile="ViewSection.aspx.cs" Inherits="ViewSection" Title="Untitled Page" %>

<%@ Register Src="~/Controls/SectionDetails.ascx" TagName="SectionDetails" TagPrefix="ms" %>
<%@ Register Src="~/Controls/CrossLinked.ascx" TagName="CrossLinked" TagPrefix="ms" %>
<%@ Register Src="~/Controls/Coreqs.ascx" TagName="Coreqs" TagPrefix="ms" %>
<%@ Register Src="~/Controls/Meetings.ascx" TagName="Meetings" TagPrefix="ms" %>
<%@ Register Src="~/Controls/Faculty.ascx" TagName="Faculty" TagPrefix="ms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
   <p>
        If you would like to review this form before you submit to registration, choose the review option.
        Assuming that you are the divison chair, the form will be available for your review from the <a href="NeedsApproval.aspx">Chair Review menu</a>.<br />
        You will be able to edit, approve, or ignore the form from that menu.    
   </p>
</asp:Panel>
    </p>
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Course Details">
            <ContentTemplate>
                <ms:SectionDetails ID="SectionDetails1" runat="server" />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Faculty">
            <ContentTemplate>
                <ms:Faculty ID="Faculty1" runat="server" />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Meetings">
            <ContentTemplate>
                <ms:Meetings ID="Meetings1" runat="server" />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Cross Linked Courses">
            <ContentTemplate>
                <ms:CrossLinked ID="CrossLinked1" runat="server" />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Co-Requisite Courses">
            <ContentTemplate>
                <ms:Coreqs ID="Coreqs1" runat="server" />
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    
    <br />
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
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
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceChanges" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
        SelectCommand="ChangeLog_Fill_Section" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="SectionsID" QueryStringField="SectionsID"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <div style="padding-bottom:15px;">&nbsp;</div>
    
</asp:Content>

