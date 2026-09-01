using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DataAcess;

public partial class LinqtoSQL : System.Web.UI.Page
{
    EntityContext db;
    protected void Page_Load(object sender, EventArgs e)
    {
        db = new EntityContext();

        System.Linq.Expressions.Expression<Func<ad_autoload, bool>> condition_cdhsLSXTSXs =
                                    s => s.hoatdong == true;

        var cdhsLSXTSXs = db.md_sanpham_bom_vattu;

        var result = cdhsLSXTSXs.Where(delegate (md_sanpham_bom_vattu tb) {
            return 1 == 1;
        });

        Response.Write("\n2:" + cdhsLSXTSXs.Take(1).FirstOrDefault());
    }

    protected void btn_Linq_Click(object sender, EventArgs e)
    {
        string filepath = Security.UrlBase() + "Controller/PublicFunction/LinqtoSQL.ashx";
        string str_start = "//Start change";
        string str_end = "//End change";
        string str_replace = "";
        string str_new = str_start + "\ntry { ";
        str_new += "var custQuery = (System.Linq.IQueryable)" + txt_Linq.Text + ";";
        str_new += "System.Data.Common.DbCommand dc = db.GetCommand(custQuery);";
        str_new += "context.Response.Write(dc.CommandText);";
        str_new += "} catch (Exception ex){";
        str_new += "context.Response.Write(\"Không phải cú pháp đúng." + "var custQuery = (IQueryable)" + txt_Linq.Text.Replace("\"", "\\\"") + ".\" + ex.Message);";
        str_new += "}\n" + str_end; 
        filepath = Server.MapPath(filepath);
        string w = File.ReadAllText(filepath);
        str_replace = str_start + VNN_Function.FindString(w, str_start, str_end) + str_end;
        w = w.Replace(str_replace, str_new);
        File.WriteAllText(filepath, w);
        VNN_VariablePublic.linqtosql_exec = true;
    }
}