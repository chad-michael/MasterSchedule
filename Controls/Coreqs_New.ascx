<%@ Control Language="C#" EnableTheming="True" AutoEventWireup="true" CodeFile="Coreqs_New.ascx.cs" Inherits="Controls.Controls_Coreqs_New" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="SectionID" runat="server" />

        <asp:Panel ID="View2" runat="server">
            <h2>Add Co-requisite/Linked</h2>
            <div style="float: left; width: 300px;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="tdkey">Department:</td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdkey">Catalog Number:</td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtCatalogNumber" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tdkey">Section Number:</td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtSectionNumber" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>

                        <td class="tdvalue" colspan="2">
                            <asp:Button ID="btnAdd" runat="server" Text="Add Coreq/Link" OnClick="btnAdd_Click" CausesValidation="false" />&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: left; padding-left: 5px;">
                <asp:GridView SkinID="GridView" ID="gvCoreq" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSourceCoreq" OnRowCommand="gvCoreq_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                        <asp:BoundField DataField="Course" HeaderText="Catalog No." SortExpression="Course" />
                        <asp:BoundField DataField="Section" HeaderText="Section No." SortExpression="Section" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDeleteRecord" CssClass="deletecoreq" runat="server" CausesValidation="false">Delete</asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                                    BackgroundCssClass="modalbg"
                                    CancelControlID="btnCancelDeleteRecord"
                                    DropShadow="true"
                                    PopupControlID="pnlDeleteRecord"
                                    TargetControlID="lnkDeleteRecord">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlDeleteRecord" runat="server" CssClass="modalwindow" DefaultButton="btnDeleteRecord">
                                    <h2>Confirmation</h2>
                                    <p>Are you sure you want to delete this coreq/link?</p>
                                    <p>
                                        <asp:Button ID="btnDeleteRecord" runat="server" CausesValidation="false" Text="Yes" CommandName="deletecoreq" CommandArgument='<%#Eval("CoreqID") %>' />&nbsp;<asp:Button ID="btnCancelDeleteRecord" runat="server" CausesValidation="false" Text="No" />
                                    </p>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div style="clear: left;"></div>
            <asp:SqlDataSource ID="SqlDataSourceCoreq" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                SelectCommand="TempCoreq_Fill" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SectionID" DefaultValue="00000000-0000-0000-0000-000000000000"
                        Name="SectionID" PropertyName="Value" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>