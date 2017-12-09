<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication2.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> - Stock Trading Simulator</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

</head>
<body>
    <form runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <!--
        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" runat="server" href="~/">Stock Trading Simulator</a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <li><a runat="server" href="~/">Home</a></li>
                        <li><a runat="server" href="~/About">About</a></li>
                        <li><a runat="server" href="~/Contact">Stock Info</a></li>
                        <li><a runat="server" href="~/Simulate1">SIMULATE NOW</a></li>
                    </ul>
                </div>
            </div>
        </div>
        -->

        <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
  <a class="navbar-brand" href="Default.aspx">Stock Trading Simulator</a>
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarColor01" aria-controls="navbarColor01" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>

  <div class="collapse navbar-collapse" id="navbarColor01">
    <ul class="navbar-nav mr-auto">
      <li class="nav-item active">
        <a class="nav-link" href="About.aspx">About <span class="sr-only">(current)</span></a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="Contact.aspx">Stock Info</a>
      </li>
      <!--li class="nav-item">
        <a class="nav-link" href="Simulate2.aspx">SIMULATE NOW</a>
      </!--li-->
    </ul>
      
      <button class="btn btn-secondary my-2 my-sm-0" type="submit" style="width:100px"><a href="/Default">Home</a>
      <button class="btn btn-secondary my-2 my-sm-0" type="submit" style="width:100px"><a href="/Login">Login</a></button>
  </div>
</nav>
        <center>
            <div style="width:1250px">
                </br>
        <h2>Welcome to account registration!</h2>
                </br>
        <div style="font-size:large; width:600px;float:left">
            <ul style="text-align:left">
            <li>
                You need to create account for stock trading simulation. 
            </li>
            <li>
                You can only use letters and numbers for your username. 
            </li>
                <li>
                    Username must be 8-12 characters long.
                </li>
            <li>
                you can use letters, numbers, and special characters "!","@","#","$","%" for password. 
            </li>
            <li>
                Password must have at least one upper case letter, one number and one special character.
            </li>
                <li>
                  Password must be 6-8 characters long.
                </li>
            </ul>
            
        </div>

       <div class="container" style="font-size:medium; width:600px;float:right">
        <div style="float:left">
            <table id="table1">

                <tr>
                     <td style="text-align:right"><b>Username</b></td>
                    <td>
                        <asp:TextBox ID="Username" Text="" runat="server"></asp:TextBox>
                    </td>
                    <td>
                         <asp:Button ID="Button2" runat="server" OnClick="validation_Click" Text="Check if username is avalable." />
                    </td>
                </tr>

                <tr>
                     <td style="text-align:right"><b>First Name</b></td>
                    <td>
                        <asp:TextBox ID="FName" Text="" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                     <td style="text-align:right"><b>Last Name</b></td>
                    <td>
                        <asp:TextBox ID="LName" Text="" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right"><b>Password</b></td>
                     <td>
                        <asp:TextBox ID="Password" TextMode="Password" Text="" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right"><b>Confirm Password</b></td>
                     <td>
                        <asp:TextBox ID="RePassowrd" TextMode="Password" Text="" runat="server"></asp:TextBox>
                    </td>
                </tr>
                                <tr>
                    <td style="text-align:right"><b>Email</b></td>
                     <td>
                        <asp:TextBox ID="Email" Text="" runat="server"></asp:TextBox>
                    </td>
                </tr>
                               
                <tr>
                    <td colspan="2" style="text-align:center">
                    
                        <asp:Button ID="Button4" runat="server" Text="Create Account" onclick="creating_Click"/>
                    
                </td>
                        </tr>
                <tr><td colspan="3">
                  <asp:Label ID="usernameVal" runat="server" Font-Bold="false" ForeColor="Red"
	               Font-Size="Large" Text="" Visible="True">
	            </asp:Label>
                    </td>
                </tr>

                                <tr><td colspan="3">

	                         <asp:Label ID="creatingInfo" runat="server" Font-Bold="false" ForeColor="Red" 
	               Font-Size="Large" Text="" Visible="True">
	            </asp:Label>
                    </td>
                </tr>

            </table>
           
    

 

            </center>
    </form>
 
</body>
</html>
