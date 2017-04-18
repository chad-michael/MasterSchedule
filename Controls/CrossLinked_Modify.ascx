<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CrossLinked_Modify.ascx.cs" Inherits="Controls.Controls_CrossLinked_Modify" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="SectionID" runat="server" />

        <asp:Panel ID="View1" runat="server">
            <h2>Add Linked Course</h2>
            <div style="float: left; width: 300px;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="tdkey">Action:</td>
                        <td class="tdvalue">
                            <asp:RadioButtonList ID="rblAction" runat="server"
                                RepeatDirection="Horizontal">
                                <asp:ListItem>Add</asp:ListItem>
                                <asp:ListItem>Drop</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ValidationGroup="cross" ControlToValidate="rblAction" ID="rfvCoreqAction" runat="server" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Action Type Required.">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceRBLAction" runat="server" TargetControlID="rfvCoreqAction">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Department:</td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="cross" ControlToValidate="txtDepartment" ID="rfvCrossDept" runat="server" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Department Required.">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceCrossDept" runat="server" TargetControlID="rfvCrossDept">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Catalog Number:</td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtCatalogNumber" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="cross" ControlToValidate="txtCatalogNumber" ID="rfvCrossCat" runat="server" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Catalog Number Required.">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceCrossCat" runat="server" TargetControlID="rfvCrossCat">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdkey">Section Number:</td>
                        <td class="tdvalue">
                            <asp:TextBox ID="txtSectionNumber" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="cross" ControlToValidate="txtSectionNumber" ID="rfvCrossSect" runat="server" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Section Number Required.">*</asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceCrossSect" runat="server" TargetControlID="rfvCrossSect">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdvalue" colspan="2">
                            <asp:Button ID="btnAdd" runat="server" Text="Update Link" OnClick="btnAdd_Click" CausesValidation="true" ValidationGroup="cross" />&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="float: left;">
                <asp:GridView SkinID="GridView" ID="gvXLink" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSourceXLink" OnRowCommand="gvXLink_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Action" HeaderText="Action" SortExpression="Action" />
                        <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                        <asp:BoundField DataField="Course" HeaderText="Catalog No." SortExpression="Course" />
                        <asp:BoundField DataField="Section" HeaderText="Section No." SortExpression="Section" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDeleteRecord" CssClass="deletexlink" runat="server" CausesValidation="false">Delete</asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                                    BackgroundCssClass="modalbg"
                                    CancelControlID="btnCancelDeleteRecord"
                                    DropShadow="true"
                                    PopupControlID="pnlDeleteRecord"
                                    TargetControlID="lnkDeleteRecord">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlDeleteRecord" runat="server" CssClass="modalwindow" DefaultButton="btnDeleteRecord">
                                    <h2>Confirmation</h2>
                                    <p>Are you sure you want to delete this linked course?</p>
                                    <p>
                                        <asp:Button ID="btnDeleteRecord" runat="server" CausesValidation="false" Text="Yes" CommandName="deletexlink" CommandArgument='<%#Eval("LinkID") %>' />&nbsp;<asp:Button ID="btnCancelDeleteRecord" runat="server" CausesValidation="false" Text="No" />
                                    </p>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceXLink" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                    SelectCommand="TempLink_Fill" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SectionID" DefaultValue="00000000-0000-0000-0000-000000000000"
                            Name="SectionID" PropertyName="Value" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
            <div style="clear: left;"></div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>