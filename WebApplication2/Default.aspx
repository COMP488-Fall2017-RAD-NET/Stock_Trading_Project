<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <body style="background-image:url('http://www.gettyimages.com/license/625756386')">
<div class="row" style=" padding:20px">
    <div class="col-md-4" style="padding: 10px">
        <h2>Simulate for Free</h2>
        <p>
            Start with virtual cash and put your trading skills to the test! Compete as often as you would like, and see how you compare to the real market.
        </p>

    </div>
    <div class="col-md-4" style="padding: 10px">
        <h2>A Stepping Stone to the Real Market</h2>
        <p>
            This is the ideal way for you to get experience with the stock market. Submit your trades in a virtual envirnment before risking your own capital.
        </p>

    </div>
    <div class="col-md-4" style="padding: 10px">
        <h2>Live and Up-to-Date Data</h2>
        <p>
            Get the most current information on the market to make the most educated decisions utilizing a variety of live tools.
        </p>

    </div>
</div>

    <div class="jumbotron">

        <!-- TradingView Widget BEGIN -->
        <script type="text/javascript" src="https://s3.tradingview.com/tv.js"></script>
        <script type="text/javascript">
            new TradingView.widget({
                "width": 980,
                "height": 610,
                "symbol": "NASDAQ:AAPL",
                "interval": "D",
                "timezone": "Etc/UTC",
                "theme": "Light",
                "style": "1",
                "locale": "en",
                "toolbar_bg": "#f1f3f6",
                "enable_publishing": false,
                "withdateranges": true,
                "allow_symbol_change": true,
                "details": true,
                "hotlist": true,
                "calendar": true,
                "news": [
                    "stocktwits",
                    "headlines"
                ],
                "hideideas": true
            });
        </script>
        <!-- TradingView Widget END -->

    </div>
    
</body>

</asp:Content>
