using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Microsoft.SqlServer.Server;
using NBAdb;
using System.Data.SqlTypes;
using System.Drawing;

namespace NBAdb
{
    public partial class StartingLineups
    {

        public static BusDriver busDriver = new BusDriver();
        public void GetStarters(int team, Label hName, Label hpg, Label hsg, Label hsf, Label hpf, Label hc, Label aName, Label apg, Label asg, Label asf, Label apf, Label ac, Label time)
        {
            int placeholder = 0;
            if(placeholder == 0)
            {
                HitEndpoint(team, hName, hpg, hsg, hsf, hpf, hc, aName, apg, asg, asf, apf, ac, time);
            }

            hName.Visible = true;
            hpg.Visible = true;
            hsg.Visible = true;
            hsf.Visible = true;
            hpf.Visible = true;
            hc.Visible = true;
            aName.Visible = true;
            apg.Visible = true;
            asg.Visible = true;
            asf.Visible = true;
            apf.Visible = true;
            ac.Visible = true;
            time.Visible = true;
        }

        public void HitEndpoint(int team, Label hName, Label hpg, Label hsg, Label hsf, Label hpf, Label hc, Label aName, Label apg, Label asg, Label asf, Label apf, Label ac, Label time)
        {
            string Year = DateTime.Today.Year.ToString();
            string Month = DateTime.Today.Month.ToString();
            string Day = DateTime.Today.Day.ToString();
            int homeID = 0;
            if(Int32.Parse(Day) < 10)
            {
                Day = "0" + Day;
            }
            if (Int32.Parse(Month) < 10)
            {
                Month = "0" + Month;
            }
            try
            {
                var sbClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
                string json = "https://stats.nba.com/js/data/leaders/00_daily_lineups_" + Year + Month + Day + ".json";
                WebRequest sbReq = WebRequest.Create(json);
                WebResponse sbResp = sbReq.GetResponse();
                string sbJson = sbClient.DownloadString(json);
                GameData gameData = JsonConvert.DeserializeObject<GameData>(sbJson);
                for(int i = 0; i < gameData.Games.Count; i++)
                {
                
                }
                foreach(Game game in gameData.Games)
                {
                    if (game.HomeTeam.TeamId == team || game.AwayTeam.TeamId == team)
                    {
                        hName.Text = game.HomeTeam.TeamAbbreviation;
                        hsg.Text = game.HomeTeam.Players[0].FirstName + " " + game.HomeTeam.Players[0].LastName + " (" + game.HomeTeam.Players[0].LineupStatus.Substring(0,1) + ")";
                        hsf.Text = game.HomeTeam.Players[1].FirstName + " " + game.HomeTeam.Players[1].LastName + " (" + game.HomeTeam.Players[1].LineupStatus.Substring(0,1) + ")";
                        hpg.Text = game.HomeTeam.Players[2].FirstName + " " + game.HomeTeam.Players[2].LastName + " (" + game.HomeTeam.Players[2].LineupStatus.Substring(0,1) + ")";                    
                        hpf.Text = game.HomeTeam.Players[3].FirstName + " " + game.HomeTeam.Players[3].LastName + " (" + game.HomeTeam.Players[3].LineupStatus.Substring(0,1) + ")";
                        hc.Text = game.HomeTeam.Players[4].FirstName + " " + game.HomeTeam.Players[4].LastName + " (" +  game.HomeTeam.Players[4].LineupStatus.Substring(0, 1) + ")";
                        aName.Text = game.AwayTeam.TeamAbbreviation;
                        asg.Text = game.AwayTeam.Players[0].FirstName + " " + game.AwayTeam.Players[0].LastName + " (" + game.AwayTeam.Players[0].LineupStatus.Substring(0,1) + ")";
                        asf.Text = game.AwayTeam.Players[1].FirstName + " " + game.AwayTeam.Players[1].LastName + " (" + game.AwayTeam.Players[1].LineupStatus.Substring(0,1) + ")";
                        apg.Text = game.AwayTeam.Players[2].FirstName + " " + game.AwayTeam.Players[2].LastName + " (" + game.AwayTeam.Players[2].LineupStatus.Substring(0,1) + ")";
                        apf.Text = game.AwayTeam.Players[3].FirstName + " " + game.AwayTeam.Players[3].LastName + " (" + game.AwayTeam.Players[3].LineupStatus.Substring(0,1) + ")";
                        ac.Text = game.AwayTeam.Players[4].FirstName + " " + game.AwayTeam.Players[4].LastName + " (" +  game.AwayTeam.Players[4].LineupStatus.Substring(0, 1) + ")";

                        time.Text = "Last updated at " + game.HomeTeam.Players[0].Timestamp.ToShortTimeString();
                    }
                    else
                    {
                    }
                    }
            }
            catch
            {

            }
        }
        public void StarterInsert()
        {
            List<string> dates = new List<string>();
            Dictionary<int, string> updateDate = new Dictionary<int, string>();
            List<int> update = new List<int>();
            using (SqlCommand GameCheck = new SqlCommand("select distinct concat(substring(cast(date as varchar(20)), 1, 4), substring(cast(date as varchar(20)), 6, 2), substring(cast(date as varchar(20)), 9, 2)) date from GameSchedule where season_id = 2024 and date <= cast(getdate() as date) and gameLabel != 'Preseason' order by date desc"))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        dates.Add(sdr.GetString(0));
                    }
                }
                busDriver.SQLdb.Close();
            }


            using (SqlCommand GameCheck = new SqlCommand("StarterUpdateCheck"))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.StoredProcedure;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        update.Add(Int32.Parse(sdr["game_id"].ToString()));
                        updateDate.Add(Int32.Parse(sdr["game_id"].ToString()), sdr["date"].ToString());
                    }
                }
                busDriver.SQLdb.Close();
            }



            int counter = 0;
            foreach (string date in dates)
            {
                try
                {
                    var sbClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
                    string json = "https://stats.nba.com/js/data/leaders/00_daily_lineups_" + date + ".json";
                    WebRequest sbReq = WebRequest.Create(json);
                    WebResponse sbResp = sbReq.GetResponse();
                    string sbJson = sbClient.DownloadString(json);
                    GameData gameData = JsonConvert.DeserializeObject<GameData>(sbJson);
                    //for (int i = 0; i < gameData.Games.Count; i++)
                    //{

                    //}

                    foreach (Game game in gameData.Games)
                    {
                        //if (game.GameId.Substring(2) == update[counter])
                        int currentGame = Int32.Parse(game.GameId);
                        if (updateDate.ContainsKey(Int32.Parse(game.GameId)))
                        {
                            for (int i = 0; i < game.HomeTeam.Players.Count; i++)
                            {
                                using (SqlCommand InsertData = new SqlCommand("StartingLineUpdate"))
                                {
                                    InsertData.CommandType = CommandType.StoredProcedure;
                                    InsertData.Parameters.AddWithValue("@season_id", 2024);
                                    InsertData.Parameters.AddWithValue("@game_id", game.GameId);
                                    InsertData.Parameters.AddWithValue("@team_id", game.HomeTeam.TeamId);
                                    InsertData.Parameters.AddWithValue("@player_id", game.HomeTeam.Players[i].PersonId);
                                    InsertData.Parameters.AddWithValue("@position", game.HomeTeam.Players[i].Position);
                                    InsertData.Parameters.AddWithValue("@rosterStatus", game.HomeTeam.Players[i].RosterStatus);
                                    InsertData.Parameters.AddWithValue("@lineupStatus", game.HomeTeam.Players[i].LineupStatus);
                                    InsertData.Parameters.AddWithValue("@home", 1);
                                    InsertData.Parameters.AddWithValue("@tricode", game.HomeTeam.TeamAbbreviation);
                                    InsertData.Parameters.AddWithValue("@player", game.HomeTeam.Players[i].PlayerName);
                                    using (SqlDataAdapter sInsertData = new SqlDataAdapter())
                                    {
                                        InsertData.Connection = busDriver.SQLdb;
                                        sInsertData.SelectCommand = InsertData;
                                        try
                                        {
                                            busDriver.SQLdb.Open();
                                        }
                                        catch
                                        {
                                            busDriver.SQLdb.Close();
                                            busDriver.SQLdb.Open();
                                        }
                                        InsertData.ExecuteScalar();
                                        busDriver.SQLdb.Close();
                                    }
                                }
                            }
                            for (int i = 0; i < game.AwayTeam.Players.Count; i++)
                            {
                                using (SqlCommand InsertData = new SqlCommand("StartingLineUpdate"))
                                {
                                    InsertData.CommandType = CommandType.StoredProcedure;
                                    InsertData.Parameters.AddWithValue("@season_id", 2024);
                                    InsertData.Parameters.AddWithValue("@game_id", game.GameId);
                                    InsertData.Parameters.AddWithValue("@team_id", game.AwayTeam.TeamId);
                                    InsertData.Parameters.AddWithValue("@player_id", game.AwayTeam.Players[i].PersonId);
                                    InsertData.Parameters.AddWithValue("@position", game.AwayTeam.Players[i].Position);
                                    InsertData.Parameters.AddWithValue("@rosterStatus", game.AwayTeam.Players[i].RosterStatus);
                                    InsertData.Parameters.AddWithValue("@lineupStatus", game.AwayTeam.Players[i].LineupStatus);
                                    InsertData.Parameters.AddWithValue("@home", 0);
                                    InsertData.Parameters.AddWithValue("@tricode", game.AwayTeam.TeamAbbreviation);
                                    InsertData.Parameters.AddWithValue("@player", game.AwayTeam.Players[i].PlayerName);
                                    using (SqlDataAdapter sInsertData = new SqlDataAdapter())
                                    {
                                        InsertData.Connection = busDriver.SQLdb;
                                        sInsertData.SelectCommand = InsertData;
                                        try
                                        {
                                            busDriver.SQLdb.Open();
                                        }
                                        catch
                                        {
                                            busDriver.SQLdb.Close();
                                            busDriver.SQLdb.Open();
                                        }
                                        InsertData.ExecuteScalar();
                                        busDriver.SQLdb.Close();
                                    }
                                }
                            }

                        }
                        else  //If the game isnt in our list of ones to update, insert
                        {
                            for (int i = 0; i < game.HomeTeam.Players.Count; i++)
                            {
                                using (SqlCommand InsertData = new SqlCommand("StartingLineupInsert"))
                                {
                                    InsertData.CommandType = CommandType.StoredProcedure;
                                    InsertData.Parameters.AddWithValue("@season_id", 2024);
                                    InsertData.Parameters.AddWithValue("@game_id", game.GameId);
                                    InsertData.Parameters.AddWithValue("@team_id", game.HomeTeam.TeamId);
                                    InsertData.Parameters.AddWithValue("@player_id", game.HomeTeam.Players[i].PersonId);
                                    InsertData.Parameters.AddWithValue("@position", game.HomeTeam.Players[i].Position);
                                    InsertData.Parameters.AddWithValue("@rosterStatus", game.HomeTeam.Players[i].RosterStatus);
                                    InsertData.Parameters.AddWithValue("@lineupStatus", game.HomeTeam.Players[i].LineupStatus);
                                    InsertData.Parameters.AddWithValue("@home", 1);
                                    InsertData.Parameters.AddWithValue("@tricode", game.HomeTeam.TeamAbbreviation);
                                    InsertData.Parameters.AddWithValue("@player", game.HomeTeam.Players[i].PlayerName);
                                    using (SqlDataAdapter sInsertData = new SqlDataAdapter())
                                    {
                                        InsertData.Connection = busDriver.SQLdb;
                                        sInsertData.SelectCommand = InsertData;
                                        try
                                        {
                                            busDriver.SQLdb.Open();
                                        }
                                        catch
                                        {
                                            busDriver.SQLdb.Close();
                                            busDriver.SQLdb.Open();
                                        }
                                        InsertData.ExecuteScalar();
                                        busDriver.SQLdb.Close();
                                    }
                                }
                            }
                            for (int i = 0; i < game.AwayTeam.Players.Count; i++)
                            {
                                using (SqlCommand InsertData = new SqlCommand("StartingLineupInsert"))
                                {
                                    InsertData.CommandType = CommandType.StoredProcedure;
                                    InsertData.Parameters.AddWithValue("@season_id", 2024);
                                    InsertData.Parameters.AddWithValue("@game_id", game.GameId);
                                    InsertData.Parameters.AddWithValue("@team_id", game.AwayTeam.TeamId);
                                    InsertData.Parameters.AddWithValue("@player_id", game.AwayTeam.Players[i].PersonId);
                                    InsertData.Parameters.AddWithValue("@position", game.AwayTeam.Players[i].Position);
                                    InsertData.Parameters.AddWithValue("@rosterStatus", game.AwayTeam.Players[i].RosterStatus);
                                    InsertData.Parameters.AddWithValue("@lineupStatus", game.AwayTeam.Players[i].LineupStatus);
                                    InsertData.Parameters.AddWithValue("@home", 0);
                                    InsertData.Parameters.AddWithValue("@tricode", game.AwayTeam.TeamAbbreviation);
                                    InsertData.Parameters.AddWithValue("@player", game.AwayTeam.Players[i].PlayerName);
                                    using (SqlDataAdapter sInsertData = new SqlDataAdapter())
                                    {
                                        InsertData.Connection = busDriver.SQLdb;
                                        sInsertData.SelectCommand = InsertData;
                                        try
                                        {
                                            busDriver.SQLdb.Open();
                                        }
                                        catch
                                        {
                                            busDriver.SQLdb.Close();
                                            busDriver.SQLdb.Open();
                                        }
                                        InsertData.ExecuteScalar();
                                        busDriver.SQLdb.Close();
                                    }
                                }
                            }

                        }
                        counter = counter + 1;
                    }
                }
                catch
                {

                }
            }
        }
    }


    public class GameData
    {
        public List<Game> Games { get; set; }
    }

    public class Game
    {
        public string GameId { get; set; }
        public int GameStatus { get; set; }
        public string GameStatusText { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
    }

    public class Team
    {
        public int TeamId { get; set; }
        public string TeamAbbreviation { get; set; }
        public List<Player> Players { get; set; }
    }

    public class Player
    {
        public int PersonId { get; set; }
        public int TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PlayerName { get; set; }
        public string LineupStatus { get; set; }
        public string Position { get; set; }
        public string RosterStatus { get; set; }
        public DateTime Timestamp { get; set; }
    }

}