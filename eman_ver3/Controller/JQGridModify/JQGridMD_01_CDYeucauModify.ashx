<%@ WebHandler Language="C#" Class="JQGridMD_01_CDYeucauModify" %>
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
public class JQGridMD_01_CDYeucauModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            case "CA_01_NhapDHYCau":
                this.CA_01_NhapDHYCau(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_NhapDHYCau(HttpContext context)
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
                string sct = item.ElementAt(0).Value.removeAllSpaceOrTrimText(true);
                string maHHVT = item.ElementAt(1).Value.removeAllSpaceOrTrimText(true);
                string slyc = item.ElementAt(2).Value.removeAllSpaceOrTrimText(true);
                string mota = item.ElementAt(3).Value.removeAllSpaceOrTrimText(true);

                string msgDT = "";

                var ycmvt = db.c_yeucaumuavt.Where(s => s.sochungtu == sct).FirstOrDefault();
                if (ycmvt == null)
                {
                    msgDT = $@"<div style='color:red' error>Không tìm thấy <b>SCT/b></div>";
                }
                else if(!string.IsNullOrWhiteSpace(ycmvt.c_nhucauvattu_id))
                {
                    msgDT = $@"<div style='color:red' error>không áp dụng với <b>YCMVT theo BOM</b></div>";
                }
                else if(ycmvt.md_trangthai_id == Helper.HIEULUC)
                {
                    msgDT = $@"<div style='color:red' error>YCMVT đã <b>Hiệu Lực</b></div>";
                }
                else
                {
                    var hhvt = db.md_sanpham.Where(s => s.ma_sanpham == maHHVT).FirstOrDefault();
                    if (hhvt == null)
                    {
                        msgDT = $@"<div style='color:red' error>Không tìm thấy <b>HHVT/b></div>";
                    }
                    else
                    {
                        var isNumeric = decimal.TryParse(slyc, out _);
                        if(!isNumeric)
                        {
                            msgDT = $@"<div style='color:red' error><b>SL yêu cầu</b> có giá trị sai</div>";
                        }
                        else if (slyc.ToNullableDecimal() <= 0)
                        {
                            msgDT = $@"<div style='color:red' error><b>SL yêu cầu</b> phải lớn hơn 0</div>";
                        }
                        else
                        {

                            var vtServer = db.c_yeucaumuavt_cdh.Where(s => s.c_yeucaumuavt_id == id & s.md_sanpham_id == hhvt.md_sanpham_id).FirstOrDefault();

                            var vt = db.c_yeucaumuavt_cdh.Local.Where(s => s.c_yeucaumuavt_id == id & s.md_sanpham_id == hhvt.md_sanpham_id).FirstOrDefault();

                            var addVT = vt == null;
                            if (addVT)
                            {
                                vt = new c_yeucaumuavt_cdh();
                                vt.c_yeucaumuavt_cdh_id = Helper.getNewId();
                                vt.c_yeucaumuavt_id = ycmvt.c_yeucaumuavt_id;
                                vt.md_sanpham_id = hhvt.md_sanpham_id;
                                vt.md_donvitinhsanpham_id = hhvt.md_donvitinhsanpham_id;
                                vt.soluong_yeucau = slyc.ToNullableDecimal().GetValueOrDefault(0);
                                vt = Helper.setDefaultValueWhenInsertOrUpdate(vt, userTK, false);
                                db.c_yeucaumuavt_cdh.Add(vt);
                                msgDT = $@"<div style='color:blue'>Đạt (add)</div>";
                            }
                            else
                            {
                                vt.md_donvitinhsanpham_id = hhvt.md_donvitinhsanpham_id;
                                vt.soluong_yeucau = slyc.ToNullableDecimal().GetValueOrDefault(0);
                                vt = Helper.setDefaultValueWhenInsertOrUpdate(vt, userTK, true);
                                msgDT = $@"<div style='color:blue'>Đạt (edit)</div>";
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
            msg.Add($@"<div error style='color:red'>{ex.ToString()}</div>");
        }

        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(msg));
    }

    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string ma_sanpham = context.Request.Form["md_sanpham_id"];

        try
        {
            decimal soluong_yeucau = decimal.Parse(context.Request.Form["soluong_yeucau"]);
            var sp = db.md_sanpham.Where(s => s.ma_sanpham == ma_sanpham).Take(1).FirstOrDefault();
            string id = context.Request.Form["id_parent"];
            var ob = db.c_yeucaumuavt.Where(s => s.c_yeucaumuavt_id == id).FirstOrDefault();
            if (ob == null)
            {
                msg = "Lỗi: Không tìm thấy YCMVT.";
                goto EndEventHandler;
            }
            if (ob.md_trangthai_id == Helper.HIEULUC)
            {
                msg = "Lỗi: YCMVT đã 'Hiệu Lực'.";
                goto EndEventHandler;
            }
            if (!string.IsNullOrWhiteSpace(ob.c_nhucauvattu_id))
            {
                msg = $@"Lỗi: Yêu cầu mua ""{ob.sochungtu}"" có liên kết với ""Nhu cầu mua vật tư"".";
                goto EndEventHandler;
            }
            if (sp == null)
            {
                msg = $@"Lỗi: Mã vật tư ""{ma_sanpham}"" không tồn tại.";
                goto EndEventHandler;
            }
            if (soluong_yeucau <= 0)
            {
                msg = "Lỗi: Số lượng yêu cầu phải lớn hơn 0.";
                goto EndEventHandler;
            }
            var cdhYCMVT = db.c_yeucaumuavt_cdh.Where(s => s.md_sanpham_id == sp.md_sanpham_id & s.c_yeucaumuavt_id == ob.c_yeucaumuavt_id).FirstOrDefault();
            if (cdhYCMVT != null)
            {
                msg = $@"Lỗi: Mã vật tư ""{ma_sanpham}"" đã được thêm trước đó.";
                goto EndEventHandler;
            }

            var object_ = new c_yeucaumuavt_cdh();
            object_.c_yeucaumuavt_cdh_id = id_new;
            object_.c_yeucaumuavt_id = id;
            VNN_Function.SetFormValue(object_.nameof(s => s.md_sanpham_id), sp.md_sanpham_id);
            VNN_Function.SetFormValue(object_.nameof(s => s.md_donvitinhsanpham_id), sp.md_donvitinhsanpham_id);
            object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
            object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
            db.c_yeucaumuavt_cdh.Add(object_);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = string.Format(@"true#Thêm mới thành công#{0}", id_new);
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = string.Format(@"false#{0}", msg);
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        string id = context.Request.Form["id"];
        string ma_sanpham = context.Request.Form["md_sanpham_id"];

        try
        {
            decimal soluong_yeucau = decimal.Parse(context.Request.Form["soluong_yeucau"]);
            var sp = db.md_sanpham.Where(s => s.ma_sanpham == ma_sanpham).Take(1).FirstOrDefault();
            var object_ = db.c_yeucaumuavt_cdh.Where(p => p.c_yeucaumuavt_cdh_id == id).Take(1).FirstOrDefault();

            if (object_ == null)
            {
                msg = "Lỗi: Không tìm thấy đối tượng cần sửa.";
                goto EndEventHandler;
            }
            var ycmvt = db.c_yeucaumuavt.Where(p => p.c_yeucaumuavt_id.Equals(object_.c_yeucaumuavt_id)).Take(1).FirstOrDefault();
            if (ycmvt.md_trangthai_id == Helper.HIEULUC)
            {
                msg = $@"Lỗi: Yêu cầu mua ""{ycmvt.sochungtu}"" đã hiệu lực, không thể chỉnh sửa.";
                goto EndEventHandler;
            }
            if (!string.IsNullOrWhiteSpace(ycmvt.c_nhucauvattu_id))
            {
                msg = $@"Lỗi: Yêu cầu mua ""{ycmvt.sochungtu}"" có liên kết với ""Nhu cầu mua vật tư"".";
                goto EndEventHandler;
            }
            if (sp == null)
            {
                msg = $@"Lỗi: Mã vật tư ""{ma_sanpham}"" không tồn tại.";
                goto EndEventHandler;
            }
            if (soluong_yeucau <= 0)
            {
                msg = "Lỗi: Số lượng yêu cầu phải lớn hơn 0.";
                goto EndEventHandler;
            }
            var cdhYCMVT = db.c_yeucaumuavt_cdh.Where(s =>
                s.md_sanpham_id == sp.md_sanpham_id
                & s.c_yeucaumuavt_id == object_.c_yeucaumuavt_id
                & s.c_yeucaumuavt_cdh_id != object_.c_yeucaumuavt_cdh_id).FirstOrDefault();
            if (cdhYCMVT != null)
            {
                msg = $@"Lỗi: Mã vật tư ""{ma_sanpham}"" đã được thêm trước đó.";
                goto EndEventHandler;
            }

            if (msg.Length <= 0)
            {
                VNN_Function.SetFormValue(object_.nameof(s => s.md_sanpham_id), sp.md_sanpham_id);
                VNN_Function.SetFormValue(object_.nameof(s => s.md_donvitinhsanpham_id), sp.md_donvitinhsanpham_id);
                object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
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
            var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            var object_s = db.c_yeucaumuavt_cdh.Where(p => ids.Contains(p.c_yeucaumuavt_cdh_id)).ToList();
            var ycmvtId = object_s.Select(s => s.c_yeucaumuavt_id).FirstOrDefault();
            var ycmvt = db.c_yeucaumuavt.Where(s => s.c_yeucaumuavt_id == ycmvtId).FirstOrDefault();

            if (object_s.Count <= 0)
            {
                msg = $@"Lỗi: Không tìm thấy đối tượng cần xóa.";
                goto EndEventHandler;
            }
            if (ycmvt == null)
            {
                msg = $@"Lỗi: Yêu cầu mua vật tư không tồn tại.";
                goto EndEventHandler;
            }
            if (ycmvt.md_trangthai_id == Helper.HIEULUC)
            {
                msg = $@"Lỗi: Yêu cầu mua vật tư đã Hiệu Lực.";
                goto EndEventHandler;
            }
            if (!string.IsNullOrWhiteSpace(ycmvt.c_nhucauvattu_id))
            {
                msg = $@"Lỗi: Yêu cầu mua ""{ycmvt.sochungtu}"" có liên kết với ""Nhu cầu mua vật tư"".";
                goto EndEventHandler;
            }

            foreach (var object_ in object_s)
            {
                db.c_yeucaumuavt_cdh.Remove(object_);
            }
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

    EndEventHandler:;

        if (msg.Length <= 0)
        {
            msg = string.Format(@"true#Xóa thành công");
            VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = string.Format(@"false#{0}", msg);
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
