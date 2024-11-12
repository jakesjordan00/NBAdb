<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParlayToolbox.aspx.cs" Inherits="NBAdb.ParlayToolbox" MaintainScrollPositionOnPostBack="true" %>
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
        <div class="col-md-9">
            <abbr title="Select a team. Once a team has been selected, the four player dropdowns will populate." style="font-size:Large; text-decoration-color:grey">Select Team:</abbr>
            <asp:DropDownList 
                ID="ddTeams"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
                runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
                class="btn btn-primary dropdown-toggle"
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddTeams_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Label ID="tWins" runat="server" Visible="false"></asp:Label>
            <asp:DropDownList 
                ID="ddTeam2"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
                runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
                class="btn btn-primary dropdown-toggle"
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddTeam2_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Label ID="t2Wins" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="col">
            <div class="row">
                <asp:Label ID="t1Name" runat="server" Text="" Font-Size="Large" Font-Bold="true"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">            
            <abbr title="Select the player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Select Player:</abbr>
            <asp:DropDownList 
                ID="ddlRoster" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList>  
            <asp:DropDownList 
            ID="ddlDNP" runat="server" aria-haspopup="true" aria-expanded="false"    
            DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
            AutoPostBack="true" 
            class="btn btn-primary dropdown-toggle"                
            OnSelectedIndexChanged="ddlDNP_SelectedIndexChanged">
            <asp:ListItem Text="DNP Player" Value="" />
        </asp:DropDownList>
            <%--<label style="margin-left:0px; font-size:12px; width:fit-content; padding:0px;">Pts</label>--%>
            <asp:CheckBox ID="chkP1Pts" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Pts"/>
            <asp:TextBox ID="txtP1Pts" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP1Ast" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Ast"/>
            <asp:TextBox ID="txtP1Ast" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP1Reb" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Reb"/>
            <asp:TextBox ID="txtP1Reb" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP13" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="3PM"/>
            <asp:TextBox ID="txtP13" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP1Blk" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Blk"/>
            <asp:TextBox ID="txtP1Blk" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP1Stl" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Stl"/>
            <asp:TextBox ID="txtP1Stl" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP1PA" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A"/>
            <asp:TextBox ID="txtP1PA" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP1PR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+R"/>
            <asp:TextBox ID="txtP1PR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP1AR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="A+R"/>
            <asp:TextBox ID="txtP1AR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP1PAR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A+R"/>
            <asp:TextBox ID="txtP1PAR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
        </div>
        <div class="col" style="margin-top:-20px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="a" runat="server" Text="Points: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1Score" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1ScoreR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
        
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <abbr title="Select the second player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Player 2:</abbr>
            <asp:DropDownList 
                ID="ddlRoster2" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster2_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList>  
            <asp:DropDownList 
            ID="ddlDNP2" runat="server" aria-haspopup="true" aria-expanded="false"    
            DataKeyNames="DNP Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
            AutoPostBack="true" 
            class="btn btn-primary dropdown-toggle"                
            OnSelectedIndexChanged="ddlDNP2_SelectedIndexChanged">
            <asp:ListItem Text="DNP Player" Value="" />
        </asp:DropDownList>  
            <asp:CheckBox ID="chkP2Pts" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Pts"/>
            <asp:TextBox ID="txtP2Pts" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP2Ast" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Ast"/>
            <asp:TextBox ID="txtP2Ast" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP2Reb" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Reb"/>
            <asp:TextBox ID="txtP2Reb" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP23" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="3PM"/>
            <asp:TextBox ID="txtP23" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP2Blk" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Blk"/>
            <asp:TextBox ID="txtP2Blk" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP2Stl" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Stl"/>
            <asp:TextBox ID="txtP2Stl" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP2PA" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A"/>
            <asp:TextBox ID="txtP2PA" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP2PR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+R"/>
            <asp:TextBox ID="txtP2PR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP2AR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="A+R"/>
            <asp:TextBox ID="txtP2AR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP2PAR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A+R"/>
            <asp:TextBox ID="txtP2PAR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
			
        </div>
        <div class="col" style="margin-top:-50px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label1" runat="server" Text="Points Against:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1ScoreAgainst" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1ScoreAgainstR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <abbr title="Select the third player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Player 3:</abbr>
            <asp:DropDownList 
                ID="ddlRoster3" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster3_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList>  
            <asp:DropDownList 
            ID="ddlDNP3" runat="server" aria-haspopup="true" aria-expanded="false"    
            DataKeyNames="DNP Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
            AutoPostBack="true" 
            class="btn btn-primary dropdown-toggle"                
            OnSelectedIndexChanged="ddlDNP3_SelectedIndexChanged">
            <asp:ListItem Text="DNP Player" Value="" />
        </asp:DropDownList>  
            <asp:CheckBox ID="chkP3Pts" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Pts"/>
            <asp:TextBox ID="txtP3Pts" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP3Ast" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Ast"/>
            <asp:TextBox ID="txtP3Ast" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP3Reb" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Reb"/>
            <asp:TextBox ID="txtP3Reb" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP33" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="3PM"/>
            <asp:TextBox ID="txtP33" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP3Blk" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Blk"/>
            <asp:TextBox ID="txtP3Blk" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP3Stl" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Stl"/>
            <asp:TextBox ID="txtP3Stl" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP3PA" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A"/>
            <asp:TextBox ID="txtP3PA" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP3PR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+R"/>
            <asp:TextBox ID="txtP3PR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP3AR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="A+R"/>
            <asp:TextBox ID="txtP3AR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP3PAR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A+R"/>
            <asp:TextBox ID="txtP3PAR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
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
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <abbr title="Select the fourth player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Player 4:</abbr>
            <asp:DropDownList 
                ID="ddlRoster4" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster4_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList>
            <asp:DropDownList 
            ID="ddlDNP4" runat="server" aria-haspopup="false" aria-expanded="false"    
            DataKeyNames="DNP Player"  BackColor="#3c3c3c" BorderColor="#3c3c3c" Font-Size="Medium" ForeColor="#3c3c3c" 
            class="btn btn-primary dropdown-toggle" Enabled="false">
            <asp:ListItem Text="DNP Player" Value="" />
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&thinsp;
            <asp:CheckBox ID="chkP4Pts" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Pts"/>
            <asp:TextBox ID="txtP4Pts" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP4Ast" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Ast"/>
            <asp:TextBox ID="txtP4Ast" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP4Reb" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Reb"/>
            <asp:TextBox ID="txtP4Reb" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP43" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="3PM"/>
            <asp:TextBox ID="txtP43" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP4Blk" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Blk"/>
            <asp:TextBox ID="txtP4Blk" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP4Stl" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Stl"/>
            <asp:TextBox ID="txtP4Stl" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP4PA" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A"/>
            <asp:TextBox ID="txtP4PA" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP4PR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+R"/>
            <asp:TextBox ID="txtP4PR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP4AR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="A+R"/>
            <asp:TextBox ID="txtP4AR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP4PAR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A+R"/>
            <asp:TextBox ID="txtP4PAR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
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
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <abbr title="Select the fifth player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Player 5:</abbr>
            <asp:DropDownList 
                ID="ddlRoster5" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster5_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList> 
            <asp:DropDownList 
            ID="ddlDNP5" runat="server" aria-haspopup="false" aria-expanded="false"   
            DataKeyNames="DNP Player"  BackColor="#3c3c3c" BorderColor="#3c3c3c" Font-Size="Medium" ForeColor="#3c3c3c"
            class="btn btn-primary dropdown-toggle" Enabled="false">
            <asp:ListItem Text="DNP Player" Value="" />
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&thinsp;
            <asp:CheckBox ID="chkP5Pts" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Pts"/>
            <asp:TextBox ID="txtP5Pts" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP5Ast" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Ast"/>
            <asp:TextBox ID="txtP5Ast" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP5Reb" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Reb"/>
            <asp:TextBox ID="txtP5Reb" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP53" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="3PM"/>
            <asp:TextBox ID="txtP53" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP5Blk" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Blk"/>
            <asp:TextBox ID="txtP5Blk" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP5Stl" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Stl"/>
            <asp:TextBox ID="txtP5Stl" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP5PA" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A"/>
            <asp:TextBox ID="txtP5PA" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP5PR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+R"/>
            <asp:TextBox ID="txtP5PR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP5AR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="A+R"/>
            <asp:TextBox ID="txtP5AR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP5PAR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A+R"/>
            <asp:TextBox ID="txtP5PAR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
        </div>
        <div class="col" style="margin-top:-140px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label21" runat="server" Text="FTM/FTA:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1FT" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1FTR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <abbr title="Select the sixth player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Player 6:</abbr>
            <asp:DropDownList 
                ID="ddlRoster6" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster6_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList> 
            <asp:DropDownList 
            ID="ddlDNP6" runat="server" aria-haspopup="false" aria-expanded="false"   
            DataKeyNames="DNP Player"  BackColor="#3c3c3c" BorderColor="#3c3c3c" Font-Size="Medium" ForeColor="#3c3c3c"
            class="btn btn-primary dropdown-toggle" Enabled="false">
            <asp:ListItem Text="DNP Player" Value="" />
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&thinsp;
            <asp:CheckBox ID="chkP6Pts" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Pts"/>
            <asp:TextBox ID="txtP6Pts" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP6Ast" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Ast"/>
            <asp:TextBox ID="txtP6Ast" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP6Reb" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Reb"/>
            <asp:TextBox ID="txtP6Reb" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP63" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="3PM"/>
            <asp:TextBox ID="txtP63" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP6Blk" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Blk"/>
            <asp:TextBox ID="txtP6Blk" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP6Stl" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Stl"/>
            <asp:TextBox ID="txtP6Stl" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP6PA" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A"/>
            <asp:TextBox ID="txtP6PA" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP6PR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+R"/>
            <asp:TextBox ID="txtP6PR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP6AR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="A+R"/>
            <asp:TextBox ID="txtP6AR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP6PAR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A+R"/>
            <asp:TextBox ID="txtP6PAR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
        </div>
        <div class="col" style="margin-top:-170px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label22" runat="server" Text="Bench Pts:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1Bench" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1BenchR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <abbr title="Select the seventh player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Player 7:</abbr>
            <asp:DropDownList 
                ID="ddlRoster7" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster7_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList> 
            <asp:DropDownList 
            ID="ddlDNP7" runat="server" aria-haspopup="false" aria-expanded="false"   
            DataKeyNames="DNP Player"  BackColor="#3c3c3c" BorderColor="#3c3c3c" Font-Size="Medium" ForeColor="#3c3c3c"
            class="btn btn-primary dropdown-toggle" Enabled="false">
            <asp:ListItem Text="DNP Player" Value="" />
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&thinsp;
            <asp:CheckBox ID="chkP7Pts" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Pts"/>
            <asp:TextBox ID="txtP7Pts" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP7Ast" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Ast"/>
            <asp:TextBox ID="txtP7Ast" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP7Reb" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Reb"/>
            <asp:TextBox ID="txtP7Reb" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP73" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="3PM"/>
            <asp:TextBox ID="txtP73" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP7Blk" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Blk"/>
            <asp:TextBox ID="txtP7Blk" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP7Stl" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Stl"/>
            <asp:TextBox ID="txtP7Stl" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP7PA" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A"/>
            <asp:TextBox ID="txtP7PA" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP7PR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+R"/>
            <asp:TextBox ID="txtP7PR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP7AR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="A+R"/>
            <asp:TextBox ID="txtP7AR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP7PAR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A+R"/>
            <asp:TextBox ID="txtP7PAR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
        </div>
        <div class="col" style="margin-top:-200px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label6" runat="server" Text="Q1 Pts/OpPts:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1Q1" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1Q1R" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <abbr title="Select the eighth player who you would like to include in your prop" style="font-size:Large; text-decoration-color:grey">Player 8:</abbr>
            <asp:DropDownList 
                ID="ddlRoster8" runat="server" aria-haspopup="true" aria-expanded="false"    
                DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
                AutoPostBack="true" 
                class="btn btn-primary dropdown-toggle"                
                OnSelectedIndexChanged="ddlRoster8_SelectedIndexChanged">
                <asp:ListItem Text="Player" Value="" />
            </asp:DropDownList> 
            <asp:DropDownList 
            ID="ddlDNP8" runat="server" aria-haspopup="false" aria-expanded="false"   
            DataKeyNames="DNP Player"  BackColor="#3c3c3c" BorderColor="#3c3c3c" Font-Size="Medium" ForeColor="#3c3c3c"
            class="btn btn-primary dropdown-toggle" Enabled="false">
            <asp:ListItem Text="DNP Player" Value="" />
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&thinsp;
            <asp:CheckBox ID="chkP8Pts" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Pts"/>
            <asp:TextBox ID="txtP8Pts" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP8Ast" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Ast"/>
            <asp:TextBox ID="txtP8Ast" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP8Reb" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Reb"/>
            <asp:TextBox ID="txtP8Reb" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP83" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="3PM"/>
            <asp:TextBox ID="txtP83" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP8Blk" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Blk"/>
            <asp:TextBox ID="txtP8Blk" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP8Stl" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="Stl"/>
            <asp:TextBox ID="txtP8Stl" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP8PA" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A"/>
            <asp:TextBox ID="txtP8PA" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP8PR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+R"/>
            <asp:TextBox ID="txtP8PR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP8AR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="A+R"/>
            <asp:TextBox ID="txtP8AR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
            <asp:CheckBox ID="chkP8PAR" runat="server" Enabled="false" CssClass="checkBox" AutoPostBack="true" Text="P+A+R"/>
            <asp:TextBox ID="txtP8PAR" runat="server" Width="40px" Enabled="false" BackColor="#3c3c3c" BorderColor="White" ForeColor="White"></asp:TextBox>
        </div>
        <div class="col" style="margin-top:-230px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label7" runat="server" Text="Q2 Pts/OpPts:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1Q2" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1Q2R" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-260px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label8" runat="server" Text="Q3 Pts/OpPts:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1Q3" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1Q3R" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-279px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label9" runat="server" Text="Q4 Pts/OpPts:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1Q4" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1Q4R" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-298px">
            <div class="row" style="margin-top:0px;">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label2" runat="server" Text="Assists:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1Ast" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1AstR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-317px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label5" runat="server" Text="Rebounds:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1Reb" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1RebR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-336px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label28" runat="server" Text="ORB/DRB:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1ODReb" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1ODRebR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-355px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label20" runat="server" Text="Blocks/Blocked:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1Blks" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1BlksR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <div class="col" style="margin-top:-374px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="Label27" runat="server" Text="Steals/TOs:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t1Stl" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t1StlR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <div class="col" style="margin-top:-340px">
            <div class="row">
                <asp:Label ID="t2Name" runat="server" Text="" Font-Size="Large" Font-Bold="true"></asp:Label>
            </div>
        </div>           
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <div class="col" style="margin-top:-350px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team2a" runat="server" Text="Points: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2Score" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2ScoreR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
	
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <div class="col" style="margin-top:-370px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team21" runat="server" Text="Points Against:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2ScoreAgainst" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2ScoreAgainstR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
	
	
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <div class="col" style="margin-top:-390px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team23" runat="server" Text="FG2M/FG2A:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2FG" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2FGR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
	
	
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <div class="col" style="margin-top:-410px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team24" runat="server" Text="FG3M/FG3A:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2FG3" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2FG3R" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
	
	
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <div class="col" style="margin-top:-430px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team221" runat="server" Text="FTM/FTA:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2FT" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2FTR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <div class="col" style="margin-top:-450px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team222" runat="server" Text="Bench Pts:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2Bench" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2BenchR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <div class="col" style="margin-top:-470px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team26" runat="server" Text="Q1 Pts/OpPts:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2Q1" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2Q1R" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <div class="col" style="margin-top:-490px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team27" runat="server" Text="Q2 Pts/OpPts:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2Q2" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2Q2R" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-510px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team28" runat="server" Text="Q3 Pts/OpPts:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2Q3" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2Q3R" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-530px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team29" runat="server" Text="Q4 Pts/OpPts:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2Q4" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2Q4R" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-550px">
            <div class="row" style="margin-top:0px;">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team22" runat="server" Text="Assists:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2Ast" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2AstR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-570px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team25" runat="server" Text="Rebounds:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2Reb" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2RebR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-590px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team228" runat="server" Text="ORB/DRB:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2ODReb" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2ODRebR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="col" style="margin-top:-610px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team220" runat="server" Text="Blocks/Blocked:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2Blks" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2BlksR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="height:auto; padding-top: 10px">
        <div class="col-md-9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </div>
        <div class="col" style="margin-top:-630px">
            <div class="row" style="margin-top:0px">
                <div class="col-md-1" style="width:125px; padding-right:0px">
                    <asp:Label ID="team227" runat="server" Text="Steals/TOs:" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-sm-1" style="width: 95px">
                    <asp:Label ID="t2Stl" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-md-1" style="width:auto">
                    <asp:Label ID="t2StlR" runat="server" Text="" Font-Size="small" Font-Bold="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <div class="row" style="height:auto; margin-top: -780px; width:fit-content">
        <div class="col-md-3" style="width:fit-content">
            <asp:CheckBox ID="chkT1Wins" runat="server" Text="Show only Wins" OnCheckedChanged="chkT1Wins_CheckedChanged" AutoPostBack="true"/><br />
            <asp:CheckBox ID="chkT1Losses" runat="server" Text="Show only Losses" OnCheckedChanged="chkT1Losses_CheckedChanged" AutoPostBack="true"/><br />
            <asp:CheckBox ID="chkT1Dynamic" runat="server" Text="Toggle Dynamic Stats" OnCheckedChanged="chkT1Dynamic_CheckedChanged" AutoPostBack="true"/>
        </div>
        <div class="col-md-3" style="width:fit-content; line-height:15px;" runat="server" id="playerPicks">
            <div class="row" style="height:fit-content; width: fit-content">
            <asp:Label ID="p1Picks" runat="server" Text="" Font-Size="Small"></asp:Label>
            </div>
            <div class="row" style="height:fit-content; width: fit-content">
            <asp:Label ID="p2Picks" runat="server" Text="" Font-Size="Small"></asp:Label>
            </div>
            <div class="row" style="height:fit-content; width: fit-content">
            <asp:Label ID="p3Picks" runat="server" Text="" Font-Size="Small"></asp:Label>
            </div>
            <div class="row" style="height:fit-content; width: fit-content">
            <asp:Label ID="p4Picks" runat="server" Text="" Font-Size="Small"></asp:Label>
            </div>
            <div class="row" style="height:fit-content; width: fit-content">
            <asp:Label ID="p5Picks" runat="server" Text="" Font-Size="Small"></asp:Label>
            </div>
            <div class="row" style="height:fit-content; width: fit-content">
            <asp:Label ID="p6Picks" runat="server" Text="" Font-Size="Small"></asp:Label>
            </div>
            <div class="row" style="height:fit-content; width: fit-content">
            <asp:Label ID="p7Picks" runat="server" Text="" Font-Size="Small"></asp:Label>
            </div>
            <div class="row" style="height:fit-content; width: fit-content">
            <asp:Label ID="p8Picks" runat="server" Text="" Font-Size="Small"></asp:Label>
            </div>
            <div class="row" style="height:fit-content; width: fit-content">
            <asp:Label ID="DNP1" runat="server" Text="" Font-Size="Small"></asp:Label>
            </div>
            <div class="row" style="height:fit-content; width: fit-content">
            <asp:Label ID="DNP2" runat="server" Text="" Font-Size="Small"></asp:Label>
            </div>
            <div class="row" style="height:fit-content; width: fit-content">
            <asp:Label ID="DNP3" runat="server" Text="" Font-Size="Small"></asp:Label>
            </div>
        </div>
    </div>
    <asp:Button ID="btnRetrieve" runat="server" Text="Get Statistical Odds" class="btn btn-primary active" BackColor="Black" ForeColor="White" BorderColor="#ba9653" OnClick="btnRetrieve_Click"/>
    <asp:Button ID="btnSave" runat="server" Text="Save Parlay" class="btn btn-primary active" BackColor="SkyBlue" ForeColor="White" BorderColor="White" OnClick="btnSave_Click" Enabled="false"/>
    <asp:Button ID="btnLoad" runat="server" Text="Load Parlay" class="btn btn-primary active" BackColor="#d9efd3" ForeColor="Black" BorderColor="Black" OnClick="btnLoad_Click" Enabled="true"/>    
        <asp:DropDownList 
            ID="ddlLoad" runat="server" aria-haspopup="true" aria-expanded="false"    
            DataKeyNames="Player"  BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black" 
            AutoPostBack="true"  Width="700px"
            class="btn btn-primary dropdown-toggle"                
            OnSelectedIndexChanged="ddlLoad_SelectedIndexChanged">
            <asp:ListItem Text="Saved Parlay" Value="" />
        </asp:DropDownList>
    <asp:Button ID="btnFill" runat="server" Text="Fill Minimums" class="btn btn-primary active" BackColor="#d9efd3" ForeColor="Black" BorderColor="Black" OnClick="btnFill_Click" Enabled="true"/>  
    <asp:Button ID="btnFillMax" runat="server" Text="Fill Maximums" class="btn btn-primary active" BackColor="#d9efd3" ForeColor="Black" BorderColor="Black" OnClick="btnFillMax_Click" Enabled="true"/>  
    <asp:Label ID="lblError" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lblOdds" runat="server" Text="" Font-Bold="true"></asp:Label><br />
    <asp:Label ID="lblOddsW" runat="server" Text="" Font-Bold="true"></asp:Label><br />
    <asp:Label ID="lblOddsL" runat="server" Text="" Font-Bold="true"></asp:Label>
    <hr />

    <div class="row"> <%--This is the main row containing our full stat section--%>
        <div class="col-md-2" runat="server" id="p1Stats" visible="false" style="width:fit-content"> <%--This is the column that contains a player's stats and info--%>
            <div class="row">
                <asp:Label ID="p1Name" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:fit-content; margin-top:-10px">
                    <asp:Label ID="p1Games" runat="server" Font-Size="Small"></asp:Label>
                    <asp:Label ID="p1GamesExt" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Points:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p1Pts" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px; padding-left:10px">
                    <asp:Label ID="p1PtsExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Assists:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
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
                <div class="col-sm-1" style="width:50px">
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
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p1FG" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p1FGExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Threes:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p13" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p13Ext" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>FTs:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p1ft" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p1ftExt" runat="server"></asp:Label>
                </div>
            </div>			
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Blocks:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
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
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
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
                    <asp:Label ID="p2GamesExt" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Points:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
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
                <div class="col-sm-1" style="width:50px">
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
                <div class="col-sm-1" style="width:50px">
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
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p2FG" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p2FGExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Threes:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p23" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p23Ext" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>FTs:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p2ft" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p2ftExt" runat="server"></asp:Label>
                </div>
            </div>			
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Blocks:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
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
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p2Stl" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p2StlExt" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <%-- p2 -> p3 --%>
        
        <div class="col-md-2" runat="server" id="p3Stats" visible="false" style="width:fit-content"> <%--This is the column that contains a player's stats and info--%>
            <div class="row">
                <asp:Label ID="p3Name" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:fit-content; margin-top:-10px">
                    <asp:Label ID="p3Games" runat="server" Font-Size="Small"></asp:Label>
                    <asp:Label ID="p3GamesExt" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Points:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p3Pts" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p3PtsExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Assists:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p3Ast" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p3AstExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Boards:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p3Reb" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p3RebExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Twos:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p3FG" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p3FGExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Threes:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p33" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p33Ext" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>FTs:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p3ft" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p3ftExt" runat="server"></asp:Label>
                </div>
            </div>			
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Blocks:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p3Blk" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p3BlkExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Steals:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p3Stl" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p3StlExt" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <%-- p3 -> p4 --%>
        
        <div class="col-md-2" runat="server" id="p4Stats" visible="false" style="width:fit-content"> <%--This is the column that contains a player's stats and info--%>
            <div class="row">
                <asp:Label ID="p4Name" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:fit-content; margin-top:-10px">
                    <asp:Label ID="p4Games" runat="server" Font-Size="Small"></asp:Label>
                    <asp:Label ID="p4GamesExt" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Points:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p4Pts" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p4PtsExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Assists:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p4Ast" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p4AstExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Boards:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p4Reb" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p4RebExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Twos:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p4FG" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p4FGExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Threes:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p43" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p43Ext" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>FTs:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p4ft" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p4ftExt" runat="server"></asp:Label>
                </div>
            </div>			
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Blocks:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p4Blk" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p4BlkExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Steals:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p4Stl" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p4StlExt" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <%-- p4 -> p5 --%>
        
        <div class="col-md-2" runat="server" id="p5Stats" visible="false" style="width:fit-content"> <%--This is the column that contains a player's stats and info--%>
            <div class="row">
                <asp:Label ID="p5Name" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:fit-content; margin-top:-10px">
                    <asp:Label ID="p5Games" runat="server" Font-Size="Small"></asp:Label>
                    <asp:Label ID="p5GamesExt" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Points:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p5Pts" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p5PtsExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Assists:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p5Ast" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p5AstExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Boards:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p5Reb" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p5RebExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Twos:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p5FG" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p5FGExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Threes:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p53" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p53Ext" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>FTs:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p5ft" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p5ftExt" runat="server"></asp:Label>
                </div>
            </div>			
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Blocks:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p5Blk" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p5BlkExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Steals:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p5Stl" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p5StlExt" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        
        <%-- p2 -> p6 --%>

        <div class="col-md-2" runat="server" id="p6Stats" visible="false" style="width:fit-content"> <%--This is the column that contains a player's stats and info--%>
            <div class="row">
                <asp:Label ID="p6Name" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:fit-content; margin-top:-10px">
                    <asp:Label ID="p6Games" runat="server" Font-Size="Small"></asp:Label>
                    <asp:Label ID="p6GamesExt" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Points:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p6Pts" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p6PtsExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Assists:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p6Ast" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p6AstExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Boards:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p6Reb" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p6RebExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Twos:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p6FG" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p6FGExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Threes:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p63" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p63Ext" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>FTs:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p6ft" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p6ftExt" runat="server"></asp:Label>
                </div>
            </div>			
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Blocks:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p6Blk" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p6BlkExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Steals:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p6Stl" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p6StlExt" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        
        <%-- p6 -> p7 --%>

        <div class="col-md-2" runat="server" id="p7Stats" visible="false" style="width:fit-content"> <%--This is the column that contains a player's stats and info--%>
            <div class="row">
                <asp:Label ID="p7Name" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:fit-content; margin-top:-10px">
                    <asp:Label ID="p7Games" runat="server" Font-Size="Small"></asp:Label>
                    <asp:Label ID="p7GamesExt" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Points:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p7Pts" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p7PtsExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Assists:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p7Ast" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p7AstExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Boards:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p7Reb" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p7RebExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Twos:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p7FG" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p7FGExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Threes:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p73" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p73Ext" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>FTs:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p7ft" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p7ftExt" runat="server"></asp:Label>
                </div>
            </div>			
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Blocks:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p7Blk" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p7BlkExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Steals:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p7Stl" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p7StlExt" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        
        <%-- p7 -> p8 --%>

        <div class="col-md-2" runat="server" id="p8Stats" visible="false" style="width:fit-content"> <%--This is the column that contains a player's stats and info--%>
            <div class="row">
                <asp:Label ID="p8Name" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:fit-content; margin-top:-10px">
                    <asp:Label ID="p8Games" runat="server" Font-Size="Small"></asp:Label>
                    <asp:Label ID="p8GamesExt" runat="server" Font-Size="Small"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Points:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p8Pts" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p8PtsExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Assists:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p8Ast" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p8AstExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Boards:</label>
                </div>
                <div class="col-sm-1" style="width:50px">
                    <asp:Label ID="p8Reb" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p8RebExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Twos:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p8FG" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p8FGExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Threes:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p83" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p83Ext" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>FTs:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p8ft" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding: 2px; overflow:visible; margin-top:-3px; margin-left:-1px">
                    <asp:Label ID="p8ftExt" runat="server"></asp:Label>
                </div>
            </div>			
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Blocks:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p8Blk" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p8BlkExt" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-1" style="width:60px">
                    <label>Steals:</label>
                </div>
                <div class="col-sm-1" style="width:fit-content; padding-right:0px">
                    <asp:Label ID="p8Stl" runat="server"></asp:Label>
                </div>
                <div class="col-sm-1" style="width:60px">
                    <asp:Label ID="p8StlExt" runat="server"></asp:Label>
                </div>
            </div>
        </div>

    </div>






<style>
    .checkBox{
        font-size:small;
        margin-left:0px;
        width:fit-content; 
        padding:0px;
    }

</style>
</asp:Content>

