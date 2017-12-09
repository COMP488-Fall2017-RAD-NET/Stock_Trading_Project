using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.Models;

namespace WebApplication2
{
    public partial class Simulate2 : System.Web.UI.Page
    {

        //protected static WebBrowser webBrowser1;
        private Stock currentStock;
        private User currentUser;
        private Portfolio currentPortfolio;
        private MySqlConnection conn;
        private String tickerString;

        protected void Page_Load(object sender, EventArgs e)
        {
            /* 
             * Probably re-write the database interaction logic using Model
             */

            /*var entities = new stocktradingEntities();
                IList<Models.Portfolio> portfolios = entities.Portfolios.ToList();
                var filteredResult = from p in portfolios
                                     where p.UserId == 1
                                     select p;

                foreach (Models.Portfolio p in filteredResult.ToList())
                {
                }*/
            
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
                    currentPortfolio.UpdateCurrentValue();
                    Session["currentPortfolio"] = currentPortfolio;
                    try
                    {
                        profitloss.Value = (currentPortfolio.currentValue - currentPortfolio.initialValue).ToString("C2");
                    }
                    catch { }
                }
        }
            else
            {
                ticker.Value = (String) Session["tickerString"];
                currentPortfolio = (Portfolio)Session["currentPortfolio"];
                try
                {
                    double sessionQuote = (double) Session["quote"];
                    quote.Value = sessionQuote.ToString("C2");
                    int sessionTradeAmount = (int) Session["tradeAmount"];
                    tradeAmount.Value = sessionTradeAmount.ToString("D");
                    profitloss.Value = (currentPortfolio.currentValue - currentPortfolio.initialValue).ToString("C2");
                }
                catch { }
               
                string eventTarget = this.Request["__EVENTTARGET"];
                string eventArgument = this.Request["__EVENTARGUMENT"];

                if (eventTarget != String.Empty && eventTarget == "callPostBack")
                {
                    if (eventArgument != String.Empty)
                        try
                        {
                            int tradeAmount = Convert.ToInt32(eventArgument);
                            Session["tradeAmount"] = tradeAmount;
                        }
                        catch (FormatException)
                        {
                            Session["tickerString"] = eventArgument.ToUpper();
                        }
                }
            }

        }

        // handle get quote button click event
        protected void GetQuote_Click(object sender, EventArgs e)
        {
            tickerString = (String)Session["tickerString"];
            currentPortfolio = (Portfolio)Session["currentPortfolio"];
            
            // verify if there is a stock in current portfolio
            if (currentPortfolio.stocks.ContainsKey(tickerString))
            {
                currentStock = currentPortfolio.GetStockFromList(tickerString);
            }
            else
            {
                currentStock = new Stock(tickerString);
            }

            Session["currentStock"] = currentStock;
            double price = currentStock.currentPrice;
            Session["quote"] = price;

            // render quote and update chart
            if (price != 0.0)
            {
                quote.Value = price.ToString("C2");
                profitloss.Value = (currentPortfolio.currentValue - currentPortfolio.initialValue).ToString("C2");
                //UpdateChart(ticker.Value);
            }
            else
            {
                Response.Write("<script>alert(\'" + tickerString + " ticker symbol not found\')</script>");
            }
        }

        // handle submit amount button click event
        protected void SubmitAmount_Click(object sender, EventArgs e)
        {
            currentStock = (Stock)Session["currentStock"];
            int transactionAmount = (int) Session["tradeAmount"];
            amount.Value = (currentStock.currentPrice * transactionAmount).ToString("C2");
        }

        // handle buy button click event
        protected void Buy_Click(object sender, EventArgs e)
        {
            // retrieve references from current session
            currentStock = (Stock)Session["currentStock"];
            currentUser = (User)Session["currentUser"];
            currentPortfolio = (Portfolio)Session["currentPortfolio"];
            conn = (MySqlConnection)Session["conn"];
            int transactionAmount = (int)Session["tradeAmount"];
            bool canSave = false;
            String type = "BUY";


            // error handling
            if (currentStock == null || currentStock.currentPrice == 0.0)
            {
                Response.Write("<script>alert(\'Please get a quote first\')</script>");
            }
            else if (transactionAmount == 0)
            {
                Response.Write("<script>alert(\'Please enter quantity\')</script>");
            }
            else
            {
                // validate transation
                if (currentPortfolio.money - currentStock.currentPrice * transactionAmount > 0)
                {
                    canSave = true;
                }
                else
                {
                    Response.Write("<script>alert(\'Insufficient money.\')</script>");
                    ticker.Value = "";
                    quote.Value = "";
                    tradeAmount.Value = "";
                    Session["tradeAmount"] = "";
                }

                // store transaction
                if (canSave)
                {
                    Transaction t = new Transaction(
                    0, currentUser.id, currentStock.ticker, type, transactionAmount, currentStock.currentPrice);
                    if (conn.InsertTransaction(t) == 1 && conn.UpdateUserPortfolio(t, currentPortfolio) == 2)
                    {
                        Response.Write("<script>alert(\'Transaction successfully saved to the database.\')</script>");
                        ticker.Value = "";
                        quote.Value = "";
                        tradeAmount.Value = "";
                        Session["tradeAmount"] = "";
                        currentPortfolio.UpdateCurrentValue();
                        profitloss.Value = (currentPortfolio.currentValue - currentPortfolio.initialValue).ToString("C2");
                    }
                    else
                    {
                        Response.Write("<script>alert(\'Error. Please try again.\')</script>");
                    }
                }
            }
        }

        // handle sell button click event
        protected void Sell_Click(object sender, EventArgs e)
        {
            // retrieve references from current session
            currentStock = (Stock)Session["currentStock"];
            currentUser = (User)Session["currentUser"];
            currentPortfolio = (Portfolio)Session["currentPortfolio"];
            conn = (MySqlConnection)Session["conn"];
            int transactionAmount = (int)Session["tradeAmount"];
            tickerString = (String)Session["tickerString"];
            bool canSave = false;
            String type = "SELL";

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
                int userAmount;
                currentPortfolio.stocks.TryGetValue(tickerString, out userAmount);
                if (userAmount - transactionAmount > 0)
                {
                    canSave = true;
                }
                else
                {
                    Response.Write("<script>alert(\'Insufficient stock.\')</script>");
                    ticker.Value = "";
                    quote.Value = "";
                    tradeAmount.Value = "";
                    Session["tradeAmount"] = "";
                }

                // store transaction
                if (canSave)
                {
                    Transaction t = new Transaction(
                    0, currentUser.id, currentStock.ticker, type, transactionAmount, currentStock.currentPrice);
                    if (conn.InsertTransaction(t) == 1 && conn.UpdateUserPortfolio(t, currentPortfolio) >= 1)
                    {
                        Response.Write("<script>alert(\'Transaction successfully saved to the database.\')</script>");
                        ticker.Value = "";
                        quote.Value = "";
                        tradeAmount.Value = "";
                        Session["tradeAmount"] = "";
                        currentPortfolio.UpdateCurrentValue();
                        profitloss.Value = (currentPortfolio.currentValue - currentPortfolio.initialValue).ToString("C2");
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