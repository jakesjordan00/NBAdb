using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Data.SqlTypes;

namespace NBAdb
{
    public  class pbpPosessions
    {
        public static void Hit(int game)
        {
            var Client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string Endpoint = "http://www.nbagameflow.com/api/combined_pbp?game_id=" + game;
            try
            {
                WebRequest Req = WebRequest.Create(Endpoint);
                WebResponse Resp = Req.GetResponse();
                string pbpPos = Client.DownloadString(Endpoint);
                string pbpPosMod = pbpPos.Replace("game_id", "~").Replace("event_num", "#").Replace("action_id", "@").Replace("possession_id", "$");
                int game_id = 0;
                List<int> event_num = new List<int>();
                List<int> action_id = new List<int>();
                List<int> possession_id = new List<int>();


                List<string> events = new List<string>();
                List<string> actions = new List<string>();
                List<string> possessions = new List<string>();

                game_id = pbpPosMod.IndexOf("~") + 4;
                string gameT = pbpPosMod.Substring(game_id, 8);

                //+4
                //game_id is 8 characters
                int eventCount = 0;
                int actionCount = 0;
                int possessionCount = 0;
                foreach(char c in pbpPosMod)
                {
                    if(c == '#')
                    {
                        eventCount++;
                    }
                    else if(c == '@')
                    {
                        actionCount++;
                    }
                    else if (c == '$')
                    {
                        possessionCount++;
                    }

                }
                int lastEvent = 0;
                for(int i = 0; i < eventCount; i++)
                {
                    event_num.Add(pbpPosMod.IndexOf('#', lastEvent));
                    string test = pbpPosMod.Substring(event_num[i] + 4, 4).Replace(",\\", "").Replace(",", "").Replace("\\", "");
                    events.Add(pbpPosMod.Substring(event_num[i] + 4, 4).Replace(",\\", "").Replace(",", "").Replace("\"", "").Replace(".0", "").Replace(".", ""));
                    lastEvent = event_num[i] + 1;
                }



                int lastAction = 0;
                for (int i = 0; i < actionCount; i++)
                {
                    action_id.Add(pbpPosMod.IndexOf('@', lastAction));
                    actions.Add(pbpPosMod.Substring(action_id[i] + 4, 3).Replace(",\\", "").Replace(",", ""));
                    lastAction = action_id[i] + 1;
                }


                int lastPossession = 0;
                for (int i = 0; i < possessionCount; i++)
                {
                    possession_id.Add(pbpPosMod.IndexOf('$', lastPossession));
                    possessions.Add(pbpPosMod.Substring(possession_id[i] + 4, 3).Replace(",\\", "").Replace(",", ""));
                    lastPossession = possession_id[i] + 1;
                }
                GameEvent pbpPosess = JsonConvert.DeserializeObject<GameEvent>(pbpPos);
            }
            catch
            {

            }
        }

        public class GameEvent
        {
            public int GameId { get; set; }
            public int EventNum { get; set; }
            public int ActionId { get; set; }
            public int Season { get; set; }
            public string SeasonType { get; set; }
            public int AwayTeamId { get; set; }
            public string AwayTeamTricode { get; set; }
            public int HomeTeamId { get; set; }
            public string HomeTeamTricode { get; set; }
            public int EndOfPossession { get; set; }
            public double PossessionId { get; set; }
            public int Period { get; set; }
            public string GameClock { get; set; }
            public int AwayScore { get; set; }
            public int HomeScore { get; set; }
            public double? TeamId { get; set; }
            public string TeamTricode { get; set; }
            public int EventMsgType { get; set; }
            public string ActionType { get; set; }
            public string ActionSubType { get; set; }
            public string Description { get; set; }
            public string SecondaryDescription { get; set; }
            public int Person1Type { get; set; }
            public int Player1Id { get; set; }
            public string Player1Name { get; set; }
            public double? Player1TeamId { get; set; }
            public string Player1TeamTricode { get; set; }
            public int Player2Type { get; set; }
            public int Player2Id { get; set; }
            public string Player2Name { get; set; }
            public double? Player2TeamId { get; set; }
            public string Player2TeamTricode { get; set; }
            public string Person3Type { get; set; }
            public int Player3Id { get; set; }
            public string Player3Name { get; set; }
            public double? Player3TeamId { get; set; }
            public string Player3TeamTricode { get; set; }
            public string ShotType { get; set; }
            public string ShotZoneBasic { get; set; }
            public string ShotZoneArea { get; set; }
            public string ShotZoneRange { get; set; }
            public int? ShotDistance { get; set; }
            public string ShotchartLocationX { get; set; }
            public string ShotchartLocationY { get; set; }
            public int Assist { get; set; }
            public int Steal { get; set; }
            public int Block { get; set; }
            public string FoulType { get; set; }
            public int FreeThrowMade { get; set; }
            public int? FreeThrowAttempt { get; set; }
            public int? FreeThrowOutOf { get; set; }
            public int TechnicalFreeThrow { get; set; }
            public int ClearPathFreeThrow { get; set; }
            public int FlagrantFreeThrow { get; set; }
            public int TransitionTakeFoulFreeThrow { get; set; }
            public string FreeThrowFoulType { get; set; }
            public double PrevEventNum { get; set; }
            public string PrevActionType { get; set; }
            public double? PrevTeamId { get; set; }
            public string PrevDescription { get; set; }
            public int OpeningTipWinnerId { get; set; }
            public string ReboundType { get; set; }
            public int SecondsRemainingInPeriod { get; set; }
            public int SecondsRemainingInGame { get; set; }
            public int PossessionTeamId { get; set; }
            public string PossessionTeamTricode { get; set; }
            public string Clock { get; set; }
        }
    }
}