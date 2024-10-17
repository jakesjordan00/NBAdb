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
            <asp:ListItem Enabled="true" Value="0022000001-0022001080-2020" Text="2020-2021"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="0022100001-0022101230-2021" Text="2021-2022"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="0022200001-0022201230-2022" Text="2022-2023"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="0022300001-0022301230-2023" Text="2023-2024"></asp:ListItem>
            <asp:ListItem Enabled="true" Value="0022400001-0022401230-2024" Text="2024-2025"></asp:ListItem>
        </asp:CheckBoxList>
        <asp:Button ID="btnLoad" runat="server" Text="Load Playoff Data" height="75px" Width="300px" Font-Size="XX-Large" OnClick="btnLoad_Click" 
        style="color:cornflowerblue; background-color:black; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid grey;"/>
        
        <br />
        <asp:Label ID="lblSeasonResult" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />        
        <asp:Label ID="lblTimeElapsed" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
