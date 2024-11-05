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
        public void GetLines( string game, int home, int away, Label labelOddsH, Label labelOddsA)
        {
            var sbClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string sbEndpoint = "https://cdn.nba.com/static/json/liveData/odds/odds_todaysGames.json";
            try
            {

                WebRequest sbReq = WebRequest.Create(sbEndpoint);
                WebResponse sbResp = sbReq.GetResponse();
                string sbJson = sbClient.DownloadString(sbEndpoint);
                Root sbJSON = JsonConvert.DeserializeObject<Root>(sbJson);
                for(int i = 0; i < sbJSON.Games.Count; i++)
                {
                    if (sbJSON.Games[i].GameId == game)
                    {
                        if (float.Parse(sbJSON.Games[i].Markets[0].Books[2].Outcomes[0].Odds) >= 2)
                        {
                            labelOddsH.Text = "+" + Math.Round(((float.Parse(sbJSON.Games[i].Markets[0].Books[2].Outcomes[0].Odds) - 1) * 100), 0).ToString();                          
                        }
                        else
                        {
                            labelOddsH.Text = Math.Round((-100 / (float.Parse(sbJSON.Games[i].Markets[0].Books[2].Outcomes[0].Odds) - 1)), 0).ToString();                            
                        }

                        if (float.Parse(sbJSON.Games[i].Markets[0].Books[2].Outcomes[1].Odds) >= 2)
                        {
                            labelOddsA.Text = "+" + Math.Round(((float.Parse(sbJSON.Games[i].Markets[0].Books[2].Outcomes[1].Odds) - 1) * 100), 0).ToString();
                        }
                        else
                        {
                            labelOddsA.Text = Math.Round((-100 / (float.Parse(sbJSON.Games[i].Markets[0].Books[2].Outcomes[1].Odds) - 1)), 0).ToString();
                        }
                    }

                }
            }
            catch
            {

            }
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