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


namespace NBAdbPre2019
{
    public partial class Pre2019
    {
        public static BusDriver busDriver = new BusDriver();
        public static List<string> games = new List<string>
        {
            "0021300001", "0021300002", "0021300003", "0021300004", "0021300005", "0021300006", "0021300007", "0021300008", "0021300009", "0021300010", "0021300011", "0021300012", "0021300013", "0021300014", "0021300015", "0021300016", "0021300017", "0021300018", "0021300019", "0021300020", "0021300021", "0021300022", "0021300023", "0021300024", "0021300025", "0021300026", "0021300027", "0021300028", "0021300029", "0021300030", "0021300031", "0021300032", "0021300033", "0021300034", "0021300035", "0021300036", "0021300037", "0021300038", "0021300039", "0021300040", "0021300041", "0021300042", "0021300043", "0021300044", "0021300045", "0021300046", "0021300047", "0021300048", "0021300049", "0021300050", "0021300051", "0021300052", "0021300053", "0021300054", "0021300055", "0021300056", "0021300057", "0021300058", "0021300059", "0021300060", "0021300061", "0021300062", "0021300063", "0021300064", "0021300065", "0021300066", "0021300067", "0021300068", "0021300069", "0021300070", "0021300071", "0021300072", "0021300073", "0021300074", "0021300075", "0021300076", "0021300077", "0021300078", "0021300079", "0021300080", "0021300081", "0021300082", "0021300083", "0021300084", "0021300085", "0021300086", "0021300087", "0021300088", "0021300089", "0021300090", "0021300091", "0021300092", "0021300093", "0021300094", "0021300095", "0021300096", "0021300097", "0021300098", "0021300099", "0021300100", "0021300101", "0021300102", "0021300103", "0021300104", "0021300105", "0021300106", "0021300107", "0021300108", "0021300109", "0021300110", "0021300111", "0021300112", "0021300113", "0021300114", "0021300115", "0021300116", "0021300117", "0021300118", "0021300119", "0021300120", "0021300121", "0021300122", "0021300123", "0021300124", "0021300125", "0021300126", "0021300127", "0021300128", "0021300129", "0021300130", "0021300131", "0021300132", "0021300133", "0021300134", "0021300135", "0021300136", "0021300137", "0021300138", "0021300139", "0021300140", "0021300141", "0021300142", "0021300143", "0021300144", "0021300145", "0021300146", "0021300147", "0021300148", "0021300149", "0021300150", "0021300151", "0021300152", "0021300153", "0021300154", "0021300155", "0021300156", "0021300157", "0021300158", "0021300159", "0021300160", "0021300161", "0021300162", "0021300163", "0021300164", "0021300165", "0021300166", "0021300167", "0021300168", "0021300169", "0021300170", "0021300171", "0021300172", "0021300173", "0021300174", "0021300175", "0021300176", "0021300177", "0021300178", "0021300179", "0021300180", "0021300181", "0021300182", "0021300183", "0021300184", "0021300185", "0021300186", "0021300187", "0021300188", "0021300189", "0021300190", "0021300191", "0021300192", "0021300193", "0021300194", "0021300195", "0021300196", "0021300197", "0021300198", "0021300199", "0021300200", "0021300201", "0021300202", "0021300203", "0021300204", "0021300205", "0021300206", "0021300207", "0021300208", "0021300209", "0021300210", "0021300211", "0021300212", "0021300213", "0021300214", "0021300215", "0021300216", "0021300217", "0021300218", "0021300219", "0021300220", "0021300221", "0021300222", "0021300223", "0021300224", "0021300225", "0021300226", "0021300227", "0021300228", "0021300229", "0021300230", "0021300231", "0021300232", "0021300233", "0021300234", "0021300235", "0021300236", "0021300237", "0021300238", "0021300239", "0021300240", "0021300241", "0021300242", "0021300243", "0021300244", "0021300245", "0021300246", "0021300247", "0021300248", "0021300249", "0021300250", "0021300251", "0021300252", "0021300253", "0021300254", "0021300255", "0021300256", "0021300257", "0021300258", "0021300259", "0021300260", "0021300261", "0021300262", "0021300263", "0021300264", "0021300265", "0021300266", "0021300267", "0021300268", "0021300269", "0021300270", "0021300271", "0021300272", "0021300273", "0021300274", "0021300275", "0021300276", "0021300277", "0021300278", "0021300279", "0021300280", "0021300281", "0021300282", "0021300283", "0021300284", "0021300285", "0021300286", "0021300287", "0021300288", "0021300289", "0021300290", "0021300291", "0021300292", "0021300293", "0021300294", "0021300295", "0021300296", "0021300297", "0021300298", "0021300299", "0021300300", "0021300301", "0021300302", "0021300303", "0021300304", "0021300305", "0021300306", "0021300307", "0021300308", "0021300309", "0021300310", "0021300311", "0021300312", "0021300313", "0021300314", "0021300315", "0021300316", "0021300317", "0021300318", "0021300319", "0021300320", "0021300321", "0021300322", "0021300323", "0021300324", "0021300325", "0021300326", "0021300327", "0021300328", "0021300329", "0021300330", "0021300331", "0021300332", "0021300333", "0021300334", "0021300335", "0021300336", "0021300337", "0021300338", "0021300339", "0021300340", "0021300341", "0021300342", "0021300343", "0021300344", "0021300345", "0021300346", "0021300347", "0021300348", "0021300349", "0021300350", "0021300351", "0021300352", "0021300353", "0021300354", "0021300355", "0021300356", "0021300357", "0021300358", "0021300359", "0021300360", "0021300361", "0021300362", "0021300363", "0021300364", "0021300365", "0021300366", "0021300367", "0021300368", "0021300369", "0021300370", "0021300371", "0021300372", "0021300373", "0021300374", "0021300375", "0021300376", "0021300377", "0021300378", "0021300379", "0021300380", "0021300381", "0021300382", "0021300383", "0021300384", "0021300385", "0021300386", "0021300387", "0021300388", "0021300389", "0021300390", "0021300391", "0021300392", "0021300393", "0021300394", "0021300395", "0021300396", "0021300397", "0021300398", "0021300399", "0021300400", "0021300401", "0021300402", "0021300403", "0021300404", "0021300405", "0021300406", "0021300407", "0021300408", "0021300409", "0021300410", "0021300411", "0021300412", "0021300413", "0021300414", "0021300415", "0021300416", "0021300417", "0021300418", "0021300419", "0021300420", "0021300421", "0021300422", "0021300423", "0021300424", "0021300425", "0021300426", "0021300427", "0021300428", "0021300429", "0021300430", "0021300431", "0021300432", "0021300433", "0021300434", "0021300435", "0021300436", "0021300437", "0021300438", "0021300439", "0021300440", "0021300441", "0021300442", "0021300443", "0021300444", "0021300445", "0021300446", "0021300447", "0021300448", "0021300449", "0021300450", "0021300451", "0021300452", "0021300453", "0021300454", "0021300455", "0021300456", "0021300457", "0021300458", "0021300459", "0021300460", "0021300461", "0021300462", "0021300463", "0021300464", "0021300465", "0021300466", "0021300467", "0021300468", "0021300469", "0021300470", "0021300471", "0021300472", "0021300473", "0021300474", "0021300475", "0021300476", "0021300477", "0021300478", "0021300479", "0021300480", "0021300481", "0021300482", "0021300483", "0021300484", "0021300485", "0021300486", "0021300487", "0021300488", "0021300489", "0021300490", "0021300491", "0021300492", "0021300493", "0021300494", "0021300495", "0021300496", "0021300497", "0021300498", "0021300499", "0021300500", "0021300501", "0021300502", "0021300503", "0021300504", "0021300505", "0021300506", "0021300507", "0021300508", "0021300509", "0021300510", "0021300511", "0021300512", "0021300513", "0021300514", "0021300515", "0021300516", "0021300517", "0021300518", "0021300519", "0021300520", "0021300521", "0021300522", "0021300523", "0021300524", "0021300525", "0021300526", "0021300527", "0021300528", "0021300529", "0021300530", "0021300531", "0021300532", "0021300533", "0021300534", "0021300535", "0021300536", "0021300537", "0021300538", "0021300539", "0021300540", "0021300541", "0021300542", "0021300543", "0021300544", "0021300545", "0021300546", "0021300547", "0021300548", "0021300549", "0021300550", "0021300551", "0021300552", "0021300553", "0021300554", "0021300555", "0021300556", "0021300557", "0021300558", "0021300559", "0021300560", "0021300561", "0021300562", "0021300563", "0021300564", "0021300565", "0021300566", "0021300567", "0021300568", "0021300569", "0021300570", "0021300571", "0021300572", "0021300573", "0021300574", "0021300575", "0021300576", "0021300577", "0021300578", "0021300579", "0021300580", "0021300581", "0021300582", "0021300583", "0021300584", "0021300585", "0021300586", "0021300587", "0021300588", "0021300589", "0021300590", "0021300591", "0021300592", "0021300593", "0021300594", "0021300595", "0021300596", "0021300597", "0021300598", "0021300599", "0021300600", "0021300601", "0021300602", "0021300603", "0021300604", "0021300605", "0021300606", "0021300607", "0021300608", "0021300609", "0021300610", "0021300611", "0021300612", "0021300613", "0021300614", "0021300615", "0021300616", "0021300617", "0021300618", "0021300619", "0021300620", "0021300621", "0021300622", "0021300623", "0021300624", "0021300625", "0021300626", "0021300627", "0021300628", "0021300629", "0021300630", "0021300631", "0021300632", "0021300633", "0021300634", "0021300635", "0021300636", "0021300637", "0021300638", "0021300639", "0021300640", "0021300641", "0021300642", "0021300643", "0021300644", "0021300645", "0021300646", "0021300647", "0021300648", "0021300649", "0021300650", "0021300651", "0021300652", "0021300653", "0021300654", "0021300655", "0021300656", "0021300657", "0021300658", "0021300659", "0021300660", "0021300661", "0021300662", "0021300663", "0021300664", "0021300665", "0021300666", "0021300667", "0021300668", "0021300669", "0021300670", "0021300671", "0021300672", "0021300673", "0021300674", "0021300675", "0021300676", "0021300677", "0021300678", "0021300679", "0021300680", "0021300681", "0021300682", "0021300683", "0021300684", "0021300685", "0021300686", "0021300687", "0021300688", "0021300689", "0021300690", "0021300691", "0021300692", "0021300693", "0021300694", "0021300695", "0021300696", "0021300697", "0021300698", "0021300699", "0021300700", "0021300701", "0021300702", "0021300703", "0021300704", "0021300705", "0021300706", "0021300707", "0021300708", "0021300709", "0021300710", "0021300711", "0021300712", "0021300713", "0021300714", "0021300715", "0021300716", "0021300717", "0021300718", "0021300719", "0021300720", "0021300721", "0021300722", "0021300723", "0021300724", "0021300725", "0021300726", "0021300727", "0021300728", "0021300729", "0021300730", "0021300731", "0021300732", "0021300733", "0021300734", "0021300735", "0021300736", "0021300737", "0021300738", "0021300739", "0021300740", "0021300741", "0021300742", "0021300743", "0021300744", "0021300745", "0021300746", "0021300747", "0021300748", "0021300749", "0021300750", "0021300751", "0021300752", "0021300753", "0021300754", "0021300755", "0021300756", "0021300757", "0021300758", "0021300759", "0021300760", "0021300761", "0021300762", "0021300763", "0021300764", "0021300765", "0021300766", "0021300767", "0021300768", "0021300769", "0021300770", "0021300771", "0021300772", "0021300773", "0021300774", "0021300775", "0021300776", "0021300777", "0021300778", "0021300779", "0021300780", "0021300781", "0021300782", "0021300783", "0021300784", "0021300785", "0021300786", "0021300787", "0021300788", "0021300789", "0021300790", "0021300791", "0021300792", "0021300793", "0021300794", "0021300795", "0021300796", "0021300797", "0021300798", "0021300799", "0021300800", "0021300801", "0021300802", "0021300803", "0021300804", "0021300805", "0021300806", "0021300807", "0021300808", "0021300809", "0021300810", "0021300811", "0021300812", "0021300813", "0021300814", "0021300815", "0021300816", "0021300817", "0021300818", "0021300819", "0021300820", "0021300821", "0021300822", "0021300823", "0021300824", "0021300825", "0021300826", "0021300827", "0021300828", "0021300829", "0021300830", "0021300831", "0021300832", "0021300833", "0021300834", "0021300835", "0021300836", "0021300837", "0021300838", "0021300839", "0021300840", "0021300841", "0021300842", "0021300843", "0021300844", "0021300845", "0021300846", "0021300847", "0021300848", "0021300849", "0021300850", "0021300851", "0021300852", "0021300853", "0021300854", "0021300855", "0021300856", "0021300857", "0021300858", "0021300859", "0021300860", "0021300861", "0021300862", "0021300863", "0021300864", "0021300865", "0021300866", "0021300867", "0021300868", "0021300869", "0021300870", "0021300871", "0021300872", "0021300873", "0021300874", "0021300875", "0021300876", "0021300877", "0021300878", "0021300879", "0021300880", "0021300881", "0021300882", "0021300883", "0021300884", "0021300885", "0021300886", "0021300887", "0021300888", "0021300889", "0021300890", "0021300891", "0021300892", "0021300893", "0021300894", "0021300895", "0021300896", "0021300897", "0021300898", "0021300899", "0021300900", "0021300901", "0021300902", "0021300903", "0021300904", "0021300905", "0021300906", "0021300907", "0021300908", "0021300909", "0021300910", "0021300911", "0021300912", "0021300913", "0021300914", "0021300915", "0021300916", "0021300917", "0021300918", "0021300919", "0021300920", "0021300921", "0021300922", "0021300923", "0021300924", "0021300925", "0021300926", "0021300927", "0021300928", "0021300929", "0021300930", "0021300931", "0021300932", "0021300933", "0021300934", "0021300935", "0021300936", "0021300937", "0021300938", "0021300939", "0021300940", "0021300941", "0021300942", "0021300943", "0021300944", "0021300945", "0021300946", "0021300947", "0021300948", "0021300949", "0021300950", "0021300951", "0021300952", "0021300953", "0021300954", "0021300955", "0021300956", "0021300957", "0021300958", "0021300959", "0021300960", "0021300961", "0021300962", "0021300963", "0021300964", "0021300965", "0021300966", "0021300967", "0021300968", "0021300969", "0021300970", "0021300971", "0021300972", "0021300973", "0021300974", "0021300975", "0021300976", "0021300977", "0021300978", "0021300979", "0021300980", "0021300981", "0021300982", "0021300983", "0021300984", "0021300985", "0021300986", "0021300987", "0021300988", "0021300989", "0021300990", "0021300991", "0021300992", "0021300993", "0021300994", "0021300995", "0021300996", "0021300997", "0021300998", "0021300999", "0021301000", "0021301001", "0021301002", "0021301003", "0021301004", "0021301005", "0021301006", "0021301007", "0021301008", "0021301009", "0021301010", "0021301011", "0021301012", "0021301013", "0021301014", "0021301015", "0021301016", "0021301017", "0021301018", "0021301019", "0021301020", "0021301021", "0021301022", "0021301023", "0021301024", "0021301025", "0021301026", "0021301027", "0021301028", "0021301029", "0021301030", "0021301031", "0021301032", "0021301033", "0021301034", "0021301035", "0021301036", "0021301037", "0021301038", "0021301039", "0021301040", "0021301041", "0021301042", "0021301043", "0021301044", "0021301045", "0021301046", "0021301047", "0021301048", "0021301049", "0021301050", "0021301051", "0021301052", "0021301053", "0021301054", "0021301055", "0021301056", "0021301057", "0021301058", "0021301059", "0021301060", "0021301061", "0021301062", "0021301063", "0021301064", "0021301065", "0021301066", "0021301067", "0021301068", "0021301069", "0021301070", "0021301071", "0021301072", "0021301073", "0021301074", "0021301075", "0021301076", "0021301077", "0021301078", "0021301079", "0021301080", "0021301081", "0021301082", "0021301083", "0021301084", "0021301085", "0021301086", "0021301087", "0021301088", "0021301089", "0021301090", "0021301091", "0021301092", "0021301093", "0021301094", "0021301095", "0021301096", "0021301097", "0021301098", "0021301099", "0021301100", "0021301101", "0021301102", "0021301103", "0021301104", "0021301105", "0021301106", "0021301107", "0021301108", "0021301109", "0021301110", "0021301111", "0021301112", "0021301113", "0021301114", "0021301115", "0021301116", "0021301117", "0021301118", "0021301119", "0021301120", "0021301121", "0021301122", "0021301123", "0021301124", "0021301125", "0021301126", "0021301127", "0021301128", "0021301129", "0021301130", "0021301131", "0021301132", "0021301133", "0021301134", "0021301135", "0021301136", "0021301137", "0021301138", "0021301139", "0021301140", "0021301141", "0021301142", "0021301143", "0021301144", "0021301145", "0021301146", "0021301147", "0021301148", "0021301149", "0021301150", "0021301151", "0021301152", "0021301153", "0021301154", "0021301155", "0021301156", "0021301157", "0021301158", "0021301159", "0021301160", "0021301161", "0021301162", "0021301163", "0021301164", "0021301165", "0021301166", "0021301167", "0021301168", "0021301169", "0021301170", "0021301171", "0021301172", "0021301173", "0021301174", "0021301175", "0021301176", "0021301177", "0021301178", "0021301179", "0021301180", "0021301181", "0021301182", "0021301183", "0021301184", "0021301185", "0021301186", "0021301187", "0021301188", "0021301189", "0021301190", "0021301191", "0021301192", "0021301193", "0021301194", "0021301195", "0021301196", "0021301197", "0021301198", "0021301199", "0021301200", "0021301201", "0021301202", "0021301203", "0021301204", "0021301205", "0021301206", "0021301207", "0021301208", "0021301209", "0021301210", "0021301211", "0021301212", "0021301213", "0021301214", "0021301215", "0021301216", "0021301217", "0021301218", "0021301219", "0021301220", "0021301221", "0021301222", "0021301223", "0021301224", "0021301225", "0021301226", "0021301227", "0021301228", 
            "0021301229", "0021301230"
        };




        public static void Go()
        {
            //WriteFile();
            for (int i = 0; i < games.Count; i++)
            {
                string season = "20" + games[i].Substring(3, 2);
                string filePath = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\" + season + "\\" + games[i] + ".txt";
                //Full File
                string jsonData = File.ReadAllText(filePath).TrimStart().TrimEnd();




                //Game and Box
                string box = jsonData.Substring(jsonData.IndexOf("\"game\": {"));
                int boxEnd = box.IndexOf("\"playByPlay");
                box = box.Substring(0, boxEnd); ;
                box = box.TrimStart().TrimEnd();
                string boxFormatted = "{" + box + "}";
                boxFormatted = boxFormatted.Replace("},}", "}}");
                boxFormatted = JToken.Parse(boxFormatted).ToString(Formatting.None);
                string boxOutput = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\" + season + "\\box\\" + games[i] + ".json";

                // Write the minified JSON back to a file
                File.WriteAllText(boxOutput, boxFormatted);
                GetDataBox(boxOutput, season);







                //PlayByPlay
                string pbp = jsonData.Substring(jsonData.IndexOf("playByPlay"));
                int pbpEnd = pbp.IndexOf("\"source\": \"hanaV3\"");
                pbp = pbp.Substring(0, pbpEnd);
                pbp = pbp.Replace("],", "]}");
                pbp = pbp.TrimStart().TrimEnd();
                string pbpFormatted = "{\"" + pbp + "}";
                pbpFormatted = JToken.Parse(pbpFormatted).ToString(Formatting.None);
                string pbpOutput = @"C:\Users\derfj\Desktop\NBAdb\NBAdb\Old Data\" + season + "\\pbp\\" + games[i] + ".json";
                // Write the minified JSON back to a file
                File.WriteAllText(pbpOutput, pbpFormatted);
                GetDataPBP(pbpOutput);
            }
        }




        public static void GetDataPBP(string file)
        {
            string jsonData = File.ReadAllText(file);
            PlayByPlayData playByPlayData = JsonConvert.DeserializeObject<PlayByPlayData>(jsonData);
            PlayByPlay pbp = JsonConvert.DeserializeObject<PlayByPlay>(jsonData);
            for (int i = 0; i < playByPlayData.playByPlay.actions.Count(); i++)
            {
                using (SqlCommand querySearch = new SqlCommand("InsertOldPlayByPlayData"))
                {
                    querySearch.Connection = busDriver.SQLdb;
                    querySearch.CommandType = CommandType.StoredProcedure;
                    //if (procedure == "playByPlayPlayoffsInsert")
                    //{
                    //    querySearch.Parameters.AddWithValue("@season_id", id);
                    //    querySearch.Parameters.AddWithValue("@series_id", dynamicVariable);
                    //    querySearch.Parameters.AddWithValue("@game_id", game_id);
                    //    querySearch.Parameters.AddWithValue("@game", Int32.Parse(game_id.ToString().Substring(7)));
                    //}

                    querySearch.Parameters.AddWithValue("@season_id", Int32.Parse("20" + playByPlayData.playByPlay.gameId.Replace("002", "").Substring(0, 2)));
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




        public static void GetDataBox(string file, string seasonString)
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
            int sellout = game.game.Sellout;                                                            //Game Table end 

            using (SqlCommand InsertData = new SqlCommand("gameInsert"))
            {
                InsertData.Connection = busDriver.SQLdb;
                InsertData.CommandType = CommandType.StoredProcedure;
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



            SqlDateTime gameDate = SqlDateTime.Parse(date);                                             //Players
            for (int i = 0; i < game.game.HomeTeam.Players.Count; i++)
            {
                BoxPost(game, "home", i, homeID, season);
                int player = game.game.HomeTeam.Players[i].PersonId;
                //PlayerTeamCheck(game_id, player, homeID, gameDate, season);
            }
            for (int i = 0; i < game.game.AwayTeam.Players.Count; i++)
            {
                BoxPost(game, "away", i, awayID, season);
                int player = game.game.AwayTeam.Players[i].PersonId;
                //PlayerTeamCheck(game_id, player, awayID, gameDate, season);
            }
        }


        public static void BoxPost(GameData game, string sender, int i, int team_id, int season)
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

            using (SqlCommand InsertData = new SqlCommand("OldPlayerBoxInsert"))
            {
                InsertData.CommandType = CommandType.StoredProcedure;
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