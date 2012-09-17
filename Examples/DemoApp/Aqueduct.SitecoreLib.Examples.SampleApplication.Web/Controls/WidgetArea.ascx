<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WidgetArea.ascx.cs"
    Inherits="Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls.WidgetArea" %>
<%@ Register TagPrefix="Aqueduct" Namespace="Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls.Widgets" Assembly="Aqueduct.SitecoreLib.Examples.SampleApplication.Web" %>

 <!-- Example row of columns -->
      <div class="row">
           <asp:Repeater ID="rptWidgets" runat="server" OnItemDataBound="rptWidgets_ItemDataBound">
            <ItemTemplate>
                <div class="span4">
                     <Aqueduct:WidgetRenderer ID="Renderer" runat="server"/>
                </div>
               
            </ItemTemplate>
        </asp:Repeater>
      </div>




       

