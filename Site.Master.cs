using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAdb
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string endpoint = "https://cdn.nba.com/static/json/liveData/scoreboard/todaysScoreboard_00.json";
            try
            {
                WebRequest BoxScoreReq = WebRequest.Create(endpoint);
                WebResponse BoxScoreResp = BoxScoreReq.GetResponse();
                string json = client.DownloadString(endpoint);
                Root JSON = JsonConvert.DeserializeObject<Root>(json);
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

                    Label label1 = new Label();
                    label1.ID = "GameClock" + i; 
                    label1.Text = JSON.Scoreboard.Games[i].GameClock; 

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

                    Label label3 = new Label();
                    label3.ID = "sbTeam" + i;
                    label3.Text = JSON.Scoreboard.Games[i].HomeTeam.TeamTricode;

                    colDiv2_1.Controls.Add(teamIcon);
                    colDiv2_1.Controls.Add(label3);

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
                    Label label5 = new Label();
                    label5.ID = "sbTeamA" + i;
                    label5.Text = JSON.Scoreboard.Games[i].AwayTeam.TeamTricode;

                    colDiv3_1.Controls.Add(teamIcon2);
                    colDiv3_1.Controls.Add(label5);

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
                    ScoresRow.Attributes.Add("style", "overflow-x: auto; overflow-y:auto; white-space: nowrap; max-width:1320px; width:1320px; min-width: 1320px; max-height:87.5px; width:100%; display:flex;");



                }
            }
            catch
            {

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
            public bool? InBonus { get; set; }
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