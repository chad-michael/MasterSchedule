<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LSBGroupContainer.ascx.cs"
    Inherits="Shared.Controls.Controls_LSBGroupContainer" %>
<div class="ILeftSideBarUserControl" style="margin-top: 18px;">
    <h3>
        <asp:Label runat="server" ID="lblHeader"></asp:Label>
    </h3>
    <ComponentArt:Menu ID="Menu1" Orientation="vertical" DefaultGroupItemSpacing="4" OverlayWindowedElements="true"
        Width="200" DefaultGroupWidth="200" AutoPostBackOnSelect="false" DefaultGroupCssClass="SubGroup"
        DefaultItemLookId="DefaultItemLook" ExpandDelay="100" ImagesBaseUrl="/images/system/"
        EnableViewState="true" runat="server">
        <ItemLooks>
            <ComponentArt:ItemLook LookId="TopItemLook" CssClass="Item" HoverCssClass="ItemH"
                ExpandedCssClass="ItemExp" LabelPaddingLeft="5" LabelPaddingRight="15" LabelPaddingTop="4"
                LabelPaddingBottom="4" RightIconUrl="arrow.gif" RightIconWidth="15" RightIconHeight="10"
                RightIconVisibility="WhenExpandable" />
            <ComponentArt:ItemLook LookId="EmptyTopItemLook" CssClass="EmptyItem" HoverCssClass="EmptyItemH"
                ExpandedCssClass="EmptyItemExp" LabelPaddingLeft="5" LabelPaddingRight="15" LabelPaddingTop="4"
                LabelPaddingBottom="4" RightIconUrl="arrow.gif" RightIconWidth="15" RightIconHeight="10"
                RightIconVisibility="WhenExpandable" />
            <ComponentArt:ItemLook LookId="DefaultItemLook" CssClass="MenuItem" RightIconUrl="arrow.gif"
                HoverCssClass="MenuItemHover" ExpandedCssClass="MenuItemHover" RightIconVisibility="WhenExpandable"
                LabelPaddingLeft="18" LabelPaddingRight="12" LabelPaddingTop="3" RightIconWidth="15" RightIconHeight="10"
                LabelPaddingBottom="4" />
            <ComponentArt:ItemLook LookId="EmptyItemLook" CssClass="EmptyMenuItem" RightIconUrl="arrow.gif"
                HoverCssClass="EmptyMenuItemHover" ExpandedCssClass="EmptyMenuItemHover" RightIconVisibility="WhenExpandable"
                LabelPaddingLeft="18" LabelPaddingRight="12" LabelPaddingTop="3" RightIconWidth="15" RightIconHeight="10"
                LabelPaddingBottom="4" />
            <ComponentArt:ItemLook LookId="BreakItem" ImageUrl="break.gif" CssClass="MenuBreak"
                ImageHeight="2" ImageWidth="100%" />
        </ItemLooks>
    </ComponentArt:Menu>
</div>