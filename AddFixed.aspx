﻿<%@ Page Language="C#" MasterPageFile="~/public/Default.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="AddFixed.aspx.cs" Inherits="addfixed"
    Theme="Default" Title="Add New Course" ValidateRequest="false" %>

<%@ Register Src="Controls/SectionDetails_New.ascx" TagName="SectionDetails" TagPrefix="uc1" %>
<%@ Register Src="Controls/Meetings_New.ascx" TagName="Meetings_New" TagPrefix="uc3" %>
<%@ Register Src="Controls/CrossLinked_New.ascx" TagName="CrossLinked_New" TagPrefix="uc4" %>
<%@ Register Src="Controls/Coreqs_New.ascx" TagName="Coreqs_New" TagPrefix="uc5" %>
<%@ Register Src="Controls/Colisted_New.ascx" TagName="Colisted_New" TagPrefix="uc7" %>
<%@ Register Src="Controls/Faculty_New.ascx" TagName="Faculty_New" TagPrefix="uc2" %>
<%@ Register Src="Controls/Comments_New.ascx" TagName="Comments_New" TagPrefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="SectionID" runat="server" />
    <h1>Add New Course now
    </h1>
    <asp:MultiView ID="mvSubmissionForm" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <p runat="server" id="divchairoption">
                When I submit this form, I would like to:
                <asp:RadioButtonList ID="radSubmitOptions" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Submit Directly to registration" Value="Direct" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Review before approving to registration" Value="Preview"></asp:ListItem>
                </asp:RadioButtonList>
            </p>
            <asp:Panel ID="example" CssClass="exCallout" runat="server" Visible="false">
                <p>
                    If you would like to review this form before you submit to registration, choose
                        the review option. Assuming that you are the divison chair, the form will be available
                        for your review from the <a href="NeedsApproval.aspx">Chair Review menu</a>.<br />
                    You will be able to edit, approve, or ignore the form from that menu.
                </p>
            </asp:Panel>
            <p>
            </p>
            <img src="images/Labels/Details.png" />
            <asp:Panel ID="Panel1" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                CssClass="formLayout">
                <br />
                <uc1:SectionDetails ID="SectionDetails1" runat="server" />
            </asp:Panel>
            <br />
            <img src="images/Labels/Faculty.png" />
            <asp:Panel ID="Panel" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                CssClass="formLayout">
                <br />
                <uc2:Faculty_New ID="Faculty1" runat="server" />
            </asp:Panel>
            <br />
            <img src="images/Labels/Meetings.png" />
            <asp:Panel ID="Panel3" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                CssClass="formLayout">
                <br />
                <uc3:Meetings_New ID="Meetings_New1" runat="server" />
            </asp:Panel>
            <br />
            <img src="images/Labels/colisted.png" />
            <asp:Panel ID="Panel6" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                CssClass="formLayout">
                <br />
                <uc7:Colisted_New ID="Colisted_New1" runat="server" />
            </asp:Panel>
            <br />
            <img src="images/Labels/CrossLinked.png" />
            <asp:Panel ID="Panel4" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                CssClass="formLayout">
                <br />
                <uc4:CrossLinked_New ID="CrossLinked_New1" runat="server" />
            </asp:Panel>
            <br />
            <img src="images/Labels/CoRequisite.png" />
            <asp:Panel ID="Panel5" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                CssClass="formLayout">
                <br />
                <uc5:Coreqs_New ID="Coreqs_New1" runat="server" />
            </asp:Panel>
            <br />
            <img src="images/Labels/Comments.png" />
            <asp:Panel ID="Panel2" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                CssClass="formLayout">
                <br />
                <uc6:Comments_New ID="Comments_New1" runat="server" />
            </asp:Panel>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </div>
            <div style="padding: 20px 0">
                <asp:Button ID="btnAddNewCourse" ValidationGroup="editsection" runat="server"
                    Text="Add Course" CausesValidation="true"
                    OnClick="confirmationData_Click" />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"
                    Text="Cancel" />
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
        <asp:View runat="server" ID="mvViewConfirm">

            <asp:Label runat="server" ID="CourseConfirmationLabel" Text=""></asp:Label>
            <asp:Button ID="btnSave" runat="server" Text="Confirm and Save" OnClick="btnAddNewCourse_Click" />
            <asp:Button ID="btnCloseConfirm" runat="server" CausesValidation="false" Text="Edit" OnClick="btnEdit_Click" />
        </asp:View>
    </asp:MultiView>
</asp:Content>