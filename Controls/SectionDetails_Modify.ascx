<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SectionDetails_Modify.ascx.cs" Inherits="Controls.Controls_SectionDetails_Modify" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdkey">Year
                </td>
                <td class="tdvalue">
                    <asp:DropDownList ValidationGroup="editsection" ID="ddYear" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceYear" DataTextField="TermYear" DataValueField="TermYear"></asp:DropDownList><asp:SqlDataSource ID="SqlDataSourceYear" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                        SelectCommand="List_TermYears" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="tdkey">Semester
                </td>
                <td class="tdvalue">
                    <asp:DropDownList ValidationGroup="editsection" ID="ddSemester" runat="server" DataSourceID="SqlDataSourceSemester" DataTextField="SemesterDesc" DataValueField="SemestersID"></asp:DropDownList><asp:SqlDataSource ID="SqlDataSourceSemester" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                        SelectCommand="List_Semesters" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddYear" DefaultValue="0" Name="TermYear" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="tdkey">Department
                </td>
                <td class="tdvalue">
                    <asp:DropDownList ValidationGroup="editsection" ID="ddDepartment" runat="server" DataSourceID="SqlDataSourceDept" DataTextField="DepartmentDesc" DataValueField="DepartmentsID"></asp:DropDownList><asp:SqlDataSource ID="SqlDataSourceDept" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                        SelectCommand="List_Departments" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="tdkey">Catalog Number
                </td>
                <td class="tdvalue">
                    <asp:TextBox ValidationGroup="editsection" ID="txtCatalogNumber" runat="server" MaxLength="6"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="editsection" ID="RequiredFieldValidator3" runat="server" ErrorMessage="A catalog number is required." ControlToValidate="txtCatalogNumber" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator3"></cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td class="tdkey">Section Number
                </td>
                <td class="tdvalue">
                    <asp:TextBox ID="txtSectionNumber" ValidationGroup="editsection" runat="server" MaxLength="5"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="editsection" ID="RequiredFieldValidator2" runat="server" ErrorMessage="A section number is required." ControlToValidate="txtSectionNumber" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator2"></cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td class="tdkey">Semester Start
                </td>
                <td class="tdvalue">
                    <div>
                        <asp:TextBox ID="txtTermStartDate" ValidationGroup="editsection" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTermStartDate" ErrorMessage="Course Start Date Required" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator4"></cc1:ValidatorCalloutExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MaskType="Date" Mask="99/99/9999" UserDateFormat="MonthDayYear" TargetControlID="txtTermStartDate"></cc1:MaskedEditExtender>
                        <asp:RegularExpressionValidator ValidationGroup="editsection" ID="RegularExpressionValidator1" runat="server" ErrorMessage="This is not a valid date." ControlToValidate="txtTermStartDate" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.][0-9]{4}$">*</asp:RegularExpressionValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RegularExpressionValidator1"></cc1:ValidatorCalloutExtender>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" TargetControlID="txtTermStartDate" OnClientDateSelectionChanged="hideCalendar"></cc1:CalendarExtender>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="tdkey">Semester End
                </td>
                <td class="tdvalue">
                    <asp:TextBox ID="txtTermEndDate" ValidationGroup="editsection" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTermEndDate" ErrorMessage="Course End Date Required" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RequiredFieldValidator5"></cc1:ValidatorCalloutExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" MaskType="Date" Mask="99/99/9999" UserDateFormat="MonthDayYear" TargetControlID="txtTermEndDate"></cc1:MaskedEditExtender>
                    <asp:RegularExpressionValidator ValidationGroup="editsection" ID="RegularExpressionValidator2" runat="server" ErrorMessage="This is not a valid date." ControlToValidate="txtTermEndDate" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.][0-9]{4}$">*</asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RegularExpressionValidator2"></cc1:ValidatorCalloutExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" TargetControlID="txtTermEndDate" OnClientDateSelectionChanged="hideCalendar"></cc1:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="tdkey">Capacity
                </td>
                <td class="tdvalue">
                    <asp:TextBox ID="txtCapacity" ValidationGroup="editsection" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="editsection" ID="RequiredFieldValidator1" runat="server" ErrorMessage="A capacity is required." ControlToValidate="txtCapacity" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator1"></cc1:ValidatorCalloutExtender>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtCapacity"></cc1:FilteredTextBoxExtender>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>