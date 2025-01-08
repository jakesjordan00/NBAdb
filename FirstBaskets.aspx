<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FirstBaskets.aspx.cs" Inherits="NBAdb.FirstBaskets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>First Baskets</h1>
    <div class="row">
        <div class="col-md-3" style="width:500px">
            <abbr title="Select a team. Once a team has been selected, the four player dropdowns will populate." style="font-size:Large; text-decoration-color:grey">
                <abbr style="font-size:small; color:red">*</abbr> 
                Team:</abbr>
            <asp:DropDownList 
                ID="ddTeams"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
                runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
                class="btn btn-primary dropdown-toggle"
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddTeams_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Label ID="lblTeam" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-md-3" style="width:550px">
            <abbr title="Select an Oppenent." style="font-size:Large; text-decoration-color:grey">Select Oppenent:</abbr>
            <asp:DropDownList 
                ID="ddOpponent"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
                runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
                class="btn btn-primary dropdown-toggle"
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddOpponent_SelectedIndexChanged">
            </asp:DropDownList>            
            <asp:Label ID="lblOpponent" runat="server" Text=""></asp:Label>
        </div>
        
    </div>
    <div class="row">
        <div class="col-md-3" style="width:500px">
            <abbr style="font-size:Large;">
                Tipoff player:</abbr>
            <asp:DropDownList 
                ID="ddTeamTip"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
                runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
                class="btn btn-primary dropdown-toggle"
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddTeamTip_SelectedIndexChanged">
            </asp:DropDownList>            
            <asp:Label ID="lblTipPct" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-md-3" style="width:450px">
            <abbr style="font-size:Large;">Oppenent Tipoff player:</abbr>
            <asp:DropDownList 
                ID="ddOpTip"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
                runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
                class="btn btn-primary dropdown-toggle"
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddOpTip_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Label ID="lblOpTipPct" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3" style="width:500px; position: relative">
            <abbr style="font-size:Large;">
                <abbr style="font-size:small; color:red">*</abbr> 
                Shooter:</abbr>
            <asp:DropDownList 
                ID="ddShooter"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
                runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
                class="btn btn-primary dropdown-toggle"
                AutoPostBack="true" 
                OnSelectedIndexChanged="ddShooter_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Label ID="lblShooter" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-md-3" style="width:450px; float:left; margin-left:500px; position: absolute">
            <div class="row" style="float:left">                
                <asp:Label ID="lblOpPGAnalytics" runat="server" Text=""></asp:Label>
            </div>
            <div class="row" style="float:left">                
                <asp:Label ID="lblOpSGAnalytics" runat="server" Text=""></asp:Label>
            </div>
            <div class="row" style="float:left">                
                <asp:Label ID="lblOpSFAnalytics" runat="server" Text=""></asp:Label>
            </div>
            <div class="row" style="float:left">                
                <asp:Label ID="lblOpPFAnalytics" runat="server" Text=""></asp:Label>
            </div>
            <div class="row" style="float:left">                
                <asp:Label ID="lblOpCAnalytics" runat="server" Text=""></asp:Label>
            </div>		
        </div>
    </div>
    <div class="row">
        <div class="col-md-3" style="width:500px">
        <abbr style="font-size:Large;">
            Shot Value:</abbr>
        <asp:DropDownList 
            ID="ddShotValue"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
            runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
            class="btn btn-primary dropdown-toggle"
            AutoPostBack="true" 
            OnSelectedIndexChanged="ddShotValue_SelectedIndexChanged">
            <asp:ListItem Text="" Value=""></asp:ListItem>
            <asp:ListItem Text="FG2" Value="FG2"></asp:ListItem>
            <asp:ListItem Text="FG3" Value="FG3"></asp:ListItem>
            <asp:ListItem Text="FT" Value="FT"></asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="lblShotValue" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-md-3" style="width:450px">
		
        </div>
    </div>
    <div class="row">
        <div class="col-md-3" style="width:500px">
        <abbr style="font-size:Large;">
            Shot Type:</abbr>
        <asp:DropDownList 
            ID="ddShotType"  data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" DataKeyNames="Team"
            runat="server" BackColor="White" BorderColor="Black" Font-Size="Medium" ForeColor="Black"
            class="btn btn-primary dropdown-toggle"
            AutoPostBack="true" 
            OnSelectedIndexChanged="ddShotType_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Label ID="lblShotType" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-md-3" style="width:450px">
		
        </div>
    </div>
    
    <div class="row">        
        <asp:Button ID="btnRetrieve" runat="server" Text ="Get Odds"  OnClick="btnRetrieve_Click" Width="150px"
        style="color:cornflowerblue; background-color:black; border-color:black; text-decoration:none;  text-align:center;border-radius: 15px; border: 3px solid grey; margin-left:20px"/>
        <br />
        <h5>New Equation:</h5>
        <asp:Label ID="lblNewOdds" runat="server" Text=""></asp:Label> 
        <asp:Label ID="lblNewOddsProbability" runat="server" Text=""></asp:Label> 
        <asp:Label ID="lblWarning" runat="server" Text=""></asp:Label>
        <h6>Old Equation:</h6>
        <asp:Label ID="lblOdds" runat="server" Text=""></asp:Label> 
        <asp:Label ID="lblOddsProbability" runat="server" Text=""></asp:Label> 
        <br />
        <asp:Label ID="lblPlayerReasoning" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblTeamReasoning" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblOpponentReasoning" runat="server" Text=""></asp:Label>
    </div>
    
    <hr />
    <div class="row"> 
        <div class="col-md-3" style="width:600px; display:flow">
            <h3>Team Starting Lineup</h3>
            <asp:CheckBoxList ID="chkTeamActive" runat="server" RepeatColumns="3" AutoPostBack="true" OnSelectedIndexChanged="chkTeamActive_SelectedIndexChanged"></asp:CheckBoxList>
        </div>
        <div class="col-md-3" style="width:600px; display:flow">
            <h3>Opponent Starting Lineup</h3>
            <asp:CheckBoxList ID="chkOpponentActive" runat="server" RepeatColumns="3" AutoPostBack="true" OnSelectedIndexChanged="chkOpponentActive_SelectedIndexChanged"></asp:CheckBoxList>
        </div>
    </div>

<%--    
    <div class="row"> 
        <div class="col-md-3" style="width:600px; display:flow">
            <h3>Team DNPs</h3>
            <asp:CheckBoxList ID="chkTeamDNP" runat="server" RepeatColumns="3" AutoPostBack="true" OnSelectedIndexChanged="chkTeamDNP_SelectedIndexChanged"></asp:CheckBoxList>
        </div>
        <div class="col-md-3" style="width:600px; display:flow">
            <h3>Opponent DNPs</h3>
            <asp:CheckBoxList ID="chkOpponentDNP" runat="server" RepeatColumns="3" AutoPostBack="true" OnSelectedIndexChanged="chkOpponentDNP_SelectedIndexChanged"></asp:CheckBoxList>
        </div>
    </div>--%>


    <br />
    <h4>Analytics</h4>
    <div class="row" style=""> 
        <div class="col-md-5" style="width:580px">
            <asp:Label ID="lblGamesActive" runat="server" Text="" Font-Bold="true"></asp:Label> 
            <asp:Label ID="lblActives" runat="server" Text="" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblPlayerStarts" runat="server" Text="" Font-Bold="false" ></asp:Label>
        </div>
        <div class="col-md-5" style="width:580px">
            <asp:Label ID="lblGamesActiveOp" runat="server" Text="" Font-Bold="true"></asp:Label> 
            <asp:Label ID="lblActivesOp" runat="server" Text="" Font-Bold="false" ></asp:Label>
            <asp:Label ID="lblPlayerStartsOp" runat="server" Text="" Font-Bold="false" ></asp:Label>

        </div>
    </div>
    <div class="row">
        <div class="col-md-1" style=" width:fit-content">
            <div class="row">
                <asp:Label ID="lblStarting" runat="server" Text="Starting Lineups" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                
            </div>
            <div class="row">
                <asp:Label ID="hName" runat="server" Text="" Font-Size="X-Large" Font-Bold="true"></asp:Label>
                <div class="col-sm-1" style="width:auto; padding-right:0px; margin-top:-5px">
                    <asp:Label ID="hpg" runat="server" Text="PG: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-md-2" style="width: fit-content; margin-top:-5px">
                    <asp:Label ID="hPGName" runat="server" Text="" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">                
                <div class="col-sm-1" style="width:auto; padding-right:0px; margin-top:-5px">
                    <asp:Label ID="hsg" runat="server" Text="SG: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-md-2" style="width: fit-content; margin-top:-5px">
                    <asp:Label ID="hSGName" runat="server" Text="" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">                
                <div class="col-sm-1" style="width:auto; padding-right:0px; margin-top:-5px">
                    <asp:Label ID="hsf" runat="server" Text="SF: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-md-2" style="width: fit-content; margin-top:-5px">
                    <asp:Label ID="hSFName" runat="server" Text="" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">                
                <div class="col-sm-1" style="width:auto; padding-right:0px; margin-top:-5px">
                    <asp:Label ID="hpf" runat="server" Text="PF: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-md-2" style="width: fit-content; margin-top:-5px">
                    <asp:Label ID="hPFName" runat="server" Text="" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">                
                <div class="col-sm-1" style="width:auto; padding-right:0px; margin-top:-5px">
                    <asp:Label ID="hc" runat="server" Text="C: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-md-2" style="width: fit-content; margin-top:-5px">
                    <asp:Label ID="hCName" runat="server" Text="" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">      
                <div class="col-md-2" style="width: fit-content; margin-top:-5px">
                    <asp:Label ID="time" runat="server" Text="" Font-Size="Small" Font-Bold="false" Font-Italic="true" Visible="false"></asp:Label>
                </div>
            </div>
        </div>    
        <div class="col-md-2">              
            <div class="row">
                <br />
                <br />
            </div>
            <div class="row">
                <asp:Label ID="aName" runat="server" Text="" Font-Size="X-Large" Font-Bold="true"></asp:Label>
                <div class="col-sm-1" style="width:auto; padding-right:0px; margin-top:-5px">
                    <asp:Label ID="apg" runat="server" Text="PG: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-md-2" style="width: fit-content; margin-top:-5px">
                    <asp:Label ID="aPGName" runat="server" Text="" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">                
                <div class="col-sm-1" style="width:auto; padding-right:0px; margin-top:-5px">
                    <asp:Label ID="asg" runat="server" Text="SG: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-md-2" style="width: fit-content; margin-top:-5px">
                    <asp:Label ID="aSGName" runat="server" Text="" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">                
                <div class="col-sm-1" style="width:auto; padding-right:0px; margin-top:-5px">
                    <asp:Label ID="asf" runat="server" Text="SF: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-md-2" style="width: fit-content; margin-top:-5px">
                    <asp:Label ID="aSFName" runat="server" Text="" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">                
                <div class="col-sm-1" style="width:auto; padding-right:0px; margin-top:-5px">
                    <asp:Label ID="apf" runat="server" Text="PF: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-md-2" style="width: fit-content; margin-top:-5px">
                    <asp:Label ID="aPFName" runat="server" Text="" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">                
                <div class="col-sm-1" style="width:auto; padding-right:0px; margin-top:-5px">
                    <asp:Label ID="ac" runat="server" Text="C: " Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <div class="col-md-2" style="width: fit-content; margin-top:-5px">
                    <asp:Label ID="aCName" runat="server" Text="" Font-Size="Medium" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>