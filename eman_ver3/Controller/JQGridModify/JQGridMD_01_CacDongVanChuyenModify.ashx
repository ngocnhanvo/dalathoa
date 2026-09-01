<%@ WebHandler Language="C#" Class="JQGridMD_01_CacDongVanChuyenModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
using Newtonsoft.Json;

public class JQGridMD_01_CacDongVanChuyenModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_CapnhatSLcanchuyen":
                this.CA_01_CapnhatSLcanchuyen(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_CapnhatSLcanchuyen(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        var rows = context.Request.Form["rows"];
        string id = context.Request.Form["id"];

        var vcnb = db.md_vanchuyennoibo.Where(s=>s.md_vanchuyennoibo_id == id).FirstOrDefault();
        if (vcnb.md_trangthai_id == Helper.HIEULUC)
        {
            msg = $@"Phiếu đã Hiệu Lực";
            goto EndEventHandler;
        }

        try
        {
            var dongHangs = JsonConvert.DeserializeObject<List<md_vanchuyennoibo_cdvc>>(rows);
            foreach (var cdvc in db.md_vanchuyennoibo_cdvc.Where(s => s.md_vanchuyennoibo_id == id).ToList())
            {
                var dongHang = dongHangs.Where(s => s.md_vanchuyennoibo_cdvc_id == cdvc.md_vanchuyennoibo_cdvc_id).FirstOrDefault();
                if (dongHang != null)
                {
                    cdvc.soluong_dichchuyen = dongHang.soluong_dichchuyen;
                    if(cdvc.soluong_dichchuyen > cdvc.soluong_muonchuyen)
                    {
                        var sp = db.md_sanpham.Where(s => s.md_sanpham_id == cdvc.md_sanpham_id).FirstOrDefault();
                        var sldc = cdvc.soluong_dichchuyen.GetValueOrDefault().DropTrailingZeros();
                        var slmc = cdvc.soluong_muonchuyen.GetValueOrDefault().DropTrailingZeros();
                        msg = $@"HHVT ""{sp.ma_sanpham}"" có SL thực chuyển lớn hơn SL dự kiến ({sldc} > {slmc})";
                        goto EndEventHandler;
                    }
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
            msg = $@"<div style='color:blue'>Cập nhật số lượng cần chuyển thành công</div>";
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
        decimal soluong_dichchuyen = decimal.Parse(context.Request.Form["soluong_dichchuyen"]);

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                string id = context.Request.Form["id"];
                var object_ = db.md_vanchuyennoibo_cdvc.Where(p => p.md_vanchuyennoibo_cdvc_id == id).Take(1).FirstOrDefault();
                var vnnb = db.md_vanchuyennoibo.Where(s => s.md_vanchuyennoibo_id == object_.md_vanchuyennoibo_id).Take(1).FirstOrDefault();
                decimal soluong_td_dc = object_.soluong_muonchuyen.GetValueOrDefault(0);
                if (object_ == null)
                {
                    msg = $@"Lỗi: Không tìm thấy đối tượng cần sửa.";
                }
                else if (vnnb.md_trangthai_id == Helper.HIEULUC)
                {
                    msg = $@"Lỗi:Dòng ""{vnnb.sochungtu}"" đã Hiệu lực";
                }
                else if (soluong_dichchuyen < 0)
                {
                    msg = $@"Lỗi: Số lượng dịch chuyển không được âm.";
                }
                else if (soluong_td_dc < soluong_dichchuyen)
                {
                    msg = $@"Lỗi: Số lượng dịch chuyển tối đa là: {soluong_td_dc.DropTrailingZeros()}";
                }


                if (msg.Length <= 0)
                {
                    object_.soluong_dichchuyen = soluong_dichchuyen;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = "Lỗi:" + ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = "true#Cập nhật thành công.";
                transaction.Commit();
            }
            else
            {
                msg = "false#" + msg;
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