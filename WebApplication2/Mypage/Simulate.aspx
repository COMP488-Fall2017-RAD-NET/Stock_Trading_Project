<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Simulate.aspx.cs" Inherits="WebApplication2.Mypage.Simulate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> - Stock Trading Simulator</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

</head>
<body>
    <form runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <!--
        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" runat="server" href="~/">Stock Trading Simulator</a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <li><a runat="server" href="~/">Home</a></li>
                        <li><a runat="server" href="~/About">About</a></li>
                        <li><a runat="server" href="~/Contact">Stock Info</a></li>
                        <li><a runat="server" href="~/Simulate1">SIMULATE NOW</a></li>
                    </ul>
                </div>
            </div>
        </div>
        -->

        <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
  <a class="navbar-brand" href="Default.aspx">Stock Trading Simulator</a>
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarColor01" aria-controls="navbarColor01" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>

  <div class="collapse navbar-collapse" id="navbarColor01">
    <ul class="navbar-nav mr-auto">
      <li class="nav-item active">
        <a class="nav-link" href="About.aspx">About <span class="sr-only">(current)</span></a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="Contact.aspx">Stock Info</a>
      </li>
      <!--li class="nav-item">
        <a class="nav-link" href="Simulate2.aspx">SIMULATE NOW</a>
      </!--li-->
    </ul>
    <form class="form-inline my-2 my-lg-0">
      <button class="btn btn-secondary my-2 my-sm-0" type="submit" style="width:100px"><a href="\Default">Logout</a></button>
     
    </form>
  </div>
</nav>

        <center><div style="width:1000px">

    <div style="width: 50%; float: left;text-align:left">
        <!--<form>-->
            <fieldset>
                <legend style="padding-top: 10px; padding-bottom: 10px">Let's trade some stocks!</legend>
                <div class="form-group">
                    <div style="padding-top: 10px; padding-bottom: 10px">
                        <label>Type a ticker symbol to get a quote</label>
                        <input id="ticker" runat="server" type="text" class="form-control" placeholder="Enter ticker here" style="padding: 10px" ClientIDMode="Static"/>
                        <asp:Button ID="getQuote" runat="server" Text="Submit" OnClick="GetQuote_Click" class="btn btn-primary" UseSubmitBehavior="false" OnClientClick="saveTickerValue()"/>
                    </div>
                    <div style="padding-top: 5px; padding-bottom: 5px">
                        <label>Your quote</label>
                        <input id="quote" runat="server" type="text" class="form-control" readonly style="padding: 10px" />
                    </div>
                </div>

                <div class="form-group">
                    <div style="padding-top: 10px; padding-bottom: 10px">
                        <label>How many shares would you like?</label>
                        <input id="tradeAmount" runat="server" type="text" class="form-control" placeholder="Enter share quantity here" style="padding: 10px" ClientIDMode="Static"/>
                        <asp:CompareValidator ControlToValidate="tradeAmount" runat="server" ErrorMessage="Integers only please" Operator="DataTypeCheck" Type="Integer" ></asp:CompareValidator>
                        <br />
                        <asp:Button ID="submitAmount" runat="server" Text="Submit" OnClick="SubmitAmount_Click" class="btn btn-primary" UseSubmitBehavior="false" OnClientClick="saveAmountValue()" />
                    </div>
                    <div style="padding-top: 5px; padding-bottom: 5px">
                        <label>Total amount</label>
                        <input id="amount" runat="server" type="text" class="form-control" readonly style="padding: 10px" />
                        <asp:Button ID="buyButton" runat="server" Text="Buy" OnClick="Buy_Click" class="btn btn-primary" UseSubmitBehavior="false" />
                        <asp:Button ID="sellButton" runat="server" Text="Sell" OnClick="Sell_Click" class="btn btn-primary" UseSubmitBehavior="false"/>
                    </div>
                </div>
            </fieldset>
        <!--</form>-->
    </div>
    
    <div style="width: 50%; float: right; padding-top: 10px; padding-bottom: 10px; font-size:20px;text-align:left">
        <a href="/Portfolio/Index">Click here to review your Portfolio</a>
        <br />
        <a href="/Transactions/Index">Click here to review your Transactions</a>
    </div>

    <div style="width: 50%; float: right; padding-top: 10px; padding-bottom: 10px; font-size:20px;text-align:left">
        <label>Your Profit / Loss:</label>
        <input id="profitloss" runat="server" type="text" class="form-control" readonly style="padding: 10px" />
        <asp:Button ID="profitlossUpdateButton" runat="server" Text="Update" OnClick="profitlossUpdateButton_Click" class="btn btn-primary" UseSubmitBehavior="false" />
    </div>
    <!--
    <div style="width: 50%; float: right">
        <legend style="padding-top: 10px; padding-bottom: 10px">Your Portfolio</legend>
        <table class="table table-striped table-hover table-bordered">
            <thead class="thead-dark">
                <tr>
                    <th>#</th>
                    <th>Column heading</th>
                    <th>Column heading</th>
                    <th>Column heading</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>1</td>
                    <td>Column content</td>
                    <td>Column content</td>
                    <td>Column content</td>
                </tr>
                <tr>
                    <td>2</td>
                    <td>Column content</td>
                    <td>Column content</td>
                    <td>Column content</td>
                </tr>
                <tr>
                    <td>3</td>
                    <td>Column content</td>
                    <td>Column content</td>
                    <td>Column content</td>
                </tr>
                <tr>
                    <td>4</td>
                    <td>Column content</td>
                    <td>Column content</td>
                    <td>Column content</td>
                </tr>
                <tr>
                    <td>5</td>
                    <td>Column content</td>
                    <td>Column content</td>
                    <td>Column content</td>
                </tr>
                <tr>
                    <td>6</td>
                    <td>Column content</td>
                    <td>Column content</td>
                    <td>Column content</td>
                </tr>
            </tbody>
        </table>
    </div>
    -->
    <script type="text/javascript">
        function saveTickerValue() {
            __doPostBack('callPostBack', document.getElementById("ticker").value);
        }
        function saveAmountValue() {
            __doPostBack('callPostBack', document.getElementById("tradeAmount").value);
        }
    </script>
            </div>
            </center>


    </form>
</body>
</html>
