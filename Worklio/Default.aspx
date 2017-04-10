<%@ page title="Home Page" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="Default.aspx.cs" inherits="Worklio._Default" %>

<%@ register src="~/Countries.ascx" tagprefix="uc1" tagname="Countries" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <h2>The list of the countries</h2>
        </div>
    </div>

    <uc1:countries runat="server" id="Countries" />
</asp:Content>
