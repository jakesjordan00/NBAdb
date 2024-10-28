<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParlayAssistantDev.aspx.cs" Inherits="NBAdb.ParlayAssistantDev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Parlay Assistant        
    <asp:DropDownList       
                     ID="ddSeason" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" 
                     runat="server" DataKeyNames="Season" AutoPostBack="true" OnSelectedIndexChanged="ddSeason_SelectedIndexChanged"
                     BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black">
        <asp:ListItem Text="2024" Value="2024" />
        <asp:ListItem Text="2023" Value="2023" />
        <asp:ListItem Text="2022" Value="2022" />
        <asp:ListItem Text="2021" Value="2021" />
        <asp:ListItem Text="2020" Value="2020" />
    </asp:DropDownList>
    </h1>
    <%-- This section hold the Team dropdown. Once a team is selected, the dropdowns will populate with the selected Team's roster. --%>
    <abbr title="Select a team. Once a team has been selected, the four player dropdowns will populate." style="font-size:Large; text-decoration-color:grey">Select Team:</abbr>
    <asp:DropDownList       
                     ID="ddTeams" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" 
                     runat="server" DataKeyNames="Team" AutoPostBack="true" OnSelectedIndexChanged="ddTeams_SelectedIndexChanged"
                     BackColor="White" BorderColor="Black" Font-Size="Large" Width="600px" ForeColor="Black">
    </asp:DropDownList>
    
    <div style="float:right; margin-right:50px">
        <div class="row" style="float:right; width:auto">
            <div class="col-md-4" style="float:right; width:fit-content">
                <asp:Label ID="tPts1"        runat="server" Text="Points: "></asp:Label>
                <br />
                <asp:Label ID="tPtsAgainst1" runat="server" Text="Points Against: "></asp:Label>
                <br />        
                <asp:Label ID="tMOV1"        runat="server" Text="MoV: "></asp:Label>
                <br />        
                <asp:Label ID="tAst1"        runat="server" Text="Assists: "></asp:Label>
                <br />        
                <asp:Label ID="tFG21"        runat="server" Text="FG2M/FG2A: "></asp:Label>
                <br />        
                <asp:Label ID="tFG31"        runat="server" Text="FG3M/FG3A: "></asp:Label>
                <br />        
                <asp:Label ID="tBench1"      runat="server" Text="Bench Points: "></asp:Label>
            </div>
            
            <div class="col-md-4" style="float:right; width:fit-content">
                <asp:Label ID="tPts"        runat="server" Text=""></asp:Label>
                <br />     
                <asp:Label ID="tPtsAgainst" runat="server" Text=""></asp:Label>
                <br />        
                <asp:Label ID="tMOV"        runat="server" Text=""></asp:Label>
                <br />        
                <asp:Label ID="tAst"        runat="server" Text=""></asp:Label>
                <br />        
                <asp:Label ID="tFG2"        runat="server" Text=""></asp:Label>
                <br />        
                <asp:Label ID="tFG3"        runat="server" Text=""></asp:Label>
                <br />        
                <asp:Label ID="tBench"      runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
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
    <div class="row" style="">
        <div class="col-md-1" style="width:50px">
            <abbr title="Select the props to include. By selecting multiple, you can test the percentage of a particular parlay." style="font-size:small; width:30px; text-decoration-color:grey">Props:</abbr>
        </div>
        <div class="col-md-1">
            <asp:CheckBox ID="chkP" runat="server" Text="&nbsp;Points" ForeColor="White" Width="80px" TextAlign="right" OnCheckedChanged="chkP_CheckedChanged" AutoPostBack="true"/>
        </div>
        <div class="col-md-1">
            <asp:CheckBox ID="chkA" runat="server" Text="&nbsp;Assists" ForeColor="White" Width="80px" TextAlign="right" OnCheckedChanged="chkA_CheckedChanged" AutoPostBack="true"/>
        </div>
        <div class="col-md-1" style="width:100px;">
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
    <br />
    <div class="Stats" id="a1StatsSection" runat="server" visible="false">
        <div class="row">
            <%-- Player 1 Averages --%>
            <div class="col-md-3" style="width: auto"> 
                <div class="tight-spacing" style="width: auto"> 
                    <asp:Label ID="a1Name" Font-Bold="true" Font-Size="XX-Large" runat="server"></asp:Label>
                </div>     
                <div class="tight-spacing" style="padding-top:2px; width: auto"> 
                    <asp:Label ID="a1Team" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>  
                <div class="tight-spacing" style="padding-top:2px; width: auto"> 
                    <asp:Label ID="a1Minutes" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>

                 <%--Points--%>
                <div class="row" style="width: auto">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="doesntneedid" runat="server" Text="Points:" ></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a1pd" runat="server">
                            <asp:Label ID="a1Points" runat="server" Text="" ForeColor="White" ></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="doesntneedidlol" runat="server" Text="Assists:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a1ad" runat="server">
                            <asp:Label ID="a1Assists" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="lalbel" runat="server" Text="Rebounds:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a1rd" runat="server">
                            <asp:Label ID="a1Rebounds" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label5" runat="server" Text="3PM:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a13d" runat="server">
                            <asp:Label ID="a1Threes" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label7" runat="server" Text="Blocks:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a1bd" runat="server">
                            <asp:Label ID="a1Blocks" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label9" runat="server" Text="Steals:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a1sd" runat="server">
                            <asp:Label ID="a1Steals" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>                
            </div>

            
            <%-- Player 2 Averages --%>
            <div class="col-md-3" style="width: auto" id="a2StatsSection" runat="server" visible="false"> 
                <div class="tight-spacing"> 
                    <asp:Label ID="a2Name" Font-Bold="true" Font-Size="XX-Large" runat="server"></asp:Label>
                </div>     
                <div class="tight-spacing" style="padding-top:2px"> 
                    <asp:Label ID="a2Team" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>  
                <div class="tight-spacing" style="padding-top:2px"> 
                    <asp:Label ID="a2Minutes" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label3" runat="server" Text="Points:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a2pd" runat="server">
                        <asp:Label ID="a2Points" runat="server" Text="" Height="10px" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label4" runat="server" Text="Assists:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a2ad" runat="server">
                        <asp:Label ID="a2Assists" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label6" runat="server" Text="Rebounds:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a2rd" runat="server">
                        <asp:Label ID="a2Rebounds" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label8" runat="server" Text="3PM:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a23d" runat="server">
                        <asp:Label ID="a2Threes" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label10" runat="server" Text="Blocks:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a2bd" runat="server">
                        <asp:Label ID="a2Blocks" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label11" runat="server" Text="Steals:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a2sd" runat="server">
                        <asp:Label ID="a2Steals" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>                
            </div>

            
            <%-- Player 3 Averages --%>
            <div class="col-md-3" style="width: auto" id="a3StatsSection" runat="server" visible="false"> 
                <div class="tight-spacing"> 
                    <asp:Label ID="a3Name" Font-Bold="true" Font-Size="XX-Large" runat="server"></asp:Label>
                </div>     
                <div class="tight-spacing" style="padding-top:2px"> 
                    <asp:Label ID="a3Team" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>  
                <div class="tight-spacing" style="padding-top:2px"> 
                    <asp:Label ID="a3Minutes" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label12" runat="server" Text="Points:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a3pd" runat="server">
                        <asp:Label ID="a3Points" runat="server" Text="" Height="10px" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label13" runat="server" Text="Assists:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a3ad" runat="server">
                        <asp:Label ID="a3Assists" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label14" runat="server" Text="Rebounds:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a3rd" runat="server">
                        <asp:Label ID="a3Rebounds" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label15" runat="server" Text="3PM:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey;" id="a33d" runat="server">
                        <asp:Label ID="a3Threes" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label16" runat="server" Text="Blocks:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a3bd" runat="server">
                        <asp:Label ID="a3Blocks" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label17" runat="server" Text="Steals:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="a3sd" runat="server">
                        <asp:Label ID="a3Steals" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>                
            </div>
        </div>
        <div style="height:25px; padding-top:5px">
            <asp:CheckBox ID="aWinsChk" runat="server" OnCheckedChanged="aWinsChk_CheckedChanged" AutoPostBack="true" Text="Show only Wins"/>
        </div>
        <div style="height:20px">
            <asp:CheckBox ID="aLossChk" runat="server" OnCheckedChanged="aLossChk_CheckedChanged" AutoPostBack="true" Text="Show only Losses"/>
        </div>
        <div style="height:20px">
            <asp:Label ID="Label18" runat="server" Text="The pop-up number displayed with each value is either the player's trend - average, win - loss avg, or loss - win avg" ForeColor="Gray" Font-Size="Small"></asp:Label>
        </div>
        


        <%-- Dynamic columns --%>
        <hr />
        <div class="row" style="width: auto;" id="dyStatsSection" runat="server" visible="false">            
            <%-- Player 1 Averages --%>
            <div class="col-md-3" style="width: auto" id="dy1StatsSection" runat="server" visible="false"> 
                <div class="tight-spacing" style="width: auto"> 
                    <asp:Label ID="dyName1" Font-Bold="true" Font-Size="XX-Large" runat="server"></asp:Label>
                </div>     
                <div class="tight-spacing" style="padding-top:2px; width: auto"> 
                    <asp:Label ID="dyTeam1" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>  
                <div class="tight-spacing" style="padding-top:2px; width: auto"> 
                    <asp:Label ID="dyMinutes1" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>
                 <%--Points--%>
                <div class="row" style="width: auto">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label22" runat="server" Text="Points:" ></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr1" runat="server">
                            <asp:Label ID="dyPts1" runat="server" Text="" ForeColor="White" ></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label24" runat="server" Text="Assists:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr2" runat="server">
                            <asp:Label ID="dyAst1" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label26" runat="server" Text="Rebounds:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr3" runat="server">
                            <asp:Label ID="dyReb1" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label28" runat="server" Text="3PM:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr4" runat="server">
                            <asp:Label ID="dy31" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label30" runat="server" Text="Blocks:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr5" runat="server">
                            <asp:Label ID="dyBlk1" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label32" runat="server" Text="Steals:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr6" runat="server">
                            <asp:Label ID="dyStl1" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>                
            </div>


                    
            <%-- Player 2 Averages --%>
            <div class="col-md-3" style="width: auto" id="dy2StatsSection" runat="server" visible="false"> 
                <div class="tight-spacing" style="width: auto"> 
                    <asp:Label ID="dyName2" Font-Bold="true" Font-Size="XX-Large" runat="server"></asp:Label>
                </div>     
                <div class="tight-spacing" style="padding-top:2px; width: auto"> 
                    <asp:Label ID="dyTeam2" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>  
                <div class="tight-spacing" style="padding-top:2px; width: auto"> 
                    <asp:Label ID="dyMinutes2" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>
                 <%--Points--%>
                <div class="row" style="width: auto">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label19" runat="server" Text="Points:" ></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr7" runat="server">
                            <asp:Label ID="dyPts2" runat="server" Text="" ForeColor="White" ></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label20" runat="server" Text="Assists:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr8" runat="server">
                            <asp:Label ID="dyAst2" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label21" runat="server" Text="Rebounds:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr9" runat="server">
                            <asp:Label ID="dyReb2" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label23" runat="server" Text="3PM:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr10" runat="server">
                            <asp:Label ID="dy32" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label25" runat="server" Text="Blocks:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr11" runat="server">
                            <asp:Label ID="dyBlk2" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label27" runat="server" Text="Steals:"></asp:Label>
                    </div>
                    <div class="col-md-2" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr12" runat="server">
                            <asp:Label ID="dyStl2" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>                
            </div>
                    
            <%-- Player 3 Averages --%>
            <div class="col-md-3" style="width: auto" id="dy3StatsSection" runat="server" visible="false"> 
                <div class="tight-spacing" style="width: auto"> 
                    <asp:Label ID="dyName3" Font-Bold="true" Font-Size="XX-Large" runat="server"></asp:Label>
                </div>     
                <div class="tight-spacing" style="padding-top:2px; width: auto"> 
                    <asp:Label ID="dyTeam3" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>  
                <div class="tight-spacing" style="padding-top:2px; width: auto"> 
                    <asp:Label ID="dyMinutes3" runat="server" Text="" Font-Bold="true" Font-Size="Small" ForeColor="LightGray" Height="10px" ></asp:Label>
                </div>
                 <%--Points--%>
                <div class="row" style="width: auto">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label29" runat="server" Text="Points:" ></asp:Label>
                    </div>
                    <div class="col-md-3" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr13" runat="server">
                            <asp:Label ID="dyPts3" runat="server" Text="" ForeColor="White" ></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label31" runat="server" Text="Assists:"></asp:Label>
                    </div>
                    <div class="col-md-3" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr14" runat="server">
                            <asp:Label ID="dyAst3" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label33" runat="server" Text="Rebounds:"></asp:Label>
                    </div>
                    <div class="col-md-3" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr15" runat="server">
                            <asp:Label ID="dyReb3" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label34" runat="server" Text="3PM:"></asp:Label>
                    </div>
                    <div class="col-md-3" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr16" runat="server">
                            <asp:Label ID="dy33" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px; padding-top:5px">
                        <asp:Label ID="Label35" runat="server" Text="Blocks:"></asp:Label>
                    </div>
                    <div class="col-md-3" style="text-align:right; padding-top:5px">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr17" runat="server">
                            <asp:Label ID="dyBlk3" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="width: 170px">
                        <asp:Label ID="Label36" runat="server" Text="Steals:"></asp:Label>
                    </div>
                    <div class="col-md-3" style="text-align:right;">
                        <abbr style="font-size:Large; text-decoration-color:grey" id="Abbr18" runat="server">
                            <asp:Label ID="dyStl3" runat="server" Text="" ForeColor="White"></asp:Label>
                        </abbr>
                    </div>
                </div>                
            </div>
        </div>
    </div>
<style>
        .tight-spacing {
    line-height: .9; /* Adjust this value as needed */
}
</style>


</asp:Content>

