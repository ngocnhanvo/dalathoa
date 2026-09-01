using DataAcess;
using DevExpress.Printing.Core.PdfExport.Metafile;
using Newtonsoft.Json;
using System;
using System.Linq;
public partial class MainPage : System.Web.UI.Page
{
    public User_TK tk = null;
    public EntityContext db = new EntityContext();
    public ADmin_JSON json = new ADmin_JSON();
    public ad_systemconfig ttc = null;
    public string[] kq_script;
    public string date_ip = "";
    public string layoutSize = "[]";
    public int version = 19;
    public bool? mixmode = false;
    public string border = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        var chk = Helper.checkBrower(Context);
        if (!chk.ok)
        {
            Response.Redirect(Security.UrlBase() + "Login.aspx");
        }
        else
        {

            tk = VNN_Function.get_user(
                Security.id_taikhoan(Context),
                Security.id_vaitro(Context),
                Security.id_phongban(Context),
                db
            );

            var conf = JsonConvert.DeserializeObject<dynamic>(tk.mauBackground);
            mixmode = conf.mixmode;
            border = conf.border;

            kq_script = VNN_VariablePublic.Module_script(json);
            ttc = json.ad_systemconfigJSON().FirstOrDefault();
            if (ttc.date_import != null)
            {
                date_ip = ttc.date_import.Value.ToString(VNN_Config.get_FormatDate());
            }

            try
            {
                var filepath = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/CustomGrid/" + tk.ma_user + "/layoutSize.json");
                if (System.IO.File.Exists(filepath))
                {
                    layoutSize = System.IO.File.ReadAllText(filepath);
                }
            }
            catch { }
        }
    }
}
