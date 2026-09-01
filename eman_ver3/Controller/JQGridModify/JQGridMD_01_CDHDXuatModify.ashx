<%@ WebHandler Language="C#" Class="JQGridMD_01_CDHDXuatModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
using Newtonsoft.Json;
public class JQGridMD_01_CDHDXuatModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_CapnhatSLXNB":
                this.CA_01_CapnhatSLXNB(context);
                break;
            case "loadPBSL":
                this.loadPBSL(context);
                break;
            case "CA_01_PhanBoSoLuongTheoTo":
                this.CA_01_PhanBoSoLuongTheoTo(context);
                break;
            case "CA_01_GhepVatTuXuat":
                this.CA_01_GhepVatTuXuat(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_GhepVatTuXuat(HttpContext context)
    {
        string id = context.Request.Form["id"];
        string idpr = context.Request.Form["idpr"];
        string[] vnn = id.Split(',');
        string type = context.Request.Form["type"];
        string msg = "";
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var xknb = db.md_xuatkhonb.Where(s => s.md_xuatkhonb_id == idpr).FirstOrDefault();
                if (xknb == null)
                {
                    msg = "Không tìm thấy phiếu xuất kho";
                }
                else if (xknb.trangthai == "HIEULUC")
                {
                    msg = @"Phiếu xuất kho đã ""Hiệu Lực""";
                }
                else
                {
                    var tbl = db.md_xuatkhonb_cdh.Where(s => vnn.Contains(s.md_xuatkhonb_cdh_id)).ToList();
                    if (type == "1")
                    {
                        tbl = db.md_xuatkhonb_cdh.Where(s => s.md_xuatkhonb_id == idpr).ToList();
                    }

                    var groupTBL = tbl.GroupBy(s => new { s.md_sanpham_id, s.md_donvitinhsanpham_id, s.md_donvitinhsanpham_id2 }).Select(s => new md_xuatkhonb_cdh
                    {
                        md_xuatkhonb_cdh_id = Helper.getNewId(),
                        md_sanpham_id = s.Key.md_sanpham_id,
                        md_donvitinhsanpham_id = s.Key.md_donvitinhsanpham_id2,
                        md_donvitinhsanpham_id2 = s.Key.md_donvitinhsanpham_id2,
                        lsx_to = string.Join(",", s.Select(t => t.lsx_to).OrderByDescending(t => t).ToList()),
                        sl_daxuat = s.Select(t => t.sl_daxuat).Sum(),
                        sl_xuat2 = 0,
                        sl_xuat = 0,
                        tong_sl_xuat = s.Select(t => t.tong_sl_xuat).Sum(),
                        sl_muonxuat = s.Select(t => t.sl_muonxuat).Sum(),
                        sl_thucxuat = 0,
                        md_xuatkhonb_id = idpr,
                        ghino = s.Select(t => t.ghino).Sum(),
                        saiso = s.Select(t => t.saiso).Sum(),
                        bophancapnhat = userTK.md_phongban_id,
                        bophantao = userTK.md_phongban_id,
                        nguoicapnhat = userTK.ad_user_id,
                        nguoitao = userTK.ad_user_id,
                        vaitrocapnhat = userTK.ad_role_id,
                        vaitrotao = userTK.ad_role_id,
                        value_bophancapnhat = userTK.ten_phongban,
                        value_bophantao = userTK.ten_phongban,
                        value_nguoicapnhat = userTK.ma_user,
                        value_nguoitao = userTK.ma_user,
                        value_vaitrocapnhat = userTK.ten_role,
                        value_vaitrotao = userTK.ten_role,
                        ngaycapnhat = DateTime.Now,
                        ngaytao = DateTime.Now,
                        hoatdong = true,
                        mota = ""
                    });

                    db.md_xuatkhonb_cdh.RemoveRange(tbl);
                    foreach (var item in groupTBL.ToList())
                    {
                        db.md_xuatkhonb_cdh.Add(item);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"<div style=""color:blue"">Ghép vật tư thành công</div>");
                transaction.Commit();
            }
            else
            {
                msg = string.Format(@"<div style=""color:red"">Lỗi:{0}</div>", msg);
                transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }

    public void CA_01_PhanBoSoLuongTheoTo(HttpContext context)
    {
        string msg = "";
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var jsonPost = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(context.Request.Form["jsonPost"]);
                string id = context.Request.Form["id"];
                foreach (var a in jsonPost)
                {
                    var b = a["items"].ToString();
                    var items = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(b);
                    foreach (var item in items)
                    {
                        string spid = item["md_sanpham_id"] + "", lsx_to = item["lsx_to"] + "", tong_sl_xuat = item["tong_sl_xuat"] + "";
                        var dongxk = db.md_xuatkhonb_cdh.Where(s =>
                            s.md_xuatkhonb_id == id
                            & s.md_sanpham_id == spid
                            & s.lsx_to == lsx_to).FirstOrDefault();

                        var tongsl = items.Where(s => s["md_sanpham_id"] + "" == spid).Select(s => decimal.Parse(s["tong_sl_xuat"] + "")).Sum();

                        if (dongxk == null)
                        {
                            msg = string.Format(@"HHVT ""{0}"" không tìm thấy", item["ma_sanpham"]);
                        }
                        else
                        {
                            decimal tslx = decimal.Parse(tong_sl_xuat);
                            if (dongxk.sl_toida_trongLSXTo2 < tslx)
                            {
                                msg = string.Format(@"HHVT ""{0}"" lỗi: SL xuất: {0}, SL tối đa {1}", tongsl, dongxk.sl_toida_trongLSXTo2);
                            }
                            else if (dongxk.sl_toida_trongLSXTo < tongsl)
                            {
                                msg = string.Format(@"HHVT ""{0}"" lỗi: Tổng SL: {0}, SL tối đa {1}", tongsl, dongxk.sl_toida_trongLSXTo);
                            }
                            else
                            {
                                dongxk.tong_sl_xuat = tslx;
                                dongxk.sl_xuat = dongxk.tong_sl_xuat;
                                dongxk.sl_xuat2 = dongxk.sl_xuat;
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                transaction.Commit();
            }
            else
            {
                msg = string.Format(@"<div style=""color:red"">Lỗi: {0}</div>", msg);
                transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }

    public void loadPBSL(HttpContext context)
    {
        string id_parent = context.Request.QueryString["id"];
        var dongXKNBs = db.md_xuatkhonb_cdh.Where(s => s.md_xuatkhonb_id == id_parent).ToList();
        var lsxTos = new List<Dictionary<string, object>>();
        var items = new List<Dictionary<string, object>>();
        foreach (var item in dongXKNBs)
        {
            var sp = db.md_sanpham.Where(s => s.md_sanpham_id == item.md_sanpham_id).FirstOrDefault();
            if (sp != null)
            {
                var lstTo = lsxTos.Where(s => s["ma_sanpham"] + "" == sp.ma_sanpham).FirstOrDefault();
                if (lstTo == null)
                {
                    lsxTos.Add(new Dictionary<string, object> {
                        { "md_sanpham_id", sp.md_sanpham_id },
                        { "ma_sanpham", sp.ma_sanpham },
                        { "ten_sanpham", sp.mota_tiengviet },
                        { "sl_toida", item.sl_toida_trongLSXTo },
                        { "items", null }
                    });
                }


                var to = new Dictionary<string, object>();
                to["md_sanpham_id"] = sp.md_sanpham_id;
                to["ma_sanpham"] = sp.ma_sanpham;
                to["lsx_to"] = item.lsx_to;
                to["tong_sl_xuat"] = item.tong_sl_xuat;
                items.Add(to);
            }
        }

        foreach (var item in lsxTos)
        {
            item["items"] = items.Where(s => s["md_sanpham_id"] + "" == item["md_sanpham_id"] + "").ToList();
        }

        context.Response.Write(JsonConvert.SerializeObject(lsxTos));
    }

    public void CA_01_CapnhatSLXNB(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        var rows = context.Request.Form["rows"];
        string id = context.Request.Form["id"];

        var vcnb = db.md_xuatkhonb.Where(s=>s.md_xuatkhonb_id == id).FirstOrDefault();
        if (vcnb.trangthai == Helper.HIEULUC)
        {
            msg = $@"Phiếu đã Hiệu Lực";
            goto EndEventHandler;
        }

        try
        {
            var dongHangs = JsonConvert.DeserializeObject<List<md_xuatkhonb_cdh>>(rows);
            foreach (var cdvc in db.md_xuatkhonb_cdh.Where(s => s.md_xuatkhonb_id == vcnb.md_xuatkhonb_id).ToList())
            {
                var dongHang = dongHangs.Where(s => s.md_xuatkhonb_cdh_id == cdvc.md_xuatkhonb_cdh_id).FirstOrDefault();
                if (dongHang != null)
                {
                    cdvc.sl_thucxuat = dongHang.sl_thucxuat;
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

    }

    public void edit(HttpContext context)
    {
        string msg = "", msgSuccess = "";
        string ma_module = context.Request.QueryString["ma_module"];
        var pbvt = context.Request.Form["pbVT"] != null;

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                decimal sl_xuat = decimal.Parse(context.Request.Form["sl_thucxuat"]);
                decimal ghino = decimal.Parse(context.Request.Form["ghino"]);
                string id = context.Request.Form["id"];
                var object_ = db.md_xuatkhonb_cdh.Where(p => p.md_xuatkhonb_cdh_id == id).Take(1).FirstOrDefault();
                var xnb = db.md_xuatkhonb.Where(p => p.md_xuatkhonb_id == object_.md_xuatkhonb_id).Take(1).FirstOrDefault();
                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == object_.md_sanpham_id).FirstOrDefault();

                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else if (sl_xuat <= 0)
                {
                    msg = "Số lượng xuất phải lớn hớn 0";
                }
                else if (sl_xuat > object_.sl_muonxuat)
                {
                    msg = "Số lượng xuất tối đa là " + object_.sl_muonxuat.GetValueOrDefault(0).DropTrailingZeros();
                }
                else if (xnb.trangthai == "HIEULUC")
                {
                    msg = @"Phiếu xuất kho đã ""Hiệu Lực""!";
                }
                else
                {
                    var slST = db.md_xuatkhonb_cdh.Where(s => s.md_xuatkhonb_id == object_.md_xuatkhonb_id
                        & s.md_sanpham_id == object_.md_sanpham_id
                        & s.md_xuatkhonb_cdh_id != object_.md_xuatkhonb_cdh_id).ToList().Sum(s => s.sl_thucxuat.GetValueOrDefault(0));

                    var tongSLMX = slST + sl_xuat;
                    var sltkTT = Helper.soLuongTonKhoThucTe(xnb.tukho, sp.md_sanpham_id, db);
                    if (sltkTT < tongSLMX)
                    {
                        if (pbvt == false)
                        {
                            msg = @"Số lượng tồn kho thực tế là: " + sltkTT.DropTrailingZeros();
                            msg += string.Format(@"<br>SL muốn xuất ở dòng đang chọn: {0}", sl_xuat.DropTrailingZeros());
                            msg += string.Format(@"<br>SL muốn xuất ở các dòng khác: {0}", slST.DropTrailingZeros());
                            msg += string.Format(@"<br>Tổng SL muốn xuất: {0}", tongSLMX.DropTrailingZeros());
                        }
                    }
                    else if (xnb.bosung == 4)
                    {
                        var lsxTsx = Extension.getLSXToId2(object_.lsx_to, db);
                        var lsxTSX_prevChk = Extension.getLSXToId_Prev(lsxTsx.tsx, db);
                        var spCDT = db.md_lenhsanxuat_tosx_cdh.Where(
                            s => s.md_lenhsanxuat_tosx_id == lsxTSX_prevChk.md_lenhsanxuat_tosx_id
                            & s.md_sanpham_id == object_.md_sanpham_id
                            ).FirstOrDefault();

                        foreach (var ddhpx_cdh in db.md_lenhsanxuat_tosx_cdh.Where(s =>
                                s.md_lenhsanxuat_tosx_id == lsxTSX_prevChk.md_lenhsanxuat_tosx_id
                                & s.md_sanpham_id == sp.md_sanpham_id).ToList())
                        {
                            var sltdChuyen = ddhpx_cdh.sl_dahoanthanh.GetValueOrDefault(0) - ddhpx_cdh.sl_dagiao.GetValueOrDefault(0);
                            if (sltdChuyen < sl_xuat)
                            {
                                msg = string.Format(@"
                                Lỗi: <b>""{0}"" thuộc ĐH ""({4})""</b>
                                <br>- SL xuất lớn hơn (SL đã nhập - SL đã giao).
                                <br>-- SL xuất:  <b>""{1}""</b>, SL đã nhập:  <b>""{2}""</b>, SL đã giao:  <b>""{3}""</b>
                            ",
                                sp.ma_sanpham
                                , sl_xuat.DropTrailingZeros()
                                , ddhpx_cdh.sl_dahoanthanh.GetValueOrDefault(0).DropTrailingZeros()
                                , ddhpx_cdh.sl_dagiao.GetValueOrDefault(0).DropTrailingZeros()
                                , object_.tenhang
                                );
                                break;
                            }
                        };

                        if (spCDT.sl_dahoanthanh.GetValueOrDefault(0) < sl_xuat)
                            msg = @"Số lượng nhập của công đoạn trước chỉ có: " + spCDT.sl_dahoanthanh.GetValueOrDefault(0).DropTrailingZeros();
                    }

                    if (msg.Length <= 0)
                    {
                        object_.ghino = (sl_xuat - object_.tong_sl_xuat + object_.truno).GetValueOrDefault(0).Set0WhenlessThan0();
                        object_.sl_thucxuat = sl_xuat;
                        db.SaveChanges();
                        msgSuccess = "Cập nhật thành công.(--)" + object_.sl_xuat2;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"true#{0}", msgSuccess);
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
        string ma_module = context.Request.QueryString["ma_module"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                foreach (var id_del_ in ids)
                {
                    var object_ = db.md_xuatkhonb_cdh.Where(p => p.md_xuatkhonb_cdh_id == id_del_).Take(1).FirstOrDefault();
                    if (object_ == null)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                    }
                    else
                    {
                        var nb = db.md_xuatkhonb.Where(s => s.md_xuatkhonb_id == object_.md_xuatkhonb_id).FirstOrDefault();
                        var xnb_cdhs = from a in db.md_vanchuyennoibo_cdvc
                                       join b in db.md_vanchuyennoibo on a.md_vanchuyennoibo_id equals b.md_vanchuyennoibo_id
                                       where b.chungtuthamchieu == nb.sochungtu & a.md_sanpham_id == object_.md_sanpham_id
                                       select new { a.md_vanchuyennoibo_cdvc_id };
                        if (nb.trangthai == "HIEULUC")
                        {
                            msg += string.Format(@"<br><b>{0}</b>: Đã được ""Hiệu Lực"".", nb.sochungtu);
                        }
                        else if (xnb_cdhs.Count() > 0)
                        {
                            msg += string.Format(@"<br><b>{0}</b>: có phiếu vận chuyển nội bộ đang sử dụng dòng hàng này.", nb.sochungtu);
                        }
                    }

                    if (msg.Length <= 0)
                    {
                        db.md_xuatkhonb_cdh.Remove(object_);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"true#Xóa thành công.");
                transaction.Commit();
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = string.Format(@"false#{0}", msg.Substring(4));
                transaction.Rollback();
            }
        }
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