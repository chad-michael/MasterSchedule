<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Meetings_Modify2.ascx.cs" Inherits="Controls.Controls_Meetings_Modify2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="SectionID" runat="server" />
        <asp:Panel ID="View3" runat="server">
            <h2>Add New Meeting</h2>
            <p>*Please Select A Location To Populate Rooms*</p>
            <div style="float: left; width: 300px;">
                <table>
                    <tr>
                        <td class="tdkey">Campus
                        </td>
                        <td class="tdvalue" colspan="2">
                            <asp:DropDownList ID="ddAddCampus" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceCampus"
                                DataTextField="CampusCode" DataValueField="CampusesID"
                                ValidationGroup="addmeeting"
                                OnSelectedIndexChanged="ddAddCampus_SelectedIndexChanged1">
                                <%--<asp:ListItem Selected="True" Text="Select Campus" Value="SelectCampus"></asp:ListItem>--%>
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
                            <asp:DropDownList ID="ddAddRoom" runat="server" ValidationGroup="addmeeting">
                                <asp:ListItem Selected="True" Text="TBD" Value="TBD"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceRooms2" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                                SelectCommand="List_Room" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddAddCampus" DefaultValue="0" Name="CampusesID"
                                        PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>

                    <tr>
                        <td class="tdkey" runat="server" id="daysLabel" visible="true">Days
                        </td>
                        <td class="tdvalue" colspan="2" runat="server" id="days" visible="true">
                            <asp:CheckBoxList ID="cblAddDays" runat="server" RepeatDirection="Vertical" ValidationGroup="addmeeting">
                                <asp:ListItem Value="M">Mon.</asp:ListItem>
                                <asp:ListItem Value="T">Tues.</asp:ListItem>
                                <asp:ListItem Value="W">Wed.</asp:ListItem>
                                <asp:ListItem Value="R">Thur.</asp:ListItem>
                                <asp:ListItem Value="F">Fri.</asp:ListItem>
                                <asp:ListItem Value="S">Sat.</asp:ListItem>
                                <asp:ListItem Value="N">Sun.</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>Meeting Type
                        </td>
                        <td>
                            <asp:RadioButtonList ID="MeetingTypeRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="LCLB" Value="LCLB"></asp:ListItem>
                                <asp:ListItem Text="WKP" Value="WKP"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ValidationGroup="addmeeting" ControlToValidate="MeetingTypeRadioButtonList"
                                ID="RequiredFieldValidator1" runat="server" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Meeting Type Required"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey" nowrap runat="server" id="startLabel" visible="true">Start Time
                        </td>
                        <td class="tdvalue" nowrap runat="server" id="startTime" visible="true">
                            <asp:TextBox ID="txtAddStartTime" runat="server" ValidationGroup="addmeeting"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="MaskedEditValidator3" ValidationGroup="addmeeting" runat="server"
                                ControlExtender="MaskedEditExtender3" ControlToValidate="txtAddStartTime" Display="Dynamic"
                                ErrorMessage="This is not a valid time." SetFocusOnError="True">*</cc1:MaskedEditValidator>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtAddStartTime"
                                AcceptAMPM="true" MaskType="Time" Mask="99:99">
                            </cc1:MaskedEditExtender>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="MaskedEditValidator3">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td class="tdvalue" rowspan="2" valign="middle" width="100%"></td>
                    </tr>
                    <tr>
                        <td valign="top" class="multiselectnote">
                            <asp:Image ID="Image2" ImageUrl="~/public/images/information.png" AlternateText="Tip!"
                                ToolTip="Tip!" ImageAlign="AbsMiddle" runat="server" />
                        </td>
                        <td valign="top">
                            <em>Type <strong>P</strong> for AM, or<strong>A</strong> for PM.</em>
                        </td>
                    </tr>

                    <tr>
                        <td class="tdkey" runat="server" id="endLabel" visible="true">End Time
                        </td>
                        <td class="tdvalue" runat="server" id="endTime" visible="true">
                            <asp:TextBox ID="txtAddEndTime" runat="server" ValidationGroup="addmeeting"></asp:TextBox>
                            <cc1:MaskedEditValidator ID="MaskedEditValidator4" ValidationGroup="addmeeting" runat="server"
                                ControlExtender="MaskedEditExtender4" ControlToValidate="txtAddEndTime" Display="Dynamic"
                                ErrorMessage="This is not a valid time." SetFocusOnError="True">*</cc1:MaskedEditValidator>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtAddEndTime"
                                AcceptAMPM="true" MaskType="Time" Mask="99:99">
                            </cc1:MaskedEditExtender>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="MaskedEditValidator4">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdvalue" colspan="3">
                            <asp:Button ID="btnAddMeeting" runat="server" Text="Add Meeting" OnClick="btnAddMeeting_Click"
                                ValidationGroup="addmeeting" />&nbsp;<asp:Button ID="btnAddCancel" runat="server"
                                    Text="Cancel" OnClick="btnAddCancel_Click" ValidationGroup="addmeeting" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: left;">
                <asp:GridView ID="gvExistingMeeting" SkinID="GridView" runat="server" AllowSorting="True"
                    AutoGenerateColumns="False" DataSourceID="sdcExistingMeetiongs" OnRowCommand="gvExistingMeetings_RowCommand">
                    <EmptyDataTemplate>
                        <p style="color: Blue; font-weight: bold;">No previously scheduled meetings exist for this course, or you have requested the removal of all existing meetings.</p>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="Campus" HeaderText="Campus" SortExpression="Campus" />
                        <asp:BoundField DataField="Room" HeaderText="Room No." SortExpression="Room">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Days" SortExpression="Days">
                            <ItemTemplate>
                                <%#FormatMeetDays(Eval("Days")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Time" SortExpression="StartTime">
                            <ItemTemplate>
                                <%#FormatTime(Eval("StartTime"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Time" SortExpression="EndTime">
                            <ItemTemplate>
                                <%#FormatTime(Eval("EndTime"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="StartTime" DataFormatString="{0: hh:mm tt}" HeaderText="Start Time"
                            HtmlEncode="False" SortExpression="StartTime">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EndTime" DataFormatString="{0: hh:mm tt}" HeaderText="End Time"
                            HtmlEncode="False" SortExpression="EndTime">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>--%>

                        <asp:TemplateField HeaderText="" SortExpression="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDeleteMeeting" runat="server" CssClass="deletemeeting" CausesValidation="false">Delete</asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalbg"
                                    CancelControlID="btnCancelDeleteMeeting" DropShadow="true" PopupControlID="pnlDeleteMeeting"
                                    TargetControlID="lnkDeleteMeeting">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlDeleteMeeting" runat="server" CssClass="modalwindow" DefaultButton="btnDeleteMeeting">
                                    <h2>Confirmation</h2>
                                    <p>
                                        Are you sure you want to mark this meeting for deletion?
                                    </p>
                                    <p>
                                        <asp:Button ID="btnDeleteMeeting" runat="server" CausesValidation="false" Text="Yes"
                                            CommandName="deleteExistingMeeting" CommandArgument='<%# Eval("MeetingID") %>' />
                                        &nbsp;<asp:Button
                                            ID="btnCancelDeleteMeeting" runat="server" CausesValidation="false" Text="No" />
                                    </p>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdcExistingMeetiongs" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                    SelectCommand="GetExistingMeetings" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="0000-0000-0000-0000" Name="SectionID" QueryStringField="NewSectionID"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                <asp:GridView ID="gvMeetings" SkinID="GridView" runat="server" AllowSorting="True"
                    AutoGenerateColumns="False" DataSourceID="SqlDataSourceMeetings" OnRowCommand="gvMeetings_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Campus" HeaderText="Campus" SortExpression="Campus" />
                        <asp:BoundField DataField="Room" HeaderText="Room No." SortExpression="Room">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Days" SortExpression="Days">
                            <ItemTemplate>
                                <%#FormatMeetDays(Eval("Days")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="Start Time"
                            HtmlEncode="False" SortExpression="StartTime">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EndTime" DataFormatString="{0:t}" HeaderText="End Time"
                            HtmlEncode="False" SortExpression="EndTime">
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Type" HeaderText="Meeting Type" HtmlEncode="False" SortExpression="Type" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDeleteMeeting" runat="server" CssClass="deletemeeting" CausesValidation="false">Delete</asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalbg"
                                    CancelControlID="btnCancelDeleteMeeting" DropShadow="true" PopupControlID="pnlDeleteMeeting"
                                    TargetControlID="lnkDeleteMeeting">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlDeleteMeeting" runat="server" CssClass="modalwindow" DefaultButton="btnDeleteMeeting">
                                    <h2>Confirmation</h2>
                                    <p>
                                        Are you sure you want to delete this meeting?
                                    </p>
                                    <p>
                                        <asp:Button ID="btnDeleteMeeting" runat="server" CausesValidation="false" Text="Yes"
                                            CommandName="deletemeeting" CommandArgument='<%# Eval("MeetingID") %>' />&nbsp;<asp:Button
                                                ID="btnCancelDeleteMeeting" runat="server" CausesValidation="false" Text="No" />
                                    </p>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceMeetings" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                    SelectCommand="TempMeeting_Fill" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SectionID" DefaultValue="00000000-0000-0000-0000-000000000000"
                            Name="SectionID" PropertyName="Value" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
            <div style="clear: left;">
            </div>
        </asp:Panel>
        <asp:SqlDataSource ID="SqlDataSourceCampus" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
            SelectCommand="List_Campus" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    </ContentTemplate>
</asp:UpdatePanel>