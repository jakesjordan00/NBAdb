using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using NBAdb;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Net.Http;
using System.Threading.Tasks;

namespace NBAdb
{
    public partial class AdminBoltOn : System.Web.UI.Page
    {
        public static BusDriver busDriver = new BusDriver();
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = "";
            string yeah = "";
            string season = "2015";
            List<int> games = new List<int>();
            List<int> playoffGames = new List<int>();
            using (SqlCommand getGames = new SqlCommand("select distinct game_id from game where season_id = " + season))
            {
                getGames.Connection = busDriver.SQLdb;
                getGames.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = getGames.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        games.Add(sdr.GetInt32(0));
                    }                   
                }
                busDriver.SQLdb.Close();
            }
            using (SqlCommand getGames = new SqlCommand("select distinct game_id from playoffGame where season_id = " + season))
            {
                getGames.Connection = busDriver.SQLdb;
                getGames.CommandType = CommandType.Text;
                busDriver.SQLdb.Open();
                using (SqlDataReader sdr = getGames.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        playoffGames.Add(sdr.GetInt32(0));
                    }
                }
                busDriver.SQLdb.Close();
            }
            string jsonStart = "{\"season\":{\"season_id\":" + season + ",\"games\":{\"regularSeason\":[{\"game_id\":";
            for (int i = 0; i < games.Count; i++)
            {
                jsonStart += "\"" + games[i] + "\"";


                string pbpPath = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\" + season + "\\pbp\\00" + games[i] + ".json";
                string boxPath = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\" + season + "\\box\\00" + games[i] + ".json";

                string pbpData = File.ReadAllText(pbpPath);
                string boxData = File.ReadAllText(boxPath);
                //pbpData = pbpData.Remove(0, 56);
                pbpData = "," + pbpData.Remove(0, 1);

                jsonStart += pbpData + ",\"box" + boxData.Remove(0, 6) + ",";
                //{"season":{"season_id":2019,"games":{"regularSeason":[{"game_id":"0021600002","playByPlay":{"actions"
                //{"playByPlay":{"gameId":"0021600001","videoAvailable":1,"actions":[
            }
            jsonStart = jsonStart.Remove(jsonStart.Length - 1);
            jsonStart +=  "],\"playoffs\":[{\"game_id\":";

            for (int i = 0; i < playoffGames.Count; i++)
            {
                string playoffPbpPath = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\playoffs\" + season + "\\pbp\\00" + playoffGames[i] + ".json";
                string playoffBoxPath = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\playoffs\" + season + "\\box\\00" + playoffGames[i] + ".json";
                string playoffPbpData = File.ReadAllText(playoffPbpPath);
                string playoffBoxData = File.ReadAllText(playoffBoxPath);
                playoffPbpData = "," + playoffPbpData.Remove(0, 1);
                jsonStart += playoffPbpData + ",\"box" + playoffPbpData.Remove(0, 6) + ",";

            }
            jsonStart = jsonStart.Remove(jsonStart.Length - 1);
            jsonStart += "}]}";

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
        }
    }
}