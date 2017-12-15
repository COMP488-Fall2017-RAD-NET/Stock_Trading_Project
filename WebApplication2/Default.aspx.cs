using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace WebApplication2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.User =
                new GenericPrincipal(new GenericIdentity(string.Empty), null);
        }
    }
}