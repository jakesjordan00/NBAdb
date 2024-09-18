<%@ Page Title="Bus Driver" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BusDriver.aspx.cs" Inherits="NBAdb.BusDriver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Bus Driver</h1>
    <hr />
    <h3>Current Database Status</h3>
    <div class="row">
        <div class="col-md-3">
            <asp:Label ID="lblTableCount" runat="server" Text="Table Count:" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblTables" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="row" runat="server" id="row" visible="false">  
        <div class="col-sm-1">
            <asp:Label ID="lblTable1" runat="server" Text=""></asp:Label>
        </div>
        <div class="container">
            <asp:PlaceHolder ID="placeholder" runat="server"></asp:PlaceHolder>
        </div>
    </div>
    <hr />
    <h3>Load Settings</h3>
    <div class="col-lg-4">     
        <h4>Select seasons for first time load</h4>
        <asp:CheckBoxList ID="chkSeasons" runat="server" RepeatColumns="2" CellPadding="5">
            <asp:ListItem Enabled="true" Value="0022000001-0022001080-2020" Text="2020-2021"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="0022100001-0022101230-2021" Text="2021-2022"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="0022200001-0022201230-2022" Text="2022-2023"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="0022300001-0022301230-2023" Text="2023-2024"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="0022400001-0022401230-2024" Text="2024-2025"></asp:ListItem>
        </asp:CheckBoxList>
        <asp:Button ID="btnFirstTimeLoad" runat="server" Text ="Load" height="75px" Width="250px" Font-Size="XX-Large" OnClick="btnFirstTimeLoad_Click" 
        style="color:cornflowerblue; background-color:black; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid grey;"/>
        <br />
        <asp:Label ID="lblSeasonResult" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />        
        <asp:Label ID="lblTimeElapsed" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>


