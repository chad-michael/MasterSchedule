<%@ Page Language="C#" MasterPageFile="~/public/Default.master" AutoEventWireup="true"
    CodeFile="Report.aspx.cs" Inherits="Report" Title="Master Schedule - Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    function RemoveTo()
    {
       $get("<%= txtTo.ClientID %>").value = "";  

    }
    
     function RemoveFrom()
    {
       $get("<%= txtFrom.ClientID %>").value = "";  

    }
</script>
    <h1>
        All Schedule Changes</h1>
    Use this screen to review all schedule changes which have been submitted.<br />
    <br />
    <table>
        <tr>
            <td>
                <b>From:
            </b>
            </td>
            <td>
                <asp:TextBox ID="txtFrom" runat="server" ></asp:TextBox>
                <cc1:calendarextender id="CalendarExtender3" runat="server" animated="true" targetcontrolid="txtFrom"
                    onclientdateselectionchanged="hideCalendar"></cc1:calendarextender>
            </td>
            
            <td>
                <img src="images/delete.png" onclick="RemoveFrom();" alt="Remove Filter" title="Remove Filter" /></td>
    
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
    
            <td>
                <b>To:
            </b>
            </td>
            <td>
              <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                <cc1:calendarextender id="CalendarExtender1" runat="server" animated="true" targetcontrolid="txtTo"
                    onclientdateselectionchanged="hideCalendar"></cc1:calendarextender>
            </td>
              <td>
                <img src="images/delete.png" onclick="RemoveTo();" alt="Remove Filter" title="Remove Filter" /></td>
        </tr>
    </table>
                                    <br />
    &nbsp;<span style="font-size: xx-small">The ??-?? in the terms dropdown list is an indication of a course which 
                                    was “Added”.</span> <table>
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
                <asp:DropDownList ID="ddlSubmittedByFilter" runat="server" DataValueField="SubmittedBy"
                    DataTextField="SubmittedBy">
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
                Division:
            </td>
            <td>
                <asp:DropDownList ID="ddlDivision" runat="server" DataValueField="DivisionsID" DataTextField="DivisionDesc"></asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnFilter" runat="server" Text="Filter Results" OnClick="btnFilter_Click" />
            </td>
        </tr>
    </table>
    <br />
    <h4>
        <asp:Label ID="lblErr" runat="server" Text=""></asp:Label>
    </h4>
    Total Records: <asp:Label ID="lblRecordCount" runat="server" Text=""></asp:Label> Page <asp:Label ID="lblCurrentPage" runat="server" Text="1"></asp:Label> of <asp:Label ID="lblTotalPages" runat="server" Text=""> </asp:Label>  <asp:Button ID="Btn_Previous" CommandName="Previous" 
            runat="server" OnCommand="ChangePage" 
            Text="Previous" />   
<asp:Button ID="Btn_Next" runat="server" CommandName="Next" 
            OnCommand="ChangePage" Text="Next" />
<br />
<br />
    <asp:GridView Width="100%" SkinID="GridView" ID="gvNeedsApproval" runat="server"
        AutoGenerateColumns="False" DataKeyNames="LogID" AllowPaging="True"
    PageIndex="1" PageSize="20">
        <Columns>
            <asp:TemplateField HeaderText="Course">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkViewWorfklow" runat="server" NavigateUrl='<%#Eval("LogID", "~/ReportWorkflow.aspx?changelogid={0}") %>'
                        CssClass="magnifier">Track Workflow</asp:HyperLink>
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
        </Columns>
        <EmptyDataTemplate>
            No master scheudle form changes match your search. 
        </EmptyDataTemplate>
		<PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
    </asp:GridView>
	<br/><br/>
</asp:Content>
