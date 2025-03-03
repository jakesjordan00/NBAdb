using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static NBAdb.SiteMaster;
using System.Net;
using Newtonsoft.Json;
using System.Data.SqlTypes;
using UglyToad.PdfPig.Graphics;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.Text.RegularExpressions;
using System.Diagnostics;
using NBAdbPre2019;
using UglyToad.PdfPig.Graphics.Operations.SpecialGraphicsState;

namespace NBAdb
{
    public partial class Admin : System.Web.UI.Page
    {
        public static BusDriver busDriver = new BusDriver();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGameNotes_Click(object sender, EventArgs e)
        {
            List<string> links = new List<string>();
            using (SqlCommand querySearch = new SqlCommand("select distinct name from team order by Name"))
            {
                querySearch.CommandType = CommandType.Text;
                using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                {
                    querySearch.Connection = busDriver.SQLdb;
                    sDataSearch.SelectCommand = querySearch;
                    busDriver.SQLdb.Open();
                    using (SqlDataReader sdr = querySearch.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            string savepath = "C:\\Users\\derfj\\Desktop\\NBAdb\\NBAdb\\Game Notes\\";
                            string team = sdr[0].ToString().ToLower();
                            using (WebClient client = new WebClient())
                            {
                                if(team == "76ers")
                                {
                                    team = "sixers";
                                }
                                if (team == "trail blazers")
                                {
                                    team = "blazers";
                                }
                                string link1 = "https://www.nba.com/gamenotes/" + team + ".pdf";
                                links.Add(link1);
                                client.DownloadFile("https://www.nba.com/gamenotes/" + team + ".pdf", savepath + team + " - " + DateTime.Today.Month + "." + DateTime.Today.Day + "." + DateTime.Today.Year + ".pdf");
                            }
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }
        }

        protected void btnGameSchedule_Click(object sender, EventArgs e)
        {

            DateTime today = DateTime.Today;
            var sbClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string schEndpoint = "https://cdn.nba.com/static/json/staticData/scheduleLeagueV2_1.json";
            

            WebRequest schReq = WebRequest.Create(schEndpoint);
            WebResponse schResp = schReq.GetResponse();
            string schJson = sbClient.DownloadString(schEndpoint);
            ScheduleLeagueV2 schJSON = JsonConvert.DeserializeObject<ScheduleLeagueV2>(schJson);
            games = schJSON.LeagueSchedule.GameDates[1].Games.Count;


            using (SqlCommand ScheduleInsert = new SqlCommand("delete from GameSchedule"))
            {
                ScheduleInsert.Connection = busDriver.SQLdb; //17
                ScheduleInsert.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                ScheduleInsert.ExecuteNonQuery();
                busDriver.SQLdb.Close();
            }
            foreach (GameDates date in schJSON.LeagueSchedule.GameDates)
            {
                DateTime datetime = DateTime.Parse(date.GameDate);
                foreach (Game game in date.Games)
                {
                    int game_id = Int32.Parse(game.GameId);
                    int home = game.HomeTeam.TeamId;
                    int hScore = game.HomeTeam.Score;
                    int away = game.AwayTeam.TeamId;
                    int aScore = game.AwayTeam.Score;
                    using (SqlCommand ScheduleInsert = new SqlCommand("GameScheduleInsert"))
                    {
                        ScheduleInsert.Connection = busDriver.SQLdb; //17
                        ScheduleInsert.CommandType = CommandType.StoredProcedure;
                        ScheduleInsert.Parameters.AddWithValue("@dateTime", game.GameDateTimeEst);
                        ScheduleInsert.Parameters.AddWithValue("@game_id", game_id);
                        ScheduleInsert.Parameters.AddWithValue("@home_id", home);
                        ScheduleInsert.Parameters.AddWithValue("@away_id", away);
                        ScheduleInsert.Parameters.AddWithValue("@homeTri", game.HomeTeam.TeamTricode);
                        ScheduleInsert.Parameters.AddWithValue("@homeScore", hScore);
                        ScheduleInsert.Parameters.AddWithValue("@awayTri", game.AwayTeam.TeamTricode);
                        ScheduleInsert.Parameters.AddWithValue("@awayScore", aScore);

                        ScheduleInsert.Parameters.AddWithValue("@homeWs", game.HomeTeam.Wins);
                        ScheduleInsert.Parameters.AddWithValue("@homeLs", game.HomeTeam.Losses);
                        ScheduleInsert.Parameters.AddWithValue("@awayWs", game.AwayTeam.Wins);
                        ScheduleInsert.Parameters.AddWithValue("@awayLs", game.AwayTeam.Losses);
                        if(game.Broadcasters.NationalTvBroadcasters.Count > 0)
                        {
                            ScheduleInsert.Parameters.AddWithValue("@broadcast", game.Broadcasters.NationalTvBroadcasters[0].BroadcasterDisplay);
                        }
                        else
                        {
                            ScheduleInsert.Parameters.AddWithValue("@broadcast", "League Pass");
                        }
                        ScheduleInsert.Parameters.AddWithValue("@gameLabel", game.GameLabel);
                        ScheduleInsert.Parameters.AddWithValue("@gameSubLabel", game.GameSubLabel);
                        ScheduleInsert.Parameters.AddWithValue("@gameSubtype", game.GameSubtype);
                        ScheduleInsert.Parameters.AddWithValue("@gameStatus", game.GameStatus);
                        ScheduleInsert.Parameters.AddWithValue("@gameStatusText", game.GameStatusText);
                        ScheduleInsert.Parameters.AddWithValue("@date", game.GameDateEst.Date);
                        ScheduleInsert.Parameters.AddWithValue("@day", game.Day);
                        ScheduleInsert.Parameters.AddWithValue("@season_id", 2024);
                        busDriver.SQLdb.Open();                    
                        ScheduleInsert.ExecuteScalar();
                        busDriver.SQLdb.Close();
                    }
                }
            }
            //If nationalTvBroadcasters is null, then "League Pass" else broadcasterDisplay
        }


        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch(); // Create a new stopwatch for each iteration
            stopwatch.Start(); // Start timing
            DateTime start = DateTime.Now;
            List<int> games = new List<int>();
            using (SqlCommand GameCheck = new SqlCommand("GamesToUpdate"))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.StoredProcedure;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        games.Add(Int32.Parse(sdr["game_id"].ToString()));
                    }
                }
                busDriver.SQLdb.Close();
            }
            foreach(int game in games)
            {
                string dynamic = "";
                using (SqlCommand GameDelete = new SqlCommand("deleteGames"))
                {
                    GameDelete.Connection = busDriver.SQLdb;
                    GameDelete.CommandType = CommandType.StoredProcedure;
                    GameDelete.Parameters.AddWithValue("@game_id", game);
                    busDriver.SQLdb.Open();
                    dynamic = GameDelete.ExecuteScalar().ToString();
                    busDriver.SQLdb.Close();
                }
                PlayByPlay playByPlay = new PlayByPlay();
                playByPlay.Init(game, "Admin", 2024, dynamic);
                BoxScore.GetJSON(game, "Refresh", 2024);                
            }

            stopwatch.Stop();
            DateTime end = DateTime.Now;
            TimeSpan timeElapsed = stopwatch.Elapsed;
            string elapsedString = timeElapsed.Hours + ":" + timeElapsed.Minutes + ":" + timeElapsed.Seconds + "." + timeElapsed.Milliseconds;
            //For example:  2022 season: 00:04:12.123. 2023 season: 00:03:54.636. 
            
            
            lblTimeElapsed.Text += "2024" + " refresh: " + timeElapsed.Hours + ":" + timeElapsed.Minutes + ":" +
            timeElapsed.Seconds + "." + timeElapsed.Milliseconds + ". ";



        }







        protected void btnInjury_Click(object sender, EventArgs e)
        {
            string ampm = "";
            string month = "";
            int hour = DateTime.Now.Hour;
            int monthint = DateTime.Now.Month;
            string houra = hour.ToString();
            if (monthint < 10)
            {
                month = "0" + DateTime.Now.Month;
            }
            else
            {
                month = DateTime.Now.Month.ToString();
            }

            if (hour < 12)
            {
                ampm = "AM";
            }
            else
            {
                hour = hour - 12;
                ampm = "PM";
            }
            if (hour < 10)
            {
                houra = "0" + hour;
            }

            int day = DateTime.Today.Day;
            string daya = day.ToString();
            if (day < 10)
            {
                daya = "0" + day;
            }

            string savepath = "C:\\Users\\derfj\\Desktop\\NBAdb\\NBAdb\\Injury Report\\";
            string fullpath = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    string link = "https://ak-static.cms.nba.com/referee/injury/Injury-Report_" + DateTime.Today.Year + "-" + month + "-" + daya + "_" + houra + ampm + ".pdf";
                    fullpath = savepath + "Injury Report " + DateTime.Today.Year + "." + month + "." + daya + "." + houra + ampm + ".pdf";
                    client.DownloadFile("https://ak-static.cms.nba.com/referee/injury/Injury-Report_" + DateTime.Today.Year + "-" + month + "-" + daya + "_" + houra + ampm + ".pdf", fullpath);
                }
                catch
                {
                    hour = DateTime.Now.Hour - 1;
                    houra = hour.ToString();
                    if (hour < 12)
                    {
                        ampm = "AM";
                    }
                    else
                    {
                        hour = hour - 12;
                        ampm = "PM";
                    }
                    if (hour < 10)
                    {
                        houra = "0" + hour;
                    }
                    
                    fullpath = savepath + "Injury Report" + DateTime.Today.Year + "." + month + "." + DateTime.Today.Day + "." + houra + ampm + ".pdf";
                    client.DownloadFile("https://ak-static.cms.nba.com/referee/injury/Injury-Report_" + DateTime.Today.Year + "-" + month + "-" + daya + "_" + houra + ampm + ".pdf", fullpath);
                }                
            }
            using (PdfDocument document = PdfDocument.Open(fullpath))
            {
                foreach (UglyToad.PdfPig.Content.Page page in document.GetPages())
                {
                    Console.WriteLine($"--- Page {page.Number} ---");
                    string text = page.Text;
                    Console.WriteLine(text);
                    ExtractTextWithSpaces(page);

                    AddSpaceBeforeCapitalLetters(text);
                    text = text;
                }
            }

        }
        static string AddSpaceBeforeCapitalLetters(string input)
        {
            // Use Regex to add a space before each uppercase letter (except the first one)
            string test = Regex.Replace(input, "(?<!^)([A-Z])", " $1");
            return Regex.Replace(input, "(?<!^)([A-Z])", " $1");
        }
        static string ExtractTextWithSpaces(UglyToad.PdfPig.Content.Page page)
        {
            var words = page.GetWords();
            string test = string.Join(" ", words);


            return string.Join(" ", words); // Reconstruct words with spaces
        }






        protected void btnPosessions_Click(object sender, EventArgs e)
        {
            using (SqlCommand GameCheck = new SqlCommand("select distinct game_id from GameSchedule where season_id = 2024 and datetime <= '" + DateTime.Now + "' and gameLabel != 'Preseason'"))
            {
                GameCheck.Connection = busDriver.SQLdb;
                GameCheck.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = GameCheck.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        pbpPosessions pbpPosessions = new pbpPosessions();
                        pbpPosessions.Hit(Int32.Parse(sdr["game_id"].ToString()));
                    }
                }
                busDriver.SQLdb.Close();
            }
        }

        public void btnStarters_Click(object sender, EventArgs e)
        {
            StartingLineups startingLineups = new StartingLineups();
            //startingLineups.StarterInsert();
            startingLineups.StarterFullRefresh();
        }

        protected void btnLineups_Click(object sender, EventArgs e)
        {
            LineupDetails lineupDetails = new LineupDetails();
            lineupDetails.init();
        }





        public class Meta
        {
            public int Version { get; set; }
            public string Request { get; set; }
            public DateTime Time { get; set; }
        }

        public class Broadcaster
        {
            public string BroadcasterScope { get; set; }
            public string BroadcasterMedia { get; set; }
            public int BroadcasterId { get; set; }
            public string BroadcasterDisplay { get; set; }
            public string BroadcasterAbbreviation { get; set; }
            public string BroadcasterDescription { get; set; }
            public string TapeDelayComments { get; set; }
            public string BroadcasterVideoLink { get; set; }
            public int RegionId { get; set; }
            public int BroadcasterTeamId { get; set; }
        }

        public class Broadcasters
        {
            public List<Broadcaster> NationalTvBroadcasters { get; set; }
            public List<Broadcaster> NationalRadioBroadcasters { get; set; }
            public List<Broadcaster> NationalOttBroadcasters { get; set; }
            public List<Broadcaster> HomeTvBroadcasters { get; set; }
            public List<Broadcaster> HomeRadioBroadcasters { get; set; }
            public List<Broadcaster> HomeOttBroadcasters { get; set; }
            public List<Broadcaster> AwayTvBroadcasters { get; set; }
            public List<Broadcaster> AwayRadioBroadcasters { get; set; }
            public List<Broadcaster> AwayOttBroadcasters { get; set; }
            public List<Broadcaster> IntlRadioBroadcasters { get; set; }
            public List<Broadcaster> IntlTvBroadcasters { get; set; }
            public List<Broadcaster> IntlOttBroadcasters { get; set; }
        }

        public class Team
        {
            public int TeamId { get; set; }
            public string TeamName { get; set; }
            public string TeamCity { get; set; }
            public string TeamTricode { get; set; }
            public string TeamSlug { get; set; }
            public int Wins { get; set; }
            public int Losses { get; set; }
            public int Score { get; set; }
            public int Seed { get; set; }
        }

        public class PointsLeader
        {
            public int PersonId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int TeamId { get; set; }
            public string TeamCity { get; set; }
            public string TeamName { get; set; }
            public string TeamTricode { get; set; }
            public double Points { get; set; }
        }

        public class Game
        {
            public string GameId { get; set; }
            public string GameCode { get; set; }
            public int GameStatus { get; set; }
            public string GameStatusText { get; set; }
            public int GameSequence { get; set; }
            public DateTime GameDateEst { get; set; }
            public DateTime GameTimeEst { get; set; }
            public DateTime GameDateTimeEst { get; set; }
            public DateTime GameDateUTC { get; set; }
            public DateTime GameTimeUTC { get; set; }
            public DateTime GameDateTimeUTC { get; set; }
            public DateTime AwayTeamTime { get; set; }
            public DateTime HomeTeamTime { get; set; }
            public string Day { get; set; }
            public int MonthNum { get; set; }
            public int WeekNumber { get; set; }
            public string WeekName { get; set; }
            public bool IfNecessary { get; set; }
            public string SeriesGameNumber { get; set; }
            public string GameLabel { get; set; }
            public string GameSubLabel { get; set; }
            public string SeriesText { get; set; }
            public string ArenaName { get; set; }
            public string ArenaState { get; set; }
            public string ArenaCity { get; set; }
            public string PostponedStatus { get; set; }
            public string BranchLink { get; set; }
            public string GameSubtype { get; set; }
            public Broadcasters Broadcasters { get; set; }
            public Team HomeTeam { get; set; }
            public Team AwayTeam { get; set; }
            public List<PointsLeader> PointsLeaders { get; set; }
        }

        public class GameDates
        {
            public string GameDate { get; set; }
            public List<Game> Games { get; set; }
        }

        public class LeagueSchedule
        {
            public string SeasonYear { get; set; }
            public string LeagueId { get; set; }
            public List<GameDates> GameDates { get; set; }
        }

        public class ScheduleLeagueV2
        {
            public Meta Meta { get; set; }
            public LeagueSchedule LeagueSchedule { get; set; }
        }

        protected void btnOldData_Click(object sender, EventArgs e)
        {

            //Pulls play by play for all of current api data
            //PullPBP();


            //Pulls data for all pre 2019 data
            Pre2019.Go();


            //Inserts only game record for all current api data and box
            //GamePost();
        }

        public static void PullPBP() 
        {
            PlayByPlay pbp = new PlayByPlay();
            using (SqlCommand TableCount = new SqlCommand("select game_id, season_id from game g where season_id > 2018 order by season_id, game_id")) 
            {
                TableCount.CommandType = CommandType.Text;
                using (SqlDataAdapter sTableCount = new SqlDataAdapter())
                {
                    TableCount.Connection = busDriver.SQLdb;
                    sTableCount.SelectCommand = TableCount;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = TableCount.ExecuteReader();
                    while(reader.Read())
                    {
                        int game = Int32.Parse(reader[0].ToString());
                        int season = Int32.Parse(reader[1].ToString());
                        //public void Init(int game_id, string sender, int id, string dynamicVariable)
                        pbp.Init(game, "FirstTimeLoad", season, "");
                    }
                    busDriver.SQLdb.Close();
                }
            }
        }


        public static void GamePost()
        {
            FirstTimeLoad first = new FirstTimeLoad();
            using (SqlCommand TableCount = new SqlCommand("select distinct p.game_id, p.season_id, g.game_id from playByPlay p left join game g on p.game_id = g.game_id where p.season_id = 2019 and g.game_id is null"))
            {
                TableCount.CommandType = CommandType.Text;
                using (SqlDataAdapter sTableCount = new SqlDataAdapter())
                {
                    TableCount.Connection = busDriver.SQLdb;
                    sTableCount.SelectCommand = TableCount;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = TableCount.ExecuteReader();
                    while (reader.Read())
                    {
                        int game = Int32.Parse(reader[0].ToString());
                        int season = Int32.Parse(reader[1].ToString());
                        first.FirstLoadGameOnly(game, season);

                    }
                    busDriver.SQLdb.Close();
                }
            }
        }


    }
}





// + "Injury Report" + DateTime.Today.Year + "." + DateTime.Today.Month + "." + DateTime.Today.Day + "." + DateTime.Today.Hour + ampm +  ".pdf"