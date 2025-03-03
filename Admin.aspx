<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="NBAdb.Admin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h4>Game Notes</h4>
        <h6>Game notes refresh daily I believe. New notes are posted for each team when they play a game that day.</h6>
        <asp:Button ID="btnGameNotes" runat="server" Text="Get Game Notes" OnClick="btnGameNotes_Click" class="btn btn-primary active" BackColor="SkyBlue" ForeColor="White" BorderColor="White"/>

        
        <hr />
        <h4>Injury Report</h4>
        <h6>Downloads PDF for latest injury report, reads contents and stores data</h6>
        <asp:Button ID="btnInjury" runat="server" Text="Get Injury Data" class="btn btn-primary active" BackColor="SkyBlue" ForeColor="White" BorderColor="White" OnClick="btnInjury_Click" />
        
        <hr />
        <h4>Game Schedule</h4>
        <h6>Pulls game schedule for 2024 season. Appears to be regular season, in-season tournament, preseason and all-star game.</h6>
        <asp:Button ID="btnGameSchedule" runat="server" Text="Get Game Schedule" OnClick="btnGameSchedule_Click" class="btn btn-primary active" BackColor="SkyBlue" ForeColor="White" BorderColor="White" />


        <hr />
        <h4>Data Refresh</h4>
        <h6>Finds the max date in the game table, stores date, then delete everything greater or equal to in game, teambox and playerbox. Query GameSchedule for all games between stored date and today, then insert.</h6>
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh Game Data" class="btn btn-primary active" BackColor="SkyBlue" ForeColor="White" BorderColor="White" OnClick="btnRefresh_Click" />
        <br />
        <asp:Label ID="lblTimeElapsed" runat="server" Text=""></asp:Label>

        <hr />
        <h4>Starting Lineups</h4>
        <h6>Gets Starting Lineups data for 2024 season</h6>
        <asp:Button ID="btnStarters" runat="server" Text="Get Starters" class="btn btn-primary active" BackColor="SkyBlue" ForeColor="White" BorderColor="White" OnClick="btnStarters_Click" />
        
        
<%--        <hr />
        <h4>Posessions</h4>
        <h6>WIP Gets Play By Play data with posessions for 2024 season</h6>--%>
        <asp:Button ID="btnPosessions" runat="server" Text="Get Posession Data" class="btn btn-primary active" BackColor="SkyBlue" ForeColor="White" BorderColor="White" OnClick="btnPosessions_Click" Visible="false" />

<%--        <hr />
        <h4>Lineup details</h4>
        <h6>Gets Lineup details</h6>
        <h10>WIP. Will ideally track who is on the court at a given time in each game for each team</h10><br />--%>
        <asp:Button ID="btnLineups" runat="server" Text="Get Lineup Details" class="btn btn-primary active" BackColor="SkyBlue" ForeColor="White" BorderColor="White" OnClick="btnLineups_Click" Visible="false" />



        <hr />
        <h4>Get Pre 2019 data</h4>
        <asp:Button ID="btnOldData" runat="server" Text="Get Old Data" class="btn btn-primary active" BackColor="SkyBlue" ForeColor="White" BorderColor="White" OnClick="btnOldData_Click" />
    </main>
</asp:Content>
