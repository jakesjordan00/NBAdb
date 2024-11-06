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
using static NBAdb.SiteMaster;

namespace NBAdb
{

    public partial class SportsBooks
    {


        public static int games = 0;
        public static List<string> gameID = new List<string>();
        public static List<float> oddsH = new List<float>();
        public static List<float> oddsA = new List<float>();
        public static int count = 0;

        public void SpotUp(int postback, string game, int home, int away, Label labelOddsH, Label labelOddsA)
        {
            if (postback == 1)
            {
                count = 0;
                GetQuickLines(games, game, home, away, labelOddsH, labelOddsA);
            }
            else
            {
                if (count == 0)
                {
                    GetLines();
                }
                GetQuickLines(games, game, home, away, labelOddsH, labelOddsA);
            }
        }


        public void GetQuickLines(int games, string game, int home, int away, Label labelOddsH, Label labelOddsA)
        {
            for (int i = 0; i < games; i++)
            {
                if (gameID[i] == game)
                {
                    if (oddsH[i] >= 2)
                    {
                        labelOddsH.Text = "+" + Math.Round(((oddsH[i] - 1) * 100), 0).ToString();
                    }
                    else
                    {
                        labelOddsH.Text = Math.Round((-100 / (oddsH[i] - 1)), 0).ToString();
                    }

                    if (oddsA[i] >= 2)
                    {
                        labelOddsA.Text = "+" + Math.Round(((oddsA[i] - 1) * 100), 0).ToString();
                    }
                    else
                    {
                        labelOddsA.Text = Math.Round((-100 / (oddsA[i] - 1)), 0).ToString();
                    }
                }
            }
        }
        public void GetLines()
        {
            var sbClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string sbEndpoint = "https://cdn.nba.com/static/json/liveData/odds/odds_todaysGames.json";
            try
            {

                WebRequest sbReq = WebRequest.Create(sbEndpoint);
                WebResponse sbResp = sbReq.GetResponse();
                string sbJson = sbClient.DownloadString(sbEndpoint);
                Root sbJSON = JsonConvert.DeserializeObject<Root>(sbJson);
                games = sbJSON.Games.Count;
                for (int i = 0; i < sbJSON.Games.Count; i++)
                {
                    try
                    {
                        gameID.Add(sbJSON.Games[i].GameId);
                    }
                    catch
                    {
                        gameID.Add("0");
                    }
                    try
                    {
                        oddsH.Add(float.Parse(sbJSON.Games[i].Markets[0].Books[2].Outcomes[0].Odds));
                    }
                    catch
                    {
                        oddsH.Add(0);
                    }
                    try
                    {
                        oddsA.Add(float.Parse(sbJSON.Games[i].Markets[0].Books[2].Outcomes[1].Odds));
                    }
                    catch
                    {
                        oddsA.Add(0);
                    }
                }
            }
            catch
            {

            }
            count++;
        }
        public class Game
        {
            public string GameId { get; set; }
            public string SrId { get; set; }
            public string SrMatchId { get; set; }
            public string HomeTeamId { get; set; }
            public string AwayTeamId { get; set; }
            public List<Market> Markets { get; set; }
        }

        public class Market
        {
            public string Name { get; set; }
            public int OddsTypeId { get; set; }
            public string GroupName { get; set; }
            public List<Book> Books { get; set; }
        }

        public class Book
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public List<Outcome> Outcomes { get; set; }
            public string Url { get; set; }
            public string CountryCode { get; set; }
        }

        public class Outcome
        {
            public int OddsFieldId { get; set; }
            public string Type { get; set; }
            public string Odds { get; set; }
            public string OpeningOdds { get; set; }
            public string OddsTrend { get; set; }
            public string Spread { get; set; }
            public string OpeningSpread { get; set; }
        }

        // Example of the root class containing a list of games
        public class Root
        {
            public List<Game> Games { get; set; }
        }

    }
}