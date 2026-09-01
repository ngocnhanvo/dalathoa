using DataAcess;
using System;
using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// Summary description for HelperEntity
/// </summary>
public class HelperEntity
{
    public HelperEntity() { }
    public class objLSNX {
        public string spId { get; set; }
        public string dvtSpId { get; set; }
        public decimal slDichChuyen { get; set; }
        public string dvtSpId2 { get; set; }
        public decimal kgDichChuyen { get; set; }
        public string dongNhapXuat { get; set; }
        public string sctDonHang { get; set; }
        public string kieuchuyen { get; set; }
        public string khoId { get; set; }
        public string loKhoId { get; set; }
        public decimal giaTriVND { get; set; }
        public DateTime? ngayChuyen { get; set; }
        public bool theoKg { get; set; }
        public bool? laphieuKK { get; set; }
        public string mota { get; set; }
        public string hanghoa { get; set; }
    }
    public string kieuNhapKho = "Nhập kho";
    public string kieuXuatKho = "Xuất kho";
    public objLSNX obj { get; set; }
    public void themHoacSuaLichSuNhapXuatKho(EntityContext db, User_TK user)
    {
        if (obj.slDichChuyen > 0)
        {
            var kgd = new md_kho_giaodich();
            kgd.md_kho_giaodich_id = Helper.getNewId();
            //kgd.laphieuKK = obj.laphieuKK;
            kgd.md_kho_id = obj.khoId;
            //kgd.md_kho_lk_id = obj.loKhoId;
            kgd.md_sanpham_id = obj.spId;
            kgd.md_donvitinhsanpham_id = obj.dvtSpId;
            kgd.soluong_dichchuyen = obj.slDichChuyen;
            //kgd.dvt2 = obj.dvtSpId2;
            //kgd.kg_dichchuyen = obj.kgDichChuyen;
            kgd.giatriVND = obj.giaTriVND;
            kgd.kieuchuyen = obj.kieuchuyen;
            kgd.dongnhapxuat = obj.dongNhapXuat;
            kgd.dongvanchuyen = obj.dongNhapXuat;
            kgd.dongkiemkho = obj.dongNhapXuat;
            kgd.dongsanxuat = obj.dongNhapXuat;
            kgd.donhang = obj.sctDonHang;
            //kgd.theoKg = obj.theoKg;
            kgd.mota = obj.mota;
            kgd.ngaychuyen = obj.ngayChuyen;
            //kgd.hanghoa = obj.hanghoa;
            kgd = Helper.setDefaultValueWhenInsertOrUpdate(kgd, user, false);
            db.md_kho_giaodich.Add(kgd);
        }
    }

    public bool kiemTraNCClaVietNam(md_doitackinhdoanh dtkd, EntityContext db)
    {
        var laVN = true;
        if (dtkd != null)
        {
            var quocgia = db.md_quocgia.Where(s => s.md_quocgia_id == dtkd.md_quocgia_id).FirstOrDefault();
            if (quocgia != null)
                laVN = quocgia.ma_quocgia == "VN";
        }
        return laVN;
    }
}