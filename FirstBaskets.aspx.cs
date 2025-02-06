using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;
using WebGrease.Activities;
using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.Ajax.Utilities;
using static UglyToad.PdfPig.PdfFonts.FontDescriptor;

namespace NBAdb
{
    public partial class FirstBaskets : System.Web.UI.Page
    {
        BusDriver busDriver = new BusDriver();
        public List<DropDownList> teams;
        public List<DropDownList> teamRosters;
        public List<int> gamesPublic = new List<int>();
        public List<int> gamesPublicOp = new List<int>();


        public string firstShotWhere = " and f.game_id in(";
        public string firstShotWhereOp = " and f.game_id in(";



        public List<int> gamesDNPPublic = new List<int>();
        public List<int> gamesDNPPublicOp = new List<int>();

        public string firstShotWhereDNP = " and f.game_id in(";
        public string firstShotWhereOpDNP = " and f.game_id in(";





        public static double teamTotal = 0;
        public static double playerTotal = 0;
        public static double playerFGpct = 0;
        public static double playerImplied = 0;
        public static double tipPct = 0;
        public static double teamDefPct = 0;
        public static double playerShotValueTotal = 0;
        public static double playerShotValueFGpct = 0;


        public static double playerFGpctWeighted = 0;
        public static double playerShotValueFGpctWeighted = 0;

        public static double tipTotal = 0;
        public static double opTipTotal = 0;

        public static double opTipLossPct = 0;
        public static double opMissPct = 0;

        //public static 
        protected void Page_Load(object sender, EventArgs e)
        {
            teams = new List<DropDownList>
            {
                ddTeams, ddOpponent
            };
            teamRosters = new List<DropDownList>
            {
                ddTeamTip, ddShooter
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
                querySearch.Parameters.AddWithValue("@season", 2024);
                using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                {
                    querySearch.Connection = busDriver.SQLdb;
                    sDataSearch.SelectCommand = querySearch;
                    using (DataSet dataT = new DataSet())
                    {
                        try { sDataSearch.Fill(dataT); } catch (SqlException) { }
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
        }



        protected void ddTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlCommand querySearch = new SqlCommand("FirstBasketCenters"))
            {
                querySearch.CommandType = CommandType.StoredProcedure;
                querySearch.Parameters.AddWithValue("@team", ddTeams.SelectedItem.Value);
                using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                {
                    querySearch.Connection = busDriver.SQLdb;
                    sDataSearch.SelectCommand = querySearch;
                    using (DataSet dataT = new DataSet())
                    {
                        try { sDataSearch.Fill(dataT); } catch (SqlException) { }
                        ddTeamTip.DataTextField = dataT.Tables[0].Columns["Player"].ToString();
                        ddTeamTip.DataValueField = dataT.Tables[0].Columns["PlayerID"].ToString();
                        ddTeamTip.DataSource = dataT;
                        ddTeamTip.DataBind();
                        ListItem emptyItem = new ListItem("Player", "");
                        ddTeamTip.Items.Insert(0, emptyItem);                        
                    }
                }
            }
            using (SqlCommand querySearch = new SqlCommand("FirstBasketRoster"))
            {
                querySearch.CommandType = CommandType.StoredProcedure;
                querySearch.Parameters.AddWithValue("@team", ddTeams.SelectedItem.Value);
                querySearch.Parameters.AddWithValue("@season", 2024);
                using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                {
                    querySearch.Connection = busDriver.SQLdb;
                    sDataSearch.SelectCommand = querySearch;
                    using (DataSet dataT = new DataSet())
                    {
                        try { sDataSearch.Fill(dataT); } catch (SqlException) { }                        
                        ddShooter.DataTextField = dataT.Tables[0].Columns["Player"].ToString();
                        ddShooter.DataValueField = dataT.Tables[0].Columns["PlayerID"].ToString();
                        ddShooter.DataSource = dataT;
                        ddShooter.DataBind();
                        ListItem emptyItem = new ListItem("Player", "");
                        ddShooter.Items.Insert(0, emptyItem);
                    }
                }
            }
            using (SqlCommand querySearch = new SqlCommand("FirstShotType"))
            {
                querySearch.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                {
                    querySearch.Connection = busDriver.SQLdb;
                    sDataSearch.SelectCommand = querySearch;
                    using (DataSet dataT = new DataSet())
                    {
                        try { sDataSearch.Fill(dataT); } catch (SqlException) { }
                        ddShotType.DataTextField = dataT.Tables[0].Columns["actionsub"].ToString();
                        ddShotType.DataSource = dataT;
                        ddShotType.DataBind();
                        ListItem emptyItem = new ListItem("Shot Type", "");
                        ddShotType.Items.Insert(0, emptyItem);
                    }
                }
            }



            using (SqlCommand GameCheck = new SqlCommand("TeamAttempts"))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.StoredProcedure;
                GameCheck.Parameters.AddWithValue("@team", ddTeams.SelectedItem.Value);
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        lblTeam.Text = sdr["Makes"].ToString() + "/" + sdr["Total"].ToString();
                        teamTotal = double.Parse(sdr["Total"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }
            using (SqlCommand GameCheck = new SqlCommand("OpponentAttempts"))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.StoredProcedure;
                GameCheck.Parameters.AddWithValue("@team", ddTeams.SelectedItem.Value);
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        lblTeam.Text += " - " + sdr["Makes"].ToString() + "/" + sdr["Total"].ToString();
                        teamDefPct = double.Parse(sdr["Miss"].ToString()) / double.Parse(sdr["Total"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }


            using (SqlCommand querySearch = new SqlCommand("StartingLineupSelect"))
            {
                querySearch.CommandType = CommandType.StoredProcedure;
                querySearch.Parameters.AddWithValue("@team", ddTeams.SelectedItem.Value);
                querySearch.Parameters.AddWithValue("@season", 2024);
                using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                {
                    querySearch.Connection = busDriver.SQLdb;
                    sDataSearch.SelectCommand = querySearch;
                    using (DataSet dataT = new DataSet())
                    {
                        try { sDataSearch.Fill(dataT); } catch (SqlException) { }
                        chkTeamActive.DataTextField = dataT.Tables[0].Columns["PlayerStarts"].ToString();
                        chkTeamActive.DataValueField = dataT.Tables[0].Columns["player_id"].ToString();
                        chkTeamActive.ControlStyle.CssClass = "test";
                        chkTeamActive.DataSource = dataT;
                        chkTeamActive.DataBind();
                    }
                }
            }
            //using (SqlCommand querySearch = new SqlCommand("SelectDNPs"))
            //{
            //    querySearch.CommandType = CommandType.StoredProcedure;
            //    querySearch.Parameters.AddWithValue("@team", ddTeams.SelectedItem.Value);
            //    querySearch.Parameters.AddWithValue("@season", 2024);
            //    using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
            //    {
            //        querySearch.Connection = busDriver.SQLdb;
            //        sDataSearch.SelectCommand = querySearch;
            //        using (DataSet dataT = new DataSet())
            //        {
            //            try { sDataSearch.Fill(dataT); } catch (SqlException) { }
            //            chkTeamDNP.DataTextField = dataT.Tables[0].Columns["PlayerDNPs"].ToString();
            //            chkTeamDNP.DataValueField = dataT.Tables[0].Columns["player_id"].ToString();
            //            chkTeamDNP.ControlStyle.CssClass = "test";
            //            chkTeamDNP.DataSource = dataT;
            //            chkTeamDNP.DataBind();
            //        }
            //    }
            //}

            StartingLineups startingLineups = new StartingLineups();
            startingLineups.GetStarters(Int32.Parse(ddTeams.SelectedItem.Value), hName, hPGName, hSGName, hSFName, hPFName, hCName, aName, aPGName, aSGName, aSFName, aPFName, aCName, time);
            if (hName.Text != "")
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

        }

        protected void ddOpponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlCommand querySearch = new SqlCommand("FirstBasketCenters"))
            {
                querySearch.CommandType = CommandType.StoredProcedure;
                querySearch.Parameters.AddWithValue("@team", ddOpponent.SelectedItem.Value);
                using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                {
                    querySearch.Connection = busDriver.SQLdb;
                    sDataSearch.SelectCommand = querySearch;
                    using (DataSet dataT = new DataSet())
                    {
                        try { sDataSearch.Fill(dataT); } catch (SqlException) { }
                        ddOpTip.DataTextField = dataT.Tables[0].Columns["Player"].ToString();
                        ddOpTip.DataValueField = dataT.Tables[0].Columns["PlayerID"].ToString();
                        ddOpTip.DataSource = dataT;
                        ddOpTip.DataBind();
                        ListItem emptyItem = new ListItem("Player", "");
                        ddOpTip.Items.Insert(0, emptyItem);
                    }
                }
            }
            using (SqlCommand GameCheck = new SqlCommand("TeamAttempts"))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.StoredProcedure;
                GameCheck.Parameters.AddWithValue("@team", ddOpponent.SelectedItem.Value);
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        lblOpponent.Text = sdr["Makes"].ToString() + "/" + sdr["Total"].ToString();
                        opMissPct = double.Parse(sdr["Miss"].ToString()) /double.Parse(sdr["Total"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }
            using (SqlCommand GameCheck = new SqlCommand("OpponentAttempts"))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.StoredProcedure;
                GameCheck.Parameters.AddWithValue("@team", ddOpponent.SelectedItem.Value);
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        lblOpponent.Text += " - " + sdr["Makes"].ToString() + "/" + sdr["Total"].ToString();
                    }
                }
                busDriver.SQLdb.Close();
            }

            using (SqlCommand querySearch = new SqlCommand("StartingLineupSelect"))
            {
                querySearch.CommandType = CommandType.StoredProcedure;
                querySearch.Parameters.AddWithValue("@team", ddOpponent.SelectedItem.Value);
                querySearch.Parameters.AddWithValue("@season", 2024);
                using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                {
                    querySearch.Connection = busDriver.SQLdb;
                    sDataSearch.SelectCommand = querySearch;
                    using (DataSet dataT = new DataSet())
                    {
                        try { sDataSearch.Fill(dataT); } catch (SqlException) { }
                        chkOpponentActive.DataTextField = dataT.Tables[0].Columns["PlayerStarts"].ToString();
                        chkOpponentActive.DataValueField = dataT.Tables[0].Columns["player_id"].ToString();
                        chkOpponentActive.ControlStyle.CssClass = "test";
                        chkOpponentActive.DataSource = dataT;
                        chkOpponentActive.DataBind();
                    }
                }
            }
            //using (SqlCommand querySearch = new SqlCommand("SelectDNPs"))
            //{
            //    querySearch.CommandType = CommandType.StoredProcedure;
            //    querySearch.Parameters.AddWithValue("@team", ddOpponent.SelectedItem.Value);
            //    querySearch.Parameters.AddWithValue("@season", 2024);
            //    using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
            //    {
            //        querySearch.Connection = busDriver.SQLdb;
            //        sDataSearch.SelectCommand = querySearch;
            //        using (DataSet dataT = new DataSet())
            //        {
            //            try { sDataSearch.Fill(dataT); } catch (SqlException) { }
            //            chkOpponentDNP.DataTextField = dataT.Tables[0].Columns["PlayerDNPs"].ToString();
            //            chkOpponentDNP.DataValueField = dataT.Tables[0].Columns["player_id"].ToString();
            //            chkOpponentDNP.ControlStyle.CssClass = "test";
            //            chkOpponentDNP.DataSource = dataT;
            //            chkOpponentDNP.DataBind();
            //        }
            //    }
            //}
        }

        protected void ddTeamTip_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlCommand GameCheck = new SqlCommand("TipWins"))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.StoredProcedure;
                GameCheck.Parameters.AddWithValue("@player", ddTeamTip.SelectedItem.Value);
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        lblTipPct.Text = sdr["Wins"].ToString() + "-" + sdr["Losses"].ToString();
                        tipPct = double.Parse(sdr["Wins"].ToString()) / (double.Parse(sdr["Wins"].ToString()) + double.Parse(sdr["Losses"].ToString()));
                        tipTotal = double.Parse(sdr["Wins"].ToString()) + double.Parse(sdr["Losses"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }
        }

        protected void ddOpTip_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlCommand GameCheck = new SqlCommand("TipWins"))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.StoredProcedure;
                GameCheck.Parameters.AddWithValue("@player", ddOpTip.SelectedItem.Value);
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        lblOpTipPct.Text = sdr["Wins"].ToString() + "-" + sdr["Losses"].ToString();
                        opTipLossPct = double.Parse(sdr["Losses"].ToString()) / (double.Parse(sdr["Wins"].ToString()) + double.Parse(sdr["Losses"].ToString()));
                        opTipTotal = double.Parse(sdr["Wins"].ToString()) + double.Parse(sdr["Losses"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }
        }

        protected void ddShooter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<int, string> teamActives = new Dictionary<int, string>();
            Dictionary<int, string> teamDNPs = new Dictionary<int, string>();
            if (ddShooter.SelectedIndex != 0)
            {
                firstShotWhere = "";
                int count = 0;
                foreach (ListItem item in chkTeamActive.Items)
                {
                    if (item.Selected)
                    {
                        count++;
                    }
                }
                string firstShotSelectActive = "select f.season_id, f.team, f.player, sum(case when Result = 'Make' then 1 else 0 end) Makes, sum(case when Result != 'Make' then 1 else 0 end) Miss, count(Shot) Total, cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 5))/cast(count(Shot)as decimal(18, 5)) as decimal(18, 5)) Pct, FGM, FGA, [FG%] FGpct, FG3M , FG3A , [FG3%] FG3pct, FG2M, FG2A, [FG2%] FG2pct, FTM, FTA, [FT%] FTpct from FirstBaskets f inner join PlayerTrend t on f.player_id = t.player_id where f.season_id = 2024 and f.player_id =" + ddShooter.SelectedItem.Value;
                if (count > 0)
                {
                    GetActives(teamActives, chkTeamActive, "team_id", "Active");
                }

                string firstShotGroupBy = " group by f.season_id, f.team, f.player, FGM, FGA, [FG%], FG3M, FG3A, [FG3%] , FG2M, FG2A, [FG2%], FTM, FTA, [FT%] ";
                string firstShot = firstShotSelectActive + firstShotWhere + firstShotGroupBy;
                using (SqlCommand GameCheck = new SqlCommand(firstShot))
                {
                    GameCheck.Connection = busDriver.SQLdb;
                    GameCheck.CommandType = CommandType.Text;
                    busDriver.SQLdb.Open();
                    using (SqlDataReader sdr = GameCheck.ExecuteReader())
                    {
                        int i = 0;
                        while (sdr.Read())
                        {
                            lblShooter.Text = sdr["Makes"].ToString() + "/" + sdr["Total"].ToString();
                            playerTotal = double.Parse(sdr["Total"].ToString());
                            playerFGpct = double.Parse(sdr["Makes"].ToString()) / double.Parse(sdr["Total"].ToString());
                            playerFGpctWeighted = double.Parse(sdr["FGpct"].ToString())/100;
                            i++;
                        }
                        if (i == 0)
                        {
                            lblShooter.Text = "No attempts";
                            playerTotal = 0;
                            playerFGpct = 0;
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }
            else
            {
                lblShooter.Text = "";
                playerTotal = 0;
                playerFGpct = 0;
                playerFGpctWeighted = 0;
            }
            
        }
        protected void ddShotValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<int, string> teamActives = new Dictionary<int, string>();
            if (ddShotValue.SelectedIndex != 0)
            {
                firstShotWhere = "";
                int count = 0;
                foreach (ListItem item in chkTeamActive.Items)
                {
                    if (item.Selected)
                    {
                        count++;
                    }
                }
                string firstShotSelectActive = "select f.season_id, f.team, f.player, f.Shot, sum(case when Result = 'Make' then 1 else 0 end) Makes, sum(case when Result != 'Make' then 1 else 0 end) Miss, count(Shot) Total, cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 5))/cast(count(Shot)as decimal(18, 5)) as decimal(18, 5)) Pct, FGM, FGA, [FG%] FGpct, FG3M , FG3A , [FG3%] FG3pct, FG2M, FG2A, [FG2%] FG2pct, FTM, FTA, [FT%] FTpct from FirstBaskets f inner join PlayerTrend t on f.player_id = t.player_id where f.season_id = 2024 and f.player_id = " + ddShooter.SelectedItem.Value + " and f.shot = '" + ddShotValue.SelectedItem.Text + "' ";
                if (count > 0)
                {
                    GetActives(teamActives, chkTeamActive, "team_id", "Active");
                }
                string firstShotGroupBy = " group by f.season_id, f.team, f.player, f.Shot, FGM, FGA, [FG%], FG3M, FG3A, [FG3%] , FG2M, FG2A, [FG2%], FTM, FTA, [FT%] ";
                string firstShot = firstShotSelectActive + firstShotWhere + firstShotGroupBy;
                using (SqlCommand GameCheck = new SqlCommand(firstShot))
                {
                    GameCheck.Connection = busDriver.SQLdb;
                    GameCheck.CommandType = CommandType.Text;
                    //GameCheck.Parameters.AddWithValue("@shot", ddShotValue.SelectedItem.Value);
                    //GameCheck.Parameters.AddWithValue("@player", ddShooter.SelectedItem.Value);
                    busDriver.SQLdb.Open();
                    using (SqlDataReader sdr = GameCheck.ExecuteReader())
                    {
                        int i = 0;
                        while (sdr.Read())
                        {
                            lblShotValue.Text = sdr["Makes"].ToString() + "/" + sdr["Total"].ToString();
                            playerShotValueTotal = double.Parse(sdr["Total"].ToString());
                            playerShotValueFGpct = double.Parse(sdr["Makes"].ToString()) / double.Parse(sdr["Total"].ToString());
                            if(ddShotValue.SelectedItem.Text == "FG2")
                            {
                                playerShotValueFGpctWeighted = double.Parse(sdr["FG2pct"].ToString())/100;
                            }
                            else if(ddShotValue.SelectedItem.Text == "FG3")
                            {
                                playerShotValueFGpctWeighted = double.Parse(sdr["FG3pct"].ToString()) / 100;
                            }
                            else if (ddShotValue.SelectedItem.Text == "FT")
                            {
                                playerShotValueFGpctWeighted = double.Parse(sdr["FTpct"].ToString()) / 100;
                            }
                            i++;
                        }
                        if (i == 0)
                        {
                            lblShotValue.Text = "No " + ddShotValue.Text + " shots";
                            playerShotValueTotal = 0;
                            playerShotValueFGpct = 0;
                            playerShotValueFGpctWeighted = 0;
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }
            else
            {
                lblShotValue.Text = "";
                playerShotValueTotal = 0;
                playerShotValueFGpct = 0;
                playerShotValueFGpctWeighted = 0;
            }
            int shooter = ddShotValue.SelectedIndex;
            ddShooter_SelectedIndexChanged(sender, e);
        }

        protected void ddShotType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        public void GetActives(Dictionary<int, string> Actives, CheckBoxList Roster, string TeamOrOpp, string ActiveorActive)
        {
            string minutes = " = '00'";
            string andOr = " or ";
            string status = "!=";
            if (ActiveorActive.Contains("Active"))
            {
                minutes = " != '00'";
                andOr = " and ";
                status = "!=";
            }
            foreach (ListItem item in Roster.Items)
            {
                if (item.Selected)
                {
                    Actives.Add(Int32.Parse(item.Value), item.Text);
                }
            }
            List<int> games = new List<int>();
            string selectFrom = "select distinct f.game_id from FirstBaskets f inner join StartingLineups d1 on f.game_id = d1.game_id and f." + TeamOrOpp + " = d1.team_id and f.season_id = d1.season_id ";
            string where = " where ";
            int i = 1;
            foreach (KeyValuePair<int, string> item in Actives)
            {
                if (i > 1)
                {
                    selectFrom += " inner join StartingLineups d" + i + " on f.game_id = d" + i + ".game_id and f." + TeamOrOpp + " = d" + i + ".team_id and f.season_id = d" + i + ".season_id ";
                }
                where += " d" + i + ".player_id = " + item.Key + " and d" + i + ".position " + status + " '' ";

                if (i != Actives.Count)
                {
                    where += " and ";
                }
                i++;
            }
            using (SqlCommand GameCheck = new SqlCommand(selectFrom + where))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {

                    if (TeamOrOpp == "team_id")
                    {
                        int j = 0;
                        if (sdr.HasRows)
                        {
                            firstShotWhere = " and f.game_id in(";
                        }
                        while (sdr.Read())
                        {
                            games.Add(sdr.GetInt32(0));
                            firstShotWhere += sdr.GetInt32(0) + ",";
                            j++;
                        }
                        if (j > 0)
                        {
                            firstShotWhere = firstShotWhere.TrimEnd(',') + ") ";

                            lblWarning.Text = "";
                            lblWarning.Visible = false;
                            lblShooter.ForeColor = System.Drawing.Color.White;
                            lblShotValue.ForeColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            lblWarning.Visible = true;
                            lblWarning.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
                            lblWarning.Text = "No games found with Actives, showing total.";
                            lblShooter.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
                            lblShotValue.ForeColor = System.Drawing.Color.LightGoldenrodYellow;
                        }
                        gamesPublic = new List<int>(games);
                    }
                    else
                    {
                        int j = 0;
                        if (sdr.HasRows)
                        {
                            firstShotWhereOp = " and f.game_id in(";
                        }
                        while (sdr.Read())
                        {
                            games.Add(sdr.GetInt32(0));
                            firstShotWhereOp += sdr.GetInt32(0) + ",";
                            j++;
                        }
                        if (j > 0)
                        {
                            firstShotWhereOp = firstShotWhereOp.TrimEnd(',') + ") ";

                            //lblWarning.Text = "";
                            //lblWarning.Visible = false;
                            //lblShooter.ForeColor = System.Drawing.Color.White;
                            //lblShotValue.ForeColor = System.Drawing.Color.White;
                        }
                        gamesPublicOp = new List<int>(games);
                    }

                }
                busDriver.SQLdb.Close();
            }
        }


        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            Dictionary<int, string> teamActives = new Dictionary<int, string>();
            Dictionary<int, string> opActives = new Dictionary<int, string>();
            int count = 0;
            foreach (ListItem item in chkTeamActive.Items)
            {
                if (item.Selected)
                {
                    count++;
                }
            }
            int countOp = 0;
            foreach (ListItem item in chkOpponentActive.Items)
            {
                if (item.Selected)
                {
                    countOp++;
                }
            }
            if (count > 0)
            {
                GetActives(teamActives, chkTeamActive, "team_id", "Active");
            }
            if (countOp > 0)
            {
                GetActives(opActives, chkOpponentActive, "Opponent_id", "Active");
            }
            string query = "";
            string selectFromOp = "select f.season_id, f.Opponent, f.Shot, sum(case when Result = 'Make' then 1 else 0 end) Makes, sum(case when Result != 'Make' then 1 else 0 end) Miss, count(Shot) ShotTotal, cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 5))/cast(count(Shot)as decimal(18, 5)) as decimal(18, 5)) Pct, (select count(Shot) from FirstBaskets b where f.Opponent = b.Opponent and f.season_id = b.season_id) Total from FirstBaskets f where f.season_id = 2024 and f.Opponent_id = " + ddOpponent.SelectedItem.Value;
            string groupOp = " group by f.season_id, f.Opponent, f.Shot";


            //using (SqlCommand GameCheck = new SqlCommand(selectFromOp + groupOp))
            //{
            //    GameCheck.Connection = busDriver.SQLdb;
            //    GameCheck.CommandType = CommandType.Text;
            //    busDriver.SQLdb.Open();
            //    using (SqlDataReader sdr = GameCheck.ExecuteReader())
            //    {
            //        while (sdr.Read())
            //        {
            //            lblTeam.Text = sdr["Makes"].ToString() + "/" + sdr["Total"].ToString();
            //            teamTotal = double.Parse(sdr["Total"].ToString());
            //        }
            //    }
            //    busDriver.SQLdb.Close();
            //}


            double p = 0;//Player Implied Probability
            if (ddShotValue.SelectedIndex != 0)
            {
                p = (playerShotValueTotal / teamTotal) * playerShotValueFGpct;
            }
            else
            {
                p = (playerTotal / teamTotal) * playerFGpct;
            }
            double pWeighted = 0;
            if (ddShotValue.SelectedIndex != 0)
            {
                pWeighted = (playerShotValueTotal / teamTotal) * (((playerShotValueTotal / teamTotal) * .6) + (playerShotValueFGpctWeighted * .4));
            }
            else
            {
                pWeighted = (playerTotal / teamTotal) * (((playerTotal / teamTotal) * .6) + (playerFGpct * .4));
            }



            double c = 0; //Tip win pct. If the opponent center is filled out, average our center's win % with their center's loss %

            double o = opMissPct;

            double td = teamDefPct;

            double b = 0;

            if (ddOpponent.SelectedIndex != 0)
            {
                b = (o + td) / 2;
            }
            else
            {
                b = td;
            }

            if (ddTeamTip.SelectedIndex != 0 && ddOpTip.SelectedIndex == 0)
            {
                c = tipPct;
            }
            else if (ddTeamTip.SelectedIndex != 0 && ddOpTip.SelectedIndex != 0)
            {
                c = (tipPct + opTipLossPct) / 2;
            }

            double implied = Math.Round(((p * c) + ((b * p) * (1 - c))) * 100, 2);
            lblOdds.Text = implied.ToString() + "%";


            double impliedWeighted = Math.Round(((pWeighted * c) + ((b * pWeighted) * (1 - c))) * 100, 2);
            //lblOddsWeighted.Text = impliedWeighted.ToString() + "%";

            lblOddsProbability.Text = "+" + Math.Round((100 / (implied / 100)) - 100).ToString();
            GetStats();

        }



        public void GetStats()
        {
            //double tTypeDetailMakes = 0; double tTypeDetailAtt = 0; double tTypeDetailPct = 0; double opDefTypeDetailMakes = 0; double opDefTypeDetailAtt = 0; double opDefTypeDetailPct = 0; double pTypeDetailMakes = 0; double pTypeDetailAtt = 0; double pTypeDetailPct = 0;


            double pMakes = 0;
            double pAtt = 0;
            double pPct = 0;

            double p1Makes = 0;
            double p1Att = 0;
            double p1Pct = 0;

            double p2Makes = 0;
            double p2Att = 0;
            double p2Pct = 0;

            double p3Makes = 0;
            double p3Att = 0;
            double p3Pct = 0;

            double tMakes = 0;
            double tAtt = 0;
            double tPct = 0;

            double t1Makes = 0;
            double t1Att = 0;
            double t1Pct = 0;

            double t2Makes = 0;
            double t2Att = 0;
            double t2Pct = 0;

            double t3Makes = 0;
            double t3Att = 0;
            double t3Pct = 0;


            double opDefMakes = 0;
            double opDefAtt = 0;
            double opDefPct = 0;

            double opDef1Makes = 0;
            double opDef1Att = 0;
            double opDef1Pct = 0;

            double opDef2Makes = 0;
            double opDef2Att = 0;
            double opDef2Pct = 0;

            double opDef3Makes = 0;
            double opDef3Att = 0;
            double opDef3Pct = 0;




            double opDefMakesPG = 0; double opDef1MakesPG = 0; double opDef2MakesPG = 0; double opDef3MakesPG = 0;
            double opDefAttPG = 0; double opDef1AttPG = 0; double opDef2AttPG = 0; double opDef3AttPG = 0;
            double opDefPctPG = 0; double opDef1PctPG = 0; double opDef2PctPG = 0; double opDef3PctPG = 0;

            double opDefMakesSG = 0; double opDef1MakesSG = 0; double opDef2MakesSG = 0; double opDef3MakesSG = 0;
            double opDefAttSG = 0; double opDef1AttSG = 0; double opDef2AttSG = 0; double opDef3AttSG = 0;
            double opDefPctSG = 0; double opDef1PctSG = 0; double opDef2PctSG = 0; double opDef3PctSG = 0;


            double opDefMakesSF = 0; double opDef1MakesSF = 0; double opDef2MakesSF = 0; double opDef3MakesSF = 0;
            double opDefAttSF = 0; double opDef1AttSF = 0; double opDef2AttSF = 0; double opDef3AttSF = 0;
            double opDefPctSF = 0; double opDef1PctSF = 0; double opDef2PctSF = 0; double opDef3PctSF = 0;

            double opDefMakesPF = 0; double opDef1MakesPF = 0; double opDef2MakesPF = 0; double opDef3MakesPF = 0;
            double opDefAttPF = 0; double opDef1AttPF = 0; double opDef2AttPF = 0; double opDef3AttPF = 0;
            double opDefPctPF = 0; double opDef1PctPF = 0; double opDef2PctPF = 0; double opDef3PctPF = 0;

            double opDefMakesC = 0; double opDef1MakesC = 0; double opDef2MakesC = 0; double opDef3MakesC = 0;
            double opDefAttC = 0; double opDef1AttC = 0; double opDef2AttC = 0; double opDef3AttC = 0;
            double opDefPctC = 0; double opDef1PctC = 0; double opDef2PctC = 0; double opDef3PctC = 0;








            double opMiss = 0;
            double opAtt = 0;
            double opPct = 0;

            double op1Miss = 0;
            double op1Att = 0;
            double op1Pct = 0;

            double op2Miss = 0;
            double op2Att = 0;
            double op2Pct = 0;

            double op3Miss = 0;
            double op3Att = 0;
            double op3Pct = 0;



            double tDefMiss = 0;
            double tDefAtt = 0;
            double tDefPct = 0;

            double tDef1Miss = 0;
            double tDef1Att = 0;
            double tDef1Pct = 0;

            double tDef2Miss = 0;
            double tDef2Att = 0;
            double tDef2Pct = 0;

            double tDef3Miss = 0;
            double tDef3Att = 0;
            double tDef3Pct = 0;


            double TipWinPct = 0;
            if (ddTeamTip.SelectedIndex != 0 && ddOpTip.SelectedIndex == 0)
            {
                TipWinPct = tipPct;
            }
            else if (ddTeamTip.SelectedIndex != 0 && ddOpTip.SelectedIndex != 0)
            {
                TipWinPct = (tipPct + opTipLossPct) / 2;
            }


            if(firstShotWhere == " and f.game_id in(")
            {
                firstShotWhere = "";
            }
            if (firstShotWhereOp == " and f.game_id in(")
            {
                firstShotWhereOp = "";
            }

            string team = "";
            string opponent = "";
            string position = "";
            int starts = 0;



            using (SqlCommand GetPosition = new SqlCommand("GetPositionTonight"))
            {
                GetPosition.Connection = busDriver.SQLdb;
                GetPosition.CommandType = CommandType.StoredProcedure;
                GetPosition.Parameters.AddWithValue("@player", ddShooter.SelectedItem.Value);
                GetPosition.Parameters.AddWithValue("@team", ddTeams.SelectedItem.Value);
                GetPosition.Parameters.AddWithValue("@season", 2024);
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GetPosition.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        position = sdr["position"].ToString();
                        starts = Int32.Parse(sdr["starts"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }
            if (position.IsNullOrWhiteSpace())
            {
                using (SqlCommand GetPosition = new SqlCommand("GetPosition"))
                {
                    GetPosition.Connection = busDriver.SQLdb;
                    GetPosition.CommandType = CommandType.StoredProcedure;
                    GetPosition.Parameters.AddWithValue("@player", ddShooter.SelectedItem.Value);
                    GetPosition.Parameters.AddWithValue("@team", ddTeams.SelectedItem.Value);
                    GetPosition.Parameters.AddWithValue("@season", 2024);
                    busDriver.SQLdb.Open();
                    using (SqlDataReader sdr = GetPosition.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            position = sdr["position"].ToString();
                            starts = Int32.Parse(sdr["starts"].ToString());
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }


            //Player Shot Totals
            string selectFromWhere = "select f.season_id, f.team, f.player, " +
                                     "sum(case when Result = 'Make' then 1 else 0 end) Makes, " +
                                     "sum(case when Result != 'Make' then 1 else 0 end) Miss, " +
                                     "count(Shot) Total," +
                                     "cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 5))/cast(count(Shot)as decimal(18, 5)) as decimal(18, 5)) Pct " +
                                     "from FirstBaskets f " +
                                     "where f.season_id = 2024 and f.player_id = " + ddShooter.SelectedItem.Value + " " + firstShotWhere;
            string groupBy = " group by f.season_id, f.team, f.player";
            using (SqlCommand PlayerTotals = new SqlCommand(selectFromWhere + groupBy))
            {
                PlayerTotals.Connection = busDriver.SQLdb;
                PlayerTotals.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = PlayerTotals.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        team = sdr["team"].ToString();
                        pMakes = double.Parse(sdr["Makes"].ToString());
                        pAtt = double.Parse(sdr["Total"].ToString());
                        pPct = float.Parse(sdr["Pct"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }

            //Player Shot Type
            selectFromWhere = selectFromWhere.Replace("f.player, ", "f.player, f.Shot, ");
            groupBy = groupBy.Replace("f.player", "f.player, f.Shot");
            using (SqlCommand PlayerShotType = new SqlCommand(selectFromWhere + groupBy))
            {
                PlayerShotType.Connection = busDriver.SQLdb;
                PlayerShotType.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = PlayerShotType.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if(sdr["Shot"].ToString() == "FT")
                        {
                            p1Makes = double.Parse(sdr["Makes"].ToString());
                            p1Att = double.Parse(sdr["Total"].ToString());
                            p1Pct = float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["Shot"].ToString() == "FG2")
                        {
                            p2Makes = double.Parse(sdr["Makes"].ToString());
                            p2Att = double.Parse(sdr["Total"].ToString());
                            p2Pct = float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["Shot"].ToString() == "FG3")
                        {
                            p3Makes = double.Parse(sdr["Makes"].ToString());
                            p3Att = double.Parse(sdr["Total"].ToString());
                            p3Pct = float.Parse(sdr["Pct"].ToString());
                        }                        
                    }
                }
                busDriver.SQLdb.Close();
            }

            //Team Totals
            selectFromWhere = "select f.season_id, f.team, " +
                "sum(case when Result = 'Make' then 1 else 0 end) Makes, " +
                "sum(case when Result != 'Make' then 1 else 0 end) Miss, " +
                "count(Shot) Total, " +
                "cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 5))/cast(count(Shot)as decimal(18, 5)) as decimal(18, 5)) Pct " +
                "from FirstBaskets f " +
                "where f.season_id = 2024 and f.team_id = " + ddTeams.SelectedItem.Value + " " + firstShotWhere;
            groupBy = " group by f.season_id, f.team";
            using (SqlCommand TeamTotals = new SqlCommand(selectFromWhere + groupBy))
            {
                TeamTotals.Connection = busDriver.SQLdb;
                TeamTotals.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = TeamTotals.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        tMakes = double.Parse(sdr["Makes"].ToString());
                        tAtt = double.Parse(sdr["Total"].ToString());
                        tPct = float.Parse(sdr["Pct"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }


            //Team Shot Type
            selectFromWhere = selectFromWhere.Replace("f.team, ", "f.team, f.Shot, ");
            groupBy = groupBy.Replace("f.team", "f.team, f.Shot");
            using (SqlCommand TeamShotType = new SqlCommand(selectFromWhere + groupBy))
            {
                TeamShotType.Connection = busDriver.SQLdb;
                TeamShotType.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = TeamShotType.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Shot"].ToString() == "FT")
                        {
                            t1Makes = double.Parse(sdr["Makes"].ToString());
                            t1Att = double.Parse(sdr["Total"].ToString());
                            t1Pct = float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["Shot"].ToString() == "FG2")
                        {
                            t2Makes = double.Parse(sdr["Makes"].ToString());
                            t2Att = double.Parse(sdr["Total"].ToString());
                            t2Pct = float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["Shot"].ToString() == "FG3")
                        {
                            t3Makes = double.Parse(sdr["Makes"].ToString());
                            t3Att = double.Parse(sdr["Total"].ToString());
                            t3Pct = float.Parse(sdr["Pct"].ToString());
                        }
                    }
                }
                busDriver.SQLdb.Close();
            }

            //Opponent Miss Totals
            selectFromWhere = "select f.season_id, f.team, " +
                "sum(case when Result = 'Make' then 1 else 0 end) Makes, " +
                "sum(case when Result != 'Make' then 1 else 0 end) Miss, " +
                "count(Shot) Total, " +
                "cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 5))/cast(count(Shot)as decimal(18, 5)) as decimal(18, 5)) Pct " +
                "from FirstBaskets f " +
                "where f.season_id = 2024 and f.team_id = " + ddOpponent.SelectedItem.Value + " " + firstShotWhereOp;
            groupBy = " group by f.season_id, f.team";
            using (SqlCommand TeamTotals = new SqlCommand(selectFromWhere + groupBy))
            {
                TeamTotals.Connection = busDriver.SQLdb;
                TeamTotals.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = TeamTotals.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        opMiss = double.Parse(sdr["Miss"].ToString());
                        opAtt = double.Parse(sdr["Total"].ToString());
                        opPct = 1 - float.Parse(sdr["Pct"].ToString());
                        opponent = sdr["team"].ToString();
                    }
                }
                busDriver.SQLdb.Close();
            }


            //Opponent Shot Miss Type
            selectFromWhere = selectFromWhere.Replace("f.team, ", "f.team, f.Shot, ");
            groupBy = groupBy.Replace("f.team", "f.team, f.Shot");
            using (SqlCommand TeamShotType = new SqlCommand(selectFromWhere + groupBy))
            {
                TeamShotType.Connection = busDriver.SQLdb;
                TeamShotType.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = TeamShotType.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Shot"].ToString() == "FT")
                        {
                            op1Miss = double.Parse(sdr["Miss"].ToString());
                            op1Att = double.Parse(sdr["Total"].ToString());
                            op1Pct = float.Parse(sdr["Pct"].ToString());
                            op1Pct = 1 - op1Pct;
                        }
                        else if (sdr["Shot"].ToString() == "FG2")
                        {
                            op2Miss = double.Parse(sdr["Miss"].ToString());
                            op2Att = double.Parse(sdr["Total"].ToString());
                            op2Pct = float.Parse(sdr["Pct"].ToString());
                            op2Pct = 1 - op2Pct;
                        }
                        else if (sdr["Shot"].ToString() == "FG3")
                        {
                            op3Miss = double.Parse(sdr["Miss"].ToString());
                            op3Att = double.Parse(sdr["Total"].ToString());
                            op3Pct = float.Parse(sdr["Pct"].ToString());
                            op3Pct = 1 - op3Pct;
                        }
                    }
                }
                busDriver.SQLdb.Close();
            }



            //Opponent Defense Totals
            selectFromWhere = "select f.season_id, f.opponent, " +
                "sum(case when Result = 'Make' then 1 else 0 end) Makes, " +
                "sum(case when Result != 'Make' then 1 else 0 end) Miss, " +
                "count(Shot) Total, " +
                "cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 5))/cast(count(Shot)as decimal(18, 5)) as decimal(18, 5)) Pct " +
                "from FirstBaskets f " +
                "where f.season_id = 2024 and f.opponent_id = " + ddOpponent.SelectedItem.Value + " " + firstShotWhereOp;
            groupBy = " group by f.season_id, f.opponent";
            using (SqlCommand OpponentTotals = new SqlCommand(selectFromWhere + groupBy))
            {
                OpponentTotals.Connection = busDriver.SQLdb;
                OpponentTotals.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = OpponentTotals.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        opDefMakes = double.Parse(sdr["Makes"].ToString());
                        opDefAtt = double.Parse(sdr["Total"].ToString());
                        opDefPct = float.Parse(sdr["Pct"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }

            //Opponent Defense Shot Type
            selectFromWhere = selectFromWhere.Replace("f.opponent, ", "f.opponent, f.Shot, ");
            groupBy = groupBy.Replace("f.opponent", "f.opponent, f.Shot");
            using (SqlCommand OpponentShotType = new SqlCommand(selectFromWhere + groupBy))
            {
                OpponentShotType.Connection = busDriver.SQLdb;
                OpponentShotType.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = OpponentShotType.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Shot"].ToString() == "FT")
                        {
                            opDef1Makes = double.Parse(sdr["Makes"].ToString());
                            opDef1Att = double.Parse(sdr["Total"].ToString());
                            opDef1Pct = float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["Shot"].ToString() == "FG2")
                        {
                            opDef2Makes = double.Parse(sdr["Makes"].ToString());
                            opDef2Att = double.Parse(sdr["Total"].ToString());
                            opDef2Pct = float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["Shot"].ToString() == "FG3")
                        {
                            opDef3Makes = double.Parse(sdr["Makes"].ToString());
                            opDef3Att = double.Parse(sdr["Total"].ToString());
                            opDef3Pct = float.Parse(sdr["Pct"].ToString());
                        }
                    }
                }
                busDriver.SQLdb.Close();
            }




            //Team Defense Miss Totals
            selectFromWhere = "select f.season_id, f.opponent, " +
                "sum(case when Result = 'Make' then 1 else 0 end) Makes, " +
                "sum(case when Result != 'Make' then 1 else 0 end) Miss, " +
                "count(Shot) Total, " +
                "cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 5))/cast(count(Shot)as decimal(18, 5)) as decimal(18, 5)) Pct " +
                "from FirstBaskets f " +
                "where f.season_id = 2024 and f.opponent_id = " + ddTeams.SelectedItem.Value + " " + firstShotWhere;
            groupBy = " group by f.season_id, f.opponent";
            using (SqlCommand OpponentTotals = new SqlCommand(selectFromWhere + groupBy))
            {
                OpponentTotals.Connection = busDriver.SQLdb;
                OpponentTotals.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = OpponentTotals.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        tDefMiss = double.Parse(sdr["Miss"].ToString());
                        tDefAtt = double.Parse(sdr["Total"].ToString());
                        tDefPct = 1 - float.Parse(sdr["Pct"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }

            //Team Defense Missed Shot Type
            selectFromWhere = selectFromWhere.Replace("f.opponent, ", "f.opponent, f.Shot, ");
            groupBy = groupBy.Replace("f.opponent", "f.opponent, f.Shot");
            using (SqlCommand OpponentShotType = new SqlCommand(selectFromWhere + groupBy))
            {
                OpponentShotType.Connection = busDriver.SQLdb;
                OpponentShotType.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = OpponentShotType.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Shot"].ToString() == "FT")
                        {
                            tDef1Miss = double.Parse(sdr["Miss"].ToString());
                            tDef1Att = double.Parse(sdr["Total"].ToString());
                            tDef1Pct = 1 - float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["Shot"].ToString() == "FG2")
                        {
                            tDef2Miss = double.Parse(sdr["Miss"].ToString());
                            tDef2Att = double.Parse(sdr["Total"].ToString());
                            tDef2Pct = 1 - float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["Shot"].ToString() == "FG3")
                        {
                            tDef3Miss = double.Parse(sdr["Miss"].ToString());
                            tDef3Att = double.Parse(sdr["Total"].ToString());
                            tDef3Pct = 1 - float.Parse(sdr["Pct"].ToString()); 
                        }
                    }
                }
                busDriver.SQLdb.Close();
            }



            //Opponent Defense Position Totals
            selectFromWhere = "select f.season_id, f.opponent, s.position, " +
                "sum(case when Result = 'Make' then 1 else 0 end) Makes, " +
                "sum(case when Result != 'Make' then 1 else 0 end) Miss, " +
                "count(Shot) Total, " +
                "cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 5))/cast(count(Shot)as decimal(18, 5)) as decimal(18, 5)) Pct " +
                "from FirstBaskets f left join StartingLineups s on f.game_id = s.game_id and f.player_id = s.player_id and f.season_id = s.season_id " +
                "where f.season_id = 2024 and f.opponent_id = " + ddOpponent.SelectedItem.Value + " " + firstShotWhereOp;
            groupBy = " group by f.season_id, f.opponent, s.position";
            using (SqlCommand OpponentTotals = new SqlCommand(selectFromWhere + groupBy))
            {
                OpponentTotals.Connection = busDriver.SQLdb;
                OpponentTotals.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = OpponentTotals.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["position"].ToString() == "PG")
                        {
                            opDefMakesPG = double.Parse(sdr["Makes"].ToString());
                            opDefAttPG = double.Parse(sdr["Total"].ToString());
                            opDefPctPG = float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["position"].ToString() == "SG")
                        {
                            opDefMakesSG = double.Parse(sdr["Makes"].ToString());
                            opDefAttSG = double.Parse(sdr["Total"].ToString());
                            opDefPctSG = float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["position"].ToString() == "SF")
                        {
                            opDefMakesSF = double.Parse(sdr["Makes"].ToString());
                            opDefAttSF = double.Parse(sdr["Total"].ToString());
                            opDefPctSF = float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["position"].ToString() == "PF")
                        {
                            opDefMakesPF = double.Parse(sdr["Makes"].ToString());
                            opDefAttPF = double.Parse(sdr["Total"].ToString());
                            opDefPctPF = float.Parse(sdr["Pct"].ToString());
                        }
                        else if (sdr["position"].ToString() == "C")
                        {
                            opDefMakesC = double.Parse(sdr["Makes"].ToString());
                            opDefAttC = double.Parse(sdr["Total"].ToString());
                            opDefPctC = float.Parse(sdr["Pct"].ToString());
                        }
                    }
                }
                busDriver.SQLdb.Close();
            }

            //Opponent Defense Position Shot Type
            selectFromWhere = selectFromWhere.Replace("s.position, ", "s.position, f.Shot, ");
            groupBy = groupBy.Replace("s.position", "s.position, f.Shot");
            using (SqlCommand OpponentShotType = new SqlCommand(selectFromWhere + groupBy))
            {
                OpponentShotType.Connection = busDriver.SQLdb;
                OpponentShotType.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = OpponentShotType.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Shot"].ToString() == "FT")
                        {
                            if (sdr["position"].ToString() == "PG")
                            {
                                opDef1MakesPG = double.Parse(sdr["Makes"].ToString());
                                opDef1AttPG = double.Parse(sdr["Total"].ToString());
                                opDef1PctPG = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "SG")
                            {
                                opDef1MakesSG = double.Parse(sdr["Makes"].ToString());
                                opDef1AttSG = double.Parse(sdr["Total"].ToString());
                                opDef1PctSG = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "SF")
                            {
                                opDef1MakesSF = double.Parse(sdr["Makes"].ToString());
                                opDef1AttSF = double.Parse(sdr["Total"].ToString());
                                opDef1PctSF = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "PF")
                            {
                                opDef1MakesPF = double.Parse(sdr["Makes"].ToString());
                                opDef1AttPF = double.Parse(sdr["Total"].ToString());
                                opDef1PctPF = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "C")
                            {
                                opDef1MakesC = double.Parse(sdr["Makes"].ToString());
                                opDef1AttC = double.Parse(sdr["Total"].ToString());
                                opDef1PctC = float.Parse(sdr["Pct"].ToString());
                            }
                        }
                        else if (sdr["Shot"].ToString() == "FG2")
                        {
                            if (sdr["position"].ToString() == "PG")
                            {
                                opDef2MakesPG = double.Parse(sdr["Makes"].ToString());
                                opDef2AttPG = double.Parse(sdr["Total"].ToString());
                                opDef2PctPG = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "SG")
                            {
                                opDef2MakesSG = double.Parse(sdr["Makes"].ToString());
                                opDef2AttSG = double.Parse(sdr["Total"].ToString());
                                opDef2PctSG = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "SF")
                            {
                                opDef2MakesSF = double.Parse(sdr["Makes"].ToString());
                                opDef2AttSF = double.Parse(sdr["Total"].ToString());
                                opDef2PctSF = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "PF")
                            {
                                opDef2MakesPF = double.Parse(sdr["Makes"].ToString());
                                opDef2AttPF = double.Parse(sdr["Total"].ToString());
                                opDef2PctPF = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "C")
                            {
                                opDef2MakesC = double.Parse(sdr["Makes"].ToString());
                                opDef2AttC = double.Parse(sdr["Total"].ToString());
                                opDef2PctC = float.Parse(sdr["Pct"].ToString());
                            }
                        }
                        else if (sdr["Shot"].ToString() == "FG3")
                        {
                            if (sdr["position"].ToString() == "PG")
                            {
                                opDef3MakesPG = double.Parse(sdr["Makes"].ToString());
                                opDef3AttPG = double.Parse(sdr["Total"].ToString());
                                opDef3PctPG = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "SG")
                            {
                                opDef3MakesSG = double.Parse(sdr["Makes"].ToString());
                                opDef3AttSG = double.Parse(sdr["Total"].ToString());
                                opDef3PctSG = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "SF")
                            {
                                opDef3MakesSF = double.Parse(sdr["Makes"].ToString());
                                opDef3AttSF = double.Parse(sdr["Total"].ToString());
                                opDef3PctSF = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "PF")
                            {
                                opDef3MakesPF = double.Parse(sdr["Makes"].ToString());
                                opDef3AttPF = double.Parse(sdr["Total"].ToString());
                                opDef3PctPF = float.Parse(sdr["Pct"].ToString());
                            }
                            else if (sdr["position"].ToString() == "C")
                            {
                                opDef3MakesC = double.Parse(sdr["Makes"].ToString());
                                opDef3AttC = double.Parse(sdr["Total"].ToString());
                                opDef3PctC = float.Parse(sdr["Pct"].ToString());
                            }
                        }
                    }
                }
                busDriver.SQLdb.Close();
            }

            //Player and Team Offensive Attempts
            double pt1Att = p1Att / t1Att; double pt2Att = p2Att / t2Att; double pt3Att = p3Att / t3Att;
            double t1AttTot = t1Att / tAtt; double t2AttTot = t2Att / tAtt; double t3AttTot = t3Att / tAtt;
            if (double.IsNaN(pt1Att))
            {
                pt1Att = 0;
            }
            if (double.IsNaN(pt2Att))
            {
                pt2Att = 0;
            }
            if (double.IsNaN(pt3Att))
            {
                pt3Att = 0;
            }
            if (double.IsNaN(t1AttTot))
            {
                t1AttTot = 0;
            }
            if (double.IsNaN(t2AttTot))
            {
                t2AttTot = 0;
            }
            if (double.IsNaN(t3AttTot))
            {
                t3AttTot = 0;
            }

            //Team Defensive Attempts
            double teamD1AttTot = tDef1Att / tDefAtt; double teamD2AttTot = tDef2Att / tDefAtt; double teamD3AttTot = tDef3Att / tDefAtt;
            if (double.IsNaN(teamD1AttTot))
            {
                teamD1AttTot = 0;
            }
            if (double.IsNaN(teamD2AttTot))
            {
                teamD2AttTot = 0;
            }
            if (double.IsNaN(teamD3AttTot))
            {
                teamD3AttTot = 0;
            }

            //Opponent Offensive Attempts
            double opOff1AttTot = op1Att / opAtt; double opOff2AttTot = op2Att / opAtt; double opOff3AttTot = op3Att / opAtt;
            if (double.IsNaN(opOff1AttTot))
            {
                opOff1AttTot = 0;
            }
            if (double.IsNaN(opOff2AttTot))
            {
                opOff2AttTot = 0;
            }
            if (double.IsNaN(opOff3AttTot))
            {
                opOff3AttTot = 0;
            }

            //Opponent Defensive Allowed Attempts
            double opD1AttTot = opDef1Att / opDefAtt; double opD2AttTot = opDef2Att / opDefAtt; double opD3AttTot = opDef3Att / opDefAtt;
            if (double.IsNaN(opD1AttTot))
            {
                opD1AttTot = 0;
            }
            if (double.IsNaN(opD2AttTot))
            {
                opD2AttTot = 0;
            }
            if (double.IsNaN(opD3AttTot))
            {
                opD3AttTot = 0;
            }


            double opDefPGAttTot = opDefAttPG / opDefAtt;
            double opDefSGAttTot = opDefAttSG / opDefAtt;
            double opDefSFAttTot = opDefAttSF / opDefAtt;
            double opDefPFAttTot = opDefAttPF / opDefAtt;
            double opDefCAttTot = opDefAttC / opDefAtt;

            lblOpPGAnalytics.Text = "Opposing PGs are  " + opDefMakesPG + "/" + opDefAttPG + ", shooting " + Math.Round(opDefPctPG * 100, 2) + "%";
            lblOpSGAnalytics.Text = "Opposing SGs are  " + opDefMakesSG + "/" + opDefAttSG + ", shooting " + Math.Round(opDefPctSG * 100, 2) + "%";
            lblOpSFAnalytics.Text = "Opposing SFs are  " + opDefMakesSF + "/" + opDefAttSF + ", shooting " + Math.Round(opDefPctSF * 100, 2) + "%";
            lblOpPFAnalytics.Text = "Opposing PFs are  " + opDefMakesPF + "/" + opDefAttPF + ", shooting " + Math.Round(opDefPctPF * 100, 2) + "%";
            lblOpCAnalytics.Text = "Opposing Cs are  " + opDefMakesC + "/" + opDefAttC + ", shooting " + Math.Round(opDefPctC * 100, 2) + "%";

            double playerFT_playerTotal = Math.Round(p1Att / pAtt, 4);
            double playerFG2_playerTotal = Math.Round(p2Att / pAtt, 4);
            double playerFG3_playerTotal = Math.Round(p3Att / pAtt, 4);
            double playerTotal_teamTotal = Math.Round(pAtt / tAtt, 4);

            string playerShotReason = "";



            //p = Player Implied Probability, Player's probability of scoring
            double p = 0;
            double pv2 = 0;

            if(p1Pct != 1)
            {
                p1Pct += p1Att / (100 - ((p1Att * p1Att) * pt1Att));
            }
            if (p2Pct != 1)
            {
                p2Pct += p2Att / (100 - ((p2Att * p2Att) * pt2Att));
            }
            if (p3Pct != 1)
            {
                p3Pct += p3Att / (100 - ((p3Att * p3Att) * pt3Att));
            }


            //if (p1Pct == 0 && p1Att != 0)
            //{
            //    p1Pct = p1Att / 100;
            //}
            //if (p2Pct == 0 && p2Att != 0)
            //{
            //    p2Pct = p2Att / 100;
            //}
            //if (p3Pct == 0 && p3Att != 0)
            //{
            //    p3Pct = p3Att / 100;
            //}

            //  pFT% * pFTA/tFTA      pFG2% * pFG2A/tFG2A   pFG3% * pFG3A/tFG2A
            p = (p1Pct * pt1Att * t1AttTot) // = Player FT% * Player FT att/Team FT att * Team FT att/Team total att
              + (p2Pct * pt2Att * t2AttTot) // = Player FG2% * Player FG2 att/Team FG2 att * Team FG2 att/Team total att
              + (p3Pct * pt3Att * t3AttTot);// = Player FG3% * Player FG3 att/Team FG3 att * Team FG3 att/Team total att

            if(position == "PG")
            {
                p = p * ((opDefPGAttTot * opDefPGAttTot) + 1);
            }
            if (position == "SG")
            {
                p = p * ((opDefSGAttTot * opDefSGAttTot) + 1);
            }
            if (position == "SF")
            {
                p = p * ((opDefSFAttTot * opDefSFAttTot) + 1);
            }
            if (position == "PF")
            {
                p = p * ((opDefPFAttTot * opDefPFAttTot) + 1);
            }
            if (position == "C")
            {
                p = p * ((opDefCAttTot * opDefCAttTot) + 1);
            }


            if (ddShotValue.SelectedItem.Text == "FT")
            {
                pv2 = p * playerFT_playerTotal;
                p = (p1Pct * pt1Att * t1AttTot);
                playerShotReason = playerFT_playerTotal + "% of his shots are " + ddShotValue.SelectedItem.Text + "s and he's taking " + Math.Round(pt1Att * 100, 2) + "% of the team's " + ddShotValue.SelectedItem.Text + "s. " + starts.ToString() + " starts at " + position;

            }
            else if (ddShotValue.SelectedItem.Text == "FG2")
            {
                pv2 = p * playerFG2_playerTotal;
                p = (p2Pct * pt2Att * t2AttTot);
                playerShotReason = playerFG2_playerTotal + "% of his shots are " + ddShotValue.SelectedItem.Text + "s and he's taking " + Math.Round(pt2Att * 100, 2) + "% of the team's " + ddShotValue.SelectedItem.Text + "s. " + starts.ToString() + " starts at " + position;

            }
            if (ddShotValue.SelectedItem.Text == "FG3")
            {
                pv2 = p * playerFG3_playerTotal;
                p = (p3Pct * pt3Att * t3AttTot);
                playerShotReason = playerFG3_playerTotal + "% of his shots are " + ddShotValue.SelectedItem.Text + "s and he's taking " + Math.Round(pt3Att * 100, 2) + "% of the team's " + ddShotValue.SelectedItem.Text + "s. " + starts.ToString() + " starts at " + position;
            }
            else
            {
                playerShotReason = "He's taking " + Math.Round(playerTotal_teamTotal * 100, 2) + "% of the team's shots. " + starts.ToString() + " starts at " + position;
            }
            pv2 = (p + (playerTotal_teamTotal * pPct)) / 2;


            //Opposing team's probability of allowing score
            double opD = 0;



            opD = (opDef1Pct * opD1AttTot) // = Opponent FT allowed % * Opponent allowed FT att/Opponent total allowed att
                + (opDef2Pct * opD2AttTot) // = Opponent FG2 allowed % * Opponent allowed FG2 att/Opponent total allowed att
                + (opDef3Pct * opD3AttTot);// = Opponent FG3 allowed % * Opponent allowed FG3 att/Opponent total allowed att



            //Team's probability of stopping score
            double teamD = 0;

            teamD = (tDef1Pct * teamD1AttTot) // = Team FT  forced miss% * Team def FT att/Team total def att
                  + (tDef2Pct * teamD2AttTot) // = Team FG2 forced miss% * Team def FG2 att/Team total def att
                  + (tDef3Pct * teamD3AttTot);// = Team FG3 forced miss% * Team def FG3 att/Team total def att



            //Opposing team's probability of missing when on offense
            double opOff = 0;


            opOff = (op1Pct * opOff1AttTot) // = Opponent FT  miss% * Opponent FT att/Opponent total att
                  + (op2Pct * opOff2AttTot) // = Opponent FG2 miss% * Opponent FG2 att/Opponent total att
                  + (op3Pct * opOff3AttTot);// = Opponent FG3  miss% * Opponent FG3 att/Opponent total att


            double TipWinRate = 0;
            double TipWinRateString = tipPct;
            if (ddTeamTip.SelectedIndex != 0 && ddOpTip.SelectedIndex == 0)
            {
                TipWinRate = tipPct;
            }
            else if (ddTeamTip.SelectedIndex != 0 && ddOpTip.SelectedIndex != 0)
            {
                //TipWinRate = (tipPct + opTipLossPct) / 2;
                TipWinRateString = tipPct;
                double weight = tipTotal / (tipTotal + opTipTotal);
                TipWinRate = (tipPct * weight) + (opTipLossPct * (1 - weight));
            }


            double PlayerImpliedOdds = (p * .85) + (opD * .15);
            double PlayerImpliedOddsv2 = (pv2 * .9) + (opD * .1);

            double BallBackChance = (teamD * .7) + (opOff * .3);


            double Equation = ((PlayerImpliedOdds * TipWinRate) + ((BallBackChance * PlayerImpliedOdds) * (1 - TipWinRate))); //* 100;
            double Equationv2 = ((PlayerImpliedOddsv2 * TipWinRate) + ((BallBackChance * PlayerImpliedOdds) * (1 - TipWinRate))); //* 100;
            Equation = Math.Round(Equation * 100, 2);

            lblNewOdds.Text = Equation.ToString() + "%";
            lblNewOddsProbability.Text = "+" + Math.Round((100 / (Equation / 100)) - 100).ToString();


            lblPlayerReasoning.Text = ddShooter.SelectedItem.Text + " has a " + Math.Round(PlayerImpliedOdds * 100, 2) + "% chance of scoring. " + playerShotReason;
            lblTeamReasoning.Text = ddTeamTip.SelectedItem.Text.Split(' ')[0] + " is winning the tip " + Math.Round(TipWinRateString * 100, 2) + "% of the time. The " + team + " have " + tAtt + " total attempts. If on defense, they have a " + Math.Round(teamD * 100, 2) + "% chance of forcing a stop.";
            lblOpponentReasoning.Text = ddOpTip.SelectedItem.Text.Split(' ')[0] + " is losing the tip " + Math.Round(opTipLossPct * 100, 2) + "% of the time. The " + opponent + " have " + opAtt + " total attempts. If on defense, they have a " + Math.Round(opD * 100, 2) + "% chance of allowing a basket.";
        }




        protected void chkTeamActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblActives.Text = "";
            if (ddShotValue.SelectedIndex == 0)
            {
                ddShooter_SelectedIndexChanged(sender, e);
            }
            else
            {
                ddShotValue_SelectedIndexChanged(sender, e);
            }

            int i = 0;
            foreach (ListItem item in chkTeamActive.Items)
            {
                if (item.Selected)
                {
                    lblActives.Text += item.Text + ", ";
                    i++;
                }
            }

            if (gamesPublic.Count == 1)
            {
                lblGamesActive.Text = "Starting Lineup - " + gamesPublic.Count + " Game: ";
                lblActives.Text = lblActives.Text.TrimEnd(' ').TrimEnd(',');
            }
            else if (gamesPublic.Count > 1)
            {
                lblGamesActive.Text = "Starting Lineup - " + gamesPublic.Count + " Games: ";
                lblActives.Text = lblActives.Text.TrimEnd(' ').TrimEnd(',');
            }
            else
            {
                lblGamesActive.Text = "";
            }
            TeamActives();
        }



        protected void TeamActives()
        {
            int count = 0;
            foreach (ListItem item in chkTeamActive.Items)
            {
                if (item.Selected)
                {
                    count++;
                }
            }
            string firstShotSelectActive = "select f.season_id, f.team, sum(case when Result = 'Make' then 1 else 0 end) Makes, sum(case when Result != 'Make' then 1 else 0 end) Miss, count(Shot) Total, cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 5))/cast(count(Shot)as decimal(18, 5)) as decimal(18, 5)) Pct from FirstBaskets f where f.season_id = 2024 and f.team_id  = " + ddTeams.SelectedItem.Value;
            string firstOpShotSelectActive = "select f.season_id, f.Opponent, sum(case when Result = 'Make' then 1 else 0 end) Makes, sum(case when Result != 'Make' then 1 else 0 end) Miss, count(Shot) Total, cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 5))/cast(count(Shot)as decimal(18, 5)) as decimal(18, 5)) Pct from FirstBaskets f where f.season_id = 2024 and f.Opponent_id = " + ddTeams.SelectedItem.Value;
            string firstShotGroupBy = " group by f.season_id, f.team";
            string firstShot = firstShotSelectActive + firstShotWhere + firstShotGroupBy;
            string firstOpShot = firstOpShotSelectActive + firstShotWhere + firstShotGroupBy.Replace("f.team", "f.Opponent");

            using (SqlCommand GameCheck = new SqlCommand(firstShot))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        lblTeam.Text = sdr["Makes"].ToString() + "/" + sdr["Total"].ToString();
                        teamTotal = double.Parse(sdr["Total"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }
            using (SqlCommand GameCheck = new SqlCommand(firstOpShot))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        lblTeam.Text += " - " + sdr["Makes"].ToString() + "/" + sdr["Total"].ToString();
                        teamDefPct = double.Parse(sdr["Miss"].ToString()) / double.Parse(sdr["Total"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }
        }




        //protected void chkTeamDNP_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //protected void chkOpponentDNP_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        protected void chkOpponentActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblActivesOp.Text = "";

            Dictionary<int, string> opActives = new Dictionary<int, string>();
            int i = 0;
            foreach (ListItem item in chkOpponentActive.Items)
            {
                if (item.Selected)
                {
                    lblActivesOp.Text += item.Text + ", ";
                    i++;
                }
            }
            if (i > 0)
            {
                GetActives(opActives, chkOpponentActive, "opponent_id", "Active");
            }

            if (gamesPublicOp.Count == 1)
            {
                lblGamesActiveOp.Text = "Starting Lineup - " + gamesPublicOp.Count + " Game: ";
                lblActivesOp.Text = lblActivesOp.Text.TrimEnd(' ').TrimEnd(',');
            }
            else if (gamesPublicOp.Count > 1)
            {
                lblGamesActiveOp.Text = "Starting Lineup - " + gamesPublicOp.Count + " Games: ";
                lblActivesOp.Text = lblActivesOp.Text.TrimEnd(' ').TrimEnd(',');
            }
            else
            {
                lblGamesActiveOp.Text = "";
            }
        }

        protected void OpponentActives()
        {
            
        }

    }
}