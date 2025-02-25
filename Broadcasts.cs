﻿using System;
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
using System.Reflection.Emit;

namespace NBAdb
{
    public partial class Broadcasts
    {
        public static int games = 0;
        public static List<string> gameID = new List<string>();
        public static List<string> broadcast = new List<string>();
        public static int count = 0;


        public void SpotUp(HyperLink link, string game, int status, int postback)
        {
            if(postback == 1)
            {
                count = 0;
                GetQuickBroadcast(games, game, status, link);
            }
            else
            {
                if(count == 0)
                {
                    GetBroadcast();
                }
                GetQuickBroadcast(games, game, status, link);
            }
        }
        public void GetQuickBroadcast(int games, string game, int status, HyperLink link)
        {
            for(int i = 0; i < games; i++)
            {
                if (gameID[i] == game)
                {
                    if (broadcast[i].Contains("TNT"))
                    {
                        link.NavigateUrl = "https://play.max.com/sports"; // Replace with your dynamic link
                        link.Target = "_blank"; // This makes the link open in a new tab
                        broadcast[i] = "TNT";
                    }
                    else if (broadcast[i].Contains("ESPN"))
                    {
                        link.NavigateUrl = "https://www.espn.com/watch/"; // Replace with your dynamic link
                        link.Target = "_blank"; // This makes the link open in a new tab
                        broadcast[i] = "ESPN";
                    }
                    else
                    {
                        link.NavigateUrl = "https://www.nba.com/game/abc-vs-def-" + game + "?watch";// Replace with your dynamic link
                        link.Target = "_blank"; // This makes the link open in a new tab
                        broadcast[i] = "LP";
                    }
                    if (status == 1)
                    {
                        link.Text = broadcast[i];
                    }
                }
            }
        }
        public void GetBroadcast()
        {
            games = 0;
            gameID.Clear();
            broadcast.Clear();
            var tvClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string tvEndpoint = "https://cdn.nba.com/static/json/liveData/channels/v2/channels_00.json";
            try
            {
                WebRequest tvReq = WebRequest.Create(tvEndpoint);
                WebResponse tvResp = tvReq.GetResponse();
                string tvJson = tvClient.DownloadString(tvEndpoint);
                Root tvJSON = JsonConvert.DeserializeObject<Root>(tvJson);
                games = tvJSON.Channels.Games.Count;
                for (int i = 0; i < tvJSON.Channels.Games.Count; i++)
                {
                    try
                    {
                        gameID.Add(tvJSON.Channels.Games[i].GameId);
                    }
                    catch
                    {
                        gameID.Add("0");
                    }
                    try
                    {
                        broadcast.Add(tvJSON.Channels.Games[i].Streams[0].UniqueName);
                    }                    
                    catch
                    {
                        broadcast.Add("NA");
                    }
                }
            }
            catch
            {

            }
            count++;
        }

        public class LogoMap
        {
            public string LogoMappingDark { get; set; }
            public string LogoMappingLight { get; set; }
        }

        public class CategoryData
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int CategoryRank { get; set; }
            public bool IsFeatureCategory { get; set; }
        }

        public class Thumbnails
        {
            public string ThumbnailLarge { get; set; }
            public string ThumbnailSmall { get; set; }
        }

        public class Stream
        {
            public int Rank { get; set; }
            public string Type { get; set; }
            public string ProductionId { get; set; }
            public object MediaKindVideoId { get; set; }
            public string UniqueName { get; set; }
            public string Status { get; set; }
            public bool IsRadio { get; set; }
            public string InMarketTeamTricode { get; set; }
            public string LogoMapping { get; set; }
            public LogoMap LogoMap { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public CategoryData CategoryData { get; set; }
            public int? InMarketTeamId { get; set; }
            public bool IsLocalAccess { get; set; }
            public Thumbnails Thumbnails { get; set; }
            public object FeaturedImage { get; set; }
            public object VideoDuration { get; set; }
        }

        public class Game
        {
            public string GameId { get; set; }
            public int? GameStatus { get; set; }
            public int? GameState { get; set; }
            public List<Stream> Streams { get; set; }
        }

        public class Channels
        {
            public DateTime LastUpdated { get; set; }
            public string GameDate { get; set; }
            public string LeagueId { get; set; }
            public List<Game> Games { get; set; }
        }

        public class Root
        {
            public bool IncludeTestGames { get; set; }
            public Channels Channels { get; set; }
        }

    }
}