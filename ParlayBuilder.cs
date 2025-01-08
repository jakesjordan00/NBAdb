using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static NBAdb.FirstTimeLoad;

namespace NBAdb
{
    public class PropBuilder
    {
        public static PropAssistant propAssistant = new PropAssistant();
        public static BusDriver busDriver = new BusDriver();
        public static int playerID = 0;
        public static int gamesPlayed = 0;
        public static int gamesMet = 0;
        public void BuildQuery(string Team, List<string> PlayerList, string Player1, string Player2, string Player3, string PlayerI, List<string> PropList, List<int> Player1Props, List<int> Player2Props, List<int> Player3Props)
        {            
            string querySelect = "select avg(pb.points) p1Pts, avg(pb.assists) p1Ast, avg(pb.reboundsTotal) p1Reb, avg(pb.threePointersMade) p13, avg(pb.blocks) p1Blk, avg(pb.steals) p1Stl";
            string queryFrom = " from playerbox pb";
            string queryWhere = " where pb.season_id = (select max(season_id) from team)";
            string FullQuery = "";

            Dictionary<int, string> Players = new Dictionary<int, string>();
            foreach (string player in PlayerList) 
            {
                string name = player.Remove(0, 3);
                if (player.Contains("1. "))
                {
                    GetPlayerID(name);
                    Players.Add(playerID, name);
                    queryWhere += " and pb.status = 'ACTIVE' and pb.minutesCalculated != 'PT00M' and pb.player_id = " + playerID;
                }
                if (player.Contains("2. "))
                {
                    GetPlayerID(name);
                    Players.Add(playerID, name);
                    queryFrom += " inner join playerBox pb2 on pb.game_id = pb2.game_id and pb.team_id = pb2.team_id and pb.season_id = pb2.season_id";
                    queryWhere += " and pb2.status = 'ACTIVE' and pb2.minutesCalculated != 'PT00M' and pb2.player_id = " + playerID;
                }
                if (player.Contains("3. "))
                {
                    GetPlayerID(name);
                    Players.Add(playerID, name);
                    queryFrom += " inner join playerBox pb3 on pb.game_id = pb3.game_id and pb.team_id = pb3.team_id and pb.season_id = pb3.season_id";
                    queryWhere += " and pb3.status = 'ACTIVE' and pb3.minutesCalculated != 'PT00M' and pb3.player_id = " + playerID;
                }
                if (player.Contains("I. "))
                {
                    GetPlayerID(name);
                    queryFrom += " inner join playerBox pbI on pb.game_id = pbI.game_id and pb.team_id = pbI.team_id and pb.season_id = pbI.season_id";
                    queryWhere += " and (pbI.status != 'ACTIVE' OR pbI.minutesCalculated = 'PT00M') and pbI.player_id = " + playerID;
                }
            }
            //Get gamesPlayed value for all players, including injured
            FullQuery = "select count(distinct pb.game_id) GamesPlayed " + queryFrom + queryWhere;
            GetGamesPlayed(FullQuery, "gp");

            int pl = 1;
            string table = "";
            string propValues = "";
            foreach (KeyValuePair<int, string> player in Players)
            {
                int pr = 0;
                foreach (string prop in PropList)
                {
                    List<int> DynamicPlayerProps = new List<int>();
                    if (pl == 1)
                    {
                        table = "";
                        DynamicPlayerProps.AddRange(Player1Props);
                    }
                    else if (pl == 2)
                    {
                        table = pl.ToString();
                        DynamicPlayerProps.AddRange(Player2Props);
                        //Testing out a new query 10/1
                        querySelect += ", avg(pb2." + prop + ") p2" + prop.First().ToString() + prop.Substring(1);
                    }
                    else if (pl == 3)
                    {
                        table = pl.ToString();
                        DynamicPlayerProps.AddRange(Player3Props);
                        querySelect += ", avg(pb3." + prop + ") p3" + prop.First().ToString() + prop.Substring(1);
                    }
                    queryWhere += " and pb" + table + "." + prop + " > " + DynamicPlayerProps[pr];
                    pr++;
                }                
                pl++;
            }
            FullQuery = querySelect + queryFrom + queryWhere;
            GetGamesPlayed(FullQuery, "gpM");
        }
        public static void GetPlayerID(string name)
        {
            using (SqlCommand PlayerSearch = new SqlCommand("GetPlayerID"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@name", name);
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    while (reader.Read())
                    {
                        playerID = reader.GetInt32(0);
                    }
                    busDriver.SQLdb.Close();
                }
            }
        }

        public static void GetGamesPlayed(string query, string sender)
        {
            using (SqlCommand PlayerSearch = new SqlCommand(query))
            {
                PlayerSearch.CommandType = CommandType.Text;
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    while (reader.Read())
                    {
                        if(sender == "gp")
                        {
                            gamesPlayed = reader.GetInt32(0);
                        }
                        else if(sender == "gpM")
                        {
                            gamesMet = reader.GetInt32(0);
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }
        }
    }
}