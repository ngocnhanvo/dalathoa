<%@ WebHandler Language="C#" Class="JQGridMD_02_GiaSPJQGSModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;

public class JQGridMD_02_GiaSPJQGSModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_NhapLieuGiaHHVT":
                this.CA_01_NhapLieuGiaHHVT(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_NhapLieuGiaHHVT(HttpContext context)
    {
        var msg = new List<string>();
        var jsonStr = context.Request.Form["json"];
        var id = context.Request.Form["id_parent"];
        var ma_module = context.Request.QueryString["ma_module"];
        try
        {
            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonStr);
            var hasErr = false;
            foreach (var item in json)
            {
                string bg = item.ElementAt(0).Value.removeAllSpaceOrTrimText(true);
                string pbg = item.ElementAt(1).Value.removeAllSpaceOrTrimText(true);
                string mhhvt = item.ElementAt(2).Value.removeAllSpaceOrTrimText(true);
                string dvt = item.ElementAt(3).Value.removeAllSpaceOrTrimText(true);
                string gia = item.ElementAt(4).Value.removeAllSpaceOrTrimText(true);
                string mota = item.ElementAt(5).Value.removeAllSpaceOrTrimText(true);

                string msgDT = "";
                var banggia = db.md_banggia.Where(s => s.ten_banggia == bg).FirstOrDefault();
                if (banggia == null)
                {
                    msgDT = $@"<div style='color:red' error>Không tìm thấy <b>bảng giá</b></div>";
                }
                else
                {
                    var phienbanggia = db.md_phienbangia.Where(s =>
                        s.md_banggia_id == banggia.md_banggia_id &
                        s.ten_phienbangia == pbg).FirstOrDefault();
                    if (phienbanggia == null)
                    {
                        msgDT = $@"<div style='color:red' error>Không tìm thấy <b>phiên bảng giá</b></div>";
                    }
                    else if (phienbanggia.trangthai != Helper.SOANTHAO)
                    {
                        msgDT = $@"<div style='color:red' error>Phiên bảng giá đã Hiệu Lực</div>";
                    }
                    else
                    {
                        var hhvt = db.md_sanpham.Where(s => s.ma_sanpham == mhhvt).FirstOrDefault();
                        if (hhvt == null)
                        {
                            msgDT = $@"<div style='color:red' error>Không tìm thấy <b>HHVT</b></div>";
                        }
                        else
                        {
                            var gia_isNumeric = decimal.TryParse(gia, out _);
                            if (!gia_isNumeric)
                            {
                                msgDT = $@"<div style='color:red' error>Giá có giá trị sai</div>";
                            }
                            else if (gia.ToNullableDecimal() <= 0)
                            {
                                msgDT = $@"<div style='color:red' error>Giá phải lơn hơn 0</div>";
                            }
                            else
                            {
                                var gspServer = db.md_giasanpham.Where(s =>
                                    s.md_phienbangia_id == phienbanggia.md_phienbangia_id &
                                    s.md_sanpham_id == hhvt.md_sanpham_id).FirstOrDefault();

                                var gsp = db.md_giasanpham.Local.Where(s =>
                                    s.md_phienbangia_id == phienbanggia.md_phienbangia_id &
                                    s.md_sanpham_id == hhvt.md_sanpham_id).FirstOrDefault();

                                var addGSP = gsp == null;
                                if (addGSP)
                                {
                                    gsp = new md_giasanpham();
                                    gsp.md_giasanpham_id = Helper.getNewId();
                                    gsp.md_banggia_id = banggia.md_banggia_id;
                                    gsp.md_phienbangia_id = phienbanggia.md_phienbangia_id;
                                    gsp.md_sanpham_id = hhvt.md_sanpham_id;
                                    gsp.gia = gia.ToNullableDecimal();
                                    gsp.md_donvitinhsanpham_id = hhvt.md_donvitinhsanpham_id;
                                    gsp.ngaytao = DateTime.Now;
                                    gsp.ngaycapnhat = DateTime.Now;
                                    gsp.nguoitao = Security.id_taikhoan(context);
                                    gsp.nguoicapnhat = gsp.nguoitao;
                                    gsp.mota = mota;
                                    gsp.hoatdong = true;
                                    db.md_giasanpham.Add(gsp);
                                    msgDT = $@"<div style='color:blue'>Đạt (add)</div>";
                                }
                                else
                                {
                                    gsp.gia = gia.ToNullableDecimal();
                                    gsp.md_donvitinhsanpham_id = hhvt.md_donvitinhsanpham_id;
                                    gsp.ngaycapnhat = DateTime.Now;
                                    gsp.nguoicapnhat = Security.id_taikhoan(context);
                                    gsp.mota = mota;
                                    msgDT = $@"<div style='color:blue'>Đạt (edit)</div>";
                                }
                            }
                        }
                    }
                }


                if (!string.IsNullOrWhiteSpace(msgDT))
                {
                    if (msgDT.LastIndexOf("error") > -1)
                        hasErr = true;
                    msg.Add(msgDT);
                }
            }

            if (!hasErr)
            {
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg.Add(ex.ToString());
        }

        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(msg));
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];

        try
        {
            string bangGiaID = "";
            string id = context.Request.Form["id_parent"];
            string ma_sanpham = context.Request.Form["md_sanpham_id"];
            string donvitinh = context.Request.Form["md_donvitinhsanpham_id"];

            var pbg = db.md_phienbangia.Where(s => s.md_phienbangia_id == id).FirstOrDefault();
            if (pbg == null)
            {
                msg = $@"Phiên bản giá không tồn tại.";
                goto EndEventHandler;
            }

            if (pbg.trangthai != Helper.SOANTHAO)
            {
                msg = $@"Phiên bản giá đã ""Hiệu Lực"".";
                goto EndEventHandler;
            }

            var sp = db.md_sanpham.Where(s => s.ma_sanpham == ma_sanpham).Take(1).FirstOrDefault();
            if (sp == null)
            {
                msg = $@"Mã HHVT ""{ma_sanpham}"" không tồn tại.";
                goto EndEventHandler;
            }

            bangGiaID = pbg.md_banggia_id;
            string chk_sp = db.md_giasanpham.
                    Where(s => s.md_sanpham_id == sp.md_sanpham_id &
                    s.md_phienbangia_id == id &
                    s.md_donvitinhsanpham_id == donvitinh
                    ).Select(s => s.md_sanpham_id).Take(1).FirstOrDefault();
            if (chk_sp != null & chk_sp != "")
            {
                msg = $@"Mã HHVT ""{ma_sanpham}"" đã tồn tại.";
                goto EndEventHandler;
            }

            if (msg.Length <= 0)
            {
                var gsp = new md_giasanpham();
                gsp.md_giasanpham_id = id_new;
                gsp.md_banggia_id = bangGiaID;
                gsp.md_phienbangia_id = id;
                gsp.md_sanpham_id = sp.md_sanpham_id;
                gsp.gia = decimal.Parse(context.Request.Form["gia"]);
                gsp.md_donvitinhsanpham_id = context.Request.Form["md_donvitinhsanpham_id"];
                gsp.ngaytao = DateTime.Now;
                gsp.ngaycapnhat = DateTime.Now;
                gsp.nguoitao = Security.id_taikhoan(context);
                gsp.nguoicapnhat = gsp.nguoitao;
                gsp.mota = context.Request.Form["mota"];
                gsp.hoatdong = true;
                db.md_giasanpham.Add(gsp);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"true#Thêm mới thành công#{id_new}";
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = $@"false#{msg}";
        }

        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];
        string ma_sanpham = context.Request.Form["md_sanpham_id"];
        string donvitinh = context.Request.Form["md_donvitinhsanpham_id"];

        try
        {
            var sp = db.md_sanpham.Where(s => s.ma_sanpham == ma_sanpham).Take(1).FirstOrDefault();
            var object_ = db.md_giasanpham.Where(p => p.md_giasanpham_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Không tìm thấy đối tượng cần sửa ";
                goto EndEventHandler;
            }

            var pbg = db.md_phienbangia.Where(s => s.md_phienbangia_id == object_.md_phienbangia_id).FirstOrDefault();
            if (pbg == null)
            {
                msg = $@"Phiên bản giá không tồn tại.";
                goto EndEventHandler;
            }

            if (pbg.trangthai != Helper.SOANTHAO)
            {
                msg = $@"Phiên bản giá đã ""Hiệu Lực"".";
                goto EndEventHandler;
            }

            if (sp == null)
            {
                msg = $@"Mã HHVT ""{ma_sanpham}"" không tồn tại.";
                goto EndEventHandler;
            }

            string chk_sp = db.md_giasanpham.
                    Where(s =>
                    s.md_sanpham_id == sp.md_sanpham_id &
                    s.md_phienbangia_id == id &
                    s.md_donvitinhsanpham_id == donvitinh &
                    s.md_sanpham_id != object_.md_sanpham_id)
                    .Select(s => s.md_sanpham_id).Take(1).FirstOrDefault();
            if (chk_sp != null & chk_sp != "")
            {
                msg = $@"Mã HHVT ""{ma_sanpham}"" đã tồn tại.";
                goto EndEventHandler;
            }

            if (msg.Length <= 0)
            {
                //var giaChoDuyet = context.Request.Form["giachoduyet"].ToNullableDecimal();
                //if (giaChoDuyet != null)
                //{
                //    var sxss = db.md_giasanpham_giaodich.Where(s => s.md_phienbangia_id == object_.md_phienbangia_id & s.sapxepso != null).ToList();
                //    int? sapxepsoMax = null;
                //    if (sxss.Count > 0)
                //        sapxepsoMax = sxss.Max(s => s.sapxepso.GetValueOrDefault(0));

                //    var gd = db.md_giasanpham_giaodich.Where(s => s.md_giasanpham_id == object_.md_giasanpham_id & s.trangthai == Helper.CHODUYET).FirstOrDefault();
                //    var add = gd == null;
                //    if (add)
                //    {
                //        gd = new md_giasanpham_giaodich();
                //        gd.md_giasanpham_giaodich_id = Helper.getNewId();
                //        gd.md_giasanpham_id = object_.md_giasanpham_id;
                //        gd.md_banggia_id = object_.md_banggia_id;
                //        gd.md_phienbangia_id = object_.md_phienbangia_id;
                //        gd.md_donvitinhsanpham_id = object_.md_donvitinhsanpham_id;
                //        gd.md_sanpham_id = object_.md_sanpham_id;
                //        gd.trangthai = Helper.CHODUYET;
                //        gd.ngaydenghi = DateTime.Now;
                //        gd.hoatdong = true;
                //    }
                //    gd.sapxepso = sapxepsoMax.GetValueOrDefault(0) + 1;
                //    gd.giacu = object_.gia;
                //    gd.giamoi = giaChoDuyet;
                //    gd = Helper.setDefaultValueWhenInsertOrUpdate(gd, userTK, !add);

                //    if (add)
                //    {
                //        db.md_giasanpham_giaodich.Add(gd);
                //    }

                //    var gdlqs = db.md_giasanpham_giaodich.Where(s => s.md_phienbangia_id == object_.md_phienbangia_id & s.md_sanpham_id == object_.md_sanpham_id).ToList();
                //    foreach (var gdlq in gdlqs)
                //    {
                //        gdlq.sapxepso = gd.sapxepso;
                //    }
                //}

                VNN_Function.SetFormValue(object_.nameof(s => s.md_sanpham_id), Helper.VNN_notpost);
                VNN_Function.SetFormValue(object_.nameof(s => s.md_banggia_id), Helper.VNN_notpost);
                VNN_Function.SetFormValue(object_.nameof(s => s.md_phienbangia_id), Helper.VNN_notpost);
                VNN_Function.SetFormValue(object_.nameof(s => s.md_donvitinhsanpham_id), Helper.VNN_notpost);
                //VNN_Function.SetFormValue(object_.nameof(s => s.gia), Helper.VNN_notpost);
                object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"true#Cập nhật thành công";
            VNN_Function.loaddulieu_Auto(db, ma_module);
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
        string ma_module = context.Request.QueryString["ma_module"];
        string id_del = context.Request.Form["id"];

        try
        {
            var object_ = db.md_giasanpham.Where(p => p.md_giasanpham_id == id_del).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = $@"Không tìm thấy đối tượng cần xóa";
                goto EndEventHandler;
            }

            var pbg = db.md_phienbangia.Where(s => s.md_phienbangia_id == object_.md_phienbangia_id).FirstOrDefault();
            if (pbg == null)
            {
                msg = $@"Phiên bản giá không tồn tại.";
                goto EndEventHandler;
            }

            if (pbg.trangthai != Helper.SOANTHAO)
            {
                msg = $@"Phiên bản giá đã ""Hiệu Lực"".";
                goto EndEventHandler;
            }

            var pbg001s = db.md_vanchuyennoibo.Where(s => s.phienbangiaNC == object_.md_phienbangia_id).Take(1).Count();
            var pbg002s = db.md_xuatkhonb.Where(s => s.phienbangiaNC == object_.md_phienbangia_id).Take(1).Count();
            var pbg003s = db.c_donmuahang.Where(s => s.md_phienbangia_id == object_.md_phienbangia_id).Take(1).Count();
            if (pbg001s > 0)
                msg = $@"Phiên bảng giá đã được áp dụng cho phiếu chuyển kho";
            else if (pbg002s > 0)
                msg = $@"Phiên bảng giá đã được áp dụng cho phiếu xuất kho nội bộ";
            else if (pbg003s > 0)
                msg = $@"Phiên bảng giá đã được áp dụng cho đơn mua hàng hóa vật tư";

            if (msg.Length <= 0)
            {
                foreach (var gd in db.md_giasanpham_giaodich.Where(s =>
                        s.md_giasanpham_id == object_.md_giasanpham_id
                        & s.trangthai == Helper.CHODUYET).ToList())
                {
                    db.md_giasanpham_giaodich.Remove(gd);
                }

                var sp = db.md_sanpham.Where(s => s.md_sanpham_id == object_.md_sanpham_id).Select(s => s.ma_sanpham).FirstOrDefault();
                VNN_Function.Write_log(context, ma_module, null, oper, "PBG:" + pbg.ten_phienbangia + ", MSP:" + sp, db);
                db.md_giasanpham.Remove(object_);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = $@"true#Xóa giá sản phẩm thành công";
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = $@"false#{msg}";
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
