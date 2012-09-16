<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageWidgetControl.ascx.cs" Inherits="Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls.Widgets.ImageWidgetControl" %>
<%@ Import Namespace="Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Classes" %>

<div class="mod-widget cc">
    <h2><%= ImageWidget.Title.BoldFirstWord() %></h2>
    <div class="mod-widget-link mod-widget-image">
        <%= ImageWidget.Link.BeginAnchorTag() %>
            <%= ImageWidget.Image.GetImage() %>
    	<%= ImageWidget.Link.EndAnchorTag() %>
    </div>
    <%= Strapline %>
    <%= ImageWidget.Link.RenderAnchorTag("cp-moreLink") %>
</div>