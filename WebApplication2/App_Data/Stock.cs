using Newtonsoft.Json.Linq;
using System;
using System.Linq;

/// <summary>
/// A Stock class
/// </summary>
public class Stock
{
    public String ticker { get; private set; }
    public double currentPrice { get; private set; }

    public Stock(String ticker)
    {
        this.ticker = ticker;
        this.currentPrice = FetchQuote();
    }

    // helper method to fetch and render quotes from a remote API
    public double FetchQuote()
    {
        // create url
        String url =
            "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=" + ticker + "&interval=60min&outputsize=compact&apikey=38HEIOY4TO9U5D4S";
        // fetch JSON string
        // parse JSON and get last price
        using (var webClient = new System.Net.WebClient())
        {
            var json = webClient.DownloadString(url);
            JObject o = JObject.Parse(json);
            double price;

            try
            {
                price = (double)o.Properties().ElementAt(1).ElementAt(0).ElementAt(0).ElementAt(0)["4. close"];
            }
            catch (Exception ex)
            {
                price = 0.0;
            }
            return price;
        }
    }
}