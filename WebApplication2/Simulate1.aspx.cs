using Newtonsoft.Json.Linq;
using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Linq;

namespace WebApplication2
{

    public partial class Simulate1 : System.Web.UI.Page
    {

        //protected static WebBrowser webBrowser1;

        // create SqlConnection
        SqlConnection connection = new SqlConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            // uncomment when necessary..
            /*if (!SetDatabaseConnection())
            {
                Response.Write("<script>alert(\'Database connection failed\')</script>");
            }*/
        }

        protected void GetQuote_Click(object sender, EventArgs e)
        {
            FetchQuote(ticker.Value);
        }

        // helper method to fetch and render quotes from a remote API
        private void FetchQuote(String ticker)
        {
            // create url
            String url =
                "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=" + ticker + "&interval=60min&outputsize=compact&apikey=22RO18Z8UHIGFQNM";

            // fetch JSON string
            // parse JSON and get last price
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(url);
                JObject o = JObject.Parse(json);

                // render quote and update chart
                try
                {
                    double price = (double)o.Properties().ElementAt(1).ElementAt(0).ElementAt(0).ElementAt(0)["4. close"];
                    quotes.Controls.Add(new Literal()
                    {
                        Text = "<p> Current price of " + ticker + ": $" + price + "</p>"
                    }
                    );
                    UpdateChart(ticker);
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert(\'" + ticker + " ticker symbol not found\')</script>");
                    Response.Write(ex);
                }
            }
        }


        // create web browser - not working yet
        private static void CreateWebBrowser()
        {
            //webBrowser1 = new WebBrowser();
        }

        // update chart - not working yet
        private void UpdateChart(String ticker)
        {
            /*String src = "\"https://s.tradingview.com/widgetembed/?symbol=" + ticker.Value + "&amp;interval=D&amp;symboledit=1&amp;saveimage=1&amp;toolbarbg=f1f3f6&amp;studies=%5B%5D&amp;hideideas=1&amp;theme=Light&amp;style=1&amp;timezone=Etc%2FUTC&amp;studies_overrides=%7B%7D&amp;overrides=%7B%7D&amp;enabled_features=%5B%5D&amp;disabled_features=%5B%5D&amp;locale=en&amp;utm_source=localhost&amp;utm_medium=widget&amp;utm_campaign=chart&amp;utm_term=" + ticker.Value + "\"";
            Thread t = new Thread(new ThreadStart(CreateWebBrowser));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            webBrowser1.Document.GetElementsByTagName("iframe")[0].SetAttribute("src", src);
            t.Abort();*/
        }

        // establish database connection
        private bool SetDatabaseConnection()
        {
            // connect to sql server
            connection.ConnectionString = "Data Source = sql.cs.luc.edu; Initial Catalog = stocktrading; Persist Security Info = True; User ID = afedorov; Password = p52521";
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }

            /* test code
             * 
            String sql = "insert into transactions values (1,'AAPL','BUY',100,98.54)";
            SqlCommand command = new SqlCommand(sql, connection);
            int result = command.ExecuteNonQuery();
            */
        }
    }
}