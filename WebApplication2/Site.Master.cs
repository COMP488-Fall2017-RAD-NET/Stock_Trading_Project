using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class SiteMaster : MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logout_button_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            Session["authenticated"] = false;
            Response.Redirect("/", true);
        }
    }
}