﻿using Newtonsoft.Json;
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
using static NBAdbPre2019.Pre2019;


namespace NBAdbPre2019
{
    public partial class Pre2019
    {
        public static BusDriver busDriver = new BusDriver();
        public static List<string> games = new List<string>
        {
            "0021800001","0021800002","0021800003","0021800004","0021800005","0021800006","0021800007","0021800008","0021800009","0021800010","0021800011","0021800012","0021800013","0021800014","0021800015","0021800016","0021800017","0021800018","0021800019","0021800020","0021800021","0021800022","0021800023","0021800024","0021800025","0021800026","0021800027","0021800028","0021800029","0021800030","0021800031","0021800032","0021800033","0021800034","0021800035","0021800036","0021800037","0021800038","0021800039","0021800040","0021800041","0021800042","0021800043","0021800044","0021800045","0021800046","0021800047","0021800048","0021800049","0021800050","0021800051","0021800052","0021800053","0021800054","0021800055","0021800056","0021800057","0021800058","0021800059","0021800060","0021800061","0021800062","0021800063","0021800064","0021800065","0021800066","0021800067","0021800068","0021800069","0021800070","0021800071","0021800072","0021800073","0021800074","0021800075","0021800076","0021800077","0021800078","0021800079","0021800080","0021800081","0021800082","0021800083","0021800084","0021800085","0021800086","0021800087","0021800088","0021800089","0021800090","0021800091","0021800092","0021800093","0021800094","0021800095","0021800096","0021800097","0021800098","0021800099","0021800100","0021800101","0021800102","0021800103","0021800104","0021800105","0021800106","0021800107","0021800108","0021800109","0021800110","0021800111","0021800112","0021800113","0021800114","0021800115","0021800116","0021800117","0021800118","0021800119","0021800120","0021800121","0021800122","0021800123","0021800124","0021800125","0021800126","0021800127","0021800128","0021800129","0021800130","0021800131","0021800132","0021800133","0021800134","0021800135","0021800136","0021800137","0021800138","0021800139","0021800140","0021800141","0021800142","0021800143","0021800144","0021800145","0021800146","0021800147","0021800148","0021800149","0021800150","0021800151","0021800152","0021800153","0021800154","0021800155","0021800156","0021800157","0021800158","0021800159","0021800160","0021800161","0021800162","0021800163","0021800164","0021800165","0021800166","0021800167","0021800168","0021800169","0021800170","0021800171","0021800172","0021800173","0021800174","0021800175","0021800176","0021800177","0021800178","0021800179","0021800180","0021800181","0021800182","0021800183","0021800184","0021800185","0021800186","0021800187","0021800188","0021800189","0021800190","0021800191","0021800192","0021800193","0021800194","0021800195","0021800196","0021800197","0021800198","0021800199","0021800200","0021800201","0021800202","0021800203","0021800204","0021800205","0021800206","0021800207","0021800208","0021800209","0021800210","0021800211","0021800212","0021800213","0021800214","0021800215","0021800216","0021800217","0021800218","0021800219","0021800220","0021800221","0021800222","0021800223","0021800224","0021800225","0021800226","0021800227","0021800228","0021800229","0021800230","0021800231","0021800232","0021800233","0021800234","0021800235","0021800236","0021800237","0021800238","0021800239","0021800240","0021800241","0021800242","0021800243","0021800244","0021800245","0021800246","0021800247","0021800248","0021800249","0021800250","0021800251","0021800252","0021800253","0021800254","0021800255","0021800256","0021800257","0021800258","0021800259","0021800260","0021800261","0021800262","0021800263","0021800264","0021800265","0021800266","0021800267","0021800268","0021800269","0021800270","0021800271","0021800272","0021800273","0021800274","0021800275","0021800276","0021800277","0021800278","0021800279","0021800280","0021800281","0021800282","0021800283","0021800284","0021800285","0021800286","0021800287","0021800288","0021800289","0021800290","0021800291","0021800292","0021800293","0021800294","0021800295","0021800296","0021800297","0021800298","0021800299","0021800300","0021800301","0021800302","0021800303","0021800304","0021800305","0021800306","0021800307","0021800308","0021800309","0021800310","0021800311","0021800312","0021800313","0021800314","0021800315","0021800316","0021800317","0021800318","0021800319","0021800320","0021800321","0021800322","0021800323","0021800324","0021800325","0021800326","0021800327","0021800328","0021800329","0021800330","0021800331","0021800332","0021800333","0021800334","0021800335","0021800336","0021800337","0021800338","0021800339","0021800340","0021800341","0021800342","0021800343","0021800344","0021800345","0021800346","0021800347","0021800348","0021800349","0021800350","0021800351","0021800352","0021800353","0021800354","0021800355","0021800356","0021800357","0021800358","0021800359","0021800360","0021800361","0021800362","0021800363","0021800364","0021800365","0021800366","0021800367","0021800368","0021800369","0021800370","0021800371","0021800372","0021800373","0021800374","0021800375","0021800376","0021800377","0021800378","0021800379","0021800380","0021800381","0021800382","0021800383","0021800384","0021800385","0021800386","0021800387","0021800388","0021800389","0021800390","0021800391","0021800392","0021800393","0021800394","0021800395","0021800396","0021800397","0021800398","0021800399","0021800400","0021800401","0021800402","0021800403","0021800404","0021800405","0021800406","0021800407","0021800408","0021800409","0021800410","0021800411","0021800412","0021800413","0021800414","0021800415","0021800416","0021800417","0021800418","0021800419","0021800420","0021800421","0021800422","0021800423","0021800424","0021800425","0021800426","0021800427","0021800428","0021800429","0021800430","0021800431","0021800432","0021800433","0021800434","0021800435","0021800436","0021800437","0021800438","0021800439","0021800440","0021800441","0021800442","0021800443","0021800444","0021800445","0021800446","0021800447","0021800448","0021800449","0021800450","0021800451","0021800452","0021800453","0021800454","0021800455","0021800456","0021800457","0021800458","0021800459","0021800460","0021800461","0021800462","0021800463","0021800464","0021800465","0021800466","0021800467","0021800468","0021800469","0021800470","0021800471","0021800472","0021800473","0021800474","0021800475","0021800476","0021800477","0021800478","0021800479","0021800480","0021800481","0021800482","0021800483","0021800484","0021800485","0021800486","0021800487","0021800488","0021800489","0021800490","0021800491","0021800492","0021800493","0021800494","0021800495","0021800496","0021800497","0021800498","0021800499","0021800500","0021800501","0021800502","0021800503","0021800504","0021800505","0021800506","0021800507","0021800508","0021800509","0021800510","0021800511","0021800512","0021800513","0021800514","0021800515","0021800516","0021800517","0021800518","0021800519","0021800520","0021800521","0021800522","0021800523","0021800524","0021800525","0021800526","0021800527","0021800528","0021800529","0021800530","0021800531","0021800532","0021800533","0021800534","0021800535","0021800536","0021800537","0021800538","0021800539","0021800540","0021800541","0021800542","0021800543","0021800544","0021800545","0021800546","0021800547","0021800548","0021800549","0021800550","0021800551","0021800552","0021800553","0021800554","0021800555","0021800556","0021800557","0021800558","0021800559","0021800560","0021800561","0021800562","0021800563","0021800564","0021800565","0021800566","0021800567","0021800568","0021800569","0021800570","0021800571","0021800572","0021800573","0021800574","0021800575","0021800576","0021800577","0021800578","0021800579","0021800580","0021800581","0021800582","0021800583","0021800584","0021800585","0021800586","0021800587","0021800588","0021800589","0021800590","0021800591","0021800592","0021800593","0021800594","0021800595","0021800596","0021800597","0021800598","0021800599","0021800600","0021800601","0021800602","0021800603","0021800604","0021800605","0021800606","0021800607","0021800608","0021800609","0021800610","0021800611","0021800612","0021800613","0021800614","0021800615","0021800616","0021800617","0021800618","0021800619","0021800620","0021800621","0021800622","0021800623","0021800624","0021800625","0021800626","0021800627","0021800628","0021800629","0021800630","0021800631","0021800632","0021800633","0021800634","0021800635","0021800636","0021800637","0021800638","0021800639","0021800640","0021800641","0021800642","0021800643","0021800644","0021800645","0021800646","0021800647","0021800648","0021800649","0021800650","0021800651","0021800652","0021800653","0021800654","0021800655","0021800656","0021800657","0021800658","0021800659","0021800660","0021800661","0021800662","0021800663","0021800664","0021800665","0021800666","0021800667","0021800668","0021800669","0021800670","0021800671","0021800672","0021800673","0021800674","0021800675","0021800676","0021800677","0021800678","0021800679","0021800680","0021800681","0021800682","0021800683","0021800684","0021800685","0021800686","0021800687","0021800688","0021800689","0021800690","0021800691","0021800692","0021800693","0021800694","0021800695","0021800696","0021800697","0021800698","0021800699","0021800700","0021800701","0021800702","0021800703","0021800704","0021800705","0021800706","0021800707","0021800708","0021800709","0021800710","0021800711","0021800712","0021800713","0021800714","0021800715","0021800716","0021800717","0021800718","0021800719","0021800720","0021800721","0021800722","0021800723","0021800724","0021800725","0021800726","0021800727","0021800728","0021800729","0021800730","0021800731","0021800732","0021800733","0021800734","0021800735","0021800736","0021800737","0021800738","0021800739","0021800740","0021800741","0021800742","0021800743","0021800744","0021800745","0021800746","0021800747","0021800748","0021800749","0021800750","0021800751","0021800752","0021800753","0021800754","0021800755","0021800756","0021800757","0021800758","0021800759","0021800760","0021800761","0021800762","0021800763","0021800764","0021800765","0021800766","0021800767","0021800768","0021800769","0021800770","0021800771","0021800772","0021800773","0021800774","0021800775","0021800776","0021800777","0021800778","0021800779","0021800780","0021800781","0021800782","0021800783","0021800784","0021800785","0021800786","0021800787","0021800788","0021800789","0021800790","0021800791","0021800792","0021800793","0021800794","0021800795","0021800796","0021800797","0021800798","0021800799","0021800800","0021800801","0021800802","0021800803","0021800804","0021800805","0021800806","0021800807","0021800808","0021800809","0021800810","0021800811","0021800812","0021800813","0021800814","0021800815","0021800816","0021800817","0021800818","0021800819","0021800820","0021800821","0021800822","0021800823","0021800824","0021800825","0021800826","0021800827","0021800828","0021800829","0021800830","0021800831","0021800832","0021800833","0021800834","0021800835","0021800836","0021800837","0021800838","0021800839","0021800840","0021800841","0021800842","0021800843","0021800844","0021800845","0021800846","0021800847","0021800848","0021800849","0021800850","0021800851","0021800852","0021800853","0021800854","0021800855","0021800856","0021800857","0021800858","0021800859","0021800860","0021800861","0021800862","0021800863","0021800864","0021800865","0021800866","0021800867","0021800868","0021800869","0021800870","0021800871","0021800872","0021800873","0021800874","0021800875","0021800876","0021800877","0021800878","0021800879","0021800880","0021800881","0021800882","0021800883","0021800884","0021800885","0021800886","0021800887","0021800888","0021800889","0021800890","0021800891","0021800892","0021800893","0021800894","0021800895","0021800896","0021800897","0021800898","0021800899","0021800900","0021800901","0021800902","0021800903","0021800904","0021800905","0021800906","0021800907","0021800908","0021800909","0021800910","0021800911","0021800912","0021800913","0021800914","0021800915","0021800916","0021800917","0021800918","0021800919","0021800920","0021800921","0021800922","0021800923","0021800924","0021800925","0021800926","0021800927","0021800928","0021800929","0021800930","0021800931","0021800932","0021800933","0021800934","0021800935","0021800936","0021800937","0021800938","0021800939","0021800940","0021800941","0021800942","0021800943","0021800944","0021800945","0021800946","0021800947","0021800948","0021800949","0021800950","0021800951","0021800952","0021800953","0021800954","0021800955","0021800956","0021800957","0021800958","0021800959","0021800960","0021800961","0021800962","0021800963","0021800964","0021800965","0021800966","0021800967","0021800968","0021800969","0021800970","0021800971","0021800972","0021800973","0021800974","0021800975","0021800976","0021800977","0021800978","0021800979","0021800980","0021800981","0021800982","0021800983","0021800984","0021800985","0021800986","0021800987","0021800988","0021800989","0021800990","0021800991","0021800992","0021800993","0021800994","0021800995","0021800996","0021800997","0021800998","0021800999","0021801000","0021801001","0021801002","0021801003","0021801004","0021801005","0021801006","0021801007","0021801008","0021801009","0021801010","0021801011","0021801012","0021801013","0021801014","0021801015","0021801016","0021801017","0021801018","0021801019","0021801020","0021801021","0021801022","0021801023","0021801024","0021801025","0021801026","0021801027","0021801028","0021801029","0021801030","0021801031","0021801032","0021801033","0021801034","0021801035","0021801036","0021801037","0021801038","0021801039","0021801040","0021801041","0021801042","0021801043","0021801044","0021801045","0021801046","0021801047","0021801048","0021801049","0021801050","0021801051","0021801052","0021801053","0021801054","0021801055","0021801056","0021801057","0021801058","0021801059","0021801060","0021801061","0021801062","0021801063","0021801064","0021801065","0021801066","0021801067","0021801068","0021801069","0021801070","0021801071","0021801072","0021801073","0021801074","0021801075","0021801076","0021801077","0021801078","0021801079","0021801080","0021801081","0021801082","0021801083","0021801084","0021801085","0021801086","0021801087","0021801088","0021801089","0021801090","0021801091","0021801092","0021801093","0021801094","0021801095","0021801096","0021801097","0021801098","0021801099","0021801100","0021801101","0021801102","0021801103","0021801104","0021801105","0021801106","0021801107","0021801108","0021801109","0021801110","0021801111","0021801112","0021801113","0021801114","0021801115","0021801116","0021801117","0021801118","0021801119","0021801120","0021801121","0021801122","0021801123","0021801124","0021801125","0021801126","0021801127","0021801128","0021801129","0021801130","0021801131","0021801132","0021801133","0021801134","0021801135","0021801136","0021801137","0021801138","0021801139","0021801140","0021801141","0021801142","0021801143","0021801144","0021801145","0021801146","0021801147","0021801148","0021801149","0021801150","0021801151","0021801152","0021801153","0021801154","0021801155","0021801156","0021801157","0021801158","0021801159","0021801160","0021801161","0021801162","0021801163","0021801164","0021801165","0021801166","0021801167","0021801168","0021801169","0021801170","0021801171","0021801172","0021801173","0021801174","0021801175","0021801176","0021801177","0021801178","0021801179","0021801180","0021801181","0021801182","0021801183","0021801184","0021801185","0021801186","0021801187","0021801188","0021801189","0021801190","0021801191","0021801192","0021801193","0021801194","0021801195","0021801196","0021801197","0021801198","0021801199","0021801200","0021801201","0021801202","0021801203","0021801204","0021801205","0021801206","0021801207","0021801208","0021801209","0021801210","0021801211","0021801212","0021801213","0021801214","0021801215","0021801216","0021801217","0021801218","0021801219","0021801220","0021801221","0021801222","0021801223","0021801224","0021801225","0021801226","0021801227","0021801228","0021801229",
            "0021801230"

        };




        public static void Go(string instructions)
        {
            string path = "";
            string yeah = "";
            if(instructions == "Postseason")
            {
                path = "playoffs\\";
            }
            //WriteFile();
            for (int i = 0; i < games.Count; i++)
            {
                string season = "20" + games[i].Substring(3, 2);
                string filePath = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\" + path + season + "\\" + games[i] + ".txt";
                ////Full File
                string jsonData = File.ReadAllText(filePath).TrimStart().TrimEnd();




                ////Game and Box
                string box = jsonData.Substring(jsonData.IndexOf("\"game\": {"));
                int boxEnd = box.IndexOf("\"playByPlay");
                box = box.Substring(0, boxEnd); ;
                box = box.TrimStart().TrimEnd();
                string boxFormatted = "{" + box + "}";
                boxFormatted = boxFormatted.Replace("},}", "}}");
                boxFormatted = JToken.Parse(boxFormatted).ToString(Formatting.None);

                string boxOutput = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\" + path + season + "\\box\\" + games[i] + ".json";
                //Write the minified JSON back to a file
                File.WriteAllText(boxOutput, boxFormatted);

                if (instructions == "Postseason")
                {
                    GetDataBox(boxOutput, season, "playoffGameInsert");
                }
                else if (instructions == "Regular Season")
                {
                    GetDataBox(boxOutput, season, "gameInsert");
                }



                //PlayByPlay
                string pbp = jsonData.Substring(jsonData.IndexOf("playByPlay"));
                int pbpEnd = pbp.IndexOf("\"source\": \"hanaV3\"");
                pbp = pbp.Substring(0, pbpEnd);
                pbp = pbp.Replace("],", "]}");
                pbp = pbp.TrimStart().TrimEnd();
                string pbpFormatted = "{\"" + pbp + "}";
                pbpFormatted = JToken.Parse(pbpFormatted).ToString(Formatting.None);
                string pbpOutput = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\" + path + season + "\\pbp\\" + games[i] + ".json";
                // Write the minified JSON back to a file
                File.WriteAllText(pbpOutput, pbpFormatted);

                if (instructions == "Postseason")
                {
                    GetDataPBP(pbpOutput, "InsertOldPlayByPlayPlayoffData");
                }
                else if(instructions == "Regular Season")
                {
                    GetDataPBP(pbpOutput, "InsertOldPlayByPlayData");
                }

            }
        }




        public static void GetDataPBP(string file, string procedure)
        {
            string jsonData = File.ReadAllText(file);
            PlayByPlayData playByPlayData = JsonConvert.DeserializeObject<PlayByPlayData>(jsonData);
            PlayByPlay pbp = JsonConvert.DeserializeObject<PlayByPlay>(jsonData);
            for (int i = 0; i < playByPlayData.playByPlay.actions.Count(); i++)
            {
                using (SqlCommand querySearch = new SqlCommand(procedure))
                {
                    querySearch.Connection = busDriver.SQLdb;
                    querySearch.CommandType = CommandType.StoredProcedure;
                    if (procedure == "InsertOldPlayByPlayPlayoffData")
                    {
                        string series = playByPlayData.playByPlay.gameId.Substring(0, 9) + "1-";
                        querySearch.Parameters.AddWithValue("@series_id", series);
                        querySearch.Parameters.AddWithValue("@game", Int32.Parse(playByPlayData.playByPlay.gameId.Substring(9)));
                    }

                    querySearch.Parameters.AddWithValue("@season_id", Int32.Parse("20" + playByPlayData.playByPlay.gameId.Replace("004", "").Substring(0, 2)));
                    querySearch.Parameters.AddWithValue("@game_id", Int32.Parse(playByPlayData.playByPlay.gameId));
                    querySearch.Parameters.AddWithValue("@actionNumber", playByPlayData.playByPlay.actions[i].actionNumber);
                    querySearch.Parameters.AddWithValue("@actionID", playByPlayData.playByPlay.actions[i].actionID);
                    querySearch.Parameters.AddWithValue("@clock", playByPlayData.playByPlay.actions[i].clock);
                    querySearch.Parameters.AddWithValue("@period", playByPlayData.playByPlay.actions[i].period);
                    querySearch.Parameters.AddWithValue("@team_id", playByPlayData.playByPlay.actions[i].teamId);
                    querySearch.Parameters.AddWithValue("@teamTricode", playByPlayData.playByPlay.actions[i].teamTricode);
                    querySearch.Parameters.AddWithValue("@player_id", playByPlayData.playByPlay.actions[i].personId);
                    if(playByPlayData.playByPlay.actions[i].xLegacy == 0 && playByPlayData.playByPlay.actions[i].yLegacy == 0)
                    {
                        querySearch.Parameters.AddWithValue("@x", SqlDouble.Null);
                        querySearch.Parameters.AddWithValue("@y", SqlDouble.Null);

                    }
                    else
                    {
                        querySearch.Parameters.AddWithValue("@x", playByPlayData.playByPlay.actions[i].xLegacy);
                        querySearch.Parameters.AddWithValue("@y", playByPlayData.playByPlay.actions[i].yLegacy);
                    }
                    querySearch.Parameters.AddWithValue("@shotDistance", playByPlayData.playByPlay.actions[i].shotDistance);
                    querySearch.Parameters.AddWithValue("@shotResult", playByPlayData.playByPlay.actions[i].shotResult);
                    querySearch.Parameters.AddWithValue("@isFieldGoal", playByPlayData.playByPlay.actions[i].isFieldGoal);
                    if(playByPlayData.playByPlay.actions[i].scoreHome is null)
                    {
                        querySearch.Parameters.AddWithValue("@scoreHome", 0);
                    }
                    else
                    {
                        querySearch.Parameters.AddWithValue("@scoreHome", playByPlayData.playByPlay.actions[i].scoreHome);
                    }
                    if (playByPlayData.playByPlay.actions[i].scoreAway is null)
                    {
                        querySearch.Parameters.AddWithValue("@scoreAway", 0);
                    }
                    else
                    {
                        querySearch.Parameters.AddWithValue("@scoreAway", playByPlayData.playByPlay.actions[i].scoreAway);
                    }

                    querySearch.Parameters.AddWithValue("@description", playByPlayData.playByPlay.actions[i].description);
                    querySearch.Parameters.AddWithValue("@actionType", playByPlayData.playByPlay.actions[i].actionType);
                    querySearch.Parameters.AddWithValue("@actionSub", playByPlayData.playByPlay.actions[i].subType);

                    if(playByPlayData.playByPlay.actions[i].shotValue == 2 && playByPlayData.playByPlay.actions[i].shotResult == "Missed")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "2PTA");
                    }
                    else if (playByPlayData.playByPlay.actions[i].shotValue == 2 && playByPlayData.playByPlay.actions[i].shotResult == "Made")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "2PTM");
                    }
                    else if (playByPlayData.playByPlay.actions[i].shotValue == 3 && playByPlayData.playByPlay.actions[i].shotResult == "Missed")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "3PTA");
                    }
                    else if (playByPlayData.playByPlay.actions[i].shotValue == 3 && playByPlayData.playByPlay.actions[i].shotResult == "Made")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "3PTM");
                    }
                    else if (playByPlayData.playByPlay.actions[i].actionType.Contains("Free Throw") && playByPlayData.playByPlay.actions[i].shotResult == "Missed")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "FTA");
                    }
                    else if (playByPlayData.playByPlay.actions[i].actionType.Contains("Free Throw") && playByPlayData.playByPlay.actions[i].shotResult == "Made")
                    {
                        querySearch.Parameters.AddWithValue("@shotType", "FTM");
                    }
                    else
                    {
                        querySearch.Parameters.AddWithValue("@shotType", SqlString.Null);
                    }
                    busDriver.SQLdb.Open();
                    querySearch.ExecuteScalar(); // Used for other than SELECT Queries
                    busDriver.SQLdb.Close();


                    //FTA
                    //FTM

                }
            }
        }




        public static void GetDataBox(string file, string seasonString, string procedure)
        {
            string jsonData = File.ReadAllText(file);
            GameData game = JsonConvert.DeserializeObject<GameData>(jsonData);
                                                                                                        //Game Table
            int season = Int32.Parse(seasonString);
            int game_id = Int32.Parse(game.game.GameId);
            string date = game.game.GameCode.Split('/')[0];
            int homeID = game.game.HomeTeamId;
            int awayID = game.game.AwayTeamId;
            int loserID = 0;
            int loserScore = 0;
            int winnerID = 0;
            int winnerScore = 0;
            if (game.game.HomeTeam.Score < game.game.AwayTeam.Score)
            {
                loserID = homeID;
                loserScore = game.game.HomeTeam.Score;
                winnerID = awayID;
                winnerScore = game.game.AwayTeam.Score;
            }
            else
            {
                loserID = awayID;
                loserScore = game.game.AwayTeam.Score;
                winnerID = homeID;
                winnerScore = game.game.HomeTeam.Score;
            }
            int arenaID = game.game.Arena.ArenaId;
            int sellout = game.game.Sellout;
            //Game Table end 

            using (SqlCommand InsertData = new SqlCommand(procedure))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
                if(procedure == "playoffGameInsert")
                {
                    string series = game.game.GameId.Substring(0, 9) + "1-";
                    InsertData.Parameters.AddWithValue("@series_id", series);
                }
                InsertData.Parameters.AddWithValue("@id", season);
                InsertData.Parameters.AddWithValue("@game_id", game_id);
                InsertData.Parameters.AddWithValue("@date", date);
                InsertData.Parameters.AddWithValue("@team_idH", homeID);
                InsertData.Parameters.AddWithValue("@team_idA", awayID);
                InsertData.Parameters.AddWithValue("@team_idW", winnerID);
                InsertData.Parameters.AddWithValue("@wScore", winnerScore);
                InsertData.Parameters.AddWithValue("@team_idL", loserID);
                InsertData.Parameters.AddWithValue("@lScore", loserScore);
                InsertData.Parameters.AddWithValue("@arena_id", arenaID);
                InsertData.Parameters.AddWithValue("@sellout", sellout);
                busDriver.SQLdb.Open();
                InsertData.ExecuteScalar();
                busDriver.SQLdb.Close();
            }

            string newSender = "";
            if (procedure == "playoffGameInsert")
            {
                newSender = "OldPlayerBoxPlayoffsInsert";
            }
            else if (procedure == "gameInsert")
            {
                newSender = "OldPlayerBoxInsert";
            }


            SqlDateTime gameDate = SqlDateTime.Parse(date);                                             //Players
            for (int i = 0; i < game.game.HomeTeam.Players.Count; i++)
            {
                BoxPost(game, "home", i, homeID, season, newSender);
                int player = game.game.HomeTeam.Players[i].PersonId;
                //PlayerTeamCheck(game_id, player, homeID, gameDate, season);
            }
            for (int i = 0; i < game.game.AwayTeam.Players.Count; i++)
            {
                BoxPost(game, "away", i, awayID, season, newSender);
                int player = game.game.AwayTeam.Players[i].PersonId;
                //PlayerTeamCheck(game_id, player, awayID, gameDate, season);
            }
        }


        public static void BoxPost(GameData game, string sender, int i, int team_id, int season, string procedure)
        {
            int game_id = Int32.Parse(game.game.GameId);
            int player = 0;
            string status = "";
            string minutes = "";
            string minutesCalculated = "";

            int starter = 0;
            string position = "";

            int points = 0;
            int assists = 0;
            int rebounds = 0;
            int dRebounds = 0;
            int oRebounds = 0;
            int blocks = 0;
            int fga = 0;
            int fgm = 0;
            double fgp = 0;
            int fouls = 0;
            int fta = 0;
            int ftm = 0;
            double ftp = 0;
            int plusMinus = 0;
            int steals = 0;
            int fg3a = 0;
            int fg3m = 0;
            double fg3p = 0;
            int turnovers = 0;
            int fg2a = 0; 
            int fg2m = 0;

            double fg2p = 0;
            if(fg2a > 0)
            {
                fg2p = fg2m / fg2a;
            }
            
            if (sender == "home")
            {
                player = game.game.HomeTeam.Players[i].PersonId;
                status = "";
                minutes = "";
                if (game.game.HomeTeam.Players[i].Statistics.Minutes == "")
                {
                    status = "INACTIVE";
                    minutes = "PT00M00.00S";
                }
                else
                {
                    status = "Active";
                    minutes = "PT" + game.game.HomeTeam.Players[i].Statistics.Minutes.Split(':')[0] + "M" + game.game.HomeTeam.Players[i].Statistics.Minutes.Split(':')[0] + ".00S";
                }
                minutesCalculated = minutes.Substring(0, 5);

                starter = 0;
                position = "";
                if (game.game.HomeTeam.Players[i].Position != "")
                {
                    starter = 1;
                    position = game.game.HomeTeam.Players[i].Position;
                }

                points = game.game.HomeTeam.Players[i].Statistics.Points;
                assists = game.game.HomeTeam.Players[i].Statistics.Assists;
                rebounds = game.game.HomeTeam.Players[i].Statistics.ReboundsTotal;
                dRebounds = game.game.HomeTeam.Players[i].Statistics.ReboundsDefensive;
                oRebounds = game.game.HomeTeam.Players[i].Statistics.ReboundsOffensive;
                blocks = game.game.HomeTeam.Players[i].Statistics.Blocks;
                fga = game.game.HomeTeam.Players[i].Statistics.FieldGoalsAttempted;
                fgm = game.game.HomeTeam.Players[i].Statistics.FieldGoalsMade;
                fgp = game.game.HomeTeam.Players[i].Statistics.FieldGoalsPercentage;
                fouls = game.game.HomeTeam.Players[i].Statistics.FoulsPersonal;
                fta = game.game.HomeTeam.Players[i].Statistics.FreeThrowsAttempted;
                ftm = game.game.HomeTeam.Players[i].Statistics.FreeThrowsMade;
                ftp = game.game.HomeTeam.Players[i].Statistics.FreeThrowsPercentage;
                plusMinus = game.game.HomeTeam.Players[i].Statistics.PlusMinusPoints;
                steals = game.game.HomeTeam.Players[i].Statistics.Steals;
                fg3a = game.game.HomeTeam.Players[i].Statistics.ThreePointersAttempted;
                fg3m = game.game.HomeTeam.Players[i].Statistics.ThreePointersMade;
                fg3p = game.game.HomeTeam.Players[i].Statistics.ThreePointersPercentage;
                turnovers = game.game.HomeTeam.Players[i].Statistics.Turnovers;
                fg2a = game.game.HomeTeam.Players[i].Statistics.FieldGoalsAttempted - game.game.HomeTeam.Players[i].Statistics.ThreePointersAttempted;
                fg2m = game.game.HomeTeam.Players[i].Statistics.FieldGoalsMade - game.game.HomeTeam.Players[i].Statistics.ThreePointersMade;
                if (fg2a > 0)
                {
                    fg2p = fg2m / fg2a;
                }
            }
            if (sender != "home")
            {
                player = game.game.AwayTeam.Players[i].PersonId;
                status = "";
                minutes = "";
                if (game.game.AwayTeam.Players[i].Statistics.Minutes == "")
                {
                    status = "INACTIVE";
                    minutes = "PT00M00.00S";
                }
                else
                {
                    status = "Active";
                    minutes = "PT" + game.game.AwayTeam.Players[i].Statistics.Minutes.Split(':')[0] + "M" + game.game.AwayTeam.Players[i].Statistics.Minutes.Split(':')[0] + ".00S";
                }
                minutesCalculated = minutes.Substring(0, 5);

                starter = 0;
                position = "";
                if (game.game.AwayTeam.Players[i].Position != "")
                {
                    starter = 1;
                    position = game.game.AwayTeam.Players[i].Position;
                }

                points = game.game.AwayTeam.Players[i].Statistics.Points;
                assists = game.game.AwayTeam.Players[i].Statistics.Assists;
                rebounds = game.game.AwayTeam.Players[i].Statistics.ReboundsTotal;
                dRebounds = game.game.AwayTeam.Players[i].Statistics.ReboundsDefensive;
                oRebounds = game.game.AwayTeam.Players[i].Statistics.ReboundsOffensive;
                blocks = game.game.AwayTeam.Players[i].Statistics.Blocks;
                fga = game.game.AwayTeam.Players[i].Statistics.FieldGoalsAttempted;
                fgm = game.game.AwayTeam.Players[i].Statistics.FieldGoalsMade;
                fgp = game.game.AwayTeam.Players[i].Statistics.FieldGoalsPercentage;
                fouls = game.game.AwayTeam.Players[i].Statistics.FoulsPersonal;
                fta = game.game.AwayTeam.Players[i].Statistics.FreeThrowsAttempted;
                ftm = game.game.AwayTeam.Players[i].Statistics.FreeThrowsMade;
                ftp = game.game.AwayTeam.Players[i].Statistics.FreeThrowsPercentage;
                plusMinus = game.game.AwayTeam.Players[i].Statistics.PlusMinusPoints;
                steals = game.game.AwayTeam.Players[i].Statistics.Steals;
                fg3a = game.game.AwayTeam.Players[i].Statistics.ThreePointersAttempted;
                fg3m = game.game.AwayTeam.Players[i].Statistics.ThreePointersMade;
                fg3p = game.game.AwayTeam.Players[i].Statistics.ThreePointersPercentage;
                turnovers = game.game.AwayTeam.Players[i].Statistics.Turnovers;
                fg2a = game.game.AwayTeam.Players[i].Statistics.FieldGoalsAttempted - game.game.AwayTeam.Players[i].Statistics.ThreePointersAttempted;
                fg2m = game.game.AwayTeam.Players[i].Statistics.FieldGoalsMade - game.game.AwayTeam.Players[i].Statistics.ThreePointersMade;               
                if (fg2a > 0)
                {
                    fg2p = fg2m / fg2a;
                }
            }

            using (SqlCommand InsertData = new SqlCommand(procedure))
            {
                InsertData.CommandType = CommandType.StoredProcedure;
                if(procedure == "OldPlayerBoxPlayoffsInsert")
                {
                    string series = game.game.GameId.Substring(0, 9) + "1-";
                    InsertData.Parameters.AddWithValue("@series_id", series);
                    InsertData.Parameters.AddWithValue("@game", Int32.Parse(game.game.GameId.Substring(9)));

                }
                InsertData.Parameters.AddWithValue("@season_id", season);
                InsertData.Parameters.AddWithValue("@game_id", game_id);
                InsertData.Parameters.AddWithValue("@team_id", team_id);
                InsertData.Parameters.AddWithValue("@player_id", player);
                InsertData.Parameters.AddWithValue("@status", status);
                InsertData.Parameters.AddWithValue("@starter", starter);
                InsertData.Parameters.AddWithValue("@position", position);
                InsertData.Parameters.AddWithValue("@points", points);
                InsertData.Parameters.AddWithValue("@assists", assists);
                InsertData.Parameters.AddWithValue("@blocks", blocks);
                InsertData.Parameters.AddWithValue("@fieldGoalsAttempted", fga);
                InsertData.Parameters.AddWithValue("@fieldGoalsMade", fgm);
                InsertData.Parameters.AddWithValue("@fieldGoalsPercentage", fgp);
                InsertData.Parameters.AddWithValue("@foulsPersonal", fouls);
                InsertData.Parameters.AddWithValue("@freeThrowsAttempted", fta);
                InsertData.Parameters.AddWithValue("@freeThrowsMade", ftm);
                InsertData.Parameters.AddWithValue("@freeThrowsPercentage", ftp);
                InsertData.Parameters.AddWithValue("@minutes", minutes);
                InsertData.Parameters.AddWithValue("@minutesCalculated", minutesCalculated);
                InsertData.Parameters.AddWithValue("@plusMinusPoints", plusMinus);
                InsertData.Parameters.AddWithValue("@reboundsDefensive", dRebounds);
                InsertData.Parameters.AddWithValue("@reboundsOffensive", oRebounds);
                InsertData.Parameters.AddWithValue("@reboundsTotal", rebounds);
                InsertData.Parameters.AddWithValue("@steals", steals);
                InsertData.Parameters.AddWithValue("@threePointersAttempted", fg3a);
                InsertData.Parameters.AddWithValue("@threePointersMade", fg3m);
                InsertData.Parameters.AddWithValue("@threePointersPercentage", fg3p);
                InsertData.Parameters.AddWithValue("@turnovers", turnovers);
                InsertData.Parameters.AddWithValue("@twoPointersAttempted", fg2a);
                InsertData.Parameters.AddWithValue("@twoPointersMade", fg2m);
                InsertData.Parameters.AddWithValue("@twoPointersPercentage", fg2p);
                using (SqlDataAdapter sInsertData = new SqlDataAdapter())
                {
                    InsertData.Connection = busDriver.SQLdb;
                    sInsertData.SelectCommand = InsertData;
                    busDriver.SQLdb.Open();
                    InsertData.ExecuteScalar();
                    busDriver.SQLdb.Close();
                }
            }

        }









        public static void PlayerTeamCheck(int game, int player, int team, SqlDateTime start, int id)
        {
            using (SqlCommand PlayerSearch = new SqlCommand("PlayerTeamCheck"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@player", player);
                PlayerSearch.Parameters.AddWithValue("@team", team);
                PlayerSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    reader.Read();
                    if (!reader.HasRows)
                    {
                        busDriver.SQLdb.Close();
                        PlayerTeamCheckOtherTeams(game, player, team, start, id);
                    }
                    else
                    {
                        busDriver.SQLdb.Close();
                    }
                }
            }
        }


        public static void PlayerTeamCheckOtherTeams(int game, int player, int team, SqlDateTime start, int id)
        {
            using (SqlCommand PlayerSearch = new SqlCommand("PlayerTeamCheckOtherTeams"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@player", player);
                PlayerSearch.Parameters.AddWithValue("@team", team);
                PlayerSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        int oldTeam = Int32.Parse(reader[2].ToString());
                        busDriver.SQLdb.Close();
                        PlayerTeamUpdate(player, oldTeam, id);
                        PlayerTeamPost(game, player, team, start, id);
                    }
                    else
                    {
                        busDriver.SQLdb.Close();
                        PlayerTeamPost(game, player, team, start, id);
                    }
                }
            }
        }

        public static void PlayerTeamUpdate(int player, int team, int id)
        {
            SqlDateTime lastGame = SqlDateTime.MaxValue;
            using (SqlCommand PlayerSearch = new SqlCommand("PlayerTeamGetLastGame"))
            {
                PlayerSearch.CommandType = CommandType.StoredProcedure;
                PlayerSearch.Parameters.AddWithValue("@player", player);
                PlayerSearch.Parameters.AddWithValue("@team", team);
                PlayerSearch.Parameters.AddWithValue("@id", id);
                using (SqlDataAdapter sPlayerSearch = new SqlDataAdapter())
                {
                    PlayerSearch.Connection = busDriver.SQLdb;
                    sPlayerSearch.SelectCommand = PlayerSearch;
                    busDriver.SQLdb.Open();
                    SqlDataReader reader = PlayerSearch.ExecuteReader();
                    reader.Read();
                    lastGame = SqlDateTime.Parse(reader[0].ToString());
                    busDriver.SQLdb.Close();
                }
            }
            using (SqlCommand InsertData = new SqlCommand("PlayerTeamUpdateLastGame"))
            {
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@player", player);
                InsertData.Parameters.AddWithValue("@team", team);
                InsertData.Parameters.AddWithValue("@id", id);
                InsertData.Parameters.AddWithValue("@LastGame", lastGame);
                using (SqlDataAdapter sInsertData = new SqlDataAdapter())
                {
                    InsertData.Connection = busDriver.SQLdb;
                    sInsertData.SelectCommand = InsertData;
                    busDriver.SQLdb.Open();
                    InsertData.ExecuteScalar();
                    busDriver.SQLdb.Close();
                }
            }


        }

        public static void PlayerTeamPost(int game, int player, int team, SqlDateTime start, int id)
        {
            using (SqlCommand InsertData = new SqlCommand("PlayerTeamPost"))
            {
                InsertData.CommandType = CommandType.StoredProcedure;
                InsertData.Parameters.AddWithValue("@player", player);
                InsertData.Parameters.AddWithValue("@team", team);
                InsertData.Parameters.AddWithValue("@id", id);
                InsertData.Parameters.AddWithValue("@FirstGame", start);
                using (SqlDataAdapter sInsertData = new SqlDataAdapter())
                {
                    InsertData.Connection = busDriver.SQLdb;
                    sInsertData.SelectCommand = InsertData;
                    busDriver.SQLdb.Open();
                    InsertData.ExecuteScalar();
                    busDriver.SQLdb.Close();
                }
            }
        }








        public class PlayByPlayData
        {
            public PlayByPlay playByPlay { get; set; }
        }

        public class PlayByPlay
        {
            public string gameId { get; set; }
            public int videoAvailable { get; set; }
            public ActionItem[] actions { get; set; }
        }

        public class ActionItem
        {
            public int actionNumber { get; set; }
            public int? actionID { get; set; }
            public string? clock { get; set; }
            public int? period { get; set; }
            public int? teamId { get; set; }
            public string? teamTricode { get; set; }
            public int? personId { get; set; }
            public string? playerName { get; set; }
            public string? description { get; set; }
            public string? actionType { get; set; }
            public string? subType { get; set; }
            public float? xLegacy { get; set; }
            public float? yLegacy { get; set; }
            public float? shotDistance { get; set; }
            public string? shotResult { get; set; }
            public int? isFieldGoal { get; set; }
            public int? scoreHome { get; set; }
            public int? scoreAway { get; set; }
            public int? shotValue { get; set; }

        }




        public class GameData
        {
            public Game game { get; set; }
        }
        public class Game
        {
            public string GameId { get; set; }
            public string GameCode { get; set; }
            public int GameStatus { get; set; }
            public string GameStatusText { get; set; }
            public int Period { get; set; }
            public string GameClock { get; set; }
            public string GameTimeUTC { get; set; }
            public string GameEt { get; set; }
            public int AwayTeamId { get; set; }
            public int HomeTeamId { get; set; }
            public string Duration { get; set; }
            public int Attendance { get; set; }
            public int Sellout { get; set; }
            public Arena Arena { get; set; }
            public List<Official> Officials { get; set; }
            public Broadcasters Broadcasters { get; set; }
            public HomeTeam HomeTeam { get; set; }
            public AwayTeam AwayTeam { get; set; }
            public LastFiveMeetings LastFiveMeetings { get; set; }
        }

        public class Arena
        {
            public int ArenaId { get; set; }
            public string ArenaName { get; set; }
            public string ArenaCity { get; set; }
            public string ArenaState { get; set; }
            public string ArenaCountry { get; set; }
            public string ArenaTimezone { get; set; }
        }

        public class Official
        {
            public int PersonId { get; set; }
            public string Name { get; set; }
            public string NameI { get; set; }
            public string FirstName { get; set; }
            public string FamilyName { get; set; }
            public string JerseyNum { get; set; }
        }

        public class Broadcasters
        {
            public List<Broadcaster> HomeTvBroadcasters { get; set; }
            public List<Broadcaster> HomeRadioBroadcasters { get; set; }
            public List<Broadcaster> AwayTvBroadcasters { get; set; }
            public List<Broadcaster> AwayRadioBroadcasters { get; set; }
        }

        public class Broadcaster
        {
            public int BroadcasterId { get; set; }
            public string BroadcastDisplay { get; set; }
            public string BroadcasterDisplay { get; set; }
            public string BroadcasterVideoLink { get; set; }
        }

        public class HomeTeam
        {
            public int TeamId { get; set; }
            public string TeamName { get; set; }
            public string TeamCity { get; set; }
            public string TeamTricode { get; set; }
            public int TeamWins { get; set; }
            public int TeamLosses { get; set; }
            public int Score { get; set; }
            public List<PeriodScore> Periods { get; set; }
            public List<Player> Players { get; set; }
            public Statistics Statistics { get; set; }
        }

        public class AwayTeam
        {
            public int TeamId { get; set; }
            public string TeamName { get; set; }
            public string TeamCity { get; set; }
            public string TeamTricode { get; set; }
            public int TeamWins { get; set; }
            public int TeamLosses { get; set; }
            public int Score { get; set; }
            public List<PeriodScore> Periods { get; set; }
            public List<Player> Players { get; set; }
            public Statistics Statistics { get; set; }
        }

        public class PeriodScore
        {
            public int Period { get; set; }
            public string PeriodType { get; set; }
            public int Score { get; set; }
        }

        public class Player
        {
            public int PersonId { get; set; }
            public string FirstName { get; set; }
            public string FamilyName { get; set; }
            public string NameI { get; set; }
            public string PlayerSlug { get; set; }
            public string Position { get; set; }
            public string JerseyNum { get; set; }
            public PlayerStatistics Statistics { get; set; }
        }

        public class PlayerStatistics
        {
            public string Minutes { get; set; }
            public int FieldGoalsMade { get; set; }
            public int FieldGoalsAttempted { get; set; }
            public double FieldGoalsPercentage { get; set; }
            public int ThreePointersMade { get; set; }
            public int ThreePointersAttempted { get; set; }
            public double ThreePointersPercentage { get; set; }
            public int FreeThrowsMade { get; set; }
            public int FreeThrowsAttempted { get; set; }
            public double FreeThrowsPercentage { get; set; }
            public int ReboundsOffensive { get; set; }
            public int ReboundsDefensive { get; set; }
            public int ReboundsTotal { get; set; }
            public int Assists { get; set; }
            public int Steals { get; set; }
            public int Blocks { get; set; }
            public int Turnovers { get; set; }
            public int FoulsPersonal { get; set; }
            public int Points { get; set; }
            public int PlusMinusPoints { get; set; }
        }

        public class Statistics
        {
            public string Minutes { get; set; }
            public int FieldGoalsMade { get; set; }
            public int FieldGoalsAttempted { get; set; }
            public double FieldGoalsPercentage { get; set; }
            public int ThreePointersMade { get; set; }
            public int ThreePointersAttempted { get; set; }
            public double ThreePointersPercentage { get; set; }
            public int FreeThrowsMade { get; set; }
            public int FreeThrowsAttempted { get; set; }
            public double FreeThrowsPercentage { get; set; }
            public int ReboundsOffensive { get; set; }
            public int ReboundsDefensive { get; set; }
            public int ReboundsTotal { get; set; }
            public int Assists { get; set; }
            public int Steals { get; set; }
            public int Blocks { get; set; }
            public int Turnovers { get; set; }
            public int FoulsPersonal { get; set; }
            public int Points { get; set; }
            public int PlusMinusPoints { get; set; }
        }

        public class LastFiveMeetings
        {
            public List<Meeting> Meetings { get; set; }
        }

        public class Meeting
        {
            public int RecencyOrder { get; set; }
            public string GameId { get; set; }
            public string GameTimeUTC { get; set; }
            public string GameEt { get; set; }
            public int GameStatus { get; set; }
            public string GameStatusText { get; set; }
            public TeamScore AwayTeam { get; set; }
            public TeamScore HomeTeam { get; set; }
        }

        public class TeamScore
        {
            public int TeamId { get; set; }
            public string TeamCity { get; set; }
            public string TeamName { get; set; }
            public string TeamTricode { get; set; }
            public int Score { get; set; }
            public int Wins { get; set; }
            public int Losses { get; set; }
        }




    }
}