<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentControl.ascx.cs" Inherits="Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls.ContentControl" %>
<%@ Register TagPrefix="aqueduct" TagName="RightWidgetArea" Src="~/Controls/WidgetArea.ascx" %>

<div>
    <article class="mod-article">
        <sc:Placeholder runat="server" Key="gallery" />
		<header class="cp-articleHeader">
	    	<h1><%= Headline %></h1>
        
		</header>
        <p><%= Summary %></p>
    	<%= Content %>
        <sc:Placeholder runat="server" Key="innercontent" />
        
	</article>
    
</div>
<aqueduct:RightWidgetArea id="WidgetControl" runat="server" Visible="false" />