using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static NBAdb.FirstTimeLoad;

namespace NBAdb
{
    public partial class Playoffs : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            List<string> seasons = new List<string>();
            foreach (ListItem item in chkSeasons.Items)
            {
                if (item.Selected)
                {
                    seasons.Add(item.Value);
                }
            }
            if (seasons.Count == 0)
            {
                lblSeasonResult.Text = "Please select a season";
                lblSeasonResult.ForeColor = System.Drawing.Color.IndianRed;
            }
            else
            {
                for (int i = 0; i < seasons.Count; i++)
                {
                    PlayoffSeries(Int32.Parse(seasons[i]));
                }
            }
        }

        protected void PlayoffSeries(int season)
        {
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string playoffEndpoint = "https://stats.nba.com/stats/playoffbracket?LeagueID=00&SeasonYear=20" + season + "&State=2";
            string playinEndpoint = "https://stats.nba.com/stats/playoffbracket?LeagueID=00&SeasonYear=20" + season + "&State=1";
            try
            {
                
                WebRequest RequestSeasonPlayoffSeries = WebRequest.Create(playoffEndpoint);
                WebResponse ResponseSeasonPlayoffSeries = RequestSeasonPlayoffSeries.GetResponse();
                string jsonPlayoff = client.DownloadString(playoffEndpoint);
                PlayoffBracketResponse playoffData = JsonConvert.DeserializeObject<PlayoffBracketResponse>(jsonPlayoff);

                WebRequest RequestSeasonPlayinSeries = WebRequest.Create(playinEndpoint);
                WebResponse ResponseSeasonPlayinSeries = RequestSeasonPlayinSeries.GetResponse();
                string jsonPlayin = client.DownloadString(playinEndpoint);
                PlayoffBracketResponse playinData = JsonConvert.DeserializeObject<PlayoffBracketResponse>(jsonPlayin);

                for (int i = 0; i < playoffData.bracket.playoffBracketSeries.Count; i++)
                {
                    int games = playoffData.bracket.playoffBracketSeries[i].highSeedSeriesWins + playoffData.bracket.playoffBracketSeries[i].lowSeedSeriesWins;
                }

                //PlayoffBracketSeries SeasonSeries = JsonConvert.DeserializeObject<PlayoffBracketSeries>(json);
            }
            catch
            {

            }
        }
    }
    public class Meta
    {
        public int version { get; set; }
        public string request { get; set; }
        public string time { get; set; }
    }

    public class PlayoffBracketSeries
    {
        public string seriesId { get; set; }
        public int roundNumber { get; set; }
        public int seriesNumber { get; set; }
        public string seriesConference { get; set; }
        public string seriesText { get; set; }
        public int seriesStatus { get; set; }
        public int seriesWinner { get; set; }
        public int highSeedId { get; set; }
        public string highSeedCity { get; set; }
        public string highSeedName { get; set; }
        public string highSeedTricode { get; set; }
        public int highSeedRank { get; set; }
        public int highSeedSeriesWins { get; set; }
        public int highSeedRegSeasonWins { get; set; }
        public int highSeedRegSeasonLosses { get; set; }
        public int lowSeedId { get; set; }
        public string lowSeedCity { get; set; }
        public string lowSeedName { get; set; }
        public string lowSeedTricode { get; set; }
        public int lowSeedRank { get; set; }
        public int lowSeedSeriesWins { get; set; }
        public int lowSeedRegSeasonWins { get; set; }
        public int lowSeedRegSeasonLosses { get; set; }
        public int displayOrderNumber { get; set; }
        public int displayTopTeam { get; set; }
        public int displayBottomTeam { get; set; }
        public string poRoundDesc { get; set; }
        public string nextGameId { get; set; }
        public string nextGameNumber { get; set; }
        public string nextGameDateTimeEt { get; set; }
        public string nextGameDateTimeUTC { get; set; }
        public int nextGameStatus { get; set; }
        public string nextGameStatusText { get; set; }
        public int nextGameBroadcasterId { get; set; }
        public string nextGameBroadcasterDisplay { get; set; }
        public string lastCompletedGameId { get; set; }
    }

    public class PlayInBracketSeries
    {
        public string matchupType { get; set; }
        public string conference { get; set; }
        public int highSeedId { get; set; }
        public string highSeedCity { get; set; }
        public string highSeedName { get; set; }
        public string highSeedTricode { get; set; }
        public int highSeedRegSeasonWins { get; set; }
        public int highSeedRegSeasonLosses { get; set; }
        public int highSeedRank { get; set; }
        public int lowSeedId { get; set; }
        public string lowSeedCity { get; set; }
        public string lowSeedName { get; set; }
        public string lowSeedTricode { get; set; }
        public int lowSeedRegSeasonWins { get; set; }
        public int lowSeedRegSeasonLosses { get; set; }
        public int lowSeedRank { get; set; }
        public string seriesId { get; set; }
        public string nextGameId { get; set; }
        public string nextGameDateTimeEt { get; set; }
        public string nextGameDateTimeUTC { get; set; }
        public int nextGameStatus { get; set; }
        public string nextGameStatusText { get; set; }
        public int nextGameBroadcasterId { get; set; }
        public string nextGameBroadcasterDisplay { get; set; }
    }


    public class Bracket
    {
        public string leagueId { get; set; }
        public string seasonYear { get; set; }
        public string bracketType { get; set; }
        public List<PlayoffBracketSeries> playoffBracketSeries { get; set; }
        public List<PlayInBracketSeries> playInBracketSeries { get; set; }
    }

    public class PlayoffBracketResponse
    {
        public Meta meta { get; set; }
        public Bracket bracket { get; set; }
    }

}