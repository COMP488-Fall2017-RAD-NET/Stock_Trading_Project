<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WebApplication2.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <h1 style="padding: 20px"> Stock Info</h1>

<div style="padding: 10px">
    <iframe src="https://investingwidgets.com/leading-stocks?theme=darkTheme" width="100%" height="100%" frameborder="0" allowtransparency="true" marginwidth="0" marginheight="0"></iframe>
</div>

<div style="padding: 10px">
    <iframe src="https://investingwidgets.com/live-commodities?theme=darkTheme" width="100%" height="100%" frameborder="0" allowtransparency="true" marginwidth="0" marginheight="0"></iframe>
</div>

<div style="padding: 10px">
    <iframe src="https://investingwidgets.com/live-indices?theme=darkTheme&pairs=166,27,172,177,168,167,170" width="100%" height="100%" frameborder="0" allowtransparency="true" marginwidth="0" marginheight="0"></iframe>
</div>
</asp:Content>
