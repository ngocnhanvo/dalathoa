using System;
using System.Linq;
using System.IO;
using System.Web;
using System.Text;
using DataAcess;
/// <summary>
/// Summary description for CreateBasicFile
/// </summary>
public class Admin_CreateBasicFile
{
    public static string CreateFileLoad_Modify(HttpContext context, Module_TK mod, int capmodule)
    {
        string kq = "";

        string sql = $@"Select {mod.select_sql} from {mod.from_sql} where 1=1 {mod.where_sql}";
        if (!string.IsNullOrWhiteSpace(mod.orderby_sql))
            sql += $@" order by {mod.orderby_sql}";
        if (VNN_Function.TestSQL(sql) == false & (mod.loai_module == "JQG" | mod.loai_module == "JQGS"))
        {
            kq = $@"{Environment.NewLine}Không thể tạo Grid do cú pháp SQL chưa chính xác.{Environment.NewLine}Vào module: ""{mod.ma_module}"" để sửa lại câu lệnh sql.";
        }
        else
        {
            //Tạo file load Controller
            string filepath = ExcuteSignalRStatic.mapPathSignalR($"~/Controller/JQGrid");
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            filepath = $"{filepath}/JQGrid{mod.ma_module}Load.ashx";
            if (!File.Exists(filepath))
            {
                string id_parent = "''";
                if (mod.ma_modulecha != null & mod.ma_modulecha != "")
                {
                    EntityContext db = new EntityContext();
                    string mod_cha = db.ad_module.Where(s => s.ad_module_id == mod.ma_modulecha).Select(s => s.ma_module).Take(1).FirstOrDefault();
                    id_parent = "id_" + mod_cha;
                }

                var temp = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/TempCode/JQGridLoad.ashx");
                var contentTemp = File.ReadAllText(temp, Encoding.Unicode);
                var content = contentTemp.Replace("JQGrid__________Load", $"JQGrid{mod.ma_module}Load");
                File.WriteAllText(filepath, content, Encoding.Unicode);
                CreateFileModify(context, mod);
                CreateFileInterface(context, mod, capmodule);
                CreateFunction_ModuleScript(context, mod);
                kq = "\n Tạo grid thành công.";
            }
        }
        return kq;
    }

    public static void CreateFileModify(HttpContext context, Module_TK mod)
    {
        //Tạo file modify Controller
        string filepath = ExcuteSignalRStatic.mapPathSignalR($"~/Controller/JQGridModify");
        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        filepath = $"{filepath}/JQGrid{mod.ma_module}Modify.ashx";

        if (!File.Exists(filepath) & (mod.ma_moduletk == mod.ma_module))
        {
            int j = mod.from_sql.Split(' ')[0].Length;
            string md_object = mod.from_sql.Substring(0, j).removeAllSpaceOrTrimText(false);
            string id_object = ADmin_UpdateLinq.get_KeyOfTable(md_object);

            var temp = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/TempCode/JQGridModify.ashx");
            var contentTemp = File.ReadAllText(temp, Encoding.Unicode);
            var content = contentTemp;
            content = content.Replace("JQGrid__________Modify", $"JQGrid{mod.ma_module}Modify");
            content = content.Replace("md_congthuctrongtinhgia_mausac_id", id_object);
            content = content.Replace("md_congthuctrongtinhgia_mausac", md_object);
            File.WriteAllText(filepath, content, Encoding.Unicode);
        }
    }

    public static void CreateFileInterface(HttpContext context, Module_TK mod, int capmodule)
    {
        string filepath = ExcuteSignalRStatic.mapPathSignalR($"~/View/Menu/Content/Module");
        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        filepath = $"{filepath}/{mod.ma_module}.aspx";
        if (!File.Exists(filepath))
        {
            if (mod.loai_module == "JQGS")
            {
                var temp = ExcuteSignalRStatic.mapPathSignalR($"~/App_Data/TempCode/Module{capmodule}.aspx");
                var contentTemp = File.ReadAllText(temp, Encoding.Unicode);
                var content = contentTemp;
                content = content.Replace("__________", mod.ma_module);
                File.WriteAllText(filepath, content, Encoding.Unicode);
            }
            else
            {

            }
        }

        var folderPrint = ExcuteSignalRStatic.mapPathSignalR($"~/View/Print/{mod.ma_module}");
        if (!Directory.Exists(folderPrint))
        {
            Directory.CreateDirectory(folderPrint);
        }
        filepath = $"{folderPrint}/ModulePrint.aspx";
        if (!File.Exists(filepath))
        {
            var temp = ExcuteSignalRStatic.mapPathSignalR($"~/App_Data/TempCode/ModulePrint.aspx");
            var contentTemp = File.ReadAllText(temp, Encoding.Unicode);
            var content = contentTemp;
            content = content.Replace("Zzma_modulezZ", mod.ma_module);
            File.WriteAllText(filepath, content, Encoding.Unicode);
        }
        filepath = $"{folderPrint}/ModulePrint.aspx.cs";
        if (!File.Exists(filepath))
        {
            var temp = ExcuteSignalRStatic.mapPathSignalR($"~/App_Data/TempCode/ModulePrint.aspx.cs");
            var contentTemp = File.ReadAllText(temp, Encoding.Unicode);
            var content = contentTemp;
            content = content.Replace("Zzma_modulezZ", mod.ma_module);
            File.WriteAllText(filepath, content, Encoding.Unicode);
        }
    }

    public static void CreateFunction_ModuleScript(HttpContext context, Module_TK mod)
    {
        var folderJS = ExcuteSignalRStatic.mapPathSignalR($"~/js/Module_script");
        if (!Directory.Exists(folderJS))
        {
            Directory.CreateDirectory(folderJS);
        }
        var filepath = $"{folderJS}/{mod.ma_module}.js";

        if (!File.Exists(filepath) & (mod.ma_moduletk == mod.ma_module))
        {
            StreamWriter w = new StreamWriter(filepath, false, Encoding.Unicode);
            w.WriteLine("//Add function at here (don't remove this line, please)");
            w.Flush();
            w.Close();
        }
    }

    public static void DeleteFileLoad_Modify(HttpContext context, string ma_module, string url)
    {
        var filePaths = new System.Collections.Generic.List<string>();
        filePaths.Add($@"Controller/JQGrid/JQGrid{ma_module}Load.ashx");
        filePaths.Add($@"Controller/JQGridModify/JQGrid{ma_module}Modify.ashx");
        filePaths.Add($@"View/Menu/Content/Module/{ma_module}.aspx");
        filePaths.Add($@"js/Module_script/{ma_module}.js");
        foreach(var filePath in filePaths)
        {
            var filePath2 = ExcuteSignalRStatic.mapPathSignalR($"~/{filePath}");
            if (File.Exists(filePath2))
            {
                try
                {
                    File.Delete(filePath2);
                }
                catch
                {

                }
            }
        }

        var folderPaths = new System.Collections.Generic.List<string>();
        folderPaths.Add($@"View/Print/{ma_module}");
        foreach (var folderPath in folderPaths)
        {
            var folderPath2 = ExcuteSignalRStatic.mapPathSignalR($"~/{folderPath}");
            if (Directory.Exists(folderPath2))
            {
                try
                {
                    Directory.Delete(folderPath2, true);
                }
                catch
                {

                }
            }
        }
    }

    //Dung de update ham ho tro cho tat ca module thua ke tu module chinh
    public static int Update_InterFace(HttpContext context, Module_TK mod_, EntityContext db)
    {
        int dem = 0;
        string str_start = "//Start Ham ho tro them cho Grid (sẽ tự update nếu module chính update)";
        string str_end = "//#End Ham ho tro them cho Grid (sẽ tự update nếu module chính update)";
        //--
        string filepath = context.Server.MapPath(Security.UrlBase() + mod_.url);
        string w = System.IO.File.ReadAllText(filepath, Encoding.Unicode);
        string str_new = str_start + VNN_Function.FindString(w, str_start, str_end) + str_end;
        //--
        foreach (ad_module mod in db.ad_module.Where(s => s.thuake == (mod_.ad_module_id)).ToList())
        {
            string filepath2 = context.Server.MapPath(Security.UrlBase() + mod.url);
            string w2 = System.IO.File.ReadAllText(filepath2, Encoding.Unicode);
            string str_replace = str_start + VNN_Function.FindString(w2, str_start, str_end) + str_end;
            w2 = w2.Replace(str_replace, str_new);
            System.IO.File.WriteAllText(filepath2, w2, Encoding.Unicode);
            System.Threading.Thread.Sleep(250);
            dem++;
        }
        return dem;
    }
}