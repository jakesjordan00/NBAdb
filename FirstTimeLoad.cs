using System;
using System.Diagnostics;
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
using System.Numerics;
using static NBAdb.FirstTimeLoad;
using System.Media;
using System.Data.SqlTypes;
using static NBAdb.BoxScorePlayoff;

namespace NBAdb
{
    public class FirstTimeLoad
    {
        public static BusDriver busDriver = new BusDriver();
        public static int arenas = 0;
        public static int teams = 0;
        public void Init(List<string> seasons, int tableCount, Label lblSeasonResult, Label lblTimeElapsed)
        {
            lblTimeElapsed.Text  = "";
            lblSeasonResult.Text = "";
            int buildID = 0;
            if (tableCount == 0)
            {
                using (busDriver.SQLdb)
                {
                    using (SqlCommand CreateTables = new SqlCommand("CreateTables"))
                    {
                        CreateTables.Connection = busDriver.SQLdb;
                        CreateTables.CommandType = CommandType.StoredProcedure;
                        busDriver.SQLdb.Open();
                        CreateTables.ExecuteScalar();
                        busDriver.SQLdb.Close();
                    }
                }
            }
            using (SqlCommand PlayerSearch = new SqlCommand("BuildLogCheck"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {

                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    while(reader.Read())
                    {
                        buildID = reader.GetInt32(0);
                    }
                    busDriver.SQLdb.Close();
                }
            }            
            for (int i = 0; i < seasons.Count; i++)
            {
                Stopwatch stopwatch = new Stopwatch(); // Create a new stopwatch for each iteration
                stopwatch.Start(); // Start timing
                DateTime start = DateTime.Now;
                SeasonValues(seasons[i]);
                arenas = 0;
                teams = 0;
                lblSeasonResult.Text += seasons[i].Split('-')[2] + " season was successful.";
                lblSeasonResult.ForeColor = System.Drawing.Color.LawnGreen;
                stopwatch.Stop();
                DateTime end = DateTime.Now;
                TimeSpan timeElapsed = stopwatch.Elapsed;
                string elapsedString = timeElapsed.Hours + ":" + timeElapsed.Minutes + ":" + timeElapsed.Seconds + "." + timeElapsed.Milliseconds;
                                                 //For example:  2022 season: 00:04:12.123. 2023 season: 00:03:54.636. 
                lblTimeElapsed.Text += seasons[i].Split('-')[2] + " season: " + timeElapsed.Hours + ":" + timeElapsed.Minutes + ":" + 
                timeElapsed.Seconds + "." + timeElapsed.Milliseconds + ". ";

                using (SqlCommand InsertDataAway = new SqlCommand("BuildLogInsert"))
                {
                    InsertDataAway.Connection = busDriver.SQLdb;
                    InsertDataAway.CommandType = CommandType.StoredProcedure;
                    InsertDataAway.Parameters.AddWithValue("@BuildID", buildID);
                    InsertDataAway.Parameters.AddWithValue("@Season", seasons[i].Split('-')[2]);
                    InsertDataAway.Parameters.AddWithValue("@TimeElapsed", elapsedString);
                    InsertDataAway.Parameters.AddWithValue("@DatetimeStarted", start);
                    InsertDataAway.Parameters.AddWithValue("@DatetimeComplete", end);
                    InsertDataAway.Parameters.AddWithValue("@Description", "First Time Load");
                    busDriver.SQLdb.Open();
                    InsertDataAway.ExecuteScalar();
                    busDriver.SQLdb.Close();
                }


                //PlayerTeams(Int32.Parse(seasons[i].Split('-')[2]));
            }
        }

        public void SeasonValues(string season)
        {
            int start = Int32.Parse(season.Split('-')[0]);
            int end   = Int32.Parse(season.Split('-')[1]);
            int id = Int32.Parse(season.Split('-')[2]);
            List<int> gameList = new List<int>();
            if(id == 2024)
            {
                using (SqlCommand GameCheck = new SqlCommand("select distinct game_id from GameSchedule g where g.season_id = 2024 and g.date <= getdate()+1 and gameLabel != 'preseason'"))
                {
                    GameCheck.Connection = busDriver.SQLdb;
                    GameCheck.CommandType = CommandType.Text;
                    busDriver.SQLdb.Open();
                    using (SqlDataReader sdr = GameCheck.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            gameList.Add(sdr.GetInt32(0));
                            end = Int32.Parse(sdr["game_id"].ToString());
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }
            if (id == 2024)
            {
                FirstLoad24(gameList, id);
            }
            else
            {
                FirstLoad(start, end, id);
            }
            Playoffs playoffs = new Playoffs();
            playoffs.FirstTimeLoadHandler(id);
        }
        public static void GameSchedule(int id)
        {

        }
        public void FirstLoad24(List<int> gameList, int id)
        {
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string boxLink = "";
            foreach(int game in gameList)
            {
                boxLink = "https://cdn.nba.com/static/json/liveData/boxscore/boxscore_00" + game.ToString() + ".json";
                try
                {
                    WebRequest BoxScoreReq = WebRequest.Create(boxLink);
                    WebResponse BoxScoreResp = BoxScoreReq.GetResponse();
                    string json = client.DownloadString(boxLink);
                    Root JSON = JsonConvert.DeserializeObject<Root>(json);
                    Official REF = JsonConvert.DeserializeObject<Official>(json);
                    Player PLAYER = JsonConvert.DeserializeObject<Player>(json);
                    SqlDateTime gameDate = JSON.game.gameEt.Date;
                    GameCheck(JSON, game, JSON.game.homeTeam.score, JSON.game.awayTeam.score, id);
                    BoxScore.GetJSON(game, "FirstTimeLoad", id);
                    int game_id = Int32.Parse(JSON.game.gameId);
                    //Send Variables to respecting Check methods
                    if (arenas < 29 || JSON.game.arena.arenaId == 465)
                    {
                        ArenaCheck(JSON, JSON.game.arena.arenaId, id); //Sends arena_id from JSON to check if we have this Arena for this season already
                    }
                    if (teams < 30)
                    {
                        int team_id = JSON.game.homeTeam.teamId;    //Set team_id equal to the teamId of the Home team from JSON
                        TeamCheck(JSON, team_id, id);                       //Send to TeamCheck, then TeamPost if not duplicate
                        int away_id = JSON.game.awayTeam.teamId;    //Set team_id equal to the teamId of the Away team from JSON                  
                        TeamCheck(JSON, away_id, id);                       //Send to TeamCheck, then TeamPost if not duplicate
                    }
                    //Officials
                    for (int j = 0; j < JSON.game.officials.Count(); j++)
                    {
                        int official_id = JSON.game.officials[j].personId;          //Set official_id equal to the official_id of the j official 
                        OfficialCheck(JSON, official_id, id, j);                  //send off to OfficialCheck
                    }
                    //Send gameID, method or 'sender', and seasonID
                    PlayByPlay playByPlay = new PlayByPlay();
                    playByPlay.Init(game, "FirstTimeLoad", id, "");

                    //Home Players
                    for (int j = 0; j < JSON.game.homeTeam.players.Count(); j++)
                    {
                        int player_id = JSON.game.homeTeam.players[j].personId;            //Set player_id equal to the player_id of the j home team's player 
                        HomePlayerCheck(JSON, player_id, j, id);                           //send off to PlayerCheck
                        int team_id = JSON.game.homeTeam.teamId;    //Set team_id equal to the teamId of the Home team from JSON
                        PlayerTeamCheck(game_id, player_id, team_id, gameDate, id);
                    }
                    //Away Players
                    for (int j = 0; j < JSON.game.awayTeam.players.Count(); j++)
                    {
                        int player_id = JSON.game.awayTeam.players[j].personId;            //Set player_id equal to the player_id of the j away team's player 
                        AwayPlayerCheck(JSON, player_id, j, id);                        //send off to PlayerCheck again
                        int team_id = JSON.game.awayTeam.teamId;    //Set team_id equal to the teamId of the Home team from JSON
                        PlayerTeamCheck(game_id, player_id, team_id, gameDate, id);
                    }
                }
                catch (Exception ex)
                {

                }
            }           
        }

        public void FirstLoad(int start, int end, int id)
        {
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string boxLink = "";
            for (int i = start; i <= end; i++)
            {
                boxLink = "https://cdn.nba.com/static/json/liveData/boxscore/boxscore_00" + i.ToString() + ".json";
                try
                {
                    WebRequest BoxScoreReq = WebRequest.Create(boxLink);
                    WebResponse BoxScoreResp = BoxScoreReq.GetResponse();
                    string json = client.DownloadString(boxLink);
                    Root JSON = JsonConvert.DeserializeObject<Root>(json);
                    Official REF = JsonConvert.DeserializeObject<Official>(json);
                    Player PLAYER = JsonConvert.DeserializeObject<Player>(json);
                    SqlDateTime gameDate = JSON.game.gameEt.Date;
                    GameCheck(JSON, i, JSON.game.homeTeam.score, JSON.game.awayTeam.score, id);
                    BoxScore.GetJSON(i, "FirstTimeLoad", id);
                    int game_id = Int32.Parse(JSON.game.gameId);
                    //Send Variables to respecting Check methods
                    if (arenas < 29 || JSON.game.arena.arenaId == 465)
                    {
                        ArenaCheck(JSON, JSON.game.arena.arenaId, id); //Sends arena_id from JSON to check if we have this Arena for this season already
                    }
                    if (teams < 30)
                    {
                        int team_id = JSON.game.homeTeam.teamId;    //Set team_id equal to the teamId of the Home team from JSON
                        TeamCheck(JSON, team_id, id);                       //Send to TeamCheck, then TeamPost if not duplicate
                        int away_id = JSON.game.awayTeam.teamId;    //Set team_id equal to the teamId of the Away team from JSON                  
                        TeamCheck(JSON, away_id, id);                       //Send to TeamCheck, then TeamPost if not duplicate
                    }
                    //Officials
                    for (int j = 0; j < JSON.game.officials.Count(); j++)
                    {
                        int official_id = JSON.game.officials[j].personId;          //Set official_id equal to the official_id of the j official 
                        OfficialCheck(JSON, official_id, id, j);                  //send off to OfficialCheck
                    }
                    //Send gameID, method or 'sender', and seasonID
                    PlayByPlay playByPlay = new PlayByPlay();
                    playByPlay.Init(i, "FirstTimeLoad", id, ""); 
                    
                    //Home Players
                    for (int j = 0; j < JSON.game.homeTeam.players.Count(); j++)
                    {
                        int player_id = JSON.game.homeTeam.players[j].personId;            //Set player_id equal to the player_id of the j home team's player 
                        HomePlayerCheck(JSON, player_id, j, id);                           //send off to PlayerCheck
                        int team_id = JSON.game.homeTeam.teamId;    //Set team_id equal to the teamId of the Home team from JSON
                        PlayerTeamCheck(game_id, player_id, team_id, gameDate, id);
                    }
                    //Away Players
                    for (int j = 0; j < JSON.game.awayTeam.players.Count(); j++)
                    {
                        int player_id = JSON.game.awayTeam.players[j].personId;            //Set player_id equal to the player_id of the j away team's player 
                        AwayPlayerCheck(JSON, player_id, j, id);                        //send off to PlayerCheck again
                        int team_id = JSON.game.awayTeam.teamId;    //Set team_id equal to the teamId of the Home team from JSON
                        PlayerTeamCheck(game_id, player_id, team_id, gameDate, id);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void PlayerTeamCheck(int game, int player, int team, SqlDateTime start, int id)
        {
            using (SqlCommand PlayerSearch = new SqlCommand("PlayerTeamCheck"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@player", player);
                PlayerSearch.Parameters.AddWithValue("@team", team);
                PlayerSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    reader.Read();
                    if (!reader.HasRows)
                    {
                        busDriver.SQLdb.Close();
                        PlayerTeamCheckOtherTeams(game, player, team, start, id);
                    }
                    else
                    {
                        busDriver.SQLdb.Close();
                    }
                }
            }
        }


        public static void PlayerTeamCheckOtherTeams(int game, int player, int team, SqlDateTime start, int id)
        {
            using (SqlCommand PlayerSearch = new SqlCommand("PlayerTeamCheckOtherTeams"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@player", player);
                PlayerSearch.Parameters.AddWithValue("@team", team);
                PlayerSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        int oldTeam = Int32.Parse(reader[2].ToString());
                        busDriver.SQLdb.Close();
                        PlayerTeamUpdate(player, oldTeam, id);
                        PlayerTeamPost(game, player, team, start, id);
                    }
                    else
                    {
                        busDriver.SQLdb.Close();
                        PlayerTeamPost(game, player, team, start, id);
                    }
                }
            }
        }

        public static void PlayerTeamUpdate(int player, int team, int id)
        {
            SqlDateTime lastGame = SqlDateTime.MaxValue;
            using (SqlCommand PlayerSearch = new SqlCommand("PlayerTeamGetLastGame"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@player", player);
                PlayerSearch.Parameters.AddWithValue("@team", team);
                PlayerSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    reader.Read();
                    lastGame = SqlDateTime.Parse(reader[0].ToString());
                    busDriver.SQLdb.Close();                    
                }
            }
            using (SqlCommand InsertData = new SqlCommand("PlayerTeamUpdateLastGame"))
            {
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@player", player);
                InsertData.Parameters.AddWithValue("@team", team);
                InsertData.Parameters.AddWithValue("@id", id);
                InsertData.Parameters.AddWithValue("@LastGame", lastGame);
                using (SqlDataAdapter sInsertData = new SqlDataAdapter())
                {
                    InsertData.Connection = busDriver.SQLdb;
                    sInsertData.SelectCommand = InsertData;
                    busDriver.SQLdb.Open();
                    InsertData.ExecuteScalar();
                    busDriver.SQLdb.Close();
                }
            }


        }

        public static void PlayerTeamPost(int game, int player, int team, SqlDateTime start, int id)
        {
            using (SqlCommand InsertData = new SqlCommand("PlayerTeamPost"))
            {
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@player", player);
                InsertData.Parameters.AddWithValue("@team", team);
                InsertData.Parameters.AddWithValue("@id", id);
                InsertData.Parameters.AddWithValue("@FirstGame", start);
                using (SqlDataAdapter sInsertData = new SqlDataAdapter())
                {
                    InsertData.Connection = busDriver.SQLdb;
                    sInsertData.SelectCommand = InsertData;
                    busDriver.SQLdb.Open();
                    InsertData.ExecuteScalar();
                    busDriver.SQLdb.Close();
                }
            }
        }

        public static void HomePlayerCheck(Root JSON, int player_id, int j, int id)
        {
            using (SqlCommand PlayerSearch = new SqlCommand("playerCheck"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@player_id", player_id);
                PlayerSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    reader.Read();
                    if (!reader.HasRows)
                    {
                        busDriver.SQLdb.Close();
                        HomePlayerPost(JSON, player_id, j, id);
                    }
                    else if (reader.HasRows && JSON.game.homeTeam.players[j].position != null && reader.GetString(4) != JSON.game.homeTeam.players[j].position)
                    {
                        string position = reader.GetString(4);
                        busDriver.SQLdb.Close();
                        PlayerUpdate(player_id, JSON.game.homeTeam.players[j].position, position, id);
                    }
                    else
                    {
                        busDriver.SQLdb.Close();
                    }
                }
            }
        }
        public static void HomePlayerPost(Root JSON, int player_id, int j, int id)
        {
            using (SqlCommand InsertData = new SqlCommand("playerInsert"))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@player_id", player_id);
                InsertData.Parameters.AddWithValue("@id", id);
                if (JSON.game.homeTeam.players[j].position is null)
                {
                    InsertData.Parameters.AddWithValue("@position", "");
                }
                else
                {
                    InsertData.Parameters.AddWithValue("@position", JSON.game.homeTeam.players[j].position);
                }
                InsertData.Parameters.AddWithValue("@name", JSON.game.homeTeam.players[j].name);
                InsertData.Parameters.AddWithValue("@number", JSON.game.homeTeam.players[j].jerseyNum);
                InsertData.Parameters.AddWithValue("@college", "");
                InsertData.Parameters.AddWithValue("@country", "");
                InsertData.Parameters.AddWithValue("@draftYear", "");
                InsertData.Parameters.AddWithValue("@draftRound", "");
                InsertData.Parameters.AddWithValue("@draftPick", "");
                busDriver.SQLdb.Open();
                InsertData.ExecuteScalar();
                busDriver.SQLdb.Close();
            }
        }

        public static void AwayPlayerCheck(Root JSON, int player_id, int j, int id)
        {
            using (SqlCommand PlayerSearch = new SqlCommand("playerCheck"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@player_id", player_id);
                PlayerSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    reader.Read();
                    if (!reader.HasRows)
                    {
                        busDriver.SQLdb.Close();
                        AwayPlayerPost(JSON, player_id, j, id);
                    }
                    else if (reader.HasRows && JSON.game.awayTeam.players[j].position != null && reader.GetString(4) != JSON.game.awayTeam.players[j].position)
                    {
                        string position = reader.GetString(4);
                        busDriver.SQLdb.Close();
                        PlayerUpdate(player_id, JSON.game.awayTeam.players[j].position, position, id);
                    }
                    else
                    {
                        busDriver.SQLdb.Close();
                    }
                }
            }
        }
        public static void AwayPlayerPost(Root JSON, int player_id, int j, int id)
        {
            using (SqlCommand InsertData = new SqlCommand("playerInsert"))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@player_id", player_id);
                InsertData.Parameters.AddWithValue("@id", id);
                if (JSON.game.awayTeam.players[j].position is null)
                {
                    InsertData.Parameters.AddWithValue("@position", "");
                }
                else
                {
                    InsertData.Parameters.AddWithValue("@position", JSON.game.awayTeam.players[j].position);
                }
                InsertData.Parameters.AddWithValue("@name", JSON.game.awayTeam.players[j].name);
                InsertData.Parameters.AddWithValue("@number", JSON.game.awayTeam.players[j].jerseyNum);
                InsertData.Parameters.AddWithValue("@college", "");
                InsertData.Parameters.AddWithValue("@country", "");
                InsertData.Parameters.AddWithValue("@draftYear", "");
                InsertData.Parameters.AddWithValue("@draftRound", "");
                InsertData.Parameters.AddWithValue("@draftPick", "");
                busDriver.SQLdb.Open();
                InsertData.ExecuteScalar();
                busDriver.SQLdb.Close();
            }
        }
        public static void PlayerUpdate(int player_id, string position, string oldPosition, int id)
        {
            using (SqlCommand InsertData = new SqlCommand("playerUpdate"))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@player_id", player_id);
                InsertData.Parameters.AddWithValue("@id", id);
                if (oldPosition != "" && !oldPosition.Contains(position))
                {
                    InsertData.Parameters.AddWithValue("@position", oldPosition + "/" + position);
                }
                else
                {
                    InsertData.Parameters.AddWithValue("@position", position);
                }
                busDriver.SQLdb.Open();
                InsertData.ExecuteScalar();
                busDriver.SQLdb.Close();
            }            
        }

        public static void GameCheck(Root JSON, int game_id, int hScore, int aScore, int id)
        {
            using (SqlCommand GameSearch = new SqlCommand("gameCheck"))
            {
                GameSearch.CommandType = CommandType.StoredProcedure;
                GameSearch.Parameters.AddWithValue("@game_id", game_id);
                GameSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sGameSearch = new SqlDataAdapter())
                {
                    GameSearch.Connection = busDriver.SQLdb;
                    sGameSearch.SelectCommand = GameSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = GameSearch.ExecuteReader();
                    reader.Read();
                    if (!reader.HasRows)
                    {
                        busDriver.SQLdb.Close();
                        GamePost(JSON, game_id, hScore, aScore, id);
                    }
                    else
                    {
                        if (reader.GetInt32(7) != hScore && reader.GetInt32(7) != aScore)
                        {
                            busDriver.SQLdb.Close();
                            GameUpdate(JSON, game_id, hScore, aScore, id);
                        }
                        else
                        {
                            busDriver.SQLdb.Close();
                        }
                    }
                }
            }
        }
        public static void GamePost(Root JSON, int game_id, int hScore, int aScore, int id)
        {
            using (SqlCommand InsertData = new SqlCommand("gameInsert"))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@game_id", game_id);
                InsertData.Parameters.AddWithValue("@id", id);
                InsertData.Parameters.AddWithValue("@date", JSON.game.gameTimeLocal.Date);
                InsertData.Parameters.AddWithValue("@team_idH", JSON.game.homeTeam.teamId);
                InsertData.Parameters.AddWithValue("@team_idA", JSON.game.awayTeam.teamId);
                if (JSON.game.homeTeam.score >= JSON.game.awayTeam.score)
                {
                    InsertData.Parameters.AddWithValue("@team_idW", JSON.game.homeTeam.teamId);
                    InsertData.Parameters.AddWithValue("@wScore", JSON.game.homeTeam.score);
                    InsertData.Parameters.AddWithValue("@team_idL", JSON.game.awayTeam.teamId);
                    InsertData.Parameters.AddWithValue("@lScore", JSON.game.awayTeam.score);
                }
                else
                {
                    InsertData.Parameters.AddWithValue("@team_idW", JSON.game.awayTeam.teamId);
                    InsertData.Parameters.AddWithValue("@wScore", JSON.game.awayTeam.score);
                    InsertData.Parameters.AddWithValue("@team_idL", JSON.game.homeTeam.teamId);
                    InsertData.Parameters.AddWithValue("@lScore", JSON.game.homeTeam.score);
                }
                InsertData.Parameters.AddWithValue("@arena_id", JSON.game.arena.arenaId);
                InsertData.Parameters.AddWithValue("@sellout", JSON.game.sellout);
                busDriver.SQLdb.Open();
                InsertData.ExecuteScalar();
                busDriver.SQLdb.Close();
            }
        }
        public static void GameUpdate(Root JSON, int game_id, int hScore, int aScore, int id)
        {
            using (SqlCommand Update = new SqlCommand("gameUpdate"))
            {
                Update.Connection = busDriver.SQLdb;
                Update.CommandType = CommandType.StoredProcedure;
                Update.Parameters.AddWithValue("@game_id", game_id);
                Update.Parameters.AddWithValue("@id", id);
                if (JSON.game.homeTeam.score >= JSON.game.awayTeam.score)
                {
                    Update.Parameters.AddWithValue("@team_idW", JSON.game.homeTeam.teamId);
                    Update.Parameters.AddWithValue("@wScore", JSON.game.homeTeam.score);
                    Update.Parameters.AddWithValue("@team_idL", JSON.game.awayTeam.teamId);
                    Update.Parameters.AddWithValue("@lScore", JSON.game.awayTeam.score);
                }
                else
                {
                    Update.Parameters.AddWithValue("@team_idW", JSON.game.awayTeam.teamId);
                    Update.Parameters.AddWithValue("@wScore", JSON.game.awayTeam.score);
                    Update.Parameters.AddWithValue("@team_idL", JSON.game.homeTeam.teamId);
                    Update.Parameters.AddWithValue("@lScore", JSON.game.homeTeam.score);
                }
                busDriver.SQLdb.Open();
                Update.ExecuteScalar();
                busDriver.SQLdb.Close();
            }
        }

        public static void OfficialCheck(Root JSON, int official_id, int id, int j)
        {
            using (SqlCommand OfficialSearch = new SqlCommand("officialCheck"))
            {
                OfficialSearch.CommandType = CommandType.StoredProcedure;
                OfficialSearch.Parameters.AddWithValue("@official_id", official_id);
                OfficialSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sOfficialSearch = new SqlDataAdapter())
                {
                    OfficialSearch.Connection = busDriver.SQLdb;
                    sOfficialSearch.SelectCommand = OfficialSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = OfficialSearch.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        busDriver.SQLdb.Close();
                        OfficialPost(JSON, official_id, id, j);
                    }
                    busDriver.SQLdb.Close();
                }
            }
        }
        public static void OfficialPost(Root JSON, int official_id, int id, int j)
        {
            using (SqlCommand InsertData = new SqlCommand("officialInsert"))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@official_id", official_id);
                InsertData.Parameters.AddWithValue("@id", id);
                InsertData.Parameters.AddWithValue("@name", JSON.game.officials[j].name);
                InsertData.Parameters.AddWithValue("@number", JSON.game.officials[j].jerseyNum);
                InsertData.Parameters.AddWithValue("@assignment", JSON.game.officials[j].assignment);
                busDriver.SQLdb.Open();
                InsertData.ExecuteScalar();
                busDriver.SQLdb.Close();
            }
            
        }

        public static void TeamCheck(Root JSON, int team_id, int id)
        {
            using (SqlCommand TeamSearch = new SqlCommand("teamCheck"))
            {
                TeamSearch.CommandType = CommandType.StoredProcedure;
                TeamSearch.Parameters.AddWithValue("@team_id", team_id);
                TeamSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sTeamSearch = new SqlDataAdapter())
                {
                    TeamSearch.Connection = busDriver.SQLdb;
                    sTeamSearch.SelectCommand = TeamSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = TeamSearch.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            teams = Int32.Parse(reader[6].ToString());
                        }
                        busDriver.SQLdb.Close();
                    }
                    else
                    {
                        busDriver.SQLdb.Close();
                        TeamPost(JSON, team_id, id);
                    }
                }
            }
        }
        public static void TeamPost(Root JSON, int team_id, int id)
        {
            using (SqlCommand InsertData = new SqlCommand("teamInsert"))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@id", id);
                InsertData.Parameters.AddWithValue("@team_id", team_id);
                if (JSON.game.homeTeam.teamId == team_id)
                {
                    InsertData.Parameters.AddWithValue("@tricode", JSON.game.homeTeam.teamTricode);
                    InsertData.Parameters.AddWithValue("@city", JSON.game.homeTeam.teamCity);
                    InsertData.Parameters.AddWithValue("@name", JSON.game.homeTeam.teamName);
                }
                else
                {
                    InsertData.Parameters.AddWithValue("@tricode", JSON.game.awayTeam.teamTricode);
                    InsertData.Parameters.AddWithValue("@city", JSON.game.awayTeam.teamCity);
                    InsertData.Parameters.AddWithValue("@name", JSON.game.awayTeam.teamName);
                }
                InsertData.Parameters.AddWithValue("@yearFounded", 0);
                busDriver.SQLdb.Open();
                InsertData.ExecuteScalar();
                busDriver.SQLdb.Close();
            }
        }
        public static void ArenaCheck(Root JSON, int arena_id, int id)
        {     
            using (SqlCommand Check = new SqlCommand("arenaCheck"))
            {
                Check.CommandType = CommandType.StoredProcedure;
                Check.Parameters.AddWithValue("@id", id);
                Check.Parameters.AddWithValue("@arena_id", arena_id);
                using (SqlDataAdapter sCheck = new SqlDataAdapter())
                {
                    Check.Connection = busDriver.SQLdb;
                    sCheck.SelectCommand = Check;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = Check.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            arenas = Int32.Parse(reader[7].ToString());
                        }
                        busDriver.SQLdb.Close();
                    }
                    else
                    {
                        busDriver.SQLdb.Close();
                        ArenaPost(JSON, arena_id, id);
                    }
                }
            }            
        }
        public static void ArenaPost(Root JSON, int arena_id, int id)
        {       
            using (SqlCommand InsertData = new SqlCommand("arenaInsert"))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@id", id);
                InsertData.Parameters.AddWithValue("@arena_id", arena_id);
                InsertData.Parameters.AddWithValue("@team_id", JSON.game.homeTeam.teamId);
                InsertData.Parameters.AddWithValue("@name", JSON.game.arena.arenaName);
                InsertData.Parameters.AddWithValue("@city", JSON.game.arena.arenaCity);
                InsertData.Parameters.AddWithValue("@state", JSON.game.arena.arenaState);
                InsertData.Parameters.AddWithValue("@country", JSON.game.arena.arenaCountry);
                busDriver.SQLdb.Open();
                InsertData.ExecuteScalar();
                busDriver.SQLdb.Close();
            }
            
        }





        public class Arena
        {
            public int arenaId { get; set; }
            public string arenaName { get; set; }
            public string arenaCity { get; set; }
            public string arenaState { get; set; }
            public string arenaCountry { get; set; }
            public string arenaTimezone { get; set; }
        }

        public class AwayTeam
        {
            public int teamId { get; set; }
            public string teamName { get; set; }
            public string teamCity { get; set; }
            public string teamTricode { get; set; }
            public int score { get; set; }
            public string inBonus { get; set; }
            public int timeoutsRemaining { get; set; }
            public List<Period> periods { get; set; }
            public List<Player> players { get; set; }
            public Statistics statistics { get; set; }
        }

        public class Game
        {
            public string gameId { get; set; }
            public DateTime gameTimeLocal { get; set; }
            public DateTime gameTimeUTC { get; set; }
            public DateTime gameTimeHome { get; set; }
            public DateTime gameTimeAway { get; set; }
            public DateTime gameEt { get; set; }
            public int duration { get; set; }
            public string gameCode { get; set; }
            public string gameStatusText { get; set; }
            public int gameStatus { get; set; }
            public int regulationPeriods { get; set; }
            public int period { get; set; }
            public string gameClock { get; set; }
            public int attendance { get; set; }
            public string sellout { get; set; }
            public Arena arena { get; set; }
            public List<Official> officials { get; set; }
            public HomeTeam homeTeam { get; set; }
            public AwayTeam awayTeam { get; set; }
        }

        public class HomeTeam
        {
            public int teamId { get; set; }
            public string teamName { get; set; }
            public string teamCity { get; set; }
            public string teamTricode { get; set; }
            public int score { get; set; }
            public string inBonus { get; set; }
            public int timeoutsRemaining { get; set; }
            public List<Period> periods { get; set; }
            public List<Player> players { get; set; }
            public Statistics statistics { get; set; }
        }

        public class Meta
        {
            public int version { get; set; }
            public int code { get; set; }
            public string request { get; set; }
            public string time { get; set; }
        }

        public class Official
        {
            public int personId { get; set; }
            public string name { get; set; }
            public string nameI { get; set; }
            public string firstName { get; set; }
            public string familyName { get; set; }
            public string jerseyNum { get; set; }
            public string assignment { get; set; }
        }

        public class Period
        {
            public int period { get; set; }
            public string periodType { get; set; }
            public int score { get; set; }
        }

        public class Player
        {
            public string status { get; set; }
            public int order { get; set; }
            public int personId { get; set; }
            public string jerseyNum { get; set; }
            public string position { get; set; }
            public string starter { get; set; }
            public string oncourt { get; set; }
            public string played { get; set; }
            public Statistics statistics { get; set; }
            public string name { get; set; }
            public string nameI { get; set; }
            public string firstName { get; set; }
            public string familyName { get; set; }
            public string notPlayingReason { get; set; }
            public string notPlayingDescription { get; set; }
        }

        public class Root
        {
            public Meta meta { get; set; }
            public Game game { get; set; }
        }

        public class Statistics
        {
            public int assists { get; set; }
            public int blocks { get; set; }
            public int blocksReceived { get; set; }
            public int fieldGoalsAttempted { get; set; }
            public int fieldGoalsMade { get; set; }
            public double fieldGoalsPercentage { get; set; }
            public int foulsOffensive { get; set; }
            public int foulsDrawn { get; set; }
            public int foulsPersonal { get; set; }
            public int foulsTechnical { get; set; }
            public int freeThrowsAttempted { get; set; }
            public int freeThrowsMade { get; set; }
            public double freeThrowsPercentage { get; set; }
            public double minus { get; set; }
            public string minutes { get; set; }
            public string minutesCalculated { get; set; }
            public double plus { get; set; }
            public double plusMinusPoints { get; set; }
            public int points { get; set; }
            public int pointsFastBreak { get; set; }
            public int pointsInThePaint { get; set; }
            public int pointsSecondChance { get; set; }
            public int reboundsDefensive { get; set; }
            public int reboundsOffensive { get; set; }
            public int reboundsTotal { get; set; }
            public int steals { get; set; }
            public int threePointersAttempted { get; set; }
            public int threePointersMade { get; set; }
            public double threePointersPercentage { get; set; }
            public int turnovers { get; set; }
            public int twoPointersAttempted { get; set; }
            public int twoPointersMade { get; set; }
            public double twoPointersPercentage { get; set; }
            public double assistsTurnoverRatio { get; set; }
            public int benchPoints { get; set; }
            public int biggestLead { get; set; }
            public string biggestLeadScore { get; set; }
            public int biggestScoringRun { get; set; }
            public string biggestScoringRunScore { get; set; }
            public int fastBreakPointsAttempted { get; set; }
            public int fastBreakPointsMade { get; set; }
            public double fastBreakPointsPercentage { get; set; }
            public double fieldGoalsEffectiveAdjusted { get; set; }
            public int foulsTeam { get; set; }
            public int foulsTeamTechnical { get; set; }
            public int leadChanges { get; set; }
            public int pointsAgainst { get; set; }
            public int pointsFromTurnovers { get; set; }
            public int pointsInThePaintAttempted { get; set; }
            public int pointsInThePaintMade { get; set; }
            public double pointsInThePaintPercentage { get; set; }
            public int reboundsPersonal { get; set; }
            public int reboundsTeam { get; set; }
            public int reboundsTeamDefensive { get; set; }
            public int reboundsTeamOffensive { get; set; }
            public int secondChancePointsAttempted { get; set; }
            public int secondChancePointsMade { get; set; }
            public double secondChancePointsPercentage { get; set; }
            public string timeLeading { get; set; }
            public int timesTied { get; set; }
            public double trueShootingAttempts { get; set; }
            public double trueShootingPercentage { get; set; }
            public int turnoversTeam { get; set; }
            public int turnoversTotal { get; set; }
        }
    }
}