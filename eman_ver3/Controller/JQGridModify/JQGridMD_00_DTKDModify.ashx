<%@ WebHandler Language="C#" Class="JQGridMD_00_DTKDModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;

public class JQGridMD_00_DTKDModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_UpdateDTKD":
                this.CA_01_UpdateDTKD(context);
                break;
            default:
                break;
        }
    }
    private md_loaidtkd layLoaiDTKD_KhachHang()
    {
        var loaiKH = db.md_loaidtkd.Where(s => s.ma_loaidtkd == "KH").FirstOrDefault();
        return loaiKH;
    }
    private md_quocgia layQuocGia_VietNam()
    {
        var qg = db.md_quocgia.Where(s => s.ma_quocgia == "VN").FirstOrDefault();
        return qg;
    }
    private md_khuvuc layKhuVuc_Asia()
    {
        var kv = db.md_khuvuc.Where(s => s.ma_khuvuc == "ASIA").FirstOrDefault();
        return kv;
    }
    public void CA_01_UpdateDTKD(HttpContext context)
    {
        var msg = new List<string>();
        var msgErrs = new List<string>();
        var jsonStr = context.Request.Form["json"];
        var id = context.Request.Form["id_parent"];
        var ma_module = context.Request.QueryString["ma_module"];
        try
        {
            var loaiKH = layLoaiDTKD_KhachHang();
            if (loaiKH == null)
            {
                msgErrs.Add($@"Không tìm thấy loại đối tác có mã ""KH""");
            }

            var quocgiaVN = layQuocGia_VietNam();
            if (quocgiaVN == null)
            {
                msgErrs.Add($@"Không tìm thấy quốc gia có mã ""VN""");
            }

            var khuvucAsia = layKhuVuc_Asia();
            if (khuvucAsia == null)
            {
                msgErrs.Add($@"Không tìm thấy khu vực có mã ""ASIA""");
            }

            if (msgErrs.Count > 0)
                goto EndEventHandler;

            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonStr);
            var hasErr = false;
            int count = 0;
            foreach (var item in json)
            {
                count++;
                string msgDT = "";
                string maDTKD = item.ElementAt(0).Value.removeAllSpaceOrTrimText(true);
                string tenDTKD = item.ElementAt(1).Value.removeAllSpaceOrTrimText(true);
                string dienThoai = item.ElementAt(2).Value.removeAllSpaceOrTrimText(true);
                string diaChi = item.ElementAt(3).Value.removeAllSpaceOrTrimText(true);
                string noCanThu = item.ElementAt(4).Value.removeAllSpaceOrTrimText(true);
                string tongBan = item.ElementAt(5).Value.removeAllSpaceOrTrimText(true);
                string masoThue = item.ElementAt(6).Value.removeAllSpaceOrTrimText(true);
                string ghichuThem = item.ElementAt(7).Value.removeAllSpaceOrTrimText(true);

                var dtkdServer = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == maDTKD).FirstOrDefault();

                var dtkd = db.md_doitackinhdoanh.Local.Where(s => s.ma_dtkd == maDTKD).FirstOrDefault();

                var addVT = dtkd == null;
                if (addVT)
                {
                    dtkd = new md_doitackinhdoanh();
                    dtkd.md_doitackinhdoanh_id = Helper.getNewId();
                    if (new string[] { "tự động", "auto" }.Contains(maDTKD.ToLower()))
                        maDTKD = VNN_VariablePublic.sochungtu(db, "KH", count, false);
                    dtkd.ma_dtkd = maDTKD;
                    dtkd.ten_dtkd = tenDTKD;
                    dtkd.tel = dienThoai;
                    dtkd.diachi = diaChi;
                    dtkd.tong_congno = noCanThu.ToNullableDecimal();
                    dtkd.tong_muaban = tongBan.ToNullableDecimal();
                    dtkd.md_loaidtkd_id = loaiKH.md_loaidtkd_id;
                    dtkd.md_quocgia_id = quocgiaVN.md_quocgia_id;
                    dtkd.md_khuvuc_id = khuvucAsia.md_khuvuc_id;
                    dtkd.masothue = masoThue;
                    dtkd = Helper.setDefaultValueWhenInsertOrUpdate(dtkd, userTK, false);
                    db.md_doitackinhdoanh.Add(dtkd);
                    msgDT = $@"<div style='color:blue'>Đạt (add)</div>";
                }
                else
                {
                    if (dtkd.md_loaidtkd_id != loaiKH.md_loaidtkd_id)
                    {
                        msgDT = $@"<div error style='color:red'>Không phải khách hàng</div>";
                    }
                    else
                    {
                        dtkd.ten_dtkd = tenDTKD;
                        dtkd.tel = dienThoai;
                        dtkd.diachi = diaChi;
                        dtkd.tong_congno = noCanThu.ToNullableDecimal();
                        dtkd.tong_muaban = tongBan.ToNullableDecimal();
                        dtkd.md_loaidtkd_id = loaiKH.md_loaidtkd_id;
                        dtkd.md_quocgia_id = quocgiaVN.md_quocgia_id;
                        dtkd.md_khuvuc_id = khuvucAsia.md_khuvuc_id;
                        dtkd.masothue = masoThue;
                        dtkd = Helper.setDefaultValueWhenInsertOrUpdate(dtkd, userTK, true);
                        msgDT = $@"<div style='color:blue'>Đạt (edit)</div>";
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
            msg.Add($@"<div error style='color:red'>{ex.ToString()}</div>");
        }

    EndEventHandler:;

        if (msgErrs.Count > 0)
        {
            var wrappedList = msgErrs
                .Select(s => $"<div error style='color:red'>{s}</div>")
                .ToList();
            msg.AddRange(wrappedList);
        }

        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(msg));
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            var loaiKH = layLoaiDTKD_KhachHang();
            var quocgiaVN = layQuocGia_VietNam();
            var khuvucAsia = layKhuVuc_Asia();
            string maDTKD = VNN_VariablePublic.sochungtu(db, "KH", 1, false);
            var exist = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == maDTKD).FirstOrDefault();
            if (exist != null)
            {
                msg = "Lỗi: Mã đối tác đã tồn tại";
                goto EndEventHandler;
            }

            var object_ = new md_doitackinhdoanh();
            object_.md_doitackinhdoanh_id = id_new;
            object_.md_loaidtkd_id = loaiKH.md_loaidtkd_id;
            object_.md_quocgia_id = quocgiaVN.md_quocgia_id;
            object_.md_khuvuc_id = khuvucAsia.md_khuvuc_id;
            VNN_Function.SetFormValue(object_.nameof(s => s.ma_dtkd), maDTKD);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            object_.hoatdong = true;
            db.md_doitackinhdoanh.Add(object_);
            db.SaveChanges();
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
        string maDTKD = context.Request.Form["ma_dtkd"].removeAllSpaceOrTrimText(true);
        try
        {
            string id = context.Request.Form["id"];
            var object_ = db.md_doitackinhdoanh.Where(p => p.md_doitackinhdoanh_id == id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "Lỗi:Không tìm thấy đối tượng cần sửa";
                goto EndEventHandler;
            }

            var exist = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == maDTKD & s.md_doitackinhdoanh_id != object_.md_doitackinhdoanh_id).FirstOrDefault();
            if (exist != null)
            {
                msg = "Lỗi: Mã đối tác đã tồn tại";
                goto EndEventHandler;
            }

            if (maDTKD != object_.ma_dtkd)
            {
                var bgs = db.md_banggia.Where(s => s.lienket_bg == object_.ma_dtkd).ToList();
                foreach (var bg in bgs)
                {
                    bg.lienket_bg = maDTKD;
                }
            }
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = string.Format(@"true#Cập nhật thành công");
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = string.Format(@"false#{0}", msg);
        }

        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];

        try
        {
            string id_del = context.Request.Form["id"];

            var object_ = db.md_doitackinhdoanh.Where(p => p.md_doitackinhdoanh_id == id_del).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "Lỗi:Không tìm thấy đối tượng cần xóa";
                goto EndEventHandler;
            }

            var pbg001s = db.c_danhsachdathang.Where(s => s.md_doitackinhdoanh_id == object_.md_doitackinhdoanh_id).Take(1).Count();
            var pbg002s = db.c_donmuahang.Where(s => s.md_doitackinhdoanh_id == object_.md_doitackinhdoanh_id).Take(1).Count();
            var pbg003s = db.md_xuatban.Where(s => s.md_doitackinhdoanh_id == object_.md_doitackinhdoanh_id).Take(1).Count();
            var pbg004s = db.md_nhapkho_ncc.Where(s => s.md_doitackinhdoanh_id == object_.md_doitackinhdoanh_id).Take(1).Count();
            var pbg005s = db.md_banggia.Where(s => s.lienket_bg == object_.ma_dtkd).Take(1).Count();
            var pbg006s = db.md_sanpham.Where(s => s.nhacungung == object_.md_doitackinhdoanh_id).Take(1).Count();
            if (pbg001s > 0)
                msg = "Lỗi:Đối tác kinh doanh đã được sử dụng trong đơn hàng";
            else if (pbg002s > 0)
                msg = "Lỗi:Đối tác kinh doanh đã được sử dụng trong đơn mua hàng hóa vật tư";
            else if (pbg003s > 0)
                msg = "Lỗi:Đối tác kinh doanh đã được sử dụng trong xuất bán";
            else if (pbg004s > 0)
                msg = "Lỗi:Đối tác kinh doanh đã được sử dụng trong nhập kho từ NCC";
            else if (pbg005s > 0)
                msg = "Lỗi:Đối tác kinh doanh đã được sử dụng trong bảng giá";
            else if (pbg006s > 0)
                msg = "Lỗi:Đối tác kinh doanh đã được sử dụng trong HHVT";

            if (msg.Length <= 0)
            {
                VNN_Function.Write_log(context, ma_module, null, oper, "MĐT:" + object_.ma_dtkd + ", TĐT:" + object_.ten_dtkd, db);
                db.md_doitackinhdoanh.Remove(object_);
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
            msg = string.Format("true#{0}", "Xóa đối tác kinh doanh thành công");
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = string.Format("false#{0}", msg);
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