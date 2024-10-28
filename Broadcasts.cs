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
using System.Reflection.Emit;

namespace NBAdb
{

    public partial class Broadcasts
    {
        public void GetBroadcast(HyperLink link, string game, int status)
        {
            var tvClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string tvEndpoint = "https://cdn.nba.com/static/json/liveData/channels/v2/channels_00.json";
            try
            {

                WebRequest tvReq = WebRequest.Create(tvEndpoint);
                WebResponse tvResp = tvReq.GetResponse();
                string tvJson = tvClient.DownloadString(tvEndpoint);
                Root tvJSON = JsonConvert.DeserializeObject<Root>(tvJson);
                string broadcast = "";
                for(int i = 0; i < tvJSON.Channels.Games.Count; i++)
                {
                    if (tvJSON.Channels.Games[i].GameId == game)
                    {
                        if (tvJSON.Channels.Games[i].Streams[0].UniqueName.Contains("TNT"))
                        {
                            link.NavigateUrl = "https://play.max.com/sports"; // Replace with your dynamic link
                            link.Target = "_blank"; // This makes the link open in a new tab
                            broadcast = "TNT";
                        }
                        else
                        {
                            link.NavigateUrl = "https://www.nba.com/game/abc-vs-def-" + game + "?watch";// Replace with your dynamic link
                            link.Target = "_blank"; // This makes the link open in a new tab
                            broadcast = "LP";
                        }




                        if (status == 1)
                        {
                            link.Text = broadcast;
                        }                        
                    }

                }
            }
            catch
            {

            }
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