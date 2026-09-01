using System;
using System.Linq;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary;
using DataAcess;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;
using System.Drawing;
using DevExpress.Web.Design.Converters;

public class VNN_VariablePublic
{
    public class Company
    {
        public string icon { get; set; }
        public string label { get; set; }
        public bool? expanded { get; set; }
        public bool? selected { get; set; }
        public List<DeparmentInCompany> items { get; set; }
    }

    public class DeparmentInCompany
    {
        public string icon { get; set; }
        public string label { get; set; }
        public bool? expanded { get; set; }
        public bool? selected { get; set; }
        public List<UserInDeparment> items { get; set; }
    }

    public class UserInDeparment
    {
        public string icon { get; set; }
        public string label { get; set; }
        public string value { get; set; }
        public bool? expanded { get; set; }
        public bool? selected { get; set; }
        public bool? @checked { get; set; }
    }
    public static string[] session_bd = new string[300];
    public static string Model_infor = "";
    public static string Form_infor = "";
    public static string auto_update_ad_selectoption = "";
    public static bool linqtosql_exec = false;
    public static int error_detail = 0;
    public static string error_content = "";
    public static bool view_origination = false;
    public static string format_so = "";
    public static Worksheet ws = null;
    public static string API_AncoTrading = System.Web.Configuration.WebConfigurationManager.AppSettings["API_AncoTrading"];
    public static string[] Module_script(ADmin_JSON json)
    {
        string[] kq = new string[4];
        List<ad_module> modules = json.ad_moduleJSON();
        var systemconfig = json.ad_systemconfigJSON().FirstOrDefault();
        modules = modules.Where(s => s.hoatdong == true & (s.thuake == null | s.thuake == "")).ToList();
        foreach (ad_module mod in modules)
        {
            string src_text = string.Format("js/Module_script/" + mod.ma_module + ".js");
            try
            {
                string src_File = ExcuteSignalRStatic.mapPathSignalR("~/" + src_text);
                if (File.Exists(src_File))
                {
                    string src_link = string.Format(Security.UrlBase() + src_text + "?ver=" + systemconfig.fax);

                    kq[0] += string.Format(@"<script async src=""{0}"" type=""text/javascript""></script>", src_link);
                    kq[3] += string.Format(@"loadScriptUrl('{0}','{1}');{2}", src_link, src_text, Environment.NewLine);

                    kq[1] += "<input type='hidden' id=\"org_grid" + mod.ma_module + "\"/>";
                    kq[2] += $@"var id_{mod.ma_module} = 0;";
                    kq[2] += $@"var filter_{mod.ma_module} = null, filterVal_{mod.ma_module} = null;";
                    kq[2] += $@"var page_{mod.ma_module} = null;";
                    kq[2] += $@"var sidx_{mod.ma_module} = null;";
                    kq[2] += $@"var sord_{mod.ma_module} = null;";
                    kq[2] += $@"let header_{mod.ma_module} = null;";
                }
            }
            catch { }
        }

        var d = new DirectoryInfo(ExcuteSignalRStatic.mapPathSignalR("~/js/Custom_Grid_script"));
        FileInfo[] Files = d.GetFiles("*.js");
        foreach (FileInfo file in Files)
        {
            string srcD_text = $@"js/Custom_Grid_script/{file.Name}";
            string srcD_link = $@"{Security.UrlBase()}{srcD_text}?ver={Guid.NewGuid()}";

            kq[0] += $@"<script src=""{srcD_link}"" type=""text/javascript""></script>";
            kq[3] += $@"loadScriptUrl('{srcD_link}','{srcD_text}');{Environment.NewLine}";
        }

        return kq;
    }

    public static string Serialize(object o)
    {
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(o);
    }

    public static string connectString_Anco(EntityContext db)
    {
        ad_systemconfig ttc = db.ad_systemconfig.FirstOrDefault();
        return ttc.connectstring_anco;
    }

    public static string sochungtu(EntityContext db, string ma_sochungtu, int congthem = 1, bool laynam = true)
    {
        var sctServer = db.md_sochungtu.Where(s => s.ma_sochungtu == ma_sochungtu & s.md_trangthai_id == Helper.HIEULUC).Take(1).FirstOrDefault();
        var sct = db.md_sochungtu.Local.Where(s => s.ma_sochungtu == ma_sochungtu & s.md_trangthai_id == Helper.HIEULUC).Take(1).FirstOrDefault();
        // get Year from md_modongky
        //md_modongky mdk = db.md_modongky.Where(s => s.hoatdong == true).Take(1).OrderByDescending(s => s.nam & s.ky).FirstOrDefault();
        string nam = DateTime.Now.Year.ToString().Substring(2, 2);
        if (laynam)
        {
            if (sct.namnay != DateTime.Now.Year)
            {
                throw new ArgumentNullException("Kế toán cần cập nhật số chứng từ theo năm " + DateTime.Now.Year);
            }
        }

        string gttd = sct.giatri_thaydoi;
        int count_gttd = gttd.Length;
        congthem = congthem * sct.buocnhay.Value;
        string gtcd = VNN_Config.load_number((int.Parse(gttd) + congthem).ToString(), count_gttd);
        string km = sct.khuonmau.Replace(gttd, gtcd);
        sct.giatri_thaydoi = gtcd;
        sct.khuonmau = km;
        if(laynam)
            km = km + "/" + sct.namnay.Value.ToString().Substring(2, 2);
        return km;
    }

    public static string sochungtu_(EntityContext db, string ma_sochungtu, int trura)
    {
        var sct = db.md_sochungtu.Where(s => s.ma_sochungtu == ma_sochungtu & s.md_trangthai_id == "HIEULUC").Take(1).FirstOrDefault();
        string gttd = sct.giatri_thaydoi;
        int count_gttd = gttd.Length;
        trura = trura * sct.buocnhay.Value;
        string gtcd = VNN_Config.load_number((int.Parse(gttd) - trura).ToString(), count_gttd);
        string km = sct.khuonmau.Replace(gttd, gtcd);
        sct.giatri_thaydoi = gtcd;
        sct.khuonmau = km;
        return km;
    }

    public static string get_mauhienthi_sochungtu(EntityContext db)
    {
        string msg = "if(1 == 2) { }";
        foreach (var sct in db.md_sochungtu.Where(s => s.md_trangthai_id == "HIEULUC").ToList())
        {
            msg += "\nelse if(ma_sct == '" + sct.ma_sochungtu + "'){";
            msg += "	kq = '" + sct.mau_hienthi + "';";
            msg += "\n}";
        }
        return msg;
    }

    public static string autoRound(decimal number, int round)
    {
        string s = Math.Round(number, round).ToString();
        decimal atr = 0;
        if (s.Contains("."))
        {
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '0')
                {
                    s = s.Remove(i);
                }
                else if (s[i] == '.')
                {
                    s = s.Remove(i);
                    break;
                }
                else
                {
                    break;
                }
            }
            atr = decimal.Parse(s);
        }
        else
        {
            atr = number;
        }
        int daucham = s.LastIndexOf(".");
        string s_ = s;
        if (daucham > 0)
            s.Substring(0, daucham);
        for (int i = s_.Length - 3; i > 0; i -= 3)
        {
            s = s.Insert(i, ",");
        }
        return s;
    }

    public static string DecodeHTML(string text)
    {
        try { text = text.Replace("0ψ0", "<").Replace("1Ψ1", ">"); }
        catch { }
        return text;
    }

    public static string EncodeHTML(string text)
    {
        try { text = text.Replace("<", "0ψ0").Replace(">", "1Ψ1"); }
        catch { }
        return text;
    }

    public static string GetModule(string link, Dictionary<string, object> jsonData)
    {
        var myUri = new Uri(link);
        string jsonString = JsonConvert.SerializeObject(jsonData);
        var request = WebRequest.Create(myUri);
        request.ContentType = "application/x-www-form-urlencoded";
        request.Method = "POST";
        string postData = string.Format(@"data={0}", jsonString);
        Stream reqStream = request.GetRequestStream();
        byte[] postArray = Encoding.UTF8.GetBytes(postData);
        reqStream.Write(postArray, 0, postArray.Length);
        reqStream.Close();
        var sr = new StreamReader(request.GetResponse().GetResponseStream());
        string result = sr.ReadToEnd();
        return result;
    }

    public static string GetModule(string link, string formData)
    {
        var context = System.Web.HttpContext.Current;
        var url = context.Request.Url;
        if (!link.Contains("http"))
        {
            link = url.GetLeftPart(UriPartial.Authority) + Security.UrlBase() + link;
        }
        var myUri = new Uri(link);
        string jsonString = JsonConvert.SerializeObject(formData);
        var request = (HttpWebRequest)WebRequest.Create(myUri);
        request.ContentType = "application/x-www-form-urlencoded";
        request.Method = "POST";
        //var iso = Encoding.GetEncoding("iso-8859-9");
        //var value = System.Web.HttpUtility.UrlEncode(context.User.Identity.Name, iso);
        //var moCookie = new Cookie("ANCO2_LOGIN", value);
        //moCookie.Domain = url.Host;
        //var CommCookie = new CookieContainer();
        //CommCookie.Add(moCookie);
        //request.CookieContainer = CommCookie;

        request.CookieContainer = new CookieContainer();
        var authCookie = context.Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName];
        request.CookieContainer.Add(new System.Net.Cookie(authCookie.Name, authCookie.Value, authCookie.Path, url.Host));
        string postData = formData;
        Stream reqStream = request.GetRequestStream();
        byte[] postArray = Encoding.UTF8.GetBytes(postData);
        reqStream.Write(postArray, 0, postArray.Length);
        reqStream.Close();
        var sr = new StreamReader(request.GetResponse().GetResponseStream());
        string result = sr.ReadToEnd();
        return result;
    }

    public static float GetWidthOfString(string str, string font, float? sizeF, bool? Bold)
    {
        Bitmap objBitmap = default(Bitmap);
        Graphics objGraphics = default(Graphics);

        objBitmap = new Bitmap(500, 200);
        objGraphics = Graphics.FromImage(objBitmap);
        var fontF = new Font(
                font == null ? "Arial" : font
                , sizeF == null ? 12 : sizeF.Value
                , Bold == null ? FontStyle.Regular : FontStyle.Bold);
        SizeF stringSize = objGraphics.MeasureString(str,
            fontF);

        objBitmap.Dispose();
        objGraphics.Dispose();
        return stringSize.Width;
    }
}

public class Module_TK
{
    public string ad_module_id;
    public string ma_module;
    public string ma_moduletk;
    public string ten_module;
    public string select_sql;
    public string from_sql;
    public string where_sql;
    public string orderby_sql;
    public string groupby_sql;
    public string procedure_sql;
    public int capmodule;
    public string ma_modulecha;
    public string url;
    public bool row_count;
    public string loai_module;
}

public class User_TK
{
    public string ad_user_id { get; set; }
    public string ad_role_id { get; set; }
    public string md_phongban_id { get; set; }
    public string chinhanh { get; set; }
    public string ma_user { get; set; }
    public string ma_nhanvien { get; set; }
    public string hoten { get; set; }
    public string ma_role { get; set; }
    public string ten_role { get; set; }
    public string ten_phongban { get; set; }
    public string ma_phongban { get; set; }
    public string mauBackground { get; set; }
    public bool? chuyenCachInBTSangPDF { get; set; }
    public bool? tuDongNhanDienCachIn { get; set; }
    public bool? btnDongMenuTuDong { get; set; }
    public bool? btnDongMenuConTuDong { get; set; }
    public string user_role { get; set; }
    public string user_part { get; set; }
    public string email { get; set; }
    public string email_pass { get; set; }
    public bool? googleAuthenticator { get; set; }
}

