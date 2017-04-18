<%@ Page Language="C#" StylesheetTheme="Default" MasterPageFile="~/public/Default.master" AutoEventWireup="true"
    CodeFile="AddDivChair.aspx.cs" Inherits="Users.Users_Users_AddDivChair" Title="Add Division Role" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CRP %>"
        SelectCommand="SELECT '( ' + RTRIM(DivisionCode) + ' ) ' + DivisionDesc AS DivisionDesc, DivisionsID FROM Divisions WHERE (DivisionsID > @DivisionsID) ORDER BY DivisionDesc">
        <SelectParameters>
            <asp:Parameter DefaultValue="12" Name="DivisionsID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Blue" Text="Division:"></asp:Label>&nbsp;
    <asp:DropDownList ID="ddlDivision" runat="server" DataSourceID="SqlDataSource1" DataTextField="DivisionDesc"
        DataValueField="DivisionsID" AutoPostBack="True">
    </asp:DropDownList><br />
    <div style="margin-top: 10px;">

        <div style="float: left;">
            People currently configured as a division chair approval level for the selected division.

            <asp:GridView ID="GridView1" SkinID="GridView" runat="server"
                AutoGenerateColumns="False" DataSourceID="SqlDataSource2" DataKeyNames="DivisionChairsID"
                EnableModelValidation="True">
                <Columns>
                    <asp:BoundField DataField="DivisionName" HeaderText="DivisionName" SortExpression="DivisionName" />
                    <asp:BoundField DataField="FirstNM" HeaderText="FirstNM" SortExpression="FirstNM" />
                    <asp:BoundField DataField="LastNM" HeaderText="LastNM" SortExpression="LastNM" />
                    <asp:CommandField ShowDeleteButton="true" />
                </Columns>
                <EmptyDataTemplate>
                    No users are configured for this division.
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                SelectCommand="SELECT DivisionChairsID, DivisionChairs.DivisionName, ERP.dbo.Bios.FirstNM, ERP.dbo.Bios.LastNM FROM DivisionChairs INNER JOIN ERP.dbo.Bios ON DivisionChairs.IDNO = ERP.dbo.Bios.IDNO WHERE (DivisionChairs.CRPDivisionID = @DivisonID)"
                DeleteCommand="DELETE FROM DivisionChairs WHERE DivisionChairsID = @DivisionChairsID">
                <DeleteParameters>
                    <asp:Parameter Name="DivisionChairsID" Type="Int32" />
                </DeleteParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlDivision" Name="DivisonID" PropertyName="SelectedValue" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
        <div style="float: left; margin-left: 25px;">
            To add a person in the Division Chair Role,<br />
            select the specific Division from the drop down,<br />
            then enter the employee's Delta ID Number<br />
            and click the Add User button.
    <br />
            <br />
            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Text="ID Number:"></asp:Label>
            <asp:TextBox ID="txtIDNO" runat="server"></asp:TextBox>&nbsp;
    <asp:ImageButton runat="server" ImageUrl="~/images/application_add.png"
        ID="btnLookupID"
        AlternateText="Click to lookup an ID number"
        ToolTip="Click here to lookup an ID number" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                BackgroundCssClass="modalbg"
                CancelControlID="btnCancelLookupID"
                DropShadow="true"
                PopupControlID="pnlLookup"
                TargetControlID="btnLookupID">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlDeleteRecord" runat="server">
            </asp:Panel>
            <asp:Panel ID="pnlLookup" CssClass="modalwindow" runat="server">
                <asp:UpdatePanel runat="server" ID="pnlDynamicSearch" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        Enter in first and last name information.<br />
                        <br />
                        <table>
                            <tr>
                                <td>First Name:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" Width="100" ID="txtFirstName" />
                                </td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>Last Name:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" Width="100" ID="txtLastName" />
                                </td>
                            </tr>
                            <tr>
                                <td>Username:</td>
                                <td colspan="4">
                                    <asp:TextBox runat="server" Width="305" ID="txtUserName" /></td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button runat="server" ID="btnGetFaculty" Text="Search for Faculty or Staff" OnClick="btnGetFaculty_Click" />
                        <br />
                        <br />
                        <div style="width: 100%;">
                            <asp:GridView runat="server" ID="gvStudents" CellPadding="4" ForeColor="#333333"
                                GridLines="None" AutoGenerateColumns="false" EmptyDataText="There are no faculty or staff who meet search criteria."
                                AllowPaging="true" PageSize="10" Width="100%"
                                OnPageIndexChanging="gvStudents_PageChanging" OnSelectedIndexChanged="gvStudents_SelectedIndexChanged">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField HeaderText="Delta ID" DataField="DeltaID" />
                                    <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                                    <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                                    <asp:BoundField HeaderText="Gender" DataField="Gender" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="btnSelectFaculty" OnClick="btnSelectFaculty_Clicked"
                                                CommandArgument='<%# Eval("DeltaID") %>' ImageUrl="~/images/accept.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGetFaculty" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <p>
                    <asp:Button ID="btnCancelLookupID" runat="server" CausesValidation="false" Text="Cancel" />
                </p>
            </asp:Panel>
            <div class="multiselectnote" runat="server" visible="false" id="errorBox">
                <table id="tblProcessError">
                    <tr>
                        <td valign="top" style="height: 41px">
                            <asp:Image ID="Image5" ImageUrl="~/public/images/information.png" ImageAlign="AbsMiddle" runat="server" /></td>
                        <td valign="top" style="height: 41px; color: Red; font-weight: bolder;">
                            <em>The request was not processed, please verfiy that the ID Number entered only contains numbers, and try again.</em></td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <asp:Button ID="btnAddDivChair" runat="server" OnClick="btnAddDivChair_Click"
                Text="Add User" />&nbsp;&nbsp;

    <asp:Button ID="btnPreviewUserRoles" runat="server" Text="Lookup User Roles" /><br />
            <h3>All roles for selected user:</h3>
            <asp:GridView ID="GridView2" SkinID="GridView" runat="server"
                AutoGenerateColumns="False" DataSourceID="sqlRolePreview"
                EnableModelValidation="True">
                <Columns>
                    <asp:BoundField DataField="DivisionName" HeaderText="DivisionName" SortExpression="DivisionName" />
                    <asp:BoundField DataField="FirstNM" HeaderText="FirstNM" SortExpression="FirstNM" />
                    <asp:BoundField DataField="LastNM" HeaderText="LastNM" SortExpression="LastNM" />
                </Columns>
                <EmptyDataTemplate>
                </EmptyDataTemplate>
            </asp:GridView>
            <br />
            <asp:SqlDataSource ID="sqlRolePreview" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                SelectCommand="SELECT DivisionChairsID, DivisionChairs.DivisionName, ERP.dbo.Bios.FirstNM, ERP.dbo.Bios.LastNM FROM DivisionChairs INNER JOIN ERP.dbo.Bios ON DivisionChairs.IDNO = ERP.dbo.Bios.IDNO WHERE (ERP.dbo.Bios.IDNO = @IDNO)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtIDNO" Name="IDNO" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>