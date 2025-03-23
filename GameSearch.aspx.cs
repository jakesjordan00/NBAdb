using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Web.Services;

namespace NBAdb
{
    public partial class GameSearch : System.Web.UI.Page
    {
        public List<DropDownList> teams;
        public static BusDriver busDriver = new BusDriver();
        protected void Page_Load(object sender, EventArgs e)
        {
            teams = new List<DropDownList>
            {
                ddTeam, ddTeam2
            };

            if (!this.IsPostBack)
            {
                this.BindGrid();
            }

        }

        protected void ddTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddTeam.SelectedItem.Text != "Team")
            {
                t2Row.Visible = true;
                btnRetrieve_Click(sender, new EventArgs());
            }
            else
            {
                t2Row.Visible = false;
            }
        }

        protected void BindGrid()
        {
            using (SqlCommand querySearch = new SqlCommand("GameSearchTeams"))
            {
                querySearch.CommandType = CommandType.StoredProcedure;
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
        }

        protected void ddTeam2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chkSeason_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSeason.Checked)
            {
                ddSeason.Enabled = true;
            }
            else
            {
                ddSeason.Enabled = false;
            }
        }
        static string Where(DropDownList Team1, CheckBoxList T1HoA, CheckBoxList T1WoL, DropDownList Team2, DropDownList Season)
        {
            string where = "where ";
            string team1 = "";
            if(Team1.SelectedItem.Text != "Team" && T1HoA.SelectedIndex == -1 && T1WoL.SelectedIndex == -1)   //Team 1 selected
            {
                team1 = " (h.team_id = " + Team1.SelectedItem.Value + " or a.team_id = " + Team1.SelectedItem.Value + ")";
                if (Team2.SelectedItem.Text != "Team")
                {
                    team1 = " ((h.team_id = " + Team1.SelectedItem.Value + " and a.team_id = " + Team2.SelectedItem.Value + ") or (a.team_id = " + Team1.SelectedItem.Value + " and h.team_id = " + Team2.SelectedItem.Value + "))";
                }
            }
            else if(Team1.SelectedItem.Text != "Team" && T1HoA.SelectedItem.Text == "H" && T1WoL.SelectedIndex == -1)    //Team 1 Selected @ Home
            {
                team1 = " h.team_id = " + Team1.SelectedItem.Value;
                if (Team2.SelectedItem.Text != "Team")
                {
                    team1 += " and a.team_id = " + Team2.SelectedItem.Value;
                }
            }
            else if (Team1.SelectedItem.Text != "Team" && T1HoA.SelectedItem.Text == "A" && T1WoL.SelectedIndex == -1)   //Team 1 Selected Away
            {
                team1 = " a.team_id = " + Team1.SelectedItem.Value;
                if (Team2.SelectedItem.Text != "Team")
                {
                    team1 += " and h.team_id = " + Team2.SelectedItem.Value;
                }
            }
            else if (Team1.SelectedItem.Text != "Team" && T1HoA.SelectedItem.Text == "H" && T1WoL.SelectedItem.Text == "W")   //Team 1 Selected Home and Winning
            {
                team1 = " h.team_id = " + Team1.SelectedItem.Value + " and w.team_id = " + Team1.SelectedItem.Value;
                if (Team2.SelectedItem.Text != "Team")
                {
                    team1 += " and a.team_id = " + Team2.SelectedItem.Value + " and l.team_id = " + Team2.SelectedItem.Value;
                }
            }
            else if (Team1.SelectedItem.Text != "Team" && T1HoA.SelectedItem.Text == "H" && T1WoL.SelectedItem.Text == "L")   //Team 1 Selected Home and Losing
            {
                team1 = " h.team_id = " + Team1.SelectedItem.Value + " and l.team_id = " + Team1.SelectedItem.Value;
                if (Team2.SelectedItem.Text != "Team")
                {
                    team1 += " and a.team_id = " + Team2.SelectedItem.Value + " and w.team_id = " + Team2.SelectedItem.Value;
                }
            }
            else if (Team1.SelectedItem.Text != "Team" && T1HoA.SelectedItem.Text == "A" && T1WoL.SelectedItem.Text == "W")   //Team 1 Selected Away and Winning
            {
                team1 = " a.team_id = " + Team1.SelectedItem.Value + " and w.team_id = " + Team1.SelectedItem.Value;
                if (Team2.SelectedItem.Text != "Team")
                {
                    team1 += " and h.team_id = " + Team2.SelectedItem.Value + " and l.team_id = " + Team2.SelectedItem.Value;
                }
            }
            else if (Team1.SelectedItem.Text != "Team" && T1HoA.SelectedItem.Text == "A" && T1WoL.SelectedItem.Text == "L")   //Team 1 Selected Away and Losing
            {
                team1 = " a.team_id = " + Team1.SelectedItem.Value + " and l.team_id = " + Team1.SelectedItem.Value;
                if (Team2.SelectedItem.Text != "Team")
                {
                    team1 += " and h.team_id = " + Team2.SelectedItem.Value + " and w.team_id = " + Team2.SelectedItem.Value;
                }
            }
            else if (Team1.SelectedItem.Text != "Team" && T1HoA.SelectedIndex == -1 && T1WoL.SelectedItem.Text == "W")   //Team 1 Selected Winning
            {
                team1 = " and w.team_id = " + Team1.SelectedItem.Value;
                if (Team2.SelectedItem.Text != "Team")
                {
                    team1 += " and l.team_id = " + Team2.SelectedItem.Value;
                }
            }
            else if (Team1.SelectedItem.Text != "Team" && T1HoA.SelectedIndex == -1 && T1WoL.SelectedItem.Text == "L")   //Team 1 Selected Losing
            {
                team1 = " and l.team_id = " + Team1.SelectedItem.Value;
                if(Team2.SelectedItem.Text != "Team")
                {
                    team1 += " and w.team_id = " + Team2.SelectedItem.Value;
                }
            }





            where = where + team1;

            return where;
        }


        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            List<string> gameLinks = new List<string>();
            string selectFrom = "select date, g.game_id, concat(a.name, ' @ ', h.name) Description, concat(right(g.season_id, 2), '-', right(g.season_id, 2) + 1) season, '' clock, concat('https://www.nba.com/game/', a.tricode, '-vs-', h.tricode, '-00', g.game_id, '/box-score?watchFullGame=true') Link, case when w.team_id = " + ddTeam.SelectedItem.Value + " then 'W' else 'L' end Win from game g inner join team h on g.team_idH = h.team_id and g.season_id = h.season_id inner join team a on g.team_idA = a.team_id and g.season_id = a.season_id inner join team w on g.team_idW = w.team_id and g.season_id = w.season_id inner join team l on g.team_idL = l.team_id and g.season_id = l.season_id ";
            string where = Where(ddTeam, chkTHoA, chkTWoL, ddTeam2, ddSeason);
            string order = " order by g.date desc, g.game_id desc";
            if(txtPlayer.Text != "")
            {
                selectFrom += " inner join playerbox pb on pb.game_id = g.game_id and pb.season_id = g.season_id inner join player p on pb.player_id = p.player_id and g.season_id = p.season_id and p.name like '%" + txtPlayer.Text + "%' ";
                where += " and (Status = 'Active' or cast(replace(replace(minutesCalculated, 'PT', ''), 'M', '') as int) != 0) ";
            }
            if(txtScore1.Text != "" && txtScore2.Text != "")
            {
                selectFrom = "select date, g.game_id, concat(a.name, ' @ ', h.name) Description, Concat('Q', period, ' - ', replace(replace(replace(pbp.clock, 'PT', ''), 'M', ':'), '0S', '')) Clock, concat(right(g.season_id, 2), '-', right(g.season_id, 2) + 1) season, concat('https://www.nba.com/game/', a.tricode, '-vs-', h.tricode, '-00', g.game_id, '/box-score?watchFullGame=true') Link, case when w.team_id = " + ddTeam.SelectedItem.Value + " then 'W' else 'L' end Win from game g inner join team h on g.team_idH = h.team_id and g.season_id = h.season_id inner join team a on g.team_idA = a.team_id and g.season_id = a.season_id inner join team w on g.team_idW = w.team_id and g.season_id = w.season_id inner join team l on g.team_idL = l.team_id and g.season_id = l.season_id ";
                selectFrom = selectFrom.Replace("select date", "select distinct date");
                selectFrom += " inner join playByPlay pbp on g.game_id = pbp.game_id and g.season_id = pbp.season_id and pbp.scoreHome = case when h.team_id = " + ddTeam.SelectedItem.Value +
                " then " + txtScore1.Text + " when h.team_id = " + ddTeam2.SelectedItem.Value + " then " + txtScore2.Text + " else null end ";
                selectFrom += "and pbp.scoreAway = case when a.team_id = " + ddTeam.SelectedItem.Value + " then " + txtScore1.Text + " when a.team_id = " + ddTeam2.SelectedItem.Value + " then " + txtScore2.Text + " else null end ";
                where += " and shotResult = 'Made' ";
            }
            string playoffs = selectFrom.Replace("playByPlay", "playByPlayPlayoffs").Replace("game g", "playoffGame g").Replace("g.season_id = h", "2023 = h").Replace("g.season_id = a", "2023 = a").Replace("g.season_id = w", "2023 = w").Replace("g.season_id = l", "2023 = l")
                + where.Replace("playByPlay", "playByPlayPlayoffs").Replace("game g", "playoffGame g").Replace("and shotResult = 'Made'", "and actionType in('Free Throw', '2PT', '3PT')");
            string queryTest = selectFrom + where + order;
            string query = selectFrom + where + " union " + playoffs + order;

            using (SqlCommand gameInfo = new SqlCommand(query))
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
                        busDriver.SQLdb.Open();
                    }

                    SqlDataReader reader = gameInfo.ExecuteReader();
                    while (reader.Read())
                    {
                        // Extract data
                        DateTime gameDate = DateTime.Parse(reader["date"].ToString());
                        string desc = reader["Description"].ToString();
                        string clock = reader["Clock"].ToString();
                        string link = reader["Link"].ToString();
                        string season = reader["season"].ToString();
                        string formattedDate = gameDate.ToString("MM/dd/yyyy");
                        string win = reader["Win"].ToString();

                        // Create hyperlink text
                        string linkText = $"{formattedDate}, {desc}. {season}, {win}. {clock}";
                        string url = $"{link}";
                        // Add to the list
                        gameLinks.Add($"<a href='{url}' target='_blank' class='game-link'>{linkText}</a>");
                    }
                    reader.Close();
                    busDriver.SQLdb.Close();
                }
            }
            // Dynamically add hyperlinks to the UI
            foreach (string link in gameLinks)
            {
                // Create a LiteralControl to display the link in the placeholder
                LiteralControl linkControl = new LiteralControl(link + "<br/>");
                GameLinksPlaceholder.Controls.Add(linkControl);
            }
        }

        protected void chkTHoA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddTeam.SelectedItem.Text != "Team")
            {
                btnRetrieve_Click(sender, new EventArgs());
            }
        }

        protected void chkTWoL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = chkTWoL.SelectedIndex;
            chkTWoL.ClearSelection();
            chkTWoL.SelectedIndex = index;
            if (ddTeam.SelectedItem.Text != "Team")
            {
                btnRetrieve_Click(sender, new EventArgs());
            }
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static List<string> GetPlayerNames(string prefix)
        {
            //List<string> players = new List<string>();

            //using (busDriver.SQLdb)
            //{
            //    string query = "select distinct Name from player WHERE name LIKE @SearchText + '%'";
            //    using (SqlCommand cmd = new SqlCommand(query, busDriver.SQLdb))
            //    {
            //        cmd.Parameters.AddWithValue("@SearchText", prefix);
            //        busDriver.SQLdb.Open();
            //        SqlDataReader reader = cmd.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            players.Add(reader["Name"].ToString());
            //        }
            //    }
            //}
            //return players;
            return new List<string> { "LeBron James", "Michael Jordan", "Kobe Bryant", "Kevin Durant", "Stephen Curry" };

        }

    }
}