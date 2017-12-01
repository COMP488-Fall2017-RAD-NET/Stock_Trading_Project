using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Simulate3 : System.Web.UI.Page
    {
        //protected static WebBrowser webBrowser1;
        private Stock currentStock;
        private User currentUser;
        private Portfolio currentPortfolio;
        private MySqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!SetDatabaseConnection())
                {
                    Response.Write("<script>alert(\'Database connection failed\')</script>");
                }
                else
                {
                    currentUser = conn.SelectUser(1);
                    Session["currentUser"] = currentUser;

                    currentPortfolio = conn.SelectUserPortfolio(currentUser);
                    Session["currentPortfolio"] = currentPortfolio;
                }
            }
            getQuote.Click += new EventHandler(this.GetQuote_Click);
            buyButton.Click += new EventHandler(this.Buy_Click);
            buyButton.Click += new EventHandler(this.Sell_Click);
        }

        protected void GetQuote_Click(object sender, EventArgs e)
        {
            currentStock = new Stock(ticker.Value);
            Session["currentStock"] = currentStock;
            double price = currentStock.currentPrice;

            // render quote and update chart
            if (price != 0.0)
            {
                quote.Value = price.ToString();
                //UpdateChart(ticker.Value);
            }
            else
            {
                Response.Write("<script>alert(\'" + ticker.Value + " ticker symbol not found\')</script>");
            }
        }

        protected void SubmitAmount_Click(object sender, EventArgs e)
        {
            currentStock = (Stock)Session["currentStock"];
            int transactionAmount = Convert.ToInt32(tradeAmount.Value);
            amount.Value = (currentStock.currentPrice * transactionAmount).ToString();
        }

        protected void Buy_Click(object sender, EventArgs e)
        {
            // retrieve references from current session
            currentStock = (Stock)Session["currentStock"];
            currentUser = (User)Session["currentUser"];
            currentPortfolio = (Portfolio)Session["currentPortfolio"];
            conn = (MySqlConnection)Session["conn"];

            // error handling
            if (currentStock == null || currentStock.currentPrice == 0.0)
            {
                Response.Write("<script>alert(\'Please get a quote first\')</script>");
            }
            else if (tradeAmount.Value == "")
            {
                Response.Write("<script>alert(\'Please enter quantity\')</script>");
            }
            else
            {
                // validate transation
                String type = "BUY";
                int transactionAmount = Convert.ToInt32(tradeAmount.Value);
                bool canSave = false;

                if (currentPortfolio.money - currentStock.currentPrice * transactionAmount > 0)
                {
                    canSave = true;
                }
                else
                {
                    Response.Write("<script>alert(\'Insufficient money.\')</script>");
                }

                // store transaction
                if (canSave)
                {
                    Transaction t = new Transaction(
                    0, currentUser.id, currentStock.ticker, type, transactionAmount, currentStock.currentPrice);
                    if (conn.InsertTransaction(t) == 1 && conn.UpdateUserPortfolio(t, currentPortfolio) >= 1)
                    {
                        Response.Write("<script>alert(\'Transaction successfully saved to the database.\')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert(\'Error. Please try again.\')</script>");
                    }
                }
            }
        }

        protected void Sell_Click(object sender, EventArgs e)
        {
            // retrieve references from current session
            currentStock = (Stock)Session["currentStock"];
            currentUser = (User)Session["currentUser"];
            currentPortfolio = (Portfolio)Session["currentPortfolio"];
            conn = (MySqlConnection)Session["conn"];

            // error handling
            if (currentStock == null || currentStock.currentPrice == 0.0)
            {
                Response.Write("<script>alert(\'Please get a quote first\')</script>");
            }
            else if (tradeAmount.Value == "")
            {
                Response.Write("<script>alert(\'Please enter quantity\')</script>");
            }
            else
            {
                // validate transation
                String type = "SELL";
                int transactionAmount = Convert.ToInt32(tradeAmount.Value);
                int userAmount;
                bool canSave = false;

                currentPortfolio.stocks.TryGetValue(ticker.Value, out userAmount);
                if (userAmount - transactionAmount > 0)
                {
                    canSave = true;
                }
                else
                {
                    Response.Write("<script>alert(\'Insufficient stock.\')</script>");
                }

                // store transaction
                if (canSave)
                {
                    Transaction t = new Transaction(
                    0, currentUser.id, currentStock.ticker, type, transactionAmount, currentStock.currentPrice);
                    if (conn.InsertTransaction(t) == 1 && conn.UpdateUserPortfolio(t, currentPortfolio) >= 1)
                    {
                        Response.Write("<script>alert(\'Transaction successfully saved to the database.\')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert(\'Error. Please try again.\')</script>");
                    }
                }
            }
        }

        // establish database connection
        private bool SetDatabaseConnection()
        {
            conn = new MySqlConnection();
            if (conn.Connect())
            {
                Session["conn"] = conn;
                return true;
            }
            else
            {
                return false;
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
    }

}