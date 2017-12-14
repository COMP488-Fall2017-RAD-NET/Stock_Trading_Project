using System;
using System.Threading;
using System.Web;

namespace WebApplication2
{
    public partial class Simulate2 : System.Web.UI.Page
    {
        private Stock currentStock;
        private User currentUser;
        private Portfolio currentPortfolio;
        private MySqlConnection conn;
        private String tickerString;

        // handle page load event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("/", true);
            }
            else if (!IsPostBack && HttpContext.Current.User.Identity.IsAuthenticated)
            {    
                if (!SetDatabaseConnection())
                {
                    Response.Write("<script>alert(\'Database connection failed\')</script>");
                }
                else
                {
                    currentUser = conn.SelectUser(conn.SelectUserid(HttpContext.Current.User.Identity.Name));
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
            else if (HttpContext.Current.User.Identity.IsAuthenticated)
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

        // handle update profit/loss click event
        protected void profitlossUpdateButton_Click(object sender, EventArgs e)
        {
            conn = (MySqlConnection)Session["conn"];
            currentUser = (User)Session["currentUser"];
            currentPortfolio = conn.SelectUserPortfolio(currentUser);
            currentPortfolio.UpdateCurrentValue();
            try
            {
                profitloss.Value = (currentPortfolio.currentValue - currentPortfolio.initialValue).ToString("C2");
            }
            catch { }
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
    }
}