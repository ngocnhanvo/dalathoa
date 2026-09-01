<%@ WebHandler Language="C#" Class="API_GuiNhanDonHang" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DataAcess;
using Newtonsoft.Json;

public class API_GuiNhanDonHang : IHttpHandler
{
    private EntityContext db = new EntityContext();
    public void ProcessRequest(HttpContext context)
    {
        string oper = context.Request.QueryString["oper"];
        switch (oper)
        {
            case "guiHDLH2":
                this.guiHDLH2(context);
                break;
            case "guiHDLH":
                this.guiHDLH(context);
                break;
            case "NhanDonHang":
                this.NhanDonHang(context);
                break;
            case "getListDSDH":
                this.getListDSDH(context);
                break;
            case "getPhieuXKs":
                this.getPhieuXKs(context);
                break;
            case "getPhieuXKsCDH":
                this.getPhieuXKsCDH(context);
                break;
        }
    }

    public void getPhieuXKs(HttpContext context)
    {
        string msg = "";
        try
        {
            string strdata = context.Request.Form["data"];
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(strdata);
            string dsdhId = data["dsdhId"].ToString();
            var pxbs = db.md_xuatban.Where(s => s.trangthai == Helper.HIEULUC & s.c_danhsachdathang_id == dsdhId).ToList()
                    .Select(s=> new {
                        s.md_xuatban_id,
                        s.sochungtu,
                        s.ngaychuyen,
                        s.so_cont,
                        s.so_seal,
                        loai_cont = db.md_loaicont.Where(t=>t.md_loaicont_id == s.loai_cont).Select(t => t.ten_cont).FirstOrDefault(),
                        tongsl = db.md_xuatban_cdh.Where(t=>t.md_xuatban_id == s.md_xuatban_id & t.sl_xuat > 0).Sum(t => t.sl_xuat),
                        s.tare,
                        s.mg
                    })
                    .ToList();
            msg = JsonConvert.SerializeObject(pxbs);
        }
        catch (Exception ex)
        {
            msg = "false##" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void getPhieuXKsCDH(HttpContext context)
    {
        string msg = "";
        try
        {
            string strdata = context.Request.Form["data"];
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(strdata);
            string dsdhId = data["md_xuatban_id"].ToString();
            var xb = db.md_xuatban.Where(s => s.md_xuatban_id == dsdhId).FirstOrDefault();
            if (xb == null)
                msg = "[]";
            else
            {
                var pxbs = db.md_xuatban_cdh.Where(s => s.md_xuatban_id == dsdhId & s.sl_xuat > 0).ToList()
                        .Select(s => new
                        {
                            xb.c_danhsachdathang_id,
                            s.md_xuatban_cdh_id,
                            s.md_xuatban_id,
                            md_sanpham_id = db.md_sanpham.Where(t => t.md_sanpham_id == s.md_sanpham_id).Select(t => t.ma_sanpham).FirstOrDefault(),
                            sl_po = 0,
                            sl_dathang = 0,
                            sl_nhapthucte = s.sl_xuat,
                            sl_conlai = 0,
                            s.sl_inner,
                            dvt_inner = db.c_dongdsdh.Where(t => t.c_danhsachdathang_id == xb.c_danhsachdathang_id & t.md_sanpham_id == s.md_sanpham_id).Select(t=>t.dvt_inner).FirstOrDefault(),
                            s.sl_outer,
                            dvt_outer = db.c_dongdsdh.Where(t => t.c_danhsachdathang_id == xb.c_danhsachdathang_id & t.md_sanpham_id == s.md_sanpham_id).Select(t=>t.dvt_outer).FirstOrDefault(),
                            s.tenkien,
                            s.sokien,
                            s.tldg,
                            s.nw,
                            s.gw,
                            s.cbm
                        })
                        .ToList();
                msg = JsonConvert.SerializeObject(pxbs);
            }
        }
        catch (Exception ex)
        {
            msg = "false##" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void guiHDLH2(HttpContext context)
    {
        string msg = "", msgEx = "";
        string strdata = context.Request.Form["data"];
        try
        {
            var update = false;
            var taptins = new List<md_ghichuhdlh>();
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(strdata);
            var result = JsonConvert.DeserializeObject<List<md_ghichuhdlh>>(data["result"] + "");
            var hdlh = (data["hdlh"] + "");
            var chdk = (data["chdk"] + "");

            if(result.Count <= 0)
            {
                msg = "Không có hướng dẫn làm hàng chi tiết.";
                goto EndEventHandler;
            }

            if(string.IsNullOrWhiteSpace(hdlh) & string.IsNullOrWhiteSpace(chdk))
            {
                msg = "Không có hướng dẫn làm hàng tổng thể";
                goto EndEventHandler;
            }

            var dhid = result.Select(s => s.lienket).Distinct().FirstOrDefault();
            var dsdh = db.c_danhsachdathang.Where(s => s.c_danhsachdathang_id == dhid).FirstOrDefault();
            if(dsdh == null)
            {
                msg = "Không tìm thấy đơn hàng.";
                goto EndEventHandler;
            }

            dsdh.huongdanlamhang2 = hdlh;
            dsdh.huongdanlamhangchung2 = chdk;

            foreach (var a in result)
            {
                var tt = new md_ghichuhdlh();
                a.CopyPropertiesTo(tt);
                tt.viewed = false;
                db.md_ghichuhdlh.Add(tt);
                update = true;
            }

            if (update)
            {
                dsdh.trangthaiHDLH = Helper.DAGUI;
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            msgEx = ex.ToString();
        }

        EndEventHandler:;

        if(msg.Length > 0)
        {
            msg = $@"
                <div style='color:red'>Có lỗi xảy ra: {msg}</div>
                <div style='display:none'>{msgEx}</div>
            ";
        }

        context.Response.Write(msg);
    }

    public void guiHDLH(HttpContext context)
    {
        string msg = "", msgEx = "";
        string strdata = context.Request.Form["data"];
        try
        {
            var update = false;
            var taptins = new List<md_taptin>();
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(strdata);
            var result = JsonConvert.DeserializeObject<List<md_taptin>>(data["result"] + "");
            if(result.Count <= 0)
            {
                msg = "Không tìm thấy tập tin.";
                goto EndEventHandler;
            }

            var dhid = result.Select(s => s.lienket).Distinct().FirstOrDefault();
            var dsdh = db.c_danhsachdathang.Where(s => s.c_danhsachdathang_id == dhid).FirstOrDefault();
            if(dsdh == null)
            {
                msg = "Không tìm thấy đơn hàng.";
                goto EndEventHandler;
            }

            foreach (var a in result)
            {

                var ttServer = db.md_taptin.Where(s => s.tentaptin == a.tentaptin & s.lienket == a.lienket).FirstOrDefault();
                var tt = db.md_taptin.Local.Where(s => s.tentaptin == a.tentaptin & s.lienket == a.lienket).FirstOrDefault();
                var add = tt == null;
                if(add)
                {
                    tt = new md_taptin();
                    a.CopyPropertiesTo(tt);
                    tt.mota = "";
                    tt.viewed = false;
                    db.md_taptin.Add(tt);
                    update = true;
                    var bytes = Convert.FromBase64String(a.mota);
                    var path = ExcuteSignalRStatic.mapPathSignalR($@"~/{a.path}");
                    System.IO.File.WriteAllBytes(path, bytes);
                }
            }

            if (update)
            {
                dsdh.trangthaiHDLH = Helper.DAGUI;
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            msgEx = ex.ToString();
        }

        EndEventHandler:;

        if(msg.Length > 0)
        {
            msg = $@"
                <div style='color:red'>Có lỗi xảy ra: {msg}</div>
                <div style='display:none'>{msgEx}</div>
            ";
        }

        context.Response.Write(msg);
    }

    public void getListDSDH(HttpContext context)
    {
        string msg = "";
        try
        {
            string strdata = context.Request.Form["data"];
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(strdata);
            var result = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["result"] + "");
            var lst = new List<Dictionary<string, object>>();
            foreach (var a in result)
            {
                string c_dsdh_id = a["c_danhsachdathang_id"] + "";
                var dsdh = db.c_danhsachdathang.Where(s => s.c_danhsachdathang_id == c_dsdh_id).FirstOrDefault();
                if (dsdh != null)
                {
                    var item = new Dictionary<string, object>();
                    item["c_danhsachdathang_id"] = dsdh.c_danhsachdathang_id;
                    item["huongdanlamhang"] = dsdh.huongdanlamhang;
                    item["huongdanlamhangchung"] = dsdh.huongdanlamhangchung;
                    item["trangthai"] = dsdh.trangthai == "HIEULUC" ? "DAHIEULUC" : dsdh.trangthai;
                    item["hdlhs"] = JsonConvert.SerializeObject(db.md_ghichuhdlh.Where(s => s.lienket == dsdh.c_danhsachdathang_id & (s.viewed ?? false) == true).ToList());
                    item["tts"] = JsonConvert.SerializeObject(db.md_taptin.Where(s => s.lienket == dsdh.c_danhsachdathang_id & (s.viewed ?? false) == true).ToList());
                    lst.Add(item);
                }
            }
            msg = JsonConvert.SerializeObject(lst);
        }
        catch (Exception ex)
        {
            msg = "false##" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void NhanDonHang(HttpContext context)
    {
        string msg = "";
        string strdata = context.Request.Form["data"];
        try
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(strdata);
            var result = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data["result"] + "");
            foreach (var a in result)
            {
                var item = JsonConvert.DeserializeObject<Dictionary<string, object>>(a["c_danhsachdathang"] + "");
                var lstItem = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(a["c_dongdsdh"] + "");
                var lstItem2 = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(a["c_phidathang"] + "");
                string c_danhsachdathang_id = item["c_danhsachdathang_id"] + "";
                var exist = db.c_danhsachdathang.Where(s => s.c_danhsachdathang_id == c_danhsachdathang_id).FirstOrDefault();
                if (exist == null)
                {
                    var sopo = item["so_po"] + "";
                    var dtkdID = item["md_doitackinhdoanh_id"] + "";
                    var c_dsdh = new c_danhsachdathang();
                    if (dtkdID == "ANCOTRADING")
                        dtkdID = "ANCO TRADING";

                    var dtkd = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == dtkdID).FirstOrDefault();
                    if(dtkd == null)
                    {
                        msg = $@"{sopo}: Không tìm thấy mã khách hàng ""{dtkdID}"" trong dữ liệu gốc";
                        goto EndEventHandler;
                    }
                    c_dsdh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id;
                    c_dsdh.anco_check = true;
                    c_dsdh.check_nangluc = false;
                    c_dsdh.reportKCS = "-";
                    c_dsdh.trangthaiHDLH = Helper.CHUAGUI;
                    c_dsdh.c_danhsachdathang_id = item["c_danhsachdathang_id"] + "";
                    c_dsdh.c_donhang_id = item["c_donhang_id"] + "";
                    c_dsdh.dg_nangluc = false;
                    c_dsdh.diachigiaohang = item["diachigiaohang"] + "";
                    c_dsdh.discount = Convert.ToDecimal(item["discount"]);
                    c_dsdh.grandtotal = Convert.ToDecimal(item["grandtotal"]);
                    c_dsdh.hangiaohang_po = Convert.ToDateTime(item["hangiaohang_po"]);
                    c_dsdh.hoatdong = true;
                    c_dsdh.huongdanlamhang = item["huongdanlamhang"] + "";
                    c_dsdh.huongdanlamhangchung = item["huongdanlamhangchung"] + "";
                    c_dsdh.isgui_hdlh = Convert.ToBoolean(item["isgui_hdlh"]);
                    //c_dsdh.khachhang = item["khachhang"] + "";
                    //c_dsdh.md_doitackinhdoanh_id = item["md_doitackinhdoanh_id"] + "";

                    c_dsdh.md_trangthai_id = item["md_trangthai_id"] + "";
                    c_dsdh.mota = item["mota"] + "";
                    //c_dsdh.ngaycapnhat = item["ngaycapnhat"].ToDateTime();
                    c_dsdh.ngaylap = Convert.ToDateTime(item["ngaylap"]);
                    c_dsdh.ngaytao = DateTime.Now;
                    c_dsdh.ngay_tigia = null;
                    c_dsdh.nguoicapnhat = item["nguoicapnhat"] + "";
                    c_dsdh.nguoitao = item["nguoitao"] + "";
                    c_dsdh.nguoi_dathang = item["nguoi_dathang"] + "";
                    c_dsdh.nguoi_phutrach = item["nguoi_phutrach"] + "";
                    c_dsdh.sochungtu = item["sochungtu"] + "";
                    c_dsdh.so_po = sopo;
                    c_dsdh.total = Convert.ToDecimal(item["total"]);
                    c_dsdh.trangthai = item["trangthai"] + "";
                    if (item.ContainsKey("cont20"))
                    {
                        c_dsdh.cont20 = Convert.ToDecimal(item["cont20"] == null ? "0" : item["cont20"]);
                    }
                    if (item.ContainsKey("cont40"))
                    {
                        c_dsdh.cont40 = Convert.ToDecimal(item["cont40"] == null ? "0" : item["cont40"]);
                    }
                    if (item.ContainsKey("cont40hc"))
                    {
                        c_dsdh.cont40hc = Convert.ToDecimal(item["cont40hc"] == null ? "0" : item["cont40hc"]);
                    }

                    decimal cbmAll = 0;
                    var chungloais = new List<string>();
                    foreach (var b in lstItem)
                    {
                        string ma_sanpham = b["md_sanpham_id"] + "";
                        chungloais.Add(ma_sanpham.Substring(0, 2));
                        var c_dongdsdh = new c_dongdsdh();
                        c_dongdsdh.w2 = Convert.ToDecimal(b["w2"] == null ? "0" : b["w2"]);
                        c_dongdsdh.w1 = Convert.ToDecimal(b["w1"] == null ? "0" : b["w1"]);
                        c_dongdsdh.vn = Convert.ToDecimal(b["vn"] == null ? "0" : b["vn"]);
                        c_dongdsdh.vl = Convert.ToDecimal(b["vl"] == null ? "0" : b["vl"]);
                        c_dongdsdh.vd = Convert.ToDecimal(b["vd"] == null ? "0" : b["vd"]);
                        c_dongdsdh.v2 = Convert.ToDecimal(b["v2"] == null ? "0" : b["v2"]);
                        cbmAll += c_dongdsdh.v2.GetValueOrDefault(0);
                        c_dongdsdh.sl_huy = Convert.ToDecimal(b["sl_huy"] == null ? "0" : b["sl_huy"]);
                        c_dongdsdh.tem_dan = b["tem_dan"] + ""; ;
                        c_dongdsdh.sothutu = Convert.ToInt32(b["sothutu"]);
                        c_dongdsdh.sl_outer = Convert.ToDecimal(b["sl_outer"]);
                        c_dongdsdh.dvt_outer = b["dvt_outer"] + "";
                        c_dongdsdh.sl_inner = Convert.ToDecimal(b["sl_inner"]);
                        c_dongdsdh.dvt_inner = b["dvt_inner"] + "";

                        c_dongdsdh.sl_dathang = Convert.ToDecimal(b["sl_dathang"]);
                        c_dongdsdh.sl_dagiao = Convert.ToDecimal(b["sl_dagiao"]);
                        c_dongdsdh.sl_cont = Convert.ToDecimal(b["sl_cont"]);
                        c_dongdsdh.sl_conlai = Convert.ToDecimal(b["sl_conlai"]);
                        c_dongdsdh.mota = b["mota"] + "";
                        c_dongdsdh.md_sanpham_id = ma_sanpham + "";
                        c_dongdsdh.md_donggoi_id = b["md_donggoi_id"] + "";
                        c_dongdsdh.md_doitackinhdoanh_id = b["md_doitackinhdoanh_id"] + "";
                        c_dongdsdh.ma_sanpham_khach = b["ma_sanpham_khach"] + "";
                        c_dongdsdh.l2 = Convert.ToDecimal(b["l2"]);
                        c_dongdsdh.l1 = Convert.ToDecimal(b["l1"]);
                        c_dongdsdh.huongdan_dathang = b["huongdan_dathang"] + "";
                        c_dongdsdh.hoatdong = true;
                        c_dongdsdh.h2 = Convert.ToDecimal(b["h2"]);
                        c_dongdsdh.h1 = Convert.ToDecimal(b["h1"]);
                        c_dongdsdh.gianhap = Convert.ToDecimal(b["gianhap"]);
                        c_dongdsdh.giachuan = Convert.ToDecimal(b["giachuan"] == null ? "0" : b["giachuan"]);
                        c_dongdsdh.phi = Convert.ToDecimal(b["phi"] == null ? "0" : b["phi"]);
                        c_dongdsdh.phidg = Convert.ToDecimal(b["phidg"] == null ? "0" : b["phidg"]);
                        c_dongdsdh.ghichu_vachngan = b["ghichu_vachngan"] + "";
                        c_dongdsdh.c_dongdsdh_id = b["c_dongdsdh_id"] + "";
                        c_dongdsdh.c_dongdonhang_id = b["c_dongdonhang_id"] + "";
                        c_dongdsdh.c_danhsachdathang_id = b["c_danhsachdathang_id"] + "";
                        c_dongdsdh.anco_check = true;
                        db.c_dongdsdh.Add(c_dongdsdh);
                    }
                    c_dsdh.chungloai = string.Join(",", chungloais.OrderBy(s => s).Distinct());
                    db.c_danhsachdathang.Add(c_dsdh);
                    //c_dsdh.discount = cbmAll;
                    c_dsdh.cbm = cbmAll;
                }

                foreach (var b in lstItem2)
                {
                    var c_phidathang = new c_phidathang();
                    c_phidathang.c_phidathang_id = b["c_phidathang_id"] + "";
                    c_phidathang.c_danhsachdathang_id = b["c_danhsachdathang_id"] + "";
                    c_phidathang.hoatdong = true;
                    c_phidathang.isphicong = Convert.ToBoolean(b["isphicong"] + "");
                    c_phidathang.mota = b["mota"] + "";
                    c_phidathang.ngaytao = DateTime.Now;
                    c_phidathang.ngaycapnhat = DateTime.Now;
                    c_phidathang.sotien = Convert.ToDecimal(b["sotien"] + "");
                    c_phidathang.anco_check = true;
                    db.c_phidathang.Add(c_phidathang);
                }
            }

            if (msg.Length <= 0)
                db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg += string.Format(@"
                <div style='color:red'>Có lỗi xảy ra: {0}</div>
                <div style='display:none'>{1}</div>
				<div style='display:none'>{2}</div>
                ", ex.Message, strdata, ex + "");
        }

        EndEventHandler:;
        context.Response.Write(msg);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}


