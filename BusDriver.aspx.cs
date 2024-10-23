using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBAdb
{
    public partial class BusDriver : System.Web.UI.Page
    {
        public static string ConnectionString = "Server=localhost;Database=NBAdb;User Id=test;Password=test123;";
        public SqlConnection SQLdb = new SqlConnection(BusDriver.ConnectionString);
        public static int tableCount = 0;
        public static int RegSeasonGameCount2020 = 1080;
        public static int RegSeasonGameCountElse = 1230;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDbStatus(); //This checks to how many tables are in the database. If there are any, First Time Load is disabled.
        }

        protected void btnFirstTimeLoad_Click(object sender, EventArgs e)
        {
            List<string> selectedSeasons = new List<string>();
            foreach (ListItem item in chkSeasons.Items)
            {
                if (item.Selected)
                {
                    selectedSeasons.Add(item.Value);
                }
            }
            if(selectedSeasons.Count == 0)
            {
                lblSeasonResult.Text = "Please select a season";
                lblSeasonResult.ForeColor = System.Drawing.Color.IndianRed;
            }
            else
            {
                FirstTimeLoad firstTimeLoad = new FirstTimeLoad();
                firstTimeLoad.Init(selectedSeasons, tableCount, lblSeasonResult, lblTimeElapsed);
            }            
        }


        protected void LoadDbStatus()   //This will load a count of Tables in the db, then send to LoadDbTableInfo if any exist.
        {
            using (SQLdb)
            {
                using (SqlCommand TableCount = new SqlCommand("TableCount")) //Here's our procedure -> SELECT COUNT(*) Tables, case when SUM(rows) is null then 0 else sum(rows) end Rows
                {                                                            //                        FROM sys.tables t inner join
                    TableCount.CommandType = CommandType.StoredProcedure;    //                        		sys.partitions p on t.object_id = p.object_id
                    using (SqlDataAdapter sTableCount = new SqlDataAdapter())//                        WHERE type_desc = 'USER_TABLE'
                    {
                        TableCount.Connection = SQLdb;
                        sTableCount.SelectCommand = TableCount;
                        SQLdb.Open();
                        SqlDataReader reader = TableCount.ExecuteReader();
                        if (reader.Read())
                        {
                            int tables = Int32.Parse(reader[0].ToString());
                            int rows = Int32.Parse(reader[1].ToString());
                            SQLdb.Close();
                            lblTables.Text = tables.ToString();
                            tableCount = tables;
                            if (tables != 0) //If we have tables, make our row section visible on the web page, then send to LoadDbTableInfo
                            {
                                row.Visible = true;
                                LoadDbTableInfo(); //Send 'er over
                                if (rows > 1000)
                                {
                                    //btnFirstTimeLoad.Enabled = false; //Since we verifiably have tables, let's make sure we disable the First Time Load button
                                }
                            }
                            else //If we don't have tables, no need to display that row. Close the connection and move on
                            {
                                row.Visible = false;
                            }
                        }
                        else
                        {
                            SQLdb.Close();
                            lblTableCount.Text = "Datbase connection is invalid, please see the Documentation page."; //I need another error handler, this is useless. It will never reach this code.
                        }                            
                    }
                }
            }            
        }
        protected void LoadDbTableInfo() //Pull the names of the tables we have
        {
            using (SQLdb)
            {
                using (SqlCommand TableNames = new SqlCommand("Tables")) //Here's our procedure -> SELECT t.Name, p.rows Rows
                {                                                        //                        from sys.tables t inner join
                    TableNames.CommandType = CommandType.StoredProcedure;//                        		sys.partitions p on t.object_id = p.object_id
                    using (SqlDataAdapter sTableNames = new SqlDataAdapter())//                    WHERE type_desc = 'USER_TABLE'
                    {
                        TableNames.Connection = SQLdb;
                        sTableNames.SelectCommand = TableNames;
                        SQLdb.Open();
                        SqlDataReader reader = TableNames.ExecuteReader();
                        while (reader.Read()) 
                        {

                            Label lbl = new Label();
                            Panel divPanel = new Panel();
                            divPanel.CssClass = "column";

                            //Get Table Names
                            lbl.Text = reader[0].ToString() + " - " + reader[1].ToString() + " rows";

                            // Add the label to a placeholder (or panel or form)
                            divPanel.Controls.Add(lbl);

                            // Optionally add a line break for display formatting
                            placeholder.Controls.Add(divPanel);
                        }
                        SQLdb.Close();
                    }
                }
                using (SqlCommand TableNames = new SqlCommand("GameCount"))
                {
                    TableNames.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sTableNames = new SqlDataAdapter())
                    {
                        TableNames.Connection = SQLdb;
                        sTableNames.SelectCommand = TableNames;
                        SQLdb.Open();
                        SqlDataReader reader = TableNames.ExecuteReader();
                        int i = 0;
                        int j = 1;
                        while (reader.Read())
                        {
                            if (reader[0].ToString() == "2020" && reader[1].ToString() == "1080")
                            {
                                chkSeasons.Items[0].Enabled = false;
                            }
                            else if (reader[0].ToString() == "2021" && reader[1].ToString() == "1230")
                            {
                                chkSeasons.Items[1].Enabled = false;
                            }
                            else if (reader[0].ToString() == "2022" && reader[1].ToString() == "1230")
                            {
                                chkSeasons.Items[2].Enabled = false;
                            }
                            else if (reader[0].ToString() == "2023" && reader[1].ToString() == "1230")
                            {
                                chkSeasons.Items[3].Enabled = false;
                            }
                        }
                        SQLdb.Close();
                    }
                }
                chkSeasons.Items[4].Enabled = true;
            }
        }

    }
}