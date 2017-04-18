<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Faculty_New.ascx.cs" Inherits="Controls.Controls_Faculty_New" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Panel ID="View2" runat="server">
    <h2>Add Faculty</h2>
    <div style="float: left; width: 300px;">
        <%--AUTO COMPLETE BOX--%>
        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
            MinimumPrefixLength="3" ServiceMethod="GetCompletionList" ServicePath="~/AutoSuggest.asmx"
            TargetControlID="txtFaculty" />
        <asp:TextBox ID="txtFaculty" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="selectionErrorLabel" runat="server" Text="Please select a faulty memeber." ForeColor="Red" Visible="false"></asp:Label>
        <br />
        <asp:Button Text="Add Faculty" ID="btnAddFaculty" OnClick="btnAddFaculty_Click" runat="server" CausesValidation="false" />
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">

                <asp:GridView ID="gvFacultyAssignment" SkinID="GridView" runat="server" AutoGenerateColumns="False"
                    DataSourceID="SqlDataSourceFaculty" OnRowCommand="gvFacultyAssignment_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="FacultyName" HeaderText="Faculty Name" ReadOnly="True"
                            SortExpression="FacultyName" />
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
                                        Are you sure you want to delete
                                <%#Eval("FacultyName") %>?
                                    </p>
                                    <p>
                                        <asp:Button ID="btnDeleteFaculty" runat="server" CausesValidation="false" Text="Yes"
                                            CommandName="deletefaculty" CommandArgument='<%#Eval("FacultyID") %>' />&nbsp;<asp:Button
                                                ID="btnCancelDeleteFaculty" runat="server" CausesValidation="false" Text="No" />
                                    </p>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:SqlDataSource ID="SqlDataSourceFaculty" runat="server" ConnectionString="<%$ ConnectionStrings:MasterSchedule %>"
                SelectCommand="TempFaculty_Fill" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="NewSectionID" DefaultValue="00000000-0000-0000-0000-000000000000"
                        Name="SectionID" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="clear: left;">
    </div>
</asp:Panel>