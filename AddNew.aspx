<%@ Page Language="C#" MasterPageFile="~/public/Default.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="AddNew.aspx.cs" Inherits="addnew"
    Theme="Default" Title="Add New Course" ValidateRequest="false" %>

<%@ Register Assembly="obout_Flyout2_NET" Namespace="OboutInc.Flyout2" TagPrefix="cc2" %>
<%@ Register Src="Controls/SectionDetails_New.ascx" TagName="SectionDetails" TagPrefix="uc1" %>
<%@ Register Src="Controls/Meetings_New.ascx" TagName="Meetings_New" TagPrefix="uc3" %>
<%--<%@ Register Src="Controls/CrossLinked_New.ascx" TagName="CrossLinked_New" TagPrefix="uc4" %>
--%>
<%@ Register Src="Controls/Coreqs_New.ascx" TagName="Coreqs_New" TagPrefix="uc5" %>
<%@ Register Src="Controls/Colisted_New.ascx" TagName="Colisted_New" TagPrefix="uc7" %>
<%@ Register Src="Controls/Faculty_New.ascx" TagName="Faculty_New" TagPrefix="uc2" %>
<%@ Register Src="Controls/Comments_New.ascx" TagName="Comments_New" TagPrefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function toggle() {
            if (cadCoreq.get_isShowing()) {
                cadCoreq.Close();
            }
            else {
                cadCoreq.Show();
            }
        }
    </script>

    <asp:HiddenField ID="SectionID" runat="server" />
    <asp:HiddenField ID="hidRooms" runat="server" />
    <h1>Add New Course
    </h1>

    <asp:MultiView ID="mvSubmissionForm" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <p runat="server" id="divchairoption" visible="false">
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
            <%--    <asp:Panel ID="pnliTunesUHeader" runat="server">
                <br />
                <img src="images/Labels/iTunesU.png" />
                <input type="button" id="btniTunesU" value="?" style="position: relative; top: -5px;" />
                <cc2:Flyout ID="flyiTunesU" runat="server" AttachTo="btniTunesU" Position="BOTTOM_RIGHT">
                    <div class="exCallout">
                        <p>
                            <b>iTunes U:</b><br />
                            iTunes U allows faculty the ability to host course content on iTunes U<br />
                            (part of iTunes and the iTunes store.<br />
                            By enabeling iTunes content, you are able to upload documents, podcasts and vodcasts
                            (videos)<br />
                            to iTunes U, and students are able to download or subscribe to the course content
                            via RSS.
                            <br />
                            To take a look around iTunes U go to http://iTunesu.delta.edu<br />
                            and login with your Delta College user name and password. Under Staff & Faculty
                            Resources,<br />
                            in the COS course you will find information and a FAQ regarding Delta College on
                            iTunes U.
                        </p>
                    </div>
                </cc2:Flyout>
            </asp:Panel>
            <asp:Panel ID="pnliTunesU" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1" CssClass="formLayout">
                <br />
                <h2>
                    Add iTunes U capabilities</h2>
                <asp:RadioButtonList ID="rbliTunesu" runat="server">
                    <asp:ListItem Text="Enable iTunes U for this course" Value="Enable iTunes U for this course"></asp:ListItem>
                    <asp:ListItem Text="No iTunes U for this course" Selected="True" Value=""></asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <br />
            </asp:Panel>--%>
            <br />
            <asp:Panel ID="pnlMyLabsPlus" runat="server">
                <br />
                <img src="images/Labels/MyLabsPlus.png" />
                <input type="button" id="btnMyLabsPlus" value="?" style="position: relative; top: -5px;" />
                <cc2:Flyout ID="flyMyLabs" runat="server" AttachTo="btnMyLabsPlus" Position="BOTTOM_RIGHT">
                    <div class="exCallout">
                        <p>
                            <b>MyLabsPlus:</b><br />
                        </p>
                    </div>
                </cc2:Flyout>
            </asp:Panel>
            <asp:Panel ID="pnliMyLabsPlus" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1" CssClass="formLayout">
                <br />
                <br />
                <h2>Pearson eCollege MyLabsPlus</h2>

                <table cellpadding="0" cellspacing="0">

                    <tr>
                        <td class="tdkey">This course is in MyLabsPlus:</td>
                        <td class="tdvalue">
                            <asp:CheckBox runat="server" ID="chkIsInMyLabs" />
                        </td>
                    </tr>
                </table>

                <br />
                <br />
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlDirectContentFeeHeader">
                <table>
                    <tr>
                        <td>
                            <h1>Direct Content Fee
                            </h1>
                        </td>
                        <td>
                            <input type="button" id="btnDirectContentFee" value="?" style="position: relative; top: -5px;" />
                        </td>
                    </tr>
                </table>
                <cc2:Flyout ID="flyoutDirectContentFee" runat="server" AttachTo="btnDirectContentFee"
                    Position="BOTTOM_RIGHT">
                    <div class="exCallout" style="width: 300px;">
                        <p>
                            <b>Direct Content Fee:</b>
                        </p>
                        <p>
                            Direct Content Fee: A $28 content fee will be assessed
                            for licensing use of video materials and/or online content when you register for
                            this course.
                            <br />
                        </p>
                    </div>
                </cc2:Flyout>
            </asp:Panel>
            <asp:Panel ID="pnlDirectContentFee" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1" CssClass="formLayout">
                <h2>Direct Content Fee
                </h2>
                <asp:CheckBox runat="server" Text="Course has a direct content fee." ID="ckDirectContentFee" />
            </asp:Panel>
            <br />
            <img src="images/Labels/colisted.png" />
            <input type="button" id="btnQColisted" value="?" style="position: relative; top: -5px;" />
            <cc2:Flyout ID="flyColisted" runat="server" AttachTo="btnQColisted" Position="BOTTOM_RIGHT">
                <div class="exCallout">
                    <p>
                        <b>Cross-Listed Courses:</b><br />
                        Equivalent courses but credit may be earned in a choice of discipline so have different
                        prefixes (and potentially differenct #s).<br />
                        Students may earn credit in one of the courses but not more than one.
                        <br />
                        <br />
                        Ex: SOC 230/BIO 230<br />
                        SOC 300/PSY 300
                    </p>
                </div>
            </cc2:Flyout>
            <asp:Panel ID="Panel6" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                CssClass="formLayout">
                <br />
                <uc7:Colisted_New ID="Colisted_New1" runat="server" />
            </asp:Panel>
            <br />
            <%--
            Removed per direction from Judy Miller on 6.18.2008
            <img src="images/Labels/CrossLinked.png" />
            <asp:Panel ID="Panel4" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1"
                CssClass="formLayout">
                <br />
                <uc4:CrossLinked_New ID="CrossLinked_New1" runat="server" />
            </asp:Panel>
            <br />--%>
            <img src="images/Labels/CoReq_Linked.png" id="imgCoreq" />
            <input type="button" id="btnQCoreq" value="?" style="position: relative; top: - 15px;" />
            <cc2:Flyout ID="flyCoreq" runat="server" AttachTo="btnQCoreq" Position="BOTTOM_RIGHT">
                <div class="exCallout">
                    <p>
                        <b>Co-Requisite/Linked Couses:</b><br />
                        Two or more courses in which students must be enrolled simultaneously.<br />
                        Students earn credit in each of the courses.
                        <br />
                        <br />
                        Ex: Learning Communities
                        <br />
                        HIS111-51 and ENG 112-51<br />
                        SOC211-54 and PSY 211-54 and CJ 210-54
                    </p>
                </div>
            </cc2:Flyout>
            &nbsp;<asp:Panel ID="Panel5" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1" CssClass="formLayout">
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
                <asp:Button ID="btnAddNewCourse" ValidationGroup="editsection" runat="server" Text="Add Course"
                    CausesValidation="true" OnClick="confirmationData_Click" />
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
            <asp:Button ID="btnCloseConfirm" runat="server" CausesValidation="false" Text="Edit"
                OnClick="btnEdit_Click" />
        </asp:View>
    </asp:MultiView>
</asp:Content>