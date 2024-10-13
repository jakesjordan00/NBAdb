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


        public void GetProcedure(string p, string i, string p2, string p3, string procedure)
        {
            if (!string.IsNullOrEmpty(p) && !string.IsNullOrEmpty(i) && string.IsNullOrEmpty(p2) && string.IsNullOrEmpty(p3))
            {
                procedure = "ParlayAveragesInjured";
            }
        }
    }
}