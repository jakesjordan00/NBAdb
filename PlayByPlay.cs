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
using static NBAdb.FirstTimeLoad;
namespace NBAdb
{
    public partial class PlayByPlay
    {
        FirstTimeLoad first = new FirstTimeLoad();
        public static BusDriver busDriver = new BusDriver();
        public static string pbpLink = "";
        BoxScorePlayoff boxScorePlayoff = new BoxScorePlayoff();
        public void Init(int game_id, string sender, int id, string dynamicVariable)
        {
            //When I get more added, like updates and such, more ifs/else ifs will be added.
            //Depending on the sender value, these would send to another method to probably check to see if the game has been posted or not and whether to update/create
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            pbpLink = "https://cdn.nba.com/static/json/liveData/playbyplay/playbyplay_00" + game_id + ".json";
            try
            {
                WebRequest PlayByPlayReq = WebRequest.Create(pbpLink);
                WebResponse PlayByPlayResp = PlayByPlayReq.GetResponse();
                string json = client.DownloadString(pbpLink);
                Root JSON = JsonConvert.DeserializeObject<Root>(json);
                int actions = JSON.game.actions.Count();
                int oldActions = 0;
                if (sender == "FirstTimeLoad")
                {
                    PlayByPlayPost(JSON, game_id, id, oldActions, actions, "playByPlayInsert", dynamicVariable);
                }
                if(sender == "Playoffs")
                {
                    PlayByPlayPost(JSON, game_id, id, oldActions, actions, "playByPlayPlayoffsInsert", dynamicVariable);
                }
                if(sender == "Placeholder")
                {
                    //Throw to another methods with actions variable that compares actions vs the rows we have in the db for that game
                }
            }
            catch
            {

            }
        }

        public static void PlayByPlayPost(Root JSON, int game_id, int id, int oldActions, int actions, string procedure, string dynamicVariable)
        {
            for (int i = oldActions; i < actions; i++)
            {
                int actionNumber = JSON.game.actions[i].actionNumber;
                string clock = JSON.game.actions[i].clock;
                DateTime timeActual = JSON.game.actions[i].timeActual;
                int period = JSON.game.actions[i].period;
                string periodType = JSON.game.actions[i].periodType;
                int? team_id = JSON.game.actions[i].teamId;
                string teamTricode = JSON.game.actions[i].teamTricode;
                //Event msg type ID
                string actionType = JSON.game.actions[i].actionType;
                string actionSub = JSON.game.actions[i].subType;
                string descriptor = JSON.game.actions[i].descriptor;
                //Need to make a case statement here
                string qualifier1 = null;
                string qualifier2 = null;
                string qualifier3 = null;
                if (JSON.game.actions[i].qualifiers.Count == 1)
                {
                    qualifier1 = JSON.game.actions[i].qualifiers[0];
                }
                if (JSON.game.actions[i].qualifiers.Count == 2)
                {
                    qualifier1 = JSON.game.actions[i].qualifiers[0];
                    qualifier2 = JSON.game.actions[i].qualifiers[1];
                }
                if (JSON.game.actions[i].qualifiers.Count == 3)
                {
                    qualifier1 = JSON.game.actions[i].qualifiers[0];
                    qualifier2 = JSON.game.actions[i].qualifiers[1];
                    qualifier3 = JSON.game.actions[i].qualifiers[2];
                }
                int player_id = JSON.game.actions[i].personId;
                double? x = JSON.game.actions[i].x;
                double? y = JSON.game.actions[i].y;
                string area = JSON.game.actions[i].area;
                string areaDetail = JSON.game.actions[i].areaDetail;
                string side = JSON.game.actions[i].side;
                double? shotDistance = JSON.game.actions[i].shotDistance;
                int scoreHome = JSON.game.actions[i].scoreHome;
                int scoreAway = JSON.game.actions[i].scoreAway;
                int isFieldGoal = JSON.game.actions[i].isFieldGoal;
                string shotResult = JSON.game.actions[i].shotResult;
                string description = JSON.game.actions[i].description;
                int? shotActionNumber = JSON.game.actions[i].shotActionNumber;
                int? player_idAST = JSON.game.actions[i].assistPersonId;
                int? player_idBLK = JSON.game.actions[i].blockPersonId;
                int? player_idSTL = JSON.game.actions[i].stealPersonId;
                int? player_idFoulDrawn = JSON.game.actions[i].foulDrawnPersonId;
                int? player_idJumpW = JSON.game.actions[i].jumpBallWonPersonId;
                int? player_idJumpL = JSON.game.actions[i].jumpBallLostPersonId;
                int? official_id = JSON.game.actions[i].officialId;

                using (SqlCommand querySearch = new SqlCommand(procedure))
                {
                    querySearch.Connection = busDriver.SQLdb;
                    querySearch.CommandType = CommandType.StoredProcedure;
                    if(procedure == "playByPlayPlayoffsInsert")
                    {
                        querySearch.Parameters.AddWithValue("@season_id", id);
                        querySearch.Parameters.AddWithValue("@series_id", dynamicVariable);
                        querySearch.Parameters.AddWithValue("@game_id", game_id);
                        querySearch.Parameters.AddWithValue("@game", Int32.Parse(game_id.ToString().Substring(7)));
                    }
                    else if(procedure == "playByPlayInsert")
                    {
                        querySearch.Parameters.AddWithValue("@id", id);
                        querySearch.Parameters.AddWithValue("@game_id", game_id);
                    }
                    querySearch.Parameters.AddWithValue("@actionNumber", actionNumber);
                    querySearch.Parameters.AddWithValue("@clock", clock);
                    querySearch.Parameters.AddWithValue("@timeActual", timeActual);
                    querySearch.Parameters.AddWithValue("@period", period);
                    querySearch.Parameters.AddWithValue("@periodType", periodType);
                    if (team_id is null)
                    {
                        team_id = 0;
                        teamTricode = "";
                    }
                    querySearch.Parameters.AddWithValue("@team_id", team_id);
                    querySearch.Parameters.AddWithValue("@teamTricode", teamTricode);
                    querySearch.Parameters.AddWithValue("@event_msg_type_id", 0);
                    querySearch.Parameters.AddWithValue("@actionType", actionType);
                    if (descriptor is null)
                    {
                        descriptor = "";
                    }
                    if (actionSub is null)
                    {
                        actionSub = "";
                        querySearch.Parameters.AddWithValue("@actionSub", actionSub);

                    }
                    else
                    {
                        querySearch.Parameters.AddWithValue("@actionSub", actionSub);
                    }
                    querySearch.Parameters.AddWithValue("@descriptor", descriptor);
                    if (qualifier1 is null)
                    {
                        qualifier1 = "";
                        qualifier2 = "";
                        qualifier3 = "";
                    }
                    querySearch.Parameters.AddWithValue("@qualifier1", qualifier1);
                    if (qualifier2 is null)
                    {
                        qualifier2 = "";
                        qualifier3 = "";
                    }
                    querySearch.Parameters.AddWithValue("@qualifier2", qualifier2);
                    if (qualifier3 is null)
                    {
                        qualifier3 = "";
                    }
                    querySearch.Parameters.AddWithValue("@qualifier3", qualifier3);
                    querySearch.Parameters.AddWithValue("@player_id", player_id);
                    if (x is null)
                    {
                        x = 0;
                        y = 0;
                    }
                    querySearch.Parameters.AddWithValue("@x", x);
                    querySearch.Parameters.AddWithValue("@y", y);
                    if (area is null)
                    {
                        area = "";
                        areaDetail = "";
                    }
                    querySearch.Parameters.AddWithValue("@area", area);
                    querySearch.Parameters.AddWithValue("@areaDetail", areaDetail);
                    if (side is null)
                    {
                        side = "";
                    }
                    querySearch.Parameters.AddWithValue("@side", side);
                    if (shotDistance is null)
                    {
                        shotDistance = 0;
                    }
                    querySearch.Parameters.AddWithValue("@shotDistance", shotDistance);
                    querySearch.Parameters.AddWithValue("@scoreHome", scoreHome);
                    querySearch.Parameters.AddWithValue("@scoreAway", scoreAway);
                    querySearch.Parameters.AddWithValue("@isFieldGoal", isFieldGoal);
                    if (shotResult is null)
                    {
                        shotResult = "";
                    }
                    querySearch.Parameters.AddWithValue("@shotResult", shotResult);
                    if (description is null)
                    {
                        description = "";
                    }
                    querySearch.Parameters.AddWithValue("@description", description);
                    if (shotActionNumber is null)
                    {
                        shotActionNumber = 0;
                    }
                    querySearch.Parameters.AddWithValue("@shotActionNumber", shotActionNumber);
                    if (player_idAST is null)
                    {
                        player_idAST = 0;
                    }
                    querySearch.Parameters.AddWithValue("@player_idAST", player_idAST);
                    if (player_idBLK is null)
                    {
                        player_idBLK = 0;
                    }
                    querySearch.Parameters.AddWithValue("@player_idBLK", player_idBLK);
                    if (player_idSTL is null)
                    {
                        player_idSTL = 0;
                    }
                    querySearch.Parameters.AddWithValue("@player_idSTL", player_idSTL);
                    if (player_idFoulDrawn is null)
                    {
                        player_idFoulDrawn = 0;
                    }
                    querySearch.Parameters.AddWithValue("@player_idFoulDrawn", player_idFoulDrawn);
                    if (player_idJumpW is null)
                    {
                        player_idJumpW = 0;
                    }
                    querySearch.Parameters.AddWithValue("@player_idJumpW", player_idJumpW);
                    if (player_idJumpL is null)
                    {
                        player_idJumpL = 0;
                    }
                    querySearch.Parameters.AddWithValue("@player_idJumpL", player_idJumpL);
                    if (official_id is null)
                    {
                        official_id = 0;
                    }
                    querySearch.Parameters.AddWithValue("@official_id", official_id);
                    busDriver.SQLdb.Open();
                    querySearch.ExecuteScalar(); // Used for other than SELECT Queries
                    busDriver.SQLdb.Close();
                }
                
            }
        }




        public class Action
        {
            public int actionNumber { get; set; }
            public string clock { get; set; }
            public DateTime timeActual { get; set; }
            public int period { get; set; }
            public string periodType { get; set; }
            public int? teamId { get; set; }
            public string teamTricode { get; set; }
            public string actionType { get; set; }
            public string subType { get; set; }
            public string descriptor { get; set; }
            public List<string> qualifiers { get; set; }
            public int personId { get; set; }
            public double? x { get; set; }
            public double? y { get; set; }
            public int possession { get; set; }
            public int scoreHome { get; set; }
            public int scoreAway { get; set; }
            public DateTime edited { get; set; }
            public int orderNumber { get; set; }
            public bool isTargetScoreLastPeriod { get; set; }
            public int? xLegacy { get; set; }
            public int? yLegacy { get; set; }
            public int isFieldGoal { get; set; }
            public string side { get; set; }
            public string description { get; set; }
            public List<int> personIdsFilter { get; set; }
            public string jumpBallRecoveredName { get; set; }
            public int? jumpBallRecoverdPersonId { get; set; }
            public string playerName { get; set; }
            public string playerNameI { get; set; }
            public string jumpBallWonPlayerName { get; set; }
            public int? jumpBallWonPersonId { get; set; }
            public string jumpBallLostPlayerName { get; set; }
            public int? jumpBallLostPersonId { get; set; }
            public string area { get; set; }
            public string areaDetail { get; set; }
            public double? shotDistance { get; set; }
            public string shotResult { get; set; }
            public string blockPlayerName { get; set; }
            public int? blockPersonId { get; set; }
            public int? stealPersonId { get; set; }
            public int? shotActionNumber { get; set; }
            public int? reboundTotal { get; set; }
            public int? reboundDefensiveTotal { get; set; }
            public int? reboundOffensiveTotal { get; set; }
            public int? pointsTotal { get; set; }
            public string assistPlayerNameInitial { get; set; }
            public int? assistPersonId { get; set; }
            public int? assistTotal { get; set; }
            public int? officialId { get; set; }
            public int? foulPersonalTotal { get; set; }
            public int? foulTechnicalTotal { get; set; }
            public string foulDrawnPlayerName { get; set; }
            public int? foulDrawnPersonId { get; set; }
        }

        public class Game
        {
            public string gameId { get; set; }
            public List<Action> actions { get; set; }
        }

        public class Meta
        {
            public int version { get; set; }
            public int code { get; set; }
            public string request { get; set; }
            public string time { get; set; }
        }

        public class Root
        {
            public Meta meta { get; set; }
            public Game game { get; set; }
        }
    }

}