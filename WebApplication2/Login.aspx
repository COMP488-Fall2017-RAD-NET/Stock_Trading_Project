<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication2.Login" %>

<!DOCTYPE html>

<html lang="en">
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
        
      <asp:LinkButton runat="server" class="btn btn-secondary my-2 my-sm-0" type="submit" style="width:100px" href="/Default">Home</asp:LinkButton>
      <asp:LinkButton runat="server" class="btn btn-secondary my-2 my-sm-0" type="submit" style="width:100px" href="/Register">Register</asp:LinkButton>
  </div>
</nav>
 <h4>Welcome to log in page</h4>
        <center>
                    
        <div>
            </br></br>

             <table id="table1">

                <tr>
                     <td style="text-align:right"><b>Username</b></td>
                    <td>
                        <asp:textbox id="Username" runat="server" Text=""></asp:textbox>
 
                    </td>

                </tr>

                <tr>
                    <td style="text-align:right"><b>Password</b></td>
                    <td>
                        <asp:textbox id="Password" runat="server" Text="" TextMode="Password"></asp:textbox>

                    </td>
                </tr>

     
           <tr>
               <td>
                    <asp:Button ID="Button2" runat="server" Text="Login" onclick="login_Click"/>
                   </td>
               <td>
              <asp:CheckBox ID="CheckBox1" Text="Remember Me" runat="server" />
          </td>
               </tr>
                 </table>
                
                   </div>


        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

      



        <br />
            
        <br />
           
        <br />

            </center>
    </form>
    </form>
</body>
</html>

