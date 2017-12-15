using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace WebApplication2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void login_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = Username.Text;
                string password = Password.Text;
                //Spassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                
                MySqlConnection conn = new MySqlConnection();
                PasswordHasher hasher = new PasswordHasher();

                if (conn.Connect())
                {
                    if (!conn.ValidateUsername(username))
                    {
                        Label1.Text = "Invalid username";
                    }
                    else
                    {
                        string dpass = conn.MatchPassword(username);
                        PasswordVerificationResult result = hasher.VerifyHashedPassword(dpass, password);

                        if (result == PasswordVerificationResult.Failed)
                        {
                            Label1.Text = "Invalid password";
                        }

                        else
                        {
                            Session["authenticated"] = true;
                            FormsAuthentication.RedirectFromLoginPage(username, CheckBox1.Checked);
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert(\'Database connection failed\')</script>");
                }
            }
        }
    }
}