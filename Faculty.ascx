<%@ Control Language="C#" EnableTheming="true" AutoEventWireup="true" CodeFile="Faculty.ascx.cs"
    Inherits="Controls_Faculty" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <div style="padding: 2px 0;">
                    <br />
                    <asp:LinkButton ID="lnkAddFaculty" runat="server" CssClass="addfaculty" OnClick="lnkAddFaculty_Click">Add Faculty</asp:LinkButton>
                </div>
                <asp:GridView ID="gvFacultyAssignment" SkinID="GridView" runat="server" AutoGenerateColumns="False"
                    DataSourceID="SqlDataSourceFaculty" OnRowCommand="gvFacultyAssignment_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="FullName" HeaderText="Faculty Name" ReadOnly="True" SortExpression="FullName" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDeleteFaculty" CssClass="deletefaculty" runat="server" CausesValidation="false">Delete</asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalbg"
                                    CancelControlID="btnCancelDeleteFaculty" DropShadow="true" PopupControlID="pnlDeleteFaculty"
                                    TargetControlID="lnkDeleteFaculty">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlDeleteFaculty" runat="server" CssClass="modalwindow" DefaultButton="btnDeleteFaculty">
                                    <h2>Confirmation</h2>
                                    <p>
                                        Are you sure you want to mark this person for deletion?
                                    </p>
                                    <p>
                                        <asp:Button ID="btnDeleteFaculty" runat="server" CausesValidation="false" Text="Yes"
                                            CommandName="deletefaculty" CommandArgument='<%#Eval("FullName") %>' />&nbsp;<asp:Button
                                                ID="btnCancelDeleteFaculty" runat="server" CausesValidation="false" Text="No" />
                                    </p>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceFaculty" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                    SelectCommand="List_FacultyAssignments" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="0" Name="SectionsID" QueryStringField="SectionsID"
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <h2>Add Faculty</h2>
                <br />
                <asp:Label ID="Label1" runat="server" Text="Add Name: " Visible="False"></asp:Label>
                <asp:TextBox ID="txtFacultyName" runat="server"></asp:TextBox>
                <h2>

                    <table>
                        <tr>
                            <td valign="top">
                                <asp:ListBox ID="lbFacultyUnassigned" ValidationGroup="addfaculty" runat="server"
                                    Width="250px" Height="250px" DataSourceID="SqlDataSourceFacultyUnassigned" DataTextField="FullName"
                                    DataValueField="IDNO" SelectionMode="Multiple"></asp:ListBox><asp:SqlDataSource ID="SqlDataSourceFacultyUnassigned"
                                        runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>" SelectCommand="List_FacultyUnassigned"
                                        SelectCommandType="StoredProcedure">
                                        <%--<SelectParameters>
                                            <asp:QueryStringParameter DefaultValue="0" Name="SectionsID" QueryStringField="SectionsID"
                                                Type="Int32" />
                                        </SelectParameters>--%>
                                    </asp:SqlDataSource>
                            </td>
                            <td valign="top">
                                <div class="multiselectnote">
                                    <table>
                                        <tr>
                                            <td valign="top">
                                                <asp:Image ID="Image1" ImageUrl="~/public/images/information.png" AlternateText="Tip!"
                                                    ToolTip="Tip!" ImageAlign="AbsMiddle" runat="server" /></td>
                                            <td valign="top">
                                                <em>To select more than one person, hold down the <strong>Ctrl</strong> key.</em></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnAddFaculty" runat="server" Text="Add" OnClick="btnAddFaculty_Click"
                                    ValidationGroup="addfaculty" />&nbsp;<asp:Button ID="btnDone" runat="server" Text="Done"
                                        OnClick="btnDone_Click" ValidationGroup="addfaculty" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </h2>
            </asp:View>
        </asp:MultiView>
    </ContentTemplate>
</asp:UpdatePanel>
&nbsp; 