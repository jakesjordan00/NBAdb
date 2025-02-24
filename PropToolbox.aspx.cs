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
using System.Xml.Linq;
using System.IO;


namespace NBAdb
{
    public partial class PropToolbox : System.Web.UI.Page
    {
        public static int counter = 0;
        public static BusDriver busDriver = new BusDriver();

        public List<DropDownList> t1Rosters;
        public List<DropDownList> t1DNPRosters;
        public List<DropDownList> t1ActiveDNPRosters;
        public List<DropDownList> t1FullRoster;
        public List<Control> statSections;

        public List<TextBox> p1; public List<CheckBox> p1chk;
        public List<TextBox> p2; public List<CheckBox> p2chk;
        public List<TextBox> p3; public List<CheckBox> p3chk;
        public List<TextBox> p4; public List<CheckBox> p4chk;
        public List<TextBox> p5; public List<CheckBox> p5chk;
        public List<TextBox> p6; public List<CheckBox> p6chk;
        public List<TextBox> p7; public List<CheckBox> p7chk;
        public List<TextBox> p8; public List<CheckBox> p8chk;


        public List<TextBox> allT1PlayerProps;
        public List<CheckBox> allT1PlayerPropsUnders;

        public static string T1PlayerProcedure = "ToolboxAverages";
        public static string T1TeamProcedure = "ParlayTeamAverageRanks";



        public List<List<TextBox>> txtList = new List<List<TextBox>>();
        public List<List<CheckBox>> chkList = new List<List<CheckBox>>();


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
                ddTeams
            };


            t1Rosters = new List<DropDownList>
            {
                ddlRoster, ddlRoster2, ddlRoster3, ddlRoster4, ddlRoster5, ddlRoster6, ddlRoster7, ddlRoster8
            };
            t1DNPRosters = new List<DropDownList>
            {
                ddlDNP, ddlDNP2, ddlDNP3, ddlDNP4, ddlDNP5, ddlDNP6, ddlDNP7, ddlDNP8
            };
            t1ActiveDNPRosters = new List<DropDownList>
            {
                ddlDNP, ddlDNP2, ddlDNP3
            };

            t1FullRoster = new List<DropDownList>
            {
               ddlRoster, ddlRoster2, ddlRoster3, ddlRoster4, ddlRoster5, ddlRoster6, ddlRoster7, ddlRoster8, ddlDNP, ddlDNP2, ddlDNP3, ddlDNP4, ddlDNP5, ddlDNP6, ddlDNP7, ddlDNP8
            };
            statSections = new List<Control>
            {
                p1Stats, p2Stats, p3Stats, p4Stats, p5Stats, p6Stats, p7Stats, p8Stats
            };
            p1 = new List<TextBox>                                                                                  
            {
                txtP1Pts, txtP1Ast, txtP1Reb, txtP13, txtP1Blk, txtP1Stl, txtP1PA, txtP1PR, txtP1AR, txtP1PAR
            };
            p2 = new List<TextBox>
            {
                txtP2Pts, txtP2Ast, txtP2Reb, txtP23, txtP2Blk, txtP2Stl, txtP2PA, txtP2PR, txtP2AR, txtP2PAR
            };
            p3 = new List<TextBox>
            {
                txtP3Pts, txtP3Ast, txtP3Reb, txtP33, txtP3Blk, txtP3Stl, txtP3PA, txtP3PR, txtP3AR, txtP3PAR
            };
            p4 = new List<TextBox>
            {
                txtP4Pts, txtP4Ast, txtP4Reb, txtP43, txtP4Blk, txtP4Stl, txtP4PA, txtP4PR, txtP4AR, txtP4PAR
            };
            p5 = new List<TextBox>
            {
                txtP5Pts, txtP5Ast, txtP5Reb, txtP53, txtP5Blk, txtP5Stl, txtP5PA, txtP5PR, txtP5AR, txtP5PAR
            };
            p6 = new List<TextBox>
            {
                txtP6Pts, txtP6Ast, txtP6Reb, txtP63, txtP6Blk, txtP6Stl, txtP6PA, txtP6PR, txtP6AR, txtP6PAR
            };
            p7 = new List<TextBox>
            {
                txtP7Pts, txtP7Ast, txtP7Reb, txtP73, txtP7Blk, txtP7Stl, txtP7PA, txtP7PR, txtP7AR, txtP7PAR
            };
            p8 = new List<TextBox>
            {
                txtP8Pts, txtP8Ast, txtP8Reb, txtP83, txtP8Blk, txtP8Stl, txtP8PA, txtP8PR, txtP8AR, txtP8PAR
            };

            p1chk = new List<CheckBox>
            {
                chkP1Pts, chkP1Ast, chkP1Reb, chkP13, chkP1Blk, chkP1Stl, chkP1PA, chkP1PR, chkP1AR, chkP1PAR
            };
            p2chk = new List<CheckBox>
            {
                chkP2Pts, chkP2Ast, chkP2Reb, chkP23, chkP2Blk, chkP2Stl, chkP2PA, chkP2PR, chkP2AR, chkP2PAR
            };
            p3chk = new List<CheckBox>
            {
                chkP3Pts, chkP3Ast, chkP3Reb, chkP33, chkP3Blk, chkP3Stl, chkP3PA, chkP3PR, chkP3AR, chkP3PAR
            };
            p4chk = new List<CheckBox>
            {
                chkP4Pts, chkP4Ast, chkP4Reb, chkP43, chkP4Blk, chkP4Stl, chkP4PA, chkP4PR, chkP4AR, chkP4PAR
            };
            p5chk = new List<CheckBox>
            {
                chkP5Pts, chkP5Ast, chkP5Reb, chkP53, chkP5Blk, chkP5Stl, chkP5PA, chkP5PR, chkP5AR, chkP5PAR
            };
            p6chk = new List<CheckBox>
            {
                chkP6Pts, chkP6Ast, chkP6Reb, chkP63, chkP6Blk, chkP6Stl, chkP6PA, chkP6PR, chkP6AR, chkP6PAR
            };
            p7chk = new List<CheckBox>
            {
                chkP7Pts, chkP7Ast, chkP7Reb, chkP73, chkP7Blk, chkP7Stl, chkP7PA, chkP7PR, chkP7AR, chkP7PAR
            };
            p8chk = new List<CheckBox>
            {
                chkP8Pts, chkP8Ast, chkP8Reb, chkP83, chkP8Blk, chkP8Stl, chkP8PA, chkP8PR, chkP8AR, chkP8PAR
            };


            txtList = new List<List<TextBox>>
            {
                p1, p2, p3, p4, p5, p6, p7, p8
            };

            chkList = new List<List<CheckBox>>
            {
                p1chk, p2chk, p3chk, p4chk, p5chk, p6chk, p7chk, p8chk
            };


            allT1PlayerProps = new List<TextBox>
            {
                txtP1Pts, txtP1Ast, txtP1Reb, txtP13, txtP1Blk, txtP1Stl, txtP1PA,txtP1PR, txtP1AR, txtP1PAR,
                txtP2Pts, txtP2Ast, txtP2Reb, txtP23, txtP2Blk, txtP2Stl, txtP2PA,txtP2PR, txtP2AR, txtP2PAR,
                txtP3Pts, txtP3Ast, txtP3Reb, txtP33, txtP3Blk, txtP3Stl, txtP3PA,txtP3PR, txtP3AR, txtP3PAR,
                txtP4Pts, txtP4Ast, txtP4Reb, txtP43, txtP4Blk, txtP4Stl, txtP4PA,txtP4PR, txtP4AR, txtP4PAR,
                txtP5Pts, txtP5Ast, txtP5Reb, txtP53, txtP5Blk, txtP5Stl, txtP5PA,txtP5PR, txtP5AR, txtP5PAR,
                txtP6Pts, txtP6Ast, txtP6Reb, txtP63, txtP6Blk, txtP6Stl, txtP6PA,txtP6PR, txtP6AR, txtP6PAR,
                txtP7Pts, txtP7Ast, txtP7Reb, txtP73, txtP7Blk, txtP7Stl, txtP7PA,txtP7PR, txtP7AR, txtP7PAR,
                txtP8Pts, txtP8Ast, txtP8Reb, txtP83, txtP8Blk, txtP8Stl, txtP8PA,txtP8PR, txtP8AR, txtP8PAR

            };

            allT1PlayerPropsUnders = new List<CheckBox>
            {
                chkP1Pts, chkP1Ast, chkP1Reb, chkP13, chkP1Blk, chkP1Stl, chkP1PA,chkP1PR, chkP1AR, chkP1PAR,
                chkP2Pts, chkP2Ast, chkP2Reb, chkP23, chkP2Blk, chkP2Stl, chkP2PA,chkP2PR, chkP2AR, chkP2PAR,
                chkP3Pts, chkP3Ast, chkP3Reb, chkP33, chkP3Blk, chkP3Stl, chkP3PA,chkP3PR, chkP3AR, chkP3PAR,
                chkP4Pts, chkP4Ast, chkP4Reb, chkP43, chkP4Blk, chkP4Stl, chkP4PA,chkP4PR, chkP4AR, chkP4PAR,
                chkP5Pts, chkP5Ast, chkP5Reb, chkP53, chkP5Blk, chkP5Stl, chkP5PA,chkP5PR, chkP5AR, chkP5PAR,
                chkP6Pts, chkP6Ast, chkP6Reb, chkP63, chkP6Blk, chkP6Stl, chkP6PA,chkP6PR, chkP6AR, chkP6PAR,
                chkP7Pts, chkP7Ast, chkP7Reb, chkP73, chkP7Blk, chkP7Stl, chkP7PA,chkP7PR, chkP7AR, chkP7PAR,
                chkP8Pts, chkP8Ast, chkP8Reb, chkP83, chkP8Blk, chkP8Stl, chkP8PA,chkP8PR, chkP8AR, chkP8PAR
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
                        foreach (DropDownList team in teams)
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
            using (SqlCommand querySearch = new SqlCommand("LoadParlays"))
            {
                querySearch.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                {
                    querySearch.Connection = busDriver.SQLdb;
                    sDataSearch.SelectCommand = querySearch;
                    using (DataSet dataT = new DataSet())
                    {
                        try
                        {
                            sDataSearch.Fill(dataT);
                        }
                        catch (SqlException)
                        {
                            lblError.Text = "No games meet the requested criteria";
                        }

                        ddlLoad.Items.Clear();
                        ListItem emptyItem = new ListItem("Saved Props", "");
                        ddlLoad.Items.Add(emptyItem);

                        foreach (DataRow row in dataT.Tables[0].Rows)
                        {
                            // Build the text for the dropdown item
                            string date = row["SaveDate"].ToString() + ": ";
                            string p1 = row["p1"].ToString();
                            string p2 = row["p2"]?.ToString();
                            string p3 = row["p3"]?.ToString();
                            string p4 = row["p4"]?.ToString();
                            string p5 = row["p5"]?.ToString();
                            string p6 = row["p6"]?.ToString();
                            string p7 = row["p7"]?.ToString();
                            string p8 = row["p8"]?.ToString();

                            // Concatenate the fields into the desired format
                            string displayText = date + p1.Split(' ')[1];

                            if (!string.IsNullOrWhiteSpace(p2)) displayText += ", " + p2.Split(' ')[1];
                            if (!string.IsNullOrWhiteSpace(p3)) displayText += ", " + p3.Split(' ')[1];
                            if (!string.IsNullOrWhiteSpace(p4)) displayText += ", " + p4.Split(' ')[1];
                            if (!string.IsNullOrWhiteSpace(p5)) displayText += ", " + p5.Split(' ')[1];
                            if (!string.IsNullOrWhiteSpace(p6)) displayText += ", " + p6.Split(' ')[1];
                            if (!string.IsNullOrWhiteSpace(p7)) displayText += ", " + p7.Split(' ')[1];
                            if (!string.IsNullOrWhiteSpace(p8)) displayText += ", " + p8.Split(' ')[1];

                            // Use "ID" as the DataValueField
                            string dataValue = row["ID"].ToString();

                            // Add each item to the dropdown list
                            ddlLoad.Items.Add(new ListItem(displayText, dataValue));
                        }
                    }
                }
            }
        }


        protected void PopulateUI(List<DropDownList> roster, List<Control> statSection)
        {
            for (int i = 0; i < roster.Count; i++)
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
        }

        protected void ddTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartingLineups startingLineups = new StartingLineups();
            startingLineups.GetStarters(Int32.Parse(ddTeams.SelectedItem.Value), hName, hPGName, hSGName, hSFName, hPFName, hCName, aName, aPGName, aSGName, aSFName, aPFName, aCName, time);
            if(hName.Text != "")
            {
                hpg.Visible = true;
                hsg.Visible = true;
                hsf.Visible = true;
                hpf.Visible = true;
                hc.Visible = true;
                apg.Visible = true;
                asg.Visible = true;
                asf.Visible = true;
                apf.Visible = true;
                ac.Visible = true;
            }
            string team = ddTeams.SelectedItem.Text;
            try
            {
                team = team.Split(' ')[3].ToLower();
            }
            catch
            {
                team = team.Split(' ')[2].ToLower();
            }
            if(team == "76ers")
            {
                team = "sixers";
            }
            teamNotes.Visible = true;
            Session["team"] = team;
            //string savepath = "C:\\Users\\derfj\\Desktop\\NBAdb\\NBAdb\\Game Notes\\";
            //string fullPath = savepath + team + " - " + DateTime.Today.Month + "." + DateTime.Today.Day + "." + DateTime.Today.Year + ".pdf";

            //if (File.Exists(fullPath))
            //{
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("Content-Disposition", "inline; filename=" + team + " - " + DateTime.Today.Month + "." + DateTime.Today.Day + "." + DateTime.Today.Year + ".pdf");
            //    Response.WriteFile(fullPath);
            //    Response.End();
            //}

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
            Label20.Visible = true;
            Label21.Visible = true;
            Label22.Visible = true;
            Label27.Visible = true;
            Label28.Visible = true;

            opAs.Visible = true;
            opRe.Visible = true;
            opODRe.Visible = true;
            opSt.Visible = true;
            opF.Visible = true;
            opBenc.Visible = true;
            opFGth.Visible = true;
            opFGtw.Visible = true;

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
                if (SeasonChange == 1)
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
            if (!ddTeams.SelectedItem.Text.IsNullOrWhiteSpace() && ddTeams.SelectedItem.Text != "Team")
            {
                opName.Visible = true;
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
                try
                {
                    busDriver.SQLdb.Open();
                }
                catch (InvalidOperationException e)
                {
                    busDriver.SQLdb.Close();
                    busDriver.SQLdb.Open();
                }
                using (SqlDataReader sdr = querySearch.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        t1Score.Text = sdr["points"].ToString();
                        t1ScoreR.Text = "(" + sdr["pointsRank"].ToString() + ")";
                        t1ScoreAgainst.Text = sdr["pointsAgainst"].ToString();
                        t1ScoreAgainstR.Text = "(" + sdr["pointsAgainstRank"].ToString() + ")";
                        t1FG.Text = sdr["twoPointersMade"].ToString() + "/" + sdr["twoPointersAttempted"].ToString();
                        t1FGR.Text = "(" + sdr["twoPointersMadeRank"].ToString() + ")/(" + sdr["twoPointersAttemptedRank"].ToString() + ")";
                        t1FG3.Text = sdr["threePointersMade"].ToString() + "/" + sdr["threePointersAttempted"].ToString();
                        t1FG3R.Text = "(" + sdr["threePointersMadeRank"].ToString() + ")/(" + sdr["threePointersAttemptedRank"].ToString() + ")";
                        t1Ast.Text = sdr["assists"].ToString();
                        t1AstR.Text = "(" + sdr["assistsRank"].ToString() + ")";
                        t1Reb.Text = sdr["reboundsPersonal"].ToString();
                        t1RebR.Text = "(" + sdr["reboundsPersonalRank"].ToString() + ")";
                        t1Bench.Text = sdr["benchPoints"].ToString();
                        t1BenchR.Text = "(" + sdr["benchPointsRank"].ToString() + ")";
                        t1Q1.Text = sdr["q1Points"].ToString() + "/" + sdr["q1PointsAgainst"].ToString();
                        t1Q1R.Text = "(" + sdr["q1PointsRank"].ToString() + ")/(" + sdr["q1PointsAgainstRank"].ToString() + ")";
                        t1Q2.Text = sdr["q2Points"].ToString() + "/" + sdr["q2PointsAgainst"].ToString();
                        t1Q2R.Text = "(" + sdr["q2PointsRank"].ToString() + ")/(" + sdr["q2PointsAgainstRank"].ToString() + ")";
                        t1Q3.Text = sdr["q3Points"].ToString() + "/" + sdr["q3PointsAgainst"].ToString();
                        t1Q3R.Text = "(" + sdr["q3PointsRank"].ToString() + ")/(" + sdr["q3PointsAgainstRank"].ToString() + ")";
                        t1Q4.Text = sdr["q4Points"].ToString() + "/" + sdr["q4PointsAgainst"].ToString();
                        t1Q4R.Text = "(" + sdr["q4PointsRank"].ToString() + ")/(" + sdr["q4PointsAgainstRank"].ToString() + ")";
                        t1Blks.Text = sdr["blocks"].ToString() + "/" + sdr["blocksReceived"].ToString();
                        t1BlksR.Text = "(" + sdr["blocksRank"].ToString() + ")/(" + sdr["blocksReceivedRank"].ToString() + ")";
                        t1FT.Text = sdr["freeThrowsMade"].ToString() + "/" + sdr["freeThrowsAttempted"].ToString();
                        t1FTR.Text = "(" + sdr["freeThrowsMadeRank"].ToString() + ")/(" + sdr["freeThrowsAttemptedRank"].ToString() + ")";
                        t1Stl.Text = sdr["steals"].ToString() + "/" + sdr["turnovers"].ToString();
                        t1StlR.Text = "(" + sdr["stealsRank"].ToString() + ")/(" + sdr["turnoversRank"].ToString() + ")";
                        t1ODReb.Text = sdr["reboundsOffensive"].ToString() + "/" + sdr["reboundsDefensive"].ToString();
                        t1ODRebR.Text = "(" + sdr["reboundsOffensiveRank"].ToString() + ")/(" + sdr["reboundsDefensiveRank"].ToString() + ")";

                        opAst.Text = sdr["opAssists"].ToString();
                        opAstR.Text = "(" + sdr["opAssistsRank"].ToString() + ")";
                        opReb.Text = sdr["opReboundsPersonal"].ToString();
                        opRebR.Text = "(" + sdr["opReboundsPersonalRank"].ToString() + ")";
                        opODReb.Text = sdr["opReboundsOffensive"].ToString() + "/" + sdr["opReboundsDefensive"].ToString();
                        opODRebR.Text = "(" + sdr["opReboundsOffensiveRank"].ToString() + ")/(" + sdr["opReboundsDefensiveRank"].ToString() + ")";
                        opFT.Text = sdr["opFreeThrowsMade"].ToString() + "/" + sdr["opFreeThrowsAttempted"].ToString();
                        opFTR.Text = "(" + sdr["opFreeThrowsMadeRank"].ToString() + ")/(" + sdr["opFreeThrowsAttemptedRank"].ToString() + ")";
                        opStl.Text = sdr["opSteals"].ToString() + "/" + sdr["opTurnovers"].ToString();
                        opStlR.Text = "(" + sdr["opStealsRank"].ToString() + ")/(" + sdr["opTurnoversRank"].ToString() + ")";
                        opBench.Text = sdr["opBenchPoints"].ToString();
                        opBenchR.Text = "(" + sdr["opBenchPointsRank"].ToString() + ")";
                        opFG.Text = sdr["opTwoPointersMade"].ToString() + "/" + sdr["opTwoPointersAttempted"].ToString();
                        opFGR.Text = "(" + sdr["opTwoPointersMadeRank"].ToString() + ")/(" + sdr["opTwoPointersAttemptedRank"].ToString() + ")";
                        opFG3.Text = sdr["opThreePointersMade"].ToString() + "/" + sdr["opThreePointersAttempted"].ToString();
                        opFG3R.Text = "(" + sdr["opThreePointersMadeRank"].ToString() + ")/(" + sdr["opThreePointersAttemptedRank"].ToString() + ")";



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
            if (ddlRoster.SelectedItem.Text != "Player")
            {
                txtP1Pts.Enabled = true; chkP1Pts.Enabled = true;
                txtP1Ast.Enabled = true; chkP1Ast.Enabled = true;
                txtP1Reb.Enabled = true; chkP1Reb.Enabled = true;
                txtP13.Enabled = true; chkP13.Enabled = true;
                txtP1Blk.Enabled = true; chkP1Blk.Enabled = true;
                txtP1Stl.Enabled = true; chkP1Stl.Enabled = true;
                txtP1PA.Enabled = true; chkP1PA.Enabled = true;
                txtP1PR.Enabled = true; chkP1PR.Enabled = true;
                txtP1AR.Enabled = true; chkP1AR.Enabled = true;
                txtP1PAR.Enabled = true; chkP1PAR.Enabled = true;
                p1Stats.Visible = true;
                p1Link.NavigateUrl = "https://www.nba.com/stats/player/" + ddlRoster.SelectedItem.Value + "/boxscores-traditional";
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p1Name, p1Games, p1Pts, p1Ast, p1Reb, p1FG, p13, p1ft, p1Blk, p1Stl, p1PtsExt, p1AstExt, p1RebExt, p1FGExt, p13Ext, p1ftExt, p1BlkExt, p1StlExt, p1GamesExt);
            }
            else
            {
                txtP1Pts.Enabled = false; chkP1Pts.Enabled = false;
                txtP1Ast.Enabled = false; chkP1Ast.Enabled = false;
                txtP1Reb.Enabled = false; chkP1Reb.Enabled = false;
                txtP13.Enabled = false; chkP13.Enabled = false;
                txtP1Blk.Enabled = false; chkP1Blk.Enabled = false;
                txtP1Stl.Enabled = false; chkP1Stl.Enabled = false;
                txtP1PA.Enabled = false; chkP1PA.Enabled = false;
                txtP1PR.Enabled = false; chkP1PR.Enabled = false;
                txtP1AR.Enabled = false; chkP1AR.Enabled = false;
                txtP1PAR.Enabled = false; chkP1PAR.Enabled = false;
                p1Link.NavigateUrl = null;
            }
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
                txtP2PA.Enabled = true; chkP2PA.Enabled = true;
                txtP2PR.Enabled = true; chkP2PR.Enabled = true;
                txtP2AR.Enabled = true; chkP2AR.Enabled = true;
                txtP2PAR.Enabled = true; chkP2PAR.Enabled = true;
                p2Stats.Visible = true;
                p2Link.NavigateUrl = "https://www.nba.com/stats/player/" + ddlRoster2.SelectedItem.Value + "/boxscores-traditional";
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
                txtP2PA.Enabled = false; chkP2PA.Enabled = false;
                txtP2PR.Enabled = false; chkP2PR.Enabled = false;
                txtP2AR.Enabled = false; chkP2AR.Enabled = false;
                txtP2PAR.Enabled = false; chkP2PAR.Enabled = false;
                p2Link.NavigateUrl = null;
            }
            //foreach (DropDownList roster in t1FullRoster)
            //{
            //    if (roster.ID != "ddlRoster2")
            //    {
            //        if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
            //        {
            //            PopulateRoster(roster, ddTeams.SelectedValue);
            //        }
            //        //if (roster.Items.Contains(ddlRoster2.SelectedItem) && ddlRoster2.SelectedItem.Text != "Player")
            //        //{
            //        //    roster.Items.Remove(ddlRoster2.SelectedItem);
            //        //}
            //    }
            //}
            //RosterCheck(t1FullRoster);
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
                txtP3PA.Enabled = true; chkP3PA.Enabled = true;
                txtP3PR.Enabled = true; chkP3PR.Enabled = true;
                txtP3AR.Enabled = true; chkP3AR.Enabled = true;
                txtP3PAR.Enabled = true; chkP3PAR.Enabled = true;
                p3Stats.Visible = true;
                p3Link.NavigateUrl = "https://www.nba.com/stats/player/" + ddlRoster3.SelectedItem.Value + "/boxscores-traditional";
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
                txtP3PA.Enabled = false; chkP3PA.Enabled = false;
                txtP3PR.Enabled = false; chkP3PR.Enabled = false;
                txtP3AR.Enabled = false; chkP3AR.Enabled = false;
                txtP3PAR.Enabled = false; chkP3PAR.Enabled = false;
                p3Link.NavigateUrl = null;
            }
            //foreach (DropDownList roster in t1FullRoster)
            //{
            //    if (roster.ID != "ddlRoster3")
            //    {
            //        if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
            //        {
            //            PopulateRoster(roster, ddTeams.SelectedValue);
            //        }
            //        //if (roster.Items.Contains(ddlRoster3.SelectedItem) && ddlRoster3.SelectedItem.Text != "Player")
            //        //{
            //        //    roster.Items.Remove(ddlRoster3.SelectedItem);
            //        //}
            //    }
            //}
            //RosterCheck(t1FullRoster);
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
                txtP4PA.Enabled = true; chkP4PA.Enabled=true;
                txtP4PR.Enabled = true; chkP4PR.Enabled = true;
                txtP4AR.Enabled = true; chkP4AR.Enabled = true;
                txtP4PAR.Enabled = true; chkP4PAR.Enabled = true;

                p4Stats.Visible = true;
                p4Link.NavigateUrl = "https://www.nba.com/stats/player/" + ddlRoster4.SelectedItem.Value + "/boxscores-traditional";
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
                txtP4PA.Enabled = false; chkP4PA.Enabled = false;
                txtP4PR.Enabled = false; chkP4PR.Enabled = false;
                txtP4AR.Enabled = false; chkP4AR.Enabled = false;
                txtP4PAR.Enabled = false; chkP4PAR.Enabled = false;
                p4Link.NavigateUrl = null;
            }
            //foreach (DropDownList roster in t1FullRoster)
            //{
            //    if (roster.ID != "ddlRoster4")
            //    {
            //        if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
            //        {
            //            PopulateRoster(roster, ddTeams.SelectedValue);
            //        }
            //        //if (roster.Items.Contains(ddlRoster4.SelectedItem) && ddlRoster4.SelectedItem.Text != "Player")
            //        //{
            //        //    roster.Items.Remove(ddlRoster4.SelectedItem);
            //        //}
            //    }
            //}
            //RosterCheck(t1FullRoster);
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
                txtP5PA.Enabled = true; chkP5PA.Enabled = true;
                txtP5PR.Enabled = true; chkP5PR.Enabled = true;
                txtP5AR.Enabled = true; chkP5AR.Enabled = true;
                txtP5PAR.Enabled = true; chkP5PAR.Enabled = true;
                p5Stats.Visible = true;
                p5Link.NavigateUrl = "https://www.nba.com/stats/player/" + ddlRoster5.SelectedItem.Value + "/boxscores-traditional";
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
                txtP5PA.Enabled = false; chkP5PA.Enabled = false;
                txtP5PR.Enabled = false; chkP5PR.Enabled = false;
                txtP5AR.Enabled = false; chkP5AR.Enabled = false;
                txtP5PAR.Enabled = false; chkP5PAR.Enabled = false;
                p5Link.NavigateUrl = null;
            }
            //foreach (DropDownList roster in t1FullRoster)
            //{
            //    if (roster.ID != "ddlRoster5")
            //    {
            //        if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
            //        {
            //            PopulateRoster(roster, ddTeams.SelectedValue);
            //        }
            //    }
            //}
            //RosterCheck(t1FullRoster);
        }

        protected void ddlRoster6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoster6.SelectedItem.Text != "Player")
            {
                txtP6Pts.Enabled = true; chkP6Pts.Enabled = true;
                txtP6Ast.Enabled = true; chkP6Ast.Enabled = true;
                txtP6Reb.Enabled = true; chkP6Reb.Enabled = true;
                txtP63.Enabled = true; chkP63.Enabled = true;
                txtP6Blk.Enabled = true; chkP6Blk.Enabled = true;
                txtP6Stl.Enabled = true; chkP6Stl.Enabled = true;
                txtP6PA.Enabled = true; chkP6PA.Enabled = true;
                txtP6PR.Enabled = true; chkP6PR.Enabled = true;
                txtP6AR.Enabled = true; chkP6AR.Enabled = true;
                txtP6PAR.Enabled = true; chkP6PAR.Enabled = true;
                p6Stats.Visible = true;
                p6Link.NavigateUrl = "https://www.nba.com/stats/player/" + ddlRoster6.SelectedItem.Value + "/boxscores-traditional";
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster6.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p6Name, p6Games, p6Pts, p6Ast, p6Reb, p6FG, p63, p6ft, p6Blk, p6Stl, p6PtsExt, p6AstExt, p6RebExt, p6FGExt, p63Ext, p6ftExt, p6BlkExt, p6StlExt, p6GamesExt);
            }
            else
            {
                txtP6Pts.Enabled = false; chkP6Pts.Enabled = false;
                txtP6Ast.Enabled = false; chkP6Ast.Enabled = false;
                txtP6Reb.Enabled = false; chkP6Reb.Enabled = false;
                txtP63.Enabled = false; chkP63.Enabled = false;
                txtP6Blk.Enabled = false; chkP6Blk.Enabled = false;
                txtP6Stl.Enabled = false; chkP6Stl.Enabled = false;
                txtP6PA.Enabled = false; chkP6PA.Enabled = false;
                txtP6PR.Enabled = false; chkP6PR.Enabled = false;
                txtP6AR.Enabled = false; chkP6AR.Enabled = false;
                txtP6PAR.Enabled = false; chkP6PAR.Enabled = false;
                p6Link.NavigateUrl = null;
            }
            //foreach (DropDownList roster in t1FullRoster)
            //{
            //    if (roster.ID != "ddlRoster6")
            //    {
            //        if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
            //        {
            //            PopulateRoster(roster, ddTeams.SelectedValue);
            //        }
            //    }
            //}
            //RosterCheck(t1FullRoster);
        }

        protected void ddlRoster7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoster7.SelectedItem.Text != "Player")
            {
                txtP7Pts.Enabled = true; chkP7Pts.Enabled = true;
                txtP7Ast.Enabled = true; chkP7Ast.Enabled = true;
                txtP7Reb.Enabled = true; chkP7Reb.Enabled = true;
                txtP73.Enabled = true; chkP73.Enabled = true;
                txtP7Blk.Enabled = true; chkP7Blk.Enabled = true;
                txtP7Stl.Enabled = true; chkP7Stl.Enabled = true;
                txtP7PA.Enabled = true; chkP7PA.Enabled = true;
                txtP7PR.Enabled = true; chkP7PR.Enabled = true;
                txtP7AR.Enabled = true; chkP7AR.Enabled = true;
                txtP7PAR.Enabled = true; chkP7PAR.Enabled = true;
                p7Stats.Visible = true;
                p7Link.NavigateUrl = "https://www.nba.com/stats/player/" + ddlRoster7.SelectedItem.Value + "/boxscores-traditional";
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster7.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p7Name, p7Games, p7Pts, p7Ast, p7Reb, p7FG, p73, p7ft, p7Blk, p7Stl, p7PtsExt, p7AstExt, p7RebExt, p7FGExt, p73Ext, p7ftExt, p7BlkExt, p7StlExt, p7GamesExt);
            }
            else
            {
                txtP7Pts.Enabled = false; chkP7Pts.Enabled = false;
                txtP7Ast.Enabled = false; chkP7Ast.Enabled = false;
                txtP7Reb.Enabled = false; chkP7Reb.Enabled = false;
                txtP73.Enabled = false; chkP73.Enabled = false;
                txtP7Blk.Enabled = false; chkP7Blk.Enabled = false;
                txtP7Stl.Enabled = false; chkP7Stl.Enabled = false;
                txtP7PA.Enabled = false; chkP7PA.Enabled = false;
                txtP7PR.Enabled = false; chkP7PR.Enabled = false;
                txtP7AR.Enabled = false; chkP7AR.Enabled = false;
                txtP7PAR.Enabled = false; chkP7PAR.Enabled = false;
                p7Link.NavigateUrl = null;
            }
            //foreach (DropDownList roster in t1FullRoster)
            //{
            //    if (roster.ID != "ddlRoster7")
            //    {
            //        if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
            //        {
            //            PopulateRoster(roster, ddTeams.SelectedValue);
            //        }
            //    }
            //}
            //RosterCheck(t1FullRoster);
        }

        protected void ddlRoster8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRoster8.SelectedItem.Text != "Player")
            {
                txtP8Pts.Enabled = true; chkP8Pts.Enabled = true;
                txtP8Ast.Enabled = true; chkP8Ast.Enabled = true;
                txtP8Reb.Enabled = true; chkP8Reb.Enabled = true;
                txtP83.Enabled = true; chkP83.Enabled = true;
                txtP8Blk.Enabled = true; chkP8Blk.Enabled = true;
                txtP8Stl.Enabled = true; chkP8Stl.Enabled = true;
                txtP8PA.Enabled = true; chkP8PA.Enabled = true;
                txtP8PR.Enabled = true; chkP8PR.Enabled = true;
                txtP8AR.Enabled = true; chkP8AR.Enabled = true;
                txtP8PAR.Enabled = true; chkP8PAR.Enabled = true;
                p8Stats.Visible = true;
                p8Link.NavigateUrl = "https://www.nba.com/stats/player/" + ddlRoster8.SelectedItem.Value + "/boxscores-traditional";
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster8.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p8Name, p8Games, p8Pts, p8Ast, p8Reb, p8FG, p83, p8ft, p8Blk, p8Stl, p8PtsExt, p8AstExt, p8RebExt, p8FGExt, p83Ext, p8ftExt, p8BlkExt, p8StlExt, p8GamesExt);
            }
            else
            {
                txtP8Pts.Enabled = false; chkP8Pts.Enabled = false;
                txtP8Ast.Enabled = false; chkP8Ast.Enabled = false;
                txtP8Reb.Enabled = false; chkP8Reb.Enabled = false;
                txtP83.Enabled = false; chkP83.Enabled = false;
                txtP8Blk.Enabled = false; chkP8Blk.Enabled = false;
                txtP8Stl.Enabled = false; chkP8Stl.Enabled = false;
                txtP8PA.Enabled = false; chkP8PA.Enabled = false;
                txtP8PR.Enabled = false; chkP8PR.Enabled = false;
                txtP8AR.Enabled = false; chkP8AR.Enabled = false;
                txtP8PAR.Enabled = false; chkP8PAR.Enabled = false;
                p8Link.NavigateUrl = null;
            }
            //foreach (DropDownList roster in t1FullRoster)
            //{
            //    if (roster.ID != "ddlRoster8")
            //    {
            //        if (roster.SelectedItem.Text == "Player" || roster.SelectedItem.Text == "DNP Player")
            //        {
            //            PopulateRoster(roster, ddTeams.SelectedValue);
            //        }
            //    }
            //}
            //RosterCheck(t1FullRoster);
        }

        public void GetAverages(string procedure, int player, int team, int season, Label name, Label games, Label pts, Label ast, Label reb, Label fg, Label fg3, Label ft, Label blk, Label stl, Label ptsE, Label astE, Label rebE, Label fgE, Label fg3E, Label ftE, Label blkE, Label stlE, Label gamesE)
        {
            using (SqlCommand PlayerSearch = new SqlCommand(procedure))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@player", player);
                PlayerSearch.Parameters.AddWithValue("@team", team);
                PlayerSearch.Parameters.AddWithValue("@season", season);
                if (procedure == "ToolboxAveragesWL")
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
                    try
                    {
                        busDriver.SQLdb.Open();
                    }
                    catch (InvalidOperationException e)
                    {
                        busDriver.SQLdb.Close();
                        busDriver.SQLdb.Open();
                    }
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    while (reader.Read())
                    {
                        name.Text = reader["Name"].ToString();
                        games.Text = reader["Games"] + "GP, " + reader["Minutes"] + "min/game";
                        gamesE.Text = reader["TrendMinutes"] + "min";
                        pts.Text = reader["Points"].ToString();

                        ast.Text = reader["Assists"].ToString();
                        reb.Text = reader["Rebounds"].ToString();
                        fg.Text = reader["FG2M"] + "/" + reader["FG2A"] + " - " + reader["FG2%"] + "%";
                        fg3.Text = reader["FG3M"] + "/" + reader["FG3A"] + " - " + reader["FG3%"] + "%";
                        ft.Text = reader["FTM"] + "/" + reader["FTA"] + " - " + reader["FT%"] + "%";
                        blk.Text = reader["Blocks"].ToString();
                        stl.Text = reader["Steals"].ToString();


                        ptsE.Text = reader["TrendPoints"].ToString();
                        astE.Text = reader["TrendAssists"].ToString();
                        rebE.Text = reader["TrendRebounds"].ToString();
                        blkE.Text = reader["TrendBlocks"].ToString();
                        stlE.Text = reader["TrendSteals"].ToString();
                        fgE.Text = reader["TrendFG2M"] + "/" + reader["TrendFG2A"] + " - " + reader["TrendFG2%"] + "%";
                        fg3E.Text = reader["TrendFG3M"] + "/" + reader["TrendFG3A"] + " - " + reader["TrendFG3%"] + "%";
                        ftE.Text = reader["TrendFTM"] + "/" + reader["TrendFTA"] + " - " + reader["TrendFT%"] + "%";


                        TrendColoring(games, gamesE, float.Parse(reader["diffMinutes"].ToString()), float.Parse(reader["MinDeviation"].ToString()));
                        TrendColoring(pts, ptsE, float.Parse(reader["diffPoints"].ToString()), float.Parse(reader["PtsDeviation"].ToString()));
                        TrendColoring(ast, astE, float.Parse(reader["diffAssists"].ToString()), float.Parse(reader["AstDeviation"].ToString()));
                        TrendColoring(reb, rebE, float.Parse(reader["diffRebounds"].ToString()), float.Parse(reader["RebDeviation"].ToString()));
                        TrendColoring(blk, blkE, float.Parse(reader["diffBlocks"].ToString()), float.Parse(reader["BlkDeviation"].ToString()));
                        TrendColoring(stl, stlE, float.Parse(reader["diffSteals"].ToString()), float.Parse(reader["StlDeviation"].ToString()));



                        TrendFieldGoalColoring(fg, fgE, float.Parse(reader["diffFG2M"].ToString()), float.Parse(reader["diffFG2A"].ToString()), float.Parse(reader["FG2MDeviation"].ToString()), float.Parse(reader["FG2ADeviation"].ToString()));
                        TrendFieldGoalColoring(fg3, fg3E, float.Parse(reader["diffFG3M"].ToString()), float.Parse(reader["diffFG3A"].ToString()), float.Parse(reader["FG3MDeviation"].ToString()), float.Parse(reader["FG3ADeviation"].ToString()));
                        TrendFieldGoalColoring(ft, ftE, float.Parse(reader["diffFTM"].ToString()), float.Parse(reader["diffFTA"].ToString()), float.Parse(reader["FTMDeviation"].ToString()), float.Parse(reader["FTADeviation"].ToString()));

                        fg3E.Attributes.Add("style", "font-size: 12px; width:fit-content; padding: 0px 0px 0px 5px");
                        fgE.Attributes.Add("style", "font-size: 12px; width:fit-content; padding: 0px 0px 0px 5px");
                        ftE.Attributes.Add("style", "font-size: 12px; width:fit-content; padding: 0px 0px 0px 5px");


                    }
                    busDriver.SQLdb.Close();
                }
            }
        }

        public void TrendColoring(Label prop, Label trend, float difference, float deviation)
        {
            if (difference > 0)
            {
                if (difference >= deviation)
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




            float made = float.Parse(prop.Text.Split('/')[0]);
            float attempted = float.Parse(prop.Text.Split('/')[1].Split(' ')[0]);
            float pct = float.Parse(prop.Text.Split(' ')[2].Split('%')[0]);


            float madeT = float.Parse(trend.Text.Split('/')[0]);
            float attemptedT = float.Parse(trend.Text.Split('/')[1].Split(' ')[0]);
            float pctT = float.Parse(trend.Text.Split(' ')[2].Split('%')[0]);


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
            if (!ddlRoster6.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster6.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p6Name, p6Games, p6Pts, p6Ast, p6Reb, p6FG, p63, p6ft, p6Blk, p6Stl, p6PtsExt, p6AstExt, p6RebExt, p6FGExt, p63Ext, p6ftExt, p6BlkExt, p6StlExt, p6GamesExt);
            }
            if (!ddlRoster7.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster7.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p7Name, p7Games, p7Pts, p7Ast, p7Reb, p7FG, p73, p7ft, p7Blk, p7Stl, p7PtsExt, p7AstExt, p7RebExt, p7FGExt, p73Ext, p7ftExt, p7BlkExt, p7StlExt, p7GamesExt);
            }
            if (!ddlRoster8.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster8.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p8Name, p8Games, p8Pts, p8Ast, p8Reb, p8FG, p83, p8ft, p8Blk, p8Stl, p8PtsExt, p8AstExt, p8RebExt, p8FGExt, p83Ext, p8ftExt, p8BlkExt, p8StlExt, p8GamesExt);
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
            if (!ddlRoster6.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster6.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p6Name, p6Games, p6Pts, p6Ast, p6Reb, p6FG, p63, p6ft, p6Blk, p6Stl, p6PtsExt, p6AstExt, p6RebExt, p6FGExt, p63Ext, p6ftExt, p6BlkExt, p6StlExt, p6GamesExt);
            }
            if (!ddlRoster7.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster7.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p7Name, p7Games, p7Pts, p7Ast, p7Reb, p7FG, p73, p7ft, p7Blk, p7Stl, p7PtsExt, p7AstExt, p7RebExt, p7FGExt, p73Ext, p7ftExt, p7BlkExt, p7StlExt, p7GamesExt);
            }
            if (!ddlRoster8.SelectedItem.Value.IsNullOrWhiteSpace())
            {
                GetAverages(T1PlayerProcedure, Int32.Parse(ddlRoster8.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p8Name, p8Games, p8Pts, p8Ast, p8Reb, p8FG, p83, p8ft, p8Blk, p8Stl, p8PtsExt, p8AstExt, p8RebExt, p8FGExt, p83Ext, p8ftExt, p8BlkExt, p8StlExt, p8GamesExt);
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
            if (chkT1Dynamic.Checked)
            {
                int players = 0;
                int DNPs = 0;
                foreach (DropDownList roster in t1Rosters)
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
            List<TextBox> p6Props = new List<TextBox>();
            List<TextBox> p7Props = new List<TextBox>();
            List<TextBox> p8Props = new List<TextBox>();

            int player1 = 0;
            int player2 = 0;
            int player3 = 0;
            int player4 = 0;
            int player5 = 0;
            int player6 = 0;
            int player7 = 0;
            int player8 = 0;

            int dnp1 = 0;
            int dnp2 = 0;
            int dnp3 = 0;

            string select = "select count(distinct p.game_id) Games ";
            string selectInfo = "select distinct p.game_id, (select date from game where p.game_id = game_id and p.season_id = season_id) Date ";
            string from = "from playerBox p inner join teamBox t on p.game_id = t.game_id and p.team_id = t.team_id and p.season_id = t.season_id";
            string where = "where p.season_id = " + ddSeason.SelectedItem.Value + " and p.team_id = " + ddTeams.SelectedItem.Value;
            string whereAbove = "where p.season_id = " + ddSeason.SelectedItem.Value + " and p.team_id = " + ddTeams.SelectedItem.Value;


            //and t.points > t.pointsAgainst

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
                if (i == 0)
                {
                    foreach (TextBox prop in p1)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            p1Props.Add(prop);
                        }
                    }
                    player1 = Int32.Parse(t1Players[0].SelectedItem.Value);
                    where += " and p.status = 'ACTIVE' and replace(replace(p.minutesCalculated, 'PT', ''), 'M', '') >  (select cast(Minutes as decimal(18, 2))/2 from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id) and p.player_id = " + player1;
                    p1Picks.Text = ddlRoster.SelectedItem.Text + ": ";
                }
                else if (i == 1)
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
                    where += " and p2.status = 'ACTIVE' and replace(replace(p2.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p2.season_id and a.team_id = p2.team_id and a.player_id = p2.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p2.season_id and a.team_id = p2.team_id and a.player_id = p2.player_id)/2) and p2.player_id = " + player2;
                    p2Picks.Text = ddlRoster2.SelectedItem.Text + ": ";
                }
                else if (i == 2)
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
                    where += " and p3.status = 'ACTIVE' and replace(replace(p3.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p3.season_id and a.team_id = p3.team_id and a.player_id = p3.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p3.season_id and a.team_id = p3.team_id and a.player_id = p3.player_id)/2) and p3.player_id = " + player3;
                    p3Picks.Text = ddlRoster3.SelectedItem.Text + ": ";
                }
                else if (i == 3)
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
                    where += " and p4.status = 'ACTIVE' and replace(replace(p4.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p4.season_id and a.team_id = p4.team_id and a.player_id = p4.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p4.season_id and a.team_id = p4.team_id and a.player_id = p4.player_id)/2) and p4.player_id = " + player4;
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
                    where += " and p5.status = 'ACTIVE' and replace(replace(p5.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p5.season_id and a.team_id = p5.team_id and a.player_id = p5.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p5.season_id and a.team_id = p5.team_id and a.player_id = p5.player_id)/2) and p5.player_id = " + player5;
                    p5Picks.Text = ddlRoster5.SelectedItem.Text + ": ";
                }
                else if (i == 5)
                {
                    foreach (TextBox prop in p6)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            p6Props.Add(prop);
                        }
                    }
                    player6 = Int32.Parse(t1Players[5].SelectedItem.Value);
                    from += " inner join playerBox p6 on p.game_id = p6.game_id and p.team_id = p6.team_id and p.season_id = p6.season_id";
                    where += " and p6.status = 'ACTIVE' and replace(replace(p6.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p6.season_id and a.team_id = p6.team_id and a.player_id = p6.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p6.season_id and a.team_id = p6.team_id and a.player_id = p6.player_id)/2) and p6.player_id = " + player6;
                    p6Picks.Text = ddlRoster6.SelectedItem.Text + ": ";
                }
                else if (i == 6)
                {
                    foreach (TextBox prop in p7)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            p7Props.Add(prop);
                        }
                    }
                    player7 = Int32.Parse(t1Players[6].SelectedItem.Value);
                    from += " inner join playerBox p7 on p.game_id = p7.game_id and p.team_id = p7.team_id and p.season_id = p7.season_id";
                    where += " and p7.status = 'ACTIVE' and replace(replace(p7.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p7.season_id and a.team_id = p7.team_id and a.player_id = p7.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p7.season_id and a.team_id = p7.team_id and a.player_id = p7.player_id)/2) and p7.player_id = " + player7;
                    p7Picks.Text = ddlRoster7.SelectedItem.Text + ": ";
                }
                else if (i == 7)
                {
                    foreach (TextBox prop in p8)
                    {
                        if (!prop.Text.IsNullOrWhiteSpace())
                        {
                            p8Props.Add(prop);
                        }
                    }
                    player8 = Int32.Parse(t1Players[7].SelectedItem.Value);
                    from += " inner join playerBox p8 on p.game_id = p8.game_id and p.team_id = p8.team_id and p.season_id = p8.season_id";
                    where += " and p8.status = 'ACTIVE' and replace(replace(p8.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p8.season_id and a.team_id = p8.team_id and a.player_id = p8.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p8.season_id and a.team_id = p8.team_id and a.player_id = p8.player_id)/2) and p8.player_id = " + player8;
                    p8Picks.Text = ddlRoster8.SelectedItem.Text + ": ";
                }
            }
            for (int i = 0; i < DNPs; i++)
            {
                if (i == 0)
                {
                    dnp1 = Int32.Parse(t1DNPs[0].SelectedItem.Value);
                    from += " left join playerbox d on p.game_id = d.game_id and p.team_id = d.team_id and p.season_id = d.season_id";
                    where += " and (d.status != 'ACTIVE' or replace(replace(d.minutesCalculated, 'PT', ''), 'M', '') < (select Minutes from playerBoxAverage a where a.season_id = d.season_id and a.team_id = d.team_id and a.player_id = d.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = d.season_id and a.team_id = d.team_id and a.player_id = d.player_id)/2)) and d.player_id = " + dnp1;
                    DNP1.Text = "DNP: " + t1DNPs[0].SelectedItem.Text;
                }
                else if (i == 1)
                {
                    dnp2 = Int32.Parse(t1DNPs[1].SelectedItem.Value);
                    from += " left join playerbox d2 on p.game_id = d2.game_id and p.team_id = d2.team_id and p.season_id = d2.season_id";
                    where += " and (d2.status != 'ACTIVE' or replace(replace(d2.minutesCalculated, 'PT', ''), 'M', '') <  (select Minutes from playerBoxAverage a where a.season_id = d2.season_id and a.team_id = d2.team_id and a.player_id = d2.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = d2.season_id and a.team_id = d2.team_id and a.player_id = d2.player_id)/2)) and d2.player_id = " + dnp2;
                    DNP2.Text = "DNP: " + t1DNPs[1].SelectedItem.Text;
                }
                else if (i == 2)
                {
                    dnp3 = Int32.Parse(t1DNPs[2].SelectedItem.Value);
                    from += " left join playerbox d3 on p.game_id = d3.game_id and p.team_id = d3.team_id and p.season_id = d3.season_id";
                    where += " and (d3.status != 'ACTIVE' or replace(replace(d3.minutesCalculated, 'PT', ''), 'M', '') < (select Minutes from playerBoxAverage a where a.season_id = d3.season_id and a.team_id = d3.team_id and a.player_id = d3.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = d3.season_id and a.team_id = d3.team_id and a.player_id = d3.player_id)/2)) and d3.player_id = " + dnp3;
                    DNP3.Text = "DNP: " + t1DNPs[2].SelectedItem.Text;
                }
            }
            string query = select + from + " " + where;
            
            string queryInfo = selectInfo + from + " " + where;

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
            if (!txtP1PA.Text.IsNullOrWhiteSpace())
            {
                if (chkP1PA.Checked)
                {
                    p1Picks.Text += "Under " + txtP1PA.Text + " P+A | ";
                    queryAbove += " and p.points + p.assists <= " + txtP1PA.Text;
                }
                else
                {
                    p1Picks.Text += txtP1PA.Text + " P+A | ";
                    queryAbove += " and p.points + p.assists >= " + txtP1PA.Text;
                }
            }
            if (!txtP1PR.Text.IsNullOrWhiteSpace())
            {
                if (chkP1PR.Checked)
                {
                    p1Picks.Text += "Under " + txtP1PR.Text + " P+R | ";
                    queryAbove += " and p.points + p.reboundsTotal <= " + txtP1PR.Text;
                }
                else
                {
                    p1Picks.Text += txtP1PR.Text + " P+R | ";
                    queryAbove += " and p.points + p.reboundsTotal >= " + txtP1PR.Text;
                }
            }
            if (!txtP1AR.Text.IsNullOrWhiteSpace())
            {
                if (chkP1AR.Checked)
                {
                    p1Picks.Text += "Under " + txtP1AR.Text + " A+R | ";
                    queryAbove += " and p.assists + p.reboundsTotal <= " + txtP1AR.Text;
                }
                else
                {
                    p1Picks.Text += txtP1AR.Text + " A+R | ";
                    queryAbove += " and p.assists + p.reboundsTotal >= " + txtP1AR.Text;
                }
            }
            if (!txtP1PAR.Text.IsNullOrWhiteSpace())
            {
                if (chkP1PAR.Checked)
                {
                    p1Picks.Text += "Under " + txtP1PAR.Text + " P+A+R | ";
                    queryAbove += " and p.points + p.assists + p.reboundsTotal <= " + txtP1PAR.Text;
                }
                else
                {
                    p1Picks.Text += txtP1PAR.Text + " P+A+R | ";
                    queryAbove += " and p.points + p.assists + p.reboundsTotal >= " + txtP1PAR.Text;
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
            if (!txtP2PA.Text.IsNullOrWhiteSpace())
            {
                if (chkP2PA.Checked)
                {
                    p2Picks.Text += "Under " + txtP2PA.Text + " P+A | ";
                    queryAbove += " and p2.points + p2.assists <= " + txtP2PA.Text;
                }
                else
                {
                    p2Picks.Text += txtP2PA.Text + " P+A | ";
                    queryAbove += " and p2.points + p2.assists >= " + txtP2PA.Text;
                }
            }
            if (!txtP2PR.Text.IsNullOrWhiteSpace())
            {
                if (chkP2PR.Checked)
                {
                    p2Picks.Text += "Under " + txtP2PR.Text + " P+R | ";
                    queryAbove += " and p2.points + p2.reboundsTotal <= " + txtP2PR.Text;
                }
                else
                {
                    p2Picks.Text += txtP2PR.Text + " P+R | ";
                    queryAbove += " and p2.points + p2.reboundsTotal >= " + txtP2PR.Text;
                }
            }
            if (!txtP2AR.Text.IsNullOrWhiteSpace())
            {
                if (chkP2AR.Checked)
                {
                    p2Picks.Text += "Under " + txtP2AR.Text + " A+R | ";
                    queryAbove += " and p2.assists + p2.reboundsTotal <= " + txtP2AR.Text;
                }
                else
                {
                    p2Picks.Text += txtP2AR.Text + " A+R | ";
                    queryAbove += " and p2.assists + p2.reboundsTotal >= " + txtP2AR.Text;
                }
            }
            if (!txtP2PAR.Text.IsNullOrWhiteSpace())
            {
                if (chkP2PAR.Checked)
                {
                    p2Picks.Text += "Under " + txtP2PAR.Text + " P+A+R | ";
                    queryAbove += " and p2.points + p2.assists + p2.reboundsTotal <= " + txtP2PAR.Text;
                }
                else
                {
                    p2Picks.Text += txtP2PAR.Text + " P+A+R | ";
                    queryAbove += " and p2.points + p2.assists + p2.reboundsTotal >= " + txtP2PAR.Text;
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
            if (!txtP3PA.Text.IsNullOrWhiteSpace())
            {
                if (chkP3PA.Checked)
                {
                    p3Picks.Text += "Under " + txtP3PA.Text + " P+A | ";
                    queryAbove += " and p3.points + p3.assists <= " + txtP3PA.Text;
                }
                else
                {
                    p3Picks.Text += txtP3PA.Text + " P+A | ";
                    queryAbove += " and p3.points + p3.assists >= " + txtP3PA.Text;
                }
            }
            if (!txtP3PR.Text.IsNullOrWhiteSpace())
            {
                if (chkP3PR.Checked)
                {
                    p3Picks.Text += "Under " + txtP3PR.Text + " P+R | ";
                    queryAbove += " and p3.points + p3.reboundsTotal <= " + txtP3PR.Text;
                }
                else
                {
                    p3Picks.Text += txtP3PR.Text + " P+R | ";
                    queryAbove += " and p3.points + p3.reboundsTotal >= " + txtP3PR.Text;
                }
            }
            if (!txtP3AR.Text.IsNullOrWhiteSpace())
            {
                if (chkP3AR.Checked)
                {
                    p3Picks.Text += "Under " + txtP3AR.Text + " A+R | ";
                    queryAbove += " and p3.assists + p3.reboundsTotal <= " + txtP3AR.Text;
                }
                else
                {
                    p3Picks.Text += txtP3AR.Text + " A+R | ";
                    queryAbove += " and p3.assists + p3.reboundsTotal >= " + txtP3AR.Text;
                }
            }
            if (!txtP3PAR.Text.IsNullOrWhiteSpace())
            {
                if (chkP3PAR.Checked)
                {
                    p3Picks.Text += "Under " + txtP3PAR.Text + " P+A+R | ";
                    queryAbove += " and p3.points + p3.assists + p3.reboundsTotal <= " + txtP3PAR.Text;
                }
                else
                {
                    p3Picks.Text += txtP3PAR.Text + " P+A+R | ";
                    queryAbove += " and p3.points + p3.assists + p3.reboundsTotal >= " + txtP3PAR.Text;
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
            if (!txtP4PA.Text.IsNullOrWhiteSpace())
            {
                if (chkP4PA.Checked)
                {
                    p4Picks.Text += "Under " + txtP4PA.Text + " P+A | ";
                    queryAbove += " and p4.points + p4.assists <= " + txtP4PA.Text;
                }
                else
                {
                    p4Picks.Text += txtP4PA.Text + " P+A | ";
                    queryAbove += " and p4.points + p4.assists >= " + txtP4PA.Text;
                }
            }
            if (!txtP4PR.Text.IsNullOrWhiteSpace())
            {
                if (chkP4PR.Checked)
                {
                    p4Picks.Text += "Under " + txtP4PR.Text + " P+R | ";
                    queryAbove += " and p4.points + p4.reboundsTotal <= " + txtP4PR.Text;
                }
                else
                {
                    p4Picks.Text += txtP4PR.Text + " P+R | ";
                    queryAbove += " and p4.points + p4.reboundsTotal >= " + txtP4PR.Text;
                }
            }
            if (!txtP4AR.Text.IsNullOrWhiteSpace())
            {
                if (chkP4AR.Checked)
                {
                    p4Picks.Text += "Under " + txtP4AR.Text + " A+R | ";
                    queryAbove += " and p4.assists + p4.reboundsTotal <= " + txtP4AR.Text;
                }
                else
                {
                    p4Picks.Text += txtP4AR.Text + " A+R | ";
                    queryAbove += " and p4.assists + p4.reboundsTotal >= " + txtP4AR.Text;
                }
            }
            if (!txtP4PAR.Text.IsNullOrWhiteSpace())
            {
                if (chkP4PAR.Checked)
                {
                    p4Picks.Text += "Under " + txtP4PAR.Text + " P+A+R | ";
                    queryAbove += " and p4.points + p4.assists + p4.reboundsTotal <= " + txtP4PAR.Text;
                }
                else
                {
                    p4Picks.Text += txtP4PAR.Text + " P+A+R | ";
                    queryAbove += " and p4.points + p4.assists + p4.reboundsTotal >= " + txtP4PAR.Text;
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
            if (!txtP5PA.Text.IsNullOrWhiteSpace())
            {
                if (chkP5PA.Checked)
                {
                    p5Picks.Text += "Under " + txtP5PA.Text + " P+A | ";
                    queryAbove += " and p5.points + p5.assists <= " + txtP5PA.Text;
                }
                else
                {
                    p5Picks.Text += txtP5PA.Text + " P+A | ";
                    queryAbove += " and p5.points + p5.assists >= " + txtP5PA.Text;
                }
            }
            if (!txtP5PR.Text.IsNullOrWhiteSpace())
            {
                if (chkP5PR.Checked)
                {
                    p5Picks.Text += "Under " + txtP5PR.Text + " P+R | ";
                    queryAbove += " and p5.points + p5.reboundsTotal <= " + txtP5PR.Text;
                }
                else
                {
                    p5Picks.Text += txtP5PR.Text + " P+R | ";
                    queryAbove += " and p5.points + p5.reboundsTotal >= " + txtP5PR.Text;
                }
            }
            if (!txtP5AR.Text.IsNullOrWhiteSpace())
            {
                if (chkP5AR.Checked)
                {
                    p5Picks.Text += "Under " + txtP5AR.Text + " A+R | ";
                    queryAbove += " and p5.assists + p5.reboundsTotal <= " + txtP5AR.Text;
                }
                else
                {
                    p5Picks.Text += txtP5AR.Text + " A+R | ";
                    queryAbove += " and p5.assists + p5.reboundsTotal >= " + txtP5AR.Text;
                }
            }
            if (!txtP5PAR.Text.IsNullOrWhiteSpace())
            {
                if (chkP5PAR.Checked)
                {
                    p5Picks.Text += "Under " + txtP5PAR.Text + " P+A+R | ";
                    queryAbove += " and p5.points + p5.assists + p5.reboundsTotal <= " + txtP5PAR.Text;
                }
                else
                {
                    p5Picks.Text += txtP5PAR.Text + " P+A+R | ";
                    queryAbove += " and p5.points + p5.assists + p5.reboundsTotal >= " + txtP5PAR.Text;
                }
            }
            if (!txtP6Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkP6Pts.Checked)
                {
                    p6Picks.Text += "Under " + txtP6Pts.Text + "pts | ";
                    queryAbove += " and p6.points <= " + txtP6Pts.Text;
                }
                else
                {
                    p6Picks.Text += txtP6Pts.Text + "pts | ";
                    queryAbove += " and p6.points >= " + txtP6Pts.Text;
                }
            }
            if (!txtP6Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkP6Ast.Checked)
                {
                    p6Picks.Text += "Under " + txtP6Ast.Text + "ast | ";
                    queryAbove += " and p6.assists <= " + txtP6Ast.Text;
                }
                else
                {
                    p6Picks.Text += txtP6Ast.Text + "ast | ";
                    queryAbove += " and p6.assists >= " + txtP6Ast.Text;
                }
            }
            if (!txtP6Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkP6Reb.Checked)
                {
                    p6Picks.Text += "Under " + txtP6Reb.Text + "reb | ";
                    queryAbove += " and p6.reboundsTotal <= " + txtP6Reb.Text;
                }
                else
                {
                    p6Picks.Text += txtP6Reb.Text + "reb | ";
                    queryAbove += " and p6.reboundsTotal >= " + txtP6Reb.Text;
                }
            }
            if (!txtP63.Text.IsNullOrWhiteSpace())
            {
                if (chkP63.Checked)
                {
                    p6Picks.Text += "Under " + txtP63.Text + " 3PM | ";
                    queryAbove += " and p6.threePointersMade <= " + txtP63.Text;
                }
                else
                {
                    p6Picks.Text += txtP63.Text + " 3PM | ";
                    queryAbove += " and p6.threePointersMade >= " + txtP63.Text;
                }
            }
            if (!txtP6Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkP6Blk.Checked)
                {
                    p6Picks.Text += "Under " + txtP6Blk.Text + "blk | ";
                    queryAbove += " and p6.blocks <= " + txtP6Blk.Text;
                }
                else
                {
                    p6Picks.Text += txtP6Blk.Text + "blk | ";
                    queryAbove += " and p6.blocks >= " + txtP6Blk.Text;
                }
            }
            if (!txtP6Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkP6Stl.Checked)
                {
                    p6Picks.Text += "Under " + txtP6Stl.Text + "stl | ";
                    queryAbove += " and p6.steals <= " + txtP6Stl.Text;
                }
                else
                {
                    p6Picks.Text += txtP6Stl.Text + "stl | ";
                    queryAbove += " and p6.steals >= " + txtP6Stl.Text;
                }
            }
            if (!txtP6PA.Text.IsNullOrWhiteSpace())
            {
                if (chkP6PA.Checked)
                {
                    p6Picks.Text += "Under " + txtP6PA.Text + " P+A | ";
                    queryAbove += " and p6.points + p6.assists <= " + txtP6PA.Text;
                }
                else
                {
                    p6Picks.Text += txtP6PA.Text + " P+A | ";
                    queryAbove += " and p6.points + p6.assists >= " + txtP6PA.Text;
                }
            }
            if (!txtP6PR.Text.IsNullOrWhiteSpace())
            {
                if (chkP6PR.Checked)
                {
                    p6Picks.Text += "Under " + txtP6PR.Text + " P+R | ";
                    queryAbove += " and p6.points + p6.reboundsTotal <= " + txtP6PR.Text;
                }
                else
                {
                    p6Picks.Text += txtP6PR.Text + " P+R | ";
                    queryAbove += " and p6.points + p6.reboundsTotal >= " + txtP6PR.Text;
                }
            }
            if (!txtP6AR.Text.IsNullOrWhiteSpace())
            {
                if (chkP6AR.Checked)
                {
                    p6Picks.Text += "Under " + txtP6AR.Text + " A+R | ";
                    queryAbove += " and p6.assists + p6.reboundsTotal <= " + txtP6AR.Text;
                }
                else
                {
                    p6Picks.Text += txtP6AR.Text + " A+R | ";
                    queryAbove += " and p6.assists + p6.reboundsTotal >= " + txtP6AR.Text;
                }
            }
            if (!txtP6PAR.Text.IsNullOrWhiteSpace())
            {
                if (chkP6PAR.Checked)
                {
                    p6Picks.Text += "Under " + txtP6PAR.Text + " P+A+R | ";
                    queryAbove += " and p6.points + p6.assists + p6.reboundsTotal <= " + txtP6PAR.Text;
                }
                else
                {
                    p6Picks.Text += txtP6PAR.Text + " P+A+R | ";
                    queryAbove += " and p6.points + p6.assists + p6.reboundsTotal >= " + txtP6PAR.Text;
                }
            }
            if (!txtP7Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkP7Pts.Checked)
                {
                    p7Picks.Text += "Under " + txtP7Pts.Text + "pts | ";
                    queryAbove += " and p7.points <= " + txtP7Pts.Text;
                }
                else
                {
                    p7Picks.Text += txtP7Pts.Text + "pts | ";
                    queryAbove += " and p7.points >= " + txtP7Pts.Text;
                }
            }
            if (!txtP7Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkP7Ast.Checked)
                {
                    p7Picks.Text += "Under " + txtP7Ast.Text + "ast | ";
                    queryAbove += " and p7.assists <= " + txtP7Ast.Text;
                }
                else
                {
                    p7Picks.Text += txtP7Ast.Text + "ast | ";
                    queryAbove += " and p7.assists >= " + txtP7Ast.Text;
                }
            }
            if (!txtP7Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkP7Reb.Checked)
                {
                    p7Picks.Text += "Under " + txtP7Reb.Text + "reb | ";
                    queryAbove += " and p7.reboundsTotal <= " + txtP7Reb.Text;
                }
                else
                {
                    p7Picks.Text += txtP7Reb.Text + "reb | ";
                    queryAbove += " and p7.reboundsTotal >= " + txtP7Reb.Text;
                }
            }
            if (!txtP73.Text.IsNullOrWhiteSpace())
            {
                if (chkP73.Checked)
                {
                    p7Picks.Text += "Under " + txtP73.Text + " 3PM | ";
                    queryAbove += " and p7.threePointersMade <= " + txtP73.Text;
                }
                else
                {
                    p7Picks.Text += txtP73.Text + " 3PM | ";
                    queryAbove += " and p7.threePointersMade >= " + txtP73.Text;
                }
            }
            if (!txtP7Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkP7Blk.Checked)
                {
                    p7Picks.Text += "Under " + txtP7Blk.Text + "blk | ";
                    queryAbove += " and p7.blocks <= " + txtP7Blk.Text;
                }
                else
                {
                    p7Picks.Text += txtP7Blk.Text + "blk | ";
                    queryAbove += " and p7.blocks >= " + txtP7Blk.Text;
                }
            }
            if (!txtP7Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkP7Stl.Checked)
                {
                    p7Picks.Text += "Under " + txtP7Stl.Text + "stl | ";
                    queryAbove += " and p7.steals <= " + txtP7Stl.Text;
                }
                else
                {
                    p7Picks.Text += txtP7Stl.Text + "stl | ";
                    queryAbove += " and p7.steals >= " + txtP7Stl.Text;
                }
            }
            if (!txtP7PA.Text.IsNullOrWhiteSpace())
            {
                if (chkP7PA.Checked)
                {
                    p7Picks.Text += "Under " + txtP7PA.Text + " P+A | ";
                    queryAbove += " and p7.points + p7.assists <= " + txtP7PA.Text;
                }
                else
                {
                    p7Picks.Text += txtP7PA.Text + " P+A | ";
                    queryAbove += " and p7.points + p7.assists >= " + txtP7PA.Text;
                }
            }
            if (!txtP7PR.Text.IsNullOrWhiteSpace())
            {
                if (chkP7PR.Checked)
                {
                    p7Picks.Text += "Under " + txtP7PR.Text + " P+R | ";
                    queryAbove += " and p7.points + p7.reboundsTotal <= " + txtP7PR.Text;
                }
                else
                {
                    p7Picks.Text += txtP7PR.Text + " P+R | ";
                    queryAbove += " and p7.points + p7.reboundsTotal >= " + txtP7PR.Text;
                }
            }
            if (!txtP7AR.Text.IsNullOrWhiteSpace())
            {
                if (chkP7AR.Checked)
                {
                    p7Picks.Text += "Under " + txtP7AR.Text + " A+R | ";
                    queryAbove += " and p7.assists + p7.reboundsTotal <= " + txtP7AR.Text;
                }
                else
                {
                    p7Picks.Text += txtP7AR.Text + " A+R | ";
                    queryAbove += " and p7.assists + p7.reboundsTotal >= " + txtP7AR.Text;
                }
            }
            if (!txtP7PAR.Text.IsNullOrWhiteSpace())
            {
                if (chkP7PAR.Checked)
                {
                    p7Picks.Text += "Under " + txtP7PAR.Text + " P+A+R | ";
                    queryAbove += " and p7.points + p7.assists + p7.reboundsTotal <= " + txtP7PAR.Text;
                }
                else
                {
                    p7Picks.Text += txtP7PAR.Text + " P+A+R | ";
                    queryAbove += " and p7.points + p7.assists + p7.reboundsTotal >= " + txtP7PAR.Text;
                }
            }
            if (!txtP8Pts.Text.IsNullOrWhiteSpace())
            {
                if (chkP8Pts.Checked)
                {
                    p8Picks.Text += "Under " + txtP8Pts.Text + "pts | ";
                    queryAbove += " and p8.points <= " + txtP8Pts.Text;
                }
                else
                {
                    p8Picks.Text += txtP8Pts.Text + "pts | ";
                    queryAbove += " and p8.points >= " + txtP8Pts.Text;
                }
            }
            if (!txtP8Ast.Text.IsNullOrWhiteSpace())
            {
                if (chkP8Ast.Checked)
                {
                    p8Picks.Text += "Under " + txtP8Ast.Text + "ast | ";
                    queryAbove += " and p8.assists <= " + txtP8Ast.Text;
                }
                else
                {
                    p8Picks.Text += txtP8Ast.Text + "ast | ";
                    queryAbove += " and p8.assists >= " + txtP8Ast.Text;
                }
            }
            if (!txtP8Reb.Text.IsNullOrWhiteSpace())
            {
                if (chkP8Reb.Checked)
                {
                    p8Picks.Text += "Under " + txtP8Reb.Text + "reb | ";
                    queryAbove += " and p8.reboundsTotal <= " + txtP8Reb.Text;
                }
                else
                {
                    p8Picks.Text += txtP8Reb.Text + "reb | ";
                    queryAbove += " and p8.reboundsTotal >= " + txtP8Reb.Text;
                }
            }
            if (!txtP83.Text.IsNullOrWhiteSpace())
            {
                if (chkP83.Checked)
                {
                    p8Picks.Text += "Under " + txtP83.Text + " 3PM | ";
                    queryAbove += " and p8.threePointersMade <= " + txtP83.Text;
                }
                else
                {
                    p8Picks.Text += txtP83.Text + " 3PM | ";
                    queryAbove += " and p8.threePointersMade >= " + txtP83.Text;
                }
            }
            if (!txtP8Blk.Text.IsNullOrWhiteSpace())
            {
                if (chkP8Blk.Checked)
                {
                    p8Picks.Text += "Under " + txtP8Blk.Text + "blk | ";
                    queryAbove += " and p8.blocks <= " + txtP8Blk.Text;
                }
                else
                {
                    p8Picks.Text += txtP8Blk.Text + "blk | ";
                    queryAbove += " and p8.blocks >= " + txtP8Blk.Text;
                }
            }
            if (!txtP8Stl.Text.IsNullOrWhiteSpace())
            {
                if (chkP8Stl.Checked)
                {
                    p8Picks.Text += "Under " + txtP8Stl.Text + "stl | ";
                    queryAbove += " and p8.steals <= " + txtP8Stl.Text;
                }
                else
                {
                    p8Picks.Text += txtP8Stl.Text + "stl | ";
                    queryAbove += " and p8.steals >= " + txtP8Stl.Text;
                }
            }
            if (!txtP8PA.Text.IsNullOrWhiteSpace())
            {
                if (chkP8PA.Checked)
                {
                    p8Picks.Text += "Under " + txtP8PA.Text + " P+A | ";
                    queryAbove += " and p8.points + p8.assists <= " + txtP8PA.Text;
                }
                else
                {
                    p8Picks.Text += txtP8PA.Text + " P+A | ";
                    queryAbove += " and p8.points + p8.assists >= " + txtP8PA.Text;
                }
            }
            if (!txtP8PR.Text.IsNullOrWhiteSpace())
            {
                if (chkP8PR.Checked)
                {
                    p8Picks.Text += "Under " + txtP8PR.Text + " P+R | ";
                    queryAbove += " and p8.points + p8.reboundsTotal <= " + txtP8PR.Text;
                }
                else
                {
                    p8Picks.Text += txtP8PR.Text + " P+R | ";
                    queryAbove += " and p8.points + p8.reboundsTotal >= " + txtP8PR.Text;
                }
            }
            if (!txtP8AR.Text.IsNullOrWhiteSpace())
            {
                if (chkP8AR.Checked)
                {
                    p8Picks.Text += "Under " + txtP8AR.Text + " A+R | ";
                    queryAbove += " and p8.assists + p8.reboundsTotal <= " + txtP8AR.Text;
                }
                else
                {
                    p8Picks.Text += txtP8AR.Text + " A+R | ";
                    queryAbove += " and p8.assists + p8.reboundsTotal >= " + txtP8AR.Text;
                }
            }
            if (!txtP8PAR.Text.IsNullOrWhiteSpace())
            {
                if (chkP8PAR.Checked)
                {
                    p8Picks.Text += "Under " + txtP8PAR.Text + " P+A+R | ";
                    queryAbove += " and p8.points + p8.assists + p8.reboundsTotal <= " + txtP8PAR.Text;
                }
                else
                {
                    p8Picks.Text += txtP8PAR.Text + " P+A+R | ";
                    queryAbove += " and p8.points + p8.assists + p8.reboundsTotal >= " + txtP8PAR.Text;
                }
            }


            queryInfo = query.Replace(select, selectInfo);

            string queryAboveInfo = queryAbove.Replace(select, selectInfo);

            string queryW = query + " and t.points > t.pointsAgainst";
            string queryAboveW= queryAbove + " and t.points > t.pointsAgainst";

            string queryL = query + " and t.points < t.pointsAgainst";
            string queryAboveL = queryAbove + " and t.points < t.pointsAgainst";

            gamesPlayed = 0;
            gamesAbove = 0;
            GetOdds(queryAbove, "ga");
            GetOdds(query, "gp");

            //gamesPlayed = 0;
            //gamesAbove = 0;
            //GetOdds(queryInfo, "gp");
            GameInfo(queryInfo, GameLinksPlaceholder, "all");
            GameInfo(queryAboveInfo, GameLinksPlaceholder, "above");


            gpW = 0;
            gaW = 0;
            GetOddsW(queryW, "gp");
            GetOddsW(queryAboveW, "ga");

            gpL = 0;
            gaL = 0;
            GetOddsL(queryL, "gp");
            GetOddsL(queryAboveL, "ga");

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
                    try
                    {
                        busDriver.SQLdb.Open();
                    }
                    catch (InvalidOperationException e)
                    {
                        busDriver.SQLdb.Close();
                        busDriver.SQLdb.Open();
                    }
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

            if (gamesAbove != 0 && gamesPlayed != 0)
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
            else if (sender != "gp" && gamesAbove == 0)
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
            else if (sender == "ga" && gamesPlayed == 0)
            {
                lblOdds.Text = "";
                lblError.Text = "Divide by 0 error i believe";
                SaveOdds = "";
                SaveProbability = 0;
            }
        }

        public List<int> allGames = new List<int>();
        public List<int> aboveGames = new List<int>();
        public Dictionary<int, (int, string)> allGamesDates = new Dictionary<int, (int, string)>();
        public Dictionary<int, int> aboveGames2 = new Dictionary<int, int>();
        public void GameInfo(string query, PlaceHolder placeholder, string sender)
        {
            // List to store game data
            List<string> gameLinks = new List<string>();
            string orderBy = " order by date desc";
            using (SqlCommand gameInfo = new SqlCommand(query + orderBy))
            {
                gameInfo.CommandType = CommandType.Text;
                using (SqlDataAdapter sGameInfo = new SqlDataAdapter())
                {
                    gameInfo.Connection = busDriver.SQLdb;
                    sGameInfo.SelectCommand = gameInfo;
                    try
                    {
                        busDriver.SQLdb.Open();
                    }
                    catch (InvalidOperationException)
                    {
                        busDriver.SQLdb.Close();
                        busDriver.SQLdb.Open();                    }

                    SqlDataReader reader = gameInfo.ExecuteReader();
                    int gameNumber = 1;
                    while (reader.Read())
                    {
                        // Extract data
                        string gameId = reader["game_id"].ToString();
                        DateTime gameDate = DateTime.Parse(reader["Date"].ToString());
                        string formattedDate = gameDate.ToString("MM/dd/yyyy");
                        if (sender == "all")
                        {
                            allGames.Add(Int32.Parse(gameId));
                            allGamesDates.Add(Int32.Parse(gameId), (gameNumber, formattedDate));
                        }
                        else
                        {
                            aboveGames.Add(Int32.Parse(gameId));
                            aboveGames2.Add(Int32.Parse(gameId), Int32.Parse(gameId));
                        }
                        // Create hyperlink text
                        string linkText = $"Game {gameNumber}: {formattedDate}";
                        string url = $"https://www.nba.com/game/abc-vs-def-00{gameId}/box-score";
                        // Add to the list
                        //gameLinks.Add($"<a href='{url}' target='_blank'>{linkText}</a>");
                        gameNumber++;
                    }
                    reader.Close();
                    busDriver.SQLdb.Close();
                }
            }
            if (sender == "above")
            {
                foreach (int game in allGames)
                {
                    string linkText = $"Game {allGamesDates[game].Item1}: {allGamesDates[game].Item2}";
                    string url = $"https://www.nba.com/game/abc-vs-def-00{game}/box-score";
                    if (aboveGames2.ContainsKey(game))
                    {
                        gameLinks.Add($"<a href='{url}' style='color: green;' target='_blank'>{linkText}</a>");
                    }
                    else
                    {
                        gameLinks.Add($"<a href='{url}' target='_blank'>{linkText}</a>");
                    }
                }
                // Dynamically add hyperlinks to the UI
                foreach (string link in gameLinks)
                {
                    // Create a LiteralControl to display the link in the placeholder
                    LiteralControl linkControl = new LiteralControl(link + "<br/>");
                    placeholder.Controls.Add(linkControl);
                }
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
        public static int gpW = 0;
        public static int gaW = 0;
        public static string SaveOddsW = "";
        public static float SaveProbabilityW = 0;
        public void GetOddsW(string query, string sender)
        {
            SaveOddsW = "";
            SaveProbabilityW = 0;
            using (SqlCommand GamesPlayed = new SqlCommand(query))
            {
                GamesPlayed.CommandType = CommandType.Text;
                using (SqlDataAdapter sGamesPlayed = new SqlDataAdapter())
                {
                    GamesPlayed.Connection = busDriver.SQLdb;
                    sGamesPlayed.SelectCommand = GamesPlayed;
                    try
                    {
                        busDriver.SQLdb.Open();
                    }
                    catch (InvalidOperationException e)
                    {
                        busDriver.SQLdb.Close();
                        busDriver.SQLdb.Open();
                    }
                    SqlDataReader reader = GamesPlayed.ExecuteReader();
                    while (reader.Read())
                    {
                        if (sender == "gp")
                        {
                            gpW = reader.GetInt32(0);
                        }
                        else if (sender == "ga")
                        {
                            gaW = reader.GetInt32(0);
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }

            if (gaW != 0 && gpW != 0)
            {
                float probability = ((float)gaW / (float)gpW) * 100;
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
                lblOddsW.Text = "In Games Won:" + gaW + "/" + gpW + " | " + "Probability: " + Math.Round(probability, 2) + "% | Implied odds: " + oddsText;
                SaveOddsW = oddsText;
                SaveProbabilityW = probability;
            }
            else if (sender != "gp" && gaW == 0)
            {

                float probability = ((float)gaW / (float)gpW) * 100;
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
                lblOddsW.Text = "In Games Won:" + gaW + "/" + gpW + " | " + "Probability: " + Math.Round(probability, 2) + "% | Implied odds: " + oddsText;
                SaveOddsW = oddsText;
                SaveProbabilityW = probability;
            }
            else if (sender == "ga" && gpW == 0)
            {
                lblOddsW.Text = "";
                lblError.Text = "Divide by 0 error i believe, Wins";
                SaveOddsW = "";
                SaveProbabilityW = 0;
            }
        }

        public static int gpL = 0;
        public static int gaL = 0;
        public static string SaveOddsL = "";
        public static float SaveProbabilityL = 0;
        public void GetOddsL(string query, string sender)
        {
            SaveOddsL = "";
            SaveProbabilityL = 0;
            using (SqlCommand GamesPlayed = new SqlCommand(query))
            {
                GamesPlayed.CommandType = CommandType.Text;
                using (SqlDataAdapter sGamesPlayed = new SqlDataAdapter())
                {
                    GamesPlayed.Connection = busDriver.SQLdb;
                    sGamesPlayed.SelectCommand = GamesPlayed;
                    try
                    {
                        busDriver.SQLdb.Open();
                    }
                    catch (InvalidOperationException e)
                    {
                        busDriver.SQLdb.Close();
                        busDriver.SQLdb.Open();
                    }
                    SqlDataReader reader = GamesPlayed.ExecuteReader();
                    while (reader.Read())
                    {
                        if (sender == "gp")
                        {
                            gpL = reader.GetInt32(0);
                        }
                        else if (sender == "ga")
                        {
                            gaL = reader.GetInt32(0);
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }

            if (gaL != 0 && gpL != 0)
            {
                float probability = ((float)gaL / (float)gpL) * 100;
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
                lblOddsL.Text = "In Games Lost:" + gaL + "/" + gpL + " | " + "Probability: " + Math.Round(probability, 2) + "% | Implied odds: " + oddsText;
                SaveOddsL = oddsText;
                SaveProbabilityL = probability;
            }
            else if (sender != "gp" && gaL == 0)
            {

                float probability = ((float)gaL / (float)gpL) * 100;
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
                lblOddsL.Text = "In Games Lost:" + gaL + "/" + gpL + " | " + "Probability: " + Math.Round(probability, 2) + "% | Implied odds: " + oddsText;
                SaveOddsL = oddsText;
                SaveProbabilityL = probability;
            }
            else if (sender == "ga" && gpL == 0)
            {
                lblOddsL.Text = "";
                lblError.Text = "Divide by 0 error i believe, Wins";
                SaveOddsL = "";
                SaveProbabilityL = 0;
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
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP1PA, chkP1PA));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP1PR, chkP1PR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP1AR, chkP1AR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP1PAR, chkP1PAR));


            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2Pts, chkP2Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2Ast, chkP2Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2Reb, chkP2Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP23, chkP23));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2Blk, chkP2Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2Stl, chkP2Stl));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2PA, chkP2PA));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2PR, chkP2PR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2AR, chkP2AR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP2PAR, chkP2PAR));


            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3Pts, chkP3Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3Ast, chkP3Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3Reb, chkP3Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP33, chkP33));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3Blk, chkP3Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3Stl, chkP3Stl));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3PA, chkP3PA));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3PR, chkP3PR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3AR, chkP3AR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP3PAR, chkP3PAR));


            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4Pts, chkP4Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4Ast, chkP4Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4Reb, chkP4Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP43, chkP43));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4Blk, chkP4Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4Stl, chkP4Stl));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4PA, chkP4PA));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4PR, chkP4PR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4AR, chkP4AR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP4PAR, chkP4PAR));


            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5Pts, chkP5Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5Ast, chkP5Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5Reb, chkP5Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP53, chkP53));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5Blk, chkP5Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5Stl, chkP5Stl));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5PA, chkP5PA));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5PR, chkP5PR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5AR, chkP5AR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP5PAR, chkP5PAR));

            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP6Pts, chkP6Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP6Ast, chkP6Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP6Reb, chkP6Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP63, chkP63));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP6Blk, chkP6Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP6Stl, chkP6Stl));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP6PA, chkP6PA));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP6PR, chkP6PR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP6AR, chkP6AR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP6PAR, chkP6PAR));


            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP7Pts, chkP7Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP7Ast, chkP7Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP7Reb, chkP7Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP73, chkP73));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP7Blk, chkP7Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP7Stl, chkP7Stl));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP7PA, chkP7PA));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP7PR, chkP7PR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP7AR, chkP7AR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP7PAR, chkP7PAR));


            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP8Pts, chkP8Pts));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP8Ast, chkP8Ast));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP8Reb, chkP8Reb));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP83, chkP83));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP8Blk, chkP8Blk));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP8Stl, chkP8Stl));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP8PA, chkP8PA));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP8PR, chkP8PR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP8AR, chkP8AR));
            t1FullList.Add(new Tuple<TextBox, CheckBox>(txtP8PAR, chkP8PAR));

            using (SqlCommand SaveProp = new SqlCommand("SaveParlay"))
            {
                SaveProp.Connection = busDriver.SQLdb; //17
                SaveProp.CommandType = CommandType.StoredProcedure;
                SaveProp.Parameters.AddWithValue("@team_id", Int32.Parse(ddTeams.SelectedItem.Value));
                for (int i = 0; i < t1Rosters.Count; i++)
                {
                    if (t1Rosters[i].SelectedItem.Text.Contains("Player"))
                    {
                        SaveProp.Parameters.AddWithValue("@p" + (i + 1), SqlString.Null);
                    }
                    else
                    {
                        SaveProp.Parameters.AddWithValue("@p" + (i + 1), t1Rosters[i].SelectedItem.Text);
                    }
                }
                for (int i = 0; i < t1ActiveDNPRosters.Count; i++)
                {
                    if (t1ActiveDNPRosters[i].SelectedItem.Text.Contains("Player"))
                    {
                        SaveProp.Parameters.AddWithValue("@dnp" + (i + 1), SqlString.Null);
                    }
                    else
                    {
                        SaveProp.Parameters.AddWithValue("@dnp" + (i + 1), t1ActiveDNPRosters[i].SelectedItem.Text);
                    }
                }
                SaveProp.Parameters.AddWithValue("@GamesPlayed", gamesPlayed);
                SaveProp.Parameters.AddWithValue("@GamesAbove", gamesAbove);
                SaveProp.Parameters.AddWithValue("@GamesPlayedW", gpW);
                SaveProp.Parameters.AddWithValue("@GamesAboveW", gaW);
                SaveProp.Parameters.AddWithValue("@GamesPlayedL", gpL);
                SaveProp.Parameters.AddWithValue("@GamesAboveL", gaL);
                SaveProp.Parameters.AddWithValue("@Odds", SaveOdds);
                if(SaveProbability.Equals(float.NaN))
                {
                    SaveProp.Parameters.AddWithValue("@Probability", 0);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@Probability", SaveProbability);
                }


                SaveProp.Parameters.AddWithValue("@OddsW", SaveOddsW);
                if (SaveProbabilityW.Equals(float.NaN))
                {
                    SaveProp.Parameters.AddWithValue("@ProbabilityW", 0);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@ProbabilityW", SaveProbabilityW);
                }


                SaveProp.Parameters.AddWithValue("@OddsL", SaveOddsL);
                if (SaveProbabilityL.Equals(float.NaN))
                {
                    SaveProp.Parameters.AddWithValue("@ProbabilityL", 0);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@ProbabilityL", SaveProbabilityL);
                }


                int parsedValue = 0;
                if (Int32.TryParse(ddlRoster.SelectedItem.Value, out parsedValue))
                {
                    SaveProp.Parameters.AddWithValue("@p1_id", parsedValue);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@p1_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlRoster2.SelectedItem.Value, out parsedValue))
                {
                    SaveProp.Parameters.AddWithValue("@p2_id", parsedValue);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@p2_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlRoster3.SelectedItem.Value, out parsedValue))
                {
                    SaveProp.Parameters.AddWithValue("@p3_id", parsedValue);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@p3_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlRoster4.SelectedItem.Value, out parsedValue))
                {
                    SaveProp.Parameters.AddWithValue("@p4_id", parsedValue);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@p4_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlRoster5.SelectedItem.Value, out parsedValue))
                {
                    SaveProp.Parameters.AddWithValue("@p5_id", parsedValue);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@p5_id", SqlInt32.Null);
                }

                if (Int32.TryParse(ddlRoster6.SelectedItem.Value, out parsedValue))
                {
                    SaveProp.Parameters.AddWithValue("@p6_id", parsedValue);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@p6_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlRoster7.SelectedItem.Value, out parsedValue))
                {
                    SaveProp.Parameters.AddWithValue("@p7_id", parsedValue);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@p7_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlRoster8.SelectedItem.Value, out parsedValue))
                {
                    SaveProp.Parameters.AddWithValue("@p8_id", parsedValue);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@p8_id", SqlInt32.Null);
                }


                if (Int32.TryParse(ddlDNP.SelectedItem.Value, out parsedValue))
                {
                    SaveProp.Parameters.AddWithValue("@dnp1_id", parsedValue);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@dnp1_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlDNP2.SelectedItem.Value, out parsedValue))
                {
                    SaveProp.Parameters.AddWithValue("@dnp2_id", parsedValue);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@dnp2_id", SqlInt32.Null);
                }
                if (Int32.TryParse(ddlDNP3.SelectedItem.Value, out parsedValue))
                {
                    SaveProp.Parameters.AddWithValue("@dnp3_id", parsedValue);
                }
                else
                {
                    SaveProp.Parameters.AddWithValue("@dnp3_id", SqlInt32.Null);
                }
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
                        SaveProp.Parameters.AddWithValue(parameter, value);
                    }
                    else
                    {
                        SaveProp.Parameters.AddWithValue(parameter, SqlString.Null);
                    }
                }
                try
                {
                    busDriver.SQLdb.Open();
                }
                catch (InvalidOperationException ea)
                {
                    busDriver.SQLdb.Close();
                    busDriver.SQLdb.Open();
                }
                SaveProp.ExecuteScalar();
                busDriver.SQLdb.Close();
            }
        }

        protected void t2btnSave_Click(object sender, EventArgs e)
        {

        }


        public void SaveProp()
        {

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int propID = Int32.Parse(ddlLoad.SelectedItem.Value);
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
            string teamID = "";
            string team = "";
            string p1 = ""; string p1ID = ""; string p1Pts = ""; string p1Ast = ""; string p1Reb = ""; string p13 = ""; string p1Blk = ""; string p1Stl = ""; string p1PA = ""; string p1PR = ""; string p1AR = ""; string p1PAR = "";
            string p2 = ""; string p2ID = ""; string p2Pts = ""; string p2Ast = ""; string p2Reb = ""; string p23 = ""; string p2Blk = ""; string p2Stl = ""; string p2PA = ""; string p2PR = ""; string p2AR = ""; string p2PAR = "";
            string p3 = ""; string p3ID = ""; string p3Pts = ""; string p3Ast = ""; string p3Reb = ""; string p33 = ""; string p3Blk = ""; string p3Stl = ""; string p3PA = ""; string p3PR = ""; string p3AR = ""; string p3PAR = "";
            string p4 = ""; string p4ID = ""; string p4Pts = ""; string p4Ast = ""; string p4Reb = ""; string p43 = ""; string p4Blk = ""; string p4Stl = ""; string p4PA = ""; string p4PR = ""; string p4AR = ""; string p4PAR = "";
            string p5 = ""; string p5ID = ""; string p5Pts = ""; string p5Ast = ""; string p5Reb = ""; string p53 = ""; string p5Blk = ""; string p5Stl = ""; string p5PA = ""; string p5PR = ""; string p5AR = ""; string p5PAR = "";
            string p6 = ""; string p6ID = ""; string p6Pts = ""; string p6Ast = ""; string p6Reb = ""; string p63 = ""; string p6Blk = ""; string p6Stl = ""; string p6PA = ""; string p6PR = ""; string p6AR = ""; string p6PAR = "";
            string p7 = ""; string p7ID = ""; string p7Pts = ""; string p7Ast = ""; string p7Reb = ""; string p73 = ""; string p7Blk = ""; string p7Stl = ""; string p7PA = ""; string p7PR = ""; string p7AR = ""; string p7PAR = "";
            string p8 = ""; string p8ID = ""; string p8Pts = ""; string p8Ast = ""; string p8Reb = ""; string p83 = ""; string p8Blk = ""; string p8Stl = ""; string p8PA = ""; string p8PR = ""; string p8AR = ""; string p8PAR = "";
            string dnp1 = ""; string dnp1ID = "";
            string dnp2 = ""; string dnp2ID = "";
            string dnp3 = ""; string dnp3ID = "";
            List<String> LoadProps;



            using (SqlCommand GamesPlayed = new SqlCommand("LoadSavedParlay"))
            {
                GamesPlayed.CommandType = CommandType.StoredProcedure;
                GamesPlayed.Parameters.AddWithValue("@ID", propID);
                using (SqlDataAdapter sGamesPlayed = new SqlDataAdapter())
                {
                    GamesPlayed.Connection = busDriver.SQLdb;
                    sGamesPlayed.SelectCommand = GamesPlayed;
                    try
                    {
                        busDriver.SQLdb.Open();
                    }
                    catch (InvalidOperationException ea)
                    {
                        busDriver.SQLdb.Close();
                        busDriver.SQLdb.Open();
                    }
                    SqlDataReader reader = GamesPlayed.ExecuteReader();
                    while (reader.Read())
                    {
                        teamID = reader["team_id"].ToString();
                        p1 = reader["p1"].ToString(); p1ID = reader["p1_id"].ToString(); p1Pts = reader["p1Pts"].ToString(); p1Ast = reader["p1Ast"].ToString(); p1Reb = reader["p1Reb"].ToString(); p13 = reader["p1FG3"].ToString(); p1Blk = reader["p1Blk"].ToString(); p1Stl = reader["p1Stl"].ToString();   p1PA = reader["p1PA"].ToString(); p1PR = reader["p1PR"].ToString(); p1AR = reader["p1AR"].ToString();  p1PAR = reader["p1PAR"].ToString();
                        p2 = reader["p2"].ToString(); p2ID = reader["p2_id"].ToString(); p2Pts = reader["p2Pts"].ToString(); p2Ast = reader["p2Ast"].ToString(); p2Reb = reader["p2Reb"].ToString(); p23 = reader["p2FG3"].ToString(); p2Blk = reader["p2Blk"].ToString(); p2Stl = reader["p2Stl"].ToString();   p2PA = reader["p2PA"].ToString(); p2PR = reader["p2PR"].ToString(); p2AR = reader["p2AR"].ToString();  p2PAR = reader["p2PAR"].ToString();
                        p3 = reader["p3"].ToString(); p3ID = reader["p3_id"].ToString(); p3Pts = reader["p3Pts"].ToString(); p3Ast = reader["p3Ast"].ToString(); p3Reb = reader["p3Reb"].ToString(); p33 = reader["p3FG3"].ToString(); p3Blk = reader["p3Blk"].ToString(); p3Stl = reader["p3Stl"].ToString();   p3PA = reader["p3PA"].ToString(); p3PR = reader["p3PR"].ToString(); p3AR = reader["p3AR"].ToString();  p3PAR = reader["p3PAR"].ToString();
                        p4 = reader["p4"].ToString(); p4ID = reader["p4_id"].ToString(); p4Pts = reader["p4Pts"].ToString(); p4Ast = reader["p4Ast"].ToString(); p4Reb = reader["p4Reb"].ToString(); p43 = reader["p4FG3"].ToString(); p4Blk = reader["p4Blk"].ToString(); p4Stl = reader["p4Stl"].ToString();   p4PA = reader["p4PA"].ToString(); p4PR = reader["p4PR"].ToString(); p4AR = reader["p4AR"].ToString();  p4PAR = reader["p4PAR"].ToString();
                        p5 = reader["p5"].ToString(); p5ID = reader["p5_id"].ToString(); p5Pts = reader["p5Pts"].ToString(); p5Ast = reader["p5Ast"].ToString(); p5Reb = reader["p5Reb"].ToString(); p53 = reader["p5FG3"].ToString(); p5Blk = reader["p5Blk"].ToString(); p5Stl = reader["p5Stl"].ToString();   p5PA = reader["p5PA"].ToString(); p5PR = reader["p5PR"].ToString(); p5AR = reader["p5AR"].ToString();  p5PAR = reader["p5PAR"].ToString();
                        p6 = reader["p6"].ToString(); p6ID = reader["p6_id"].ToString(); p6Pts = reader["p6Pts"].ToString(); p6Ast = reader["p6Ast"].ToString(); p6Reb = reader["p6Reb"].ToString(); p63 = reader["p6FG3"].ToString(); p6Blk = reader["p6Blk"].ToString(); p6Stl = reader["p6Stl"].ToString();   p6PA = reader["p6PA"].ToString(); p6PR = reader["p6PR"].ToString(); p6AR = reader["p6AR"].ToString();  p6PAR = reader["p6PAR"].ToString();
                        p7 = reader["p7"].ToString(); p7ID = reader["p7_id"].ToString(); p7Pts = reader["p7Pts"].ToString(); p7Ast = reader["p7Ast"].ToString(); p7Reb = reader["p7Reb"].ToString(); p73 = reader["p7FG3"].ToString(); p7Blk = reader["p7Blk"].ToString(); p7Stl = reader["p7Stl"].ToString();   p7PA = reader["p7PA"].ToString(); p7PR = reader["p7PR"].ToString(); p7AR = reader["p7AR"].ToString();  p7PAR = reader["p7PAR"].ToString();
                        p8 = reader["p8"].ToString(); p8ID = reader["p8_id"].ToString(); p8Pts = reader["p8Pts"].ToString(); p8Ast = reader["p8Ast"].ToString(); p8Reb = reader["p8Reb"].ToString(); p83 = reader["p8FG3"].ToString(); p8Blk = reader["p8Blk"].ToString(); p8Stl = reader["p8Stl"].ToString();   p8PA = reader["p8PA"].ToString(); p8PR = reader["p8PR"].ToString(); p8AR = reader["p8AR"].ToString();  p8PAR = reader["p8PAR"].ToString();
                        dnp1 = reader["dnp1"].ToString(); dnp1ID = reader["dnp1_id"].ToString();
                        dnp2 = reader["dnp2"].ToString(); dnp2ID = reader["dnp2_id"].ToString();
                        dnp3 = reader["dnp3"].ToString(); dnp3ID = reader["dnp3_id"].ToString();
                    }
                }
                busDriver.SQLdb.Close();
            }

            LoadProps = new List<String>
            {
                 p1Pts,  p1Ast,  p1Reb,  p13,  p1Blk,  p1Stl, p1PA, p1PR, p1AR, p1PAR,
                 p2Pts,  p2Ast,  p2Reb,  p23,  p2Blk,  p2Stl, p2PA, p2PR, p2AR, p2PAR,
                 p3Pts,  p3Ast,  p3Reb,  p33,  p3Blk,  p3Stl, p3PA, p3PR, p3AR, p3PAR,
                 p4Pts,  p4Ast,  p4Reb,  p43,  p4Blk,  p4Stl, p4PA, p4PR, p4AR, p4PAR,
                 p5Pts,  p5Ast,  p5Reb,  p53,  p5Blk,  p5Stl,  p5PA, p5PR, p5AR, p5PAR,
                 p6Pts,  p6Ast,  p6Reb,  p63,  p6Blk,  p6Stl, p6PA, p6PR, p6AR, p6PAR,
                 p7Pts,  p7Ast,  p7Reb,  p73,  p7Blk,  p7Stl, p7PA, p7PR, p7AR, p7PAR,
                 p8Pts,  p8Ast,  p8Reb,  p83,  p8Blk,  p8Stl,  p8PA, p8PR, p8AR, p8PAR,
            };



            ddTeams.SelectedValue = teamID;
            ddTeams_SelectedIndexChanged(sender, e);
            ddlRoster.SelectedValue = p1ID;
            ddlRoster_SelectedIndexChanged(sender, e);
            if (!p2.IsNullOrWhiteSpace())
            {
                ddlRoster2.SelectedValue = p2ID;
                ddlRoster2_SelectedIndexChanged(sender, e);
            }
            if (!p3.IsNullOrWhiteSpace())
            {
                ddlRoster3.SelectedValue = p3ID;
                ddlRoster3_SelectedIndexChanged(sender, e);
            }
            if (!p4.IsNullOrWhiteSpace())
            {
                ddlRoster4.SelectedValue = p4ID;
                ddlRoster4_SelectedIndexChanged(sender, e);
            }
            if (!p5.IsNullOrWhiteSpace())
            {
                ddlRoster5.SelectedValue = p5ID;
                ddlRoster5_SelectedIndexChanged(sender, e);
            }
            if (!p6.IsNullOrWhiteSpace())
            {
                ddlRoster6.SelectedValue = p6ID;
                ddlRoster6_SelectedIndexChanged(sender, e);
            }
            if (!p7.IsNullOrWhiteSpace())
            {
                ddlRoster7.SelectedValue = p7ID;
                ddlRoster7_SelectedIndexChanged(sender, e);
            }
            if (!p8.IsNullOrWhiteSpace())
            {
                ddlRoster8.SelectedValue = p8ID;
                ddlRoster8_SelectedIndexChanged(sender, e);
            }
            if (!dnp1.IsNullOrWhiteSpace())
            {
                ddlDNP.SelectedValue = dnp1ID;
                ddlDNP_SelectedIndexChanged(sender, e);
            }
            if (!dnp2.IsNullOrWhiteSpace())
            {
                ddlDNP2.SelectedValue = dnp2ID;
                ddlDNP2_SelectedIndexChanged(sender, e);
            }
            if (!dnp3.IsNullOrWhiteSpace())
            {
                ddlDNP3.SelectedValue = dnp3ID;
                ddlDNP3_SelectedIndexChanged(sender, e);
            }

            for (int i = 0; i < LoadProps.Count; i++)
            {
                if (LoadProps[i].Contains("u"))
                {
                    allT1PlayerPropsUnders[i].Checked = true;
                    LoadProps[i] = LoadProps[i].Replace("u", "");                    
                }
                if (!LoadProps[i].IsNullOrWhiteSpace())
                {
                    allT1PlayerProps[i].Text = LoadProps[i];
                }
            }


        }
        


        protected void ddlLoad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnFill_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (DropDownList roster in t1Rosters)
            {
                List<TextBox> props = new List<TextBox>();
                if (!roster.SelectedItem.Text.IsNullOrWhiteSpace() && roster.SelectedItem.Text != "Player")
                {
                    if (i == 0)
                    {
                        props = new List<TextBox>(p1);
                    }
                    else if (i == 1)
                    {
                        props = new List<TextBox>(p2);
                    }
                    else if (i == 2)
                    {
                        props = new List<TextBox>(p3);
                    }
                    else if (i == 3)
                    {
                        props = new List<TextBox>(p4);
                    }
                    else if (i == 4)
                    {
                        props = new List<TextBox>(p5);
                    }
                    else if (i == 5)
                    {
                        props = new List<TextBox>(p6);
                    }
                    else if (i == 6)
                    {
                        props = new List<TextBox>(p7);
                    }
                    else if (i == 7)
                    {
                        props = new List<TextBox>(p8);
                    }

                    using (SqlCommand querySearch = new SqlCommand("FillMinimums"))
                    {
                        querySearch.Connection = busDriver.SQLdb;
                        querySearch.CommandType = CommandType.StoredProcedure;
                        querySearch.Parameters.AddWithValue("@season", ddSeason.SelectedItem.Value);
                        querySearch.Parameters.AddWithValue("@team", ddTeams.SelectedItem.Value);
                        querySearch.Parameters.AddWithValue("@player", roster.SelectedItem.Value);
                        try
                        {
                            busDriver.SQLdb.Open();
                        }
                        catch (InvalidOperationException ea)
                        {
                            busDriver.SQLdb.Close();
                            busDriver.SQLdb.Open();
                        }
                        using (SqlDataReader sdr = querySearch.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                props[0].Text = sdr["points"].ToString();
                                props[1].Text = sdr["assists"].ToString();
                                props[2].Text = sdr["reboundsTotal"].ToString();
                                props[3].Text = sdr["threePointersMade"].ToString();
                                props[4].Text = sdr["blocks"].ToString();
                                props[5].Text = sdr["steals"].ToString();
                                props[6].Text = sdr["PA"].ToString();
                                props[7].Text = sdr["PR"].ToString();
                                props[8].Text = sdr["AR"].ToString();
                                props[9].Text = sdr["PAR"].ToString();
                            }
                        }
                        busDriver.SQLdb.Close();
                    }
                }

                i++;
            }
        }

        protected void btnFillMax_Click(object sender, EventArgs e)
        {
            List<TextBox> props = new List<TextBox>();
            List<DropDownList> empty = new List<DropDownList>();
            List<CheckBox> unders = new List<CheckBox>();
            List<DropDownList> player = new List<DropDownList>();
            List<List<TextBox>> propList = new List<List<TextBox>>();
            List<List<CheckBox>> underList = new List<List<CheckBox>>();


            int j = 0;
            foreach (DropDownList roster in t1Rosters)
            {
                if (roster.SelectedItem.Text.IsNullOrWhiteSpace() || roster.SelectedItem.Text == "Player")
                {
                    empty.Add(roster);
                    propList.Add(txtList[j]);
                    underList.Add(chkList[j]);
                }
                else
                {
                    player.Add(roster);
                }
                j++;
            }

            int looper = 4;
            if(empty.Count < player.Count)
            {
                looper = empty.Count;
            }
            else
            {
                looper = player.Count;
            }


            for(int i = 0; i < looper; i++)
            {
                //empty[i].SelectedItem.Text = player[i].SelectedItem.Text;
                //empty[i].SelectedItem.Value = player[i].SelectedItem.Value;
                empty[i].SelectedIndex = player[i].SelectedIndex;
                using (SqlCommand querySearch = new SqlCommand("FillMaximums"))
                {
                    querySearch.Connection = busDriver.SQLdb;
                    querySearch.CommandType = CommandType.StoredProcedure;
                    querySearch.Parameters.AddWithValue("@season", ddSeason.SelectedItem.Value);
                    querySearch.Parameters.AddWithValue("@team", ddTeams.SelectedItem.Value);
                    querySearch.Parameters.AddWithValue("@player", empty[i].SelectedItem.Value);
                    try
                    {
                        busDriver.SQLdb.Open();
                    }
                    catch (InvalidOperationException ea)
                    {
                        busDriver.SQLdb.Close();
                        busDriver.SQLdb.Open();
                    }
                    using (SqlDataReader sdr = querySearch.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            propList[i][0].Text = sdr["points"].ToString();
                            propList[i][1].Text = sdr["assists"].ToString();
                            propList[i][2].Text = sdr["reboundsTotal"].ToString();
                            propList[i][3].Text = sdr["threePointersMade"].ToString();
                            propList[i][4].Text = sdr["blocks"].ToString();
                            propList[i][5].Text = sdr["steals"].ToString();
                            propList[i][6].Text = sdr["PA"].ToString();
                            propList[i][7].Text = sdr["PR"].ToString();
                            propList[i][8].Text = sdr["AR"].ToString();
                            propList[i][9].Text = sdr["PAR"].ToString();
                            propList[i][0].Enabled = true;
                            propList[i][1].Enabled = true;
                            propList[i][2].Enabled = true;
                            propList[i][3].Enabled = true;
                            propList[i][4].Enabled = true;
                            propList[i][5].Enabled = true;
                            propList[i][6].Enabled = true;
                            propList[i][7].Enabled = true;
                            propList[i][8].Enabled = true;
                            propList[i][9].Enabled = true;

                            underList[i][0].Checked = true;  underList[i][0].Enabled = true;
                            underList[i][1].Checked = true;  underList[i][1].Enabled = true;
                            underList[i][2].Checked = true;  underList[i][2].Enabled = true;
                            underList[i][3].Checked = true;  underList[i][3].Enabled = true;
                            underList[i][4].Checked = true;  underList[i][4].Enabled = true;
                            underList[i][5].Checked = true;  underList[i][5].Enabled = true;
                            underList[i][6].Checked = true;  underList[i][6].Enabled = true;
                            underList[i][7].Checked = true;  underList[i][7].Enabled = true;
                            underList[i][8].Checked = true;  underList[i][8].Enabled = true;
                            underList[i][9].Checked = true;  underList[i][9].Enabled = true;
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }
            
        }

        protected void btnSmartFill_Click(object sender, EventArgs e)
        {
            List<TextBox> props = new List<TextBox>();
            List<DropDownList> empty = new List<DropDownList>();
            List<CheckBox> unders = new List<CheckBox>();
            List<DropDownList> player = new List<DropDownList>();
            List<List<TextBox>> propList = new List<List<TextBox>>();
            List<List<TextBox>> propUList = new List<List<TextBox>>();
            List<List<CheckBox>> underList = new List<List<CheckBox>>();
            string selectMin = "select Min(p.points) points, Min(p.assists) assists, Min(p.reboundsTotal) reboundsTotal,  Min(p.threePointersMade) threePointersMade,  Min(p.blocks) blocks,  Min(p.steals) steals, Min(p.points) + Min(p.assists) [PA], Min(p.points) + Min(p.reboundsTotal) [PR], Min(p.assists) + Min(p.reboundsTotal) [AR], Min(p.points) + Min(p.assists) + Min(p.reboundsTotal) [PAR]";
            string selectMax = "select Max(p.points) points, Max(p.assists) assists, Max(p.reboundsTotal) reboundsTotal,  Max(p.threePointersMade) threePointersMade,  Max(p.blocks) blocks,  Max(p.steals) steals, Max(p.points) + Max(p.assists) [PA], Max(p.points) + Max(p.reboundsTotal) [PR], Max(p.assists) + Max(p.reboundsTotal) [AR], Max(p.points) + Max(p.assists) + Max(p.reboundsTotal) [PAR]";
            string from = " from playerBox p inner join teamBox t on p.game_id = t.game_id and p.team_id = t.team_id and p.season_id = t.season_id ";
            string where = " where p.season_id = " + ddSeason.SelectedItem.Text + " and p.team_id = " + ddTeams.SelectedItem.Value;
        
            if(ddlDNP.SelectedItem.Text != "DNP Player")
            {
                from = " from playerBox p inner join teamBox t on p.game_id = t.game_id and p.team_id = t.team_id and p.season_id = t.season_id ";
                from += " left join playerbox d on p.game_id = d.game_id and p.team_id = d.team_id and p.season_id = d.season_id ";
                where += " and (d.status != 'ACTIVE' or replace(replace(d.minutesCalculated, 'PT', ''), 'M', '') < (select Minutes from playerBoxAverage a where a.season_id = d.season_id and a.team_id = d.team_id and a.player_id = d.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = d.season_id and a.team_id = d.team_id and a.player_id = d.player_id)/2)) and d.player_id = " + ddlDNP.SelectedItem.Value;
            }
            if (ddlDNP2.SelectedItem.Text != "DNP Player")
            {
                from += " left join playerbox d2 on p.game_id = d2.game_id and p.team_id = d2.team_id and p.season_id = d2.season_id ";
                where += " and (d2.status != 'ACTIVE' or replace(replace(d2.minutesCalculated, 'PT', ''), 'M', '') <  (select Minutes from playerBoxAverage a where a.season_id = d2.season_id and a.team_id = d2.team_id and a.player_id = d2.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = d2.season_id and a.team_id = d2.team_id and a.player_id = d2.player_id)/2)) and d2.player_id = " + ddlDNP2.SelectedItem.Value;
            }
            if (ddlDNP3.SelectedItem.Text != "DNP Player")
            {
                from += " left join playerbox d3 on p.game_id = d3.game_id and p.team_id = d3.team_id and p.season_id = d3.season_id ";
                where += " and (d3.status != 'ACTIVE' or replace(replace(d3.minutesCalculated, 'PT', ''), 'M', '') < (select Minutes from playerBoxAverage a where a.season_id = d3.season_id and a.team_id = d3.team_id and a.player_id = d3.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = d3.season_id and a.team_id = d3.team_id and a.player_id = d3.player_id)/2))  and d3.player_id = " + ddlDNP3.SelectedItem.Value;
            }

            int j = 0;
            foreach (DropDownList roster in t1Rosters)
            {
                if (roster.SelectedItem.Text != "Player")
                {
                    player.Add(roster);
                    propList.Add(txtList[j]);
                }
                else
                {
                    empty.Add(roster);
                    propUList.Add(txtList[j]);
                    underList.Add(chkList[j]);
                }
                j++;
            }

            if (player.Count == 1)
            {
                where += " and p.status = 'ACTIVE' and replace(replace(p.minutesCalculated, 'PT', ''), 'M', '') >  (select cast(Minutes as decimal(18, 2))/2 from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id)";

                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[0].SelectedItem.Value, propList[0]);

                SmartBuilder(selectMax + from + where + " and p.player_id = " + player[0].SelectedItem.Value, propUList[0]);

                empty[0].SelectedIndex = player[0].SelectedIndex;
                foreach (CheckBox check in chkList[1])
                {
                    check.Checked = true; check.Enabled = true;
                }

            }
            if (player.Count == 2)
            {
                from += " inner join playerBox p2 on p.game_id = p2.game_id and p.team_id = p2.team_id and p.season_id = p2.season_id";
                where += " and p.status = 'ACTIVE' and replace(replace(p.minutesCalculated, 'PT', ''), 'M', '') >  (select cast(Minutes as decimal(18, 2))/2 from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id) and p2.status = 'ACTIVE' and replace(replace(p2.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p2.season_id and a.team_id = p2.team_id and a.player_id = p2.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p2.season_id and a.team_id = p2.team_id and a.player_id = p2.player_id)/2)";
                
                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[0].SelectedItem.Value + " and p2.player_id = " + player[1].SelectedItem.Value, propList[0]);
                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[1].SelectedItem.Value + " and p2.player_id = " + player[0].SelectedItem.Value, propList[1]);

                //SmartBuilder(selectMax + from + where + " and p.player_id = " + player[0].SelectedItem.Value + " and p2.player_id = " + player[1].SelectedItem.Value, propUList[0]);
                //SmartBuilder(selectMax + from + where + " and p.player_id = " + player[1].SelectedItem.Value + " and p2.player_id = " + player[0].SelectedItem.Value, propUList[1]);

                //empty[0].SelectedIndex = player[0].SelectedIndex;
                //empty[1].SelectedIndex = player[1].SelectedIndex;
                //foreach (CheckBox check in chkList[2])
                //{
                //    check.Checked = true; check.Enabled = true;
                //}
                //foreach (CheckBox check in chkList[3])
                //{
                //    check.Checked = true; check.Enabled = true;
                //}

            }
            else if (player.Count == 3)
            {
                from += " inner join playerBox p2 on p.game_id = p2.game_id and p.team_id = p2.team_id and p.season_id = p2.season_id inner join playerBox p3 on p.game_id = p3.game_id and p.team_id = p3.team_id and p.season_id = p3.season_id";
                where += " and p.status = 'ACTIVE' and replace(replace(p.minutesCalculated, 'PT', ''), 'M', '') >  (select cast(Minutes as decimal(18, 2))/2 from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id) and p2.status = 'ACTIVE' and replace(replace(p2.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p2.season_id and a.team_id = p2.team_id and a.player_id = p2.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p2.season_id and a.team_id = p2.team_id and a.player_id = p2.player_id)/2) and p3.status = 'ACTIVE' and replace(replace(p3.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p3.season_id and a.team_id = p3.team_id and a.player_id = p3.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p3.season_id and a.team_id = p3.team_id and a.player_id = p3.player_id)/2)";


                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[0].SelectedItem.Value + " and p2.player_id = " + player[1].SelectedItem.Value + " and p3.player_id = " + player[2].SelectedItem.Value , propList[0]);
                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[1].SelectedItem.Value + " and p2.player_id = " + player[2].SelectedItem.Value + " and p3.player_id = " + player[0].SelectedItem.Value , propList[1]);
                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[2].SelectedItem.Value + " and p2.player_id = " + player[0].SelectedItem.Value + " and p3.player_id = " + player[1].SelectedItem.Value , propList[2]);


                //SmartBuilder(selectMax + from + where + " and p.player_id = " + player[0].SelectedItem.Value + " and p2.player_id = " + player[1].SelectedItem.Value + " and p3.player_id = " + player[2].SelectedItem.Value, propUList[0]);
                //SmartBuilder(selectMax + from + where + " and p.player_id = " + player[1].SelectedItem.Value + " and p2.player_id = " + player[2].SelectedItem.Value + " and p3.player_id = " + player[0].SelectedItem.Value, propUList[1]);
                //SmartBuilder(selectMax + from + where + " and p.player_id = " + player[2].SelectedItem.Value + " and p2.player_id = " + player[0].SelectedItem.Value + " and p3.player_id = " + player[1].SelectedItem.Value, propUList[2]);

                //empty[0].SelectedIndex = player[0].SelectedIndex;
                //empty[1].SelectedIndex = player[1].SelectedIndex;
                //empty[2].SelectedIndex = player[2].SelectedIndex;
                //foreach (CheckBox check in chkList[3])
                //{
                //    check.Checked = true; check.Enabled = true;
                //}
                //foreach (CheckBox check in chkList[4])
                //{
                //    check.Checked = true; check.Enabled = true;
                //}
                //foreach (CheckBox check in chkList[5])
                //{
                //    check.Checked = true; check.Enabled = true;
                //}
            }
            else if (player.Count == 4)
            {
                from += " inner join playerBox p2 on p.game_id = p2.game_id and p.team_id = p2.team_id and p.season_id = p2.season_id inner join playerBox p3 on p.game_id = p3.game_id and p.team_id = p3.team_id and p.season_id = p3.season_id inner join playerBox p4 on p.game_id = p4.game_id and p.team_id = p4.team_id and p.season_id = p4.season_id";
                where += " and p.status = 'ACTIVE' and replace(replace(p.minutesCalculated, 'PT', ''), 'M', '') >  (select cast(Minutes as decimal(18, 2))/2 from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id) and p2.status = 'ACTIVE' and replace(replace(p2.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p2.season_id and a.team_id = p2.team_id and a.player_id = p2.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p2.season_id and a.team_id = p2.team_id and a.player_id = p2.player_id)/2) and p3.status = 'ACTIVE' and replace(replace(p3.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p3.season_id and a.team_id = p3.team_id and a.player_id = p3.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p3.season_id and a.team_id = p3.team_id and a.player_id = p3.player_id)/2) and p4.status = 'ACTIVE' and replace(replace(p4.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p4.season_id and a.team_id = p4.team_id and a.player_id = p4.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p4.season_id and a.team_id = p4.team_id and a.player_id = p4.player_id)/2)";
                
                

                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[0].SelectedItem.Value + " and p2.player_id = " + player[1].SelectedItem.Value + " and p3.player_id = " + player[2].SelectedItem.Value + " and p4.player_id = " + player[3].SelectedItem.Value, propList[0]);
                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[1].SelectedItem.Value + " and p2.player_id = " + player[0].SelectedItem.Value + " and p3.player_id = " + player[3].SelectedItem.Value + " and p4.player_id = " + player[2].SelectedItem.Value, propList[1]);
                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[2].SelectedItem.Value + " and p2.player_id = " + player[3].SelectedItem.Value + " and p3.player_id = " + player[0].SelectedItem.Value + " and p4.player_id = " + player[1].SelectedItem.Value, propList[2]);
                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[3].SelectedItem.Value + " and p2.player_id = " + player[2].SelectedItem.Value + " and p3.player_id = " + player[1].SelectedItem.Value + " and p4.player_id = " + player[0].SelectedItem.Value, propList[3]);

                //SmartBuilder(selectMax + from + where + " and p.player_id = " + player[0].SelectedItem.Value + " and p2.player_id = " + player[1].SelectedItem.Value + " and p3.player_id = " + player[2].SelectedItem.Value + " and p4.player_id = " + player[3].SelectedItem.Value, propUList[0]);
                //SmartBuilder(selectMax + from + where + " and p.player_id = " + player[1].SelectedItem.Value + " and p2.player_id = " + player[0].SelectedItem.Value + " and p3.player_id = " + player[3].SelectedItem.Value + " and p4.player_id = " + player[2].SelectedItem.Value, propUList[1]);
                //SmartBuilder(selectMax + from + where + " and p.player_id = " + player[2].SelectedItem.Value + " and p2.player_id = " + player[3].SelectedItem.Value + " and p3.player_id = " + player[0].SelectedItem.Value + " and p4.player_id = " + player[1].SelectedItem.Value, propUList[2]);
                //SmartBuilder(selectMax + from + where + " and p.player_id = " + player[3].SelectedItem.Value + " and p2.player_id = " + player[2].SelectedItem.Value + " and p3.player_id = " + player[1].SelectedItem.Value + " and p4.player_id = " + player[0].SelectedItem.Value, propUList[3]);

                //empty[0].SelectedIndex = player[0].SelectedIndex;
                //empty[1].SelectedIndex = player[1].SelectedIndex;
                //empty[2].SelectedIndex = player[2].SelectedIndex;
                //empty[3].SelectedIndex = player[3].SelectedIndex;
                //foreach (CheckBox check in chkList[4])
                //{
                //    check.Checked = true; check.Enabled = true;
                //}
                //foreach (CheckBox check in chkList[5])
                //{
                //    check.Checked = true; check.Enabled = true;
                //}
                //foreach (CheckBox check in chkList[6])
                //{
                //    check.Checked = true; check.Enabled = true;
                //}
                //foreach (CheckBox check in chkList[7])
                //{
                //    check.Checked = true; check.Enabled = true;
                //}
            }
            else if (player.Count == 5)
            {
                from += " inner join playerBox p2 on p.game_id = p2.game_id and p.team_id = p2.team_id and p.season_id = p2.season_id inner join playerBox p3 on p.game_id = p3.game_id and p.team_id = p3.team_id and p.season_id = p3.season_id inner join playerBox p4 on p.game_id = p4.game_id and p.team_id = p4.team_id and p.season_id = p4.season_id inner join playerBox p5 on p.game_id = p5.game_id and p.team_id = p5.team_id and p.season_id = p5.season_id";
                where += " and p.status = 'ACTIVE' and replace(replace(p.minutesCalculated, 'PT', ''), 'M', '') >  (select cast(Minutes as decimal(18, 2))/2 from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id) and p2.status = 'ACTIVE' and replace(replace(p2.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p2.season_id and a.team_id = p2.team_id and a.player_id = p2.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p2.season_id and a.team_id = p2.team_id and a.player_id = p2.player_id)/2) and p3.status = 'ACTIVE' and replace(replace(p3.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p3.season_id and a.team_id = p3.team_id and a.player_id = p3.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p3.season_id and a.team_id = p3.team_id and a.player_id = p3.player_id)/2) and p4.status = 'ACTIVE' and replace(replace(p4.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p4.season_id and a.team_id = p4.team_id and a.player_id = p4.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p4.season_id and a.team_id = p4.team_id and a.player_id = p4.player_id)/2) and p5.status = 'ACTIVE' and replace(replace(p5.minutesCalculated, 'PT', ''), 'M', '') >  (select Minutes from playerBoxAverage a where a.season_id = p5.season_id and a.team_id = p5.team_id and a.player_id = p5.player_id) - ((select Minutes from playerBoxAverage a where a.season_id = p5.season_id and a.team_id = p5.team_id and a.player_id = p5.player_id)/2)";


                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[0].SelectedItem.Value + " and p2.player_id = " + player[1].SelectedItem.Value + " and p3.player_id = " + player[2].SelectedItem.Value + " and p4.player_id = " + player[3].SelectedItem.Value + " and p5.player_id = " + player[4].SelectedItem.Value, propList[0]);
                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[1].SelectedItem.Value + " and p2.player_id = " + player[0].SelectedItem.Value + " and p3.player_id = " + player[3].SelectedItem.Value + " and p4.player_id = " + player[2].SelectedItem.Value + " and p5.player_id = " + player[4].SelectedItem.Value, propList[1]);
                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[2].SelectedItem.Value + " and p2.player_id = " + player[3].SelectedItem.Value + " and p3.player_id = " + player[0].SelectedItem.Value + " and p4.player_id = " + player[1].SelectedItem.Value + " and p5.player_id = " + player[4].SelectedItem.Value, propList[2]);
                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[3].SelectedItem.Value + " and p2.player_id = " + player[2].SelectedItem.Value + " and p3.player_id = " + player[1].SelectedItem.Value + " and p4.player_id = " + player[0].SelectedItem.Value + " and p5.player_id = " + player[4].SelectedItem.Value, propList[3]);
                SmartBuilder(selectMin + from + where + " and p.player_id = " + player[4].SelectedItem.Value + " and p2.player_id = " + player[2].SelectedItem.Value + " and p3.player_id = " + player[1].SelectedItem.Value + " and p4.player_id = " + player[0].SelectedItem.Value + " and p5.player_id = " + player[3].SelectedItem.Value, propList[4]);


            }

        }

        public void SmartBuilder(string query, List<TextBox> propList)
        {
            using (SqlCommand querySearch = new SqlCommand(query))
            {
                querySearch.Connection = busDriver.SQLdb;
                querySearch.CommandType = CommandType.Text;
                try
                {
                    busDriver.SQLdb.Open();
                }
                catch (InvalidOperationException ea)
                {
                    busDriver.SQLdb.Close();
                    busDriver.SQLdb.Open();
                }
                using (SqlDataReader sdr = querySearch.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        propList[0].Text = sdr["points"].ToString();
                        propList[1].Text = sdr["assists"].ToString();
                        propList[2].Text = sdr["reboundsTotal"].ToString();
                        propList[3].Text = sdr["threePointersMade"].ToString();
                        propList[4].Text = sdr["blocks"].ToString();
                        propList[5].Text = sdr["steals"].ToString();
                        propList[6].Text = sdr["PA"].ToString();
                        propList[7].Text = sdr["PR"].ToString();
                        propList[8].Text = sdr["AR"].ToString();
                        propList[9].Text = sdr["PAR"].ToString();
                        propList[0].Enabled = true;
                        propList[1].Enabled = true;
                        propList[2].Enabled = true;
                        propList[3].Enabled = true;
                        propList[4].Enabled = true;
                        propList[5].Enabled = true;
                        propList[6].Enabled = true;
                        propList[7].Enabled = true;
                        propList[8].Enabled = true;
                        propList[9].Enabled = true;
                    }
                }
                busDriver.SQLdb.Close();
            }
            foreach(TextBox prop in propList)
            {
                if(prop.Text == "0")
                {
                    prop.Text = "";
                }
            }
        }
    }
  }



//public List<List<TextBox>> txtList = new List<List<TextBox>>();
//public List<List<CheckBox>> chkList = new List<List<CheckBox>>();



//txtList = new List<List<TextBox>>
//            {
//                p1, p2, p3, p4, p5, p6, p7, p8
//            };

//chkList = new List<List<CheckBox>>
//            {
//                p1chk, p2chk, p3chk, p4chk, p5chk, p6chk, p7chk, p8chk
//            };