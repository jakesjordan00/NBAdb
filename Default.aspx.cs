using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace NBAdb
{
    public partial class _Default : Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            string team = Session["team"].ToString();

            string savepath = "C:\\Users\\derfj\\Desktop\\NBAdb\\NBAdb\\Game Notes\\";
            string fullPath = savepath + team + " - " + DateTime.Today.Month + "." + DateTime.Today.Day + "." + DateTime.Today.Year + ".pdf";

            if (File.Exists(fullPath))
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "inline; filename=" + team + " - " + DateTime.Today.Month + "." + DateTime.Today.Day + "." + DateTime.Today.Year + ".pdf");
                Response.WriteFile(fullPath);
                Response.End();
            }

        }
    }
}