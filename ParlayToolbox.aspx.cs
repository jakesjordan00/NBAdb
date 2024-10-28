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

namespace NBAdb
{
    public partial class ParlayToolbox : System.Web.UI.Page
    {
        public static BusDriver busDriver = new BusDriver();

        public List<DropDownList> t1Rosters;
        public List<DropDownList> t2Rosters;



        protected void Page_Load(object sender, EventArgs e)
        {
            t1Rosters = new List<DropDownList>
            {
                ddlRoster, ddlRoster2, ddlRoster3, ddlRoster4, ddlRoster5
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
                        ddTeams.DataTextField = dataT.Tables[0].Columns["Team"].ToString();
                        ddTeams.DataValueField = dataT.Tables[0].Columns["TeamID"].ToString();
                        ddTeams.DataSource = dataT;
                        ddTeams.DataBind();
                        ListItem emptyItem = new ListItem("Team", "");
                        ddTeams.Items.Insert(0, emptyItem);
                    }
                }
            }
        }

        protected void ddSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddTeams_SelectedIndexChanged(sender, e);
        }

        protected void ddTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            tWins.Visible = true;
            a.Visible = true;
            Label1.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            Label4.Visible = true;
            p1Stats.Visible = false;
            foreach(DropDownList roster in t1Rosters)
            {
                PopulateRoster(roster);
            }
            t1Name.Text = ddTeams.SelectedItem.Text;
            TeamAverages(Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value));
        }
        protected void TeamAverages(int team, int season)
        {
            using (SqlCommand querySearch = new SqlCommand("ParlayTeamAverageRanks"))
            {
                querySearch.Connection = busDriver.SQLdb;
                querySearch.CommandType = CommandType.StoredProcedure;
                querySearch.Parameters.AddWithValue("@team", team);
                querySearch.Parameters.AddWithValue("@season", season);
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
                        t1FGR.Text = "(" + sdr["twoPointersMadeRank"].ToString() + ")" + "/" + "("+ sdr["twoPointersAttemptedRank"].ToString() + ")";
                        t1FG3.Text = sdr["threePointersMade"].ToString() + "/" + sdr["threePointersAttempted"].ToString();
                        t1FG3R.Text = "(" + sdr["threePointersMadeRank"].ToString() + ")" + "/" + "(" + sdr["threePointersAttemptedRank"].ToString() + ")";
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


        protected void PopulateRoster(DropDownList Roster)
        {
            using (SqlCommand querySearch = new SqlCommand("ParlayRoster"))
            {
                querySearch.CommandType = CommandType.StoredProcedure;
                querySearch.Parameters.AddWithValue("@team", ddTeams.SelectedValue);
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
                        ListItem emptyItem = new ListItem("Player", "");
                        Roster.Items.Insert(0, emptyItem);
                    }
                }
            }
        }

        protected void ddlRoster_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DropDownList roster in t1Rosters)
            {
                if(roster.ID != "ddlRoster")
                {
                    if(roster.SelectedItem == null)
                    {
                        PopulateRoster(roster);
                    }
                    if (roster.Items.Contains(ddlRoster.SelectedItem))
                    {
                        roster.Items.Remove(ddlRoster.SelectedItem);
                    }
                }     
            }
            p1Stats.Visible = true;
            GetAverages(Int32.Parse(ddlRoster.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p1Name, p1Games, p1Pts, p1Ast, p1Reb, p1FG, p13, p1Blk, p1Stl);
        }

        protected void ddlRoster2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DropDownList roster in t1Rosters)
            {
                if (roster.ID != "ddlRoster2")
                {
                    if (roster.SelectedItem == null)
                    {
                        PopulateRoster(roster);
                    }
                    if (roster.Items.Contains(ddlRoster2.SelectedItem))
                    {
                        roster.Items.Remove(ddlRoster2.SelectedItem);
                    }
                }
            }
            p2Stats.Visible = true;
            GetAverages(Int32.Parse(ddlRoster2.SelectedItem.Value), Int32.Parse(ddTeams.SelectedItem.Value), Int32.Parse(ddSeason.SelectedItem.Value), p2Name, p2Games, p2Pts, p2Ast, p2Reb, p2FG, p23, p2Blk, p2Stl);

        }

        protected void ddlRoster3_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DropDownList roster in t1Rosters)
            {
                if (roster.ID != "ddlRoster3")
                {
                    if (roster.SelectedItem == null)
                    {
                        PopulateRoster(roster);
                    }
                    if (roster.Items.Contains(ddlRoster3.SelectedItem))
                    {
                        roster.Items.Remove(ddlRoster3.SelectedItem);
                    }
                }
            }

        }
        protected void ddlRoster4_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (DropDownList roster in t1Rosters)
            {
                if (roster.ID != "ddlRoster4")
                {
                    if (roster.SelectedItem == null)
                    {
                        PopulateRoster(roster);
                    }
                    if (roster.Items.Contains(ddlRoster4.SelectedItem))
                    {
                        roster.Items.Remove(ddlRoster4.SelectedItem);
                    }
                }
            }
        }
        protected void ddlRoster5_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (DropDownList roster in t1Rosters)
            {
                if (roster.ID != "ddlRoster5")
                {
                    if (roster.SelectedItem == null)
                    {
                        PopulateRoster(roster);
                    }
                    if (roster.Items.Contains(ddlRoster5.SelectedItem))
                    {
                        roster.Items.Remove(ddlRoster5.SelectedItem);
                    }
                }
            }
        }

        public void GetAverages(int player, int team, int season, Label name, Label games, Label pts, Label ast, Label reb, Label fg, Label fg3, Label blk, Label stl)
        {
            using (SqlCommand PlayerSearch = new SqlCommand("ToolboxAverages"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@player", player);
                PlayerSearch.Parameters.AddWithValue("@team", team);
                PlayerSearch.Parameters.AddWithValue("@season", season);
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
                        pts.Text = reader["Points"].ToString();
                        ast.Text = reader["Assists"].ToString();
                        reb.Text = reader["Rebounds"].ToString();
                        fg.Text = reader["FG2M"] + "/" + reader["FG2A"] + " - " + reader["FG2%"] + "%";
                        fg3.Text = reader["FG3M"] + "/" + reader["FG3A"] + " - " + reader["FG3%"]+ "%";
                        blk.Text = reader["Blocks"].ToString();
                        stl.Text = reader["Steals"].ToString();
                    }
                    busDriver.SQLdb.Close();
                }
            }
        }


    }
}