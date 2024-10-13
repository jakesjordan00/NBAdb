using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using static NBAdb.FirstTimeLoad;
using static NBAdb.PlayByPlay;
using Microsoft.Ajax.Utilities;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace NBAdb
{
    public class ParlayAveragesExtended
    {
        public ParlayAssistant parlayAssistant = new ParlayAssistant();
        public static BusDriver busDriver = new BusDriver();
        public static int win = 3;
        public static int season = 2023;
        public static int    games = 0;

        public static int    p1minutes = 0;
        public static double p1points = 0;
        public static double p1assists = 0;
        public static double p1rebounds = 0;
        public static double p1blocks = 0;
        public static double p1steals = 0;
        public static double p1fgm = 0;
        public static double p1fga = 0;
        public static double p1fgp = 0;
        public static double p1fg3m = 0;
        public static double p1fg3a = 0;
        public static double p1fg3p = 0;

        public static int    p2minutes = 0;
        public static double p2points = 0;
        public static double p2assists = 0;
        public static double p2rebounds = 0;
        public static double p2blocks = 0;
        public static double p2steals = 0;
        public static double p2fgm = 0;
        public static double p2fga = 0;
        public static double p2fgp = 0;
        public static double p2fg3m = 0;
        public static double p2fg3a = 0;
        public static double p2fg3p = 0;

        public static int    p3minutes = 0;
        public static double p3points = 0;
        public static double p3assists = 0;
        public static double p3rebounds = 0;
        public static double p3blocks = 0;
        public static double p3steals = 0;
        public static double p3fgm = 0;
        public static double p3fga = 0;
        public static double p3fgp = 0;
        public static double p3fg3m = 0;
        public static double p3fg3a = 0;
        public static double p3fg3p = 0;

        //@Player varchar(255), @Player2 varchar(255), @Player3 varchar(255), @Injured varchar(255), @Team varchar(255), @season int

        public void GetProcedure(string p, string p2, string p3, string i, string team, int season, bool win, bool loss)
        {
            string procedure = "";
            int WinLossTotal = 2;
            if (!string.IsNullOrEmpty(p) && !string.IsNullOrEmpty(i) && string.IsNullOrEmpty(p2) && string.IsNullOrEmpty(p3))
            {
                procedure = "ParlayAveragesInjured";
            }
            else if (!string.IsNullOrEmpty(p) && !string.IsNullOrEmpty(p2) && string.IsNullOrEmpty(p3) && string.IsNullOrEmpty(i))
            {
                procedure = "ParlayAveragesTwo";
            }
            else if (!string.IsNullOrEmpty(p) && !string.IsNullOrEmpty(p2) && !string.IsNullOrEmpty(i) && string.IsNullOrEmpty(p3))
            {
                procedure = "ParlayAveragesTwoInjured";
            }
            else if (!string.IsNullOrEmpty(p) && !string.IsNullOrEmpty(p2) && !string.IsNullOrEmpty(p3) && string.IsNullOrEmpty(i))
            {
                procedure = "ParlayAveragesThree";
            }
            else if (!string.IsNullOrEmpty(p) && !string.IsNullOrEmpty(p2) && !string.IsNullOrEmpty(i) && !string.IsNullOrEmpty(p3))
            {
                procedure = "ParlayAveragesThreeInjured";
            }



            team = team.Remove(0, 6);
            if(win == true)
            {
                WinLossTotal = 1;
            }
            else if (loss == true)
            {
                WinLossTotal = 0;
            }
            else if(win != true && loss != true)
            {
                WinLossTotal = 2;
            }
            GetAverages(procedure, p, p2, p3, i, team, season, WinLossTotal);
        }

        public void GetAverages(string procedure, string p, string p2, string p3, string i, string team, int season, int winLossTotal)
        {
            p1minutes = 0;      p2minutes = 0;      p3minutes = 0;
            p1points = 0;       p2points = 0;       p3points = 0;
            p1assists = 0;      p2assists = 0;      p3assists = 0;
            p1rebounds = 0;     p2rebounds = 0;     p3rebounds = 0;
            p1blocks = 0;       p2blocks = 0;       p3blocks = 0;
            p1steals = 0;       p2steals = 0;       p3steals = 0;
            p1fgm = 0;          p2fgm = 0;          p3fgm = 0;
            p1fga = 0;          p2fga = 0;          p3fga = 0;
            p1fgp = 0;          p2fgp = 0;          p3fgp = 0;
            p1fg3m = 0;         p2fg3m = 0;         p3fg3m = 0;
            p1fg3a = 0;         p2fg3a = 0;         p3fg3a = 0;
            p1fg3p = 0;         p2fg3p = 0;         p3fg3p = 0;     games = 0;
            using (SqlCommand ParlayAverageFinder = new SqlCommand(procedure))
            {
                ParlayAverageFinder.CommandType = CommandType.StoredProcedure;
                ParlayAverageFinder.Parameters.AddWithValue("@Player", p);
                ParlayAverageFinder.Parameters.AddWithValue("@Player2", p2);
                ParlayAverageFinder.Parameters.AddWithValue("@Player3", p3);
                ParlayAverageFinder.Parameters.AddWithValue("@Injured", i);
                ParlayAverageFinder.Parameters.AddWithValue("@Team", team);
                ParlayAverageFinder.Parameters.AddWithValue("@season", season);
                using (SqlDataAdapter sParlayAverageFinder = new SqlDataAdapter())
                {
                    ParlayAverageFinder.Connection = busDriver.SQLdb;
                    sParlayAverageFinder.SelectCommand = ParlayAverageFinder;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = ParlayAverageFinder.ExecuteReader();
                    while (reader.Read())
                    {
                        games = (int)reader[5];
                        p1points = (double)reader[6];
                        p1assists = (double)reader[7];
                        p1rebounds = (double)reader[8];
                        p1fg3m = (double)reader[14];
                        p1blocks = (double)reader[9];
                        p1steals = (double)reader[10];
                        p1minutes = (int)reader[17];
                        if(procedure.Contains("Two") || procedure.Contains("Three"))
                        {
                            p2points = (double)reader[20];
                            p2assists = (double)reader[21];
                            p2rebounds = (double)reader[22];
                            p2blocks = (double)reader[23];
                            p2steals = (double)reader[24];
                            p2fg3m = (double)reader[28];
                            p2minutes = (int)reader[31];
                        }
                        if (procedure.Contains("Three"))
                        {
                            p3points = (double)reader[34];
                            p3assists = (double)reader[35];
                            p3rebounds = (double)reader[36];
                            p3blocks = (double)reader[37];
                            p3steals = (double)reader[38];
                            p3fg3m = (double)reader[42];
                            p3minutes = (int)reader[45];
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }
        }


        public void PopulateUI(Label aName, Label aTeam, Label Name, Label Team, Label Points, Label Assists, Label Rebounds, Label Threes, Label Blocks, Label Steals, Label Minutes, int pointer)
        {
            Name.Text = aName.Text;
            Team.Text = aTeam.Text;
            if(pointer == 1)
            {
                Minutes.Text = games + "GP, " + p1minutes + "min/game";
                Points.Text = p1points.ToString();
                Assists.Text = p1assists.ToString();
                Rebounds.Text = p1rebounds.ToString();
                Threes.Text = p1fg3m.ToString();
                Blocks.Text = p1blocks.ToString();
                Steals.Text = p1steals.ToString();
            }
            else if(pointer == 2)
            {
                Minutes.Text = games + "GP, " + p2minutes + "min/game";
                Points.Text = p2points.ToString();
                Assists.Text = p2assists.ToString();
                Rebounds.Text = p2rebounds.ToString();
                Threes.Text = p2fg3m.ToString();
                Blocks.Text = p2blocks.ToString();
                Steals.Text = p2steals.ToString();
            }
            else if (pointer == 3)
            {
                Minutes.Text = games + "GP, " + p3minutes + "min/game";
                Points.Text = p3points.ToString();
                Assists.Text = p3assists.ToString();
                Rebounds.Text = p3rebounds.ToString();
                Threes.Text = p3fg3m.ToString();
                Blocks.Text = p3blocks.ToString();
                Steals.Text = p3steals.ToString();
            }
        }
    }
}