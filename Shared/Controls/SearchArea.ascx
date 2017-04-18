<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchArea.ascx.cs" Inherits="Shared.Controls.Controls_SearchArea" %>
<select name="QuickLink" onchange="window.location=this.form.QuickLink.value;">
    <option value="#" selected="selected">Quicklinks</option>
    <option value="http://www.delta.edu/academicservices/">Academic Divisions</option>
    <option value="/bookstore.aspx">Bookstore</option>
    <option value="https://elearning.delta.edu/">eLearning</option>
    <option value="http://webmail.delta.edu">Email</option>
    <option value="/llic.aspx">Library Learning Information Center</option>
    <option value="https://my.delta.edu">MyDelta</option>
    <option value="/pages/177.aspx">Payment Options</option>
    <%-- <option value="/planet">Planetarium</option>--%>
    <option value="/pages/10930.aspx">Pool / Fitness</option>
    <%--<option value="/broadcasting">Quality Public Broadcasting</option>--%>
    <option value="http://www.delta.edu/pages/12086.aspx">Student Services</option>
</select>
<input runat="server" id="searchbox" class="searchbox" type="text" name="" size="20"
    maxlength="255" value="Search..." onclick="this.value = '';" />
<asp:ImageButton runat="server" CssClass="searchButton" ID="sa" ToolTip="Go" AlternateText="Go"
    ImageUrl="/images/system/search_gobutton.gif" Width="21" Height="19" BorderStyle="None"
    OnClick="sa_Click" />