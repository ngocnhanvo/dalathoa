<%@ WebHandler Language="C#" Class="JQGridSelectOptionHTML" %>
using System;
using System.Web;
using System.Linq;
using DataAcess;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public class JQGridSelectOptionHTML : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string oper = "vnn";
        if (Security.id_taikhoan(context) != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
        switch (oper)
        {
            case "selKho":
                this.selKho(context);
                break;
            case "selBoPhanToPX":
                this.selBoPhanToPX(context);
                break;
            case "selBoPhanLay":
                this.selBoPhanLay(context);
                break;
            case "selChucNang":
                this.selChucNang(context);
                break;
        }
    }

    public void selChucNang(HttpContext context)
    {
        var db = new EntityContext();
        string str = "<select>";
        str += context.Request.QueryString["firstnull"] == "true" ? string.Format(@"<option value=""""></option>") : "";
        foreach (var cn in db.ad_case.Where(s => s.hoatdong == true).ToList().OrderBy(s => s.ten_case))
        {
            str += string.Format(@"<option value=""{0}"" macase=""{2}"">{1}</option>",
                cn.ad_case_id,
                cn.ten_case,
                cn.ma_case
            );
        }
        str += "</select>";
        context.Response.Write(str);
    }

    public void selKho(HttpContext context)
    {
        var db = new EntityContext();
        string str = "<select>";
        str += context.Request.QueryString["firstnull"] == "true" ? string.Format(@"<option value=""""></option>") : "";
        foreach (var cn in db.md_kho.Where(s => s.hoatdong == true).ToList().OrderBy(s => int.Parse(Regex.Replace(s.ma_kho, "[^0-9]", ""))))
        {
            var ktts = Helper.KHOTONTHO.Split(',');
            var kttps = Helper.KHOTONTP.Split(',');
            str += string.Format(@"<option value=""{0}"" pxId=""{2}"" toId=""{3}"" pbId=""{4}"" loaikho=""{5}"">{1}</option>",
                cn.md_kho_id,
                cn.ten_kho,
                cn.md_phanxuong_id,
                cn.md_to_id,
                cn.phongbanId,
                cn.vattu == true ? "KVT" :
                    cn.ma_kho == Helper.KhoThoChoHoanThien ? "KSX1" :
                        cn.ma_kho == Helper.KhoHangSauHoanThien ? "KSX2" :
                            cn.ma_kho == Helper.KHOTRON ? "KHH" :
                                ktts.Contains(cn.ma_kho) ? "KTSX" :
                                    kttps.Contains(cn.ma_kho) ? "KTTP" :
                                        cn.ma_kho == Helper.KHODG ? "KDG" : "KTP"
            );
        }
        str += "</select>";
        context.Response.Write(str);
    }

    public void selBoPhanToPX(HttpContext context)
    {
        string sql = string.Format(@"
            select * from (
	            select md_phongban_id as id, ten_phongban as value, 2 as orderby, ROW_NUMBER() over (ORDER BY ten_phongban) + 999 as Num
	            from ad_department
                where ma_phongban not like 'CD-%'
            )A
            order by A.orderby, A.Num 
        ");

        var dt = Mbg.Data.SqlClient.SqlHelper.GetData(sql);

        string str = "<select>";
        str += context.Request.QueryString["firstnull"] == "true" ? string.Format(@"<option value=""""></option>") : "";
        foreach (System.Data.DataRow row in dt.Rows)
        {
            str += string.Format(@"<option value=""{0}"">{1}</option>", row["id"] + "", row["value"] + "");
        }
        str += "</select>";
        context.Response.Write(str);
    }

    public void selBoPhanLay(HttpContext context)
    {
        var db = new EntityContext();
        string id = context.Request.QueryString["id"];
        var cdhs = db.md_xuatkhonb_cdh.Where(s => s.md_xuatkhonb_id == id & s.sl_muonxuat != null).Select(s => s.md_kho_id).Distinct().ToList();
        var bophanlay = db.md_phanxuong_to.Where(s => cdhs.Contains(s.md_to_id)).Select(s => new { id = s.md_to_id, ten = s.ten_to });
        var bophanlay2 = db.ad_department.Where(s => cdhs.Contains(s.md_phongban_id)).Select(s => new { id = s.md_phongban_id, ten = s.ten_phongban });
        var bophanlayAll = bophanlay.Union(bophanlay2);
        string str = "";
        foreach (var cn in bophanlayAll.ToList())
        {
            str += string.Format(@"<option value=""{0}"">{1}</option>"
                , cn.id
                , cn.ten
            );
        }
        context.Response.Write(str);
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
