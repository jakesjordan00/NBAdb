using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
        public static BusDriver busDriver = new BusDriver();
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
                    string conference = playoffData.bracket.playoffBracketSeries[i].seriesConference;
                    string round = playoffData.bracket.playoffBracketSeries[i].poRoundDesc;
                    int games = playoffData.bracket.playoffBracketSeries[i].highSeedSeriesWins + playoffData.bracket.playoffBracketSeries[i].lowSeedSeriesWins;
                    string seriesID = "004" + season.ToString() + "00" + playoffData.bracket.playoffBracketSeries[i].roundNumber + playoffData.bracket.playoffBracketSeries[i].seriesNumber + "1-" + games.ToString();
                    string firstGame = "004" + season.ToString() + "00" + playoffData.bracket.playoffBracketSeries[i].roundNumber + playoffData.bracket.playoffBracketSeries[i].seriesNumber + "1";
                    string lastGame = "004" + season.ToString() + "00" + playoffData.bracket.playoffBracketSeries[i].roundNumber + playoffData.bracket.playoffBracketSeries[i].seriesNumber + games.ToString();
                    using (SqlCommand InsertData = new SqlCommand("PlayoffSeriesInsert"))
                    {
                        InsertData.Connection = busDriver.SQLdb;
                        InsertData.CommandType = CommandType.StoredProcedure;
                        InsertData.Parameters.AddWithValue("@season_id",                    Int32.Parse(("20" + season.ToString())));
                        InsertData.Parameters.AddWithValue("@series_id",                    seriesID);
                        InsertData.Parameters.AddWithValue("@games",                        games);
                        InsertData.Parameters.AddWithValue("@roundNumber",                  playoffData.bracket.playoffBracketSeries[i].roundNumber);
                        InsertData.Parameters.AddWithValue("@seriesNumber",                 playoffData.bracket.playoffBracketSeries[i].seriesNumber);
                        InsertData.Parameters.AddWithValue("@seriesConference",             playoffData.bracket.playoffBracketSeries[i].seriesConference);
                        InsertData.Parameters.AddWithValue("@seriesText",                   playoffData.bracket.playoffBracketSeries[i].seriesText);
                        InsertData.Parameters.AddWithValue("@seriesStatus",                 playoffData.bracket.playoffBracketSeries[i].seriesStatus);
                        InsertData.Parameters.AddWithValue("@seriesWinner",                 playoffData.bracket.playoffBracketSeries[i].seriesWinner);
                        InsertData.Parameters.AddWithValue("@highSeed_id",                   playoffData.bracket.playoffBracketSeries[i].highSeedId);
                        InsertData.Parameters.AddWithValue("@highSeedCity",                 playoffData.bracket.playoffBracketSeries[i].highSeedCity   );
                        InsertData.Parameters.AddWithValue("@highSeedName",                 playoffData.bracket.playoffBracketSeries[i].highSeedName);
                        InsertData.Parameters.AddWithValue("@highSeedTricode",              playoffData.bracket.playoffBracketSeries[i].highSeedTricode);
                        InsertData.Parameters.AddWithValue("@highSeedRank",                 playoffData.bracket.playoffBracketSeries[i].highSeedRank);
                        InsertData.Parameters.AddWithValue("@highSeedSeriesWins",           playoffData.bracket.playoffBracketSeries[i].highSeedSeriesWins);
                        InsertData.Parameters.AddWithValue("@highSeedRegSeasonWins",        playoffData.bracket.playoffBracketSeries[i].highSeedRegSeasonWins);
                        InsertData.Parameters.AddWithValue("@highSeedRegSeasonLosses",      playoffData.bracket.playoffBracketSeries[i].highSeedRegSeasonLosses);
                        InsertData.Parameters.AddWithValue("@lowSeed_id",                   playoffData.bracket.playoffBracketSeries[i].lowSeedId);
                        InsertData.Parameters.AddWithValue("@lowSeedCity",                  playoffData.bracket.playoffBracketSeries[i].lowSeedCity);
                        InsertData.Parameters.AddWithValue("@lowSeedName",                  playoffData.bracket.playoffBracketSeries[i].lowSeedName);
                        InsertData.Parameters.AddWithValue("@lowSeedTricode",               playoffData.bracket.playoffBracketSeries[i].lowSeedTricode);
                        InsertData.Parameters.AddWithValue("@lowSeedRank",                  playoffData.bracket.playoffBracketSeries[i].lowSeedRank);
                        InsertData.Parameters.AddWithValue("@lowSeedSeriesWins",            playoffData.bracket.playoffBracketSeries[i].lowSeedSeriesWins);
                        InsertData.Parameters.AddWithValue("@lowSeedRegSeasonWins",         playoffData.bracket.playoffBracketSeries[i].lowSeedRegSeasonWins);
                        InsertData.Parameters.AddWithValue("@lowSeedRegSeasonLosses",       playoffData.bracket.playoffBracketSeries[i].lowSeedRegSeasonLosses);
                        InsertData.Parameters.AddWithValue("@displayOrderNumber",           playoffData.bracket.playoffBracketSeries[i].displayOrderNumber);
                        InsertData.Parameters.AddWithValue("@displayTopTeam",               playoffData.bracket.playoffBracketSeries[i].displayTopTeam);
                        InsertData.Parameters.AddWithValue("@displayBottomTeam",            playoffData.bracket.playoffBracketSeries[i].displayBottomTeam);
                        InsertData.Parameters.AddWithValue("@firstGame_id", firstGame);
                        InsertData.Parameters.AddWithValue("@lastGame_id", lastGame);
                        if (playoffData.bracket.playoffBracketSeries[i].seriesConference == "NBA Finals")
                        {
                            InsertData.Parameters.AddWithValue("@roundDesc", "NBA Finals");
                        }
                        else
                        {
                            InsertData.Parameters.AddWithValue("@roundDesc", playoffData.bracket.playoffBracketSeries[i].poRoundDesc);
                        }
                        InsertData.Parameters.AddWithValue("@nextGame_id",                  playoffData.bracket.playoffBracketSeries[i].nextGameId);
                        InsertData.Parameters.AddWithValue("@nextGameNumber",               playoffData.bracket.playoffBracketSeries[i].nextGameNumber);
                        InsertData.Parameters.AddWithValue("@nextGameDateTimeEt",           playoffData.bracket.playoffBracketSeries[i].nextGameDateTimeEt);
                        InsertData.Parameters.AddWithValue("@nextGameDateTimeUTC",          playoffData.bracket.playoffBracketSeries[i].nextGameDateTimeUTC);
                        InsertData.Parameters.AddWithValue("@nextGameStatus",               playoffData.bracket.playoffBracketSeries[i].nextGameStatus);
                        InsertData.Parameters.AddWithValue("@nextGameStatusText",           playoffData.bracket.playoffBracketSeries[i].nextGameStatusText);
                        InsertData.Parameters.AddWithValue("@nextGameBroadcaster_id",       playoffData.bracket.playoffBracketSeries[i].nextGameBroadcasterId);
                        InsertData.Parameters.AddWithValue("@nextGameBroadcasterDisplay",   playoffData.bracket.playoffBracketSeries[i].nextGameBroadcasterDisplay);
                        InsertData.Parameters.AddWithValue("@lastCompletedGame_id",            playoffData.bracket.playoffBracketSeries[i].lastCompletedGameId);
                        busDriver.SQLdb.Open();
                        try
                        {
                            InsertData.ExecuteScalar();
                        }
                        catch (SqlException ex)
                        {

                        }
                        busDriver.SQLdb.Close();
                        BoxScorePlayoff boxScorePlayoff = new BoxScorePlayoff();
                        boxScorePlayoff.Init(seriesID, firstGame, lastGame, season + 2000, games);
    }
                }
                for (int i = 0; i < playinData.bracket.playInBracketSeries.Count; i++)
                {
                    string gameidTemplate = "005";

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