<%@ Page Title="Playoffs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Playoffs.aspx.cs" Inherits="NBAdb.Playoffs" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
        <h5>Due to the way Playoff game_ids are formatted, they must be loaded seperately</h5>
    </main>
    <hr />
    <h3>Load Settings</h3>
    <div class="col-lg-4">     
        <h4>Select seasons to load Playoff data</h4>
        <asp:CheckBoxList ID="chkSeasons" runat="server" RepeatColumns="2" CellPadding="5">
            <asp:ListItem Enabled="true" Value="20" Text="2020-2021"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="21" Text="2021-2022"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="22" Text="2022-2023"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="23" Text="2023-2024"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="24" Text="2024-2025"></asp:ListItem>
        </asp:CheckBoxList>
        <asp:Button ID="btnLoad" runat="server" Text="Load Playoff Data" height="75px" Width="300px" Font-Size="XX-Large" OnClick="btnLoad_Click" 
        style="color:cornflowerblue; background-color:black; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid grey;"/>
        
        <br />
        <asp:Label ID="lblSeasonResult" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />        
        <asp:Label ID="lblTimeElapsed" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
