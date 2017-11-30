using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    public int id { get; set; }
    public String username { get; set; }
    public String firstName { get; set; }
    public String lastName { get; set; }
    public String email { get; set; }
    public String password { get; set; }

    public User(int id)
    {
        this.id = id;
        this.username = "";
        this.firstName = "";
        this.lastName = "";
        this.email = "";
        this.password = "";
    }

    public override string ToString()
    {
        return String.Format("'{0}','{1}','{2}','{3}','{4}','{5}'", id, username, firstName, lastName, email, password);
    }
}