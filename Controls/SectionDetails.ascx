<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SectionDetails.ascx.cs" Inherits="Controls.Controls_SectionDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">

                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="tdkey">Year
                        </td>
                        <td class="tdvalue">
                            <asp:Label ID="lblTermYear" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Semester
                        </td>
                        <td class="tdvalue">
                            <asp:Label ID="lblSemester" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Department
                        </td>
                        <td class="tdvalue">
                            <asp:Label ID="lblDepartment" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Catalog Number
                        </td>
                        <td class="tdvalue">
                            <asp:Label ID="lblCatalogNumber" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Section Number
                        </td>
                        <td class="tdvalue">
                            <asp:Label ID="lblSectionNumber" runat="server" />
                        </td>
                    </tr>
                    <!-- Commented out by Cyrus 11.15.2007 -->
                    <%-- <tr>
                <td class="tdkey">
                    Semester Start
                </td>
                <td class="tdvalue">
                    <asp:Label ID="lblTermStartDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="tdkey">
                    Semester End
                </td>
                <td class="tdvalue">
                    <asp:Label ID="lblTermEndDate" runat="server" />
                </td>
            </tr>--%>
                    <tr>
                        <td class="tdkey">Capacity
                        </td>
                        <td class="tdvalue">
                            <asp:Label ID="lblCapacity" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="tdkey">Year
                        </td>
                        <td class="tdvalue">
                            <asp:Label ID="lblYear" runat="server" Text=""></asp:Label>
                            <%--<asp:DropDownList ValidationGroup="editcourse" ID="ddYear" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceYear" DataTextField="TermYear" DataValueField="TermYear"></asp:DropDownList><asp:SqlDataSource ID="SqlDataSourceYear" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                        SelectCommand="List_TermYears" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Semester
                        </td>
                        <td class="tdvalue">
                            <asp:Label ID="lblCourseSemester" runat="server" Text=""></asp:Label>
                            <%-- <asp:DropDownList ValidationGroup="editcourse" ID="ddSemester" runat="server" DataSourceID="SqlDataSourceSemester" DataTextField="SemesterDesc" DataValueField="SemestersID"></asp:DropDownList><asp:SqlDataSource ID="SqlDataSourceSemester" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                    SelectCommand="List_Semesters" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddYear" DefaultValue="0" Name="TermYear" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Department
                        </td>
                        <td class="tdvalue">
                            <asp:HiddenField ID="hdfDepartmentID" runat="server" />
                            <asp:Label ID="lblCourseDepartment" runat="server" Text=""></asp:Label>
                            <%--<asp:DropDownList ValidationGroup="editcourse" ID="ddDepartment" runat="server" DataSourceID="SqlDataSourceDept" DataTextField="DepartmentDesc" DataValueField="DepartmentsID"></asp:DropDownList><asp:SqlDataSource ID="SqlDataSourceDept" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                    SelectCommand="List_Departments" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Catalog Number
                        </td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtCatalogNumber" runat="server" ValidationGroup="editcourse"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="editcourse" ID="RequiredFieldValidator3" runat="server" ErrorMessage="A catalog number is required." ControlToValidate="txtCatalogNumber" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator3"></cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Section Number
                        </td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtSectionNumber" runat="server" ValidationGroup="editcourse"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="editcourse" ID="RequiredFieldValidator2" runat="server" ErrorMessage="A section number is required." ControlToValidate="txtSectionNumber" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator2"></cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>

                    <tr>
                        <td class="tdkey">Section Start
                        </td>
                        <td class="tdvalue">
                            <div>
                                <asp:TextBox ID="txtTermStartDate" runat="server" ValidationGroup="editcourse"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MaskType="Date" Mask="99/99/9999" UserDateFormat="MonthDayYear" TargetControlID="txtTermStartDate"></cc1:MaskedEditExtender>
                                <asp:RegularExpressionValidator ValidationGroup="editcourse" ID="RegularExpressionValidator1" runat="server" ErrorMessage="This is not a valid date." ControlToValidate="txtTermStartDate" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.][0-9]{4}$">*</asp:RegularExpressionValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RegularExpressionValidator1"></cc1:ValidatorCalloutExtender>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" TargetControlID="txtTermStartDate" OnClientDateSelectionChanged="hideCalendar"></cc1:CalendarExtender>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Section End
                        </td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtTermEndDate" runat="server" ValidationGroup="editcourse"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" MaskType="Date" Mask="99/99/9999" UserDateFormat="MonthDayYear" TargetControlID="txtTermEndDate"></cc1:MaskedEditExtender>
                            <asp:RegularExpressionValidator ValidationGroup="editcourse" ID="RegularExpressionValidator2" runat="server" ErrorMessage="This is not a valid date." ControlToValidate="txtTermEndDate" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.][0-9]{4}$">*</asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RegularExpressionValidator2"></cc1:ValidatorCalloutExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" TargetControlID="txtTermEndDate" OnClientDateSelectionChanged="hideCalendar"></cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Capacity
                        </td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtCapacity" runat="server" ValidationGroup="editcourse"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="editcourse" ID="RequiredFieldValidator1" runat="server" ErrorMessage="A capacity is required." ControlToValidate="txtCapacity" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator1"></cc1:ValidatorCalloutExtender>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtCapacity"></cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2" class="tdkey">Short Note:  Required if changing the section number.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdvalue">
                            <asp:TextBox ID="txtShortNote" Width="250px" MaxLength="200" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="tdvalue"></td>
                        <td class="tdvalue">&nbsp;</td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </ContentTemplate>
</asp:UpdatePanel>