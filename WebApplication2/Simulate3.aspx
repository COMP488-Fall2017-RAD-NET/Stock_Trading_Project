<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Simulate3.aspx.cs" Inherits="WebApplication2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset>
            <legend style="padding-top: 10px; padding-bottom: 10px">Let's trade some stocks!</legend>
            <div class="form-group">
                <div style="padding-top: 10px; padding-bottom: 10px">
                    <label>Type a ticker symbol to get a quote</label>
                    <input id="ticker" runat="server" type="text" class="form-control" placeholder="Enter quote here" style="padding: 10px" />
                    <asp:Button ID="getQuote" runat="server" Text="Submit" OnClick="GetQuote_Click" type="submit" class="btn btn-primary" CausesValidation="False" />
                </div>
                <div style="padding-top: 5px; padding-bottom: 5px">
                    <label>Your quote</label>
                    <input id="quote" runat="server" type="text" class="form-control" readonly="true" style="padding: 10px" />
                </div>
            </div>

            <div class="form-group">
                <div style="padding-top: 10px; padding-bottom: 10px">
                    <label>How many shares would you like?</label>
                    <input id="tradeAmount" runat="server" type="text" class="form-control" placeholder="Enter share quantity here" style="padding: 10px" />
                    <button type="submit" class="btn btn-primary">Submit</button>
                </div>
                <div style="padding-top: 5px; padding-bottom: 5px">
                    <label>Total amount</label>
                    <input type="text" class="form-control" readonly="true" style="padding: 10px" />
                    <asp:Button ID="buyButton" runat="server" Text="Buy" OnClick="Buy_Click" type="submit" class="btn btn-primary" CausesValidation="False" />
                    <asp:Button ID="sellButton" runat="server" Text="Sell" OnClick="Sell_Click" type="submit" class="btn btn-primary" CausesValidation="False" />
                </div>
            </div>
        </fieldset>
    </div>
    </form>
</body>
</html>
