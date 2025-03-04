using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using NBAdb;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Net.Http;
using System.Threading.Tasks;
using static NBAdbPre2019.Pre2019;


namespace NBAdbPre2019
{
    public partial class Pre2019
    {
        public static BusDriver busDriver = new BusDriver();
        public static List<string> games = new List<string>
        {
           "0041800101", "0041800102", "0041800103", "0041800104", "0041800111", "0041800112", "0041800113", "0041800114", "0041800115", "0041800121", "0041800122", "0041800123", "0041800124", "0041800125", "0041800131", "0041800132", "0041800133", "0041800134", "0041800141", "0041800142", "0041800143", "0041800144", "0041800145", "0041800146", "0041800151", "0041800152", "0041800153", "0041800154", "0041800155", "0041800156", "0041800157", "0041800161", "0041800162", "0041800163", "0041800164", "0041800165", "0041800171", "0041800172", "0041800173", "0041800174", "0041800175", "0041800201", "0041800202", "0041800203", "0041800204", "0041800205", "0041800211", "0041800212", "0041800213", "0041800214", "0041800215", "0041800216", "0041800217", "0041800221", "0041800222", "0041800223", "0041800224", "0041800225", "0041800226", "0041800231", "0041800232", "0041800233", "0041800234", "0041800235", "0041800236", "0041800237", "0041800301", "0041800302", "0041800303", "0041800304", "0041800305", "0041800306", "0041800311", "0041800312", "0041800313", "0041800314", "0041800401", "0041800402", "0041800403", "0041800404", "0041800405", "0041800406", "0041700101", "0041700102", "0041700103", "0041700104", "0041700105", "0041700106", "0041700111", "0041700112", "0041700113", "0041700114", "0041700115", "0041700116", "0041700117", "0041700121", "0041700122", "0041700123", "0041700124", "0041700125", "0041700131", "0041700132", "0041700133", "0041700134", "0041700135", "0041700136", "0041700137", "0041700141", "0041700142", "0041700143", "0041700144", "0041700145", "0041700151", "0041700152", "0041700153", "0041700154", "0041700161", "0041700162", "0041700163", "0041700164", "0041700171", "0041700172", "0041700173", "0041700174", "0041700175", "0041700176", "0041700201", "0041700202", "0041700203", "0041700204", "0041700211", "0041700212", "0041700213", "0041700214", "0041700215", "0041700221", "0041700222", "0041700223", "0041700224", "0041700225", "0041700231", "0041700232", "0041700233", "0041700234", "0041700235", "0041700301", "0041700302", "0041700303", "0041700304", "0041700305", "0041700306", "0041700307", "0041700311", "0041700312", "0041700313", "0041700314", "0041700315", "0041700316", "0041700317", "0041700401", "0041700402", "0041700403", "0041700404", "0041600101", "0041600102", "0041600103", "0041600104", "0041600105", "0041600106", "0041600111", "0041600112", "0041600113", "0041600114", "0041600121", "0041600122", "0041600123", "0041600124", "0041600125", "0041600126", "0041600131", "0041600132", "0041600133", "0041600134", "0041600135", "0041600136", "0041600141", "0041600142", "0041600143", "0041600144", "0041600151", "0041600152", "0041600153", "0041600154", "0041600155", "0041600156", "0041600161", "0041600162", "0041600163", "0041600164", "0041600165", "0041600171", "0041600172", "0041600173", "0041600174", "0041600175", "0041600176", "0041600177", "0041600201", "0041600202", "0041600203", "0041600204", "0041600205", "0041600206", "0041600207", "0041600211", "0041600212", "0041600213", "0041600214", "0041600221", "0041600222", "0041600223", "0041600224", "0041600231", "0041600232", "0041600233", "0041600234", "0041600235", "0041600236", "0041600301", "0041600302", "0041600303", "0041600304", "0041600305", "0041600311", "0041600312", "0041600313", "0041600314", "0041600401", "0041600402", "0041600403", "0041600404", "0041600405", "0041500101", "0041500102", "0041500103", "0041500104", "0041500111", "0041500112", "0041500113", "0041500114", "0041500115", "0041500116", "0041500117", "0041500121", "0041500122", "0041500123", "0041500124", "0041500125", "0041500126", "0041500127", "0041500131", "0041500132", "0041500133", "0041500134", "0041500135", "0041500136", "0041500141", "0041500142", "0041500143", "0041500144", "0041500145", "0041500151", "0041500152", "0041500153", "0041500154", "0041500161", "0041500162", "0041500163", "0041500164", "0041500165", "0041500171", "0041500172", "0041500173", "0041500174", "0041500175", "0041500176", "0041500201", "0041500202", "0041500203", "0041500204", "0041500211", "0041500212", "0041500213", "0041500214", "0041500215", "0041500216", "0041500217", "0041500221", "0041500222", "0041500223", "0041500224", "0041500225", "0041500231", "0041500232", "0041500233", "0041500234", "0041500235", "0041500236", "0041500301", "0041500302", "0041500303", "0041500304", "0041500305", "0041500306", "0041500311", "0041500312", "0041500313", "0041500314", "0041500315", "0041500316", "0041500317", "0041500401", "0041500402", "0041500403", "0041500404", "0041500405", "0041500406", "0041500407", "0041400101", "0041400102", "0041400103", "0041400104", "0041400105", "0041400106", "0041400111", "0041400112", "0041400113", "0041400114", "0041400121", "0041400122", "0041400123", "0041400124", "0041400125", "0041400126", "0041400131", "0041400132", "0041400133", "0041400134", "0041400141", "0041400142", "0041400143", "0041400144", "0041400151", "0041400152", "0041400153", "0041400154", "0041400155", "0041400161", "0041400162", "0041400163", "0041400164", "0041400165", "0041400166", "0041400167", "0041400171", "0041400172", "0041400173", "0041400174", "0041400175", "0041400201", "0041400202", "0041400203", "0041400204", "0041400205", "0041400206", "0041400211", "0041400212", "0041400213", "0041400214", "0041400215", "0041400216", "0041400221", "0041400222", "0041400223", "0041400224", "0041400225", "0041400226", "0041400231", "0041400232", "0041400233", "0041400234", "0041400235", "0041400236", "0041400237", "0041400301", "0041400302", "0041400303", "0041400304", "0041400311", "0041400312", "0041400313", "0041400314", "0041400315", "0041400401", "0041400402", "0041400403", "0041400404", "0041400405", "0041400406", "0041300101", "0041300102", "0041300103", "0041300104", "0041300105", "0041300106", "0041300107", "0041300111", "0041300112", "0041300113", "0041300114", "0041300121", "0041300122", "0041300123", "0041300124", "0041300125", "0041300126", "0041300127", "0041300131", "0041300132", "0041300133", "0041300134", "0041300135", "0041300141", "0041300142", "0041300143", "0041300144", "0041300145", "0041300146", "0041300147", "0041300151", "0041300152", "0041300153", "0041300154", "0041300155", "0041300156", "0041300157", "0041300161", "0041300162", "0041300163", "0041300164", "0041300165", "0041300166", "0041300167", "0041300171", "0041300172", "0041300173", "0041300174", "0041300175", "0041300176", "0041300201", "0041300202", "0041300203", "0041300204", "0041300205", "0041300206", "0041300211", "0041300212", "0041300213", "0041300214", "0041300215", "0041300221", "0041300222", "0041300223", "0041300224", "0041300225", "0041300231", "0041300232", "0041300233", "0041300234", "0041300235", "0041300236", "0041300301", "0041300302", "0041300303", "0041300304", "0041300305", "0041300306", "0041300311", "0041300312", "0041300313", "0041300314", "0041300315", "0041300316", "0041300401", "0041300402", "0041300403", "0041300404", "0041300405", "0041200101", "0041200102", "0041200103", "0041200104", "0041200111", "0041200112", "0041200113", "0041200114", "0041200115", "0041200116", "0041200121", "0041200122", "0041200123", "0041200124", "0041200125", "0041200126", "0041200131", "0041200132", "0041200133", "0041200134", "0041200135", "0041200136", "0041200137", "0041200141", "0041200142", "0041200143", "0041200144", "0041200145", "0041200146", "0041200151", "0041200152", "0041200153", "0041200154", "0041200161", "0041200162", "0041200163", "0041200164", "0041200165", "0041200166", "0041200171", "0041200172", "0041200173", "0041200174", "0041200175", "0041200176", "0041200201", "0041200202", "0041200203", "0041200204", "0041200205", "0041200211", "0041200212", "0041200213", "0041200214", "0041200215", "0041200216", "0041200221", "0041200222", "0041200223", "0041200224", "0041200225", "0041200231", "0041200232", "0041200233", "0041200234", "0041200235", "0041200236", "0041200301", "0041200302", "0041200303", "0041200304", "0041200305", "0041200306", "0041200307", "0041200311", "0041200312", "0041200313", "0041200314", "0041200401", "0041200402", "0041200403", "0041200404", "0041200405", 
           "0041200406", "0041200407"
        };




        public static void Go(string instructions)
        {
            string path = "";
            string yeah = "";
            if(instructions == "Postseason")
            {
                path = "playoffs\\";
            }
            //WriteFile();
            for (int i = 0; i < games.Count; i++)
            {
                string season = "20" + games[i].Substring(3, 2);
                //string filePath = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\" + path + season + "\\" + games[i] + ".txt";
                ////Full File
                //string jsonData = File.ReadAllText(filePath).TrimStart().TrimEnd();




                ////Game and Box
                //string box = jsonData.Substring(jsonData.IndexOf("\"game\": {"));
                //int boxEnd = box.IndexOf("\"playByPlay");
                //box = box.Substring(0, boxEnd); ;
                //box = box.TrimStart().TrimEnd();
                //string boxFormatted = "{" + box + "}";
                //boxFormatted = boxFormatted.Replace("},}", "}}");
                //boxFormatted = JToken.Parse(boxFormatted).ToString(Formatting.None);

                string boxOutput = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\" + path + season + "\\box\\" + games[i] + ".json";
                // Write the minified JSON back to a file
                //File.WriteAllText(boxOutput, boxFormatted);
                
                if (instructions == "Postseason")
                {
                    GetDataBox(boxOutput, season, "playoffGameInsert");
                }
                else if (instructions == "Regular Season")
                {
                    GetDataBox(boxOutput, season, "gameInsert");
                }



                ////PlayByPlay
                //string pbp = jsonData.Substring(jsonData.IndexOf("playByPlay"));
                //int pbpEnd = pbp.IndexOf("\"source\": \"hanaV3\"");
                //pbp = pbp.Substring(0, pbpEnd);
                //pbp = pbp.Replace("],", "]}");
                //pbp = pbp.TrimStart().TrimEnd();
                //string pbpFormatted = "{\"" + pbp + "}";
                //pbpFormatted = JToken.Parse(pbpFormatted).ToString(Formatting.None);
                string pbpOutput = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\" + path + season + "\\pbp\\" + games[i] + ".json";
                // Write the minified JSON back to a file
                //File.WriteAllText(pbpOutput, pbpFormatted);

                if (instructions == "Postseason")
                {
                    GetDataPBP(pbpOutput, "InsertOldPlayByPlayPlayoffData");
                }
                else if(instructions == "Regular Season")
                {
                    GetDataPBP(pbpOutput, "InsertOldPlayByPlayData");
                }

            }
        }




        public static void GetDataPBP(string file, string procedure)
        {
            string jsonData = File.ReadAllText(file);
            PlayByPlayData playByPlayData = JsonConvert.DeserializeObject<PlayByPlayData>(jsonData);
            PlayByPlay pbp = JsonConvert.DeserializeObject<PlayByPlay>(jsonData);
            for (int i = 0; i < playByPlayData.playByPlay.actions.Count(); i++)
            {
                using (SqlCommand querySearch = new SqlCommand(procedure))
                {
                    querySearch.Connection = busDriver.SQLdb;
                    querySearch.CommandType = CommandType.StoredProcedure;
                    if (procedure == "InsertOldPlayByPlayPlayoffData")
                    {
                        string series = playByPlayData.playByPlay.gameId.Substring(0, 9) + "1-";
                        querySearch.Parameters.AddWithValue("@series_id", series);
                        querySearch.Parameters.AddWithValue("@game", Int32.Parse(playByPlayData.playByPlay.gameId.Substring(9)));
                    }

                    querySearch.Parameters.AddWithValue("@season_id", Int32.Parse("20" + playByPlayData.playByPlay.gameId.Replace("004", "").Substring(0, 2)));
                    querySearch.Parameters.AddWithValue("@game_id", Int32.Parse(playByPlayData.playByPlay.gameId));
                    querySearch.Parameters.AddWithValue("@actionNumber", playByPlayData.playByPlay.actions[i].actionNumber);
                    querySearch.Parameters.AddWithValue("@actionID", playByPlayData.playByPlay.actions[i].actionID);
                    querySearch.Parameters.AddWithValue("@clock", playByPlayData.playByPlay.actions[i].clock);
                    querySearch.Parameters.AddWithValue("@period", playByPlayData.playByPlay.actions[i].period);
                    querySearch.Parameters.AddWithValue("@team_id", playByPlayData.playByPlay.actions[i].teamId);
                    querySearch.Parameters.AddWithValue("@teamTricode", playByPlayData.playByPlay.actions[i].teamTricode);
                    querySearch.Parameters.AddWithValue("@player_id", playByPlayData.playByPlay.actions[i].personId);
                    if(playByPlayData.playByPlay.actions[i].xLegacy == 0 && playByPlayData.playByPlay.actions[i].yLegacy == 0)
                    {
                        querySearch.Parameters.AddWithValue("@x", SqlDouble.Null);
                        querySearch.Parameters.AddWithValue("@y", SqlDouble.Null);

                    }
                    else
                    {
                        querySearch.Parameters.AddWithValue("@x", playByPlayData.playByPlay.actions[i].xLegacy);
                        querySearch.Parameters.AddWithValue("@y", playByPlayData.playByPlay.actions[i].yLegacy);
                    }
                    querySearch.Parameters.AddWithValue("@shotDistance", playByPlayData.playByPlay.actions[i].shotDistance);
                    querySearch.Parameters.AddWithValue("@shotResult", playByPlayData.playByPlay.actions[i].shotResult);
                    querySearch.Parameters.AddWithValue("@isFieldGoal", playByPlayData.playByPlay.actions[i].isFieldGoal);
                    if(playByPlayData.playByPlay.actions[i].scoreHome is null)
                    {
                        querySearch.Parameters.AddWithValue("@scoreHome", 0);
                    }
                    else
                    {
                        querySearch.Parameters.AddWithValue("@scoreHome", playByPlayData.playByPlay.actions[i].scoreHome);
                    }
                    if (playByPlayData.playByPlay.actions[i].scoreAway is null)
                    {
                        querySearch.Parameters.AddWithValue("@scoreAway", 0);
                    }
                    else
                    {
                        querySearch.Parameters.AddWithValue("@scoreAway", playByPlayData.playByPlay.actions[i].scoreAway);
                    }

                    querySearch.Parameters.AddWithValue("@description", playByPlayData.playByPlay.actions[i].description);
                    querySearch.Parameters.AddWithValue("@actionType", playByPlayData.playByPlay.actions[i].actionType);
                    querySearch.Parameters.AddWithValue("@actionSub", playByPlayData.playByPlay.actions[i].subType);

                    if(playByPlayData.playByPlay.actions[i].shotValue == 2 && playByPlayData.playByPlay.actions[i].shotResult == "Missed")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "2PTA");
                    }
                    else if (playByPlayData.playByPlay.actions[i].shotValue == 2 && playByPlayData.playByPlay.actions[i].shotResult == "Made")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "2PTM");
                    }
                    else if (playByPlayData.playByPlay.actions[i].shotValue == 3 && playByPlayData.playByPlay.actions[i].shotResult == "Missed")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "3PTA");
                    }
                    else if (playByPlayData.playByPlay.actions[i].shotValue == 3 && playByPlayData.playByPlay.actions[i].shotResult == "Made")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "3PTM");
                    }
                    else if (playByPlayData.playByPlay.actions[i].actionType.Contains("Free Throw") && playByPlayData.playByPlay.actions[i].shotResult == "Missed")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "FTA");
                    }
                    else if (playByPlayData.playByPlay.actions[i].actionType.Contains("Free Throw") && playByPlayData.playByPlay.actions[i].shotResult == "Made")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "FTM");
                    }
                    else
                    {
                        querySearch.Parameters.AddWithValue("@shotType", SqlString.Null);
                    }
                    busDriver.SQLdb.Open();
                    querySearch.ExecuteScalar(); // Used for other than SELECT Queries
                    busDriver.SQLdb.Close();


                    //FTA
                    //FTM

                }
            }
        }




        public static void GetDataBox(string file, string seasonString, string procedure)
        {
            string jsonData = File.ReadAllText(file);
            GameData game = JsonConvert.DeserializeObject<GameData>(jsonData);
                                                                                                        //Game Table
            int season = Int32.Parse(seasonString);
            int game_id = Int32.Parse(game.game.GameId);
            string date = game.game.GameCode.Split('/')[0];
            int homeID = game.game.HomeTeamId;
            int awayID = game.game.AwayTeamId;
            int loserID = 0;
            int loserScore = 0;
            int winnerID = 0;
            int winnerScore = 0;
            if (game.game.HomeTeam.Score < game.game.AwayTeam.Score)
            {
                loserID = homeID;
                loserScore = game.game.HomeTeam.Score;
                winnerID = awayID;
                winnerScore = game.game.AwayTeam.Score;
            }
            else
            {
                loserID = awayID;
                loserScore = game.game.AwayTeam.Score;
                winnerID = homeID;
                winnerScore = game.game.HomeTeam.Score;
            }
            int arenaID = game.game.Arena.ArenaId;
            int sellout = game.game.Sellout;
            //Game Table end 

            using (SqlCommand InsertData = new SqlCommand(procedure))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
                if(procedure == "playoffGameInsert")
                {
                    string series = game.game.GameId.Substring(0, 9) + "1-";
                    InsertData.Parameters.AddWithValue("@series_id", series);
                }
                InsertData.Parameters.AddWithValue("@id", season);
                InsertData.Parameters.AddWithValue("@game_id", game_id);
                InsertData.Parameters.AddWithValue("@date", date);
                InsertData.Parameters.AddWithValue("@team_idH", homeID);
                InsertData.Parameters.AddWithValue("@team_idA", awayID);
                InsertData.Parameters.AddWithValue("@team_idW", winnerID);
                InsertData.Parameters.AddWithValue("@wScore", winnerScore);
                InsertData.Parameters.AddWithValue("@team_idL", loserID);
                InsertData.Parameters.AddWithValue("@lScore", loserScore);
                InsertData.Parameters.AddWithValue("@arena_id", arenaID);
                InsertData.Parameters.AddWithValue("@sellout", sellout);
                busDriver.SQLdb.Open();
                InsertData.ExecuteScalar();
                busDriver.SQLdb.Close();
            }

            string newSender = "";
            if (procedure == "playoffGameInsert")
            {
                newSender = "OldPlayerBoxPlayoffsInsert";
            }
            else if (procedure == "gameInsert")
            {
                newSender = "OldPlayerBoxInsert";
            }


            SqlDateTime gameDate = SqlDateTime.Parse(date);                                             //Players
            for (int i = 0; i < game.game.HomeTeam.Players.Count; i++)
            {
                BoxPost(game, "home", i, homeID, season, newSender);
                int player = game.game.HomeTeam.Players[i].PersonId;
                //PlayerTeamCheck(game_id, player, homeID, gameDate, season);
            }
            for (int i = 0; i < game.game.AwayTeam.Players.Count; i++)
            {
                BoxPost(game, "away", i, awayID, season, newSender);
                int player = game.game.AwayTeam.Players[i].PersonId;
                //PlayerTeamCheck(game_id, player, awayID, gameDate, season);
            }
        }


        public static void BoxPost(GameData game, string sender, int i, int team_id, int season, string procedure)
        {
            int game_id = Int32.Parse(game.game.GameId);
            int player = 0;
            string status = "";
            string minutes = "";
            string minutesCalculated = "";

            int starter = 0;
            string position = "";

            int points = 0;
            int assists = 0;
            int rebounds = 0;
            int dRebounds = 0;
            int oRebounds = 0;
            int blocks = 0;
            int fga = 0;
            int fgm = 0;
            double fgp = 0;
            int fouls = 0;
            int fta = 0;
            int ftm = 0;
            double ftp = 0;
            int plusMinus = 0;
            int steals = 0;
            int fg3a = 0;
            int fg3m = 0;
            double fg3p = 0;
            int turnovers = 0;
            int fg2a = 0; 
            int fg2m = 0;

            double fg2p = 0;
            if(fg2a > 0)
            {
                fg2p = fg2m / fg2a;
            }
            
            if (sender == "home")
            {
                player = game.game.HomeTeam.Players[i].PersonId;
                status = "";
                minutes = "";
                if (game.game.HomeTeam.Players[i].Statistics.Minutes == "")
                {
                    status = "INACTIVE";
                    minutes = "PT00M00.00S";
                }
                else
                {
                    status = "Active";
                    minutes = "PT" + game.game.HomeTeam.Players[i].Statistics.Minutes.Split(':')[0] + "M" + game.game.HomeTeam.Players[i].Statistics.Minutes.Split(':')[0] + ".00S";
                }
                minutesCalculated = minutes.Substring(0, 5);

                starter = 0;
                position = "";
                if (game.game.HomeTeam.Players[i].Position != "")
                {
                    starter = 1;
                    position = game.game.HomeTeam.Players[i].Position;
                }

                points = game.game.HomeTeam.Players[i].Statistics.Points;
                assists = game.game.HomeTeam.Players[i].Statistics.Assists;
                rebounds = game.game.HomeTeam.Players[i].Statistics.ReboundsTotal;
                dRebounds = game.game.HomeTeam.Players[i].Statistics.ReboundsDefensive;
                oRebounds = game.game.HomeTeam.Players[i].Statistics.ReboundsOffensive;
                blocks = game.game.HomeTeam.Players[i].Statistics.Blocks;
                fga = game.game.HomeTeam.Players[i].Statistics.FieldGoalsAttempted;
                fgm = game.game.HomeTeam.Players[i].Statistics.FieldGoalsMade;
                fgp = game.game.HomeTeam.Players[i].Statistics.FieldGoalsPercentage;
                fouls = game.game.HomeTeam.Players[i].Statistics.FoulsPersonal;
                fta = game.game.HomeTeam.Players[i].Statistics.FreeThrowsAttempted;
                ftm = game.game.HomeTeam.Players[i].Statistics.FreeThrowsMade;
                ftp = game.game.HomeTeam.Players[i].Statistics.FreeThrowsPercentage;
                plusMinus = game.game.HomeTeam.Players[i].Statistics.PlusMinusPoints;
                steals = game.game.HomeTeam.Players[i].Statistics.Steals;
                fg3a = game.game.HomeTeam.Players[i].Statistics.ThreePointersAttempted;
                fg3m = game.game.HomeTeam.Players[i].Statistics.ThreePointersMade;
                fg3p = game.game.HomeTeam.Players[i].Statistics.ThreePointersPercentage;
                turnovers = game.game.HomeTeam.Players[i].Statistics.Turnovers;
                fg2a = game.game.HomeTeam.Players[i].Statistics.FieldGoalsAttempted - game.game.HomeTeam.Players[i].Statistics.ThreePointersAttempted;
                fg2m = game.game.HomeTeam.Players[i].Statistics.FieldGoalsMade - game.game.HomeTeam.Players[i].Statistics.ThreePointersMade;
                if (fg2a > 0)
                {
                    fg2p = fg2m / fg2a;
                }
            }
            if (sender != "home")
            {
                player = game.game.AwayTeam.Players[i].PersonId;
                status = "";
                minutes = "";
                if (game.game.AwayTeam.Players[i].Statistics.Minutes == "")
                {
                    status = "INACTIVE";
                    minutes = "PT00M00.00S";
                }
                else
                {
                    status = "Active";
                    minutes = "PT" + game.game.AwayTeam.Players[i].Statistics.Minutes.Split(':')[0] + "M" + game.game.AwayTeam.Players[i].Statistics.Minutes.Split(':')[0] + ".00S";
                }
                minutesCalculated = minutes.Substring(0, 5);

                starter = 0;
                position = "";
                if (game.game.AwayTeam.Players[i].Position != "")
                {
                    starter = 1;
                    position = game.game.AwayTeam.Players[i].Position;
                }

                points = game.game.AwayTeam.Players[i].Statistics.Points;
                assists = game.game.AwayTeam.Players[i].Statistics.Assists;
                rebounds = game.game.AwayTeam.Players[i].Statistics.ReboundsTotal;
                dRebounds = game.game.AwayTeam.Players[i].Statistics.ReboundsDefensive;
                oRebounds = game.game.AwayTeam.Players[i].Statistics.ReboundsOffensive;
                blocks = game.game.AwayTeam.Players[i].Statistics.Blocks;
                fga = game.game.AwayTeam.Players[i].Statistics.FieldGoalsAttempted;
                fgm = game.game.AwayTeam.Players[i].Statistics.FieldGoalsMade;
                fgp = game.game.AwayTeam.Players[i].Statistics.FieldGoalsPercentage;
                fouls = game.game.AwayTeam.Players[i].Statistics.FoulsPersonal;
                fta = game.game.AwayTeam.Players[i].Statistics.FreeThrowsAttempted;
                ftm = game.game.AwayTeam.Players[i].Statistics.FreeThrowsMade;
                ftp = game.game.AwayTeam.Players[i].Statistics.FreeThrowsPercentage;
                plusMinus = game.game.AwayTeam.Players[i].Statistics.PlusMinusPoints;
                steals = game.game.AwayTeam.Players[i].Statistics.Steals;
                fg3a = game.game.AwayTeam.Players[i].Statistics.ThreePointersAttempted;
                fg3m = game.game.AwayTeam.Players[i].Statistics.ThreePointersMade;
                fg3p = game.game.AwayTeam.Players[i].Statistics.ThreePointersPercentage;
                turnovers = game.game.AwayTeam.Players[i].Statistics.Turnovers;
                fg2a = game.game.AwayTeam.Players[i].Statistics.FieldGoalsAttempted - game.game.AwayTeam.Players[i].Statistics.ThreePointersAttempted;
                fg2m = game.game.AwayTeam.Players[i].Statistics.FieldGoalsMade - game.game.AwayTeam.Players[i].Statistics.ThreePointersMade;               
                if (fg2a > 0)
                {
                    fg2p = fg2m / fg2a;
                }
            }

            using (SqlCommand InsertData = new SqlCommand(procedure))
            {
                InsertData.CommandType = CommandType.StoredProcedure;
                if(procedure == "OldPlayerBoxPlayoffsInsert")
                {
                    string series = game.game.GameId.Substring(0, 9) + "1-";
                    InsertData.Parameters.AddWithValue("@series_id", series);
                    InsertData.Parameters.AddWithValue("@game", Int32.Parse(game.game.GameId.Substring(9)));

                }
                InsertData.Parameters.AddWithValue("@season_id", season);
                InsertData.Parameters.AddWithValue("@game_id", game_id);
                InsertData.Parameters.AddWithValue("@team_id", team_id);
                InsertData.Parameters.AddWithValue("@player_id", player);
                InsertData.Parameters.AddWithValue("@status", status);
                InsertData.Parameters.AddWithValue("@starter", starter);
                InsertData.Parameters.AddWithValue("@position", position);
                InsertData.Parameters.AddWithValue("@points", points);
                InsertData.Parameters.AddWithValue("@assists", assists);
                InsertData.Parameters.AddWithValue("@blocks", blocks);
                InsertData.Parameters.AddWithValue("@fieldGoalsAttempted", fga);
                InsertData.Parameters.AddWithValue("@fieldGoalsMade", fgm);
                InsertData.Parameters.AddWithValue("@fieldGoalsPercentage", fgp);
                InsertData.Parameters.AddWithValue("@foulsPersonal", fouls);
                InsertData.Parameters.AddWithValue("@freeThrowsAttempted", fta);
                InsertData.Parameters.AddWithValue("@freeThrowsMade", ftm);
                InsertData.Parameters.AddWithValue("@freeThrowsPercentage", ftp);
                InsertData.Parameters.AddWithValue("@minutes", minutes);
                InsertData.Parameters.AddWithValue("@minutesCalculated", minutesCalculated);
                InsertData.Parameters.AddWithValue("@plusMinusPoints", plusMinus);
                InsertData.Parameters.AddWithValue("@reboundsDefensive", dRebounds);
                InsertData.Parameters.AddWithValue("@reboundsOffensive", oRebounds);
                InsertData.Parameters.AddWithValue("@reboundsTotal", rebounds);
                InsertData.Parameters.AddWithValue("@steals", steals);
                InsertData.Parameters.AddWithValue("@threePointersAttempted", fg3a);
                InsertData.Parameters.AddWithValue("@threePointersMade", fg3m);
                InsertData.Parameters.AddWithValue("@threePointersPercentage", fg3p);
                InsertData.Parameters.AddWithValue("@turnovers", turnovers);
                InsertData.Parameters.AddWithValue("@twoPointersAttempted", fg2a);
                InsertData.Parameters.AddWithValue("@twoPointersMade", fg2m);
                InsertData.Parameters.AddWithValue("@twoPointersPercentage", fg2p);
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








        public class PlayByPlayData
        {
            public PlayByPlay playByPlay { get; set; }
        }

        public class PlayByPlay
        {
            public string gameId { get; set; }
            public int videoAvailable { get; set; }
            public ActionItem[] actions { get; set; }
        }

        public class ActionItem
        {
            public int actionNumber { get; set; }
            public int? actionID { get; set; }
            public string? clock { get; set; }
            public int? period { get; set; }
            public int? teamId { get; set; }
            public string? teamTricode { get; set; }
            public int? personId { get; set; }
            public string? playerName { get; set; }
            public string? description { get; set; }
            public string? actionType { get; set; }
            public string? subType { get; set; }
            public float? xLegacy { get; set; }
            public float? yLegacy { get; set; }
            public float? shotDistance { get; set; }
            public string? shotResult { get; set; }
            public int? isFieldGoal { get; set; }
            public int? scoreHome { get; set; }
            public int? scoreAway { get; set; }
            public int? shotValue { get; set; }

        }




        public class GameData
        {
            public Game game { get; set; }
        }
        public class Game
        {
            public string GameId { get; set; }
            public string GameCode { get; set; }
            public int GameStatus { get; set; }
            public string GameStatusText { get; set; }
            public int Period { get; set; }
            public string GameClock { get; set; }
            public string GameTimeUTC { get; set; }
            public string GameEt { get; set; }
            public int AwayTeamId { get; set; }
            public int HomeTeamId { get; set; }
            public string Duration { get; set; }
            public int Attendance { get; set; }
            public int Sellout { get; set; }
            public Arena Arena { get; set; }
            public List<Official> Officials { get; set; }
            public Broadcasters Broadcasters { get; set; }
            public HomeTeam HomeTeam { get; set; }
            public AwayTeam AwayTeam { get; set; }
            public LastFiveMeetings LastFiveMeetings { get; set; }
        }

        public class Arena
        {
            public int ArenaId { get; set; }
            public string ArenaName { get; set; }
            public string ArenaCity { get; set; }
            public string ArenaState { get; set; }
            public string ArenaCountry { get; set; }
            public string ArenaTimezone { get; set; }
        }

        public class Official
        {
            public int PersonId { get; set; }
            public string Name { get; set; }
            public string NameI { get; set; }
            public string FirstName { get; set; }
            public string FamilyName { get; set; }
            public string JerseyNum { get; set; }
        }

        public class Broadcasters
        {
            public List<Broadcaster> HomeTvBroadcasters { get; set; }
            public List<Broadcaster> HomeRadioBroadcasters { get; set; }
            public List<Broadcaster> AwayTvBroadcasters { get; set; }
            public List<Broadcaster> AwayRadioBroadcasters { get; set; }
        }

        public class Broadcaster
        {
            public int BroadcasterId { get; set; }
            public string BroadcastDisplay { get; set; }
            public string BroadcasterDisplay { get; set; }
            public string BroadcasterVideoLink { get; set; }
        }

        public class HomeTeam
        {
            public int TeamId { get; set; }
            public string TeamName { get; set; }
            public string TeamCity { get; set; }
            public string TeamTricode { get; set; }
            public int TeamWins { get; set; }
            public int TeamLosses { get; set; }
            public int Score { get; set; }
            public List<PeriodScore> Periods { get; set; }
            public List<Player> Players { get; set; }
            public Statistics Statistics { get; set; }
        }

        public class AwayTeam
        {
            public int TeamId { get; set; }
            public string TeamName { get; set; }
            public string TeamCity { get; set; }
            public string TeamTricode { get; set; }
            public int TeamWins { get; set; }
            public int TeamLosses { get; set; }
            public int Score { get; set; }
            public List<PeriodScore> Periods { get; set; }
            public List<Player> Players { get; set; }
            public Statistics Statistics { get; set; }
        }

        public class PeriodScore
        {
            public int Period { get; set; }
            public string PeriodType { get; set; }
            public int Score { get; set; }
        }

        public class Player
        {
            public int PersonId { get; set; }
            public string FirstName { get; set; }
            public string FamilyName { get; set; }
            public string NameI { get; set; }
            public string PlayerSlug { get; set; }
            public string Position { get; set; }
            public string JerseyNum { get; set; }
            public PlayerStatistics Statistics { get; set; }
        }

        public class PlayerStatistics
        {
            public string Minutes { get; set; }
            public int FieldGoalsMade { get; set; }
            public int FieldGoalsAttempted { get; set; }
            public double FieldGoalsPercentage { get; set; }
            public int ThreePointersMade { get; set; }
            public int ThreePointersAttempted { get; set; }
            public double ThreePointersPercentage { get; set; }
            public int FreeThrowsMade { get; set; }
            public int FreeThrowsAttempted { get; set; }
            public double FreeThrowsPercentage { get; set; }
            public int ReboundsOffensive { get; set; }
            public int ReboundsDefensive { get; set; }
            public int ReboundsTotal { get; set; }
            public int Assists { get; set; }
            public int Steals { get; set; }
            public int Blocks { get; set; }
            public int Turnovers { get; set; }
            public int FoulsPersonal { get; set; }
            public int Points { get; set; }
            public int PlusMinusPoints { get; set; }
        }

        public class Statistics
        {
            public string Minutes { get; set; }
            public int FieldGoalsMade { get; set; }
            public int FieldGoalsAttempted { get; set; }
            public double FieldGoalsPercentage { get; set; }
            public int ThreePointersMade { get; set; }
            public int ThreePointersAttempted { get; set; }
            public double ThreePointersPercentage { get; set; }
            public int FreeThrowsMade { get; set; }
            public int FreeThrowsAttempted { get; set; }
            public double FreeThrowsPercentage { get; set; }
            public int ReboundsOffensive { get; set; }
            public int ReboundsDefensive { get; set; }
            public int ReboundsTotal { get; set; }
            public int Assists { get; set; }
            public int Steals { get; set; }
            public int Blocks { get; set; }
            public int Turnovers { get; set; }
            public int FoulsPersonal { get; set; }
            public int Points { get; set; }
            public int PlusMinusPoints { get; set; }
        }

        public class LastFiveMeetings
        {
            public List<Meeting> Meetings { get; set; }
        }

        public class Meeting
        {
            public int RecencyOrder { get; set; }
            public string GameId { get; set; }
            public string GameTimeUTC { get; set; }
            public string GameEt { get; set; }
            public int GameStatus { get; set; }
            public string GameStatusText { get; set; }
            public TeamScore AwayTeam { get; set; }
            public TeamScore HomeTeam { get; set; }
        }

        public class TeamScore
        {
            public int TeamId { get; set; }
            public string TeamCity { get; set; }
            public string TeamName { get; set; }
            public string TeamTricode { get; set; }
            public int Score { get; set; }
            public int Wins { get; set; }
            public int Losses { get; set; }
        }




    }
}