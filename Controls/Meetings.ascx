<%@ Control Language="C#" EnableTheming="true" AutoEventWireup="true" CodeFile="Meetings.ascx.cs" Inherits="Controls.Controls_Meetings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <div style="padding: 2px 0;">
                    <asp:LinkButton ID="lnkAddMeeting" runat="server" CssClass="addmeeting" OnClick="lnkAddMeeting_Click" CausesValidation="false">Add New Meeting</asp:LinkButton>
                </div>
                <asp:GridView ID="gvMeetings" SkinID="GridView" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSourceMeetings" OnRowCommand="gvMeetings_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="CampusCode" HeaderText="Campus" SortExpression="CampusCode" />
                        <asp:BoundField DataField="RoomNumber" HeaderText="Room No." SortExpression="RoomNumber">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Days" SortExpression="MeetDays">
                            <ItemTemplate>
                                <%#FormatMeetDays(Eval("MeetDays")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MeetStartTime" DataFormatString="{0:t}" HeaderText="Start Time"
                            HtmlEncode="False" SortExpression="MeetStartTime">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MeetEndTime" DataFormatString="{0:t}" HeaderText="End Time"
                            HtmlEncode="False" SortExpression="MeetEndTime">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="" SortExpression="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEditMeeting" runat="server" CssClass="editmeeting" CommandName="editmeeting" CommandArgument='<%#Eval("SectionMeetingsID") %>' CausesValidation="false">Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" SortExpression="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDeleteMeeting" runat="server" CssClass="deletemeeting" CausesValidation="false">Delete</asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                                    BackgroundCssClass="modalbg"
                                    CancelControlID="btnCancelDeleteMeeting"
                                    DropShadow="true"
                                    PopupControlID="pnlDeleteMeeting"
                                    TargetControlID="lnkDeleteMeeting">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlDeleteMeeting" runat="server" CssClass="modalwindow" DefaultButton="btnDeleteMeeting">
                                    <h2>Confirmation</h2>
                                    <p>Are you sure you want to mark this meeting for deletion?</p>
                                    <p>
                                        <asp:Button ID="btnDeleteMeeting" runat="server" CausesValidation="false" Text="Yes" CommandName="deletemeeting" CommandArgument='<%# FormatDeleteEntry(Eval("CampusCode"), Eval("RoomNumber"), Eval("MeetDays"), Eval("MeetStartTime"), Eval("MeetEndTime")) %>' />&nbsp;<asp:Button ID="btnCancelDeleteMeeting" runat="server" CausesValidation="false" Text="No" />
                                    </p>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceMeetings" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                    SelectCommand="SELECT [SectionMeetingsID], [MeetDays], [MeetStartTime], [MeetEndTime], [RoomNumber], [CampusCode] FROM [SectionMeetings] WHERE ([SectionsID] = @SectionsID)">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="0" Name="SectionsID" QueryStringField="SectionsID"
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <asp:HiddenField ID="UpdateMeetingID" runat="server" />
                <h2>Edit Meeting</h2>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="tdkey">Campus
                        </td>
                        <td class="tdvalue" colspan="2">
                            <asp:DropDownList ID="ddCampus" runat="server" AutoPostBack="True"
                                DataSourceID="SqlDataSourceCampus" DataTextField="CampusCode"
                                DataValueField="CampusesID" ValidationGroup="editmeeting"
                                OnSelectedIndexChanged="ddCampus_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Building
                        </td>
                        <td class="tdvalue" colspan="2">
                            <asp:DropDownList ID="ddAddBuilding" runat="server" ValidationGroup="addbuilding">
                            </asp:DropDownList>
                            <asp:SqlDataSource
                                ID="BuildingDataSource"
                                runat="server"
                                ConnectionString="<%$ ConnectionStrings:CRP %>"
                                SelectCommand="SELECT [BuildingID], [RoomNumber], [CampusesID] FROM [CRP].[dbo].[Building]"
                                SelectCommandType="Text">
                                <SelectParameters>
                                    <asp:ControlParameter
                                        ControlID="ddAddCampus"
                                        DefaultValue="0"
                                        Name="CampusesID"
                                        PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Room
                        </td>
                        <td class="tdvalue" colspan="2">
                            <asp:DropDownList ID="ddRoom" runat="server" DataSourceID="SqlDataSourceRooms" DataTextField="RoomNumber" DataValueField="RoomsID" ValidationGroup="editmeeting"></asp:DropDownList><asp:SqlDataSource ID="SqlDataSourceRooms" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                                SelectCommand="List_Room" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddCampus" DefaultValue="0" Name="CampusesID" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Days
                            <asp:Label runat="server" ID="lblDays" Text=""></asp:Label>
                        </td>
                        <td class="tdvalue" colspan="2">
                            <asp:CheckBoxList ID="cblDays" runat="server" OnSelectedIndexChanged="cblAddDays_SelectedIndexChanged" RepeatDirection="Horizontal" ValidationGroup="editmeeting">
                                <asp:ListItem Value="N">Sun.</asp:ListItem>
                                <asp:ListItem Value="M">Mon.</asp:ListItem>
                                <asp:ListItem Value="T">Tues.</asp:ListItem>
                                <asp:ListItem Value="W">Wed.</asp:ListItem>
                                <asp:ListItem Value="R">Thur.</asp:ListItem>
                                <asp:ListItem Value="F">Fri.</asp:ListItem>
                                <asp:ListItem Value="S">Sat.</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey" nowrap>Start Time
                        </td>
                        <td class="tdvalue" nowrap>
                            <asp:TextBox ID="txtStartTime" runat="server" ValidationGroup="editmeeting"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="MaskedEditValidator1" ValidationGroup="editmeeting" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtStartTime" Display="Dynamic" ErrorMessage="This is not a valid time." SetFocusOnError="True">*</cc1:MaskedEditValidator>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartTime" AcceptAMPM="true" MaskType="Time" Mask="99:99"></cc1:MaskedEditExtender>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="MaskedEditValidator1"></cc1:ValidatorCalloutExtender>
                        </td>
                        <td class="tdvalue" rowspan="2" valign="middle" width="100%">
                            <div class="multiselectnote">
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <asp:Image ID="Image1" ImageUrl="~/public/images/information.png" AlternateText="Tip!" ToolTip="Tip!" ImageAlign="AbsMiddle" runat="server" /></td>
                                        <td valign="top"><em>Type <strong>A</strong> for AM, or<br />
                                            <strong>P</strong> for PM.</em></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">End Time
                        </td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtEndTime" runat="server" ValidationGroup="editmeeting"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="MaskedEditValidator2" ValidationGroup="editmeeting" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtEndTime" Display="Dynamic" ErrorMessage="This is not a valid time." SetFocusOnError="True">*</cc1:MaskedEditValidator>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtEndTime" AcceptAMPM="true" MaskType="Time" Mask="99:99"></cc1:MaskedEditExtender>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="MaskedEditValidator2"></cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <!-- Added 11.15.2007  Cyrus Loree -->
                    <tr>
                        <td class="tdkey">Section Start<asp:Label runat="server" ID="lblStart" Text=""></asp:Label>
                        </td>
                        <td class="tdvalue">
                            <div>
                                <asp:TextBox ID="txtEditTermStartDate" runat="server" ValidationGroup="editcourse"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" MaskType="Date" Mask="99/99/9999" UserDateFormat="MonthDayYear" TargetControlID="txtEditTermStartDate"></cc1:MaskedEditExtender>
                                <asp:RegularExpressionValidator ValidationGroup="editcourse" ID="RegularExpressionValidator3" runat="server" ErrorMessage="This is not a valid date." ControlToValidate="txtEditTermStartDate" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.][0-9]{4}$">*</asp:RegularExpressionValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RegularExpressionValidator1"></cc1:ValidatorCalloutExtender>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Animated="true" TargetControlID="txtEditTermStartDate" OnClientDateSelectionChanged="hideCalendar"></cc1:CalendarExtender>
                            </div>
                        </td>
                        <td class="tdvalue" rowspan="2" valign="middle" width="100%">
                            <div class="multiselectnote">
                                <table id="updateDateError" runat="server" visible="false">
                                    <tr>
                                        <td valign="top" style="height: 41px">
                                            <asp:Image ID="Image3" ImageUrl="~/public/images/information.png" AlternateText="Tip!" ToolTip="Tip!" ImageAlign="AbsMiddle" runat="server" /></td>
                                        <td valign="top" style="height: 41px; color: Red; font-weight: bolder;"><em>The Section Start Date and Start Day do not match!<br />
                                            Correct this before proceeding.</em></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Section End
                        </td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtEditTermEndDate" runat="server" ValidationGroup="editcourse"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" MaskType="Date" Mask="99/99/9999" UserDateFormat="MonthDayYear" TargetControlID="txtEditTermEndDate"></cc1:MaskedEditExtender>
                            <asp:RegularExpressionValidator ValidationGroup="editcourse" ID="RegularExpressionValidator4" runat="server" ErrorMessage="This is not a valid date." ControlToValidate="txtEditTermEndDate" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.][0-9]{4}$">*</asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RegularExpressionValidator2"></cc1:ValidatorCalloutExtender>
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Animated="true" TargetControlID="txtEditTermEndDate" OnClientDateSelectionChanged="hideCalendar"></cc1:CalendarExtender>
                        </td>
                    </tr>
                    <!-- End Add -->
                    <tr>
                        <td class="tdvalue"></td>
                        <td class="tdvalue" colspan="2">
                            <asp:Button ID="btnUpdateMeeting" runat="server" Text="Save" OnClick="btnUpdateMeeting_Click" ValidationGroup="editmeeting" />&nbsp;<asp:Button ID="btnUpdateCancel" runat="server" Text="Cancel" OnClick="btnUpdateCancel_Click" ValidationGroup="editmeeting" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <h2>Add New Meeting</h2>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="tdkey">Campus
                        </td>
                        <td class="tdvalue" colspan="2">
                            <asp:DropDownList ID="ddAddCampus" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceCampus" DataTextField="CampusCode" DataValueField="CampusesID" ValidationGroup="addmeeting"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Room
                        </td>
                        <td class="tdvalue" colspan="2">
                            <asp:DropDownList ID="ddAddRoom" runat="server" DataSourceID="SqlDataSourceRooms2" DataTextField="RoomNumber" DataValueField="RoomsID" ValidationGroup="addmeeting"></asp:DropDownList><asp:SqlDataSource ID="SqlDataSourceRooms2" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                                SelectCommand="List_Room" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddAddCampus" DefaultValue="0" Name="CampusesID"
                                        PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Days
                        </td>
                        <td class="tdvalue" colspan="2">
                            <asp:CheckBoxList ID="cblAddDays" runat="server" RepeatDirection="Horizontal" ValidationGroup="addmeeting" OnSelectedIndexChanged="cblAddDays_SelectedIndexChanged">
                                <asp:ListItem Value="N">Sun.</asp:ListItem>
                                <asp:ListItem Value="M">Mon.</asp:ListItem>
                                <asp:ListItem Value="T">Tues.</asp:ListItem>
                                <asp:ListItem Value="W">Wed.</asp:ListItem>
                                <asp:ListItem Value="R">Thur.</asp:ListItem>
                                <asp:ListItem Value="F">Fri.</asp:ListItem>
                                <asp:ListItem Value="S">Sat.</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey" nowrap>Start Time
                        </td>
                        <td class="tdvalue" nowrap>
                            <asp:TextBox ID="txtAddStartTime" runat="server" ValidationGroup="addmeeting"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="MaskedEditValidator3" ValidationGroup="addmeeting" runat="server" ControlExtender="MaskedEditExtender3" ControlToValidate="txtAddStartTime" Display="Dynamic" ErrorMessage="This is not a valid time." SetFocusOnError="True">*</cc1:MaskedEditValidator>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtAddStartTime" AcceptAMPM="true" MaskType="Time" Mask="99:99"></cc1:MaskedEditExtender>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="MaskedEditValidator3"></cc1:ValidatorCalloutExtender>
                        </td>
                        <td class="tdvalue" rowspan="2" valign="middle" width="100%">
                            <div class="multiselectnote">
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <asp:Image ID="Image2" ImageUrl="~/public/images/information.png" AlternateText="Tip!" ToolTip="Tip!" ImageAlign="AbsMiddle" runat="server" /></td>
                                        <td valign="top"><em>Type <strong>A</strong> for AM, or<br />
                                            <strong>P</strong> for PM.</em></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">End Time
                        </td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtAddEndTime" runat="server" ValidationGroup="addmeeting"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="MaskedEditValidator4" ValidationGroup="addmeeting" runat="server" ControlExtender="MaskedEditExtender4" ControlToValidate="txtAddEndTime" Display="Dynamic" ErrorMessage="This is not a valid time." SetFocusOnError="True">*</cc1:MaskedEditValidator>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtAddEndTime" AcceptAMPM="true" MaskType="Time" Mask="99:99"></cc1:MaskedEditExtender>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="MaskedEditValidator4"></cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <!-- Added 11.15.2007 Cyrus Loree -->
                    <tr>
                        <td class="tdkey">Section Start
                        </td>
                        <td class="tdvalue">
                            <div>
                                <asp:TextBox ID="txtAddTermStartDate" runat="server" ValidationGroup="editcourse"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" MaskType="Date" Mask="99/99/9999" UserDateFormat="MonthDayYear" TargetControlID="txtAddTermStartDate"></cc1:MaskedEditExtender>
                                <asp:RegularExpressionValidator ValidationGroup="editcourse" ID="RegularExpressionValidator1" runat="server" ErrorMessage="This is not a valid date." ControlToValidate="txtAddTermStartDate" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.][0-9]{4}$">*</asp:RegularExpressionValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RegularExpressionValidator1"></cc1:ValidatorCalloutExtender>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" TargetControlID="txtAddTermStartDate" OnClientDateSelectionChanged="hideCalendar"></cc1:CalendarExtender>
                            </div>
                        </td>
                        <td class="tdvalue" rowspan="2" valign="middle" width="100%">
                            <div class="multiselectnote">
                                <table id="insertDateError" runat="server" visible="false">
                                    <tr>
                                        <td valign="top" style="height: 41px">
                                            <asp:Image ID="Image4" ImageUrl="~/public/images/information.png" AlternateText="Tip!" ToolTip="Tip!" ImageAlign="AbsMiddle" runat="server" /></td>
                                        <td valign="top" style="height: 41px; color: Red; font-weight: bolder;"><em>The Section Start Date and Start Day do not match!<br />
                                            Correct this before proceeding.</em></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Section End
                        </td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtAddTermEndDate" runat="server" ValidationGroup="editcourse"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" MaskType="Date" Mask="99/99/9999" UserDateFormat="MonthDayYear" TargetControlID="txtAddTermEndDate"></cc1:MaskedEditExtender>
                            <asp:RegularExpressionValidator ValidationGroup="editcourse" ID="RegularExpressionValidator2" runat="server" ErrorMessage="This is not a valid date." ControlToValidate="txtTermEndDate" Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.][0-9]{4}$">*</asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RegularExpressionValidator2"></cc1:ValidatorCalloutExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" TargetControlID="txtTermEndDate" OnClientDateSelectionChanged="hideCalendar"></cc1:CalendarExtender>
                        </td>
                    </tr>
                    <!-- End Add -->
                    <tr>
                        <td class="tdvalue"></td>
                        <td class="tdvalue" colspan="2">
                            <asp:Button ID="btnAddMeeting" runat="server" Text="Save" OnClick="btnAddMeeting_Click" ValidationGroup="addmeeting" />&nbsp;<asp:Button ID="btnAddCancel" runat="server" Text="Cancel" OnClick="btnAddCancel_Click" ValidationGroup="addmeeting" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>

        <asp:SqlDataSource ID="SqlDataSourceCampus" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>" SelectCommand="List_Campus" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    </ContentTemplate>
</asp:UpdatePanel>