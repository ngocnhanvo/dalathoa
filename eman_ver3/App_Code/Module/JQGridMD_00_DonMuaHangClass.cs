using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JQGridMD_00_DonMuaHangModify
/// </summary>
public class JQGridMD_00_DonMuaHangClass
{
    public JQGridMD_00_DonMuaHangClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void TinhThueDonMuaHang(c_donmuahang object_, User_TK userTK, EntityContext db, bool? submit = false)
    {
        var ddhsServer = db.c_donmuahang_cdmh.Where(s => s.c_donmuahang_id == object_.c_donmuahang_id).ToList();
        var ddhs = db.c_donmuahang_cdmh.Local.Where(s => s.c_donmuahang_id == object_.c_donmuahang_id).ToList();
        var tong_tienhang = ddhs.Sum(s => s.thanhtien.GetValueOrDefault(0));
        var tong_tienthue = ddhs.Sum(s => s.thanhtienThue.GetValueOrDefault(0));
        decimal tong_tatca = tong_tienhang + Math.Floor(tong_tienthue);
        //--
        string donvitinh = "VND";
        //--
        var tth = VNN_ConvertMoney.convert((double)tong_tienhang, donvitinh).FirstOrDefault();
        var ttc = VNN_ConvertMoney.convert((double)tong_tatca, donvitinh).FirstOrDefault();
        object_.tong_tienhang = (decimal)tth.Value;
        object_.tong_tatca = (decimal)ttc.Value;
        object_.chu_tong_tienhang = tth.Key;
        object_.chu_tong_tatca = ttc.Key;

        if (submit.GetValueOrDefault(false))
            db.SaveChanges();
    }

    public void TinhThueDonBanHang(c_danhsachdathang object_, User_TK userTK, EntityContext db, bool? submit = false)
    {
        var ddhsServer = db.c_dongdsdh.Where(s => s.c_danhsachdathang_id == object_.c_danhsachdathang_id).ToList();
        var ddhs = db.c_dongdsdh.Local.Where(s => s.c_danhsachdathang_id == object_.c_danhsachdathang_id).ToList();
        var tong_tienhang = ddhs.Sum(s => s.thanhtien.GetValueOrDefault(0));
        var tong_tienthue = ddhs.Sum(s => s.thanhtienThue.GetValueOrDefault(0));
        decimal tong_tatca = tong_tienhang + Math.Floor(tong_tienthue);
        //--
        string donvitinh = "VND";
        //--
        var tth = VNN_ConvertMoney.convert((double)tong_tienhang, donvitinh).FirstOrDefault();
        var ttc = VNN_ConvertMoney.convert((double)tong_tatca, donvitinh).FirstOrDefault();
        object_.tong_tienhang = (decimal)tth.Value;
        object_.tong_tatca = (decimal)ttc.Value;
        object_.chu_tong_tienhang = tth.Key;
        object_.chu_tong_tatca = ttc.Key;

        if(submit.GetValueOrDefault(false))
            db.SaveChanges();
    }
}