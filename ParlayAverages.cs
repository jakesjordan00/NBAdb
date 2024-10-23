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
    public class ParlayAverages
    {
        public  ParlayAssistant parlayAssistant = new ParlayAssistant();
        public static BusDriver busDriver = new BusDriver();
        public static int win = 3;

        public void GetAverages(bool Wins, bool Loss, string player, string team, Label Name, Label Team, Label Points, Label Assists, Label Rebounds, Label Threes, Label Blocks, Label Steals, Label Minutes, HtmlGenericControl pd, HtmlGenericControl ad, HtmlGenericControl rd, HtmlGenericControl fg3md, HtmlGenericControl bd, HtmlGenericControl sd, int season)
        {
            //int    season   = 0;
            int    games    = 0;
            int    minutes  = 0;
            double points   = 0;
            double assists  = 0;
            double rebounds = 0;
            double blocks   = 0;
            double steals   = 0;
            double fgm = 0;
            double fga = 0;
            double fgp = 0;
            double fg3m = 0;
            double fg3a = 0;
            double fg3p = 0;

            using (SqlCommand PlayerSearch = new SqlCommand("ParlayAverages"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@Player", player);
                PlayerSearch.Parameters.AddWithValue("@Team", team);
                PlayerSearch.Parameters.AddWithValue("@Season", season);
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    while (reader.Read())
                    {
                        if(Wins == false && Loss == false)
                        {
                            if(reader["Win/Loss/Total"].ToString() == "Total")
                            {
                                season   = (int)reader[0];
                                games    = (int)reader[5];
                                minutes  = (int)reader[6];
                                points   = (double)reader[7];
                                assists  = (double)reader[8];
                                rebounds = (double)reader[9];
                                blocks   = (double)reader[10];
                                steals   = (double)reader[11];
                                fgm      = (double)reader[12];
                                fga      = (double)reader[13];
                                fgp      = (double)reader[14];
                                fg3m     = (double)reader[15];
                                fg3a     = (double)reader[16];
                                fg3p     = (double)reader[17];
                                Minutes.Text = games + "GP, " + minutes + "min/game";
                                win = 3;
                            }
                        }
                        else if (reader["Win/Loss/Total"].ToString() == "Win" && Wins == true && Loss == false)
                        {
                            season = (int)reader[0];
                            games = (int)reader[5];
                            minutes = (int)reader[6];
                            points = (double)reader[7];
                            assists = (double)reader[8];
                            rebounds = (double)reader[9];
                            blocks = (double)reader[10];
                            steals = (double)reader[11];
                            fgm = (double)reader[12];
                            fga = (double)reader[13];
                            fgp = (double)reader[14];
                            fg3m = (double)reader[15];
                            fg3a = (double)reader[16];
                            fg3p = (double)reader[17];
                            Minutes.Text = games + " Wins, " + minutes + "min/game";
                            win = 1;
                            //GetDeltas(player, team, 1, pd, ad, rd, fg3md, bd, sd);
                        }
                        else if (reader["Win/Loss/Total"].ToString() == "Loss" && Wins == false && Loss == true)
                        {
                            season = (int)reader[0];
                            games = (int)reader[5];
                            minutes = (int)reader[6];
                            points = (double)reader[7];
                            assists = (double)reader[8];
                            rebounds = (double)reader[9];
                            blocks = (double)reader[10];
                            steals = (double)reader[11];
                            fgm = (double)reader[12];
                            fga = (double)reader[13];
                            fgp = (double)reader[14];
                            fg3m = (double)reader[15];
                            fg3a = (double)reader[16];
                            fg3p = (double)reader[17];
                            Minutes.Text = games + " Losses, " + minutes + "min/game";
                            win = 0;
                            //GetDeltas(player, team, 0, pd, ad, rd, fg3md, bd, sd);
                        }
                        
                    }
                    busDriver.SQLdb.Close();
                }
            }
            GetDeltas(player, team, win, pd, ad, rd, fg3md, bd, sd, points, assists, rebounds, fg3m, blocks, steals, season);
            //parlayAssistant.AddPlayerLabel(season, team, player, games, minutes, points, assists, rebounds, blocks, steals);
            Name.Text = player;
            Team.Text = season + " " + team;
            Points.Text = points.ToString();
            Assists.Text = assists.ToString();
            Rebounds.Text = rebounds.ToString();
            Threes.Text = fg3m.ToString();
            Blocks.Text = blocks.ToString();
            Steals.Text = steals.ToString();
        }

        public void GetDeltas(string player, string team, int win, HtmlGenericControl pd, HtmlGenericControl ad, HtmlGenericControl rd, HtmlGenericControl fg3md, HtmlGenericControl bd, HtmlGenericControl sd, double p, double a, double r, double three, double b, double s, int season)
        {
            double pts = 0;
            double ast = 0;
            double reb = 0;
            double fg3 = 0;
            double blk = 0;
            double stl = 0;
            using (SqlCommand ParlayAverageFinder = new SqlCommand("ParlayAverageFinder"))
            {
                ParlayAverageFinder.CommandType = CommandType.StoredProcedure;
                ParlayAverageFinder.Parameters.AddWithValue("@Player", player);
                ParlayAverageFinder.Parameters.AddWithValue("@Team", team);
                ParlayAverageFinder.Parameters.AddWithValue("@Season", season);
                using (SqlDataAdapter sParlayAverageFinder = new SqlDataAdapter())
                {
                    ParlayAverageFinder.Connection = busDriver.SQLdb;
                    sParlayAverageFinder.SelectCommand = ParlayAverageFinder;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader1 = ParlayAverageFinder.ExecuteReader();
                    while (reader1.Read())
                    {
                        if(win == 1)
                        {
                            //PointsDelta = 19
                            pd.Attributes["title"] =    reader1[19].ToString();
                            ad.Attributes["title"] =    reader1[20].ToString();
                            rd.Attributes["title"] =    reader1[21].ToString();
                            fg3md.Attributes["title"] = reader1[27].ToString();
                            bd.Attributes["title"] =    reader1[22].ToString();
                            sd.Attributes["title"] =    reader1[23].ToString();
                            pts = Convert.ToDouble(reader1[19]);
                            ast = Convert.ToDouble(reader1[20]);
                            reb = Convert.ToDouble(reader1[21]);
                            fg3 = Convert.ToDouble(reader1[27]);
                            blk = Convert.ToDouble(reader1[22]);
                            stl = Convert.ToDouble(reader1[23]);
                        }
                        else if(win == 0)
                        {
                            pd.Attributes["title"] = reader1[19] != DBNull.Value ? (-1 * Convert.ToDouble(reader1[19])).ToString() : "N/A";
                            ad.Attributes["title"] = reader1[20] != DBNull.Value ? (-1 * Convert.ToDouble(reader1[20])).ToString() : "N/A";
                            rd.Attributes["title"] = reader1[21] != DBNull.Value ? (-1 * Convert.ToDouble(reader1[21])).ToString() : "N/A";
                            fg3md.Attributes["title"] = reader1[27] != DBNull.Value ? (-1 * Convert.ToDouble(reader1[27])).ToString() : "N/A";
                            bd.Attributes["title"] = reader1[22] != DBNull.Value ? (-1 * Convert.ToDouble(reader1[22])).ToString() : "N/A";
                            sd.Attributes["title"] = reader1[23] != DBNull.Value ? (-1 * Convert.ToDouble(reader1[23])).ToString() : "N/A";
                            pts = Convert.ToDouble(reader1[19]);
                            ast = Convert.ToDouble(reader1[20]);
                            reb = Convert.ToDouble(reader1[21]);
                            fg3 = Convert.ToDouble(reader1[27]);
                            blk = Convert.ToDouble(reader1[22]);
                            stl = Convert.ToDouble(reader1[23]);
                        }
                        else
                        {
                            pd.Attributes["title"] = "";
                            ad.Attributes["title"] = "";
                            rd.Attributes["title"] = "";
                            fg3md.Attributes["title"] = "";
                            bd.Attributes["title"] = "";
                            sd.Attributes["title"] = "";
                        }
                    }
                    busDriver.SQLdb.Close();
                }
            }
            if(win == 3)
            {
                using (SqlCommand PlayerTrendFinder = new SqlCommand("PlayerTrendFinder"))
                {
                    PlayerTrendFinder.CommandType = CommandType.StoredProcedure;
                    PlayerTrendFinder.Parameters.AddWithValue("@Player", player);
                    using (SqlDataAdapter sPlayerTrendFinder = new SqlDataAdapter())
                    {
                        PlayerTrendFinder.Connection = busDriver.SQLdb;
                        sPlayerTrendFinder.SelectCommand = PlayerTrendFinder;
                        busDriver.SQLdb.Open();
                        SqlDataReader reader = PlayerTrendFinder.ExecuteReader();
                        while (reader.Read())
                        {
                            pd.Attributes["title"]    = (Convert.ToDouble(reader[2]) - p).ToString();
                            ad.Attributes["title"]    = (Convert.ToDouble(reader[3]) - a).ToString();
                            rd.Attributes["title"]    = (Convert.ToDouble(reader[4]) - r).ToString();
                            fg3md.Attributes["title"] = (Convert.ToDouble(reader[10]) - three).ToString();
                            bd.Attributes["title"]    = (Convert.ToDouble(reader[5]) - b).ToString();
                            sd.Attributes["title"]    = (Convert.ToDouble(reader[6]) - s).ToString();
                        }
                        busDriver.SQLdb.Close();
                    }
                }
            }
            List<HtmlGenericControl> controls = new List<HtmlGenericControl>
            {
             pd, ad, rd, fg3md, bd, sd
            };
            foreach (HtmlGenericControl control in controls)
            {
                // Check the title attribute value
                double titleValue;
                if (double.TryParse(control.Attributes["title"], out titleValue))
                {
                    // Conditionally set the style attribute based on the title value
                    if (titleValue > 0)
                    {
                        control.Attributes["style"] = "font-size:Large; text-decoration-color: green; color: green";
                    }
                    else if (titleValue < 0)
                    {
                        control.Attributes["style"] = "font-size:Large; text-decoration-color:red; color: red";
                    }
                    else
                    {
                        control.Attributes["style"] = "font-size:Large; text-decoration-color:";
                    }
                }
            }

        }
    }
}