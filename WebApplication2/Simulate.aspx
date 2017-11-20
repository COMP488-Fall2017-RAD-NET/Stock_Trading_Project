<%@ Page Language="C#" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
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
