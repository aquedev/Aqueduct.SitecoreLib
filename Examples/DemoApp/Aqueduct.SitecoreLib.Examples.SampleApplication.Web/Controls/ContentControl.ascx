<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentControl.ascx.cs" Inherits="Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls.ContentControl" %>
<%@ Register TagPrefix="aqueduct" TagName="RightWidgetArea" Src="~/Controls/WidgetArea.ascx" %>


  <!-- Main hero unit for a primary marketing message or call to action -->
      <div class="hero-unit">
        <h1><%= Headline %></h1>
        <p><%= Summary %></p>
        <p><a class="btn btn-primary btn-large">Learn more &raquo;</a></p>
      </div>
      <aqueduct:RightWidgetArea id="WidgetControl" runat="server" Visible="false" />
     
