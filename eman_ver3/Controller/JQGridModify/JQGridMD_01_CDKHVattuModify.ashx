<%@ WebHandler Language="C#" Class="JQGridMD_01_CDKHVattuModify" %>
using System;
using System.Web;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using DataAcess;
public class JQGridMD_01_CDKHVattuModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_DuyetSoluongKHMVT":
                this.CA_01_YeuCauSoluongKHMVT(context);
                break;
            case "CA_01_khmvt_DOINCC":
                this.CA_01_khmvt_DOINCC(context);
                break;
            case "CA_01_LamMoiHangTonKho":
                this.CA_01_LamMoiHangTonKho(context);
                break;
            case "CA_01_DuýetLMuaVT":
                this.CA_01_DuýetLMuaVT(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_DuýetLMuaVT(HttpContext context)
    {
        string data = context.Request.Form["data"];
        string msg = "";
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var datas = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(data);
                int count = 0;
                foreach(var row in datas)
                {
                    string id = row["id"].ToString();
                    string slDuyet = row["slDuyet"] + "";
                    var dkhmvt = db.c_kehoachmuavt_cdh.Where(s => s.c_kehoachmuavt_cdh_id == id).FirstOrDefault();

                    if (dkhmvt != null) {
                        if (count == 0)
                        {
                            var khmvt = db.c_kehoachmuavt.Where(s => s.c_kehoachmuavt_id == dkhmvt.c_kehoachmuavt_id).FirstOrDefault();
                            if (khmvt == null)
                            {
                                msg = @"KHMVT không được tìm thấy";
                                break;
                            }
                            else if (khmvt.md_trangthai_id != Helper.DANHAN)
                            {
                                msg = @"KHMVT không ở trạng thái ""Đã Xác Nhận""";
                                break;
                            }
                            count = 1;
                        }

                        if(msg.Length <= 0)
                        {
                            decimal? slduyet = null;
                            if (!string.IsNullOrEmpty(slDuyet))
                                slduyet = decimal.Parse(slDuyet);
                            dkhmvt.sl_duyet2 = slduyet;
                        }
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if(msg.Length <= 0)
            {
                msg = string.Format(@"<div style='color:blue'>Điều chỉnh SL duyệt thành công</div>");
                transaction.Commit();
            }
            else
            {
                msg = string.Format(@"<div style='color:red' class='error'>Lỗi: {0}</div>", msg);
                transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }

    public void CA_01_LamMoiHangTonKho(HttpContext context)
    {
        string idparent = context.Request.Form["id"];
        string msg = "";

        try
        {
            var khmvt = db.c_kehoachmuavt.FirstOrDefault(s => s.c_kehoachmuavt_id == idparent);
            if (khmvt.md_trangthai_id != Helper.SOANTHAO)
            {
                msg = string.Format(@"Kế hoach này đã ""Hiệu Lực"".");
            }
            else
            {
                db.c_kehoachmuavt_dklht.RemoveRange(db.c_kehoachmuavt_dklht.Where(s => s.c_kehoachmuavt_id == khmvt.c_kehoachmuavt_id));
                foreach (var khmvt_cdh in db.c_kehoachmuavt_cdh.Where(s => s.c_kehoachmuavt_id == idparent).ToList())
                {
                    var spvt = db.md_sanpham.Where(s => s.md_sanpham_id == khmvt_cdh.md_sanpham_id).FirstOrDefault();

                    var tonkho = (from a in db.md_kho_sanpham
                                  join b in db.md_kho on a.md_kho_id equals b.md_kho_id
                                  where b.vattu == true & a.md_sanpham_id == khmvt_cdh.md_sanpham_id
                                  select new { a.md_sanpham_id, a.md_kho_id, a.soluong }).ToList();
                    decimal sl_tonkho = 0,
                               sl_tonkhotoithieu = khmvt_cdh.sl_tonkho_toithieu.GetValueOrDefault(0),
                               sl_khmvtCan = khmvt_cdh.sl_can.GetValueOrDefault(0);

                    if (tonkho.Count > 0)
                    {
                        foreach (var tonkho_ in tonkho)
                        {
                            decimal sl_tonkho_ = tonkho_.soluong.GetValueOrDefault(0) -
                                            db.md_kho_giucho.Where(s =>
                                                s.md_sanpham_id == tonkho_.md_sanpham_id &
                                                s.md_kho_id == tonkho_.md_kho_id
                                            ).Select(s => s.soluong_giucho).Sum()
                                            .GetValueOrDefault(0).Set0WhenlessThan0().GetValueOrDefault(0);
                            sl_tonkho += sl_tonkho_;
                            var dklht = new c_kehoachmuavt_dklht();
                            dklht.c_kehoachmuavt_dklht_id = Helper.getNewId();
                            dklht.c_kehoachmuavt_id = khmvt.c_kehoachmuavt_id;
                            dklht.md_kho_id = tonkho_.md_kho_id;
                            dklht.md_sanpham_id = tonkho_.md_sanpham_id;
                            dklht.md_donvitinhsanpham_id = khmvt_cdh.md_donvitinhsanpham_id;
                            dklht.sl_tonkho = sl_tonkho_;
                            sl_tonkho_ = (sl_tonkho_ - sl_tonkhotoithieu).Set0WhenlessThan0().GetValueOrDefault(0);
                            //dklht.sl_layton = sl_tonkho_ > sl_khmvtCan ? sl_khmvtCan : sl_tonkho_;
                            dklht.sl_layton = 0;
                            var lt_tt = dklht.sl_layton;
                            //dklht.sl_layton = lt_tt > sl_tonkhotoithieu ? lt_tt - sl_tonkhotoithieu : 0;
                            sl_tonkhotoithieu = (sl_tonkhotoithieu - lt_tt.GetValueOrDefault(0)).Set0WhenlessThan0().GetValueOrDefault(0);
                            sl_khmvtCan = (sl_khmvtCan - dklht.sl_layton.GetValueOrDefault(0)).Set0WhenlessThan0().GetValueOrDefault(0);
                            db.c_kehoachmuavt_dklht.Add(dklht);
                        }
                    }

                    khmvt_cdh.sl_tonkho_toithieu = spvt.sl_tonkho_toithieu.GetValueOrDefault(0);
                    khmvt_cdh.sl_tonkho = sl_tonkho;
                    sl_tonkho = (sl_tonkho - khmvt_cdh.sl_tonkho_toithieu.GetValueOrDefault(0)).Set0WhenlessThan0().GetValueOrDefault(0);
                    khmvt_cdh.sl_layton = 0;

                    var slxk = db.md_kho_giaodich.Where(s =>
                        s.ngaychuyen >= khmvt.tungay
                        & s.ngaychuyen <= khmvt.denngay
                        & s.kieuchuyen == Helper.XuatKho
                        & !s.dongnhapxuat.StartsWith("PVC")
                        & s.md_sanpham_id == khmvt_cdh.md_sanpham_id)
                        .ToList().Sum(s => s.soluong_dichchuyen.GetValueOrDefault(0));
                    khmvt_cdh.sl_xuatkho = slxk;

                    var sldn = (khmvt_cdh.sl_can.GetValueOrDefault(0) - khmvt_cdh.sl_layton.GetValueOrDefault(0)
                                + khmvt_cdh.sl_tonkho_toithieu.GetValueOrDefault(0) - khmvt_cdh.sl_tonkho.GetValueOrDefault(0)).Set0WhenlessThan0().GetValueOrDefault(0);
                    khmvt_cdh.sl_denghi = sldn;
                    khmvt_cdh.sl_duyet = null;
                }
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if (msg.Length <= 0)
        {

        }
        else
        {
            msg = string.Format(@"<div style=""color:red"">Lỗi:{0}</div>", msg);
        }

        context.Response.Write(msg);
    }

    public void CA_01_khmvt_DOINCC(HttpContext context)
    {
        string msg = "";
        string md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
        string[] vnn = context.Request.Form["id"].Split(',');
        md_doitackinhdoanh_id = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == md_doitackinhdoanh_id).Select(s => s.md_doitackinhdoanh_id).FirstOrDefault();
        foreach (var khmvt_cdh in db.c_kehoachmuavt_cdh.Where(s => vnn.Contains(s.c_kehoachmuavt_cdh_id)).ToList())
        {
            khmvt_cdh.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
        }
        db.SaveChanges();
        msg = "Cập nhật các dòng hàng thành công.";
        context.Response.Write(msg);
    }

    public void CA_01_YeuCauSoluongKHMVT(HttpContext context)
    {
        string sel_val = context.Request.Form["sel_val"];
        decimal slvc_case = decimal.Parse(context.Request.Form["slvc_case"]);
        string[] vnn = context.Request.Form["id"].Split(',');
        string msg = "";
        c_kehoachmuavt khmvt = null;
        foreach (var khmvt_cdh in db.c_kehoachmuavt_cdh.Where(s => vnn.Contains(s.c_kehoachmuavt_cdh_id)).ToList())
        {
            if (khmvt == null)
            {
                khmvt = db.c_kehoachmuavt.Where(s => s.c_kehoachmuavt_id == khmvt_cdh.c_kehoachmuavt_id).Take(1).FirstOrDefault();
                if (khmvt.c_donmuavattu_id != " ")
                {
                    msg = "<div style='color:red'>lỗi: Kế hoạch \"" + khmvt.sochungtu + "\" đã tạo đơn mua vật tư, không thể chỉnh sửa.</div>";
                    break;
                }
            }

            var slc = khmvt_cdh.sl_can.GetValueOrDefault(0);
            var sltk = khmvt_cdh.sl_tonkho.GetValueOrDefault(0);
            var sltktt = khmvt_cdh.sl_tonkho_toithieu.GetValueOrDefault(0);
            var slyc = (slc - sltk + sltktt).Set0WhenlessThan0().GetValueOrDefault(0);
            khmvt_cdh.sl_denghi = slyc;

            if (sel_val == "0")
            {
                khmvt_cdh.sl_duyet = khmvt_cdh.sl_can.GetValueOrDefault(0);
                khmvt_cdh.sl_canthem = (khmvt_cdh.sl_duyet.GetValueOrDefault(0) - slyc).Set0WhenlessThan0();
            }
            else if (sel_val == "1")
            {
                khmvt_cdh.sl_duyet = khmvt_cdh.sl_denghi.GetValueOrDefault(0);
                khmvt_cdh.sl_canthem = (khmvt_cdh.sl_duyet.GetValueOrDefault(0) - slyc).Set0WhenlessThan0();
            }
            else if (sel_val == "2")
            {
                khmvt_cdh.sl_duyet = khmvt_cdh.sl_denghi.GetValueOrDefault(0) + khmvt_cdh.sl_canthem.GetValueOrDefault(0);
            }
            else
            {
                khmvt_cdh.sl_duyet = slvc_case;
                khmvt_cdh.sl_canthem = (khmvt_cdh.sl_duyet.GetValueOrDefault(0) - slyc).Set0WhenlessThan0();
            }
        }
        if (msg == "")
        {
            msg = "<div style='color:blue'>Duyệt số lượng cần mua thành công.</div>";
            db.SaveChanges();
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
        string mota_tiengviet = context.Request.Form["mota_tiengviet"];
        string md_doitackinhdoanh_id = context.Request.Form["md_doitackinhdoanh_id"];
        string id = context.Request.Form["id"];

        try
        {
            var sl_duyet = context.Request.Form["sl_duyet"].removeAllSpaceOrTrimText(true).ToNullableDecimal().GetValueOrDefault(0);
            var ngayPhaiCo = VNN_Config.setDateTime(context.Request.Form["ngayphaico"].removeAllSpaceOrTrimText(true));

            var object_ = db.c_kehoachmuavt_cdh.Where(p => p.c_kehoachmuavt_cdh_id == id).Take(1).FirstOrDefault();

            if (object_ == null)
            {
                msg = $@"Không tìm thấy đối tượng cần sửa ";
                goto EndEventHandler;
            }

            if (ngayPhaiCo == DateTime.MinValue | ngayPhaiCo == DateTime.MinValue.AddDays(1))
            {
                msg = $@"""Ngày phải có"" có giá trị sai";
                goto EndEventHandler;
            }

            var khmvt = db.c_kehoachmuavt.Where(s => s.c_kehoachmuavt_id == object_.c_kehoachmuavt_id).Take(1).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(khmvt.c_donmuavattu_id))
            {
                msg = $@"Kế hoạch đã tạo đơn mua vật tư, không thể chỉnh sửa";
                goto EndEventHandler;
            }

            if (khmvt.md_trangthai_id == Helper.HIEULUC)
            {
                msg = $@"Lỗi: Kế hoạch đã Hiệu Lực.";
                goto EndEventHandler;
            }
            var slc = object_.sl_can.GetValueOrDefault(0);
            var sltk = object_.sl_tonkho.GetValueOrDefault(0);
            var sltktt = object_.sl_tonkho_toithieu.GetValueOrDefault(0);
            var slyc = (slc - sltk + sltktt).Set0WhenlessThan0().GetValueOrDefault(0);
            object_.sl_duyet = sl_duyet;
            object_.sl_denghi = slyc;
            object_.sl_canthem = (sl_duyet - slyc).Set0WhenlessThan0();
            object_.sl_layton = 0;
            object_.md_doitackinhdoanh_id = md_doitackinhdoanh_id;
            object_.ngayphaico = ngayPhaiCo;
            object_.nguoicapnhat = userTK.ad_user_id;
            object_.value_nguoicapnhat = userTK.ma_user;
            object_.ngaycapnhat = DateTime.Now;
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if(msg.Length <= 0)
        {
            msg = "true#Cập nhật thành công";
        }
        else
        {
            msg = $@"false#{msg}";
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