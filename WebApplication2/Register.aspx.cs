using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private string Usernamevalidation(string s)
        {
            string username = s;
            bool validUsername = username.All(c => Char.IsLetterOrDigit(c));
            string label = "";
            if (username.Length < 8 || username.Length > 12)
            {
                label = "Invalid username, must be between 8-12 characters!";
            }

            else if (!validUsername)
            {
                label = "Invalid username, must use letters or numbers!";
            }

            else
            {
                MySqlConnection conn = new MySqlConnection();
                if (conn.Connect())
                {
                    if (conn.ValidateUsername(s))
                    {
                        label = "Username already exists. Please choose another one!";
                    }

                    else
                    {
                        label = "You can use this username.";
                    }
                    conn.Disconnect();
                }
                else
                {
                    Response.Write("<script>alert(\'Database connection failed\')</script>");
                }

            }

            return label;
        }

        private string PasswordValidation(string s)
        {
            string password = s;
            string label = "";
            bool validpassword = password.All(c => Char.IsLetterOrDigit(c) || c.Equals('!') || c.Equals('@') || c.Equals('#') || c.Equals('$') || c.Equals('%'));

            bool validpassword2 = password.Contains("!") || password.Contains("@") || password.Contains("#") || password.Contains("$") || password.Contains("%");

            if (password.Length < 6 || password.Length > 8)
            {
                label = "Invalid password, must be between 6-8 characters!";
            }

            else if (!validpassword)
            {
                label = "Invalid password, must use letters, numbers or special characters above!";
            }

            else if (!(password.Any(char.IsUpper) && password.Any(char.IsDigit)))
            {
                label = "Invalid password, must have at least one upper case letter and one number!";
            }

            else if (!validpassword2)
            {
                label = "Invalid password, must have at least one special character!";
            }

            else
                label = "OK";

            return label;


        }


        protected void validation_Click(object sender, EventArgs e)
        {
            string username = Username.Text;
            usernameVal.Text = "";
            creatingInfo.Text = "";
            usernameVal.Text = Usernamevalidation(username);

        }

        protected void creating_Click(object sender, EventArgs e)
        {
            string username = Username.Text;
            string label = Usernamevalidation(username);
            int validation = 1;
            while (true)
            {
                if (label == "You can use this username.")
                {
                    creatingInfo.Text = "";
                }

                else
                {
                    creatingInfo.Text = label;
                    validation = 0;
                    break;
                }


                String password = Password.Text;
                label = PasswordValidation(password);
                if (label == "OK")
                {
                    creatingInfo.Text = label;
                }

                else
                {
                    creatingInfo.Text = label;
                    validation = 0;
                    break;
                }


                string rePassword = RePassowrd.Text;
                if (password != rePassword)
                {
                    creatingInfo.Text = "Passwords don't match!";
                    validation = 0;
                    break;
                }

                if (validation == 1)
                {
                    //password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
                    PasswordHasher hasher = new PasswordHasher();
                    String hashedPassword = hasher.HashPassword(password);

                    MySqlConnection conn = new MySqlConnection();

                    if (conn.Connect())
                    {
                        User user = new User(0);
                        user.firstName = FName.Text;
                        user.username = username;
                        user.lastName = LName.Text;
                        user.password = hashedPassword;
                        user.email = Email.Text;

                        conn.InsertUser(user);
                        conn.Disconnect();
                        Server.Transfer("Login.aspx", true);
                    }
                    else
                    {
                        Response.Write("<script>alert(\'Database connection failed\')</script>");
                    }
                }
            }
        }
/*
        protected void Button2_Click(object sender, EventArgs e)
        {
            string username = Username.Text;
            usernameVal.Text = "";
            creatingInfo.Text = "";
            usernameVal.Text = Usernamevalidation(username);
         }
 */
    }
}