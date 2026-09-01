<%@ WebHandler Language="C#" Class="JQGridMD_01_CDHDXuatKModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using DataAcess;
using Newtonsoft.Json;

public class JQGridMD_01_CDHDXuatKModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public EntityContext db = new EntityContext();
    public EntityFunction entityFunc = new EntityFunction();
    User_TK userTK = null;

    public string oper = "vnn";
    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
        {
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
            userTK = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context), db);
        }

        switch (oper)
        {
            case "add":
                this.add(context);
                break;
            case "edit":
                this.edit(context);
                break;
            case "del":
                this.del(context);
                break;
            case "CA_01_CapnhatSLXBan":
                this.CA_01_CapnhatSLXBan(context);
                break;
            case "CA_01_GhepKien":
                this.CA_01_GhepKien(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_GhepKien(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        var rows = context.Request.Form["rows"];
        var id = context.Request.Form["id"].removeAllSpaceOrTrimText(true);
        var ids = context.Request.Form["ids"].removeAllSpaceOrTrimText(true).Split(',');
        var tenkien = context.Request.Form["tenkien"].removeAllSpaceOrTrimText(true);
        var sokien = context.Request.Form["sokien"].removeAllSpaceOrTrimText(true);

        var vcnb = db.md_xuatban.Where(s=>s.md_xuatban_id == id).FirstOrDefault();
        if (vcnb.sent.GetValueOrDefault(false))
        {
            msg = $@"Phiếu đã gửi cho Ancotrading";
            goto EndEventHandler;
        }

        try
        {
            foreach (var cdvc in db.md_xuatban_cdh.Where(s => ids.Contains(s.md_xuatban_cdh_id)).ToList())
            {
                cdvc.tenkien = tenkien;
                cdvc.sokien = sokien.ToNullableInt();
                cdvc.ngaycapnhat = DateTime.Now;
            }

            db.SaveChanges();
        }
        catch(Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        context.Response.Write(msg);
    }

    public void CA_01_CapnhatSLXBan(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        var rows = context.Request.Form["rows"];
        string id = context.Request.Form["id"];

        var vcnb = db.md_xuatban.Where(s=>s.md_xuatban_id == id).FirstOrDefault();
        if (vcnb.trangthai == Helper.HIEULUC)
        {
            msg = $@"Phiếu đã Hiệu Lực";
            goto EndEventHandler;
        }

        try
        {
            var dongHangs = JsonConvert.DeserializeObject<List<md_xuatban_cdh>>(rows);
            foreach (var cdvc in db.md_xuatban_cdh.Where(s => s.md_xuatban_id == id).ToList())
            {
                var dongHang = dongHangs.Where(s => s.md_xuatban_cdh_id == cdvc.md_xuatban_cdh_id).FirstOrDefault();
                if (dongHang != null)
                {
                    cdvc.sl_xuat = dongHang.sl_xuat;
                    cdvc.sl_inner = dongHang.sl_inner;
                    cdvc.sl_outer = dongHang.sl_outer;
                    cdvc.tldg = dongHang.tldg;
                    cdvc.nw = dongHang.nw;
                    cdvc.gw = dongHang.gw;
                    cdvc.cbm = dongHang.cbm;
                    cdvc.ngaycapnhat = DateTime.Now;
                }
            }

            db.SaveChanges();
        }
        catch(Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"<div style='color:blue'>Cập nhật số lượng xuất bán thành công</div>";
        }
        else
        {
            msg = $@"<div style='color:red' error>{msg}</div>";
        }
        context.Response.Write(msg);
    }

    public void add(HttpContext context)
    {
        string msg = "";
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        decimal tong_sl_xuat = decimal.Parse(context.Request.Form["tong_sl_xuat"]);
        decimal sl_xuat = decimal.Parse(context.Request.Form["sl_xuat"]);
        string md_donvitinhsanpham_id = context.Request.Form["md_donvitinhsanpham_id"];
        decimal nhanvoi = 1, saiso = 0;

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                string id = context.Request.Form["id"];
                var object_ = db.md_xuatban_cdh.Where(p => p.md_xuatban_cdh_id == id).Take(1).FirstOrDefault();
                var xnb = db.md_xuatban.Where(p => p.md_xuatban_id == object_.md_xuatban_id).Take(1).FirstOrDefault();
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == object_.md_sanpham_id).FirstOrDefault();
                string ghichu_donvi2 = "";
                string ten_dvt = db.md_donvitinhsanpham.
                        Where(s => s.md_donvitinhsanpham_id == md_donvitinhsanpham_id).Select(s => s.ten_dvt).FirstOrDefault();
                string ten_dvt2 = db.md_donvitinhsanpham.
                        Where(s => s.md_donvitinhsanpham_id == object_.md_donvitinhsanpham_id).Select(s => s.ten_dvt).FirstOrDefault();

                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                }

                if (sl_xuat <= 0)
                {
                    msg = "Số lượng xuất phải lớn hớn 0";
                }
                else if (object_.md_donvitinhsanpham_id != md_donvitinhsanpham_id)
                {
                    var dvtsp_cddv = db.md_donvitinhsanpham_cddv.
                            Where(s =>
                                s.md_donvitinhsanpham_id == md_donvitinhsanpham_id
                                & s.md_dvtsp_id == sp.md_donvitinhsanpham_id
                                & s.md_sanpham_id == sp.ma_sanpham
                            ).Take(1).FirstOrDefault();

                    if (dvtsp_cddv != null)
                    {
                        nhanvoi = dvtsp_cddv.nhanvoi.Value;
                        saiso = dvtsp_cddv.chiacho.Value * sl_xuat;
                        if (saiso > dvtsp_cddv.saiso_toida.Value | saiso < 0)
                            saiso = dvtsp_cddv.saiso_toida.Value;
                        ghichu_donvi2 += "=> 1 " + ten_dvt + " = " + nhanvoi.DropTrailingZeros() + " " + ten_dvt2;
                        ghichu_donvi2 += " (Sai số cho phép: " + dvtsp_cddv.chiacho.Value.DropTrailingZeros() + " " + ten_dvt2 + ")";
                        if (sl_xuat > 1)
                        {
                            ghichu_donvi2 += "\n=> " + sl_xuat + " " + ten_dvt + " = " + (sl_xuat * nhanvoi).DropTrailingZeros() + " " + ten_dvt2;
                            ghichu_donvi2 += " (Sai số cho phép: " + saiso.DropTrailingZeros() + " " + ten_dvt2 + ")";
                        }
                    }
                    else
                    {
                        msg = "Đơn vị tính đã chọn không thể quy đổi.";
                    }
                }

                if ((sl_xuat * nhanvoi - saiso) > object_.tong_sl_xuat)
                {
                    msg = string.Format(@"false#Chỉ có thể xuất số lượng tối đa là: ""{0} {1}""",
                        object_.tong_sl_xuat.Value.DropTrailingZeros(), ten_dvt2);
                    msg += string.Format(@"<br> + Số lượng đang muốn xuất: ""{0} {1}""",
                        (sl_xuat * nhanvoi).DropTrailingZeros(), ten_dvt2);
                    msg += string.Format(@"<br> + Độ sai lệch cho phép: ""{0} {1}""",
                        saiso.DropTrailingZeros(), ten_dvt2);
                }

                foreach (var ncc in db.md_xuatban.Where(s => s.md_xuatban_id == object_.md_xuatban_id).ToList())
                {
                    if (ncc.trangthai == "HIEULUC")
                    {
                        msg = "Phiếu xuất kho đã hiệu lực !";
                    }
                }
                if (msg.Length <= 0)
                {
                    VNN_Function.SetFormValue(object_.nameof(s=>s.ghichu_donvi2), ghichu_donvi2);
                    VNN_Function.SetFormValue(object_.nameof(s=>s.md_sanpham_id), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.md_donvitinhsanpham_id), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.tenhang), "VNN_notpost");
                    VNN_Function.SetFormValue(object_.nameof(s=>s.sl_xuat), sl_xuat.ToString());
                    VNN_Function.SetFormValue(object_.nameof(s=>s.sl_muonxuat), "VNN_notpost");
                    object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                    object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"true#Cập nhật thành công");
                transaction.Commit();
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = string.Format(@"false#{0}", msg);
                transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        string msg = "";
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