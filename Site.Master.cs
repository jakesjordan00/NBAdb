using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace NBAdb
{
    public partial class SiteMaster : MasterPage
    {
        public static int postback = 0;
        public static int games = 0;
        public static List<string> gameID = new List<string>();
        public static List<int> gameStatus = new List<int>();
        public static List<string> gameStatusText = new List<string>();
        public static List<string> gameStart = new List<string>();
        public static List<int> homeID = new List<int>();
        public static List<string> homeTri = new List<string>();
        public static List<string> homeCity = new List<string>();
        public static List<string> homeName = new List<string>();
        public static List<int> homeScore = new List<int>();
        public static List<int> awayID = new List<int>();
        public static List<string> awayTri = new List<string>();
        public static List<string> awayCity = new List<string>();
        public static List<string> awayName = new List<string>();
        public static List<int> awayScore = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                postback = 0;
                games = 0;
                gameID.Clear();
                gameStatus.Clear();
                gameStatusText.Clear();
                gameStart.Clear();
                homeID.Clear();
                homeTri.Clear();
                homeCity.Clear();
                homeName.Clear();
                homeScore.Clear();
                awayID.Clear();
                awayTri.Clear();
                awayCity.Clear();
                awayName.Clear();
                awayScore.Clear();

                var sbClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
                string sbEndpoint = "https://cdn.nba.com/static/json/liveData/scoreboard/todaysScoreboard_00.json";
                try
                {
                    WebRequest sbReq = WebRequest.Create(sbEndpoint);
                    WebResponse sbResp = sbReq.GetResponse();
                    string sbJson = sbClient.DownloadString(sbEndpoint);
                    Root JSON = JsonConvert.DeserializeObject<Root>(sbJson);
                    games = JSON.Scoreboard.Games.Count;
                    for (int i = 0; i < JSON.Scoreboard.Games.Count; i++)
                    {
                        gameID.Add(JSON.Scoreboard.Games[i].GameId);
                        gameStatus.Add(JSON.Scoreboard.Games[i].GameStatus);
                        gameStatusText.Add(JSON.Scoreboard.Games[i].GameStatusText);
                        gameStart.Add(JSON.Scoreboard.Games[i].GameEt.ToShortTimeString());
                        homeID.Add(JSON.Scoreboard.Games[i].HomeTeam.TeamId);
                        homeTri.Add(JSON.Scoreboard.Games[i].HomeTeam.TeamTricode);
                        homeCity.Add(JSON.Scoreboard.Games[i].HomeTeam.TeamCity);
                        homeName.Add(JSON.Scoreboard.Games[i].HomeTeam.TeamName);
                        homeScore.Add(JSON.Scoreboard.Games[i].HomeTeam.Score);
                        awayID.Add(JSON.Scoreboard.Games[i].AwayTeam.TeamId);
                        awayTri.Add(JSON.Scoreboard.Games[i].AwayTeam.TeamTricode);
                        awayCity.Add(JSON.Scoreboard.Games[i].AwayTeam.TeamCity);
                        awayName.Add(JSON.Scoreboard.Games[i].AwayTeam.TeamName);
                        awayScore.Add(JSON.Scoreboard.Games[i].AwayTeam.Score);




                    }

                }
                catch
                {

                }
            }
            else
            {
                postback = 1;
            }
            List<Tuple<int, string>> containerWidth = new List<Tuple<int, string>>();
            containerWidth.Add(new Tuple<int, string>(0, "0px"));
            containerWidth.Add(new Tuple<int, string>(1, "150px"));
            containerWidth.Add(new Tuple<int, string>(2, "300px"));
            containerWidth.Add(new Tuple<int, string>(3, "450px"));
            containerWidth.Add(new Tuple<int, string>(4, "600px"));
            containerWidth.Add(new Tuple<int, string>(5, "750px"));
            containerWidth.Add(new Tuple<int, string>(6, "900px"));
            containerWidth.Add(new Tuple<int, string>(7, "1050px"));
            containerWidth.Add(new Tuple<int, string>(8, "1200px"));
            containerWidth.Add(new Tuple<int, string>(9, "1350px"));
            containerWidth.Add(new Tuple<int, string>(10, "1500px"));
            containerWidth.Add(new Tuple<int, string>(11, "1650px"));
            containerWidth.Add(new Tuple<int, string>(12, "1800px"));
            containerWidth.Add(new Tuple<int, string>(13, "1950px"));
            containerWidth.Add(new Tuple<int, string>(14, "2100px"));
            containerWidth.Add(new Tuple<int, string>(15, "2250px"));


            ScoresRow.Attributes.Add("style", "overflow-x: auto; overflow-y:hidden; white-space: nowrap; max-width:" + containerWidth[games].Item2 + "; width:" + containerWidth[games].Item2 +
                "; min-width: " + containerWidth[games].Item2 + "; max-height:87.5px; display:flex;");

            for (int i = 0; i < games; i++)
            {
                LoadScoreboard(games, gameID[i], gameStatus[i], gameStatusText[i], gameStart[i], homeID[i], homeTri[i], homeCity[i], homeName[i], homeScore[i], awayID[i], awayTri[i], awayCity[i], awayName[i], awayScore[i], i, postback);
            }



        }



        public void LoadScoreboard(int gameCount, string game_id, int status, string statusText, string start, int hID, string hTri, string hCity, string hName, int hScore,
        int aID, string aTri, string aCity, string aName, int aScore, int counter, int postback)
        {
            // Create a container div with class 'col-md-4'
            Panel colDiv = new Panel();
            colDiv.CssClass = "col-md-3";
            colDiv.Attributes.Add("style", "width:150px");

            // First row
            Panel rowDiv1 = new Panel();
            rowDiv1.Attributes.Add("class", "row");
            rowDiv1.Attributes.Add("style", "width:150px");

            Panel colDiv1_1 = new Panel();
            colDiv1_1.Attributes.Add("style", "width:75px; text-align:left; font-size: small");

            HyperLink label1 = new HyperLink();
            label1.ID = "GameClock" + counter;

            Broadcasts broadcasts = new Broadcasts();
            broadcasts.SpotUp(label1, game_id, status, postback);


            if (status == 2)
            {
                label1.Text = statusText;
            }
            else if (status == 3)
            {
                label1.Text = "Final";
            }
            else
            {

            }
            label1.Attributes.Add("style", "text-decoration: none; color: inherit;");

            colDiv1_1.Controls.Add(label1);






            Panel colDiv1_2 = new Panel();
            colDiv1_2.Attributes.Add("style", "width:75px; text-align:right; font-size: small");

            Label label2 = new Label();
            label2.ID = "GameStart" + counter;
            label2.Text = start;

            colDiv1_2.Controls.Add(label2);

            rowDiv1.Controls.Add(colDiv1_1);
            rowDiv1.Controls.Add(colDiv1_2);



            // Second row
            Panel rowDiv2 = new Panel();
            rowDiv2.Attributes.Add("class", "row");
            rowDiv2.Attributes.Add("style", "width:150px");

            Panel colDiv2_1 = new Panel();
            colDiv2_1.Attributes.Add("style", "width:75px; text-align:left");

            Image teamIcon = new Image();
            teamIcon.ImageUrl = "https://cdn.nba.com/logos/nba/streams/L/nss-" + hTri.ToLower() + "-studio.png"; // Set dynamic image URL
            teamIcon.Width = Unit.Pixel(20); // Adjust size to your preference
            teamIcon.Height = Unit.Pixel(20);

            HyperLink label3 = new HyperLink();
            label3.ID = "sbTeam" + counter;
            label3.Text = hTri;
            if (hTri == "NOP")
            {
                label3.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/no/new-orleans-pelicans";
            }
            else if (hTri == "UTA")
            {
                label3.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/utah/utah-jazz";
            }
            else
            {
                label3.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/" + hTri.ToLower() + "/" + hCity.ToLower() + "-" + hName.ToLower();
            }
            label3.Target = "_blank"; // This makes the link open in a new tab
            label3.Attributes.Add("style", "text-decoration: none; color: inherit;");


            Label labelOdds = new Label();
            labelOdds.ID = "sbOdds" + counter;
            labelOdds.Attributes.Add("style", "font-size:12px; padding-left: 5px");

            colDiv2_1.Controls.Add(teamIcon);
            colDiv2_1.Controls.Add(label3);
            colDiv2_1.Controls.Add(labelOdds);


            Panel colDiv2_2 = new Panel();
            colDiv2_2.Attributes.Add("style", "width:75px; text-align:right");

            Label label4 = new Label();
            label4.ID = "sbTeamPts" + counter;
            label4.Text = hScore.ToString();

            colDiv2_2.Controls.Add(label4);

            rowDiv2.Controls.Add(colDiv2_1);
            rowDiv2.Controls.Add(colDiv2_2);


            // Second row
            Panel rowDiv3 = new Panel();
            rowDiv3.Attributes.Add("class", "row");
            rowDiv3.Attributes.Add("style", "width:150px");

            Panel colDiv3_1 = new Panel();
            colDiv3_1.Attributes.Add("style", "width:75px; text-align:left");


            Image teamIcon2 = new Image();
            teamIcon2.ImageUrl = "https://cdn.nba.com/logos/nba/streams/L/nss-" + aTri.ToLower() + "-studio.png"; // Set dynamic image URL
            teamIcon2.Width = Unit.Pixel(20); // Adjust size to your preference
            teamIcon2.Height = Unit.Pixel(20);


            HyperLink label5 = new HyperLink();
            label5.ID = "sbTeamA" + counter;
            label5.Text = aTri;

            if (aTri == "NOP")
            {
                label5.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/no/new-orleans-pelicans";
            }
            else if (aTri == "UTA")
            {
                label5.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/utah/utah-jazz";
            }
            else
            {
                label5.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/" + aTri.ToLower() + "/" + aCity.ToLower() + "-" + aName.ToLower();
            }




            label5.Target = "_blank"; // This makes the link open in a new tab
            label5.Attributes.Add("style", "text-decoration: none; color: inherit;");


            Label labelOddsA = new Label();
            labelOddsA.ID = "sbOddsA" + counter;
            labelOddsA.Attributes.Add("style", "font-size:12px; padding-left: 5px");



            SportsBooks sportsBooks = new SportsBooks();
            sportsBooks.SpotUp(postback, game_id, hID, aID, labelOdds, labelOddsA);

            colDiv3_1.Controls.Add(teamIcon2);
            colDiv3_1.Controls.Add(label5);
            colDiv3_1.Controls.Add(labelOddsA);

            Panel colDiv3_2 = new Panel();
            colDiv3_2.Attributes.Add("style", "width:75px; text-align:right");

            Label label6 = new Label();
            label6.ID = "sbTeamPtsA" + counter;
            label6.Text = aScore.ToString();

            colDiv3_2.Controls.Add(label6);

            rowDiv3.Controls.Add(colDiv3_1);
            rowDiv3.Controls.Add(colDiv3_2);


            // Add both rows to the main column div
            colDiv.Controls.Add(rowDiv1);
            colDiv.Controls.Add(rowDiv2);
            colDiv.Controls.Add(rowDiv3);

            // Finally, add the column div to the ScoresContainer
            ScoresRow.Controls.Add(colDiv); // ScoresContainer is the container div with runat="server"


        }

        public class Meta
        {
            public int Version { get; set; }
            public string Request { get; set; }
            public DateTime Time { get; set; }
            public int Code { get; set; }
        }

        public class Period
        {
            public int PeriodNumber { get; set; }
            public string PeriodType { get; set; }
            public int Score { get; set; }
        }

        public class Team
        {
            public int TeamId { get; set; }
            public string TeamName { get; set; }
            public string TeamCity { get; set; }
            public string TeamTricode { get; set; }
            public int Wins { get; set; }
            public int Losses { get; set; }
            public int Score { get; set; }
            public int? Seed { get; set; }
            public string? InBonus { get; set; }
            public int TimeoutsRemaining { get; set; }
            public List<Period> Periods { get; set; }
        }

        public class Leaders
        {
            public int PersonId { get; set; }
            public string Name { get; set; }
            public string JerseyNum { get; set; }
            public string Position { get; set; }
            public string TeamTricode { get; set; }
            public string PlayerSlug { get; set; }
            public int Points { get; set; }
            public int Rebounds { get; set; }
            public int Assists { get; set; }
        }

        public class GameLeaders
        {
            public Leaders HomeLeaders { get; set; }
            public Leaders AwayLeaders { get; set; }
        }

        public class PbOdds
        {
            public object Team { get; set; }
            public double Odds { get; set; }
            public int Suspended { get; set; }
        }

        public class Game
        {
            public string GameId { get; set; }
            public string GameCode { get; set; }
            public int GameStatus { get; set; }
            public string GameStatusText { get; set; }
            public int Period { get; set; }
            public string GameClock { get; set; }
            public DateTime GameTimeUTC { get; set; }
            public DateTime GameEt { get; set; }
            public int RegulationPeriods { get; set; }
            public bool IfNecessary { get; set; }
            public string SeriesGameNumber { get; set; }
            public string GameLabel { get; set; }
            public string GameSubLabel { get; set; }
            public string SeriesText { get; set; }
            public string SeriesConference { get; set; }
            public string PoRoundDesc { get; set; }
            public string GameSubtype { get; set; }
            public Team HomeTeam { get; set; }
            public Team AwayTeam { get; set; }
            public GameLeaders GameLeaders { get; set; }
            public PbOdds PbOdds { get; set; }
        }

        public class Scoreboard
        {
            public string GameDate { get; set; }
            public string LeagueId { get; set; }
            public string LeagueName { get; set; }
            public List<Game> Games { get; set; }
        }

        public class Root
        {
            public Meta Meta { get; set; }
            public Scoreboard Scoreboard { get; set; }
        }

    }
}