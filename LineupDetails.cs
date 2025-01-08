using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NBAdb
{
    public class LineupDetails
    {
        public static BusDriver busDriver = new BusDriver();



        public void init()
        {
            //int lastGame = 0;
            //using (SqlCommand GameCheck = new SqlCommand(""))
            //{
            //    GameCheck.Connection = busDriver.SQLdb;
            //    GameCheck.CommandType = CommandType.Text;
            //    busDriver.SQLdb.Open();
            //    using (SqlDataReader sdr = GameCheck.ExecuteReader())
            //    {
            //        while (sdr.Read())
            //        {
            //            lastGame = Int32.Parse(sdr["game_id"].ToString());
            //        }
            //    }
            //    busDriver.SQLdb.Close();
            //}


            //Get games without Lineup Detail
            Dictionary<int, (int, int)> gameTeams = new Dictionary<int, (int, int)>();
            using (SqlCommand GameCheck = new SqlCommand("Lineups_pbpGamesV1"))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.StoredProcedure;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        gameTeams.Add(Int32.Parse(sdr["game_id"].ToString()), (Int32.Parse(sdr["home_id"].ToString()), Int32.Parse(sdr["away_id"].ToString())));
                    }
                }
                busDriver.SQLdb.Close();
            }
            foreach(KeyValuePair<int, (int, int)> gameTeam in gameTeams)
            {

                GetPlayByPlayDetails(gameTeam.Key, gameTeam.Value.Item1, gameTeam.Value.Item2);
            }


        }


        public void GetPlayByPlayDetails(int game_id, int home_id, int away_id)
        {
            int quarter = 1;
            string clock = "PT12M00.00S";

            int homePG = 0;
            int homeSG = 0;
            int homeSF = 0;
            int homePF = 0;
            int homeC = 0;

            int awayPG = 0;
            int awaySG = 0;
            int awaySF = 0;
            int awayPF = 0;
            int awayC = 0;

            TimeSpan duration = TimeSpan.FromSeconds(0);
            //      1,   2,   3,   4,   5,   6,    7,    8,    9,    10,  11,  12,  13,  14,  15
            //plyr, Pts, Ast, Reb, FGM, FGA, FG2M, FG2A, FG3M, FG3A, FTM, FTA, BLK, STL, ORB, DRB
            Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)> homePGstats = new Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>();
            Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)> homeSGstats = new Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>();
            Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)> homeSFstats = new Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>();
            Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)> homePFstats = new Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>();
            Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)> homeCstats = new Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>();

            Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)> awayPGstats = new Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>();
            Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)> awaySGstats = new Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>();
            Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)> awaySFstats = new Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>();
            Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)> awayPFstats = new Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>();
            Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)> awayCstats = new Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>();

            Dictionary<string, int> homePlayerPos = new Dictionary<string, int>();
            Dictionary<string, int> awayPlayerPos = new Dictionary<string, int>();
            //SGet starters for each team
            using (SqlCommand pbpSelect = new SqlCommand("Lineups_starters"))
            {
                pbpSelect.Connection = busDriver.SQLdb;
                pbpSelect.CommandType = CommandType.StoredProcedure;
                pbpSelect.Parameters.AddWithValue("@game_id", game_id);
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = pbpSelect.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (Int32.Parse(sdr["team_id"].ToString()) == home_id)
                        {
                            homePlayerPos.Add(sdr["position"].ToString(), Int32.Parse(sdr["player_id"].ToString()));
                        }
                        else if (Int32.Parse(sdr["team_id"].ToString()) == away_id)
                        {
                            awayPlayerPos.Add(sdr["position"].ToString(), Int32.Parse(sdr["player_id"].ToString()));
                        }
                    }
                }
                busDriver.SQLdb.Close();
            }

            List<Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>> home = new List<Dictionary<int, (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int)>>();
            foreach (KeyValuePair<string, int> item in homePlayerPos)
            {
                homePG = homePlayerPos["PG"];
                homeSG = homePlayerPos["SG"];
                homeSF = homePlayerPos["SF"];
                homePF = homePlayerPos["PF"];
                homeC = homePlayerPos["C"];
            }


            homePGstats.Add(homePlayerPos["PG"], (homePlayerPos["PG"],0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
            homeSGstats.Add(homePlayerPos["SG"], (homePlayerPos["SG"],0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
            homeSFstats.Add(homePlayerPos["SF"], (homePlayerPos["SF"],0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
            homePFstats.Add(homePlayerPos["PF"], (homePlayerPos["PF"],0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
            homeCstats.Add( homePlayerPos["C"],   (homePlayerPos["C"], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
            home.Add(homePGstats);
            home.Add(homeSGstats);
            home.Add(homeSFstats);
            home.Add(homePFstats);
            home.Add(homeCstats);
            foreach (KeyValuePair<string, int> item in awayPlayerPos)
            {
                awayPG = awayPlayerPos["PG"];
                awaySG = awayPlayerPos["SG"];
                awaySF = awayPlayerPos["SF"];
                awayPF = awayPlayerPos["PF"];
                awayC = awayPlayerPos["C"];
                //awayPGstats.Add(awayPlayerPos["PG"], (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
                //awaySGstats.Add(awayPlayerPos["SG"], (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
                //awaySFstats.Add(awayPlayerPos["SF"], (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
                //awayPFstats.Add(awayPlayerPos["PF"], (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
                //awayCstats.Add(awayPlayerPos["C"], (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
            }






            Dictionary<int, (int, string, int)> homeChanges = new Dictionary<int, (int, string, int)>();
            //Select * from playByPlay for game_id variable
            using (SqlCommand pbpSelect = new SqlCommand("LineupChanges"))
            {
                pbpSelect.Connection = busDriver.SQLdb;
                pbpSelect.CommandType = CommandType.StoredProcedure;
                pbpSelect.Parameters.AddWithValue("@game", game_id);
                pbpSelect.Parameters.AddWithValue("@team", home_id);
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = pbpSelect.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        homeChanges.Add(Int32.Parse(sdr["actionNumber"].ToString()), (Int32.Parse(sdr["actionNumber"].ToString()), sdr["clock"].ToString(), Int32.Parse(sdr["period"].ToString())));
                    }
                }
                busDriver.SQLdb.Close();
            }


             
            //foreach(var lineupchange in homeChanges)
            //{
            //    foreach (var dictionary in home)
            //    {
            //        foreach (var kvp in dictionary)
            //        {
            //            int key = kvp.Key;
            //            var value = kvp.Value;
            //            using (SqlCommand pbpSelect = new SqlCommand("playerStats"))
            //            {
            //                pbpSelect.Connection = busDriver.SQLdb;
            //                pbpSelect.CommandType = CommandType.StoredProcedure;
            //                pbpSelect.Parameters.AddWithValue("@game", game_id);
            //                pbpSelect.Parameters.AddWithValue("@team", home_id);
            //                pbpSelect.Parameters.AddWithValue("@player", kvp.Key);
            //                pbpSelect.Parameters.AddWithValue("@change", lineupchange.Key);
            //                busDriver.SQLdb.Open();
            //                using (SqlDataReader sdr = pbpSelect.ExecuteReader())
            //                {
            //                    while (sdr.Read())
            //                    {
            //                        kvp.Value.Item1 = Int32.Parse(sdr["Pts"].ToString());
            //                    }
            //                }
            //                busDriver.SQLdb.Close();
            //            }
            //        }
            //    }
            //}

            //for(int i = 0; i < homeChanges.Count; i++)
            //{
            //    for(int j = 0; j < home.Count; j++)
            //    {
            //        for(int k = 0; k < home[j].Count; k++)
            //        {
            //            using (SqlCommand pbpSelect = new SqlCommand("playerStats"))
            //            {
            //                pbpSelect.Connection = busDriver.SQLdb;
            //                pbpSelect.CommandType = CommandType.StoredProcedure;
            //                pbpSelect.Parameters.AddWithValue("@game", game_id);
            //                pbpSelect.Parameters.AddWithValue("@team", home_id);
            //                pbpSelect.Parameters.AddWithValue("@player", home[j]);
            //                pbpSelect.Parameters.AddWithValue("@change", lineupchange.Key);
            //                busDriver.SQLdb.Open();
            //                using (SqlDataReader sdr = pbpSelect.ExecuteReader())
            //                {
            //                    while (sdr.Read())
            //                    {
            //                        kvp.Value.Item1 = Int32.Parse(sdr["Pts"].ToString());
            //                    }
            //                }
            //                busDriver.SQLdb.Close();
            //            }
            //        }
            //    }
            //}















            Dictionary<int, (string, int)> awayChanges = new Dictionary<int, (string, int)>();
            //Select * from playByPlay for game_id variable
            using (SqlCommand pbpSelect = new SqlCommand("LineupChanges"))
            {
                pbpSelect.Connection = busDriver.SQLdb;
                pbpSelect.CommandType = CommandType.StoredProcedure;
                pbpSelect.Parameters.AddWithValue("@game", game_id);
                pbpSelect.Parameters.AddWithValue("@team", away_id);
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = pbpSelect.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        awayChanges.Add(Int32.Parse(sdr["actionNumber"].ToString()), (sdr["clock"].ToString(), Int32.Parse(sdr["period"].ToString())));
                    }
                }
                busDriver.SQLdb.Close();
            }












            //Select * from playByPlay for game_id variable
            using (SqlCommand pbpSelect = new SqlCommand("Lineups_pbpSelect"))
            {
                pbpSelect.Connection = busDriver.SQLdb;
                pbpSelect.CommandType = CommandType.StoredProcedure;
                pbpSelect.Parameters.AddWithValue("@game_id", game_id);
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = pbpSelect.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        string clockTest = sdr["clock"].ToString();
                        TimeSpan clocktest = System.Xml.XmlConvert.ToTimeSpan(sdr["clock"].ToString());
                        duration += System.Xml.XmlConvert.ToTimeSpan(clock) - System.Xml.XmlConvert.ToTimeSpan(clockTest);
                        clock = sdr["clock"].ToString();


                    }
                }
                busDriver.SQLdb.Close();
            }


        }



    }
}