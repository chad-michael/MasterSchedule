<%@ Page Language="C#" Theme="Default" MasterPageFile="~/public/Default.master" AutoEventWireup="true"
    CodeFile="AddCourse.aspx.cs" Inherits="AddCourse" Title="Untitled Page" %>

<%@ Register Src="~/Controls/SectionDetails_New.ascx" TagName="SectionDetails" TagPrefix="ms" %>
<%@ Register Src="~/Controls/CrossLinked_New.ascx" TagName="CrossLinked" TagPrefix="ms" %>
<%@ Register Src="~/Controls/Coreqs_New.ascx" TagName="Coreqs" TagPrefix="ms" %>
<%@ Register Src="~/Controls/Meetings_New.ascx" TagName="Meetings" TagPrefix="ms" %>
<%@ Register Src="~/Controls/Faculty_New.ascx" TagName="Faculty" TagPrefix="ms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Add New Course</h1>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
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
                        If you would like to review this form before you submit to registration, choose
                        the review option. Assuming that you are the divison chair, the form will be available
                        for your review from the <a href="NeedsApproval.aspx">Chair Review menu</a>.<br />
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
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </div>
            <div style="padding: 20px 0">
                <asp:Button ID="btnAddNewCourse" runat="server" Text="Add Course" OnClick="btnAddNewCourse_Click" />&nbsp;<asp:Button
                    ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            Course has been submitted for approval.
            <asp:GridView ID="gvPendingChanges" SkinID="GridView" runat="server" AllowPaging="False"
                AllowSorting="False" AutoGenerateColumns="False" DataKeyNames="LogID" HorizontalAlign="Left"
                Width="100%">
                <Columns>
                    <asp:TemplateField ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# String.Format("~/ChangeEdit.aspx?changelogid={0}&returnurl=default.aspx", Eval("LogID")) %>'
                                runat="server">
                                Edit
                                <asp:Image ID="Image1" ImageUrl="~/images/application_edit.png" BorderStyle="none"
                                    runat="server" />
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DateSubmitted" HeaderText="Date Submitted" SortExpression="DateSubmitted">
                        <ItemStyle HorizontalAlign="Right" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SubmittedBy" HeaderText="By User" SortExpression="SubmittedBy">
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Change" HeaderText="Change" SortExpression="Change" HtmlEncode="False">
                        <ItemStyle Width="100%" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    There was an error submitting your course add request.
                </EmptyDataTemplate>
            </asp:GridView>
        </asp:View>
    </asp:MultiView>
</asp:Content>