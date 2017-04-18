<%@ Page Language="C#" MasterPageFile="~/public/Default.master" Theme="Default"
 AutoEventWireup="true" CodeFile="ProcessScreen.aspx.cs" Inherits="ProcessScreen" 
 Title="Master Schedule - Processing" %>

<%@ Register Src="Controls/ApprovalGrid.ascx" TagName="ApprovalGrid" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    <hr />
     <uc1:ApprovalGrid ProcessName="bookstore" ScreenTitle="Bookstore Processing" StatusFilter="pending"
     id="ApprovalGrid2" runat="server">
    </uc1:ApprovalGrid>
    
    <hr />
     <uc1:ApprovalGrid ProcessName="humanresources" ScreenTitle="Human Resources" StatusFilter="pending"
     id="ApprovalGrid3" runat="server">
    </uc1:ApprovalGrid>
    
    <hr />
     <uc1:ApprovalGrid ProcessName="roomscheduling" ScreenTitle="Room Scheduling" StatusFilter="pending"
     id="ApprovalGrid4" runat="server">
    </uc1:ApprovalGrid>



</asp:Content>

