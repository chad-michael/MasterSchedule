<%@ Page Language="C#" MasterPageFile="~/public/Default.master" AutoEventWireup="true" CodeFile="ReportWorkflow.aspx.cs" Inherits="ReportWorkflow" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <a href="Report.aspx"><span style="color: #0000ff; text-decoration: underline">Back to Reports</span></a><br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ProcessingID"
        DataSourceID="WorkflowDS">
        <Columns>
            <asp:BoundField DataField="ProcessName" HeaderText="Workflow Step" SortExpression="ProcessName" />
            <asp:BoundField DataField="ActionTaken" HeaderText="Last Action" NullDisplayText="No Action"
                SortExpression="ActionTaken" />
            <asp:BoundField DataField="ProcessedBy" HeaderText="ProcessedBy" NullDisplayText="Not Processed"
                SortExpression="ProcessedBy" />
            <asp:BoundField DataField="ProcessedOn" HeaderText="ProcessedOn" NullDisplayText="Not Processed"
                SortExpression="ProcessedOn" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <asp:GridView ID="gvNeedsApproval" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="LogID" SkinID="GridView" Width="100%" >
        <Columns>
            <asp:BoundField DataField="DateSubmitted" HeaderText="Submitted" SortExpression="DateSubmitted" />
            <asp:BoundField DataField="SubmittedBy" HeaderText="By User" SortExpression="SubmittedBy" />
            <asp:TemplateField HeaderText="Course">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkViewCourse" runat="server" CssClass="magnifier" NavigateUrl='<%#Eval("SectionsID", "~/ViewSection.aspx?SectionsID={0}") %>'
                        Style='<%#ViewCourseCss(Eval("SectionsID")) %>'><%#Eval("Term") %>&nbsp;<%#Eval("CourseNumber") %>&nbsp;<%#Eval("SectionNumber") %></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Change" HeaderText="Change" HtmlEncode="False" SortExpression="Change" />
        </Columns>
        <EmptyDataTemplate>
            Change Not Found!
        </EmptyDataTemplate>
    </asp:GridView>
    &nbsp;<br />
    <br />
    <asp:SqlDataSource ID="WorkflowDS" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
        SelectCommand="SELECT * FROM [ChangeLogProcessing] WHERE ([ChangeLogId] = @ChangeLogId) ORDER BY [ProcessName]">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="ChangeLogId" QueryStringField="changelogid"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>



</asp:Content>

