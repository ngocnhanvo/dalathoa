<%@ WebHandler Language="C#" Class="JQGridSelectOptionJson" %>
using System;
using System.Web;
using System.Linq;
using DataAcess;
using System.Collections.Generic;
using Newtonsoft.Json;
public class JQGridSelectOptionJson : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string oper = "vnn";
        if (Security.id_taikhoan(context) != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
        switch (oper)
        {
            case "selGhepBo":
                this.selGhepBo(context);
                break;
        }
    }

    public void selGhepBo(HttpContext context)
    {
        var db = new EntityContext();
        string id_parent = context.Request.QueryString["id_parent"];
        var nknb = db.md_nhapkhonb.Where(s => s.md_nhapkhonb_id == id_parent).FirstOrDefault();
        var lsx = db.md_lenhsanxuat.Where(s => s.md_lenhsanxuat_id == nknb.md_lenhsanxuat_id).FirstOrDefault();
        string[] arr = nknb.md_lenhsanxuat_tosx_id.Split(new[] { " --- " }, StringSplitOptions.None);
        string sctLSX = arr[0];
        string tentoLSX = arr[1];
        string toId = db.md_phanxuong_to.Where(s => s.md_phanxuong_id == lsx.md_phanxuong_id &
        s.ten_to == tentoLSX).Select(s => s.md_to_id).FirstOrDefault();
        var lsxTSX = db.md_lenhsanxuat_tosx.Where(s => s.md_lenhsanxuat_id == lsx.md_lenhsanxuat_id
                            & s.md_phanxuong_to_id == toId).FirstOrDefault();

        var cdhs = db.md_lenhsanxuat_tosx_cdh.Where(s=>s.md_lenhsanxuat_tosx_id == lsxTSX.md_lenhsanxuat_tosx_id).ToList();
        var json = new List<Dictionary<string, object>>();
        var tongJsonSLTH = new List<Dictionary<string, object>>();
        var ddhpxs = from a in db.md_dondathangphanxuong_cdh.Where(s => s.md_dondathangphanxuong_id == lsx.md_dondathangphanxuong_id)
                     join b in db.md_sanpham on a.md_sanpham_id equals b.md_sanpham_id
                     orderby b.ma_sanpham
                     select new { a.md_sanpham_id, a.tong_sl_dat, a.sl_hoanthanh, a.sl_giamhanngach, b.ma_sanpham, b.md_donvitinhsanpham_id };

        foreach (var item in ddhpxs.ToList())
        {
            decimal soluong = -1;
            var jsonSLTH = new List<Dictionary<string, object>>();
            foreach (var cdh in cdhs.Where(s=>s.macuoi.Contains(item.ma_sanpham)))
            {
                var itemTong = tongJsonSLTH.Where(s => s["macai"].ToString() == cdh.md_sanpham_id).FirstOrDefault();
                decimal sltru = (itemTong == null) ? 0 : decimal.Parse(itemTong["soluong"] + "");
                decimal slhn = (cdh.sl_dahoanthanh.GetValueOrDefault(0) - cdh.sl_danhapkho.GetValueOrDefault(0) - sltru).Set0WhenlessThan0().GetValueOrDefault(0);
                if(slhn < soluong | soluong == -1)
                {
                    soluong = slhn;
                }

                jsonSLTH.Add(new Dictionary<string, object> {
                    { "macai", cdh.md_sanpham_id },
                    { "soluong", 0 }
                });
            }

            decimal sltd = item.tong_sl_dat.GetValueOrDefault(0) - item.sl_hoanthanh.GetValueOrDefault(0) - item.sl_giamhanngach.GetValueOrDefault(0);

            decimal sl_ = sltd > soluong ? soluong : sltd;
            foreach(var itemSLTH in jsonSLTH)
            {
                itemSLTH["soluong"] = sl_;
                string spId = itemSLTH["macai"] + "";
                var itemTong = tongJsonSLTH.Where(s => s["macai"].ToString() == spId).FirstOrDefault();
                if (itemTong == null)
                {
                    tongJsonSLTH.Add(itemSLTH);
                }
                else
                {
                    itemTong["soluong"] = decimal.Parse(itemTong["soluong"] + "") + sl_;
                }
            }

            
            json.Add(new Dictionary<string, object> {
                { "mabo", item.ma_sanpham },
                { "maboId", item.md_sanpham_id },
                { "dvtId", item.md_donvitinhsanpham_id },
                { "soluongDH", sltd },
                { "soluong", sl_ }
            });
        }

        context.Response.Write(JsonConvert.SerializeObject(json));
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
