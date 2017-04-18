<%@ Control Language="C#" AutoEventWireup="true" EnableTheming="true"
    CodeFile="ApprovalGrid.ascx.cs" Inherits="Controls.Controls_ApprovalGrid" %>
<asp:Panel runat="server" ID="processScreen">

    <asp:HiddenField runat="server" ID="hProcessName" />
    <asp:HiddenField runat="server" ID="hStatusFilter" />

    <h1>
        <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></h1>
    <asp:Label ID="lblScreenTitle" runat="server" Text=""></asp:Label>
    <asp:GridView Width="100%"
        SkinID="GridView" ID="gvNeedsApproval"
        runat="server" AutoGenerateColumns="False"
        DataKeyNames="LogID" DataSourceID="SqlDataSourceNeedsApproval"
        OnRowCommand="gvNeedsApproval_RowCommand" AllowPaging="False" AllowSorting="True">
        <Columns>
            <asp:BoundField DataField="DateSubmitted" HeaderText="Submitted" SortExpression="DateSubmitted" />
            <asp:BoundField DataField="SubmittedBy" HeaderText="By User" SortExpression="SubmittedBy" />
            <asp:TemplateField HeaderText="Course">
                <ItemTemplate>
                    <asp:HyperLink Style='<%#ViewCourseCss(Eval("SectionsID")) %>' ID="lnkViewCourse" runat="server" NavigateUrl='<%#Eval("SectionsID", "~/ViewSection.aspx?SectionsID={0}") %>' CssClass="magnifier"><%#Eval("Term") %>&nbsp;<%#Eval("CourseNumber") %>&nbsp;<%#Eval("SectionNumber") %></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Change" HeaderText="Change" SortExpression="Change" HtmlEncode="False" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnApprove" runat="server" CssClass="approve" CommandName="approve" CommandArgument='<%#Eval("ProcessingId") %>'>Processed</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemStyle Wrap="false" />
                <ItemTemplate>
                    <asp:LinkButton ID="btnIgnore" runat="server" CssClass="ignore" CommandName="ignore" CommandArgument='<%#Eval("ProcessingId") %>'>Not Processed</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            There are no pending changes that need your approval.
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceNeedsApproval" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
        SelectCommand="ChangeLog_Fill_ChairByProcess" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="DeltaID" SessionField="deltaid" Type="String" />
            <asp:ControlParameter ControlID="hProcessName" Name="ProcessName" PropertyName="Value"
                Type="String" />
            <asp:ControlParameter ControlID="hStatusFilter" DefaultValue="pending" Name="Status"
                PropertyName="Value" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Panel>