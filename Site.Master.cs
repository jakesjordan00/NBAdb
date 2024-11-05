using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace NBAdb
{
    public partial class SiteMaster : MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {


                var sbClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
                string sbEndpoint = "https://cdn.nba.com/static/json/liveData/scoreboard/todaysScoreboard_00.json";
                try
                {
                    WebRequest sbReq = WebRequest.Create(sbEndpoint);
                    WebResponse sbResp = sbReq.GetResponse();
                    string sbJson = sbClient.DownloadString(sbEndpoint);
                    Root JSON = JsonConvert.DeserializeObject<Root>(sbJson);
                    for (int i = 0; i < JSON.Scoreboard.Games.Count; i++)
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
                        label1.ID = "GameClock" + i;

                        Broadcasts broadcasts = new Broadcasts();
                        broadcasts.GetBroadcast(label1, JSON.Scoreboard.Games[i].GameId, JSON.Scoreboard.Games[i].GameStatus);


                        if (JSON.Scoreboard.Games[i].GameStatus == 2)
                        {
                            label1.Text = JSON.Scoreboard.Games[i].GameStatusText;
                        }
                        else if (JSON.Scoreboard.Games[i].GameStatus == 3)
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
                        label2.ID = "GameStart" + i;
                        label2.Text = JSON.Scoreboard.Games[i].GameEt.ToShortTimeString();

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
                        teamIcon.ImageUrl = "https://cdn.nba.com/logos/nba/streams/L/nss-" + JSON.Scoreboard.Games[i].HomeTeam.TeamTricode.ToLower() + "-studio.png"; // Set dynamic image URL
                        teamIcon.Width = Unit.Pixel(20); // Adjust size to your preference
                        teamIcon.Height = Unit.Pixel(20);

                        HyperLink label3 = new HyperLink();
                        label3.ID = "sbTeam" + i;
                        label3.Text = JSON.Scoreboard.Games[i].HomeTeam.TeamTricode;
                        if (JSON.Scoreboard.Games[i].HomeTeam.TeamTricode == "NOP")
                        {
                            label3.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/no/new-orleans-pelicans";
                        }
                        else if (JSON.Scoreboard.Games[i].HomeTeam.TeamTricode == "UTA")
                        {
                            label3.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/utah/utah-jazz";
                        }
                        else
                        {
                            label3.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/" + JSON.Scoreboard.Games[i].HomeTeam.TeamTricode.ToLower() + "/" + JSON.Scoreboard.Games[i].HomeTeam.TeamCity.ToLower() + "-" + JSON.Scoreboard.Games[i].HomeTeam.TeamName.ToLower();
                        }
                        label3.Target = "_blank"; // This makes the link open in a new tab
                        label3.Attributes.Add("style", "text-decoration: none; color: inherit;");


                        Label labelOdds = new Label();
                        labelOdds.ID = "sbOdds" + i;
                        labelOdds.Attributes.Add("style", "font-size:12px; padding-left: 5px");

                        colDiv2_1.Controls.Add(teamIcon);
                        colDiv2_1.Controls.Add(label3);
                        colDiv2_1.Controls.Add(labelOdds);


                        Panel colDiv2_2 = new Panel();
                        colDiv2_2.Attributes.Add("style", "width:75px; text-align:right");

                        Label label4 = new Label();
                        label4.ID = "sbTeamPts" + i;
                        label4.Text = JSON.Scoreboard.Games[i].HomeTeam.Score.ToString();

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
                        teamIcon2.ImageUrl = "https://cdn.nba.com/logos/nba/streams/L/nss-" + JSON.Scoreboard.Games[i].AwayTeam.TeamTricode.ToLower() + "-studio.png"; // Set dynamic image URL
                        teamIcon2.Width = Unit.Pixel(20); // Adjust size to your preference
                        teamIcon2.Height = Unit.Pixel(20);


                        HyperLink label5 = new HyperLink();
                        label5.ID = "sbTeamA" + i;
                        label5.Text = JSON.Scoreboard.Games[i].AwayTeam.TeamTricode;

                        if (JSON.Scoreboard.Games[i].AwayTeam.TeamTricode == "NOP")
                        {
                            label5.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/no/new-orleans-pelicans";
                        }
                        else if (JSON.Scoreboard.Games[i].AwayTeam.TeamTricode == "UTA")
                        {
                            label5.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/utah/utah-jazz";
                        }
                        else
                        {
                            label5.NavigateUrl = "https://www.espn.com/nba/team/depth/_/name/" + JSON.Scoreboard.Games[i].AwayTeam.TeamTricode.ToLower() + "/" + JSON.Scoreboard.Games[i].AwayTeam.TeamCity.ToLower() + "-" + JSON.Scoreboard.Games[i].AwayTeam.TeamName.ToLower();
                        }




                        label5.Target = "_blank"; // This makes the link open in a new tab
                        label5.Attributes.Add("style", "text-decoration: none; color: inherit;");


                        Label labelOddsA = new Label();
                        labelOddsA.ID = "sbOddsA" + i;
                        labelOddsA.Attributes.Add("style", "font-size:12px; padding-left: 5px");

                        SportsBooks sportsBooks = new SportsBooks();
                        sportsBooks.GetLines(JSON.Scoreboard.Games[i].GameId, JSON.Scoreboard.Games[i].HomeTeam.TeamId, JSON.Scoreboard.Games[i].AwayTeam.TeamId, labelOdds, labelOddsA);

                        colDiv3_1.Controls.Add(teamIcon2);
                        colDiv3_1.Controls.Add(label5);
                        colDiv3_1.Controls.Add(labelOddsA);

                        Panel colDiv3_2 = new Panel();
                        colDiv3_2.Attributes.Add("style", "width:75px; text-align:right");

                        Label label6 = new Label();
                        label6.ID = "sbTeamPtsA" + i;
                        label6.Text = JSON.Scoreboard.Games[i].AwayTeam.Score.ToString();

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


                    ScoresRow.Attributes.Add("style", "overflow-x: auto; overflow-y:hidden; white-space: nowrap; max-width:" + containerWidth[JSON.Scoreboard.Games.Count].Item2 + "; width:" + containerWidth[JSON.Scoreboard.Games.Count].Item2 +
                        "; min-width: " + containerWidth[JSON.Scoreboard.Games.Count].Item2 + "; max-height:87.5px; display:flex;");





                    //if (JSON.Scoreboard.Games.Count == 8)
                    //{
                    //    ScoresContainer.Attributes.Add("style", "overflow-x: auto; overflow-y: hidden");
                    //    ScoresRow.Attributes.Add("style", "overflow-x: auto; overflow-y:hidden; white-space: nowrap; max-width:1200px; width:1200px; min-width: 1200px; max-height:87.5px; display:flex;");
                    //}
                    //else if(JSON.Scoreboard.Games.Count == 9)
                    //{
                    //    ScoresContainer.Attributes.Add("style", "overflow-x: auto; overflow-y: hidden");
                    //    ScoresRow.Attributes.Add("style", "overflow-x: auto; overflow-y:hidden; white-space: nowrap; max-width:1350px; width:1350px; min-width: 1350px; max-height:87.5px; display:flex;");
                    //}
                    //else if (JSON.Scoreboard.Games.Count == 10)
                    //{
                    //    ScoresContainer.Attributes.Add("style", "overflow-x: auto; overflow-y: hidden");
                    //    ScoresRow.Attributes.Add("style", "overflow-x: auto; overflow-y:hidden; white-space: nowrap; max-width:1500px; width:1500px; min-width: 1500px; max-height:87.5px; display:flex;");
                    //}
                    //else if (JSON.Scoreboard.Games.Count == 11)
                    //{
                    //    ScoresContainer.Attributes.Add("style", "overflow-x: auto; overflow-y: hidden");
                    //    ScoresRow.Attributes.Add("style", "overflow-x: auto; overflow-y:hidden; white-space: nowrap; max-width:1500px; width:1500px; min-width: 1500px; max-height:87.5px; display:flex;");
                    //}
                    //else if (JSON.Scoreboard.Games.Count == 12)
                    //{
                    //    ScoresContainer.Attributes.Add("style", "overflow-x: auto; overflow-y: hidden");
                    //    ScoresRow.Attributes.Add("style", "overflow-x: auto; overflow-y:hidden; white-space: nowrap; max-width:1500px; width:1500px; min-width: 1500px; max-height:87.5px; display:flex;");
                    //}
                    //else
                    //{
                    //    ScoresContainer.Attributes.Add("style", "overflow-x: hidden; overflow-y: hidden");
                    //    ScoresRow.Attributes.Add("style", "overflow-x: auto; overflow-y:hidden; white-space: nowrap; max-width:1350px; width:1350px; min-width: 1350px; max-height:87.5px; display:flex;");
                    //}



                }
                catch
                {

                }
            }
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