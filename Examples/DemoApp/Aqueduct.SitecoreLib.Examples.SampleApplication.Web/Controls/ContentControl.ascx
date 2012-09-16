<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentControl.ascx.cs" Inherits="Aqueduct.SitecoreLib.Examples.SampleApplication.Web.Controls.ContentControl" %>
<%@ Register TagPrefix="aqueduct" TagName="RightWidgetArea" Src="~/layouts/Shared/RightWidgetArea.ascx" %>

<div class="<%= Show3Column ? "page-content span-7-col col" : "page-content span-10-col col" %>">
    <article class="mod-article">
        <sc:Placeholder runat="server" Key="gallery" />
		<header class="cp-articleHeader">
	    	<h1><%= Headline %></h1>
            <asp:PlaceHolder ID="phByline" runat="server" Visible="false">
	    	<p>
                Published: <time pubdate datetime="<%= ByLine.Published.ToString("yyyy-MM-dd") %>"><%= ByLine.Published.ToString("dd MMMM, yyyy")%></time><br />
				by <%= ByLine.Name %>
	    	</p>
            </asp:PlaceHolder>
		</header>
        <p><%= Summary %></p>
    	<%= Content %>
        <sc:Placeholder runat="server" Key="innercontent" />
        <div class="mod-pagination cc">
	        <div class="mod-pagination-previous">
                <asp:HyperLink runat="server" ID="PreviousArticle"><span class="ico ico-arrowLeft"></span>Previous Article</asp:HyperLink>
	        </div>
	        <div class="mod-pagination-next">
                <asp:HyperLink runat="server" ID="NextArticle">Next Article<span class="ico ico-arrowRight"></span></asp:HyperLink>
	        </div>
        </div>
	</article>
    <div class="related-content">
        <sc:Placeholder runat="server" Key="related" />
    </div>
</div>
<aqueduct:RightWidgetArea id="RightWidgets" runat="server" Visible="false" />