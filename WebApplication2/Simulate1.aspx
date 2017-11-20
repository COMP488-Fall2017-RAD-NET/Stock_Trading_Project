<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Simulate1.aspx.cs" Inherits="WebApplication2.Simulate1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">   
          <div id="quotes" runat="server">
    
        Please enter ticker symbol:<br />
        <input id="ticker" type="text" runat="server" /><br />
        <asp:Button ID="getQuote" runat="server" Text="Get quote" OnClick="GetQuote_Click" />
    
    </div>

    </form>
</body>
</html>
