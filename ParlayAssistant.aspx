<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParlayAssistant.aspx.cs" Inherits="NBAdb.ParlayAssistant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Parlay Assistant</h1>
    <%-- This section hold the Team dropdown. Once a team is selected, the dropdowns will populate with the selected Team's roster. --%>
    <abbr title="Select a team. Once a team has been selected, the four player dropdowns will populate." style="font-size:Large; text-decoration-color:grey">Select Team:</abbr>
    <asp:DropDownList       
                     ID="ddTeams" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" 
                     runat="server" DataKeyNames="Team" AutoPostBack="true" OnSelectedIndexChanged="ddTeams_SelectedIndexChanged"
                     BackColor="White" BorderColor="Black" Font-Size="Large" Width="350px" ForeColor="Black">
    </asp:DropDownList>
    <br />
    <%-- Here, we select our first player. --%>
    <abbr title="Select the player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Select Player:</abbr>
    <asp:DropDownList 
        ID="ddlRoster" runat="server"    DataKeyNames="Player"  AutoPostBack="true" BackColor="White" ForeColor="Black"
        RepeatColumns="4" Width="300px" BorderColor="Black"  class="btn btn-primary dropdown-toggle" data-toggle="dropdown" 
        aria-haspopup="true" aria-expanded="false" OnSelectedIndexChanged="ddlRoster_SelectedIndexChanged">
        <asp:ListItem Text="Player" Value="" />
    </asp:DropDownList>    
    <%-- and here we select an Injured player if we would like --%>
    <abbr title="If a teammate of the 'Prop Player' is ruled out and you would like to check games meeting this criteria, select that ruled out player" style="font-size:Large; text-decoration-color:grey">Select Injured Player:</abbr> 
    <asp:DropDownList 
        ID="ddlInjured" runat="server"    DataKeyNames="Player"  AutoPostBack="true" BackColor="White" ForeColor="Black"
        RepeatColumns="4" Width="300px" BorderColor="Black"  class="btn btn-primary dropdown-toggle" data-toggle="dropdown" 
        aria-haspopup="true" aria-expanded="false" OnSelectedIndexChanged="ddlInjured_SelectedIndexChanged">
        <asp:ListItem Text="Injured Player" Value="" />
    </asp:DropDownList>
    <br />
    <%-- This section contains the options for props we want to include in our Parlay. This first row are our checkboxes --%>
    <div class="row">
        <div class="col-md-1" style="width:50px">
            <abbr title="Select the props to include. By selecting multiple, you can test the percentage of a particular parlay." style="font-size:small; width:30px; text-decoration-color:grey">Props:</abbr>
        </div>
        <div class="col-md-1">
            <asp:CheckBox ID="chkP" runat="server" Text="&nbsp;Points" ForeColor="White" Width="80px" TextAlign="right" OnCheckedChanged="chkP_CheckedChanged" AutoPostBack="true"/>
        </div>
        <div class="col-md-1">
            <asp:CheckBox ID="chkA" runat="server" Text="&nbsp;Assists" ForeColor="White" Width="80px" TextAlign="right" OnCheckedChanged="chkA_CheckedChanged" AutoPostBack="true"/>
        </div>
        <div class="col-md-1">
            <asp:CheckBox ID="chkR" runat="server" Text="&nbsp;Rebounds" ForeColor="White" Width="100px" TextAlign="right" OnCheckedChanged="chkR_CheckedChanged" AutoPostBack="true"/>
        </div>
        <div class="col-md-1" style="width:120px;">
            <asp:CheckBox ID="chk3" runat="server" Text="&nbsp;Threes Made" ForeColor="White" Width="120px" TextAlign="right" OnCheckedChanged="chk3_CheckedChanged" AutoPostBack="true"/>
        </div>
        <div class="col-md-1">
            <asp:CheckBox ID="chkB" runat="server" Text="&nbsp;Blocks" ForeColor="White" Width="80px" TextAlign="right" OnCheckedChanged="chkB_CheckedChanged" AutoPostBack="true"/>
        </div>
        <div class="col-md-1">
            <asp:CheckBox ID="chkS" runat="server" Text="&nbsp;Steals" ForeColor="White" Width="80px" TextAlign="right" OnCheckedChanged="chkS_CheckedChanged" AutoPostBack="true"/>
        </div>
    </div>
    <%-- and our second row holds the text boxes. If a checkbox is selected, its respective textbox will appear. If it's not or is unselected, it will  --%>
    <div class="row">
        <div class="col-md-1" style="width:50px;">
            <abbr title="Enter the values for the props. Say you expect your player to score over 22.5 points, enter 22.5 or 23, and so on for the other values. If you leave the box blank, it will default to 0." style="font-size:small; width:30px; text-decoration-color:grey;">Values:</abbr>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt1P" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt1A" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt1R" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1" style="width:120px;">
            <asp:TextBox ID="txt13" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt1B" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt1S" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
    </div>

    <%-- This section is a repeat of the first, but if we'd like to include a second player in the Parlay. --%>    
    <abbr title="If you would like to include props for multiple players, select a player from this dropdown. Textboxes for their specific prop values will appear upon making a selection." style="font-size:Large; text-decoration-color:grey">Select 2nd Player:</abbr>
    <asp:DropDownList 
        ID="ddlRoster2" runat="server"    DataKeyNames="Player"  AutoPostBack="true" BackColor="White" ForeColor="Black"
        RepeatColumns="4" Width="300px" BorderColor="Black"  class="btn btn-primary dropdown-toggle" data-toggle="dropdown" 
        aria-haspopup="true" aria-expanded="false" OnSelectedIndexChanged="ddlRoster2_SelectedIndexChanged">
        <asp:ListItem Text="Player" Value="" />
    </asp:DropDownList>
    <br />
    <div class="row">
        <div class="col-md-1" style="width:50px;">
            <asp:Label ID="Label1" runat="server" Text="Values:" ForeColor="White" Font-Size="Small" Visible="false"></asp:Label>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt2P" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt2A" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt2R" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1" style="width:120px;">
            <asp:TextBox ID="txt23" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt2B" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt2S" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
    </div>
    
    
    <%-- This section is a repeat of the first and second, but if we'd like to include a third player in the Parlay. --%>   
    <abbr title="Same thing as the 2nd Player dropdown above. Textboxes for their specific prop values will appear upon making a selection." style="font-size:Large; text-decoration-color:grey">Select 3rd Player:</abbr>
    <asp:DropDownList 
        ID="ddlRoster3" runat="server"    DataKeyNames="Player"  AutoPostBack="true" BackColor="White" ForeColor="Black"
        RepeatColumns="4" Width="300px" BorderColor="Black"  class="btn btn-primary dropdown-toggle" data-toggle="dropdown" 
        aria-haspopup="true" aria-expanded="false" OnSelectedIndexChanged="ddlRoster3_SelectedIndexChanged">
        <asp:ListItem Text="Player" Value="" />
    </asp:DropDownList>
    <br />
    <div class="row">
        <div class="col-md-1" style="width:50px;">
            <asp:Label ID="Label2" runat="server" Text="Values:" ForeColor="White" Font-Size="Small" Visible="false"></asp:Label>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt3P" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt3A" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt3R" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1" style="width:120px;">
            <asp:TextBox ID="txt33" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt3B" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
        <div class="col-md-1">
            <asp:TextBox ID="txt3S" runat="server" BackColor="White" ForeColor="Black" Visible="false" Width="40px" MaxLength="4"></asp:TextBox>
        </div>
    </div>



    
    <asp:Button class="btn btn-primary active" ID="btnRetrieve" runat="server" Text="Retrieve" OnClick="btnRetrieve_Click" BackColor="Black" ForeColor="White" BorderColor="#ba9653" />
    <asp:Label ID="lblError" runat="server" Text="" forecolor="Red"></asp:Label>
</asp:Content>
