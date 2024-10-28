<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParlayToolbox.aspx.cs" Inherits="NBAdb.ParlayToolbox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-2" style="width:fit-content">
            <h1 style="width:fit-content">Parlay Toolbox
                <asp:DropDownList ID="ddSeason" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" 
                 runat="server" DataKeyNames="Season" AutoPostBack="true" OnSelectedIndexChanged="ddSeason_SelectedIndexChanged"
                 BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black">
                    <asp:ListItem Text="2024" Value="2024" />
                    <asp:ListItem Text="2023" Value="2023" />
                    <asp:ListItem Text="2022" Value="2022" />
                    <asp:ListItem Text="2021" Value="2021" />
                    <asp:ListItem Text="2020" Value="2020" />
                </asp:DropDownList>
            </h1>
        </div>       
    </div>
    <div class="row" style="height:auto">
        <div class="col">
            <abbr title="Select a team. Once a team has been selected, the four player dropdowns will populate." style="font-size:Large; text-decoration-color:grey">Select Team:</abbr>
            <asp:DropDownList 
                ID="ddTeams"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
                runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
                class="btn btn-primary dropdown-toggle"
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddTeams_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Label ID="tWins" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="col">
            <div class="row">
                <asp:Label ID="t1Name" runat="server" Text="" Font-Size="Large" Font-Bold="true"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col">            
            <abbr title="Select the player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Select Player:</abbr>
            <asp:DropDownList 
                ID="ddlRoster" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList>  
        </div>
        <div class="col" style="margin-top:-20px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="a" runat="server" Text="Points: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 55px">
                    <asp:Label ID="t1Score" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1ScoreR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
        
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <abbr title="Select the second player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Player 2:</abbr>
            <asp:DropDownList 
                ID="ddlRoster2" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster2_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList>  
        </div>
        <div class="col" style="margin-top:-50px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label1" runat="server" Text="Points Against:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 55px">
                    <asp:Label ID="t1ScoreAgainst" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1ScoreAgainstR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <abbr title="Select the third player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Player 3:</abbr>
            <asp:DropDownList 
                ID="ddlRoster3" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster3_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList>  
        </div>
        <div class="col" style="margin-top:-80px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label3" runat="server" Text="FG2M/FG2A:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1FG" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1FGR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <abbr title="Select the fourth player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Player 4:</abbr>
            <asp:DropDownList 
                ID="ddlRoster4" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster4_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList>  
        </div>
        <div class="col" style="margin-top:-110px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label4" runat="server" Text="FG3M/FG3A:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1FG3" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1FG3R" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <abbr title="Select the fifth player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Player 5:</abbr>
            <asp:DropDownList 
                ID="ddlRoster5" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster5_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList>  
        </div>
        <div class="col" style="margin-top:-140px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label2" runat="server" Text="Assists:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 55px">
                    <asp:Label ID="t1Ast" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1AstR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
    <hr />

    <div class="row"> <%--This is the main row containing our full stat section--%>
        <div class="col-md-2" runat="server" id="p1Stats" visible="false" style="width:fit-content"> <%--This is the column that contains a player's stats and info--%>
            <div class="row">
                <asp:Label ID="p1Name" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:fit-content; margin-top:-10px">
                    <asp:Label ID="p1Games" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Points:</label>
                </div>
                <div class="col-sm-1" style="width:40px">
                    <asp:Label ID="p1Pts" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p1PtsExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Assists:</label>
                </div>
                <div class="col-sm-1" style="width:40px">
                    <asp:Label ID="p1Ast" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p1AstExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Boards:</label>
                </div>
                <div class="col-sm-1" style="width:40px">
                    <asp:Label ID="p1Reb" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p1RebExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Twos:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content">
                    <asp:Label ID="p1FG" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p1FGExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Threes:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content">
                    <asp:Label ID="p13" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p13Ext" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Blocks:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content">
                    <asp:Label ID="p1Blk" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p1BlkExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Steals:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content">
                    <asp:Label ID="p1Stl" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p1StlExt" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <%-- p1 -> p2 --%>

        <div class="col-md-2" runat="server" id="p2Stats" visible="false" style="width:fit-content"> <%--This is the column that contains a player's stats and info--%>
            <div class="row">
                <asp:Label ID="p2Name" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:fit-content; margin-top:-10px">
                    <asp:Label ID="p2Games" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Points:</label>
                </div>
                <div class="col-sm-1" style="width:40px">
                    <asp:Label ID="p2Pts" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p2PtsExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Assists:</label>
                </div>
                <div class="col-sm-1" style="width:40px">
                    <asp:Label ID="p2Ast" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p2AstExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Boards:</label>
                </div>
                <div class="col-sm-1" style="width:40px">
                    <asp:Label ID="p2Reb" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p2RebExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Twos:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content">
                    <asp:Label ID="p2FG" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p2FGExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Threes:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content">
                    <asp:Label ID="p23" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p23Ext" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Blocks:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content">
                    <asp:Label ID="p2Blk" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p2BlkExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Steals:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content">
                    <asp:Label ID="p2Stl" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p2StlExt" runat="server"></asp:Label>
                </div>
            </div>
        </div>


    </div>

</asp:Content>
