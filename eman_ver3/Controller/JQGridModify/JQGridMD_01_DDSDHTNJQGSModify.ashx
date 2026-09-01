<%@ WebHandler Language="C#" Class="JQGridMD_01_DDSDHModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Data.SqlClient;
using DataAcess;
using System.Collections.Generic;

public class JQGridMD_01_DDSDHModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public EntityContext db = new EntityContext();
    public EntityFunction entityFunc = new EntityFunction();
    User_TK userTK = null;
    Public pub = new Public();

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
            case "CA_01_DSDHImport":
                this.CA_01_DSDHImport(context);
                break;
            case "add":
                this.add(context);
                break;
            case "edit":
                this.edit(context);
                break;
            case "del":
                this.del(context);
                break;
            case "selectBOM":
                this.selectBOM(context);
                break;

            default:
                break;
        }
    }

    public void CA_01_DSDHImport(HttpContext context)
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
                string msp = item.ElementAt(0).Value.removeAllSpaceOrTrimText(true);
                string sld = item.ElementAt(1).Value.removeAllSpaceOrTrimText(true);
                string giaban = item.ElementAt(2).Value.removeAllSpaceOrTrimText(true);
                string mta = item.ElementAt(3).Value.removeAllSpaceOrTrimText(true);

                string msgDT = "";
                var spid = db.md_sanpham.Where(s => s.ma_sanpham == msp).Select(s => s.md_sanpham_id).FirstOrDefault();
                var ddsdhServer = db.c_dongdsdh.Where(s => s.md_sanpham_id == spid & s.c_danhsachdathang_id == id).FirstOrDefault();
                var ddsdh = db.c_dongdsdh.Local.Where(s => s.md_sanpham_id == spid & s.c_danhsachdathang_id == id).FirstOrDefault();
                if (ddsdh == null)
                    msgDT = add(context, ma_module, id, msp, sld, giaban, mta, false);
                else
                    msgDT = edit(context, ma_module, ddsdh.c_dongdsdh_id, id, msp, sld, giaban, mta, false);

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
            msg.Add(ex.Message);
        }

        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(msg));
    }

    public void selectBOM(HttpContext context)
    {
        bool firstnull = bool.Parse(context.Request.QueryString["firstnull"]);
        string str = "<select>";

        str += firstnull ? string.Format(@"<option px="""" value=""""></option>") : "";
        foreach (var cn in db.md_sanpham_bom.Where(s =>
            string.IsNullOrEmpty(s.md_phanxuong_id) & string.IsNullOrEmpty(s.md_to_id))
                .OrderBy(s => s.trangthai).ToList())
        {
            str += string.Format(@"<option sp=""{2}"" value=""{0}"">{1}</option>", cn.md_sanpham_bom_id, cn.ten_phienban, cn.md_sanpham_id);
        }
        str += "</select>";
        context.Response.Write(str);
    }

    public string add(HttpContext context, string ma_module = "", string id = "", string ma_sanpham = "", string sldathang = "", string giaban = "", string mota = "", bool updated = true)
    {
        string msg = "", id_new = Helper.getNewId();
        string ten_phienban = context.Request.Form["md_sanpham_bom_id"];
        if (string.IsNullOrWhiteSpace(ma_module))
            ma_module = context.Request.QueryString["ma_module"];
        if (string.IsNullOrWhiteSpace(ma_sanpham))
            ma_sanpham = context.Request.Form["ma_sanpham"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(id))
            id = context.Request.Form["id_parent"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(sldathang))
            sldathang = context.Request.Form["sl_dathang"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(giaban))
            giaban = context.Request.Form["gianhap"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(mota))
            mota = context.Request.Form["mota"].removeAllSpaceOrTrimText(true);

        try
        {
            var sp = db.md_sanpham.Where(s => s.ma_sanpham == ma_sanpham).FirstOrDefault();
            if (sp == null)
            {
                msg = $"Mã <b>{ma_sanpham}</b> không tồn tại.";
                goto EndEventHandler;
            }

            var dsdhServer = db.c_danhsachdathang.Where(s => s.c_danhsachdathang_id == id).FirstOrDefault();
            var dsdh = db.c_danhsachdathang.Local.Where(s => s.c_danhsachdathang_id == id).FirstOrDefault();
            if (dsdh == null)
            {
                msg = $"Không tìm thấy đơn hàng.";
                goto EndEventHandler;
            }
            if (dsdh.trangthai != Helper.SOANTHAO)
            {
                msg = $@"Ðơn hàng ""{dsdh.sochungtu}"" đã xử lý.";
                goto EndEventHandler;
            }
            int ddsdh = db.c_dongdsdh.Where(s => s.md_sanpham_id == sp.md_sanpham_id & s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id).Count();
            if (ddsdh > 0)
            {
                msg = $"Đã được thêm trước đó.";
                goto EndEventHandler;
            }
            var sldathang_Val = sldathang.ToNullableDecimal();
            if (sldathang_Val == null)
            {
                msg = "SL đặt hàng không thể bỏ trống";
                goto EndEventHandler;
            }
            if (sldathang_Val <= 0)
            {
                msg = $"Số lượng đặt hàng phải lớn hơn 0";
                goto EndEventHandler;
            }
            var giaban_Val = giaban.removeAllSpaceOrTrimText(false).ToNullableDecimal();
            if (giaban_Val == null)
            {
                msg = $"Giá bán phải có giá trị";
                goto EndEventHandler;
            }
            if (giaban_Val < 0)
            {
                msg = $"Giá bán không thể có giá trị âm";
                goto EndEventHandler;
            }

            if (msg.Length <= 0)
            {
                var object_ = new c_dongdsdh();
                object_.c_dongdsdh_id = id_new;
                object_.c_danhsachdathang_id = id;
                object_.sl_conlai = sldathang_Val;
                object_.sl_dagiao = 0;
                object_.sl_hanngach = 0;
                object_.sl_giamhanngach = 0;
                object_.giadoichieu = giaban_Val;
                object_.phi = 0;
                object_.phidg = 0;
                VNN_Function.SetFormValue(object_.nameof(s => s.sl_dathang), sldathang_Val.ToString());
                VNN_Function.SetFormValue(object_.nameof(s => s.gianhap), giaban_Val.ToString());
                VNN_Function.SetFormValue(object_.nameof(s => s.md_sanpham_id), sp.md_sanpham_id);
                VNN_Function.SetFormValue(object_.nameof(s => s.mota), mota);
                object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
                object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                db.c_dongdsdh.Add(object_);
                if (updated)
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
            if (updated)
            {
                msg = $@"true#Thêm thành công#{id_new}";
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = $"<div msp='{ma_sanpham}' style='color:blue'>Ðã đạt</div>";
            }
        }
        else
        {
            if (updated)
                msg = $@"false#{msg}";
            else
                msg = $"<div msp='{ma_sanpham}' style='color:red' error>{msg}</div>";
        }

        if (updated)
            context.Response.Write(msg);
        return msg;
    }

    public string edit(HttpContext context, string ma_module = "", string id = "", string id_parent = "", string ma_sanpham = "", string sldathang = "", string giaban = "", string mota = "", bool updated = true)
    {
        string msg = "";
        int sl_dathang = 0;
        if (string.IsNullOrWhiteSpace(id))
            id = context.Request.Form["id"];
        if (string.IsNullOrWhiteSpace(ma_module))
            ma_module = context.Request.QueryString["ma_module"];
        if (string.IsNullOrWhiteSpace(ma_sanpham))
            ma_sanpham = context.Request.Form["ma_sanpham"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(id_parent))
            id_parent = context.Request.Form["id_parent"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(giaban))
            giaban = context.Request.Form["gianhap"].removeAllSpaceOrTrimText(true);
        if (string.IsNullOrWhiteSpace(sldathang))
            sldathang = context.Request.Form["sl_dathang"].removeAllSpaceOrTrimText(true);
        try
        {
            var object_Server = db.c_dongdsdh.Where(p => p.c_dongdsdh_id == id).FirstOrDefault();
            var object_ = db.c_dongdsdh.Local.Where(p => p.c_dongdsdh_id == id).FirstOrDefault();
            if (object_ == null)
            {
                msg = "Không tìm thấy dòng cần sửa";
                goto EndEventHandler;
            }

            var dsdh = db.c_danhsachdathang.Where(s => s.c_danhsachdathang_id == id_parent).FirstOrDefault();
            if (dsdh.trangthai != Helper.SOANTHAO)
            {
                msg = $@"Ðơn hàng ""{dsdh.sochungtu}"" không ở trạng thái Soạn Thảo";
                goto EndEventHandler;
            }

            var sp = db.md_sanpham.Where(s => s.ma_sanpham == ma_sanpham).FirstOrDefault();
            if (sp == null)
            {
                msg = $@"Mã hàng ""{ma_sanpham}"" không có trong dữ liệu gốc";
                goto EndEventHandler;
            }

            var sldathang_Val = sldathang.ToNullableDouble();
            if (sldathang_Val == null)
            {
                msg = "SL đặt hàng không thể bỏ trống";
                goto EndEventHandler;
            }
            if (sldathang_Val <= 0)
            {
                msg = $"Số lượng đặt hàng phải lớn hơn 0";
                goto EndEventHandler;
            }
            var giaban_Val = giaban.removeAllSpaceOrTrimText(false).ToNullableDecimal();
            if (giaban_Val == null)
            {
                msg = $"Giá bán phải có giá trị";
                goto EndEventHandler;
            }
            if (giaban_Val < 0)
            {
                msg = $"Giá bán không thể có giá trị âm";
                goto EndEventHandler;
            }

            object_.sl_conlai = sl_dathang;
            object_.sl_dagiao = 0;
            VNN_Function.SetFormValue(object_.nameof(s => s.gianhap), giaban.ToString());
            VNN_Function.SetFormValue(object_.nameof(s => s.sl_dathang), sldathang_Val.ToString());
            VNN_Function.SetFormValue(object_.nameof(s => s.md_sanpham_id), sp.md_sanpham_id);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
            if (updated)
                db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
        }

    EndEventHandler:;
        if (msg.Length <= 0)
        {
            if (updated)
            {
                msg = string.Format(@"true#Sửa thành công");
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = $"<div msp='{ma_sanpham}' style='color:blue'>Có thể sửa</div>";
            }
        }
        else
        {
            if (updated)
                msg = string.Format(@"false#{0}", msg);
            else
                msg = $"<div msp='{ma_sanpham}' style='color:red' error>{msg}</div>";
        }

        if (updated)
            context.Response.Write(msg);
        return msg;
    }

    public void del(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];

        try
        {
            var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            foreach (var id_del_ in ids)
            {
                var object_ = db.c_dongdsdh.Where(p => p.c_dongdsdh_id == id_del_).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy dòng cần xóa.", id_del_);
                }
                else
                {
                    var dsdh = db.c_danhsachdathang.Where(s => s.c_danhsachdathang_id == object_.c_danhsachdathang_id).FirstOrDefault();
                    if (dsdh.trangthai == Helper.HIEULUC)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Ðã xử lý.", dsdh.sochungtu);
                    }
                    else if (db.md_hanngach.Where(s => s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id).Count() > 0)
                    {
                        msg += string.Format(@"<br><b>{0}</b>: Ðã giảm hạn ngạch.", dsdh.sochungtu);
                    }
                    else
                    {
                        db.c_dongdsdh.Remove(object_);
                        db.SaveChanges();
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
            msg = string.Format(@"true#Xóa thành công");
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = string.Format(@"false#{0}", msg.Substring(4));
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