
using System.Data.Entity;
namespace DataAcess
{
    public class EntityContext10table
    {
        public void exec(DbModelBuilder modelBuilder)
        {
            #region Start Code
            modelBuilder.Entity<md_thongbao>().ToTable("md_thongbao");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.md_thongbao_id)
                            .HasColumnName("md_thongbao_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_thongbao>().HasKey<string>(p => p.md_thongbao_id);
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.ma_thongbao)
                            .HasColumnName("ma_thongbao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.nguoinhan)
                            .HasColumnName("nguoinhan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.noidung)
                            .HasColumnName("noidung")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.tieude)
                            .HasColumnName("tieude")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thongbao>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_thue_sanpham>().ToTable("md_thue_sanpham");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.md_thue_sanpham_id)
                            .HasColumnName("md_thue_sanpham_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_thue_sanpham>().HasKey<string>(p => p.md_thue_sanpham_id);
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.giatri)
                            .HasColumnName("giatri")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.macdinh)
                            .HasColumnName("macdinh")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.ten_thue_sanpham)
                            .HasColumnName("ten_thue_sanpham")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_thue_sanpham>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_tonghopcongno>().ToTable("md_tonghopcongno");
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.md_tonghopcongno_id)
                            .HasColumnName("md_tonghopcongno_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_tonghopcongno>().HasKey<string>(p => p.md_tonghopcongno_id);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.cocuoiky)
                            .HasColumnName("cocuoiky")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.cocuoiky_usd)
                            .HasColumnName("cocuoiky_usd")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.codauky)
                            .HasColumnName("codauky")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.codauky_usd)
                            .HasColumnName("codauky_usd")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.cotrongky)
                            .HasColumnName("cotrongky")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.cotrongky_usd)
                            .HasColumnName("cotrongky_usd")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.iskh)
                            .HasColumnName("iskh")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.isncc)
                            .HasColumnName("isncc")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.ma_dtkd)
                            .HasColumnName("ma_dtkd")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.md_namtaichinh_id)
                            .HasColumnName("md_namtaichinh_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.md_namtaichinh_ky_id)
                            .HasColumnName("md_namtaichinh_ky_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.nam)
                            .HasColumnName("nam")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.nocuoiky)
                            .HasColumnName("nocuoiky")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.nocuoiky_usd)
                            .HasColumnName("nocuoiky_usd")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.nodauky)
                            .HasColumnName("nodauky")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.nodauky_usd)
                            .HasColumnName("nodauky_usd")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.notrongky)
                            .HasColumnName("notrongky")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.notrongky_usd)
                            .HasColumnName("notrongky_usd")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.soky)
                            .HasColumnName("soky")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.ten_dtkd)
                            .HasColumnName("ten_dtkd")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tonghopcongno>()
                            .Property(p => p.tygia)
                            .HasColumnName("tygia")
                            .HasColumnType("decimal").HasPrecision(18, 8);
modelBuilder.Entity<md_tonghopkho>().ToTable("md_tonghopkho");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.md_tonghopkho_id)
                            .HasColumnName("md_tonghopkho_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_tonghopkho>().HasKey<string>(p => p.md_tonghopkho_id);
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.ma_sanpham)
                            .HasColumnName("ma_sanpham")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.md_namtaichinh_id)
                            .HasColumnName("md_namtaichinh_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.md_namtaichinh_ky_id)
                            .HasColumnName("md_namtaichinh_ky_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.nam)
                            .HasColumnName("nam")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.sl_cuoiky)
                            .HasColumnName("sl_cuoiky")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.sl_dauky)
                            .HasColumnName("sl_dauky")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.sl_nhaptrongky)
                            .HasColumnName("sl_nhaptrongky")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.sl_xuattrongky)
                            .HasColumnName("sl_xuattrongky")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.soky)
                            .HasColumnName("soky")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tonghopkho>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_trangthai>().ToTable("md_trangthai");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.md_trangthai_id)
                            .HasColumnName("md_trangthai_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_trangthai>().HasKey<string>(p => p.md_trangthai_id);
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.textTT)
                            .HasColumnName("textTT")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.typeTT)
                            .HasColumnName("typeTT")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_trangthai>()
                            .Property(p => p.valueTT)
                            .HasColumnName("valueTT")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_travexacnhan>().ToTable("md_travexacnhan");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.md_travexacnhan_id)
                            .HasColumnName("md_travexacnhan_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_travexacnhan>().HasKey<string>(p => p.md_travexacnhan_id);
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.denkho)
                            .HasColumnName("denkho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.donhang)
                            .HasColumnName("donhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.id_lienquan)
                            .HasColumnName("id_lienquan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.loai)
                            .HasColumnName("loai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.ngaychuyen)
                            .HasColumnName("ngaychuyen")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.ngaydenghi)
                            .HasColumnName("ngaydenghi")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.ngayhieuluc)
                            .HasColumnName("ngayhieuluc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.ngayxacnhan)
                            .HasColumnName("ngayxacnhan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.sct_lienquan)
                            .HasColumnName("sct_lienquan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.trangthai)
                            .HasColumnName("trangthai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.tukho)
                            .HasColumnName("tukho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_travexacnhan_cdh>().ToTable("md_travexacnhan_cdh");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.md_travexacnhan_cdh_id)
                            .HasColumnName("md_travexacnhan_cdh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_travexacnhan_cdh>().HasKey<string>(p => p.md_travexacnhan_cdh_id);
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.md_travexacnhan_id)
                            .HasColumnName("md_travexacnhan_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.soluong_dichchuyen)
                            .HasColumnName("soluong_dichchuyen")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.soluong_muonchuyen)
                            .HasColumnName("soluong_muonchuyen")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.soluong_toida)
                            .HasColumnName("soluong_toida")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_travexacnhan_cdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_tygia>().ToTable("md_tygia");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.md_tygia_id)
                            .HasColumnName("md_tygia_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_tygia>().HasKey<string>(p => p.md_tygia_id);
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.chia_cho)
                            .HasColumnName("chia_cho")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.hieuluc_denngay)
                            .HasColumnName("hieuluc_denngay")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.hieuluc_tungay)
                            .HasColumnName("hieuluc_tungay")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.nhan_voi)
                            .HasColumnName("nhan_voi")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.sang_dongtien_id)
                            .HasColumnName("sang_dongtien_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.ten_tygia)
                            .HasColumnName("ten_tygia")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.tu_dongtien_id)
                            .HasColumnName("tu_dongtien_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_tygia>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_vanchuyennoibo>().ToTable("md_vanchuyennoibo");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.md_vanchuyennoibo_id)
                            .HasColumnName("md_vanchuyennoibo_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_vanchuyennoibo>().HasKey<string>(p => p.md_vanchuyennoibo_id);
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.sochungtu)
                            .HasColumnName("sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.banggiaNC)
                            .HasColumnName("banggiaNC")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.c_doichieuhangton_id)
                            .HasColumnName("c_doichieuhangton_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.chungtu_lenhsx)
                            .HasColumnName("chungtu_lenhsx")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.chungtuthamchieu)
                            .HasColumnName("chungtuthamchieu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.denkho)
                            .HasColumnName("denkho")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.donhang_thamchieu)
                            .HasColumnName("donhang_thamchieu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.ketoan)
                            .HasColumnName("ketoan")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.kiemtraSL)
                            .HasColumnName("kiemtraSL")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.laytonTP)
                            .HasColumnName("laytonTP")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.laytonTPhoacBTP)
                            .HasColumnName("laytonTPhoacBTP")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.loaichuyen)
                            .HasColumnName("loaichuyen")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.loaichuyen_id)
                            .HasColumnName("loaichuyen_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.loaiphieuHN)
                            .HasColumnName("loaiphieuHN")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.md_lenhsanxuat_tosx_id)
                            .HasColumnName("md_lenhsanxuat_tosx_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.md_to_id)
                            .HasColumnName("md_to_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.md_trangthai_id)
                            .HasColumnName("md_trangthai_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.ngaychuyen)
                            .HasColumnName("ngaychuyen")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.ngaydenghi)
                            .HasColumnName("ngaydenghi")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.ngayhieuluc)
                            .HasColumnName("ngayhieuluc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.ngayxacnhan)
                            .HasColumnName("ngayxacnhan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.nguoiHL)
                            .HasColumnName("nguoiHL")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.phienbangiaNC)
                            .HasColumnName("phienbangiaNC")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.phieugiamHN)
                            .HasColumnName("phieugiamHN")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.pxPhieuHN)
                            .HasColumnName("pxPhieuHN")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.sctdathang)
                            .HasColumnName("sctdathang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.tiencongDG)
                            .HasColumnName("tiencongDG")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.tukho)
                            .HasColumnName("tukho")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_vanchuyennoibo_cdvc>().ToTable("md_vanchuyennoibo_cdvc");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.md_vanchuyennoibo_cdvc_id)
                            .HasColumnName("md_vanchuyennoibo_cdvc_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_vanchuyennoibo_cdvc>().HasKey<string>(p => p.md_vanchuyennoibo_cdvc_id);
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.chuyenton)
                            .HasColumnName("chuyenton")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.chuyentonTP)
                            .HasColumnName("chuyentonTP")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.dachuyenhet)
                            .HasColumnName("dachuyenhet")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.denkho)
                            .HasColumnName("denkho")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.dento)
                            .HasColumnName("dento")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.gianhancong)
                            .HasColumnName("gianhancong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.lsx_to)
                            .HasColumnName("lsx_to")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.lsxId)
                            .HasColumnName("lsxId")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.ma_tukho)
                            .HasColumnName("ma_tukho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.md_vanchuyennoibo_id)
                            .HasColumnName("md_vanchuyennoibo_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.ngay_hethan)
                            .HasColumnName("ngay_hethan")
                            .HasColumnType("date");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.sl_tonkhoDK)
                            .HasColumnName("sl_tonkhoDK")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.sl_tonkhoTK)
                            .HasColumnName("sl_tonkhoTK")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.soluong_dachuyen)
                            .HasColumnName("soluong_dachuyen")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.soluong_dichchuyen)
                            .HasColumnName("soluong_dichchuyen")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.soluong_muonchuyen)
                            .HasColumnName("soluong_muonchuyen")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.soluong_toida)
                            .HasColumnName("soluong_toida")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.tenhang)
                            .HasColumnName("tenhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.tukho)
                            .HasColumnName("tukho")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.tuto)
                            .HasColumnName("tuto")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_cdvc>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_vanchuyennoibo_dalayton>().ToTable("md_vanchuyennoibo_dalayton");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.md_vanchuyennoibo_dalayton_id)
                            .HasColumnName("md_vanchuyennoibo_dalayton_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_vanchuyennoibo_dalayton>().HasKey<string>(p => p.md_vanchuyennoibo_dalayton_id);
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.gianhancong)
                            .HasColumnName("gianhancong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.lsx)
                            .HasColumnName("lsx")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.md_vanchuyennoibo_id)
                            .HasColumnName("md_vanchuyennoibo_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.sldalay)
                            .HasColumnName("sldalay")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.soluong_layton)
                            .HasColumnName("soluong_layton")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.tongsoluong)
                            .HasColumnName("tongsoluong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.tosx)
                            .HasColumnName("tosx")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_dalayton>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_vanchuyennoibo_rabo>().ToTable("md_vanchuyennoibo_rabo");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.md_vanchuyennoibo_rabo_id)
                            .HasColumnName("md_vanchuyennoibo_rabo_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_vanchuyennoibo_rabo>().HasKey<string>(p => p.md_vanchuyennoibo_rabo_id);
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.chuyenton)
                            .HasColumnName("chuyenton")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.dachuyenhet)
                            .HasColumnName("dachuyenhet")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.denkho)
                            .HasColumnName("denkho")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.dento)
                            .HasColumnName("dento")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.gianhancong)
                            .HasColumnName("gianhancong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.lsx_to)
                            .HasColumnName("lsx_to")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.lsxId)
                            .HasColumnName("lsxId")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.ma_tukho)
                            .HasColumnName("ma_tukho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.md_sanphambo_id)
                            .HasColumnName("md_sanphambo_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.md_vanchuyennoibo_id)
                            .HasColumnName("md_vanchuyennoibo_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.ngay_hethan)
                            .HasColumnName("ngay_hethan")
                            .HasColumnType("date");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.soluong_dachuyen)
                            .HasColumnName("soluong_dachuyen")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.soluong_dichchuyen)
                            .HasColumnName("soluong_dichchuyen")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.soluong_muonchuyen)
                            .HasColumnName("soluong_muonchuyen")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.soluong_toida)
                            .HasColumnName("soluong_toida")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.tenhang)
                            .HasColumnName("tenhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.tukho)
                            .HasColumnName("tukho")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.tuto)
                            .HasColumnName("tuto")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vanchuyennoibo_rabo>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_vattukhoan>().ToTable("md_vattukhoan");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.md_vattukhoan_id)
                            .HasColumnName("md_vattukhoan_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_vattukhoan>().HasKey<string>(p => p.md_vattukhoan_id);
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.md_to_id)
                            .HasColumnName("md_to_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.nam)
                            .HasColumnName("nam")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.thang)
                            .HasColumnName("thang")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.tienvtkhoan)
                            .HasColumnName("tienvtkhoan")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.tienvtvipham)
                            .HasColumnName("tienvtvipham")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_vattukhoan>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_xuatban>().ToTable("md_xuatban");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.md_xuatban_id)
                            .HasColumnName("md_xuatban_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_xuatban>().HasKey<string>(p => p.md_xuatban_id);
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.sochungtu)
                            .HasColumnName("sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.c_danhsachdathang_id)
                            .HasColumnName("c_danhsachdathang_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.diachi)
                            .HasColumnName("diachi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.donhang_thamchieu)
                            .HasColumnName("donhang_thamchieu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.ketoan)
                            .HasColumnName("ketoan")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.loai_cont)
                            .HasColumnName("loai_cont")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.md_doitackinhdoanh_id)
                            .HasColumnName("md_doitackinhdoanh_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.mg)
                            .HasColumnName("mg")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.ngaychuyen)
                            .HasColumnName("ngaychuyen")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.ngaydenghi)
                            .HasColumnName("ngaydenghi")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.ngaydonhang)
                            .HasColumnName("ngaydonhang")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.ngayhieuluc)
                            .HasColumnName("ngayhieuluc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.ngayxacnhan)
                            .HasColumnName("ngayxacnhan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.nguoiHL)
                            .HasColumnName("nguoiHL")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.phieuXNNK)
                            .HasColumnName("phieuXNNK")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.sctdathang)
                            .HasColumnName("sctdathang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.sent)
                            .HasColumnName("sent")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.so_cont)
                            .HasColumnName("so_cont")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.so_seal)
                            .HasColumnName("so_seal")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.tare)
                            .HasColumnName("tare")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.tiencongghep)
                            .HasColumnName("tiencongghep")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.tienlencong)
                            .HasColumnName("tienlencong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.trangthai)
                            .HasColumnName("trangthai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.tukho)
                            .HasColumnName("tukho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban>()
                            .Property(p => p.xuat_thanhly)
                            .HasColumnName("xuat_thanhly")
                            .HasColumnType("int");
modelBuilder.Entity<md_xuatban_cdh>().ToTable("md_xuatban_cdh");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.md_xuatban_cdh_id)
                            .HasColumnName("md_xuatban_cdh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_xuatban_cdh>().HasKey<string>(p => p.md_xuatban_cdh_id);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.cbm)
                            .HasColumnName("cbm")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.check_kho)
                            .HasColumnName("check_kho")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.ghichu_donvi2)
                            .HasColumnName("ghichu_donvi2")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.gw)
                            .HasColumnName("gw")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.md_xuatban_id)
                            .HasColumnName("md_xuatban_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.nw)
                            .HasColumnName("nw")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.sl_daxuat)
                            .HasColumnName("sl_daxuat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.sl_inner)
                            .HasColumnName("sl_inner")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.sl_muonxuat)
                            .HasColumnName("sl_muonxuat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.sl_outer)
                            .HasColumnName("sl_outer")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.sl_tonkho)
                            .HasColumnName("sl_tonkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.sl_xuat)
                            .HasColumnName("sl_xuat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.sokien)
                            .HasColumnName("sokien")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.tenhang)
                            .HasColumnName("tenhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.tenkien)
                            .HasColumnName("tenkien")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.tldg)
                            .HasColumnName("tldg")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.tong_sl_xuat)
                            .HasColumnName("tong_sl_xuat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.tukho)
                            .HasColumnName("tukho")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatban_cdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_xuatkhonb>().ToTable("md_xuatkhonb");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.md_xuatkhonb_id)
                            .HasColumnName("md_xuatkhonb_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_xuatkhonb>().HasKey<string>(p => p.md_xuatkhonb_id);
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.sochungtu)
                            .HasColumnName("sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.banggiaNC)
                            .HasColumnName("banggiaNC")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.baotri_vattu)
                            .HasColumnName("baotri_vattu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.bosung)
                            .HasColumnName("bosung")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.chungtu_lenhsx)
                            .HasColumnName("chungtu_lenhsx")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.chungtuthamchieu)
                            .HasColumnName("chungtuthamchieu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.diachi)
                            .HasColumnName("diachi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.donhang_thamchieu)
                            .HasColumnName("donhang_thamchieu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.ketoan)
                            .HasColumnName("ketoan")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.kh_ddhpx)
                            .HasColumnName("kh_ddhpx")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.khuon)
                            .HasColumnName("khuon")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.lenhdonggoi)
                            .HasColumnName("lenhdonggoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.md_doitackinhdoanh_id)
                            .HasColumnName("md_doitackinhdoanh_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.md_lenhsanxuat_id)
                            .HasColumnName("md_lenhsanxuat_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.md_lenhsanxuat_tosx_id)
                            .HasColumnName("md_lenhsanxuat_tosx_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.md_to_id)
                            .HasColumnName("md_to_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.ngaychuyen)
                            .HasColumnName("ngaychuyen")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.ngaydenghi)
                            .HasColumnName("ngaydenghi")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.ngayhieuluc)
                            .HasColumnName("ngayhieuluc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.ngayxacnhan)
                            .HasColumnName("ngayxacnhan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.nguoiHL)
                            .HasColumnName("nguoiHL")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.phienbangiaNC)
                            .HasColumnName("phienbangiaNC")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.phieuXNNK)
                            .HasColumnName("phieuXNNK")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.sctdathang)
                            .HasColumnName("sctdathang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.trangthai)
                            .HasColumnName("trangthai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.tukho)
                            .HasColumnName("tukho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb>()
                            .Property(p => p.xuatden)
                            .HasColumnName("xuatden")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_xuatkhonb_cdh>().ToTable("md_xuatkhonb_cdh");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.md_xuatkhonb_cdh_id)
                            .HasColumnName("md_xuatkhonb_cdh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_xuatkhonb_cdh>().HasKey<string>(p => p.md_xuatkhonb_cdh_id);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.check_kho)
                            .HasColumnName("check_kho")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.dangno)
                            .HasColumnName("dangno")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.datruno)
                            .HasColumnName("datruno")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.dento)
                            .HasColumnName("dento")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.ghichu_donvi2)
                            .HasColumnName("ghichu_donvi2")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.ghino)
                            .HasColumnName("ghino")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.gianhancong)
                            .HasColumnName("gianhancong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.lsx_to)
                            .HasColumnName("lsx_to")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.lsxId)
                            .HasColumnName("lsxId")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.md_donvitinhsanpham_id2)
                            .HasColumnName("md_donvitinhsanpham_id2")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.md_xuatkhonb_id)
                            .HasColumnName("md_xuatkhonb_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.saiso)
                            .HasColumnName("saiso")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.sl_daxuat)
                            .HasColumnName("sl_daxuat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.sl_muonxuat)
                            .HasColumnName("sl_muonxuat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.sl_thucxuat)
                            .HasColumnName("sl_thucxuat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.sl_toida_trongLSXTo)
                            .HasColumnName("sl_toida_trongLSXTo")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.sl_toida_trongLSXTo2)
                            .HasColumnName("sl_toida_trongLSXTo2")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.sl_tonkho)
                            .HasColumnName("sl_tonkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.sl_xuat)
                            .HasColumnName("sl_xuat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.sl_xuat2)
                            .HasColumnName("sl_xuat2")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.tenhang)
                            .HasColumnName("tenhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.tong_sl_xuat)
                            .HasColumnName("tong_sl_xuat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.truno)
                            .HasColumnName("truno")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.tuto)
                            .HasColumnName("tuto")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_cdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_xuatkhonb_sanpham>().ToTable("md_xuatkhonb_sanpham");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.md_xuatkhonb_sanpham_id)
                            .HasColumnName("md_xuatkhonb_sanpham_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_xuatkhonb_sanpham>().HasKey<string>(p => p.md_xuatkhonb_sanpham_id);
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.md_xuatkhonb_id)
                            .HasColumnName("md_xuatkhonb_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.soluong)
                            .HasColumnName("soluong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_xuatkhonb_sanpham>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                    
                    
            #endregion End Code
        }
    }
}
