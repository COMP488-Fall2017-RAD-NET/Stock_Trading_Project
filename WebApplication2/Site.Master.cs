using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class SiteMaster : MasterPage
    {
        //protected static WebBrowser webBrowser1;
        private Stock currentStock;
        private User currentUser;
        private Portfolio currentPortfolio;
        private MySqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void GetQuote_Click(object sender, EventArgs e)
        {
            Response.Write("<p>Button clicked</p>");
        }
    }
}