using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;
using Microsoft.Ajax.Utilities;

namespace NBAdb
{
    public partial class ParlayAssistant : System.Web.UI.Page
    {
        public static int p1Changes = 0;
        public static int pIChanges = 0;
        public static int p2Changes = 0;
        public static int p3Changes = 0;
        public static List<string> Player1Picks = new List<string>();
        public static List<string> PlayerIPicks = new List<string>();
        public static List<string> Player2Picks = new List<string>();
        public static List<string> Player3Picks = new List<string>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }
        protected void BindGrid()
        {
            SqlConnection sqlConnect = new SqlConnection("Server=localhost;Database=nbaDB;User Id=test;Password=test123;");
            using (sqlConnect)
            {
                using (SqlCommand querySearch = new SqlCommand("ParlayTeams"))
                {
                    querySearch.CommandType = CommandType.StoredProcedure;
                    querySearch.Parameters.AddWithValue("@Season", ddSeason.SelectedValue);
                    using (SqlDataAdapter sDataSearch = new SqlDataAdapter())
                    {
                        querySearch.Connection = sqlConnect;
                        sDataSearch.SelectCommand = querySearch;
                        using (DataSet dataT = new DataSet())
                        {
                            try { sDataSearch.Fill(dataT); } catch (SqlException) { lblError.Text = "No games meet the requested criteria"; }
                            ddTeams.DataTextField = dataT.Tables[0].Columns["Team"].ToString();
                            ddTeams.DataSource = dataT;
                            ddTeams.DataBind();
                            ListItem emptyItem = new ListItem("Team", "");
                            ddTeams.Items.Insert(0, emptyItem);
                        }
                    }
                }
            }
        }
        protected void ddTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlRoster.ClearSelection();
            ddlRoster.Items.Clear();
            ddlRoster2.ClearSelection();
            ddlRoster2.Items.Clear();
            ddlRoster3.ClearSelection();
            ddlRoster3.Items.Clear();
            ddlInjured.ClearSelection();
            ddlInjured.Items.Clear();
            a1StatsSection.Visible = false;
            a2StatsSection.Visible = false;
            a3StatsSection.Visible = false;
            PopulateRoster();            
        }
        protected void PopulateRoster()
        {
            string team = ddTeams.SelectedValue;
            SqlConnection sqlConnect = new SqlConnection("Server=localhost;Database=nbaDB;User Id=test;Password=test123;");
            using (sqlConnect)
            {
                using (SqlCommand querySearch = new SqlCommand("ParlayRoster"))
                {
                    querySearch.Connection = sqlConnect;

                    querySearch.CommandType = CommandType.StoredProcedure;
                    querySearch.Parameters.AddWithValue("@team", team);
                    querySearch.Parameters.AddWithValue("@season", ddSeason.SelectedValue);
                    sqlConnect.Open();
                    using (SqlDataReader sdr = querySearch.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Text = sdr["Player"].ToString();
                            ddlRoster.Items.Add(item);

                        }
                    }
                    ListItem emptyItem = new ListItem("Player", "");
                    ddlRoster.Items.Insert(0, emptyItem);
                    sqlConnect.Close();
                }
                using (SqlCommand querySearch = new SqlCommand("ParlayRoster"))
                {
                    querySearch.Connection = sqlConnect;

                    querySearch.CommandType = CommandType.StoredProcedure;
                    querySearch.Parameters.AddWithValue("@team", team);
                    querySearch.Parameters.AddWithValue("@season", ddSeason.SelectedValue);
                    sqlConnect.Open();
                    using (SqlDataReader sdr = querySearch.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Text = sdr["Player"].ToString();
                            ddlRoster2.Items.Add(item);
                        }
                    }
                    ListItem emptyItem = new ListItem("Player", "");
                    ddlRoster2.Items.Insert(0, emptyItem);
                    sqlConnect.Close();
                }
                using (SqlCommand querySearch = new SqlCommand("ParlayRoster"))
                {
                    querySearch.Connection = sqlConnect;

                    querySearch.CommandType = CommandType.StoredProcedure;
                    querySearch.Parameters.AddWithValue("@team", team);
                    querySearch.Parameters.AddWithValue("@season", ddSeason.SelectedValue);
                    sqlConnect.Open();
                    using (SqlDataReader sdr = querySearch.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Text = sdr["Player"].ToString();
                            ddlInjured.Items.Add(item);
                        }
                    }
                    ListItem emptyItem = new ListItem("Injured Player", "");
                    ddlInjured.Items.Insert(0, emptyItem);
                    sqlConnect.Close();
                }
                using (SqlCommand querySearch = new SqlCommand("ParlayRoster"))
                {
                    querySearch.Connection = sqlConnect;

                    querySearch.CommandType = CommandType.StoredProcedure;
                    querySearch.Parameters.AddWithValue("@team", team);
                    querySearch.Parameters.AddWithValue("@season", ddSeason.SelectedValue);
                    sqlConnect.Open();
                    using (SqlDataReader sdr = querySearch.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Text = sdr["Player"].ToString();
                            ddlRoster3.Items.Add(item);
                        }
                        ListItem emptyItem = new ListItem("Player", "");
                        ddlRoster3.Items.Insert(0, emptyItem);
                        sqlConnect.Close();
                    }

                }
            }
        }


        protected void ddlRoster_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            Player1Picks.Add(ddlRoster.SelectedValue);
            a1StatsSection.Visible = true;
            ParlayAverages parlayAverages = new ParlayAverages();
            parlayAverages.GetAverages(aWinsChk.Checked, aLossChk.Checked, ddlRoster.SelectedValue, ddTeams.SelectedValue.Remove(0, 6), a1Name, a1Team, a1Points, a1Assists, a1Rebounds, a1Threes, a1Blocks, a1Steals, a1Minutes, a1pd, a1ad, a1rd, a13d, a1bd, a1sd, Int32.Parse(ddSeason.SelectedValue));
            if (p1Changes != 0 && Player1Picks[p1Changes - 1].ToString() != "")
            {
                ListItem item = new ListItem();
                item.Text = Player1Picks[p1Changes - 1].ToString();
                if (!ddlInjured.Items.Contains(item))
                {
                    ddlInjured.Items.Add(item);
                }
                if (!ddlRoster2.Items.Contains(item))
                {
                    ddlRoster2.Items.Add(item);
                }
                if (!ddlRoster3.Items.Contains(item))
                {
                    ddlRoster3.Items.Add(item);
                }
            }


            ParlayAveragesExtended parlayAveragesExtended = new ParlayAveragesExtended();
            if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlInjured.SelectedValue.IsNullOrWhiteSpace() && ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = false;
                dy3StatsSection.Visible = false;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = false;
                a3StatsSection.Visible = false;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
            }
            else if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = true;
                dy3StatsSection.Visible = false;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = true;
                a3StatsSection.Visible = false;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
                parlayAveragesExtended.PopulateUI(a2Name, a2Team, dyName2, dyTeam2, dyPts2, dyAst2, dyReb2, dy32, dyBlk2, dyStl2, dyMinutes2, 2);
            }
            else if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = true;
                dy3StatsSection.Visible = true;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = true;
                a3StatsSection.Visible = true;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
                parlayAveragesExtended.PopulateUI(a2Name, a2Team, dyName2, dyTeam2, dyPts2, dyAst2, dyReb2, dy32, dyBlk2, dyStl2, dyMinutes2, 2);
                parlayAveragesExtended.PopulateUI(a3Name, a3Team, dyName3, dyTeam3, dyPts3, dyAst3, dyReb3, dy33, dyBlk3, dyStl3, dyMinutes3, 3);
            }
            else
            {
                dyStatsSection.Visible = false;
                dy1StatsSection.Visible = false;
                dy2StatsSection.Visible = false;
                dy3StatsSection.Visible = false;
                a2StatsSection.Visible = false;
                a3StatsSection.Visible = false;
            }







            if (ddlInjured.Items.Contains(ddlRoster.SelectedItem))
            {
                ddlInjured.Items.Remove(ddlRoster.SelectedValue);
            }
            //else
            //{
            //    ddlInjured.Items.Add(ddlRoster.SelectedValue);
            //}
            if (ddlRoster2.Items.Contains(ddlRoster.SelectedItem))
            {
                ddlRoster2.Items.Remove(ddlRoster.SelectedValue);
            }
            //else
            //{
            //    ddlRoster2.Items.Add(ddlRoster.SelectedValue);
            //}

            if (ddlRoster3.Items.Contains(ddlRoster.SelectedItem))
            {
                ddlRoster3.Items.Remove(ddlRoster.SelectedValue);
            }
            //else
            //{
            //    ddlRoster3.Items.Add(ddlRoster.SelectedValue);
            //}
            p1Changes += 1;
        }
        public void ddlInjured_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlayerIPicks.Add(ddlInjured.SelectedValue);
            ParlayAveragesExtended parlayAveragesExtended = new ParlayAveragesExtended();
            if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlInjured.SelectedValue.IsNullOrWhiteSpace() && ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = false;
                dy3StatsSection.Visible = false;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = false;
                a3StatsSection.Visible = false;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
            }
            else if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = true;
                dy3StatsSection.Visible = false;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = true;
                a3StatsSection.Visible = false;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
                parlayAveragesExtended.PopulateUI(a2Name, a2Team, dyName2, dyTeam2, dyPts2, dyAst2, dyReb2, dy32, dyBlk2, dyStl2, dyMinutes2, 2);
            }
            else if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = true;
                dy3StatsSection.Visible = true;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = true;
                a3StatsSection.Visible = true;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
                parlayAveragesExtended.PopulateUI(a2Name, a2Team, dyName2, dyTeam2, dyPts2, dyAst2, dyReb2, dy32, dyBlk2, dyStl2, dyMinutes2, 2);
                parlayAveragesExtended.PopulateUI(a3Name, a3Team, dyName3, dyTeam3, dyPts3, dyAst3, dyReb3, dy33, dyBlk3, dyStl3, dyMinutes3, 3);
            }
            else
            {
                dyStatsSection.Visible = false;
                dy1StatsSection.Visible = false;
                dy2StatsSection.Visible = false;
                dy3StatsSection.Visible = false;
                a2StatsSection.Visible = false;
                a3StatsSection.Visible = false;
            }

            if (pIChanges != 0 && PlayerIPicks[pIChanges - 1].ToString() != "")
            {
                ListItem item = new ListItem();
                item.Text = PlayerIPicks[pIChanges - 1].ToString();
                if (!ddlRoster.Items.Contains(item))
                {
                    ddlRoster.Items.Add(item);
                }
                if (!ddlRoster2.Items.Contains(item))
                {
                    ddlRoster2.Items.Add(item);
                }
                if (!ddlRoster3.Items.Contains(item))
                {
                    ddlRoster3.Items.Add(item);
                }
            }
            if (ddlRoster2.Items.Contains(ddlInjured.SelectedItem))
            {
                ddlRoster2.Items.Remove(ddlInjured.SelectedValue);
            }
            //else
            //{
            //    ddlRoster2.Items.Add(ddlInjured.SelectedValue);
            //}
            if (ddlRoster3.Items.Contains(ddlInjured.SelectedItem))
            {
                ddlRoster3.Items.Remove(ddlInjured.SelectedValue);
            }
            //else
            //{
            //    ddlRoster3.Items.Add(ddlInjured.SelectedValue);
            //}
            if (ddlRoster.Items.Contains(ddlInjured.SelectedItem))
            {
                ddlRoster.Items.Remove(ddlInjured.SelectedValue);
            }
            //else
            //{
            //    ddlRoster.Items.Add(ddlInjured.SelectedValue);
            //}
            pIChanges += 1;
        }

        protected void ddlRoster2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label1.Visible = true;
            if (ddlRoster2.SelectedValue != "" && ddlRoster3.SelectedValue != "Player")
            {
                if (chkP.Checked == true)
                {
                    txt2P.Visible = true;
                }
                if (chkA.Checked == true)
                {
                    txt2A.Visible = true;
                }
                if (chkR.Checked == true)
                {
                    txt2R.Visible = true;
                }
                if (chk3.Checked == true)
                {
                    txt23.Visible = true;
                }
                if (chkB.Checked == true)
                {
                    txt2B.Visible = true;
                }
                if (chkS.Checked == true)
                {
                    txt2S.Visible = true;
                }
            }
            if (ddlRoster2.SelectedValue == "" || ddlRoster2.SelectedValue == "Player")
            {
                txt2P.Visible = false; txt2A.Visible = false; txt2R.Visible = false; txt23.Visible = false; txt2B.Visible = false; txt2S.Visible = false;
            }
            Player2Picks.Add(ddlRoster2.SelectedValue);
            a2StatsSection.Visible = true;
            ParlayAverages parlayAverages = new ParlayAverages();
            parlayAverages.GetAverages(aWinsChk.Checked, aLossChk.Checked, ddlRoster2.SelectedValue, ddTeams.SelectedValue.Remove(0, 6), a2Name, a2Team, a2Points, a2Assists, a2Rebounds, a2Threes, a2Blocks, a2Steals, a2Minutes, a2pd, a2ad, a2rd, a23d, a2bd, a2sd, Int32.Parse(ddSeason.SelectedValue));
            ParlayAveragesExtended parlayAveragesExtended = new ParlayAveragesExtended();
            if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlInjured.SelectedValue.IsNullOrWhiteSpace() && ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = false;
                dy3StatsSection.Visible = false;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = false;
                a3StatsSection.Visible = false;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
            }
            else if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = true;
                dy3StatsSection.Visible = false;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = true;
                a3StatsSection.Visible = false;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
                parlayAveragesExtended.PopulateUI(a2Name, a2Team, dyName2, dyTeam2, dyPts2, dyAst2, dyReb2, dy32, dyBlk2, dyStl2, dyMinutes2, 2);
            }
            else if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = true;
                dy3StatsSection.Visible = true;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = true;
                a3StatsSection.Visible = true;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
                parlayAveragesExtended.PopulateUI(a2Name, a2Team, dyName2, dyTeam2, dyPts2, dyAst2, dyReb2, dy32, dyBlk2, dyStl2, dyMinutes2, 2);
                parlayAveragesExtended.PopulateUI(a3Name, a3Team, dyName3, dyTeam3, dyPts3, dyAst3, dyReb3, dy33, dyBlk3, dyStl3, dyMinutes3, 3);
            }
            else
            {
                dyStatsSection.Visible = false;
                dy1StatsSection.Visible = false;
                dy2StatsSection.Visible = false;
                dy3StatsSection.Visible = false;
                a2StatsSection.Visible = false;
                a3StatsSection.Visible = false;
            }

            if (p2Changes != 0 && Player2Picks[p2Changes - 1].ToString() != "")
            {
                ListItem item = new ListItem();
                item.Text = Player2Picks[p2Changes - 1].ToString();
                if (!ddlRoster.Items.Contains(item))
                {
                    ddlRoster.Items.Add(item);
                }
                if (!ddlInjured.Items.Contains(item))
                {
                    ddlInjured.Items.Add(item);
                }
                if (!ddlRoster3.Items.Contains(item))
                {
                    ddlRoster3.Items.Add(item);
                }
            }
            if (ddlRoster3.Items.Contains(ddlRoster2.SelectedItem))
            {
                ddlRoster3.Items.Remove(ddlRoster2.SelectedValue);
            }
            //else
            //{
            //    ddlRoster3.Items.Add(ddlRoster2.SelectedValue);
            //}
            if (ddlRoster.Items.Contains(ddlRoster2.SelectedItem))
            {
                ddlRoster.Items.Remove(ddlRoster2.SelectedValue);
            }
            //else
            //{
            //    ddlRoster.Items.Add(ddlRoster2.SelectedValue);
            //}
            if (ddlInjured.Items.Contains(ddlRoster2.SelectedItem))
            {
                ddlInjured.Items.Remove(ddlRoster2.SelectedValue);
            }
            //else
            //{
            //    ddlInjured.Items.Add(ddlRoster2.SelectedValue);
            //}
            p2Changes += 1;
        }

        protected void ddlRoster3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label2.Visible = true;
            if (ddlRoster3.SelectedValue != "" && ddlRoster3.SelectedValue != "Player")
            {
                if (chkP.Checked == true)
                {
                    txt3P.Visible = true;
                }
                if (chkA.Checked == true)
                {
                    txt3A.Visible = true;
                }
                if (chkR.Checked == true)
                {
                    txt3R.Visible = true;
                }
                if (chk3.Checked == true)
                {
                    txt33.Visible = true;
                }
                if (chkB.Checked == true)
                {
                    txt3B.Visible = true;
                }
                if (chkS.Checked == true)
                {
                    txt3S.Visible = true;
                }
            }
            if (ddlRoster3.SelectedValue == "" || ddlRoster3.SelectedValue == "Player")
            {
                txt3P.Visible = false; txt3A.Visible = false; txt3R.Visible = false; txt33.Visible = false; txt3B.Visible = false; txt3S.Visible = false;
            }
            Player3Picks.Add(ddlRoster3.SelectedValue);
            a3StatsSection.Visible = true;
            ParlayAverages parlayAverages = new ParlayAverages();
            parlayAverages.GetAverages(aWinsChk.Checked, aLossChk.Checked, ddlRoster3.SelectedValue, ddTeams.SelectedValue.Remove(0, 6), a3Name, a3Team, a3Points, a3Assists, a3Rebounds, a3Threes, a3Blocks, a3Steals, a3Minutes, a3pd, a3ad, a3rd, a33d, a3bd, a3sd, Int32.Parse(ddSeason.SelectedValue));
            ParlayAveragesExtended parlayAveragesExtended = new ParlayAveragesExtended();
            if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlInjured.SelectedValue.IsNullOrWhiteSpace() && ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = false;
                dy3StatsSection.Visible = false;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = false;
                a3StatsSection.Visible = false;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
            }
            else if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = true;
                dy3StatsSection.Visible = false;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = true;
                a3StatsSection.Visible = false;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
                parlayAveragesExtended.PopulateUI(a2Name, a2Team, dyName2, dyTeam2, dyPts2, dyAst2, dyReb2, dy32, dyBlk2, dyStl2, dyMinutes2, 2);
            }
            else if (!ddlRoster.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster2.SelectedValue.IsNullOrWhiteSpace() && !ddlRoster3.SelectedValue.IsNullOrWhiteSpace())
            {
                parlayAveragesExtended.GetProcedure(ddlRoster.SelectedValue, ddlRoster2.SelectedValue, ddlRoster3.SelectedValue, ddlInjured.SelectedValue, ddTeams.SelectedValue, Int32.Parse(ddSeason.SelectedValue), aWinsChk.Checked, aLossChk.Checked);
                dyStatsSection.Visible = true;
                dy1StatsSection.Visible = true;
                dy2StatsSection.Visible = true;
                dy3StatsSection.Visible = true;
                a1StatsSection.Visible = true;
                a2StatsSection.Visible = true;
                a3StatsSection.Visible = true;
                parlayAveragesExtended.PopulateUI(a1Name, a1Team, dyName1, dyTeam1, dyPts1, dyAst1, dyReb1, dy31, dyBlk1, dyStl1, dyMinutes1, 1);
                parlayAveragesExtended.PopulateUI(a2Name, a2Team, dyName2, dyTeam2, dyPts2, dyAst2, dyReb2, dy32, dyBlk2, dyStl2, dyMinutes2, 2);
                parlayAveragesExtended.PopulateUI(a3Name, a3Team, dyName3, dyTeam3, dyPts3, dyAst3, dyReb3, dy33, dyBlk3, dyStl3, dyMinutes3, 3);
            }
            else
            {
                dyStatsSection.Visible = false;
                dy1StatsSection.Visible = false;
                dy2StatsSection.Visible = false;
                dy3StatsSection.Visible = false;
                a2StatsSection.Visible = false;
                a3StatsSection.Visible = false;
            }

            if (p3Changes != 0 && Player3Picks[p3Changes - 1].ToString() != "")
            {
                ListItem item = new ListItem();
                item.Text = Player3Picks[p3Changes - 1].ToString();
                if (!ddlRoster.Items.Contains(item))
                {
                    ddlRoster.Items.Add(item);
                }
                if (!ddlInjured.Items.Contains(item))
                {
                    ddlInjured.Items.Add(item);
                }
                if (!ddlRoster2.Items.Contains(item))
                {
                    ddlRoster2.Items.Add(item);
                }
            }
            if (ddlRoster2.Items.Contains(ddlRoster3.SelectedItem))
            {
                ddlRoster2.Items.Remove(ddlRoster3.SelectedValue);
            }
            //else
            //{
            //    ddlRoster2.Items.Add(ddlRoster2.SelectedValue);
            //}
            if (ddlRoster.Items.Contains(ddlRoster3.SelectedItem))
            {
                ddlRoster.Items.Remove(ddlRoster3.SelectedValue);
            }
            //else
            //{
            //    ddlRoster.Items.Add(ddlRoster2.SelectedValue);
            //}
            if (ddlInjured.Items.Contains(ddlRoster3.SelectedItem))
            {
                ddlInjured.Items.Remove(ddlRoster3.SelectedValue);
            }
            //else
            //{
            //    ddlInjured.Items.Add(ddlRoster2.SelectedValue);
            //}
            p3Changes += 1;
        }

        protected void chkP_CheckedChanged(object sender, EventArgs e)
        {
            txt1P.Visible = true;
            txt1P.Text = "0";
            if (ddlRoster2.SelectedValue != "")
            {
                txt2P.Visible = true;
                txt2P.Text = "0";
            }
            if (ddlRoster3.SelectedValue != "")
            {
                txt3P.Visible = true;
                txt3P.Text = "0";
            }

            if (chkP.Checked == false)
            {
                txt1P.Visible = false;
                txt2P.Visible = false;
                txt3P.Visible = false;
                txt1P.Text = "";
                txt2P.Text = "";
                txt3P.Text = "";
            }
        }

        protected void chkA_CheckedChanged(object sender, EventArgs e)
        {
            txt1A.Visible = true;
            txt1A.Text = "0";
            if (ddlRoster2.SelectedValue != "")
            {
                txt2A.Visible = true;
                txt2A.Text = "0";
            }
            if (ddlRoster3.SelectedValue != "")
            {
                txt3A.Visible = true;
                txt3A.Text = "0";
            }
            if (chkA.Checked == false)
            {
                txt1A.Visible = false;
                txt2A.Visible = false;
                txt3A.Visible = false;
                txt1A.Text = "";
                txt2A.Text = "";
                txt3A.Text = "";
            }
        }

        protected void chkR_CheckedChanged(object sender, EventArgs e)
        {
            txt1R.Visible = true;
            txt1R.Text = "0";
            if (ddlRoster2.Text != "")
            {
                txt2R.Visible = true;
                txt2R.Text = "0";
            }
            if (ddlRoster3.SelectedValue != "")
            {
                txt3R.Visible = true;
                txt3R.Text = "0";
            }
            if (chkR.Checked == false)
            {
                txt1R.Visible = false;
                txt2R.Visible = false;
                txt3R.Visible = false;
                txt1R.Text = "";
                txt2R.Text = "";
                txt3R.Text = "";
            }
        }
        protected void chk3_CheckedChanged(object sender, EventArgs e)
        {
            txt13.Visible = true;
            txt13.Text = "0";
            if (ddlRoster2.SelectedValue != "")
            {
                txt23.Visible = true;
                txt23.Text = "0";
            }
            if (ddlRoster3.SelectedValue != "")
            {
                txt33.Visible = true;
                txt33.Text = "0";
            }
            if (chk3.Checked == false)
            {
                txt13.Visible = false;
                txt23.Visible = false;
                txt33.Visible = false;
                txt13.Text = "";
                txt23.Text = "";
                txt33.Text = "";
            }
        }

        protected void chkS_CheckedChanged(object sender, EventArgs e)
        {
            txt1S.Visible = true;
            txt1S.Text = "0";
            if (ddlRoster2.SelectedValue != "")
            {
                txt2S.Visible = true;
                txt2S.Text = "0";
            }
            if (ddlRoster3.SelectedValue != "")
            {
                txt3S.Visible = true;
                txt3S.Text = "0";
            }
            if (chkS.Checked == false)
            {
                txt1S.Visible = false;
                txt2S.Visible = false;
                txt3S.Visible = false;
                txt1S.Text = "";
                txt2S.Text = "";
                txt3S.Text = "";
            }
        }

        protected void chkB_CheckedChanged(object sender, EventArgs e)
        {
            txt1B.Visible = true;
            txt1B.Text = "0";
            if (ddlRoster2.SelectedValue != "")
            {
                txt2B.Visible = true;
                txt2B.Text = "0";
            }
            if (ddlRoster3.SelectedValue != "")
            {
                txt3B.Visible = true;
                txt3B.Text = "0";
            }
            if (chkB.Checked == false)
            {
                txt1B.Visible = false;
                txt2B.Visible = false;
                txt3B.Visible = false;
                txt1B.Text = "";
                txt2B.Text = "";
                txt3B.Text = "";
            }
        }
        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            // List to store controls with values
            List<string> controlsWithValue = new List<string>();

            string Query = "";

            string Team = "";
            string player1 = "";
            string playerI = "";
            string player2 = "";
            string player3 = "";
            List<string> PlayerList = new List<string>();
            List<string> PropList = new List<string>();
            List<int> Player1Props = new List<int>();
            List<int> Player2Props = new List<int>();
            List<int> Player3Props = new List<int>();

            // Filter and find only specific controls on the page
            FindSpecificControls(this.Controls, controlsWithValue);

            // Display or process the list of controls that have values
            foreach (var controlValue in controlsWithValue)
            {

                Response.Write(controlValue + "<br/>"); // Example to write to the page
                if (controlValue.Contains("DropDownList ID: ddTeams"))
                {
                    Team = controlValue.Remove(0, 33);
                }
                if (controlValue.Contains("DropDownList ID: ddlRoster,"))
                {
                    PlayerList.Add("1. " + controlValue.Remove(0, 35));
                    player1 = controlValue.Remove(0, 35);
                }
                if (controlValue.Contains("DropDownList ID: ddlInjured,"))
                {
                    PlayerList.Add("I. " + controlValue.Remove(0, 36));
                    playerI = controlValue.Remove(0, 36);
                }
                if (controlValue.Contains("DropDownList ID: ddlRoster2,"))
                {
                    PlayerList.Add("2. " + controlValue.Remove(0, 36));
                    player2 = controlValue.Remove(0, 36);
                }
                if (controlValue.Contains("DropDownList ID: ddlRoster3,"))
                {
                    PlayerList.Add("3. " + controlValue.Remove(0, 36));
                    player3 = controlValue.Remove(0, 36);
                }
                if (controlValue.Contains("CheckBox ID: "))
                {
                    string prop = controlValue.Remove(17).Remove(0, 16);
                    if(prop == "P")
                    {
                        prop = "points";
                    }
                    if (prop == "A")
                    {
                        prop = "assists";
                    }
                    if (prop == "R")
                    {
                        prop = "reboundsTotal";
                    }
                    if (prop == "3")
                    {
                        prop = "threePointersMade";
                    }
                    if (prop == "B")
                    {
                        prop = "blocks";
                    }
                    if (prop == "S")
                    {
                        prop = "steals";
                    }
                    PropList.Add(prop);
                }
                //if (controlValue.Contains("txt1"))
                //{
                //    Player1Props.Add(Int32.Parse(controlValue.Remove(0, 26)));
                //}
            }
            foreach (string prop in PropList)
            {
                string propID = "";
                if(prop == "threePointersMade")
                {
                    propID = "3";
                }
                else
                {
                    propID = prop.Substring(0, 1).ToUpper();
                }
                foreach (string player in PlayerList)
                {
                    if (player.Contains("1. "))
                    {
                        // Dynamically construct the TextBox ID
                        string controlId = "txt1" + propID; // e.g., txt1P, txt1A, txt1R

                        // Use recursive FindControl to get the TextBox by its ID
                        TextBox dynamicTextBox = FindControlRecursive(this, controlId) as TextBox;
                        if (dynamicTextBox != null)
                        {
                            // Check if the text is null or whitespace, and assign "0" if true
                            if (string.IsNullOrWhiteSpace(dynamicTextBox.Text))
                            {
                                dynamicTextBox.Text = "0";
                            }
                            // Now safely parse the value as it's guaranteed to be a valid number
                            Player1Props.Add(Int32.Parse(dynamicTextBox.Text));
                        }
                    }
                    if (player.Contains("2. "))
                    {
                        // Dynamically construct the TextBox ID
                        string controlId = "txt2" + propID; // e.g., txt1P, txt1A, txt1R

                        // Use recursive FindControl to get the TextBox by its ID
                        TextBox dynamicTextBox = FindControlRecursive(this, controlId) as TextBox;
                        if (dynamicTextBox != null)
                        {
                            // Check if the text is null or whitespace, and assign "0" if true
                            if (string.IsNullOrWhiteSpace(dynamicTextBox.Text))
                            {
                                dynamicTextBox.Text = "0";
                            }
                            // Now safely parse the value as it's guaranteed to be a valid number
                            Player2Props.Add(Int32.Parse(dynamicTextBox.Text));
                        }
                    }
                    if (player.Contains("3. "))
                    {
                        // Dynamically construct the TextBox ID
                        string controlId = "txt3" + propID; // e.g., txt1P, txt1A, txt1R

                        // Use recursive FindControl to get the TextBox by its ID
                        TextBox dynamicTextBox = FindControlRecursive(this, controlId) as TextBox;
                        if (dynamicTextBox != null)
                        {
                            // Check if the text is null or whitespace, and assign "0" if true
                            if (string.IsNullOrWhiteSpace(dynamicTextBox.Text))
                            {
                                dynamicTextBox.Text = "0";
                            }
                            // Now safely parse the value as it's guaranteed to be a valid number
                            Player3Props.Add(Int32.Parse(dynamicTextBox.Text));
                        }
                    }
                }
            }
            ParlayBuilder parlayBuilder = new ParlayBuilder();
            parlayBuilder.BuildQuery(Team, PlayerList, player1, player2, player3, playerI, PropList, Player1Props, Player2Props, Player3Props);

        }

        protected void BuildQuery1(string Team, List<string> PlayerList, string Player1, string Player2, string Player3, string PlayerI, List<string> PropList, List<int> Player1Props, List<int> Player2Props, List<int> Player3Props)
        {
            string query = "";
            if (Player1 != "")
            {
                query += "";
            }
        }

        private void FindSpecificControls(ControlCollection controls, List<string> controlsWithValue)
        {
            foreach (Control control in controls)
            {
                // Only check TextBox, DropDownList, and CheckBox
                if (control is TextBox txt && !string.IsNullOrEmpty(txt.Text))
                {
                    controlsWithValue.Add($"TextBox ID: {txt.ID}, Value: {txt.Text}");
                }
                else if (control is DropDownList ddl && ddl.SelectedValue != "")
                {
                    controlsWithValue.Add($"DropDownList ID: {ddl.ID}, Value: {ddl.SelectedValue}");
                }
                else if (control is CheckBox chk && chk.Checked)
                {
                    controlsWithValue.Add($"CheckBox ID: {chk.ID}, Checked: {chk.Checked}");
                }

                // Recursively search for child controls only if the current control is a container (e.g., Panel)
                if (control.HasControls())
                {
                    FindSpecificControls(control.Controls, controlsWithValue);
                }
            }
        }

        private Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
            {
                return root;
            }
            foreach (Control child in root.Controls)
            {
                Control foundControl = FindControlRecursive(child, id);
                if (foundControl != null)
                {
                    return foundControl;
                }
            }
            return null;
        }

        protected void aWinsChk_CheckedChanged(object sender, EventArgs e)
        {
            ParlayAverages parlayAverages = new ParlayAverages();
            if (aWinsChk.Checked)
            {
                aLossChk.Checked = false;
            }
            parlayAverages.GetAverages(aWinsChk.Checked, aLossChk.Checked, ddlRoster.SelectedValue, ddTeams.SelectedValue.Remove(0, 6), a1Name, a1Team, a1Points, a1Assists, a1Rebounds, a1Threes, a1Blocks, a1Steals, a1Minutes, a1pd, a1ad, a1rd, a13d, a1bd, a1sd, Int32.Parse(ddSeason.SelectedValue));
            parlayAverages.GetAverages(aWinsChk.Checked, aLossChk.Checked, ddlRoster2.SelectedValue, ddTeams.SelectedValue.Remove(0, 6), a2Name, a2Team, a2Points, a2Assists, a2Rebounds, a2Threes, a2Blocks, a2Steals, a2Minutes, a2pd, a2ad, a2rd, a23d, a2bd, a2sd, Int32.Parse(ddSeason.SelectedValue));
            parlayAverages.GetAverages(aWinsChk.Checked, aLossChk.Checked, ddlRoster3.SelectedValue, ddTeams.SelectedValue.Remove(0, 6), a3Name, a3Team, a3Points, a3Assists, a3Rebounds, a3Threes, a3Blocks, a3Steals, a3Minutes, a3pd, a3ad, a3rd, a33d, a3bd, a3sd, Int32.Parse(ddSeason.SelectedValue));
        }

        protected void aLossChk_CheckedChanged(object sender, EventArgs e)
        {
            ParlayAverages parlayAverages = new ParlayAverages();
            if (aLossChk.Checked)
            {
                aWinsChk.Checked = false;
            }
            parlayAverages.GetAverages(aWinsChk.Checked, aLossChk.Checked, ddlRoster.SelectedValue, ddTeams.SelectedValue.Remove(0, 6), a1Name, a1Team, a1Points, a1Assists, a1Rebounds, a1Threes, a1Blocks, a1Steals, a1Minutes, a1pd, a1ad, a1rd, a13d, a1bd, a1sd, Int32.Parse(ddSeason.SelectedValue));
            parlayAverages.GetAverages(aWinsChk.Checked, aLossChk.Checked, ddlRoster2.SelectedValue, ddTeams.SelectedValue.Remove(0, 6), a2Name, a2Team, a2Points, a2Assists, a2Rebounds, a2Threes, a2Blocks, a2Steals, a2Minutes, a2pd, a2ad, a2rd, a23d, a2bd, a2sd, Int32.Parse(ddSeason.SelectedValue));
            parlayAverages.GetAverages(aWinsChk.Checked, aLossChk.Checked, ddlRoster3.SelectedValue, ddTeams.SelectedValue.Remove(0, 6), a3Name, a3Team, a3Points, a3Assists, a3Rebounds, a3Threes, a3Blocks, a3Steals, a3Minutes, a3pd, a3ad, a3rd, a33d, a3bd, a3sd, Int32.Parse(ddSeason.SelectedValue));
        }

        protected void ddSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            string p1 = ddlRoster.SelectedValue;
            string p2 = ddlRoster2.SelectedValue;
            string p3 = ddlRoster3.SelectedValue;
            string i = ddlInjured.SelectedValue;
            lblError.Text = "";
            ddlRoster.Items.Clear();
            ddlRoster2.Items.Clear();
            ddlRoster3.Items.Clear();
            ddlInjured.Items.Clear();
            //ddTeams_SelectedIndexChanged(sender, e);
            PopulateRoster();

            if (!p1.IsNullOrWhiteSpace())
            {
                try
                {
                    ddlRoster.SelectedValue = p1;
                    ddlRoster_SelectedIndexChanged(sender, e);
                }
                catch
                {
                    lblError.Text = "One of the players selected switched teams between the selected season and season selected prior.";
                    ddTeams_SelectedIndexChanged(sender, e);
                }
            }
            if (!p2.IsNullOrWhiteSpace())
            {
                try
                {
                    ddlRoster2.SelectedValue = p2;
                    ddlRoster2_SelectedIndexChanged(sender, e);
                }
                catch
                {
                    lblError.Text = "One of the players selected switched teams between the selected season and season selected prior.";
                    ddTeams_SelectedIndexChanged(sender, e);
                }
            }
            if (!p3.IsNullOrWhiteSpace())
            {
                try
                {
                    ddlRoster3.SelectedValue = p3;
                    ddlRoster3_SelectedIndexChanged(sender, e);
                }
                catch
                {
                    lblError.Text = "One of the players selected switched teams between the selected season and season selected prior.";
                    ddTeams_SelectedIndexChanged(sender, e);
                }
            }
            if (!i.IsNullOrWhiteSpace())
            {
                try
                {
                    ddlInjured.SelectedValue = i;
                    ddlInjured_SelectedIndexChanged(sender, e);
                }
                catch
                {
                    lblError.Text = "One of the players selected switched teams between the selected season and season selected prior.";
                    ddTeams_SelectedIndexChanged(sender, e);
                }
                //}
            }



    }
}
 