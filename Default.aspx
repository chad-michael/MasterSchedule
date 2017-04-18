<%@ Page Language="C#" MasterPageFile="~/public/Default.master" Theme="Default" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" Title="Master Schedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="TestDataMessage" runat="server">
        <br />
        </h1>
    </asp:Panel>
    <asp:Panel ID="pnlDivisionChair" runat="server">
        <div class="NeedsApprovalMsg">
            <asp:Image ID="imgExclamation" runat="server" ImageUrl="~/public/images/exclamation.png"
                ImageAlign="AbsMiddle" AlternateText="Needs Approval:" Style="padding-bottom: 3px;" />&nbsp;You
            have
            <asp:Label ID="lblNoChanges" runat="server" CssClass="NeedsApprovalNumber"></asp:Label>
            pending changes to approve!&nbsp;<asp:HyperLink ID="lnkPendingChanges" runat="server"
                NavigateUrl="~/NeedsApproval.aspx" CssClass="ViewPendingChanges">View Pending Changes</asp:HyperLink>
        </div>
    </asp:Panel>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <h1>Select a Course</h1>
            </td>
            <td>
                <div style="padding: 2px 5px 0 0; text-align: right;">
                    <strong>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/AddNew.aspx" CssClass="addcourse">Add a New Course</asp:HyperLink></strong>
                </div>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>Term
            </td>
            <td>
                <asp:DropDownList ID="ddlTerm" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Department
            </td>
            <td>
                <asp:DropDownList ID="ddDepartment" runat="server" AutoPostBack="True" OnDataBound="ddDepartment_DataBound"
                    OnSelectedIndexChanged="ddDepartment_Changed">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Course
            </td>
            <td>
                <asp:DropDownList ID="ddCourse" runat="server"
                    OnSelectedIndexChanged="ddCourse_Changed"
                    OnDataBound="ddCourse_DataBound" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvSchedule" runat="server" Width="100%" AutoGenerateColumns="False"
        DataKeyNames="SectionsID" SkinID="GridView">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkViewSection" runat="server" NavigateUrl='<%#Eval("SectionsID", "~/ViewSection2.aspx?SectionsID={0}") %>'
                        CssClass="magnifier">Select</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CourseNumber" HeaderText="Course" SortExpression="CourseNumber" />
            <asp:BoundField DataField="SectionNumber" HeaderText="Sec." SortExpression="SectionNumber">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CourseTitle" HeaderText="Title" SortExpression="CourseTitle" />
            <asp:BoundField DataField="Credits" HeaderText="Credits" SortExpression="Credits">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Capacity" HeaderText="Cap." SortExpression="Capacity"
                DataFormatString="{0:0}" HtmlEncode="False">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="SectionStartDate" HeaderText="Start Date" SortExpression="SectionStartDate"
                DataFormatString="{0:d}" HtmlEncode="False">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="SectionEndDate" HeaderText="End Date" SortExpression="SectionEndDate"
                DataFormatString="{0:d}" HtmlEncode="False">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="MeetStartTime" HeaderText="Start Time" SortExpression="MeetStartTime"
                DataFormatString="{0:t}" HtmlEncode="False">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="MeetEndTime" HeaderText="End Time" SortExpression="MeetEndTime"
                DataFormatString="{0:t}" HtmlEncode="False">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Days" SortExpression="MeetDays">
                <ItemTemplate>
                    <%#FormatMeetDays(Eval("MeetDays")) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="norecords">
                <strong>No courses found. Please change your search parameters and try again.</strong>
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceSchedule" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
        SelectCommand="MasterSchedule_SearchByTerm" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlTerm" DefaultValue="0" Name="TermsId" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:ControlParameter ControlID="ddDepartment" DefaultValue="0" Name="DepartmentsID"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="ddCourse" DefaultValue="0" Name="CoursesID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>