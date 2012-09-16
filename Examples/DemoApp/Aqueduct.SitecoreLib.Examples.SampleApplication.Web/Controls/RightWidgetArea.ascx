<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RightWidgetArea.ascx.cs"
    Inherits="Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls.WidgetArea" %>
<%@ Register TagPrefix="safc" Namespace="SAFC.Site.layouts.Widgets" Assembly="SAFC.Site" %>
<%@ Register TagPrefix="safc" Namespace="Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls.Widgets" Assembly="Aqueduct.SitecoreLib.Examples.SampleApplication.Web" %>
<div id="relatedContent" class="aside span-3-col col">
    <aside>
        <header>
            <h1 class="visuallyhidden">Related content</h1>
        </header>
        <asp:Repeater ID="rptWidgets" runat="server" OnItemDataBound="rptWidgets_ItemDataBound">
            <ItemTemplate>
                <safc:WidgetRenderer ID="Renderer" runat="server"/>
            </ItemTemplate>
        </asp:Repeater>
    </aside>
</div>
