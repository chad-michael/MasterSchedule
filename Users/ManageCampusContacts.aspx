<%@ Page Language="C#" StylesheetTheme="Default" MasterPageFile="~/public/Default.master" AutoEventWireup="true"
    CodeFile="ManageCampusContacts.aspx.cs" Inherits="Users.Users_Users_AddDivChair" Title="Add Division Role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CRP %>"
        SelectCommand="SELECT [CampusesID], [CampusCode], [CampusName], [CampusDesc] FROM [Campuses] ORDER BY [CampusCode]"></asp:SqlDataSource>

    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Blue" Text="Division:"></asp:Label>&nbsp;
    <asp:DropDownList ID="ddlDivision" runat="server" DataSourceID="SqlDataSource1" DataTextField="CampusCode"
        DataValueField="CampusCode" AutoPostBack="True">
    </asp:DropDownList><br />
    <div style="margin-top: 10px;">

        <div style="float: left; height: 167px;">
            People currently configured as a contact for a campus.

            <asp:GridView ID="GridView1" SkinID="GridView" runat="server"
                AutoGenerateColumns="False" EnableModelValidation="True"
                DataSourceID="SqlDataSource2">
                <Columns>
                    <asp:BoundField DataField="MeetingCenter" HeaderText="MeetingCenter"
                        SortExpression="MeetingCenter" />
                    <asp:BoundField DataField="ContactIDNO" HeaderText="ContactIDNO"
                        SortExpression="ContactIDNO" />
                    <asp:BoundField DataField="FirstNM" HeaderText="FirstNM"
                        SortExpression="FirstNM" />
                    <asp:BoundField DataField="LastNM" HeaderText="LastNM"
                        SortExpression="LastNM" />
                    <asp:BoundField DataField="DeltaEmail" HeaderText="DeltaEmail"
                        SortExpression="DeltaEmail" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnDeleteMe" CommandArgument='<%#Eval("MeetingCenterContactID")%>' OnClick="btnDeleteMe_Click" Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No users are configured for this campus.
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                SelectCommand="SELECT [MeetingCenterContactID], [MeetingCenter], [ContactIDNO], B.FirstNM, B.LastNM, C.DeltaEmail FROM [MeetingCenterContact]  AS A
LEFT JOIN ERP.dbo.BIOS AS B ON A.ContactIDNO = B.IDNO
LEFT JOIN ERP.dbo.UNAMES AS C ON B.IDNO = C.IDNO

WHERE ([MeetingCenter] = @MeetingCenter)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlDivision" Name="MeetingCenter"
                        PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
        <div style="float: left; margin-left: 25px;">
            To add a person as a campus contact,<br />
            select the campus from the drop down<br />
            then enter the employee's Delta ID Number<br />
            and click the Add User button.
    <br />
            <br />
            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Text="ID Number:"></asp:Label>
            <asp:TextBox ID="txtIDNO" runat="server"></asp:TextBox>&nbsp;
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
                Text="Add User" />
        </div>
    </div>
</asp:Content>