<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Simulate2.aspx.cs" Inherits="WebApplication2.Simulate2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="width:50%; float:left" >
    <form >
  <fieldset>
    <legend style="padding-top: 10px; padding-bottom: 10px">Let's trade some stocks!</legend>
    <div class="form-group">
        <div style=""padding-top: 10px; padding-bottom: 10px">
      <label>Type a ticker symbol to get a quote</label>
      <input type="text" class="form-control" placeholder="Enter quote here" style ="padding:10px">
      <button type="submit" class="btn btn-primary" >Submit</button>
            </div>
        <div style=""padding-top: 5px; padding-bottom: 5px">
        <label>Your quote</label>
      <input type="text" class="form-control" readonly style ="padding:10px">
            </div>
    </div>

      <div class="form-group">
          <div style=""padding-top: 10px; padding-bottom: 10px">
      <label>How many shares would you like?</label>
      <input type="text" class="form-control" placeholder="Enter share quantity here" style ="padding:10px">
      <button type="submit" class="btn btn-primary" >Submit</button>
              </div>
          <div style=""padding-top: 5px; padding-bottom: 5px">
        <label>Total amount</label>
      <input type="text" class="form-control" readonly style ="padding:10px">
      <button type="submit" class="btn btn-primary">Buy</button>
      <button type="submit" class="btn btn-primary">Sell</button>
              </div>
    </div>
    
  </fieldset>
</form>
        </div>

    <div style="width:50%; float:right">
       <legend style="padding-top: 10px; padding-bottom: 10px">Your Portfolio</legend>
    <table class="table table-striped table-hover table-bordered" >
  <thead class="thead-dark">
    <tr>
      <th>#</th>
      <th>Column heading</th>
      <th>Column heading</th>
      <th>Column heading</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>1</td>
      <td>Column content</td>
      <td>Column content</td>
      <td>Column content</td>
    </tr>
    <tr>
      <td>2</td>
      <td>Column content</td>
      <td>Column content</td>
      <td>Column content</td>
    </tr>
       <tr>
      <td>3</td>
      <td>Column content</td>
      <td>Column content</td>
      <td>Column content</td>
    </tr>
    <tr>
      <td>4</td>
      <td>Column content</td>
      <td>Column content</td>
      <td>Column content</td>
    </tr>
       <tr>
      <td>5</td>
      <td>Column content</td>
      <td>Column content</td>
      <td>Column content</td>
    </tr>
    <tr>
      <td>6</td>
      <td>Column content</td>
      <td>Column content</td>
      <td>Column content</td>
    </tr>
      </tbody>
        </table>
        </div>

</asp:Content>
