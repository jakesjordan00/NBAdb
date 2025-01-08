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
using static NBAdb.FirstTimeLoad;
#nullable enable

namespace NBAdb
{

    public class BoxScore
    {
        FirstTimeLoad first = new FirstTimeLoad();public static BusDriver busDriver = new BusDriver();
        public static void GetJSON(int id, string sender, int season)
        {
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            string boxLink = "https://cdn.nba.com/static/json/liveData/boxscore/boxscore_00" + id + ".json";
            WebRequest BoxScoreReq = WebRequest.Create(boxLink);
            WebResponse BoxScoreResp = BoxScoreReq.GetResponse();
            string json = client.DownloadString(boxLink);
            Root JSON = JsonConvert.DeserializeObject<Root>(json);
            BoxPost(JSON, season);
            if(sender == "Refresh")
            {
                GameCheck(JSON, id, JSON.game.homeTeam.score, JSON.game.awayTeam.score, 2024);
            }
        }


        public static void BoxPost(Root JSON, int id)
        {
            using (SqlCommand InsertData = new SqlCommand("teamBoxInsert"))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@id", id);
                InsertData.Parameters.AddWithValue("@game_id", JSON.game.gameId);
                InsertData.Parameters.AddWithValue("@team_id", JSON.game.homeTeam.teamId);
                InsertData.Parameters.AddWithValue("@atHome", 1);
                InsertData.Parameters.AddWithValue("@matchup_id", JSON.game.awayTeam.teamId);
                InsertData.Parameters.AddWithValue("@points", JSON.game.homeTeam.score);
                InsertData.Parameters.AddWithValue("@pointsAgainst", JSON.game.awayTeam.score);
                InsertData.Parameters.AddWithValue("@q1Points", JSON.game.homeTeam.periods[0].score);
                InsertData.Parameters.AddWithValue("@q1PointsAgainst", JSON.game.awayTeam.periods[0].score);
                InsertData.Parameters.AddWithValue("@q2Points", JSON.game.homeTeam.periods[1].score);
                InsertData.Parameters.AddWithValue("@q2PointsAgainst", JSON.game.awayTeam.periods[1].score);
                InsertData.Parameters.AddWithValue("@q3Points", JSON.game.homeTeam.periods[2].score);
                InsertData.Parameters.AddWithValue("@q3PointsAgainst", JSON.game.awayTeam.periods[2].score);
                InsertData.Parameters.AddWithValue("@q4Points", JSON.game.homeTeam.periods[3].score);
                InsertData.Parameters.AddWithValue("@q4PointsAgainst", JSON.game.awayTeam.periods[3].score);
                if (JSON.game.homeTeam.periods.Count() < 5)
                {
                    InsertData.Parameters.AddWithValue("@otPoints", SqlInt32.Null);
                    InsertData.Parameters.AddWithValue("@otPointsAgainst", SqlInt32.Null);
                }
                if (JSON.game.homeTeam.periods.Count() == 5)
                {
                    InsertData.Parameters.AddWithValue("@otPoints", JSON.game.homeTeam.periods[4].score);
                    InsertData.Parameters.AddWithValue("@otPointsAgainst", JSON.game.awayTeam.periods[4].score);
                }
                if (JSON.game.homeTeam.periods.Count() == 6)
                {
                    InsertData.Parameters.AddWithValue("@otPoints", JSON.game.homeTeam.periods[4].score + JSON.game.homeTeam.periods[5].score);
                    InsertData.Parameters.AddWithValue("@otPointsAgainst", JSON.game.awayTeam.periods[4].score + JSON.game.awayTeam.periods[5].score);
                }
                if (JSON.game.homeTeam.periods.Count() == 7)
                {
                    InsertData.Parameters.AddWithValue("@otPoints", JSON.game.homeTeam.periods[4].score + JSON.game.homeTeam.periods[5].score + JSON.game.homeTeam.periods[6].score);
                    InsertData.Parameters.AddWithValue("@otPointsAgainst", JSON.game.awayTeam.periods[4].score + JSON.game.awayTeam.periods[5].score + JSON.game.awayTeam.periods[6].score);
                }
                InsertData.Parameters.AddWithValue("@assists", JSON.game.homeTeam.statistics.assists);
                InsertData.Parameters.AddWithValue("@blocks", JSON.game.homeTeam.statistics.blocks);
                InsertData.Parameters.AddWithValue("@blocksReceived", JSON.game.homeTeam.statistics.blocksReceived);
                InsertData.Parameters.AddWithValue("@fieldGoalsAttempted", JSON.game.homeTeam.statistics.fieldGoalsAttempted);
                InsertData.Parameters.AddWithValue("@fieldGoalsMade", JSON.game.homeTeam.statistics.fieldGoalsMade);
                InsertData.Parameters.AddWithValue("@fieldGoalsPercentage", JSON.game.homeTeam.statistics.fieldGoalsPercentage);
                InsertData.Parameters.AddWithValue("@foulsOffensive", JSON.game.homeTeam.statistics.foulsOffensive);
                InsertData.Parameters.AddWithValue("@foulsDrawn", JSON.game.homeTeam.statistics.foulsDrawn);
                InsertData.Parameters.AddWithValue("@foulsPersonal", JSON.game.homeTeam.statistics.foulsPersonal);
                InsertData.Parameters.AddWithValue("@foulsTechnical", JSON.game.homeTeam.statistics.foulsTechnical);
                InsertData.Parameters.AddWithValue("@freeThrowsAttempted", JSON.game.homeTeam.statistics.freeThrowsAttempted);
                InsertData.Parameters.AddWithValue("@freeThrowsMade", JSON.game.homeTeam.statistics.freeThrowsMade);
                InsertData.Parameters.AddWithValue("@freeThrowsPercentage", JSON.game.homeTeam.statistics.freeThrowsPercentage);
                InsertData.Parameters.AddWithValue("@minus", JSON.game.homeTeam.statistics.minus);
                InsertData.Parameters.AddWithValue("@minutes", JSON.game.homeTeam.statistics.minutes);
                InsertData.Parameters.AddWithValue("@minutesCalculated", JSON.game.homeTeam.statistics.minutesCalculated);
                InsertData.Parameters.AddWithValue("@plus", JSON.game.homeTeam.statistics.plus);
                InsertData.Parameters.AddWithValue("@plusMinusPoints", JSON.game.homeTeam.statistics.plusMinusPoints);
                InsertData.Parameters.AddWithValue("@pointsFastBreak", JSON.game.homeTeam.statistics.pointsFastBreak);
                InsertData.Parameters.AddWithValue("@pointsInThePaint", JSON.game.homeTeam.statistics.pointsInThePaint);
                InsertData.Parameters.AddWithValue("@pointsSecondChance", JSON.game.homeTeam.statistics.pointsSecondChance);
                InsertData.Parameters.AddWithValue("@reboundsDefensive", JSON.game.homeTeam.statistics.reboundsDefensive);
                InsertData.Parameters.AddWithValue("@reboundsOffensive", JSON.game.homeTeam.statistics.reboundsOffensive);
                InsertData.Parameters.AddWithValue("@reboundsTotal", JSON.game.homeTeam.statistics.reboundsTeam);
                InsertData.Parameters.AddWithValue("@steals", JSON.game.homeTeam.statistics.steals);
                InsertData.Parameters.AddWithValue("@threePointersAttempted", JSON.game.homeTeam.statistics.threePointersAttempted);
                InsertData.Parameters.AddWithValue("@threePointersMade", JSON.game.homeTeam.statistics.threePointersMade);
                InsertData.Parameters.AddWithValue("@threePointersPercentage", JSON.game.homeTeam.statistics.threePointersPercentage);
                InsertData.Parameters.AddWithValue("@turnovers", JSON.game.homeTeam.statistics.turnovers);
                InsertData.Parameters.AddWithValue("@twoPointersAttempted", JSON.game.homeTeam.statistics.twoPointersAttempted);
                InsertData.Parameters.AddWithValue("@twoPointersMade", JSON.game.homeTeam.statistics.twoPointersMade);
                InsertData.Parameters.AddWithValue("@twoPointersPercentage", JSON.game.homeTeam.statistics.twoPointersPercentage);
                InsertData.Parameters.AddWithValue("@assistsTurnoverRatio", JSON.game.homeTeam.statistics.assistsTurnoverRatio);
                InsertData.Parameters.AddWithValue("@benchPoints", JSON.game.homeTeam.statistics.benchPoints);
                if (JSON.game.homeTeam.statistics.biggestLeadScore == null)
                {

                    InsertData.Parameters.AddWithValue("@biggestLead", SqlInt32.Null); ;
                    InsertData.Parameters.AddWithValue("@biggestLeadScore", SqlString.Null); ;
                }
                else
                {
                    InsertData.Parameters.AddWithValue("@biggestLead", JSON.game.homeTeam.statistics.biggestLead);
                    InsertData.Parameters.AddWithValue("@biggestLeadScore", JSON.game.homeTeam.statistics.biggestLeadScore);
                }
                InsertData.Parameters.AddWithValue("@biggestScoringRun", JSON.game.homeTeam.statistics.biggestScoringRun);
                if (JSON.game.homeTeam.statistics.biggestScoringRunScore == null)
                {
                    InsertData.Parameters.AddWithValue("@biggestScoringRunScore", SqlString.Null);
                }
                else
                {
                    InsertData.Parameters.AddWithValue("@biggestScoringRunScore", JSON.game.homeTeam.statistics.biggestScoringRunScore);
                }
                InsertData.Parameters.AddWithValue("@fastBreakPointsAttempted", JSON.game.homeTeam.statistics.fastBreakPointsAttempted);
                InsertData.Parameters.AddWithValue("@fastBreakPointsMade", JSON.game.homeTeam.statistics.fastBreakPointsMade);
                InsertData.Parameters.AddWithValue("@fastBreakPointsPercentage", JSON.game.homeTeam.statistics.fastBreakPointsPercentage);
                InsertData.Parameters.AddWithValue("@fieldGoalsEffectiveAdjusted", JSON.game.homeTeam.statistics.fieldGoalsEffectiveAdjusted);
                InsertData.Parameters.AddWithValue("@foulsTeam", JSON.game.homeTeam.statistics.foulsTeam);
                InsertData.Parameters.AddWithValue("@foulsTeamTechnical", JSON.game.homeTeam.statistics.foulsTeamTechnical);
                InsertData.Parameters.AddWithValue("@leadChanges", JSON.game.homeTeam.statistics.leadChanges);
                InsertData.Parameters.AddWithValue("@pointsFromTurnovers", JSON.game.homeTeam.statistics.pointsFromTurnovers);
                InsertData.Parameters.AddWithValue("@pointsInThePaintAttempted", JSON.game.homeTeam.statistics.pointsInThePaintAttempted);
                InsertData.Parameters.AddWithValue("@pointsInThePaintMade", JSON.game.homeTeam.statistics.pointsInThePaintMade);
                InsertData.Parameters.AddWithValue("@pointsInThePaintPercentage", JSON.game.homeTeam.statistics.pointsInThePaintPercentage);
                InsertData.Parameters.AddWithValue("@reboundsPersonal", JSON.game.homeTeam.statistics.reboundsPersonal);
                InsertData.Parameters.AddWithValue("@reboundsTeam", JSON.game.homeTeam.statistics.reboundsTeam);
                InsertData.Parameters.AddWithValue("@reboundsTeamDefensive", JSON.game.homeTeam.statistics.reboundsTeamDefensive);
                InsertData.Parameters.AddWithValue("@reboundsTeamOffensive", JSON.game.homeTeam.statistics.reboundsTeamOffensive);
                InsertData.Parameters.AddWithValue("@secondChancePointsAttempted", JSON.game.homeTeam.statistics.secondChancePointsAttempted);
                InsertData.Parameters.AddWithValue("@secondChancePointsMade", JSON.game.homeTeam.statistics.secondChancePointsMade);
                InsertData.Parameters.AddWithValue("@secondChancePointsPercentage", JSON.game.homeTeam.statistics.secondChancePointsPercentage);
                if (JSON.game.homeTeam.statistics.timeLeading == null)
                {
                    InsertData.Parameters.AddWithValue("@timeLeading", SqlString.Null);
                }
                else
                {
                    InsertData.Parameters.AddWithValue("@timeLeading", JSON.game.homeTeam.statistics.timeLeading);
                }
                InsertData.Parameters.AddWithValue("@timesTied", JSON.game.homeTeam.statistics.timesTied);
                InsertData.Parameters.AddWithValue("@trueShootingAttempts", JSON.game.homeTeam.statistics.trueShootingAttempts);
                InsertData.Parameters.AddWithValue("@trueShootingPercentage", JSON.game.homeTeam.statistics.trueShootingPercentage);
                InsertData.Parameters.AddWithValue("@turnoversTeam", JSON.game.homeTeam.statistics.turnoversTeam);
                InsertData.Parameters.AddWithValue("@turnoversTotal", JSON.game.homeTeam.statistics.turnoversTotal);
                busDriver.SQLdb.Open();
                InsertData.ExecuteScalar();
                busDriver.SQLdb.Close();
                PlayerBoxInsert(JSON, 0, id);
            }
            using (SqlCommand InsertDataAway = new SqlCommand("teamBoxInsert"))
            {
                InsertDataAway.Connection = busDriver.SQLdb;
                InsertDataAway.CommandType = CommandType.StoredProcedure;
                InsertDataAway.Parameters.AddWithValue("@id", id);
                InsertDataAway.Parameters.AddWithValue("@game_id", JSON.game.gameId);
                InsertDataAway.Parameters.AddWithValue("@team_id", JSON.game.awayTeam.teamId);
                InsertDataAway.Parameters.AddWithValue("@atHome", 0);
                InsertDataAway.Parameters.AddWithValue("@matchup_id", JSON.game.homeTeam.teamId);
                InsertDataAway.Parameters.AddWithValue("@points", JSON.game.awayTeam.score);
                InsertDataAway.Parameters.AddWithValue("@pointsAgainst", JSON.game.homeTeam.score);
                InsertDataAway.Parameters.AddWithValue("@q1Points", JSON.game.awayTeam.periods[0].score);
                InsertDataAway.Parameters.AddWithValue("@q1PointsAgainst", JSON.game.homeTeam.periods[0].score);
                InsertDataAway.Parameters.AddWithValue("@q2Points", JSON.game.awayTeam.periods[1].score);
                InsertDataAway.Parameters.AddWithValue("@q2PointsAgainst", JSON.game.homeTeam.periods[1].score);
                InsertDataAway.Parameters.AddWithValue("@q3Points", JSON.game.awayTeam.periods[2].score);
                InsertDataAway.Parameters.AddWithValue("@q3PointsAgainst", JSON.game.homeTeam.periods[2].score);
                InsertDataAway.Parameters.AddWithValue("@q4Points", JSON.game.awayTeam.periods[3].score);
                InsertDataAway.Parameters.AddWithValue("@q4PointsAgainst", JSON.game.homeTeam.periods[3].score);
                if (JSON.game.awayTeam.periods.Count() < 5)
                {
                    InsertDataAway.Parameters.AddWithValue("@otPoints", SqlInt32.Null);
                    InsertDataAway.Parameters.AddWithValue("@otPointsAgainst", SqlInt32.Null);
                }
                if (JSON.game.awayTeam.periods.Count() == 5)
                {
                    InsertDataAway.Parameters.AddWithValue("@otPoints", JSON.game.awayTeam.periods[4].score);
                    InsertDataAway.Parameters.AddWithValue("@otPointsAgainst", JSON.game.homeTeam.periods[4].score);
                }
                if (JSON.game.awayTeam.periods.Count() == 6)
                {
                    InsertDataAway.Parameters.AddWithValue("@otPoints", JSON.game.awayTeam.periods[4].score + JSON.game.awayTeam.periods[5].score);
                    InsertDataAway.Parameters.AddWithValue("@otPointsAgainst", JSON.game.homeTeam.periods[4].score + JSON.game.homeTeam.periods[5].score);
                }
                if (JSON.game.awayTeam.periods.Count() == 7)
                {
                    InsertDataAway.Parameters.AddWithValue("@otPoints", JSON.game.awayTeam.periods[4].score + JSON.game.awayTeam.periods[5].score + JSON.game.awayTeam.periods[6].score);
                    InsertDataAway.Parameters.AddWithValue("@otPointsAgainst", JSON.game.homeTeam.periods[4].score + JSON.game.homeTeam.periods[5].score + JSON.game.homeTeam.periods[6].score);
                }
                InsertDataAway.Parameters.AddWithValue("@assists", JSON.game.awayTeam.statistics.assists);
                InsertDataAway.Parameters.AddWithValue("@blocks", JSON.game.awayTeam.statistics.blocks);
                InsertDataAway.Parameters.AddWithValue("@blocksReceived", JSON.game.awayTeam.statistics.blocksReceived);
                InsertDataAway.Parameters.AddWithValue("@fieldGoalsAttempted", JSON.game.awayTeam.statistics.fieldGoalsAttempted);
                InsertDataAway.Parameters.AddWithValue("@fieldGoalsMade", JSON.game.awayTeam.statistics.fieldGoalsMade);
                InsertDataAway.Parameters.AddWithValue("@fieldGoalsPercentage", JSON.game.awayTeam.statistics.fieldGoalsPercentage);
                InsertDataAway.Parameters.AddWithValue("@foulsOffensive", JSON.game.awayTeam.statistics.foulsOffensive);
                InsertDataAway.Parameters.AddWithValue("@foulsDrawn", JSON.game.awayTeam.statistics.foulsDrawn);
                InsertDataAway.Parameters.AddWithValue("@foulsPersonal", JSON.game.awayTeam.statistics.foulsPersonal);
                InsertDataAway.Parameters.AddWithValue("@foulsTechnical", JSON.game.awayTeam.statistics.foulsTechnical);
                InsertDataAway.Parameters.AddWithValue("@freeThrowsAttempted", JSON.game.awayTeam.statistics.freeThrowsAttempted);
                InsertDataAway.Parameters.AddWithValue("@freeThrowsMade", JSON.game.awayTeam.statistics.freeThrowsMade);
                InsertDataAway.Parameters.AddWithValue("@freeThrowsPercentage", JSON.game.awayTeam.statistics.freeThrowsPercentage);
                InsertDataAway.Parameters.AddWithValue("@minus", JSON.game.awayTeam.statistics.minus);
                InsertDataAway.Parameters.AddWithValue("@minutes", JSON.game.awayTeam.statistics.minutes);
                InsertDataAway.Parameters.AddWithValue("@minutesCalculated", JSON.game.awayTeam.statistics.minutesCalculated);
                InsertDataAway.Parameters.AddWithValue("@plus", JSON.game.awayTeam.statistics.plus);
                InsertDataAway.Parameters.AddWithValue("@plusMinusPoints", JSON.game.awayTeam.statistics.plusMinusPoints);
                InsertDataAway.Parameters.AddWithValue("@pointsFastBreak", JSON.game.awayTeam.statistics.pointsFastBreak);
                InsertDataAway.Parameters.AddWithValue("@pointsInThePaint", JSON.game.awayTeam.statistics.pointsInThePaint);
                InsertDataAway.Parameters.AddWithValue("@pointsSecondChance", JSON.game.awayTeam.statistics.pointsSecondChance);
                InsertDataAway.Parameters.AddWithValue("@reboundsDefensive", JSON.game.awayTeam.statistics.reboundsDefensive);
                InsertDataAway.Parameters.AddWithValue("@reboundsOffensive", JSON.game.awayTeam.statistics.reboundsOffensive);
                InsertDataAway.Parameters.AddWithValue("@reboundsTotal", JSON.game.awayTeam.statistics.reboundsTeam);
                InsertDataAway.Parameters.AddWithValue("@steals", JSON.game.awayTeam.statistics.steals);
                InsertDataAway.Parameters.AddWithValue("@threePointersAttempted", JSON.game.awayTeam.statistics.threePointersAttempted);
                InsertDataAway.Parameters.AddWithValue("@threePointersMade", JSON.game.awayTeam.statistics.threePointersMade);
                InsertDataAway.Parameters.AddWithValue("@threePointersPercentage", JSON.game.awayTeam.statistics.threePointersPercentage);
                InsertDataAway.Parameters.AddWithValue("@turnovers", JSON.game.awayTeam.statistics.turnovers);
                InsertDataAway.Parameters.AddWithValue("@twoPointersAttempted", JSON.game.awayTeam.statistics.twoPointersAttempted);
                InsertDataAway.Parameters.AddWithValue("@twoPointersMade", JSON.game.awayTeam.statistics.twoPointersMade);
                InsertDataAway.Parameters.AddWithValue("@twoPointersPercentage", JSON.game.awayTeam.statistics.twoPointersPercentage);
                InsertDataAway.Parameters.AddWithValue("@assistsTurnoverRatio", JSON.game.awayTeam.statistics.assistsTurnoverRatio);
                InsertDataAway.Parameters.AddWithValue("@benchPoints", JSON.game.awayTeam.statistics.benchPoints);
                if (JSON.game.awayTeam.statistics.biggestLeadScore == null)
                {

                    InsertDataAway.Parameters.AddWithValue("@biggestLead", SqlInt32.Null); ;
                    InsertDataAway.Parameters.AddWithValue("@biggestLeadScore", SqlString.Null); ;
                }
                else
                {
                    InsertDataAway.Parameters.AddWithValue("@biggestLead", JSON.game.awayTeam.statistics.biggestLead);
                    InsertDataAway.Parameters.AddWithValue("@biggestLeadScore", JSON.game.awayTeam.statistics.biggestLeadScore);
                }
                if (JSON.game.awayTeam.statistics.biggestScoringRunScore == null)
                {
                    InsertDataAway.Parameters.AddWithValue("@biggestScoringRunScore", SqlString.Null);
                }
                else
                {
                    InsertDataAway.Parameters.AddWithValue("@biggestScoringRunScore", JSON.game.awayTeam.statistics.biggestScoringRunScore);
                }
                InsertDataAway.Parameters.AddWithValue("@biggestScoringRun", JSON.game.awayTeam.statistics.biggestScoringRun);
                InsertDataAway.Parameters.AddWithValue("@fastBreakPointsAttempted", JSON.game.awayTeam.statistics.fastBreakPointsAttempted);
                InsertDataAway.Parameters.AddWithValue("@fastBreakPointsMade", JSON.game.awayTeam.statistics.fastBreakPointsMade);
                InsertDataAway.Parameters.AddWithValue("@fastBreakPointsPercentage", JSON.game.awayTeam.statistics.fastBreakPointsPercentage);
                InsertDataAway.Parameters.AddWithValue("@fieldGoalsEffectiveAdjusted", JSON.game.awayTeam.statistics.fieldGoalsEffectiveAdjusted);
                InsertDataAway.Parameters.AddWithValue("@foulsTeam", JSON.game.awayTeam.statistics.foulsTeam);
                InsertDataAway.Parameters.AddWithValue("@foulsTeamTechnical", JSON.game.awayTeam.statistics.foulsTeamTechnical);
                InsertDataAway.Parameters.AddWithValue("@pointsFromTurnovers", JSON.game.awayTeam.statistics.pointsFromTurnovers);
                InsertDataAway.Parameters.AddWithValue("@pointsInThePaintAttempted", JSON.game.awayTeam.statistics.pointsInThePaintAttempted);
                InsertDataAway.Parameters.AddWithValue("@pointsInThePaintMade", JSON.game.awayTeam.statistics.pointsInThePaintMade);
                InsertDataAway.Parameters.AddWithValue("@pointsInThePaintPercentage", JSON.game.awayTeam.statistics.pointsInThePaintPercentage);
                InsertDataAway.Parameters.AddWithValue("@reboundsPersonal", JSON.game.awayTeam.statistics.reboundsPersonal);
                InsertDataAway.Parameters.AddWithValue("@reboundsTeam", JSON.game.awayTeam.statistics.reboundsTeam);
                InsertDataAway.Parameters.AddWithValue("@reboundsTeamDefensive", JSON.game.awayTeam.statistics.reboundsTeamDefensive);
                InsertDataAway.Parameters.AddWithValue("@reboundsTeamOffensive", JSON.game.awayTeam.statistics.reboundsTeamOffensive);
                InsertDataAway.Parameters.AddWithValue("@secondChancePointsAttempted", JSON.game.awayTeam.statistics.secondChancePointsAttempted);
                InsertDataAway.Parameters.AddWithValue("@secondChancePointsMade", JSON.game.awayTeam.statistics.secondChancePointsMade);
                InsertDataAway.Parameters.AddWithValue("@secondChancePointsPercentage", JSON.game.awayTeam.statistics.secondChancePointsPercentage);
                if (JSON.game.awayTeam.statistics.timeLeading == null)
                {
                    InsertDataAway.Parameters.AddWithValue("@timeLeading", SqlString.Null);
                }
                else
                {
                    InsertDataAway.Parameters.AddWithValue("@timeLeading", JSON.game.awayTeam.statistics.timeLeading);
                }
                InsertDataAway.Parameters.AddWithValue("@leadChanges", JSON.game.awayTeam.statistics.leadChanges);
                InsertDataAway.Parameters.AddWithValue("@timesTied", JSON.game.awayTeam.statistics.timesTied);
                InsertDataAway.Parameters.AddWithValue("@trueShootingAttempts", JSON.game.awayTeam.statistics.trueShootingAttempts);
                InsertDataAway.Parameters.AddWithValue("@trueShootingPercentage", JSON.game.awayTeam.statistics.trueShootingPercentage);
                InsertDataAway.Parameters.AddWithValue("@turnoversTeam", JSON.game.awayTeam.statistics.turnoversTeam);
                InsertDataAway.Parameters.AddWithValue("@turnoversTotal", JSON.game.awayTeam.statistics.turnoversTotal);
                busDriver.SQLdb.Open();
                InsertDataAway.ExecuteScalar();
                busDriver.SQLdb.Close();
                PlayerBoxInsertAway(JSON, 0, id);
            }
        }
        public static void PlayerBoxInsertAway(Root JSON, int updateCheck, int id)
        {
            int players = JSON.game.awayTeam.players.Count;
            for (int i = 0; i < players; i++)
            {
                using (SqlCommand InsertDataAway = new SqlCommand("playerBoxInsert"))
                {
                    InsertDataAway.Connection = busDriver.SQLdb;
                    InsertDataAway.CommandType = CommandType.StoredProcedure;
                    InsertDataAway.Parameters.AddWithValue("@id", id);
                    InsertDataAway.Parameters.AddWithValue("@game_id", JSON.game.gameId);
                    InsertDataAway.Parameters.AddWithValue("@team_id", JSON.game.awayTeam.teamId);
                    InsertDataAway.Parameters.AddWithValue("@player_id", JSON.game.awayTeam.players[i].personId);
                    InsertDataAway.Parameters.AddWithValue("@status", JSON.game.awayTeam.players[i].status);
                    InsertDataAway.Parameters.AddWithValue("@starter", JSON.game.awayTeam.players[i].starter);
                    if (JSON.game.awayTeam.players[i].position == null)
                    {
                        InsertDataAway.Parameters.AddWithValue("@position", SqlString.Null);
                    }
                    else
                    {
                        InsertDataAway.Parameters.AddWithValue("@position", JSON.game.awayTeam.players[i].position);
                    }
                    InsertDataAway.Parameters.AddWithValue("@points", JSON.game.awayTeam.players[i].statistics.points);
                    InsertDataAway.Parameters.AddWithValue("@assists", JSON.game.awayTeam.players[i].statistics.assists);
                    InsertDataAway.Parameters.AddWithValue("@blocks", JSON.game.awayTeam.players[i].statistics.blocks);
                    InsertDataAway.Parameters.AddWithValue("@blocksReceived", JSON.game.awayTeam.players[i].statistics.blocksReceived);
                    InsertDataAway.Parameters.AddWithValue("@fieldGoalsAttempted", JSON.game.awayTeam.players[i].statistics.fieldGoalsAttempted);
                    InsertDataAway.Parameters.AddWithValue("@fieldGoalsMade", JSON.game.awayTeam.players[i].statistics.fieldGoalsMade);
                    InsertDataAway.Parameters.AddWithValue("@fieldGoalsPercentage", JSON.game.awayTeam.players[i].statistics.fieldGoalsPercentage);
                    InsertDataAway.Parameters.AddWithValue("@foulsOffensive", JSON.game.awayTeam.players[i].statistics.foulsOffensive);
                    InsertDataAway.Parameters.AddWithValue("@foulsDrawn", JSON.game.awayTeam.players[i].statistics.foulsDrawn);
                    InsertDataAway.Parameters.AddWithValue("@foulsPersonal", JSON.game.awayTeam.players[i].statistics.foulsPersonal);
                    InsertDataAway.Parameters.AddWithValue("@foulsTechnical", JSON.game.awayTeam.players[i].statistics.foulsTechnical);
                    InsertDataAway.Parameters.AddWithValue("@freeThrowsAttempted", JSON.game.awayTeam.players[i].statistics.freeThrowsAttempted);
                    InsertDataAway.Parameters.AddWithValue("@freeThrowsMade", JSON.game.awayTeam.players[i].statistics.freeThrowsMade);
                    InsertDataAway.Parameters.AddWithValue("@freeThrowsPercentage", JSON.game.awayTeam.players[i].statistics.freeThrowsPercentage);
                    InsertDataAway.Parameters.AddWithValue("@minus", JSON.game.awayTeam.players[i].statistics.minus);
                    InsertDataAway.Parameters.AddWithValue("@minutes", JSON.game.awayTeam.players[i].statistics.minutes);
                    InsertDataAway.Parameters.AddWithValue("@minutesCalculated", JSON.game.awayTeam.players[i].statistics.minutesCalculated);
                    InsertDataAway.Parameters.AddWithValue("@plus", JSON.game.awayTeam.players[i].statistics.plus);
                    InsertDataAway.Parameters.AddWithValue("@plusMinusPoints", JSON.game.awayTeam.players[i].statistics.plusMinusPoints);
                    InsertDataAway.Parameters.AddWithValue("@pointsFastBreak", JSON.game.awayTeam.players[i].statistics.pointsFastBreak);
                    InsertDataAway.Parameters.AddWithValue("@pointsInThePaint", JSON.game.awayTeam.players[i].statistics.pointsInThePaint);
                    InsertDataAway.Parameters.AddWithValue("@pointsSecondChance", JSON.game.awayTeam.players[i].statistics.pointsSecondChance);
                    InsertDataAway.Parameters.AddWithValue("@reboundsDefensive", JSON.game.awayTeam.players[i].statistics.reboundsDefensive);
                    InsertDataAway.Parameters.AddWithValue("@reboundsOffensive", JSON.game.awayTeam.players[i].statistics.reboundsOffensive);
                    InsertDataAway.Parameters.AddWithValue("@reboundsTotal", JSON.game.awayTeam.players[i].statistics.reboundsTotal);
                    InsertDataAway.Parameters.AddWithValue("@steals", JSON.game.awayTeam.players[i].statistics.steals);
                    InsertDataAway.Parameters.AddWithValue("@threePointersAttempted", JSON.game.awayTeam.players[i].statistics.threePointersAttempted);
                    InsertDataAway.Parameters.AddWithValue("@threePointersMade", JSON.game.awayTeam.players[i].statistics.threePointersMade);
                    InsertDataAway.Parameters.AddWithValue("@threePointersPercentage", JSON.game.awayTeam.players[i].statistics.threePointersPercentage);
                    InsertDataAway.Parameters.AddWithValue("@turnovers", JSON.game.awayTeam.players[i].statistics.turnovers);
                    InsertDataAway.Parameters.AddWithValue("@twoPointersAttempted", JSON.game.awayTeam.players[i].statistics.twoPointersAttempted);
                    InsertDataAway.Parameters.AddWithValue("@twoPointersMade", JSON.game.awayTeam.players[i].statistics.twoPointersMade);
                    InsertDataAway.Parameters.AddWithValue("@twoPointersPercentage", JSON.game.awayTeam.players[i].statistics.twoPointersPercentage);
                    if (JSON.game.awayTeam.players[i].notPlayingReason == null)
                    {
                        InsertDataAway.Parameters.AddWithValue("@statusReason", SqlString.Null);
                    }
                    else
                    {
                        InsertDataAway.Parameters.AddWithValue("@statusReason", JSON.game.awayTeam.players[i].notPlayingReason);
                    }
                    if (JSON.game.awayTeam.players[i].notPlayingDescription == null)
                    {
                        InsertDataAway.Parameters.AddWithValue("@statusDescription", SqlString.Null);
                    }
                    else
                    {
                        InsertDataAway.Parameters.AddWithValue("@statusDescription", JSON.game.awayTeam.players[i].notPlayingDescription);
                    }
                    busDriver.SQLdb.Open();
                    InsertDataAway.ExecuteScalar();
                    busDriver.SQLdb.Close();
                }                
            }
        }

        public static void PlayerBoxInsert(Root JSON, int updateCheck, int id)
        {
            int players = JSON.game.homeTeam.players.Count;
            for (int i = 0; i < players; i++)
            {
                using (SqlCommand InsertData = new SqlCommand("playerBoxInsert"))
                {
                    InsertData.Connection = busDriver.SQLdb;
                    InsertData.CommandType = CommandType.StoredProcedure;
                    InsertData.Parameters.AddWithValue("@id", id);
                    InsertData.Parameters.AddWithValue("@game_id", JSON.game.gameId);
                    InsertData.Parameters.AddWithValue("@team_id", JSON.game.homeTeam.teamId);
                    InsertData.Parameters.AddWithValue("@player_id", JSON.game.homeTeam.players[i].personId);
                    InsertData.Parameters.AddWithValue("@status", JSON.game.homeTeam.players[i].status);
                    InsertData.Parameters.AddWithValue("@starter", JSON.game.homeTeam.players[i].starter);
                    if (JSON.game.homeTeam.players[i].position == null)
                    {
                        InsertData.Parameters.AddWithValue("@position", SqlString.Null);
                    }
                    else
                    {
                        InsertData.Parameters.AddWithValue("@position", JSON.game.homeTeam.players[i].position);
                    }
                    InsertData.Parameters.AddWithValue("@points", JSON.game.homeTeam.players[i].statistics.points);
                    InsertData.Parameters.AddWithValue("@assists", JSON.game.homeTeam.players[i].statistics.assists);
                    InsertData.Parameters.AddWithValue("@blocks", JSON.game.homeTeam.players[i].statistics.blocks);
                    InsertData.Parameters.AddWithValue("@blocksReceived", JSON.game.homeTeam.players[i].statistics.blocksReceived);
                    InsertData.Parameters.AddWithValue("@fieldGoalsAttempted", JSON.game.homeTeam.players[i].statistics.fieldGoalsAttempted);
                    InsertData.Parameters.AddWithValue("@fieldGoalsMade", JSON.game.homeTeam.players[i].statistics.fieldGoalsMade);
                    InsertData.Parameters.AddWithValue("@fieldGoalsPercentage", JSON.game.homeTeam.players[i].statistics.fieldGoalsPercentage);
                    InsertData.Parameters.AddWithValue("@foulsOffensive", JSON.game.homeTeam.players[i].statistics.foulsOffensive);
                    InsertData.Parameters.AddWithValue("@foulsDrawn", JSON.game.homeTeam.players[i].statistics.foulsDrawn);
                    InsertData.Parameters.AddWithValue("@foulsPersonal", JSON.game.homeTeam.players[i].statistics.foulsPersonal);
                    InsertData.Parameters.AddWithValue("@foulsTechnical", JSON.game.homeTeam.players[i].statistics.foulsTechnical);
                    InsertData.Parameters.AddWithValue("@freeThrowsAttempted", JSON.game.homeTeam.players[i].statistics.freeThrowsAttempted);
                    InsertData.Parameters.AddWithValue("@freeThrowsMade", JSON.game.homeTeam.players[i].statistics.freeThrowsMade);
                    InsertData.Parameters.AddWithValue("@freeThrowsPercentage", JSON.game.homeTeam.players[i].statistics.freeThrowsPercentage);
                    InsertData.Parameters.AddWithValue("@minus", JSON.game.homeTeam.players[i].statistics.minus);
                    InsertData.Parameters.AddWithValue("@minutes", JSON.game.homeTeam.players[i].statistics.minutes);
                    InsertData.Parameters.AddWithValue("@minutesCalculated", JSON.game.homeTeam.players[i].statistics.minutesCalculated);
                    InsertData.Parameters.AddWithValue("@plus", JSON.game.homeTeam.players[i].statistics.plus);
                    InsertData.Parameters.AddWithValue("@plusMinusPoints", JSON.game.homeTeam.players[i].statistics.plusMinusPoints);
                    InsertData.Parameters.AddWithValue("@pointsFastBreak", JSON.game.homeTeam.players[i].statistics.pointsFastBreak);
                    InsertData.Parameters.AddWithValue("@pointsInThePaint", JSON.game.homeTeam.players[i].statistics.pointsInThePaint);
                    InsertData.Parameters.AddWithValue("@pointsSecondChance", JSON.game.homeTeam.players[i].statistics.pointsSecondChance);
                    InsertData.Parameters.AddWithValue("@reboundsDefensive", JSON.game.homeTeam.players[i].statistics.reboundsDefensive);
                    InsertData.Parameters.AddWithValue("@reboundsOffensive", JSON.game.homeTeam.players[i].statistics.reboundsOffensive);
                    InsertData.Parameters.AddWithValue("@reboundsTotal", JSON.game.homeTeam.players[i].statistics.reboundsTotal);
                    InsertData.Parameters.AddWithValue("@steals", JSON.game.homeTeam.players[i].statistics.steals);
                    InsertData.Parameters.AddWithValue("@threePointersAttempted", JSON.game.homeTeam.players[i].statistics.threePointersAttempted);
                    InsertData.Parameters.AddWithValue("@threePointersMade", JSON.game.homeTeam.players[i].statistics.threePointersMade);
                    InsertData.Parameters.AddWithValue("@threePointersPercentage", JSON.game.homeTeam.players[i].statistics.threePointersPercentage);
                    InsertData.Parameters.AddWithValue("@turnovers", JSON.game.homeTeam.players[i].statistics.turnovers);
                    InsertData.Parameters.AddWithValue("@twoPointersAttempted", JSON.game.homeTeam.players[i].statistics.twoPointersAttempted);
                    InsertData.Parameters.AddWithValue("@twoPointersMade", JSON.game.homeTeam.players[i].statistics.twoPointersMade);
                    InsertData.Parameters.AddWithValue("@twoPointersPercentage", JSON.game.homeTeam.players[i].statistics.twoPointersPercentage);
                    if (JSON.game.homeTeam.players[i].notPlayingReason == null)
                    {
                        InsertData.Parameters.AddWithValue("@statusReason", SqlString.Null);
                    }
                    else
                    {
                        InsertData.Parameters.AddWithValue("@statusReason", JSON.game.homeTeam.players[i].notPlayingReason);
                    }
                    if (JSON.game.homeTeam.players[i].notPlayingDescription == null)
                    {
                        InsertData.Parameters.AddWithValue("@statusDescription", SqlString.Null);
                    }
                    else
                    {
                        InsertData.Parameters.AddWithValue("@statusDescription", JSON.game.homeTeam.players[i].notPlayingDescription);
                    }
                    busDriver.SQLdb.Open();
                    InsertData.ExecuteScalar();
                    busDriver.SQLdb.Close();
                }                
            }
        }

        public static void GameCheck(Root JSON, int game_id, int hScore, int aScore, int id)
        {
            using (SqlCommand GameSearch = new SqlCommand("gameCheck"))
            {
                GameSearch.CommandType = CommandType.StoredProcedure;
                GameSearch.Parameters.AddWithValue("@game_id", game_id);
                GameSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sGameSearch = new SqlDataAdapter())
                {
                    GameSearch.Connection = busDriver.SQLdb;
                    sGameSearch.SelectCommand = GameSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = GameSearch.ExecuteReader();
                    reader.Read();
                    if (!reader.HasRows)
                    {
                        busDriver.SQLdb.Close();
                        GamePost(JSON, game_id, hScore, aScore, id);
                    }
                    else
                    {
                        if (reader.GetInt32(7) != hScore && reader.GetInt32(7) != aScore)
                        {
                            busDriver.SQLdb.Close();
                            GameUpdate(JSON, game_id, hScore, aScore, id);
                        }
                        else
                        {
                            busDriver.SQLdb.Close();
                        }
                    }
                }
            }
        }
        public static void GamePost(Root JSON, int game_id, int hScore, int aScore, int id)
        {
            using (SqlCommand InsertData = new SqlCommand("gameInsert"))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@game_id", game_id);
                InsertData.Parameters.AddWithValue("@id", id);
                InsertData.Parameters.AddWithValue("@date", JSON.game.gameTimeLocal.Date);
                InsertData.Parameters.AddWithValue("@team_idH", JSON.game.homeTeam.teamId);
                InsertData.Parameters.AddWithValue("@team_idA", JSON.game.awayTeam.teamId);
                if (JSON.game.homeTeam.score >= JSON.game.awayTeam.score)
                {
                    InsertData.Parameters.AddWithValue("@team_idW", JSON.game.homeTeam.teamId);
                    InsertData.Parameters.AddWithValue("@wScore", JSON.game.homeTeam.score);
                    InsertData.Parameters.AddWithValue("@team_idL", JSON.game.awayTeam.teamId);
                    InsertData.Parameters.AddWithValue("@lScore", JSON.game.awayTeam.score);
                }
                else
                {
                    InsertData.Parameters.AddWithValue("@team_idW", JSON.game.awayTeam.teamId);
                    InsertData.Parameters.AddWithValue("@wScore", JSON.game.awayTeam.score);
                    InsertData.Parameters.AddWithValue("@team_idL", JSON.game.homeTeam.teamId);
                    InsertData.Parameters.AddWithValue("@lScore", JSON.game.homeTeam.score);
                }
                InsertData.Parameters.AddWithValue("@arena_id", JSON.game.arena.arenaId);
                InsertData.Parameters.AddWithValue("@sellout", JSON.game.sellout);
                busDriver.SQLdb.Open();
                InsertData.ExecuteScalar();
                busDriver.SQLdb.Close();
            }
        }
        public static void GameUpdate(Root JSON, int game_id, int hScore, int aScore, int id)
        {
            using (SqlCommand Update = new SqlCommand("gameUpdate"))
            {
                Update.Connection = busDriver.SQLdb;
                Update.CommandType = CommandType.StoredProcedure;
                Update.Parameters.AddWithValue("@game_id", game_id);
                Update.Parameters.AddWithValue("@id", id);
                if (JSON.game.homeTeam.score >= JSON.game.awayTeam.score)
                {
                    Update.Parameters.AddWithValue("@team_idW", JSON.game.homeTeam.teamId);
                    Update.Parameters.AddWithValue("@wScore", JSON.game.homeTeam.score);
                    Update.Parameters.AddWithValue("@team_idL", JSON.game.awayTeam.teamId);
                    Update.Parameters.AddWithValue("@lScore", JSON.game.awayTeam.score);
                }
                else
                {
                    Update.Parameters.AddWithValue("@team_idW", JSON.game.awayTeam.teamId);
                    Update.Parameters.AddWithValue("@wScore", JSON.game.awayTeam.score);
                    Update.Parameters.AddWithValue("@team_idL", JSON.game.homeTeam.teamId);
                    Update.Parameters.AddWithValue("@lScore", JSON.game.homeTeam.score);
                }
                busDriver.SQLdb.Open();
                Update.ExecuteScalar();
                busDriver.SQLdb.Close();
            }
        }





        public class Arena
        {
            public int arenaId { get; set; }
            public string arenaName { get; set; }
            public string arenaCity { get; set; }
            public string arenaState { get; set; }
            public string arenaCountry { get; set; }
            public string arenaTimezone { get; set; }
        }

        public class AwayTeam
        {
            public int teamId { get; set; }
            public string teamName { get; set; }
            public string teamCity { get; set; }
            public string teamTricode { get; set; }
            public int score { get; set; }
            public string inBonus { get; set; }
            public int timeoutsRemaining { get; set; }
            public List<Period> periods { get; set; }
            public List<Player> players { get; set; }
            public Statistics statistics { get; set; }
        }

        public class Game
        {
            public string gameId { get; set; }
            public DateTime gameTimeLocal { get; set; }
            public DateTime gameTimeUTC { get; set; }
            public DateTime gameTimeHome { get; set; }
            public DateTime gameTimeAway { get; set; }
            public DateTime gameEt { get; set; }
            public int duration { get; set; }
            public string gameCode { get; set; }
            public string gameStatusText { get; set; }
            public int gameStatus { get; set; }
            public int regulationPeriods { get; set; }
            public int period { get; set; }
            public string gameClock { get; set; }
            public int attendance { get; set; }
            public string sellout { get; set; }
            public Arena arena { get; set; }
            public List<Official> officials { get; set; }
            public HomeTeam homeTeam { get; set; }
            public AwayTeam awayTeam { get; set; }
        }

        public class HomeTeam
        {
            public int teamId { get; set; }
            public string teamName { get; set; }
            public string teamCity { get; set; }
            public string teamTricode { get; set; }
            public int score { get; set; }
            public string inBonus { get; set; }
            public int timeoutsRemaining { get; set; }
            public List<Period> periods { get; set; }
            public List<Player> players { get; set; }
            public Statistics statistics { get; set; }
        }

        public class Meta
        {
            public int version { get; set; }
            public int code { get; set; }
            public string request { get; set; }
            public string time { get; set; }
        }

        public class Official
        {
            public int personId { get; set; }
            public string name { get; set; }
            public string nameI { get; set; }
            public string firstName { get; set; }
            public string familyName { get; set; }
            public string jerseyNum { get; set; }
            public string assignment { get; set; }
        }

        public class Period
        {
            public int period { get; set; }
            public string periodType { get; set; }
            public int score { get; set; }
        }

        public class Player
        {
            public string status { get; set; }
            public int order { get; set; }
            public int personId { get; set; }
            public string jerseyNum { get; set; }
            public string position { get; set; }
            public string starter { get; set; }
            public string oncourt { get; set; }
            public string played { get; set; }
            public Statistics statistics { get; set; }
            public string name { get; set; }
            public string nameI { get; set; }
            public string firstName { get; set; }
            public string familyName { get; set; }
            public string notPlayingReason { get; set; }
            public string notPlayingDescription { get; set; }
        }

        public class Root
        {
            public Meta meta { get; set; }
            public Game game { get; set; }
        }

        public class Statistics
        {
            public int assists { get; set; }
            public int blocks { get; set; }
            public int blocksReceived { get; set; }
            public int fieldGoalsAttempted { get; set; }
            public int fieldGoalsMade { get; set; }
            public double fieldGoalsPercentage { get; set; }
            public int foulsOffensive { get; set; }
            public int foulsDrawn { get; set; }
            public int foulsPersonal { get; set; }
            public int foulsTechnical { get; set; }
            public int freeThrowsAttempted { get; set; }
            public int freeThrowsMade { get; set; }
            public double freeThrowsPercentage { get; set; }
            public double minus { get; set; }
            public string minutes { get; set; }
            public string minutesCalculated { get; set; }
            public double plus { get; set; }
            public double plusMinusPoints { get; set; }
            public int points { get; set; }
            public int pointsFastBreak { get; set; }
            public int pointsInThePaint { get; set; }
            public int pointsSecondChance { get; set; }
            public int reboundsDefensive { get; set; }
            public int reboundsOffensive { get; set; }
            public int reboundsTotal { get; set; }
            public int steals { get; set; }
            public int threePointersAttempted { get; set; }
            public int threePointersMade { get; set; }
            public double threePointersPercentage { get; set; }
            public int turnovers { get; set; }
            public int twoPointersAttempted { get; set; }
            public int twoPointersMade { get; set; }
            public double twoPointersPercentage { get; set; }
            public double assistsTurnoverRatio { get; set; }
            public int benchPoints { get; set; }
            public int biggestLead { get; set; }
            public string biggestLeadScore { get; set; }
            public int biggestScoringRun { get; set; }
            public string biggestScoringRunScore { get; set; }
            public int fastBreakPointsAttempted { get; set; }
            public int fastBreakPointsMade { get; set; }
            public double fastBreakPointsPercentage { get; set; }
            public double fieldGoalsEffectiveAdjusted { get; set; }
            public int foulsTeam { get; set; }
            public int foulsTeamTechnical { get; set; }
            public int leadChanges { get; set; }
            public int pointsAgainst { get; set; }
            public int pointsFromTurnovers { get; set; }
            public int pointsInThePaintAttempted { get; set; }
            public int pointsInThePaintMade { get; set; }
            public double pointsInThePaintPercentage { get; set; }
            public int reboundsPersonal { get; set; }
            public int reboundsTeam { get; set; }
            public int reboundsTeamDefensive { get; set; }
            public int reboundsTeamOffensive { get; set; }
            public int secondChancePointsAttempted { get; set; }
            public int secondChancePointsMade { get; set; }
            public double secondChancePointsPercentage { get; set; }
            public string timeLeading { get; set; }
            public int timesTied { get; set; }
            public double trueShootingAttempts { get; set; }
            public double trueShootingPercentage { get; set; }
            public int turnoversTeam { get; set; }
            public int turnoversTotal { get; set; }
        }
    }
}