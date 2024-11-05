using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;
using static NBAdb.SiteMaster;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using System.Collections;
using System.Web.Routing;
using System.Web.UI.WebControls.WebParts;
using Newtonsoft.Json.Linq;
using System.Data.SqlTypes;

namespace NBAdb
{
    public partial class ParlayToolbox : System.Web.UI.Page
    {
        public static int counter = 0;
        public static BusDriver busDriver = new BusDriver();

        public List<DropDownList> t1Rosters;
        public List<DropDownList> t1DNPRosters;
        public List<DropDownList> t1FullRoster;
        public List<Control> statSections;

        public List<TextBox> p1;
        public List<TextBox> p2;
        public List<TextBox> p3;
        public List<TextBox> p4;
        public List<TextBox> p5;


        public List<TextBox> allT1PlayerProps;
        public List<CheckBox> allT1PlayerPropsUnders;

        public static string T1PlayerProcedure = "ToolboxAverages";
        public static string T1TeamProcedure = "ParlayTeamAverageRanks";





        public List<DropDownList> t2Rosters;
        public List<DropDownList> t2DNPRosters;
        public List<DropDownList> t2FullRoster;
        public List<Control> t2StatSections;


        public List<TextBox> t2p1;
        public List<TextBox> t2p2;
        public List<TextBox> t2p3;
        public List<TextBox> t2p4;
        public List<TextBox> t2p5;

        public List<TextBox> allT2PlayerProps;
        public List<CheckBox> allT2PlayerPropsUnders;

        public static string T2PlayerProcedure = "ToolboxAverages";
        public static string T2TeamProcedure = "ParlayTeamAverageRanks";


        public List<DropDownList> teams;

        protected void Page_Load(object sender, EventArgs e)
        {
            teams = new List<DropDownList>
            {
                ddTeams, ddTeams2
            };


            t1Rosters = new List<DropDownList>
            {
                ddlRoster, ddlRoster2, ddlRoster3, ddlRoster4, ddlRoster5
            };
            t1DNPRosters = new List<DropDownList>
            {
                ddlDNP, ddlDNP2, ddlDNP3, ddlDNP4, ddlDNP5
            };

            t1FullRoster = new List<DropDownList>
            {
               ddlRoster, ddlRoster2, ddlRoster3, ddlRoster4, ddlRoster5, ddlDNP, ddlDNP2, ddlDNP3, ddlDNP4, ddlDNP5
            };
            statSections = new List<Control>
            {
                p1Stats, p2Stats, p3Stats, p4Stats, p5Stats
            };
            p1 = new List<TextBox>
            {
                txtP1Pts, txtP1Ast, txtP1Reb, txtP13, txtP1Blk, txtP1Stl
            };
            p2 = new List<TextBox>
            {
                txtP2Pts, txtP2Ast, txtP2Reb, txtP23, txtP2Blk, txtP2Stl
            };
            p3 = new List<TextBox>
            {
                txtP3Pts, txtP3Ast, txtP3Reb, txtP33, txtP3Blk, txtP3Stl
            };
            p4 = new List<TextBox>
            {
                txtP4Pts, txtP4Ast, txtP4Reb, txtP43, txtP4Blk, txtP4Stl
            };
            p5 = new List<TextBox>
            {
                txtP5Pts, txtP5Ast, txtP5Reb, txtP53, txtP5Blk, txtP5Stl
            };
            allT1PlayerProps = new List<TextBox>
            {
                txtP1Pts, txtP1Ast, txtP1Reb, txtP13, txtP1Blk, txtP1Stl,
                txtP2Pts, txtP2Ast, txtP2Reb, txtP23, txtP2Blk, txtP2Stl,
                txtP3Pts, txtP3Ast, txtP3Reb, txtP33, txtP3Blk, txtP3Stl,
                txtP4Pts, txtP4Ast, txtP4Reb, txtP43, txtP4Blk, txtP4Stl,
                txtP5Pts, txtP5Ast, txtP5Reb, txtP53, txtP5Blk, txtP5Stl

            };

            allT1PlayerPropsUnders = new List<CheckBox>
            {
                chkP1Pts, chkP1Ast, chkP1Reb, chkP13, chkP1Blk, chkP1Stl,
                chkP2Pts, chkP2Ast, chkP2Reb, chkP23, chkP2Blk, chkP2Stl,
                chkP3Pts, chkP3Ast, chkP3Reb, chkP33, chkP3Blk, chkP3Stl,
                chkP4Pts, chkP4Ast, chkP4Reb, chkP43, chkP4Blk, chkP4Stl,
                chkP5Pts, chkP5Ast, chkP5Reb, chkP53, chkP5Blk, chkP5Stl
            };




            t2Rosters = new List<DropDownList>
            {
                ddlT2Roster, ddlT2Roster2, ddlT2Roster3, ddlT2Roster4, ddlT2Roster5
            };
            t2DNPRosters = new List<DropDownList>
            {
                ddlT2DNP, ddlT2DNP2, ddlT2DNP3, ddlT2DNP4, ddlT2DNP5
            };

            t2FullRoster = new List<DropDownList>
            {
                ddlT2Roster, ddlT2Roster2, ddlT2Roster3, ddlT2Roster4, ddlT2Roster5, ddlT2DNP, ddlT2DNP2, ddlT2DNP3, ddlT2DNP4, ddlT2DNP5
            };
            t2StatSections = new List<Control>
            {
                t2p1Stats, t2p2Stats, t2p3Stats, t2p4Stats, t2p5Stats
            };
            t2p1 = new List<TextBox>
            {
                txtT2P1Pts, txtT2P1Ast, txtT2P1Reb, txtT2P13, txtT2P1Blk, txtT2P1Stl
            };
            t2p2 = new List<TextBox>
            {
                txtT2P2Pts, txtT2P2Ast, txtT2P2Reb, txtT2P23, txtT2P2Blk, txtT2P2Stl
            };
            t2p3 = new List<TextBox>
            {
                txtT2P3Pts, txtT2P3Ast, txtT2P3Reb, txtT2P33, txtT2P3Blk, txtT2P3Stl
            };
            t2p4 = new List<TextBox>
            {
                txtT2P4Pts, txtT2P4Ast, txtT2P4Reb, txtT2P43, txtT2P4Blk, txtT2P4Stl
            };
            t2p5 = new List<TextBox>
            {
                txtT2P5Pts, txtT2P5Ast, txtT2P5Reb, txtT2P53, txtT2P5Blk, txtT2P5Stl
            };
            allT2PlayerProps = new List<TextBox>
            {
                txtT2P1Pts, txtT2P1Ast, txtT2P1Reb, txtT2P13, txtT2P1Blk, txtT2P1Stl,
                txtT2P2Pts, txtT2P2Ast, txtT2P2Reb, txtT2P23, txtT2P2Blk, txtT2P2Stl,
                txtT2P3Pts, txtT2P3Ast, txtT2P3Reb, txtT2P33, txtT2P3Blk, txtT2P3Stl,
                txtT2P4Pts, txtT2P4Ast, txtT2P4Reb, txtT2P43, txtT2P4Blk, txtT2P4Stl,
                txtT2P5Pts, txtT2P5Ast, txtT2P5Reb, txtT2P53, txtT2P5Blk, txtT2P5Stl

            };

            allT2PlayerPropsUnders = new List<CheckBox>
            {
                chkT2P1Pts, chkT2P1Ast, chkT2P1Reb, chkT2P13, chkT2P1Blk, chkT2P1Stl,
                chkT2P2Pts, chkT2P2Ast, chkT2P2Reb, chkT2P23, chkT2P2Blk, chkT2P2Stl,
                chkT2P3Pts, chkT2P3Ast, chkT2P3Reb, chkT2P33, chkT2P3Blk, chkT2P3Stl,
                chkT2P4Pts, chkT2P4Ast, chkT2P4Reb, chkT2P43, chkT2P4Blk, chkT2P4Stl,
			    chkT2P5Pts, chkT2P5Ast, chkT2P5Reb, chkT2P53, chkT2P5Blk, chkT2P5Stl

            };




            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }
        protected void BindGrid()
        {
            using (SqlCommand querySearch = new SqlCommand("ParlayTeams"))
            {
                querySearch.CommandType = CommandType.StoredProcedure;
                querySearch.Parameters.AddWithValue("@season", ddSeason.SelectedValue);
                using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                {
                    querySearch.Connection = busDriver.SQLdb;
                    sDataSearch.SelectCommand = querySearch;
                    using (DataSet dataT = new DataSet())
                    {
                        try { sDataSearch.Fill(dataT); } catch (SqlException) { lblError.Text = "No games meet the requested criteria"; }
                        foreach(DropDownList team in teams)
                        {
                            team.DataTextField = dataT.Tables[0].Columns["Team"].ToString();
                            team.DataValueField = dataT.Tables[0].Columns["TeamID"].ToString();
                            team.DataSource = dataT;
                            team.DataBind();
                            ListItem emptyItem = new ListItem("Team", "");
                            team.Items.Insert(0, emptyItem);
                        }
                    }
                }
            }
        }
        protected void PopulateUI(List<DropDownList> roster, List<Control> statSection)
        {
            for(int i = 0;i < roster.Count;i++)
            {
                if (roster[i].SelectedItem.Value.IsNullOrWhiteSpace())
                {
                    statSection[i].Visible = false;
                }
            }
        }
        public static int SeasonChange = 0;

        protected void ddSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblOdds.Text = "";
            playerPicks.Attributes.Add("style", "visibility: hidden");
            gamesPlayed = 0;
            gamesAbove = 0;
            SeasonChange = 1;
            ddTeams_SelectedIndexChanged(sender, e);
            ddTeams2_SelectedIndexChanged(sender, e);
        }

        protected void ddTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblOdds.Text = "";
            playerPicks.Attributes.Add("style", "visibility: hidden");
            gamesPlayed = 0;
            gamesAbove = 0;
            tWins.Visible = true;
            a.Visible = true;
            Label1.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            Label4.Visible = true;
            Label5.Visible = true;
            Label6.Visible = true;
            Label7.Visible = true;
            Label8.Visible = true;
            Label9.Visible = true;
            foreach (TextBox textbox in allT1PlayerProps)
            {
                textbox.Text = "";
            }
            foreach (CheckBox check in allT1PlayerPropsUnders)
            {
                check.Checked = false;
            }


            foreach (DropDownList roster in t1FullRoster)
            {
                string ID = roster.SelectedItem.Value;
                string Name = roster.SelectedItem.Text;
                roster.Items.Clear();
                PopulateRoster(roster, ddTeams.SelectedValue);
                if(SeasonChange == 1)
                {
                    if (!ID.IsNullOrWhiteSpace())
                    {
                        roster.SelectedItem.Value = ID;
                        roster.SelectedItem.Text = Name;
                        if (roster.ID == "ddlRoster")
                        {
                            ddlRoster_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlRoster2")
                        {
                            ddlRoster2_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlRoster3")
                        {
                            ddlRoster3_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlRoster4")
                        {
                            ddlRoster4_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlRoster5")
                        {
                            ddlRoster5_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlDNP")
                        {
                            ddlDNP_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlDNP2")
                        {
                            ddlDNP2_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlDNP3")
                        {
                            ddlDNP3_SelectedIndexChanged(sender, e);
                        }
                    }
                }
            }            
            PopulateUI(t1Rosters, statSections);
            SeasonChange = 0;
            if (!ddTeams.SelectedItem.Text.IsNullOrWhiteSpace())
            {
                t1Name.Text = ddTeams.SelectedItem.Text;
                TeamAverages(Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value));
            }
        }
        protected void TeamAverages(int team, int season)
        {
            using (SqlCommand querySearch = new SqlCommand(T1TeamProcedure))
            {
                querySearch.Connection = busDriver.SQLdb;
                querySearch.CommandType = CommandType.StoredProcedure;
                querySearch.Parameters.AddWithValue("@team", team);
                querySearch.Parameters.AddWithValue("@season", season);
                if (T1TeamProcedure == "ParlayTeamAverageRanksWL")
                {
                    if (chkT1Wins.Checked)
                    {
                        querySearch.Parameters.AddWithValue("@win", 1);
                    }
                    else
                    {
                        querySearch.Parameters.AddWithValue("@win", 0);
                    }
                }
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = querySearch.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        t1Score.Text = sdr["points"].ToString();
                        t1ScoreR.Text = "(" + sdr["pointsRank"].ToString() + ")";
                        t1ScoreAgainst.Text = sdr["pointsAgainst"].ToString();
                        t1ScoreAgainstR.Text = "(" + sdr["pointsAgainstRank"].ToString() + ")";
                        t1FG.Text = sdr["twoPointersMade"].ToString() + "/" + sdr["twoPointersAttempted"].ToString();
                        t1FGR.Text = "(" + sdr["twoPointersMadeRank"].ToString() + ")/("+ sdr["twoPointersAttemptedRank"].ToString() + ")";
                        t1FG3.Text = sdr["threePointersMade"].ToString() + "/" + sdr["threePointersAttempted"].ToString();
                        t1FG3R.Text = "(" + sdr["threePointersMadeRank"].ToString() + ")/(" + sdr["threePointersAttemptedRank"].ToString() + ")";
                        t1Ast.Text = sdr["assists"].ToString();
                        t1AstR.Text = "(" + sdr["assistsRank"].ToString() + ")";
                        t1Reb.Text = sdr["reboundsPersonal"].ToString();
                        t1RebR.Text = "(" + sdr["reboundsPersonalRank"].ToString() + ")";
                        t1Q1.Text = sdr["q1Points"].ToString() + "/" + sdr["q1PointsAgainst"].ToString();
                        t1Q1R.Text = "(" + sdr["q1PointsRank"].ToString() + ")/(" + sdr["q1PointsAgainstRank"].ToString() + ")";
                        t1Q2.Text = sdr["q2Points"].ToString() + "/" + sdr["q2PointsAgainst"].ToString();
                        t1Q2R.Text = "(" + sdr["q2PointsRank"].ToString() + ")/(" + sdr["q2PointsAgainstRank"].ToString() + ")";
                        t1Q3.Text = sdr["q3Points"].ToString() + "/" + sdr["q3PointsAgainst"].ToString();
                        t1Q3R.Text = "(" + sdr["q3PointsRank"].ToString() + ")/(" + sdr["q3PointsAgainstRank"].ToString() + ")";
                        t1Q4.Text = sdr["q4Points"].ToString() + "/" + sdr["q4PointsAgainst"].ToString();
                        t1Q4R.Text = "(" + sdr["q4PointsRank"].ToString() + ")/(" + sdr["q4PointsAgainstRank"].ToString() + ")";


                        string cRank = "";
                        string lRank = "";
                        if(sdr["ConferenceRank"].ToString() == "1")
                        {
                            cRank = "st";
                        }
                        else if(sdr["ConferenceRank"].ToString() == "2")
                        {
                            cRank = "nd";
                        }
                        else if (sdr["ConferenceRank"].ToString() == "3")
                        {
                            cRank = "rd";
                        }
                        else
                        {
                            cRank = "th";
                        }

                        if (sdr["LeagueRank"].ToString() == "1")
                        {
                            lRank = "st";
                        }
                        else if (sdr["LeagueRank"].ToString() == "2")
                        {
                            lRank = "nd";
                        }
                        else if (sdr["LeagueRank"].ToString() == "3")
                        {
                            lRank = "rd";
                        }
                        else
                        {
                            lRank = "th";
                        }



                        tWins.Text = sdr["Wins"] + "-" + sdr["Losses"] + ", " + sdr["ConferenceRank"] + cRank + " in " + sdr["Conference"] + ", " + sdr["LeagueRank"] + lRank + " overall";
                    }
                }
                busDriver.SQLdb.Close();
            }
            
        }


        protected void PopulateRoster(DropDownList Roster, string team)
        {
            using (SqlCommand querySearch = new SqlCommand("ParlayRoster"))
            {
                querySearch.CommandType = CommandType.StoredProcedure;
                querySearch.Parameters.AddWithValue("@team", team);
                querySearch.Parameters.AddWithValue("@season", ddSeason.SelectedValue);
                using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                {
                    querySearch.Connection = busDriver.SQLdb;
                    sDataSearch.SelectCommand = querySearch;
                    using (DataSet dataT = new DataSet())
                    {
                        try { sDataSearch.Fill(dataT); } catch (SqlException) { lblError.Text = "No games meet the requested criteria"; }
                        Roster.DataTextField = dataT.Tables[0].Columns["Player"].ToString();
                        Roster.DataValueField = dataT.Tables[0].Columns["PlayerID"].ToString();
                        Roster.DataSource = dataT;
                        Roster.DataBind();
                        ListItem emptyItem = new ListItem("", "");
                        if (Roster.ID.Contains("DNP"))
                        {
                            emptyItem = new ListItem("DNP Player", "");
                        }
                        else
                        {
                            emptyItem = new ListItem("Player", "");
                        }
                        Roster.Items.Insert(0, emptyItem);
                    }
                }
            }
        }

        protected void ddlRoster_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlRoster.SelectedItem.Text != "Player")
            {
                txtP1Pts.Enabled = true;        chkP1Pts.Enabled = true;
                txtP1Ast.Enabled = true;        chkP1Ast.Enabled = true;
                txtP1Reb.Enabled = true;        chkP1Reb.Enabled = true;
                txtP13.Enabled = true;          chkP13.Enabled = true;
                txtP1Blk.Enabled = true;        chkP1Blk.Enabled = true;
                txtP1Stl.Enabled = true;        chkP1Stl.Enabled = true;
                p1Stats.Visible = true;
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p1Name, p1Games, p1Pts, p1Ast, p1Reb, p1FG, p13, p1ft, p1Blk, p1Stl, p1PtsExt, p1AstExt, p1RebExt, p1FGExt, p13Ext, p1ftExt, p1BlkExt, p1StlExt, p1GamesExt);
            }
            else
            {
                txtP1Pts.Enabled = false;chkP1Pts.Enabled = false;
                txtP1Ast.Enabled = false;chkP1Ast.Enabled = false;
                txtP1Reb.Enabled = false;chkP1Reb.Enabled = false;
                txtP13.Enabled = false;  chkP13.Enabled = false;
                txtP1Blk.Enabled = false;chkP1Blk.Enabled = false;
                txtP1Stl.Enabled = false; chkP1Stl.Enabled = false;
            }
            foreach (DropDownList roster in t1FullRoster)
            {
                if(roster.ID != "ddlRoster")
                {
                    if(roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlRoster.SelectedItem) && ddlRoster.SelectedItem.Text != "Player")
                    {
                        roster.Items.Remove(ddlRoster.SelectedItem);
                    }
                }
            }
            RosterCheck(t1FullRoster);
        }

        protected void ddlRoster2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoster2.SelectedItem.Text != "Player")
            {
                txtP2Pts.Enabled = true; chkP2Pts.Enabled = true;
                txtP2Ast.Enabled = true; chkP2Ast.Enabled = true;
                txtP2Reb.Enabled = true; chkP2Reb.Enabled = true;
                txtP23.Enabled = true; chkP23.Enabled = true;
                txtP2Blk.Enabled = true; chkP2Blk.Enabled = true;
                txtP2Stl.Enabled = true; chkP2Stl.Enabled = true;
                p2Stats.Visible = true;
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster2.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p2Name, p2Games, p2Pts, p2Ast, p2Reb, p2FG, p23, p2ft, p2Blk, p2Stl, p2PtsExt, p2AstExt, p2RebExt, p2FGExt, p23Ext, p2ftExt, p2BlkExt, p2StlExt, p2GamesExt);
            }
            else
            {
                txtP2Pts.Enabled = false; chkP2Pts.Enabled = false;
                txtP2Ast.Enabled = false; chkP2Ast.Enabled = false;
                txtP2Reb.Enabled = false; chkP2Reb.Enabled = false;
                txtP23.Enabled = false; chkP23.Enabled = false;
                txtP2Blk.Enabled = false; chkP2Blk.Enabled = false;
                txtP2Stl.Enabled = false; chkP2Stl.Enabled = false;
            }
            foreach (DropDownList roster in t1FullRoster)
            {
                if (roster.ID != "ddlRoster2")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlRoster2.SelectedItem) && ddlRoster2.SelectedItem.Text != "Player")
                    {
                        roster.Items.Remove(ddlRoster2.SelectedItem);
                    }
                }
            }
            RosterCheck(t1FullRoster);
        }

        protected void ddlRoster3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoster3.SelectedItem.Text != "Player")
            {
                txtP3Pts.Enabled = true; chkP3Pts.Enabled = true;
                txtP3Ast.Enabled = true; chkP3Ast.Enabled = true;
                txtP3Reb.Enabled = true; chkP3Reb.Enabled = true;
                txtP33.Enabled = true; chkP33.Enabled = true;
                txtP3Blk.Enabled = true; chkP3Blk.Enabled = true;
                txtP3Stl.Enabled = true; chkP3Stl.Enabled = true;
                p3Stats.Visible = true;
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster3.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p3Name, p3Games, p3Pts, p3Ast, p3Reb, p3FG, p33, p3ft, p3Blk, p3Stl, p3PtsExt, p3AstExt, p3RebExt, p3FGExt, p33Ext, p3ftExt, p3BlkExt, p3StlExt, p3GamesExt);
            }
            else
            {
                txtP3Pts.Enabled = false; chkP3Pts.Enabled = false;
                txtP3Ast.Enabled = false; chkP3Ast.Enabled = false;
                txtP3Reb.Enabled = false; chkP3Reb.Enabled = false;
                txtP33.Enabled = false; chkP33.Enabled = false;
                txtP3Blk.Enabled = false; chkP3Blk.Enabled = false;
                txtP3Stl.Enabled = false; chkP3Stl.Enabled = false;
            }
            foreach (DropDownList roster in t1FullRoster)
            {
                if (roster.ID != "ddlRoster3")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlRoster3.SelectedItem) && ddlRoster3.SelectedItem.Text != "Player")
                    {
                        roster.Items.Remove(ddlRoster3.SelectedItem);
                    }
                }
            }
            RosterCheck(t1FullRoster);
        }
        protected void ddlRoster4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoster4.SelectedItem.Text != "Player")
            {
                txtP4Pts.Enabled = true; chkP4Pts.Enabled = true;
                txtP4Ast.Enabled = true; chkP4Ast.Enabled = true;
                txtP4Reb.Enabled = true; chkP4Reb.Enabled = true;
                txtP43.Enabled = true; chkP43.Enabled = true;
                txtP4Blk.Enabled = true; chkP4Blk.Enabled = true;
                txtP4Stl.Enabled = true; chkP4Stl.Enabled = true;
                p4Stats.Visible = true;
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster4.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p4Name, p4Games, p4Pts, p4Ast, p4Reb, p4FG, p43, p4ft, p4Blk, p4Stl, p4PtsExt, p4AstExt, p4RebExt, p4FGExt, p43Ext, p4ftExt, p4BlkExt, p4StlExt, p4GamesExt);
            }
            else
            {
                txtP4Pts.Enabled = false; chkP4Pts.Enabled = false;
                txtP4Ast.Enabled = false; chkP4Ast.Enabled = false;
                txtP4Reb.Enabled = false; chkP4Reb.Enabled = false;
                txtP43.Enabled = false; chkP43.Enabled = false;
                txtP4Blk.Enabled = false; chkP4Blk.Enabled = false;
                txtP4Stl.Enabled = false; chkP4Stl.Enabled = false;
            }
            foreach (DropDownList roster in t1FullRoster)
            {
                if (roster.ID != "ddlRoster4")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlRoster4.SelectedItem) && ddlRoster4.SelectedItem.Text != "Player")
                    {
                        roster.Items.Remove(ddlRoster4.SelectedItem);
                    }
                }
            }
            RosterCheck(t1FullRoster);
        }
        protected void ddlRoster5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoster5.SelectedItem.Text != "Player")
            {
                txtP5Pts.Enabled = true; chkP5Pts.Enabled = true;
                txtP5Ast.Enabled = true; chkP5Ast.Enabled = true;
                txtP5Reb.Enabled = true; chkP5Reb.Enabled = true;
                txtP53.Enabled = true; chkP53.Enabled = true;
                txtP5Blk.Enabled = true; chkP5Blk.Enabled = true;
                txtP5Stl.Enabled = true; chkP5Stl.Enabled = true;
                p5Stats.Visible = true;
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster5.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p5Name, p5Games, p5Pts, p5Ast, p5Reb, p5FG, p53, p5ft, p5Blk, p5Stl, p5PtsExt, p5AstExt, p5RebExt, p5FGExt, p53Ext, p5ftExt, p5BlkExt, p5StlExt, p5GamesExt);
            }
            else
            {
                txtP5Pts.Enabled = false; chkP5Pts.Enabled = false;
                txtP5Ast.Enabled = false; chkP5Ast.Enabled = false;
                txtP5Reb.Enabled = false; chkP5Reb.Enabled = false;
                txtP53.Enabled = false; chkP53.Enabled = false;
                txtP5Blk.Enabled = false; chkP5Blk.Enabled = false;
                txtP5Stl.Enabled = false; chkP5Stl.Enabled = false;
            }
            foreach (DropDownList roster in t1FullRoster)
            {
                if (roster.ID != "ddlRoster5")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlRoster5.SelectedItem) && ddlRoster5.SelectedItem.Text != "Player")
                    {
                        roster.Items.Remove(ddlRoster5.SelectedItem);
                    }
                }
            }
            RosterCheck(t1FullRoster);
        }

        public void GetAverages(string procedure, int player, int team, int season, Label name, Label games, Label pts, Label ast, Label reb, Label fg, Label fg3, Label ft, Label blk, Label stl, Label ptsE, Label astE, Label rebE, Label fgE, Label fg3E, Label ftE, Label blkE, Label stlE, Label gamesE)
        {
            using (SqlCommand PlayerSearch = new SqlCommand(procedure))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@player", player);
                PlayerSearch.Parameters.AddWithValue("@team", team);
                PlayerSearch.Parameters.AddWithValue("@season", season);
                if(procedure == "ToolboxAveragesWL")
                {
                    if (chkT1Wins.Checked)
                    {
                        PlayerSearch.Parameters.AddWithValue("@win", 1);
                    }
                    else
                    {
                        PlayerSearch.Parameters.AddWithValue("@win", 0);
                    }
                }
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    while (reader.Read()) 
                    {
                        name.Text = reader["Name"].ToString();
                        games.Text = reader["Games"] + "GP, " + reader["Minutes"] + "min/game";
                        gamesE.Text = reader["Minutes"] + "min";
                        pts.Text = reader["Points"].ToString();
                        
                        ast.Text = reader["Assists"].ToString();
                        reb.Text = reader["Rebounds"].ToString();
                        fg.Text = reader["FG2M"] + "/" + reader["FG2A"] + " - " + reader["FG2%"] + "%";
                        fg3.Text = reader["FG3M"] + "/" + reader["FG3A"] + " - " + reader["FG3%"]+ "%";
                        ft.Text = reader["FTM"] + "/" + reader["FTA"] + " - " + reader["FT%"] + "%";
                        blk.Text = reader["Blocks"].ToString();
                        stl.Text = reader["Steals"].ToString();


                        ptsE.Text = reader["TrendPoints"].ToString();
                        astE.Text = reader["TrendAssists"].ToString();
                        rebE.Text = reader["TrendRebounds"].ToString();
                        fgE.Text = reader["TrendFG2M"] + "/" + reader["TrendFG2A"] + " - " + reader["TrendFG2%"] + "%";
                        fg3E.Text = reader["TrendFG3M"] + "/" + reader["TrendFG3A"] + " - " + reader["TrendFG3%"] + "%";
                        ftE.Text = reader["TrendFTM"] + "/" + reader["TrendFTA"] + " - " + reader["TrendFT%"] + "%";


                        TrendColoring(games, gamesE, float.Parse(reader["diffMinutes"].ToString()), float.Parse(reader["MinDeviation"].ToString()));
                        TrendColoring(pts, ptsE, float.Parse(reader["diffPoints"].ToString()), float.Parse(reader["PtsDeviation"].ToString()));
                        TrendColoring(ast, astE, float.Parse(reader["diffAssists"].ToString()), float.Parse(reader["AstDeviation"].ToString()));
                        TrendColoring(reb, rebE, float.Parse(reader["diffRebounds"].ToString()), float.Parse(reader["RebDeviation"].ToString()));



                        TrendFieldGoalColoring(fg, fgE, float.Parse(reader["diffFG2M"].ToString()), float.Parse(reader["diffFG2A"].ToString()), float.Parse(reader["FG2MDeviation"].ToString()), float.Parse(reader["FG2ADeviation"].ToString()));
                        TrendFieldGoalColoring(fg3, fg3E, float.Parse(reader["diffFG3M"].ToString()), float.Parse(reader["diffFG3A"].ToString()), float.Parse(reader["FG3MDeviation"].ToString()), float.Parse(reader["FG3ADeviation"].ToString()));
                        TrendFieldGoalColoring(ft, ftE, float.Parse(reader["diffFTM"].ToString()), float.Parse(reader["diffFTA"].ToString()), float.Parse(reader["FTMDeviation"].ToString()), float.Parse(reader["FTADeviation"].ToString()));

                        fg3E.Attributes.Add("style", "font-size: 12px; width:fit-content; padding: 0px 0px 0px 5px");
                        fgE.Attributes.Add("style",  "font-size: 12px; width:fit-content; padding: 0px 0px 0px 5px");
                        ftE.Attributes.Add("style",  "font-size: 12px; width:fit-content; padding: 0px 0px 0px 5px");


                    }
                    busDriver.SQLdb.Close();
                }
            }
        }

        public void TrendColoring(Label prop, Label trend, float difference, float deviation)
        {
            if(difference > 0)
            {
                if(difference >= deviation)
                {
                    trend.Attributes.Add("style", "color: green");
                }
                else
                {
                    trend.Attributes.Add("style", "color: yellowgreen");
                }
            }
            else
            {
                difference = Math.Abs(difference);
                if (difference >= deviation)
                {
                    trend.Attributes.Add("style", "color: red");
                }
                else
                {
                    trend.Attributes.Add("style", "color: orange");
                }

            }
        }


        public void TrendFieldGoalColoring(Label prop, Label trend, float madeDiff, float attemptDiff, float madeDev, float attemptDev)
        {
            string m = prop.Text.Split('/')[0];
            string a = prop.Text.Split('/')[1].Split(' ')[0];
            string p = prop.Text.Split(' ')[2].Split('%')[0];




            float made =      float.Parse(prop.Text.Split('/')[0]);
            float attempted = float.Parse(prop.Text.Split('/')[1].Split(' ')[0]);
            float pct =       float.Parse(prop.Text.Split(' ')[2].Split('%')[0]);


            float madeT =      float.Parse(trend.Text.Split('/')[0]);
            float attemptedT = float.Parse(trend.Text.Split('/')[1].Split(' ')[0]); 
            float pctT =       float.Parse(trend.Text.Split(' ')[2].Split('%')[0]);


            if (madeDiff > 0)
            {
                if (madeDiff >= madeDev)
                {
                    trend.Text = "<span style=\"color:green\">" + madeT + "</span>" + "/";
                }
                else
                {
                    trend.Text = "<span style=\"color:yellowgreen\">" + madeT + "</span>" + "/";
                }
            }
            else
            {
                madeDiff = Math.Abs(madeDiff);
                if (madeDiff >= madeDev)
                {
                    trend.Text = "<span style=\"color:red\">" + madeT + "</span>" + "/";
                }
                else
                {
                    trend.Text = "<span style=\"color:orange\">" + madeT + "</span>" + "/";
                }
            }


            if (attemptDiff > 0)
            {
                if (attemptDiff >= attemptDev)
                {
                    trend.Text += "<span style=\"color:green\">" + attemptedT + "</span>" + " - ";
                }
                else
                {
                    trend.Text += "<span style=\"color:yellowgreen\">" + attemptedT + "</span>" + " - ";
                }
            }
            else
            {
                attemptDiff = Math.Abs(attemptDiff);
                if (attemptDiff >= attemptDev)
                {
                    trend.Text += "<span style=\"color:red\">" + attemptedT + "</span>" + " - ";
                }
                else
                {
                    trend.Text += "<span style=\"color:orange\">" + attemptedT + "</span>" + " - ";
                }
            }

            if (pctT < pct)
            {
                trend.Text += "<span style=\"color:red\">" + pctT + "</span>" + "%";
            }
            else
            {
                trend.Text += "<span style=\"color:green\">" + pctT + "</span>" + "%";
            }
        }




        protected void chkT1Wins_CheckedChanged(object sender, EventArgs e)
        {
            if (chkT1Wins.Checked)
            {
                chkT1Losses.Checked = false;
                T1PlayerProcedure = "ToolboxAveragesWL";
                T1TeamProcedure = "ParlayTeamAverageRanksWL";
            }
            if(!chkT1Wins.Checked && !chkT1Losses.Checked)
            {
                T1TeamProcedure = "ParlayTeamAverageRanks";
                T1PlayerProcedure = "ToolboxAverages";
            }
            TeamAverages(Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value));
            if (!ddlRoster.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p1Name, p1Games, p1Pts, p1Ast, p1Reb, p1FG, p13, p1ft, p1Blk, p1Stl, p1PtsExt, p1AstExt, p1RebExt, p1FGExt, p13Ext, p1ftExt, p1BlkExt, p1StlExt, p1GamesExt);
            }
            if (!ddlRoster2.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster2.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p2Name, p2Games, p2Pts, p2Ast, p2Reb, p2FG, p23, p2ft, p2Blk, p2Stl, p2PtsExt, p2AstExt, p2RebExt, p2FGExt, p23Ext, p2ftExt, p2BlkExt, p2StlExt, p2GamesExt);
            }
            if (!ddlRoster3.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster3.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p3Name, p3Games, p3Pts, p3Ast, p3Reb, p3FG, p33, p3ft, p3Blk, p3Stl, p3PtsExt, p3AstExt, p3RebExt, p3FGExt, p33Ext, p3ftExt, p3BlkExt, p3StlExt, p3GamesExt);
            }
            if (!ddlRoster4.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster4.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p4Name, p4Games, p4Pts, p4Ast, p4Reb, p4FG, p43, p4ft, p4Blk, p4Stl, p4PtsExt, p4AstExt, p4RebExt, p4FGExt, p43Ext, p4ftExt, p4BlkExt, p4StlExt, p4GamesExt);
            }
            if (!ddlRoster5.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster5.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p5Name, p5Games, p5Pts, p5Ast, p5Reb, p5FG, p53, p5ft, p5Blk, p5Stl, p5PtsExt, p5AstExt, p5RebExt, p5FGExt, p53Ext, p5ftExt, p5BlkExt, p5StlExt, p5GamesExt);
            }




        }

        protected void chkT1Losses_CheckedChanged(object sender, EventArgs e)
        {
            if (chkT1Losses.Checked)
            {
                chkT1Wins.Checked = false;
                T1PlayerProcedure = "ToolboxAveragesWL";
                T1TeamProcedure = "ParlayTeamAverageRanksWL";
            }
            if (!chkT1Wins.Checked && !chkT1Losses.Checked)
            {
                T1TeamProcedure = "ParlayTeamAverageRanks";
                T1PlayerProcedure = "ToolboxAverages";
            }
            TeamAverages(Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value));
            if (!ddlRoster.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p1Name, p1Games, p1Pts, p1Ast, p1Reb, p1FG, p13, p1ft, p1Blk, p1Stl, p1PtsExt, p1AstExt, p1RebExt, p1FGExt, p13Ext, p1ftExt, p1BlkExt, p1StlExt, p1GamesExt);
            }
            if (!ddlRoster2.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster2.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p2Name, p2Games, p2Pts, p2Ast, p2Reb, p2FG, p23, p2ft, p2Blk, p2Stl, p2PtsExt, p2AstExt, p2RebExt, p2FGExt, p23Ext, p2ftExt, p2BlkExt, p2StlExt, p2GamesExt);
            }
            if (!ddlRoster3.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster3.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p3Name, p3Games, p3Pts, p3Ast, p3Reb, p3FG, p33, p3ft, p3Blk, p3Stl, p3PtsExt, p3AstExt, p3RebExt, p3FGExt, p33Ext, p3ftExt, p3BlkExt, p3StlExt, p3GamesExt);
            }
            if (!ddlRoster4.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster4.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p4Name, p4Games, p4Pts, p4Ast, p4Reb, p4FG, p43, p4ft, p4Blk, p4Stl, p4PtsExt, p4AstExt, p4RebExt, p4FGExt, p43Ext, p4ftExt, p4BlkExt, p4StlExt, p4GamesExt);
            }
            if (!ddlRoster5.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster5.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p5Name, p5Games, p5Pts, p5Ast, p5Reb, p5FG, p53, p5ft, p5Blk, p5Stl, p5PtsExt, p5AstExt, p5RebExt, p5FGExt, p53Ext, p5ftExt, p5BlkExt, p5StlExt, p5GamesExt);
            }
        }

        protected void ddlDNP_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DropDownList roster in t1FullRoster)
            {
                if (roster.ID != "ddlDNP")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlDNP.SelectedItem))
                    {
                        roster.Items.Remove(ddlDNP.SelectedItem);
                    }
                }
            }
            RosterCheck(t1FullRoster);
        }
        protected void ddlDNP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DropDownList roster in t1FullRoster)
            {
                if (roster.ID != "ddlDNP2")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlDNP2.SelectedItem))
                    {
                        roster.Items.Remove(ddlDNP2.SelectedItem);
                    }
                }
            }
            RosterCheck(t1FullRoster);
        }

        protected void ddlDNP3_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DropDownList roster in t1FullRoster)
            {
                if (roster.ID != "ddlDNP3")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlDNP3.SelectedItem))
                    {
                        roster.Items.Remove(ddlDNP3.SelectedItem);
                    }
                }
            }
            RosterCheck(t1FullRoster);
        }

        protected void chkT1Dynamic_CheckedChanged(object sender, EventArgs e)
        {
            if(chkT1Dynamic.Checked)
            {
                int players = 0;
                int DNPs = 0;
                foreach(DropDownList roster in t1Rosters)
                {
                    if (!roster.SelectedItem.Value.IsNullOrWhiteSpace())
                    {
                        players++;
                    }
                }
                foreach (DropDownList roster in t1DNPRosters)
                {
                    if (!roster.SelectedItem.Value.IsNullOrWhiteSpace())
                    {
                        DNPs++;
                    }
                }
                QueryBuilder(players, DNPs);
            }
            else
            {
                ddTeams_SelectedIndexChanged(sender, e);
            }
        }

        public void QueryBuilder(int players, int DNPs)
        {
            string SelectFrom = "";
            string Where = "where pb.season = " + ddSeason.SelectedItem.Value + " and pb.status = 'ACTIVE' and pb.minutesCalculated != 'PT00M'";
            string GroupBy = "";

            if (chkT1Wins.Checked)
            {
                SelectFrom = "SELECT pb.season_id, pb.team_id, pb.player_id, CASE WHEN tb.points > tb.pointsAgainst THEN 1 ELSE 0 END AS Win, COUNT(pb.game_id) AS Games, ROUND(AVG(CAST(pb.points AS float)), 2) AS Points, ROUND(AVG(CAST(pb.assists AS float)), 2) AS Assists, ROUND(AVG(CAST(pb.reboundsTotal AS float)), 2) AS Rebounds, ROUND(AVG(CAST(pb.blocks AS float)), 2) AS Blocks, ROUND(AVG(CAST(pb.steals AS float)), 2) AS Steals, ROUND(AVG(CAST(pb.fieldGoalsMade AS float)), 2) AS FGM, ROUND(AVG(CAST(pb.fieldGoalsAttempted AS float)), 2) AS FGA, ROUND(AVG(pb.fieldGoalsPercentage) * 100, 2) AS [FG%], ROUND(AVG(CAST(pb.threePointersMade AS float)), 2) AS FG3M, ROUND(AVG(CAST(pb.threePointersAttempted AS float)), 2) AS FG3A, ROUND(AVG(pb.threePointersPercentage) * 100, 2) AS [FG3%], AVG(cast(SUBSTRING(pb.minutesCalculated, 3, 2) as int)) Minutes, ROUND(AVG(CAST(pb.twoPointersMade AS float)), 2) AS FG2M, ROUND(AVG(CAST(pb.twoPointersAttempted AS float)), 2) AS FG2A, ROUND(AVG(pb.twoPointersPercentage) * 100, 2) AS [FG2%], ROUND(AVG(CAST(pb.freeThrowsMade AS float)), 2) AS FTM, ROUND(AVG(CAST(pb.freeThrowsAttempted AS float)), 2) AS FTA, ROUND(AVG(pb.freeThrowsPercentage) * 100, 2) AS [FT%] FROM playerBox pb inner join teamBox tb on pb.game_id = tb.game_id and pb.team_id = tb.team_id and pb.season_id = tb.season_id\r\n";
                Where = Where + " and CASE WHEN tb.points > tb.pointsAgainst THEN 1 ELSE 0 END = 1";
                GroupBy = "group by pb.season_id, pb.team_id, pb.player_id, CASE WHEN tb.points > tb.pointsAgainst THEN 1 ELSE 0 END";
            }
            else if (chkT1Losses.Checked)
            {
                SelectFrom = "SELECT pb.season_id, pb.team_id, pb.player_id, CASE WHEN tb.points > tb.pointsAgainst THEN 1 ELSE 0 END AS Win, COUNT(pb.game_id) AS Games, ROUND(AVG(CAST(pb.points AS float)), 2) AS Points, ROUND(AVG(CAST(pb.assists AS float)), 2) AS Assists, ROUND(AVG(CAST(pb.reboundsTotal AS float)), 2) AS Rebounds, ROUND(AVG(CAST(pb.blocks AS float)), 2) AS Blocks, ROUND(AVG(CAST(pb.steals AS float)), 2) AS Steals, ROUND(AVG(CAST(pb.fieldGoalsMade AS float)), 2) AS FGM, ROUND(AVG(CAST(pb.fieldGoalsAttempted AS float)), 2) AS FGA, ROUND(AVG(pb.fieldGoalsPercentage) * 100, 2) AS [FG%], ROUND(AVG(CAST(pb.threePointersMade AS float)), 2) AS FG3M, ROUND(AVG(CAST(pb.threePointersAttempted AS float)), 2) AS FG3A, ROUND(AVG(pb.threePointersPercentage) * 100, 2) AS [FG3%], AVG(cast(SUBSTRING(pb.minutesCalculated, 3, 2) as int)) Minutes, ROUND(AVG(CAST(pb.twoPointersMade AS float)), 2) AS FG2M, ROUND(AVG(CAST(pb.twoPointersAttempted AS float)), 2) AS FG2A, ROUND(AVG(pb.twoPointersPercentage) * 100, 2) AS [FG2%], ROUND(AVG(CAST(pb.freeThrowsMade AS float)), 2) AS FTM, ROUND(AVG(CAST(pb.freeThrowsAttempted AS float)), 2) AS FTA, ROUND(AVG(pb.freeThrowsPercentage) * 100, 2) AS [FT%] FROM playerBox pb inner join teamBox tb on pb.game_id = tb.game_id and pb.team_id = tb.team_id and pb.season_id = tb.season_id\r\n";
                Where = Where + " and CASE WHEN tb.points > tb.pointsAgainst THEN 1 ELSE 0 END = 0";
                GroupBy = "group by pb.season_id, pb.team_id, pb.player_id, CASE WHEN tb.points > tb.pointsAgainst THEN 1 ELSE 0 END";
            }
            else
            {
                SelectFrom = "SELECT pb.season_id, pb.team_id, pb.player_id, COUNT(pb.game_id) AS Games, ROUND(AVG(CAST(pb.points AS float)), 2) AS Points, ROUND(AVG(CAST(pb.assists AS float)), 2) AS Assists, ROUND(AVG(CAST(pb.reboundsTotal AS float)), 2) AS Rebounds, ROUND(AVG(CAST(pb.blocks AS float)), 2) AS Blocks, ROUND(AVG(CAST(pb.steals AS float)), 2) AS Steals, ROUND(AVG(CAST(pb.fieldGoalsMade AS float)), 2) AS FGM, ROUND(AVG(CAST(pb.fieldGoalsAttempted AS float)), 2) AS FGA, ROUND(AVG(pb.fieldGoalsPercentage) * 100, 2) AS [FG%], ROUND(AVG(CAST(pb.threePointersMade AS float)), 2) AS FG3M, ROUND(AVG(CAST(pb.threePointersAttempted AS float)), 2) AS FG3A, ROUND(AVG(pb.threePointersPercentage) * 100, 2) AS [FG3%], AVG(cast(SUBSTRING(pb.minutesCalculated, 3, 2) as int)) Minutes, ROUND(AVG(CAST(pb.twoPointersMade AS float)), 2) AS FG2M, ROUND(AVG(CAST(pb.twoPointersAttempted AS float)), 2) AS FG2A, ROUND(AVG(pb.twoPointersPercentage) * 100, 2) AS [FG2%], ROUND(AVG(CAST(pb.freeThrowsMade AS float)), 2) AS FTM, ROUND(AVG(CAST(pb.freeThrowsAttempted AS float)), 2) AS FTA, ROUND(AVG(pb.freeThrowsPercentage) * 100, 2) AS [FT%] FROM playerBox pb";
                
                GroupBy = "group by pb.season_id, pb.team_id, pb.player_id";
            }


        }

        public static int gamesPlayed = 0;
        public static int gamesAbove = 0;

        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            playerPicks.Attributes.Add("style", "visibility: visible; width:fit-content; line-height:15px;");
            int players = 0;
            int DNPs = 0;
            List<DropDownList> t1Players = new List<DropDownList>();
            List<DropDownList> t1DNPs = new List<DropDownList>();
            List<TextBox> p1Props = new List<TextBox>();
            List<TextBox> p2Props = new List<TextBox>();
            List<TextBox> p3Props = new List<TextBox>();
            List<TextBox> p4Props = new List<TextBox>();
            List<TextBox> p5Props = new List<TextBox>();

            int player1 = 0;
            int player2 = 0;
            int player3 = 0;
            int player4 = 0;
            int player5 = 0;

            int dnp1 = 0;
            int dnp2 = 0;
            int dnp3 = 0;

            string select = "select count(distinct p.game_id) Games ";
            string from = "from playerBox p";
            string where = "where p.season_id = " + ddSeason.SelectedItem.Value + " and p.team_id = " + ddTeams.SelectedItem.Value;
            string whereAbove = "where p.season_id = " + ddSeason.SelectedItem.Value + " and p.team_id = " + ddTeams.SelectedItem.Value;


            foreach (DropDownList roster in t1Rosters)
            {
                if (!roster.SelectedItem.Value.IsNullOrWhiteSpace())
                {
                    players++;
                    t1Players.Add(roster);
                }
            }
            foreach (DropDownList roster in t1DNPRosters)
            {
                if (!roster.SelectedItem.Value.IsNullOrWhiteSpace())
                {
                    DNPs++;
                    t1DNPs.Add(roster);
                }
            }


            for (int i = 0; i < players; i++)
            {
                if(i == 0)
                {
                    foreach (TextBox prop in p1)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            p1Props.Add(prop);
                        }
                    }
                    player1 = Int32.Parse(t1Players[0].SelectedItem.Value);
                    where += " and p.status = 'ACTIVE' and p.minutesCalculated != 'PT00M' and p.player_id = " + player1;
                    p1Picks.Text = ddlRoster.SelectedItem.Text + ": ";
                }
                else if(i == 1)
                {
                    foreach (TextBox prop in p2)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            p2Props.Add(prop);
                        }
                    }
                    player2 = Int32.Parse(t1Players[1].SelectedItem.Value);
                    from += " inner join playerBox p2 on p.game_id = p2.game_id and p.team_id = p2.team_id and p.season_id = p2.season_id";
                    where += " and p2.status = 'ACTIVE' and p2.minutesCalculated != 'PT00M' and p2.player_id = " + player2;
                    p2Picks.Text = ddlRoster2.SelectedItem.Text + ": ";
                }
                else if( i == 2)
                {
                    foreach (TextBox prop in p3)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            p3Props.Add(prop);
                        }
                    }
                    player3 = Int32.Parse(t1Players[2].SelectedItem.Value);
                    from += " inner join playerBox p3 on p.game_id = p3.game_id and p.team_id = p3.team_id and p.season_id = p3.season_id";
                    where += " and p3.status = 'ACTIVE' and p3.minutesCalculated != 'PT00M' and p3.player_id = " + player3;
                    p3Picks.Text = ddlRoster3.SelectedItem.Text + ": ";
                }
                else if(i == 3)
                {
                    foreach (TextBox prop in p4)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            p4Props.Add(prop);
                        }
                    }
                    player4 = Int32.Parse(t1Players[3].SelectedItem.Value);
                    from += " inner join playerBox p4 on p.game_id = p4.game_id and p.team_id = p4.team_id and p.season_id = p4.season_id";
                    where += " and p4.status = 'ACTIVE' and p4.minutesCalculated != 'PT00M' and p4.player_id = " + player4;
                    p4Picks.Text = ddlRoster4.SelectedItem.Text + ": ";
                }
                else if (i == 4)
                {
                    foreach (TextBox prop in p5)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            p5Props.Add(prop);
                        }
                    }
                    player5 = Int32.Parse(t1Players[4].SelectedItem.Value);
                    from += " inner join playerBox p5 on p.game_id = p5.game_id and p.team_id = p5.team_id and p.season_id = p5.season_id";
                    where += " and p5.status = 'ACTIVE' and p5.minutesCalculated != 'PT00M' and p5.player_id = " + player5;
                    p5Picks.Text = ddlRoster5.SelectedItem.Text + ": ";
                }
            }
            for (int i = 0; i < DNPs; i++)
            {
                if (i == 0)
                {
                    dnp1 = Int32.Parse(t1DNPs[0].SelectedItem.Value);
                    from += " inner join playerBox d on p.game_id = d.game_id and p.team_id = d.team_id and p.season_id = d.season_id";
                    where += " and (d.status != 'ACTIVE' or d.minutesCalculated = 'PT00M') and d.player_id = " + dnp1;
                    DNP1.Text = "DNP: " + t1DNPs[0].SelectedItem.Text;
                }
                else if (i == 1)
                {
                    dnp2 = Int32.Parse(t1DNPs[1].SelectedItem.Value);
                    from += " inner join playerBox d2 on p.game_id = d2.game_id and p.team_id = d2.team_id and p.season_id = d2.season_id";
                    where += " and (d2.status != 'ACTIVE' or d2.minutesCalculated = 'PT00M') and d2.player_id = " + dnp2;
                    DNP2.Text = "DNP: " + t1DNPs[1].SelectedItem.Text;
                }
                else if (i == 2)
                {
                    dnp3 = Int32.Parse(t1DNPs[2].SelectedItem.Value);
                    from += " inner join playerBox d3 on p.game_id = d3.game_id and p.team_id = d3.team_id and p.season_id = d3.season_id";
                    where += " and (d3.status != 'ACTIVE' or d3.minutesCalculated = 'PT00M') and d3.player_id = " + dnp3;
                    DNP3.Text = "DNP: " + t1DNPs[2].SelectedItem.Text;
                }
            }
            string query = select + from + " " + where;
            string queryAbove = query;

            if (!txtP1Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkP1Pts.Checked)
                {
                    p1Picks.Text += "Under " + txtP1Pts.Text + "pts | ";
                    queryAbove += " and p.points <= " + txtP1Pts.Text;
                }
                else
                {
                    p1Picks.Text += txtP1Pts.Text + "pts | ";
                    queryAbove += " and p.points >= " + txtP1Pts.Text;
                }
            }
            if (!txtP1Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkP1Ast.Checked)
                {
                    p1Picks.Text += "Under " + txtP1Ast.Text + "ast | ";
                    queryAbove += " and p.assists <= " + txtP1Ast.Text;
                }
                else
                {
                    p1Picks.Text += txtP1Ast.Text + "ast | ";
                    queryAbove += " and p.assists >= " + txtP1Ast.Text;
                }
            }
            if (!txtP1Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkP1Reb.Checked)
                {
                    p1Picks.Text += "Under " + txtP1Reb.Text + "reb | ";
                    queryAbove += " and p.reboundsTotal <= " + txtP1Reb.Text;
                }
                else
                {
                    p1Picks.Text += txtP1Reb.Text + "reb | ";
                    queryAbove += " and p.reboundsTotal >= " + txtP1Reb.Text;
                }
            }
            if (!txtP13.Text.IsNullOrWhiteSpace())
            {
                if (chkP13.Checked)
                {
                    p1Picks.Text += "Under " + txtP13.Text + " 3PM | ";
                    queryAbove += " and p.threePointersMade <= " + txtP13.Text;
                }
                else
                {
                    p1Picks.Text += txtP13.Text + " 3PM | ";
                    queryAbove += " and p.threePointersMade >= " + txtP13.Text;
                }
            }
            if (!txtP1Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkP1Blk.Checked)
                {
                    p1Picks.Text += "Under " + txtP1Blk.Text + "blk | ";
                    queryAbove += " and p.blocks <= " + txtP1Blk.Text;
                }
                else
                {
                    p1Picks.Text += txtP1Blk.Text + "blk | ";
                    queryAbove += " and p.blocks >= " + txtP1Blk.Text;
                }
            }
            if (!txtP1Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkP1Stl.Checked)
                {
                    p1Picks.Text += "Under " + txtP1Stl.Text + "stl | ";
                    queryAbove += " and p.steals <= " + txtP1Stl.Text;
                }
                else
                {
                    p1Picks.Text += txtP1Stl.Text + "stl | ";
                    queryAbove += " and p.steals >= " + txtP1Stl.Text;
                }
            }




            if (!txtP2Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkP2Pts.Checked)
                {
                    p2Picks.Text += "Under " + txtP2Pts.Text + "pts | ";
                    queryAbove += " and p2.points <= " + txtP2Pts.Text;
                }
                else
                {
                    p2Picks.Text += txtP2Pts.Text + "pts | ";
                    queryAbove += " and p2.points >= " + txtP2Pts.Text;
                }
            }
            if (!txtP2Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkP2Ast.Checked)
                {
                    p2Picks.Text += "Under " + txtP2Ast.Text + "ast | ";
                    queryAbove += " and p2.assists <= " + txtP2Ast.Text;
                }
                else
                {
                    p2Picks.Text += txtP2Ast.Text + "ast | ";
                    queryAbove += " and p2.assists >= " + txtP2Ast.Text;
                }
            }
            if (!txtP2Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkP2Reb.Checked)
                {
                    p2Picks.Text += "Under " + txtP2Reb.Text + "reb | ";
                    queryAbove += " and p2.reboundsTotal <= " + txtP2Reb.Text;
                }
                else
                {
                    p2Picks.Text += txtP2Reb.Text + "reb | ";
                    queryAbove += " and p2.reboundsTotal >= " + txtP2Reb.Text;
                }
            }
            if (!txtP23.Text.IsNullOrWhiteSpace())
            {
                if (chkP23.Checked)
                {
                    p2Picks.Text += "Under " + txtP23.Text + " 3PM | ";
                    queryAbove += " and p2.threePointersMade <= " + txtP23.Text;
                }
                else
                {
                    p2Picks.Text += txtP23.Text + " 3PM | ";
                    queryAbove += " and p2.threePointersMade >= " + txtP23.Text;
                }
            }
            if (!txtP2Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkP2Blk.Checked)
                {
                    p2Picks.Text += "Under " + txtP2Blk.Text + "blk | ";
                    queryAbove += " and p2.blocks <= " + txtP2Blk.Text;
                }
                else
                {
                    p2Picks.Text += txtP2Blk.Text + "blk | ";
                    queryAbove += " and p2.blocks >= " + txtP2Blk.Text;
                }
            }
            if (!txtP2Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkP2Stl.Checked)
                {
                    p2Picks.Text += "Under " + txtP2Stl.Text + "stl | ";
                    queryAbove += " and p2.steals <= " + txtP2Stl.Text;
                }
                else
                {
                    p2Picks.Text += txtP2Stl.Text + "stl | ";
                    queryAbove += " and p2.steals >= " + txtP2Stl.Text;
                }
            }





            if (!txtP3Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkP3Pts.Checked)
                {
                    p3Picks.Text += "Under " + txtP3Pts.Text + "pts | ";
                    queryAbove += " and p3.points <= " + txtP3Pts.Text;
                }
                else
                {
                    p3Picks.Text += txtP3Pts.Text + "pts | ";
                    queryAbove += " and p3.points >= " + txtP3Pts.Text;
                }
            }
            if (!txtP3Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkP3Ast.Checked)
                {
                    p3Picks.Text += "Under " + txtP3Ast.Text + "ast | ";
                    queryAbove += " and p3.assists <= " + txtP3Ast.Text;
                }
                else
                {
                    p3Picks.Text += txtP3Ast.Text + "ast | ";
                    queryAbove += " and p3.assists >= " + txtP3Ast.Text;
                }
            }
            if (!txtP3Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkP3Reb.Checked)
                {
                    p3Picks.Text += "Under " + txtP3Reb.Text + "reb | ";
                    queryAbove += " and p3.reboundsTotal <= " + txtP3Reb.Text;
                }
                else
                {
                    p3Picks.Text += txtP3Reb.Text + "reb | ";
                    queryAbove += " and p3.reboundsTotal >= " + txtP3Reb.Text;
                }
            }
            if (!txtP33.Text.IsNullOrWhiteSpace())
            {
                if (chkP33.Checked)
                {
                    p3Picks.Text += "Under " + txtP33.Text + " 3PM | ";
                    queryAbove += " and p3.threePointersMade <= " + txtP33.Text;
                }
                else
                {
                    p3Picks.Text += txtP33.Text + " 3PM | ";
                    queryAbove += " and p3.threePointersMade >= " + txtP33.Text;
                }
            }
            if (!txtP3Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkP3Blk.Checked)
                {
                    p3Picks.Text += "Under " + txtP3Blk.Text + "blk | ";
                    queryAbove += " and p3.blocks <= " + txtP3Blk.Text;
                }
                else
                {
                    p3Picks.Text += txtP3Blk.Text + "blk | ";
                    queryAbove += " and p3.blocks >= " + txtP3Blk.Text;
                }
            }
            if (!txtP3Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkP3Stl.Checked)
                {
                    p3Picks.Text += "Under " + txtP3Stl.Text + "stl | ";
                    queryAbove += " and p3.steals <= " + txtP3Stl.Text;
                }
                else
                {
                    p3Picks.Text += txtP3Stl.Text + "stl | ";
                    queryAbove += " and p3.steals >= " + txtP3Stl.Text;
                }
            }




            if (!txtP4Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkP4Pts.Checked)
                {
                    p4Picks.Text += "Under " + txtP4Pts.Text + "pts | ";
                    queryAbove += " and p4.points <= " + txtP4Pts.Text;
                }
                else
                {
                    p4Picks.Text += txtP4Pts.Text + "pts | ";
                    queryAbove += " and p4.points >= " + txtP4Pts.Text;
                }
            }
            if (!txtP4Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkP4Ast.Checked)
                {
                    p4Picks.Text += "Under " + txtP4Ast.Text + "ast | ";
                    queryAbove += " and p4.assists <= " + txtP4Ast.Text;
                }
                else
                {
                    p4Picks.Text += txtP4Ast.Text + "ast | ";
                    queryAbove += " and p4.assists >= " + txtP4Ast.Text;
                }
            }
            if (!txtP4Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkP4Reb.Checked)
                {
                    p4Picks.Text += "Under " + txtP4Reb.Text + "reb | ";
                    queryAbove += " and p4.reboundsTotal <= " + txtP4Reb.Text;
                }
                else
                {
                    p4Picks.Text += txtP4Reb.Text + "reb | ";
                    queryAbove += " and p4.reboundsTotal >= " + txtP4Reb.Text;
                }
            }
            if (!txtP43.Text.IsNullOrWhiteSpace())
            {
                if (chkP43.Checked)
                {
                    p4Picks.Text += "Under " + txtP43.Text + " 3PM | ";
                    queryAbove += " and p4.threePointersMade <= " + txtP43.Text;
                }
                else
                {
                    p4Picks.Text += txtP43.Text + " 3PM | ";
                    queryAbove += " and p4.threePointersMade >= " + txtP43.Text;
                }
            }
            if (!txtP4Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkP4Blk.Checked)
                {
                    p4Picks.Text += "Under " + txtP4Blk.Text + "blk | ";
                    queryAbove += " and p4.blocks <= " + txtP4Blk.Text;
                }
                else
                {
                    p4Picks.Text += txtP4Blk.Text + "blk | ";
                    queryAbove += " and p4.blocks >= " + txtP4Blk.Text;
                }
            }
            if (!txtP4Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkP4Stl.Checked)
                {
                    p4Picks.Text += "Under " + txtP4Stl.Text + "stl | ";
                    queryAbove += " and p4.steals <= " + txtP4Stl.Text;
                }
                else
                {
                    p4Picks.Text += txtP4Stl.Text + "stl | ";
                    queryAbove += " and p4.steals >= " + txtP4Stl.Text;
                }
            }

            if (!txtP5Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkP5Pts.Checked)
                {
                    p5Picks.Text += "Under " + txtP5Pts.Text + "pts | ";
                    queryAbove += " and p5.points <= " + txtP5Pts.Text;
                }
                else
                {
                    p5Picks.Text += txtP5Pts.Text + "pts | ";
                    queryAbove += " and p5.points >= " + txtP5Pts.Text;
                }
            }
            if (!txtP5Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkP5Ast.Checked)
                {
                    p5Picks.Text += "Under " + txtP5Ast.Text + "ast | ";
                    queryAbove += " and p5.assists <= " + txtP5Ast.Text;
                }
                else
                {
                    p5Picks.Text += txtP5Ast.Text + "ast | ";
                    queryAbove += " and p5.assists >= " + txtP5Ast.Text;
                }
            }
            if (!txtP5Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkP5Reb.Checked)
                {
                    p5Picks.Text += "Under " + txtP5Reb.Text + "reb | ";
                    queryAbove += " and p5.reboundsTotal <= " + txtP5Reb.Text;
                }
                else
                {
                    p5Picks.Text += txtP5Reb.Text + "reb | ";
                    queryAbove += " and p5.reboundsTotal >= " + txtP5Reb.Text;
                }
            }
            if (!txtP53.Text.IsNullOrWhiteSpace())
            {
                if (chkP53.Checked)
                {
                    p5Picks.Text += "Under " + txtP53.Text + " 3PM | ";
                    queryAbove += " and p5.threePointersMade <= " + txtP53.Text;
                }
                else
                {
                    p5Picks.Text += txtP53.Text + " 3PM | ";
                    queryAbove += " and p5.threePointersMade >= " + txtP53.Text;
                }
            }
            if (!txtP5Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkP5Blk.Checked)
                {
                    p5Picks.Text += "Under " + txtP5Blk.Text + "blk | ";
                    queryAbove += " and p5.blocks <= " + txtP5Blk.Text;
                }
                else
                {
                    p5Picks.Text += txtP5Blk.Text + "blk | ";
                    queryAbove += " and p5.blocks >= " + txtP5Blk.Text;
                }
            }
            if (!txtP5Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkP5Stl.Checked)
                {
                    p5Picks.Text += "Under " + txtP5Stl.Text + "stl | ";
                    queryAbove += " and p5.steals <= " + txtP5Stl.Text;
                }
                else
                {
                    p5Picks.Text += txtP5Stl.Text + "stl | ";
                    queryAbove += " and p5.steals >= " + txtP5Stl.Text;
                }
            }

            gamesPlayed = 0;
            gamesAbove = 0;
            GetOdds(query, "gp");
            GetOdds(queryAbove, "ga");

        }
        public static string SaveOdds = "";
        public static float SaveProbability = 0;
        public void GetOdds(string query, string sender)
        {
            SaveOdds = "";
            SaveProbability = 0;
            using (SqlCommand GamesPlayed = new SqlCommand(query))
            {
                GamesPlayed.CommandType = CommandType.Text;
                using (SqlDataAdapter sGamesPlayed = new SqlDataAdapter())
                {
                    GamesPlayed.Connection = busDriver.SQLdb;
                    sGamesPlayed.SelectCommand = GamesPlayed;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = GamesPlayed.ExecuteReader();
                    while (reader.Read())
                    {
                        if (sender == "gp")
                        {
                            gamesPlayed = reader.GetInt32(0);
                        }
                        else if (sender == "ga")
                        {
                            gamesAbove = reader.GetInt32(0);
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }

            if(gamesAbove != 0 && gamesPlayed != 0)
            {
                float probability = ((float)gamesAbove / (float)gamesPlayed) * 100;
                float odds = 0;
                string oddsText = "";
                if (probability <= 50)
                {
                    odds = (100 / (probability / 100)) - 100;
                    oddsText = "+" + odds;
                }
                else
                {
                    odds = (probability / (1 - (probability / 100))) * -1;
                    oddsText = odds.ToString();
                }
                lblError.Text = "";
                lblOdds.Text = gamesAbove + "/" + gamesPlayed + " | " + "Probability: " + Math.Round(probability, 2) + "% | Implied odds: " + oddsText;
                SaveOdds = oddsText;
                SaveProbability = probability;
            }
            else if(sender != "gp" && gamesAbove == 0)
            {

                float probability = ((float)gamesAbove / (float)gamesPlayed) * 100;
                float odds = 0;
                string oddsText = "";
                if (probability <= 50)
                {
                    odds = (100 / (probability / 100)) - 100;
                    oddsText = "+" + odds;
                }
                else
                {
                    odds = (probability / (1 - (probability / 100))) * -1;
                    oddsText = odds.ToString();
                }
                lblError.Text = "0 games with criteria met";
                lblOdds.Text = gamesAbove + "/" + gamesPlayed + " | " + "Probability: " + Math.Round(probability, 2) + "% | Implied odds: " + oddsText;
                SaveOdds = oddsText;
                SaveProbability = probability;
            }
            else if(sender == "ga" &&  gamesPlayed == 0)
            {
                lblOdds.Text = "";
                lblError.Text = "Divide by 0 error i believe";
                SaveOdds = "";
                SaveProbability = 0;
            }
        }


        protected void ddTeams2_SelectedIndexChanged(object sender, EventArgs e)
        {

            t2lblError.Text = "";
            t2lblOdds.Text = "";
            playerPicks.Attributes.Add("style", "visibility: hidden");
            gamesPlayed = 0;
            gamesAbove = 0;
            t2Wins.Visible = true;
            Label10.Visible = true;
            Label11.Visible = true;
            Label12.Visible = true;
            Label13.Visible = true;
            Label14.Visible = true;
            Label15.Visible = true;
            Label16.Visible = true;
            Label17.Visible = true;
            Label18.Visible = true;
            Label19.Visible = true;
            foreach (TextBox textbox in allT2PlayerProps)
            {
                textbox.Text = "";
            }
            foreach (CheckBox check in allT2PlayerPropsUnders)
            {
                check.Checked = false;
            }


            foreach (DropDownList roster in t2FullRoster)
            {
                string ID = roster.SelectedItem.Value;
                string Name = roster.SelectedItem.Text;
                roster.Items.Clear();
                PopulateRoster(roster, ddTeams2.SelectedValue);
                if (SeasonChange == 1)
                {
                    if (!ID.IsNullOrWhiteSpace())
                    {
                        roster.SelectedItem.Value = ID;
                        roster.SelectedItem.Text = Name;
                        if (roster.ID == "ddlRoster")
                        {
                            ddlT2Roster_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlRoster2")
                        {
                            ddlT2Roster2_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlRoster3")
                        {
                            ddlT2Roster3_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlRoster4")
                        {
                            ddlT2Roster4_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlRoster5")
                        {
                            ddlT2Roster5_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlDNP")
                        {
                            ddlT2DNP_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlDNP2")
                        {
                            ddlT2DNP2_SelectedIndexChanged(sender, e);
                        }
                        else if (roster.ID == "ddlDNP3")
                        {
                            ddlT2DNP3_SelectedIndexChanged(sender, e);
                        }
                    }
                }
            }
            PopulateUI(t2Rosters, t2StatSections);
            SeasonChange = 0;
            if (!ddTeams2.SelectedItem.Text.IsNullOrWhiteSpace())
            {
                t2Name.Text = ddTeams2.SelectedItem.Text;
                Team2Averages(Int32.Parse(ddTeams2.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value));
            }
        }

        protected void ddlT2Roster_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlT2Roster.SelectedItem.Text != "Player")
            {
                txtT2P1Pts.Enabled = true; chkT2P1Pts.Enabled = true;
                txtT2P1Ast.Enabled = true; chkT2P1Ast.Enabled = true;
                txtT2P1Reb.Enabled = true; chkT2P1Reb.Enabled = true;
                txtT2P13.Enabled = true; chkT2P13.Enabled = true;
                txtT2P1Blk.Enabled = true; chkT2P1Blk.Enabled = true;
                txtT2P1Stl.Enabled = true; chkT2P1Stl.Enabled = true;
                t2p1Stats.Visible = true;
                GetAverages(T2PlayerProcedure, Int32.Parse(ddlT2Roster.SelectedItem.Value), Int32.Parse(ddTeams2.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), t2p1Name, t2p1Games, t2p1Pts, t2p1Ast, t2p1Reb, t2p1FG, t2p13, t2p1ft, t2p1Blk, t2p1Stl, t2p1PtsExt, t2p1AstExt, t2p1RebExt, t2p1FGExt, t2p13Ext, t2p1ftExt, t2p1BlkExt, t2p1StlExt, t2p1GamesExt);
            }
            else
            {
                txtT2P1Pts.Enabled = false; chkT2P1Pts.Enabled = false;
                txtT2P1Ast.Enabled = false; chkT2P1Ast.Enabled = false;
                txtT2P1Reb.Enabled = false; chkT2P1Reb.Enabled = false;
                txtT2P13.Enabled = false; chkT2P13.Enabled = false;
                txtT2P1Blk.Enabled = false; chkT2P1Blk.Enabled = false;
                txtT2P1Stl.Enabled = false; chkT2P1Stl.Enabled = false;
            }
            foreach (DropDownList roster in t2FullRoster)
            {
                if (roster.ID != "ddlT2Roster")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams2.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlT2Roster.SelectedItem))
                    {
                        roster.Items.Remove(ddlT2Roster.SelectedItem);
                    }
                }
            }
            RosterCheck(t2FullRoster);
        }


        protected void ddlT2Roster2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlT2Roster2.SelectedItem.Text != "Player")
            {
                txtT2P2Pts.Enabled = true; chkT2P2Pts.Enabled = true;
                txtT2P2Ast.Enabled = true; chkT2P2Ast.Enabled = true;
                txtT2P2Reb.Enabled = true; chkT2P2Reb.Enabled = true;
                txtT2P23.Enabled = true; chkT2P23.Enabled = true;
                txtT2P2Blk.Enabled = true; chkT2P2Blk.Enabled = true;
                txtT2P2Stl.Enabled = true; chkT2P2Stl.Enabled = true;
                t2p2Stats.Visible = true;
                GetAverages(T2PlayerProcedure, Int32.Parse(ddlT2Roster2.SelectedItem.Value), Int32.Parse(ddTeams2.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), t2p2Name, t2p2Games, t2p2Pts, t2p2Ast, t2p2Reb, t2p2FG, t2p23, t2p2ft, t2p2Blk, t2p2Stl, t2p2PtsExt, t2p2AstExt, t2p2RebExt, t2p2FGExt, t2p23Ext, t2p2ftExt, t2p2BlkExt, t2p2StlExt, t2p2GamesExt);
            }
            else
            {
                txtT2P2Pts.Enabled = false; chkT2P2Pts.Enabled = false;
                txtT2P2Ast.Enabled = false; chkT2P2Ast.Enabled = false;
                txtT2P2Reb.Enabled = false; chkT2P2Reb.Enabled = false;
                txtT2P23.Enabled = false; chkT2P23.Enabled = false;
                txtT2P2Blk.Enabled = false; chkT2P2Blk.Enabled = false;
                txtT2P2Stl.Enabled = false; chkT2P2Stl.Enabled = false;
            }
            foreach (DropDownList roster in t2FullRoster)
            {
                if (roster.ID != "ddlT2Roster2")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams2.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlT2Roster2.SelectedItem) && ddlT2Roster2.SelectedItem.Text != "Player")
                    {
                        roster.Items.Remove(ddlT2Roster2.SelectedItem);
                    }
                }
            }
            RosterCheck(t2FullRoster);
        }
        protected void ddlT2Roster3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlT2Roster3.SelectedItem.Text != "Player")
            {
                txtT2P3Pts.Enabled = true; chkT2P3Pts.Enabled = true;
                txtT2P3Ast.Enabled = true; chkT2P3Ast.Enabled = true;
                txtT2P3Reb.Enabled = true; chkT2P3Reb.Enabled = true;
                txtT2P33.Enabled = true; chkT2P33.Enabled = true;
                txtT2P3Blk.Enabled = true; chkT2P3Blk.Enabled = true;
                txtT2P3Stl.Enabled = true; chkT2P3Stl.Enabled = true;
                t2p3Stats.Visible = true;
                GetAverages(T2PlayerProcedure, Int32.Parse(ddlT2Roster3.SelectedItem.Value), Int32.Parse(ddTeams2.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), t2p3Name, t2p3Games, t2p3Pts, t2p3Ast, t2p3Reb, t2p3FG, t2p33, t2p3ft, t2p3Blk, t2p3Stl, t2p3PtsExt, t2p3AstExt, t2p3RebExt, t2p3FGExt, t2p33Ext, t2p3ftExt, t2p3BlkExt, t2p3StlExt, t2p3GamesExt);
            }
            else
            {
                txtT2P3Pts.Enabled = false; chkT2P3Pts.Enabled = false;
                txtT2P3Ast.Enabled = false; chkT2P3Ast.Enabled = false;
                txtT2P3Reb.Enabled = false; chkT2P3Reb.Enabled = false;
                txtT2P33.Enabled = false; chkT2P33.Enabled = false;
                txtT2P3Blk.Enabled = false; chkT2P3Blk.Enabled = false;
                txtT2P3Stl.Enabled = false; chkT2P3Stl.Enabled = false;
            }
            foreach (DropDownList roster in t2FullRoster)
            {
                if (roster.ID != "ddlT2Roster3")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams2.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlT2Roster3.SelectedItem) && ddlT2Roster3.SelectedItem.Text != "Player")
                    {
                        roster.Items.Remove(ddlT2Roster3.SelectedItem);
                    }
                }
            }
            RosterCheck(t2FullRoster);
        }
        protected void ddlT2Roster4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlT2Roster4.SelectedItem.Text != "Player")
            {
                txtT2P4Pts.Enabled = true; chkT2P4Pts.Enabled = true;
                txtT2P4Ast.Enabled = true; chkT2P4Ast.Enabled = true;
                txtT2P4Reb.Enabled = true; chkT2P4Reb.Enabled = true;
                txtT2P43.Enabled = true; chkT2P43.Enabled = true;
                txtT2P4Blk.Enabled = true; chkT2P4Blk.Enabled = true;
                txtT2P4Stl.Enabled = true; chkT2P4Stl.Enabled = true;
                t2p4Stats.Visible = true;
                GetAverages(T2PlayerProcedure, Int32.Parse(ddlT2Roster4.SelectedItem.Value), Int32.Parse(ddTeams2.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), t2p4Name, t2p4Games, t2p4Pts, t2p4Ast, t2p4Reb, t2p4FG, t2p43, t2p4ft, t2p4Blk, t2p4Stl, t2p4PtsExt, t2p4AstExt, t2p4RebExt, t2p4FGExt, t2p43Ext, t2p4ftExt, t2p4BlkExt, t2p4StlExt, t2p4GamesExt);
            }
            else
            {
                txtT2P4Pts.Enabled = false; chkT2P4Pts.Enabled = false;
                txtT2P4Ast.Enabled = false; chkT2P4Ast.Enabled = false;
                txtT2P4Reb.Enabled = false; chkT2P4Reb.Enabled = false;
                txtT2P43.Enabled = false; chkT2P43.Enabled = false;
                txtT2P4Blk.Enabled = false; chkT2P4Blk.Enabled = false;
                txtT2P4Stl.Enabled = false; chkT2P4Stl.Enabled = false;
            }
            foreach (DropDownList roster in t2FullRoster)
            {
                if (roster.ID != "ddlT2Roster4")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams2.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlT2Roster4.SelectedItem) && ddlT2Roster4.SelectedItem.Text != "Player")
                    {
                        roster.Items.Remove(ddlT2Roster4.SelectedItem);
                    }
                }
            }
            RosterCheck(t2FullRoster);
        }
        protected void ddlT2Roster5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlT2Roster5.SelectedItem.Text != "Player")
            {
                txtT2P5Pts.Enabled = true; chkT2P5Pts.Enabled = true;
                txtT2P5Ast.Enabled = true; chkT2P5Ast.Enabled = true;
                txtT2P5Reb.Enabled = true; chkT2P5Reb.Enabled = true;
                txtT2P53.Enabled = true; chkT2P53.Enabled = true;
                txtT2P5Blk.Enabled = true; chkT2P5Blk.Enabled = true;
                txtT2P5Stl.Enabled = true; chkT2P5Stl.Enabled = true;
                t2p5Stats.Visible = true;
                GetAverages(T2PlayerProcedure, Int32.Parse(ddlT2Roster5.SelectedItem.Value), Int32.Parse(ddTeams2.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), t2p5Name, t2p5Games, t2p5Pts, t2p5Ast, t2p5Reb, t2p5FG, t2p53, t2p5ft, t2p5Blk, t2p5Stl, t2p5PtsExt, t2p5AstExt, t2p5RebExt, t2p5FGExt, t2p53Ext, t2p5ftExt, t2p5BlkExt, t2p5StlExt, t2p5GamesExt);
            }
            else
            {
                txtT2P5Pts.Enabled = false; chkT2P5Pts.Enabled = false;
                txtT2P5Ast.Enabled = false; chkT2P5Ast.Enabled = false;
                txtT2P5Reb.Enabled = false; chkT2P5Reb.Enabled = false;
                txtT2P53.Enabled = false; chkT2P53.Enabled = false;
                txtT2P5Blk.Enabled = false; chkT2P5Blk.Enabled = false;
                txtT2P5Stl.Enabled = false; chkT2P5Stl.Enabled = false;
            }
            foreach (DropDownList roster in t2FullRoster)
            {
                if (roster.ID != "ddlT2Roster5")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams2.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlT2Roster5.SelectedItem) && ddlT2Roster5.SelectedItem.Text != "Player")
                    {
                        roster.Items.Remove(ddlT2Roster5.SelectedItem);
                    }
                }
            }
            RosterCheck(t2FullRoster);
        }




        protected void ddlT2DNP_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DropDownList roster in t2FullRoster)
            {
                if (roster.ID != "ddlT2DNP")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams2.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlT2DNP.SelectedItem))
                    {
                        roster.Items.Remove(ddlT2DNP.SelectedItem);
                    }
                }
            }
            RosterCheck(t2FullRoster);
        }
        protected void ddlT2DNP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DropDownList roster in t2FullRoster)
            {
                if (roster.ID != "ddlT2DNP2")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams2.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlT2DNP2.SelectedItem))
                    {
                        roster.Items.Remove(ddlT2DNP2.SelectedItem);
                    }
                }
            }
            RosterCheck(t2FullRoster);
        }
        protected void ddlT2DNP3_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DropDownList roster in t2FullRoster)
            {
                if (roster.ID != "ddlT2DNP3")
                {
                    if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
                    {
                        PopulateRoster(roster, ddTeams2.SelectedValue);
                    }
                    if (roster.Items.Contains(ddlT2DNP3.SelectedItem))
                    {
                        roster.Items.Remove(ddlT2DNP3.SelectedItem);
                    }
                }
            }
            RosterCheck(t2FullRoster);
        }



        protected void chkT2Dynamic_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkT2Losses_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkT2Wins_CheckedChanged(object sender, EventArgs e)
        {

        }




        protected void Team2Averages(int team, int season)
        {
            using (SqlCommand querySearch = new SqlCommand(T2TeamProcedure))
            {
                querySearch.Connection = busDriver.SQLdb;
                querySearch.CommandType = CommandType.StoredProcedure;
                querySearch.Parameters.AddWithValue("@team", team);
                querySearch.Parameters.AddWithValue("@season", season);
                if (T2TeamProcedure == "ParlayTeamAverageRanksWL")
                {
                    if (chkT2Wins.Checked)
                    {
                        querySearch.Parameters.AddWithValue("@win", 1);
                    }
                    else
                    {
                        querySearch.Parameters.AddWithValue("@win", 0);
                    }
                }
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = querySearch.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        t2Score.Text = sdr["points"].ToString();
                        t2ScoreR.Text = "(" + sdr["pointsRank"].ToString() + ")";
                        t2ScoreAgainst.Text = sdr["pointsAgainst"].ToString();
                        t2ScoreAgainstR.Text = "(" + sdr["pointsAgainstRank"].ToString() + ")";
                        t2FG.Text = sdr["twoPointersMade"].ToString() + "/" + sdr["twoPointersAttempted"].ToString();
                        t2FGR.Text = "(" + sdr["twoPointersMadeRank"].ToString() + ")/(" + sdr["twoPointersAttemptedRank"].ToString() + ")";
                        t2FG3.Text = sdr["threePointersMade"].ToString() + "/" + sdr["threePointersAttempted"].ToString();
                        t2FG3R.Text = "(" + sdr["threePointersMadeRank"].ToString() + ")/(" + sdr["threePointersAttemptedRank"].ToString() + ")";
                        t2Ast.Text = sdr["assists"].ToString();
                        t2AstR.Text = "(" + sdr["assistsRank"].ToString() + ")";
                        t2Reb.Text = sdr["reboundsPersonal"].ToString();
                        t2RebR.Text = "(" + sdr["reboundsPersonalRank"].ToString() + ")";
                        t2Q1.Text = sdr["q1Points"].ToString() + "/" + sdr["q1PointsAgainst"].ToString();
                        t2Q1R.Text = "(" + sdr["q1PointsRank"].ToString() + ")/(" + sdr["q1PointsAgainstRank"].ToString() + ")";
                        t2Q2.Text = sdr["q2Points"].ToString() + "/" + sdr["q2PointsAgainst"].ToString();
                        t2Q2R.Text = "(" + sdr["q2PointsRank"].ToString() + ")/(" + sdr["q2PointsAgainstRank"].ToString() + ")";
                        t2Q3.Text = sdr["q3Points"].ToString() + "/" + sdr["q3PointsAgainst"].ToString();
                        t2Q3R.Text = "(" + sdr["q3PointsRank"].ToString() + ")/(" + sdr["q3PointsAgainstRank"].ToString() + ")";
                        t2Q4.Text = sdr["q4Points"].ToString() + "/" + sdr["q4PointsAgainst"].ToString();
                        t2Q4R.Text = "(" + sdr["q4PointsRank"].ToString() + ")/(" + sdr["q4PointsAgainstRank"].ToString() + ")";
                        string cRank = "";
                        string lRank = "";
                        if (sdr["ConferenceRank"].ToString() == "1")
                        {
                            cRank = "st";
                        }
                        else if (sdr["ConferenceRank"].ToString() == "2")
                        {
                            cRank = "nd";
                        }
                        else if (sdr["ConferenceRank"].ToString() == "3")
                        {
                            cRank = "rd";
                        }
                        else
                        {
                            cRank = "th";
                        }

                        if (sdr["LeagueRank"].ToString() == "1")
                        {
                            lRank = "st";
                        }
                        else if (sdr["LeagueRank"].ToString() == "2")
                        {
                            lRank = "nd";
                        }
                        else if (sdr["LeagueRank"].ToString() == "3")
                        {
                            lRank = "rd";
                        }
                        else
                        {
                            lRank = "th";
                        }
                        t2Wins.Text = sdr["Wins"] + "-" + sdr["Losses"] + ", " + sdr["ConferenceRank"] + cRank + " in " + sdr["Conference"] + ", " + sdr["LeagueRank"] + lRank + " overall";
                    }
                }
                busDriver.SQLdb.Close();
            }

        }



        public static int t2gamesPlayed = 0;
        public static int t2gamesAbove = 0;

        protected void t2btnRetrieve_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            t2playerPicks.Attributes.Add("style", "visibility: visible; width:fit-content; line-height:15px;");
            int players = 0;
            int DNPs = 0;
            List<DropDownList> t2Players = new List<DropDownList>();
            List<DropDownList> t2DNPs = new List<DropDownList>();
            List<TextBox> t2p1Props = new List<TextBox>();
            List<TextBox> t2p2Props = new List<TextBox>();
            List<TextBox> t2p3Props = new List<TextBox>();
            List<TextBox> t2p4Props = new List<TextBox>();
            List<TextBox> t2p5Props = new List<TextBox>();

            int player1 = 0;
            int player2 = 0;
            int player3 = 0;
            int player4 = 0;
            int player5 = 0;

            int dnp1 = 0;
            int dnp2 = 0;
            int dnp3 = 0;

            string select = "select count(distinct p.game_id) Games ";
            string from = "from playerBox p";
            string where = "where p.season_id = " + ddSeason.SelectedItem.Value + " and p.team_id = " + ddTeams2.SelectedItem.Value;
            string whereAbove = "where p.season_id = " + ddSeason.SelectedItem.Value + " and p.team_id = " + ddTeams2.SelectedItem.Value;


            foreach (DropDownList roster in t2Rosters)
            {
                if (!roster.SelectedItem.Value.IsNullOrWhiteSpace())
                {
                    players++;
                    t2Players.Add(roster);
                }
            }
            foreach (DropDownList roster in t2DNPRosters)
            {
                if (!roster.SelectedItem.Value.IsNullOrWhiteSpace())
                {
                    DNPs++;
                    t2DNPs.Add(roster);
                }
            }


            for (int i = 0; i < players; i++)
            {
                if (i == 0)
                {
                    foreach (TextBox prop in t2p1)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            t2p1Props.Add(prop);
                        }
                    }
                    player1 = Int32.Parse(t2Players[0].SelectedItem.Value);
                    where += " and p.status = 'ACTIVE' and p.minutesCalculated != 'PT00M' and p.player_id = " + player1;
                    t2p1Picks.Text = ddlT2Roster.SelectedItem.Text + ": ";
                }
                else if (i == 1)
                {
                    foreach (TextBox prop in t2p2)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            t2p2Props.Add(prop);
                        }
                    }
                    player2 = Int32.Parse(t2Players[1].SelectedItem.Value);
                    from += " inner join playerBox p2 on p.game_id = p2.game_id and p.team_id = p2.team_id and p.season_id = p2.season_id";
                    where += " and p2.status = 'ACTIVE' and p2.minutesCalculated != 'PT00M' and p2.player_id = " + player2;
                    t2p2Picks.Text = ddlT2Roster2.SelectedItem.Text + ": ";
                }
                else if (i == 2)
                {
                    foreach (TextBox prop in t2p3)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            t2p3Props.Add(prop);
                        }
                    }
                    player3 = Int32.Parse(t2Players[2].SelectedItem.Value);
                    from += " inner join playerBox p3 on p.game_id = p3.game_id and p.team_id = p3.team_id and p.season_id = p3.season_id";
                    where += " and p3.status = 'ACTIVE' and p3.minutesCalculated != 'PT00M' and p3.player_id = " + player3;
                    t2p3Picks.Text = ddlT2Roster3.SelectedItem.Text + ": ";
                }
                else if (i == 3)
                {
                    foreach (TextBox prop in t2p4)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            t2p4Props.Add(prop);
                        }
                    }
                    player4 = Int32.Parse(t2Players[3].SelectedItem.Value);
                    from += " inner join playerBox p4 on p.game_id = p4.game_id and p.team_id = p4.team_id and p.season_id = p4.season_id";
                    where += " and p4.status = 'ACTIVE' and p4.minutesCalculated != 'PT00M' and p4.player_id = " + player4;
                    t2p4Picks.Text = ddlT2Roster4.SelectedItem.Text + ": ";
                }
                else if (i == 4)
                {
                    foreach (TextBox prop in t2p5)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            t2p5Props.Add(prop);
                        }
                    }
                    player5 = Int32.Parse(t2Players[4].SelectedItem.Value);
                    from += " inner join playerBox p5 on p.game_id = p5.game_id and p.team_id = p5.team_id and p.season_id = p5.season_id";
                    where += " and p5.status = 'ACTIVE' and p5.minutesCalculated != 'PT00M' and p5.player_id = " + player5;
                    t2p5Picks.Text = ddlT2Roster5.SelectedItem.Text + ": ";
                }
            }
            for (int i = 0; i < DNPs; i++)
            {
                if (i == 0)
                {
                    dnp1 = Int32.Parse(t2DNPs[0].SelectedItem.Value);
                    from += " inner join playerBox d on p.game_id = d.game_id and p.team_id = d.team_id and p.season_id = d.season_id";
                    where += " and (d.status != 'ACTIVE' or d.minutesCalculated = 'PT00M') and d.player_id = " + dnp1;
                    t2DNP1.Text = "DNP: " + t2DNPs[0].SelectedItem.Text;
                }
                else if (i == 1)
                {
                    dnp2 = Int32.Parse(t2DNPs[1].SelectedItem.Value);
                    from += " inner join playerBox d2 on p.game_id = d2.game_id and p.team_id = d2.team_id and p.season_id = d2.season_id";
                    where += " and (d2.status != 'ACTIVE' or d2.minutesCalculated = 'PT00M') and d2.player_id = " + dnp2;
                    t2DNP2.Text = "DNP: " + t2DNPs[1].SelectedItem.Text;
                }
                else if (i == 2)
                {
                    dnp3 = Int32.Parse(t2DNPs[2].SelectedItem.Value);
                    from += " inner join playerBox d3 on p.game_id = d3.game_id and p.team_id = d3.team_id and p.season_id = d3.season_id";
                    where += " and (d3.status != 'ACTIVE' or d3.minutesCalculated = 'PT00M') and d3.player_id = " + dnp3;
                    t2DNP3.Text = "DNP: " + t2DNPs[2].SelectedItem.Text;
                }
            }
            string query = select + from + " " + where;
            string queryAbove = query;

            if (!txtT2P1Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P1Pts.Checked)
                {
                    t2p1Picks.Text += "Under " + txtT2P1Pts.Text + "pts | ";
                    queryAbove += " and p.points <= " + txtT2P1Pts.Text;
                }
                else
                {
                    t2p1Picks.Text += txtT2P1Pts.Text + "pts | ";
                    queryAbove += " and p.points >= " + txtT2P1Pts.Text;
                }
            }
            if (!txtT2P1Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P1Ast.Checked)
                {
                    t2p1Picks.Text += "Under " + txtT2P1Ast.Text + "ast | ";
                    queryAbove += " and p.assists <= " + txtT2P1Ast.Text;
                }
                else
                {
                    t2p1Picks.Text += txtT2P1Ast.Text + "ast | ";
                    queryAbove += " and p.assists >= " + txtT2P1Ast.Text;
                }
            }
            if (!txtT2P1Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P1Reb.Checked)
                {
                    t2p1Picks.Text += "Under " + txtT2P1Reb.Text + "reb | ";
                    queryAbove += " and p.reboundsTotal <= " + txtT2P1Reb.Text;
                }
                else
                {
                    t2p1Picks.Text += txtT2P1Reb.Text + "reb | ";
                    queryAbove += " and p.reboundsTotal >= " + txtT2P1Reb.Text;
                }
            }
            if (!txtT2P13.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P13.Checked)
                {
                    t2p1Picks.Text += "Under " + txtT2P13.Text + " 3PM | ";
                    queryAbove += " and p.threePointersMade <= " + txtT2P13.Text;
                }
                else
                {
                    t2p1Picks.Text += txtT2P13.Text + " 3PM | ";
                    queryAbove += " and p.threePointersMade >= " + txtT2P13.Text;
                }
            }
            if (!txtT2P1Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P1Blk.Checked)
                {
                    t2p1Picks.Text += "Under " + txtT2P1Blk.Text + "blk | ";
                    queryAbove += " and p.blocks <= " + txtT2P1Blk.Text;
                }
                else
                {
                    t2p1Picks.Text += txtT2P1Blk.Text + "blk | ";
                    queryAbove += " and p.blocks >= " + txtT2P1Blk.Text;
                }
            }
            if (!txtT2P1Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P1Stl.Checked)
                {
                    t2p1Picks.Text += "Under " + txtT2P1Stl.Text + "stl | ";
                    queryAbove += " and p.steals <= " + txtT2P1Stl.Text;
                }
                else
                {
                    t2p1Picks.Text += txtT2P1Stl.Text + "stl | ";
                    queryAbove += " and p.steals >= " + txtT2P1Stl.Text;
                }
            }




            if (!txtT2P2Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P2Pts.Checked)
                {
                    t2p2Picks.Text += "Under " + txtT2P2Pts.Text + "pts | ";
                    queryAbove += " and p2.points <= " + txtT2P2Pts.Text;
                }
                else
                {
                    t2p2Picks.Text += txtT2P2Pts.Text + "pts | ";
                    queryAbove += " and p2.points >= " + txtT2P2Pts.Text;
                }
            }
            if (!txtT2P2Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P2Ast.Checked)
                {
                    t2p2Picks.Text += "Under " + txtT2P2Ast.Text + "ast | ";
                    queryAbove += " and p2.assists <= " + txtT2P2Ast.Text;
                }
                else
                {
                    t2p2Picks.Text += txtT2P2Ast.Text + "ast | ";
                    queryAbove += " and p2.assists >= " + txtT2P2Ast.Text;
                }
            }
            if (!txtT2P2Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P2Reb.Checked)
                {
                    t2p2Picks.Text += "Under " + txtT2P2Reb.Text + "reb | ";
                    queryAbove += " and p2.reboundsTotal <= " + txtT2P2Reb.Text;
                }
                else
                {
                    t2p2Picks.Text += txtT2P2Reb.Text + "reb | ";
                    queryAbove += " and p2.reboundsTotal >= " + txtT2P2Reb.Text;
                }
            }
            if (!txtT2P23.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P23.Checked)
                {
                    t2p2Picks.Text += "Under " + txtT2P23.Text + " 3PM | ";
                    queryAbove += " and p2.threePointersMade <= " + txtT2P23.Text;
                }
                else
                {
                    t2p2Picks.Text += txtT2P23.Text + " 3PM | ";
                    queryAbove += " and p2.threePointersMade >= " + txtT2P23.Text;
                }
            }
            if (!txtT2P2Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P2Blk.Checked)
                {
                    t2p2Picks.Text += "Under " + txtT2P2Blk.Text + "blk | ";
                    queryAbove += " and p2.blocks <= " + txtT2P2Blk.Text;
                }
                else
                {
                    t2p2Picks.Text += txtT2P2Blk.Text + "blk | ";
                    queryAbove += " and p2.blocks >= " + txtT2P2Blk.Text;
                }
            }
            if (!txtT2P2Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P2Stl.Checked)
                {
                    t2p2Picks.Text += "Under " + txtT2P2Stl.Text + "stl | ";
                    queryAbove += " and p2.steals <= " + txtT2P2Stl.Text;
                }
                else
                {
                    t2p2Picks.Text += txtT2P2Stl.Text + "stl | ";
                    queryAbove += " and p2.steals >= " + txtT2P2Stl.Text;
                }
            }





            if (!txtT2P3Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P3Pts.Checked)
                {
                    t2p3Picks.Text += "Under " + txtT2P3Pts.Text + "pts | ";
                    queryAbove += " and p3.points <= " + txtT2P3Pts.Text;
                }
                else
                {
                    t2p3Picks.Text += txtT2P3Pts.Text + "pts | ";
                    queryAbove += " and p3.points >= " + txtT2P3Pts.Text;
                }
            }
            if (!txtT2P3Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P3Ast.Checked)
                {
                    t2p3Picks.Text += "Under " + txtT2P3Ast.Text + "ast | ";
                    queryAbove += " and p3.assists <= " + txtT2P3Ast.Text;
                }
                else
                {
                    t2p3Picks.Text += txtT2P3Ast.Text + "ast | ";
                    queryAbove += " and p3.assists >= " + txtT2P3Ast.Text;
                }
            }
            if (!txtT2P3Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P3Reb.Checked)
                {
                    t2p3Picks.Text += "Under " + txtT2P3Reb.Text + "reb | ";
                    queryAbove += " and p3.reboundsTotal <= " + txtT2P3Reb.Text;
                }
                else
                {
                    t2p3Picks.Text += txtT2P3Reb.Text + "reb | ";
                    queryAbove += " and p3.reboundsTotal >= " + txtT2P3Reb.Text;
                }
            }
            if (!txtT2P33.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P33.Checked)
                {
                    t2p3Picks.Text += "Under " + txtT2P33.Text + " 3PM | ";
                    queryAbove += " and p3.threePointersMade <= " + txtT2P33.Text;
                }
                else
                {
                    t2p3Picks.Text += txtT2P33.Text + " 3PM | ";
                    queryAbove += " and p3.threePointersMade >= " + txtT2P33.Text;
                }
            }
            if (!txtT2P3Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P3Blk.Checked)
                {
                    t2p3Picks.Text += "Under " + txtT2P3Blk.Text + "blk | ";
                    queryAbove += " and p3.blocks <= " + txtT2P3Blk.Text;
                }
                else
                {
                    t2p3Picks.Text += txtT2P3Blk.Text + "blk | ";
                    queryAbove += " and p3.blocks >= " + txtT2P3Blk.Text;
                }
            }
            if (!txtT2P3Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P3Stl.Checked)
                {
                    t2p3Picks.Text += "Under " + txtT2P3Stl.Text + "stl | ";
                    queryAbove += " and p3.steals <= " + txtT2P3Stl.Text;
                }
                else
                {
                    t2p3Picks.Text += txtT2P3Stl.Text + "stl | ";
                    queryAbove += " and p3.steals >= " + txtT2P3Stl.Text;
                }
            }




            if (!txtT2P4Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P4Pts.Checked)
                {
                    t2p4Picks.Text += "Under " + txtT2P4Pts.Text + "pts | ";
                    queryAbove += " and p4.points <= " + txtT2P4Pts.Text;
                }
                else
                {
                    t2p4Picks.Text += txtT2P4Pts.Text + "pts | ";
                    queryAbove += " and p4.points >= " + txtT2P4Pts.Text;
                }
            }
            if (!txtT2P4Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P4Ast.Checked)
                {
                    t2p4Picks.Text += "Under " + txtT2P4Ast.Text + "ast | ";
                    queryAbove += " and p4.assists <= " + txtT2P4Ast.Text;
                }
                else
                {
                    t2p4Picks.Text += txtT2P4Ast.Text + "ast | ";
                    queryAbove += " and p4.assists >= " + txtT2P4Ast.Text;
                }
            }
            if (!txtT2P4Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P4Reb.Checked)
                {
                    t2p4Picks.Text += "Under " + txtT2P4Reb.Text + "reb | ";
                    queryAbove += " and p4.reboundsTotal <= " + txtT2P4Reb.Text;
                }
                else
                {
                    t2p4Picks.Text += txtT2P4Reb.Text + "reb | ";
                    queryAbove += " and p4.reboundsTotal >= " + txtT2P4Reb.Text;
                }
            }
            if (!txtT2P43.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P43.Checked)
                {
                    t2p4Picks.Text += "Under " + txtT2P43.Text + " 3PM | ";
                    queryAbove += " and p4.threePointersMade <= " + txtT2P43.Text;
                }
                else
                {
                    t2p4Picks.Text += txtT2P43.Text + " 3PM | ";
                    queryAbove += " and p4.threePointersMade >= " + txtT2P43.Text;
                }
            }
            if (!txtT2P4Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P4Blk.Checked)
                {
                    t2p4Picks.Text += "Under " + txtT2P4Blk.Text + "blk | ";
                    queryAbove += " and p4.blocks <= " + txtT2P4Blk.Text;
                }
                else
                {
                    t2p4Picks.Text += txtT2P4Blk.Text + "blk | ";
                    queryAbove += " and p4.blocks >= " + txtT2P4Blk.Text;
                }
            }
            if (!txtT2P4Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P4Stl.Checked)
                {
                    t2p4Picks.Text += "Under " + txtT2P4Stl.Text + "stl | ";
                    queryAbove += " and p4.steals <= " + txtT2P4Stl.Text;
                }
                else
                {
                    t2p4Picks.Text += txtT2P4Stl.Text + "stl | ";
                    queryAbove += " and p4.steals >= " + txtT2P4Stl.Text;
                }
            }

            if (!txtT2P5Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P5Pts.Checked)
                {
                    t2p5Picks.Text += "Under " + txtT2P5Pts.Text + "pts | ";
                    queryAbove += " and p5.points <= " + txtT2P5Pts.Text;
                }
                else
                {
                    t2p5Picks.Text += txtT2P5Pts.Text + "pts | ";
                    queryAbove += " and p5.points >= " + txtT2P5Pts.Text;
                }
            }
            if (!txtT2P5Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P5Ast.Checked)
                {
                    t2p5Picks.Text += "Under " + txtT2P5Ast.Text + "ast | ";
                    queryAbove += " and p5.assists <= " + txtT2P5Ast.Text;
                }
                else
                {
                    t2p5Picks.Text += txtT2P5Ast.Text + "ast | ";
                    queryAbove += " and p5.assists >= " + txtT2P5Ast.Text;
                }
            }
            if (!txtT2P5Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P5Reb.Checked)
                {
                    t2p5Picks.Text += "Under " + txtT2P5Reb.Text + "reb | ";
                    queryAbove += " and p5.reboundsTotal <= " + txtT2P5Reb.Text;
                }
                else
                {
                    t2p5Picks.Text += txtT2P5Reb.Text + "reb | ";
                    queryAbove += " and p5.reboundsTotal >= " + txtT2P5Reb.Text;
                }
            }
            if (!txtT2P53.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P53.Checked)
                {
                    t2p5Picks.Text += "Under " + txtT2P53.Text + " 3PM | ";
                    queryAbove += " and p5.threePointersMade <= " + txtT2P53.Text;
                }
                else
                {
                    t2p5Picks.Text += txtT2P53.Text + " 3PM | ";
                    queryAbove += " and p5.threePointersMade >= " + txtT2P53.Text;
                }
            }
            if (!txtT2P5Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P5Blk.Checked)
                {
                    t2p5Picks.Text += "Under " + txtT2P5Blk.Text + "blk | ";
                    queryAbove += " and p5.blocks <= " + txtT2P5Blk.Text;
                }
                else
                {
                    t2p5Picks.Text += txtT2P5Blk.Text + "blk | ";
                    queryAbove += " and p5.blocks >= " + txtT2P5Blk.Text;
                }
            }
            if (!txtT2P5Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkT2P5Stl.Checked)
                {
                    t2p5Picks.Text += "Under " + txtT2P5Stl.Text + "stl | ";
                    queryAbove += " and p5.steals <= " + txtT2P5Stl.Text;
                }
                else
                {
                    t2p5Picks.Text += txtT2P5Stl.Text + "stl | ";
                    queryAbove += " and p5.steals >= " + txtT2P5Stl.Text;
                }
            }

            t2gamesPlayed = 0;
            t2gamesAbove = 0;
            GetT2Odds(query, "gp");
            GetT2Odds(queryAbove, "ga");
        }

        public void GetT2Odds(string query, string sender)
        {
            using (SqlCommand GamesPlayed = new SqlCommand(query))
            {
                GamesPlayed.CommandType = CommandType.Text;
                using (SqlDataAdapter sGamesPlayed = new SqlDataAdapter())
                {
                    GamesPlayed.Connection = busDriver.SQLdb;
                    sGamesPlayed.SelectCommand = GamesPlayed;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = GamesPlayed.ExecuteReader();
                    while (reader.Read())
                    {
                        if (sender == "gp")
                        {
                            t2gamesPlayed = reader.GetInt32(0);
                        }
                        else if (sender == "ga")
                        {
                            t2gamesAbove = reader.GetInt32(0);
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }

            if (t2gamesAbove != 0 && t2gamesPlayed != 0)
            {
                float probability = ((float)t2gamesAbove / (float)t2gamesPlayed) * 100;
                float odds = 0;
                string oddsText = "";
                if (probability <= 50)
                {
                    odds = (100 / (probability / 100)) - 100;
                    oddsText = "+" + odds;
                }
                else
                {
                    odds = (probability / (1 - (probability / 100))) * -1;
                    oddsText = odds.ToString();
                }
                t2lblError.Text = "";
                t2lblOdds.Text = t2gamesAbove + "/" + t2gamesPlayed + " | " + "Probability: " + Math.Round(probability, 2) + "% | Implied odds: " + oddsText;
            }
            else if (sender != "gp" && t2gamesAbove == 0)
            {

                float probability = ((float)t2gamesAbove / (float)t2gamesPlayed) * 100;
                float odds = 0;
                string oddsText = "";
                if (probability <= 50)
                {
                    odds = (100 / (probability / 100)) - 100;
                    oddsText = "+" + odds;
                }
                else
                {
                    odds = (probability / (1 - (probability / 100))) * -1;
                    oddsText = odds.ToString();
                }
                t2lblError.Text = "0 games with criteria met";
                t2lblOdds.Text = t2gamesAbove + "/" + t2gamesPlayed + " | " + "Probability: " + Math.Round(probability, 2) + "% | Implied odds: " + oddsText;
            }
            else if (sender == "ga" && t2gamesPlayed == 0)
            {
                t2lblOdds.Text = "";
                t2lblError.Text = "Divide by 0 error i believe";
            }
        }

        public void RosterCheck(List<DropDownList> rosters)
        {

            foreach (DropDownList roster in rosters)
            {
                int addPlayer = 0;
                int addDNP = 0;
                foreach (ListItem player in roster.Items)
                {
                    if (player.Text == "Player")
                    {
                        addPlayer = 1;
                    }
                    if (player.Text.Contains("DNP"))
                    {
                        addDNP = 1;
                    }
                }
                if (addPlayer == 0)
                {
                    ListItem emptyItem = new ListItem("", "");
                    if (roster.ID.Contains("ddlRoster"))
                    {
                        emptyItem = new ListItem("Player", "");
                        roster.Items.Insert(0, emptyItem);
                    }
                }
                if (addDNP == 0)
                {
                    ListItem emptyItem = new ListItem("", "");
                    if (roster.ID.Contains("DNP"))
                    {
                        emptyItem = new ListItem("DNP Player", "");
                        roster.Items.Insert(0, emptyItem);
                    }
                }
            }
        }

        public static List<Tuple<TextBox, CheckBox>> t1FullList;
        public void btnSave_Click(object sender, EventArgs e)
        {

            t1FullList = new List<Tuple<TextBox, CheckBox>>();
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP1Pts, chkP1Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP1Ast, chkP1Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP1Reb, chkP1Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP13, chkP13));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP1Blk, chkP1Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP1Stl, chkP1Stl));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2Pts, chkP2Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2Ast, chkP2Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2Reb, chkP2Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP23, chkP23));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2Blk, chkP2Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2Stl, chkP2Stl));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3Pts, chkP3Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3Ast, chkP3Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3Reb, chkP3Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP33, chkP33));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3Blk, chkP3Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3Stl, chkP3Stl));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4Pts, chkP4Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4Ast, chkP4Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4Reb, chkP4Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP43, chkP43));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4Blk, chkP4Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4Stl, chkP4Stl));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5Pts, chkP5Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5Ast, chkP5Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5Reb, chkP5Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP53, chkP53));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5Blk, chkP5Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5Stl, chkP5Stl));
           
            using (SqlCommand SaveParlay = new SqlCommand("SaveParlay"))
            {
                SaveParlay.Connection = busDriver.SQLdb; //17
                SaveParlay.CommandType = CommandType.StoredProcedure;
                SaveParlay.Parameters.AddWithValue("@team_id", Int32.Parse(ddTeams.SelectedItem.Value));
                SaveParlay.Parameters.AddWithValue("@p1", ddlRoster.SelectedItem.Text);
                SaveParlay.Parameters.AddWithValue("@p2", ddlRoster2.SelectedItem.Text);
                SaveParlay.Parameters.AddWithValue("@p3", ddlRoster3.SelectedItem.Text);
                SaveParlay.Parameters.AddWithValue("@p4", ddlRoster4.SelectedItem.Text);
                SaveParlay.Parameters.AddWithValue("@p5", ddlRoster5.SelectedItem.Text);
                SaveParlay.Parameters.AddWithValue("@GamesPlayed", gamesPlayed);
                SaveParlay.Parameters.AddWithValue("@GamesAbove", gamesAbove);
                SaveParlay.Parameters.AddWithValue("@Odds", SaveOdds);
                SaveParlay.Parameters.AddWithValue("@Probability", SaveProbability);




                int parsedValue = 0;
                if (Int32.TryParse(ddlRoster.SelectedItem.Value, out parsedValue))
                {
                    SaveParlay.Parameters.AddWithValue("@p1_id", parsedValue);
                }
                else
                {
                    SaveParlay.Parameters.AddWithValue("@p1_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlRoster2.SelectedItem.Value, out parsedValue))
                {
                    SaveParlay.Parameters.AddWithValue("@p2_id", parsedValue);
                }
                else
                {
                    SaveParlay.Parameters.AddWithValue("@p2_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlRoster3.SelectedItem.Value, out parsedValue))
                {
                    SaveParlay.Parameters.AddWithValue("@p3_id", parsedValue);
                }
                else
                {
                    SaveParlay.Parameters.AddWithValue("@p3_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlRoster4.SelectedItem.Value, out parsedValue))
                {
                    SaveParlay.Parameters.AddWithValue("@p4_id", parsedValue);
                }
                else
                {
                    SaveParlay.Parameters.AddWithValue("@p4_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlRoster5.SelectedItem.Value, out parsedValue))
                {
                    SaveParlay.Parameters.AddWithValue("@p5_id", parsedValue);
                }
                else
                {
                    SaveParlay.Parameters.AddWithValue("@p5_id", SqlInt32.Null);
                }


                if (Int32.TryParse(ddlDNP.SelectedItem.Value, out parsedValue))
                {
                    SaveParlay.Parameters.AddWithValue("@dnp1_id", parsedValue);
                }
                else
                {
                    SaveParlay.Parameters.AddWithValue("@dnp1_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlDNP2.SelectedItem.Value, out parsedValue))
                {
                    SaveParlay.Parameters.AddWithValue("@dnp2_id", parsedValue);
                }
                else
                {
                    SaveParlay.Parameters.AddWithValue("@dnp2_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlDNP3.SelectedItem.Value, out parsedValue))
                {
                    SaveParlay.Parameters.AddWithValue("@dnp3_id", parsedValue);
                }
                else
                {
                    SaveParlay.Parameters.AddWithValue("@dnp3_id", SqlInt32.Null);
                }
                SaveParlay.Parameters.AddWithValue("@dnp1", ddlDNP.SelectedItem.Text);
                SaveParlay.Parameters.AddWithValue("@dnp2", ddlDNP2.SelectedItem.Text);
                SaveParlay.Parameters.AddWithValue("@dnp3", ddlDNP3.SelectedItem.Text);
                foreach (Tuple<TextBox, CheckBox> tuple in t1FullList)
                {
                    string parameter = "@" + tuple.Item1.ID.Replace("txt", "");
                    if (!tuple.Item1.Text.IsNullOrWhiteSpace())
                    {
                        string value = "";
                        if (tuple.Item2.Checked)
                        {
                            value = "u" + tuple.Item1.Text;
                        }
                        else
                        {
                            value = tuple.Item1.Text;
                        }
                        SaveParlay.Parameters.AddWithValue(parameter, value);
                    }
                    else
                    {
                        SaveParlay.Parameters.AddWithValue(parameter, SqlString.Null);
                    }
                }
                busDriver.SQLdb.Open();
                SaveParlay.ExecuteScalar();
                busDriver.SQLdb.Close();
            }
        }

        protected void t2btnSave_Click(object sender, EventArgs e)
        {

        }


        public void SaveParlay()
        {

        }
    }
}