using System.Data.Entity;
namespace DataAcess
{
    public class EntityContext02table
    {
        public void exec(DbModelBuilder modelBuilder)
        {
            #region Start Code
            modelBuilder.Entity<c_doichieuhangton_thdh>().ToTable("c_doichieuhangton_thdh");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.c_doichieuhangton_thdh_id)
                            .HasColumnName("c_doichieuhangton_thdh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton_thdh>().HasKey<string>(p => p.c_doichieuhangton_thdh_id);
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.c_doichieuhangton_id)
                            .HasColumnName("c_doichieuhangton_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.sl_damua)
                            .HasColumnName("sl_damua")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.sl_dasanxuat)
                            .HasColumnName("sl_dasanxuat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.sl_datmua)
                            .HasColumnName("sl_datmua")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.sl_donhang)
                            .HasColumnName("sl_donhang")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.sl_giamhanngach)
                            .HasColumnName("sl_giamhanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.sl_layhangton)
                            .HasColumnName("sl_layhangton")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.sl_sanxuat)
                            .HasColumnName("sl_sanxuat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.sl_thieu)
                            .HasColumnName("sl_thieu")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_thdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton_tk>().ToTable("c_doichieuhangton_tk");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.c_doichieuhangton_tk_id)
                            .HasColumnName("c_doichieuhangton_tk_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton_tk>().HasKey<string>(p => p.c_doichieuhangton_tk_id);
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.c_doichieuhangton_id)
                            .HasColumnName("c_doichieuhangton_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.inbaocao)
                            .HasColumnName("inbaocao")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.macuoi)
                            .HasColumnName("macuoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.macuoi_id)
                            .HasColumnName("macuoi_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.makho)
                            .HasColumnName("makho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.mota_tienganh)
                            .HasColumnName("mota_tienganh")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.mota_tiengviet)
                            .HasColumnName("mota_tiengviet")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.sanpham)
                            .HasColumnName("sanpham")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.sl_conlai)
                            .HasColumnName("sl_conlai")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.sl_dathang)
                            .HasColumnName("sl_dathang")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.sl_giamhanngach)
                            .HasColumnName("sl_giamhanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.sl_giucho)
                            .HasColumnName("sl_giucho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.sl_trongkho)
                            .HasColumnName("sl_trongkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.tachbom)
                            .HasColumnName("tachbom")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tk>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton_tkkd>().ToTable("c_doichieuhangton_tkkd");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.c_doichieuhangton_tkkd_id)
                            .HasColumnName("c_doichieuhangton_tkkd_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton_tkkd>().HasKey<string>(p => p.c_doichieuhangton_tkkd_id);
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.c_doichieuhangton_id)
                            .HasColumnName("c_doichieuhangton_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.kieudang)
                            .HasColumnName("kieudang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.trangthai)
                            .HasColumnName("trangthai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_tkkd>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_dongdsdh>().ToTable("c_dongdsdh");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.c_dongdsdh_id)
                            .HasColumnName("c_dongdsdh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_dongdsdh>().HasKey<string>(p => p.c_dongdsdh_id);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.anco_check)
                            .HasColumnName("anco_check")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.c_danhsachdathang_id)
                            .HasColumnName("c_danhsachdathang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.c_dongdonhang_id)
                            .HasColumnName("c_dongdonhang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.chenhlechgia)
                            .HasColumnName("chenhlechgia")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.chungloai)
                            .HasColumnName("chungloai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.danhapTho)
                            .HasColumnName("danhapTho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.dvt_inner)
                            .HasColumnName("dvt_inner")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.dvt_outer)
                            .HasColumnName("dvt_outer")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.ghichu_vachngan)
                            .HasColumnName("ghichu_vachngan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.giachuan)
                            .HasColumnName("giachuan")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.giadoichieu)
                            .HasColumnName("giadoichieu")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.gianhap)
                            .HasColumnName("gianhap")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.h1)
                            .HasColumnName("h1")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.h2)
                            .HasColumnName("h2")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.han_giaohang)
                            .HasColumnName("han_giaohang")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.huongdan_dathang)
                            .HasColumnName("huongdan_dathang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.l1)
                            .HasColumnName("l1")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.l2)
                            .HasColumnName("l2")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.ma_sanpham_khach)
                            .HasColumnName("ma_sanpham_khach")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.md_doitackinhdoanh_id)
                            .HasColumnName("md_doitackinhdoanh_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.md_donggoi_id)
                            .HasColumnName("md_donggoi_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.md_sanpham_bom_id)
                            .HasColumnName("md_sanpham_bom_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.mota_tienganh)
                            .HasColumnName("mota_tienganh")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.mota_tiengviet)
                            .HasColumnName("mota_tiengviet")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.phi)
                            .HasColumnName("phi")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.phidg)
                            .HasColumnName("phidg")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_conlai)
                            .HasColumnName("sl_conlai")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_cont)
                            .HasColumnName("sl_cont")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_dagiao)
                            .HasColumnName("sl_dagiao")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_dathang)
                            .HasColumnName("sl_dathang")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_donggoi)
                            .HasColumnName("sl_donggoi")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_donggoiTP)
                            .HasColumnName("sl_donggoiTP")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_giamhanngach)
                            .HasColumnName("sl_giamhanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_hanngach)
                            .HasColumnName("sl_hanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_huy)
                            .HasColumnName("sl_huy")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_inner)
                            .HasColumnName("sl_inner")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_nhaphang)
                            .HasColumnName("sl_nhaphang")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_outer)
                            .HasColumnName("sl_outer")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_thuhoi)
                            .HasColumnName("sl_thuhoi")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_thuhoiDG)
                            .HasColumnName("sl_thuhoiDG")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sl_thuhoiTP)
                            .HasColumnName("sl_thuhoiTP")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.sothutu)
                            .HasColumnName("sothutu")
                            .HasColumnType("int");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.tem_dan)
                            .HasColumnName("tem_dan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.thanhtien)
                            .HasColumnName("thanhtien")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.thanhtienThue)
                            .HasColumnName("thanhtienThue")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.thue)
                            .HasColumnName("thue")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.v2)
                            .HasColumnName("v2")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.vd)
                            .HasColumnName("vd")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.vl)
                            .HasColumnName("vl")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.vn)
                            .HasColumnName("vn")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.w1)
                            .HasColumnName("w1")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_dongdsdh>()
                            .Property(p => p.w2)
                            .HasColumnName("w2")
                            .HasColumnType("numeric").HasPrecision(18, 8);
modelBuilder.Entity<c_donmuahang>().ToTable("c_donmuahang");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.c_donmuahang_id)
                            .HasColumnName("c_donmuahang_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<c_donmuahang>().HasKey<string>(p => p.c_donmuahang_id);
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.sochungtu)
                            .HasColumnName("sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.bosung)
                            .HasColumnName("bosung")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.c_kehoachdathang_dhncc_id)
                            .HasColumnName("c_kehoachdathang_dhncc_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.c_kehoachmuavt_id)
                            .HasColumnName("c_kehoachmuavt_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.chiphi)
                            .HasColumnName("chiphi")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.chu_tong_tatca)
                            .HasColumnName("chu_tong_tatca")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.chu_tong_tienhang)
                            .HasColumnName("chu_tong_tienhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.diadiem_giaohang)
                            .HasColumnName("diadiem_giaohang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.donhang_thamchieu)
                            .HasColumnName("donhang_thamchieu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.giamgia)
                            .HasColumnName("giamgia")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.hinhthucthanhtoan)
                            .HasColumnName("hinhthucthanhtoan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.huongdan_lamhang)
                            .HasColumnName("huongdan_lamhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.md_banggia_id)
                            .HasColumnName("md_banggia_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.md_dieukienthanhtoan_id)
                            .HasColumnName("md_dieukienthanhtoan_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.md_doitackinhdoanh_id)
                            .HasColumnName("md_doitackinhdoanh_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.md_dongtien_id)
                            .HasColumnName("md_dongtien_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.md_phienbangia_id)
                            .HasColumnName("md_phienbangia_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.md_trangthai_id)
                            .HasColumnName("md_trangthai_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.ngaydonhang)
                            .HasColumnName("ngaydonhang")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.ngaygiaohang)
                            .HasColumnName("ngaygiaohang")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.ngayhieuluc)
                            .HasColumnName("ngayhieuluc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.ngayketthuc)
                            .HasColumnName("ngayketthuc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.ngaythanhtoan)
                            .HasColumnName("ngaythanhtoan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.ngayxacnhan)
                            .HasColumnName("ngayxacnhan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.nguoilienhe)
                            .HasColumnName("nguoilienhe")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.phieunhapkho)
                            .HasColumnName("phieunhapkho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.phieuXNNK)
                            .HasColumnName("phieuXNNK")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.sctdathang)
                            .HasColumnName("sctdathang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.sctkehoach)
                            .HasColumnName("sctkehoach")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.so_donmuahang)
                            .HasColumnName("so_donmuahang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.tong_tatca)
                            .HasColumnName("tong_tatca")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.tong_tienhang)
                            .HasColumnName("tong_tienhang")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.tygiaVND)
                            .HasColumnName("tygiaVND")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_donmuahang_cdmh>().ToTable("c_donmuahang_cdmh");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.c_donmuahang_cdmh_id)
                            .HasColumnName("c_donmuahang_cdmh_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<c_donmuahang_cdmh>().HasKey<string>(p => p.c_donmuahang_cdmh_id);
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.c_donmuahang_id)
                            .HasColumnName("c_donmuahang_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.dongiamua)
                            .HasColumnName("dongiamua")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.giachuan)
                            .HasColumnName("giachuan")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.ngay_hethan)
                            .HasColumnName("ngay_hethan")
                            .HasColumnType("date");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.saiso)
                            .HasColumnName("saiso")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.sl_dadat)
                            .HasColumnName("sl_dadat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.sl_dadat2)
                            .HasColumnName("sl_dadat2")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.sl_hanngach)
                            .HasColumnName("sl_hanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.sl_tonkho)
                            .HasColumnName("sl_tonkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.thanhtien)
                            .HasColumnName("thanhtien")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.thanhtienThue)
                            .HasColumnName("thanhtienThue")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.thue)
                            .HasColumnName("thue")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_cdmh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_donmuahang_thue>().ToTable("c_donmuahang_thue");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.c_donmuahang_thue_id)
                            .HasColumnName("c_donmuahang_thue_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_donmuahang_thue>().HasKey<string>(p => p.c_donmuahang_thue_id);
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.c_donmuahang_id)
                            .HasColumnName("c_donmuahang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.doanhnghiep)
                            .HasColumnName("doanhnghiep")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.md_thue_sanpham_id)
                            .HasColumnName("md_thue_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.tochuc)
                            .HasColumnName("tochuc")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.tong_tien_chiu_thue)
                            .HasColumnName("tong_tien_chiu_thue")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.tong_tien_thue)
                            .HasColumnName("tong_tien_thue")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_donmuahang_thue>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_hoadonbanhang>().ToTable("c_hoadonbanhang");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.c_hoadonbanhang_id)
                            .HasColumnName("c_hoadonbanhang_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<c_hoadonbanhang>().HasKey<string>(p => p.c_hoadonbanhang_id);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.sochungtu)
                            .HasColumnName("sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.c_danhsachdathang_id)
                            .HasColumnName("c_danhsachdathang_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.chiphi)
                            .HasColumnName("chiphi")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.cod_kov)
                            .HasColumnName("cod_kov")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.diachi_nguoinhan)
                            .HasColumnName("diachi_nguoinhan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.diachi_nguoinhan_kov)
                            .HasColumnName("diachi_nguoinhan_kov")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.ghino_kov)
                            .HasColumnName("ghino_kov")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.giamgia)
                            .HasColumnName("giamgia")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.giamgia_kov)
                            .HasColumnName("giamgia_kov")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.hinhthucthanhtoan)
                            .HasColumnName("hinhthucthanhtoan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.hinhthucthanhtoan_kov)
                            .HasColumnName("hinhthucthanhtoan_kov")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.hoten_nguoinhan)
                            .HasColumnName("hoten_nguoinhan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.khachcantra_kov)
                            .HasColumnName("khachcantra_kov")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.khachthanhtoan_kov)
                            .HasColumnName("khachthanhtoan_kov")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.loai_kov)
                            .HasColumnName("loai_kov")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.loaithu_kov)
                            .HasColumnName("loaithu_kov")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.md_dieukienthanhtoan_id)
                            .HasColumnName("md_dieukienthanhtoan_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.mota_kov)
                            .HasColumnName("mota_kov")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.ngay_kov)
                            .HasColumnName("ngay_kov")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.ngaygiao)
                            .HasColumnName("ngaygiao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.ngayhieuluc)
                            .HasColumnName("ngayhieuluc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.ngayhoadon)
                            .HasColumnName("ngayhoadon")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.ngayhoanthanh)
                            .HasColumnName("ngayhoanthanh")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.nguoimua_kov)
                            .HasColumnName("nguoimua_kov")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.nguoimuaid_kov)
                            .HasColumnName("nguoimuaid_kov")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.nguoinhan_kov)
                            .HasColumnName("nguoinhan_kov")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.nhanvien_kov)
                            .HasColumnName("nhanvien_kov")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.nhanvienid)
                            .HasColumnName("nhanvienid")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.sdt_nguoinhan)
                            .HasColumnName("sdt_nguoinhan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.sdt_nguoinhan_kov)
                            .HasColumnName("sdt_nguoinhan_kov")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.thanhtoan)
                            .HasColumnName("thanhtoan")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.thongtinnhanhang)
                            .HasColumnName("thongtinnhanhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.thongtinsanpham)
                            .HasColumnName("thongtinsanpham")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.thongtinthanhtoan)
                            .HasColumnName("thongtinthanhtoan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.thongtinxuathoadon)
                            .HasColumnName("thongtinxuathoadon")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.thuho_cod_kov)
                            .HasColumnName("thuho_cod_kov")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.thukhac_kov)
                            .HasColumnName("thukhac_kov")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.thuthue_kov)
                            .HasColumnName("thuthue_kov")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.thuvanchuyen_kov)
                            .HasColumnName("thuvanchuyen_kov")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.tienthua_kov)
                            .HasColumnName("tienthua_kov")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.tong_tatca)
                            .HasColumnName("tong_tatca")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.tong_tienhang)
                            .HasColumnName("tong_tienhang")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.tongsoluong_kov)
                            .HasColumnName("tongsoluong_kov")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.tongtienhang_kov)
                            .HasColumnName("tongtienhang_kov")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.trangthai)
                            .HasColumnName("trangthai")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.trangthaicam)
                            .HasColumnName("trangthaicam")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.trangthaigiaohang)
                            .HasColumnName("trangthaigiaohang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.trangthaihoadon)
                            .HasColumnName("trangthaihoadon")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.trangthaithanhtoan)
                            .HasColumnName("trangthaithanhtoan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_hoadonbanhang_cdmh>().ToTable("c_hoadonbanhang_cdmh");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.c_hoadonbanhang_cdmh_id)
                            .HasColumnName("c_hoadonbanhang_cdmh_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<c_hoadonbanhang_cdmh>().HasKey<string>(p => p.c_hoadonbanhang_cdmh_id);
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.c_danhsachdathang_id)
                            .HasColumnName("c_danhsachdathang_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.c_hoadonbanhang_id)
                            .HasColumnName("c_hoadonbanhang_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.dongiamua)
                            .HasColumnName("dongiamua")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.giachuan)
                            .HasColumnName("giachuan")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.key_id)
                            .HasColumnName("key_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.md_sanpham_pr_id)
                            .HasColumnName("md_sanpham_pr_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.sl_dadat)
                            .HasColumnName("sl_dadat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.sl_hanngach)
                            .HasColumnName("sl_hanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.sl_tonkho)
                            .HasColumnName("sl_tonkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.stt)
                            .HasColumnName("stt")
                            .HasColumnType("int");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.thanhtien)
                            .HasColumnName("thanhtien")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.thanhtienThue)
                            .HasColumnName("thanhtienThue")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.thue)
                            .HasColumnName("thue")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_hoadonbanhang_cdmh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang>().ToTable("c_kehoachdathang");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.c_kehoachdathang_id)
                            .HasColumnName("c_kehoachdathang_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang>().HasKey<string>(p => p.c_kehoachdathang_id);
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.c_danhsachdathang_id)
                            .HasColumnName("c_danhsachdathang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.c_doichieuhangton_id)
                            .HasColumnName("c_doichieuhangton_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.donhang_thamchieu)
                            .HasColumnName("donhang_thamchieu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.donhangtron)
                            .HasColumnName("donhangtron")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.hangiaohangPO)
                            .HasColumnName("hangiaohangPO")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.ngaybatdausx)
                            .HasColumnName("ngaybatdausx")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.ngayhoanthanh)
                            .HasColumnName("ngayhoanthanh")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.ngaykehoach)
                            .HasColumnName("ngaykehoach")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.nhomKH)
                            .HasColumnName("nhomKH")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.nhomKHBTP)
                            .HasColumnName("nhomKHBTP")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.sanxuatton)
                            .HasColumnName("sanxuatton")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.sodonhang)
                            .HasColumnName("sodonhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.ten_kh)
                            .HasColumnName("ten_kh")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.tinhNCVT)
                            .HasColumnName("tinhNCVT")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.trangthai)
                            .HasColumnName("trangthai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.trangthaiSav)
                            .HasColumnName("trangthaiSav")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.xulykehoach)
                            .HasColumnName("xulykehoach")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.xulyNCVT)
                            .HasColumnName("xulyNCVT")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang>()
                            .Property(p => p.xuongTuChoiLSX)
                            .HasColumnName("xuongTuChoiLSX")
                            .HasColumnType("bit");
modelBuilder.Entity<c_kehoachdathang_cdhcd>().ToTable("c_kehoachdathang_cdhcd");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.c_kehoachdathang_cdhcd_id)
                            .HasColumnName("c_kehoachdathang_cdhcd_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang_cdhcd>().HasKey<string>(p => p.c_kehoachdathang_cdhcd_id);
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.c_kehoachdathang_id)
                            .HasColumnName("c_kehoachdathang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.macuoi)
                            .HasColumnName("macuoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.md_doitackinhdoanh_id)
                            .HasColumnName("md_doitackinhdoanh_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.noigiaohang)
                            .HasColumnName("noigiaohang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.sl_candat)
                            .HasColumnName("sl_candat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.sl_giamhanngach)
                            .HasColumnName("sl_giamhanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.sl_phanphoi)
                            .HasColumnName("sl_phanphoi")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_cdhcd>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang_dhcpx>().ToTable("c_kehoachdathang_dhcpx");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.c_kehoachdathang_dhcpx_id)
                            .HasColumnName("c_kehoachdathang_dhcpx_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang_dhcpx>().HasKey<string>(p => p.c_kehoachdathang_dhcpx_id);
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.c_danhsachdathang_id)
                            .HasColumnName("c_danhsachdathang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.c_kehoachdathang_id)
                            .HasColumnName("c_kehoachdathang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.dongdathang)
                            .HasColumnName("dongdathang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.donhang)
                            .HasColumnName("donhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.hdlh)
                            .HasColumnName("hdlh")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.hdlhchung)
                            .HasColumnName("hdlhchung")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.lenhSXBTP)
                            .HasColumnName("lenhSXBTP")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.lenhSXTP)
                            .HasColumnName("lenhSXTP")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.ngayBDSX)
                            .HasColumnName("ngayBDSX")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.ngayHTcham)
                            .HasColumnName("ngayHTcham")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.sctdathang)
                            .HasColumnName("sctdathang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.tinh_ncvt)
                            .HasColumnName("tinh_ncvt")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>().ToTable("c_kehoachdathang_dhcpx_cdh");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.c_kehoachdathang_dhcpx_cdh_id)
                            .HasColumnName("c_kehoachdathang_dhcpx_cdh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>().HasKey<string>(p => p.c_kehoachdathang_dhcpx_cdh_id);
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.c_kehoachdathang_dhcpx_id)
                            .HasColumnName("c_kehoachdathang_dhcpx_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.c_kehoachdathang_id)
                            .HasColumnName("c_kehoachdathang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.macuoi)
                            .HasColumnName("macuoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.md_sanpham_bom_id)
                            .HasColumnName("md_sanpham_bom_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.noigiaohang)
                            .HasColumnName("noigiaohang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.soluong)
                            .HasColumnName("soluong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_cdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>().ToTable("c_kehoachdathang_dhcpx_vattu");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.c_kehoachdathang_dhcpx_vattu_id)
                            .HasColumnName("c_kehoachdathang_dhcpx_vattu_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>().HasKey<string>(p => p.c_kehoachdathang_dhcpx_vattu_id);
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.c_kehoachdathang_dhcpx_id)
                            .HasColumnName("c_kehoachdathang_dhcpx_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.c_kehoachdathang_id)
                            .HasColumnName("c_kehoachdathang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.sl_duyetmua)
                            .HasColumnName("sl_duyetmua")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.sl_giamhanngach)
                            .HasColumnName("sl_giamhanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.sl_hanngach)
                            .HasColumnName("sl_hanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.soluong)
                            .HasColumnName("soluong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhcpx_vattu>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang_dhncc>().ToTable("c_kehoachdathang_dhncc");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.c_kehoachdathang_dhncc_id)
                            .HasColumnName("c_kehoachdathang_dhncc_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang_dhncc>().HasKey<string>(p => p.c_kehoachdathang_dhncc_id);
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.banggia)
                            .HasColumnName("banggia")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.c_kehoachdathang_id)
                            .HasColumnName("c_kehoachdathang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.chungtu)
                            .HasColumnName("chungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.donhang)
                            .HasColumnName("donhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.md_doitackinhdoanh_id)
                            .HasColumnName("md_doitackinhdoanh_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.sctdathang)
                            .HasColumnName("sctdathang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.thoihan_giaohang)
                            .HasColumnName("thoihan_giaohang")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.thoihan_hoanthanh)
                            .HasColumnName("thoihan_hoanthanh")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>().ToTable("c_kehoachdathang_dhncc_cdh");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.c_kehoachdathang_dhncc_cdh_id)
                            .HasColumnName("c_kehoachdathang_dhncc_cdh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>().HasKey<string>(p => p.c_kehoachdathang_dhncc_cdh_id);
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.c_kehoachdathang_dhncc_id)
                            .HasColumnName("c_kehoachdathang_dhncc_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.c_kehoachdathang_id)
                            .HasColumnName("c_kehoachdathang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.macuoi)
                            .HasColumnName("macuoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.ngaycan)
                            .HasColumnName("ngaycan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.soluong)
                            .HasColumnName("soluong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathang_dhncc_cdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathangtong>().ToTable("c_kehoachdathangtong");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.c_kehoachdathangtong_id)
                            .HasColumnName("c_kehoachdathangtong_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachdathangtong>().HasKey<string>(p => p.c_kehoachdathangtong_id);
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.denngay)
                            .HasColumnName("denngay")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.loaiKH)
                            .HasColumnName("loaiKH")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.ngaybatdausx)
                            .HasColumnName("ngaybatdausx")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.ngayhieuluc)
                            .HasColumnName("ngayhieuluc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.ngaykehoach)
                            .HasColumnName("ngaykehoach")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.ten_kh)
                            .HasColumnName("ten_kh")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.trangthai)
                            .HasColumnName("trangthai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.tungay)
                            .HasColumnName("tungay")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachdathangtong>()
                            .Property(p => p.xulykehoach)
                            .HasColumnName("xulykehoach")
                            .HasColumnType("bit");
modelBuilder.Entity<c_kehoachmuavt>().ToTable("c_kehoachmuavt");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.c_kehoachmuavt_id)
                            .HasColumnName("c_kehoachmuavt_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<c_kehoachmuavt>().HasKey<string>(p => p.c_kehoachmuavt_id);
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.sochungtu)
                            .HasColumnName("sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.bophanyc)
                            .HasColumnName("bophanyc")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.bophanyc_value)
                            .HasColumnName("bophanyc_value")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.c_donmuavattu_id)
                            .HasColumnName("c_donmuavattu_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.denngay)
                            .HasColumnName("denngay")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.denngayNCVT)
                            .HasColumnName("denngayNCVT")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.md_trangthai_id)
                            .HasColumnName("md_trangthai_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.ngaycan)
                            .HasColumnName("ngaycan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.ngaykehoach)
                            .HasColumnName("ngaykehoach")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.phieuhangton)
                            .HasColumnName("phieuhangton")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.ten_kehoach)
                            .HasColumnName("ten_kehoach")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.tungay)
                            .HasColumnName("tungay")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.tungayNCVT)
                            .HasColumnName("tungayNCVT")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachmuavt_cdh>().ToTable("c_kehoachmuavt_cdh");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.c_kehoachmuavt_cdh_id)
                            .HasColumnName("c_kehoachmuavt_cdh_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<c_kehoachmuavt_cdh>().HasKey<string>(p => p.c_kehoachmuavt_cdh_id);
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.c_kehoachmuavt_id)
                            .HasColumnName("c_kehoachmuavt_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.md_doitackinhdoanh_id)
                            .HasColumnName("md_doitackinhdoanh_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.ngayphaico)
                            .HasColumnName("ngayphaico")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.sl_can)
                            .HasColumnName("sl_can")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.sl_canthem)
                            .HasColumnName("sl_canthem")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.sl_conlai)
                            .HasColumnName("sl_conlai")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.sl_denghi)
                            .HasColumnName("sl_denghi")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.sl_duyet)
                            .HasColumnName("sl_duyet")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.sl_duyet2)
                            .HasColumnName("sl_duyet2")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.sl_layton)
                            .HasColumnName("sl_layton")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.sl_tonkho)
                            .HasColumnName("sl_tonkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.sl_tonkho_toithieu)
                            .HasColumnName("sl_tonkho_toithieu")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.sl_xuatkho)
                            .HasColumnName("sl_xuatkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_cdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachmuavt_dklht>().ToTable("c_kehoachmuavt_dklht");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.c_kehoachmuavt_dklht_id)
                            .HasColumnName("c_kehoachmuavt_dklht_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_kehoachmuavt_dklht>().HasKey<string>(p => p.c_kehoachmuavt_dklht_id);
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.c_kehoachmuavt_id)
                            .HasColumnName("c_kehoachmuavt_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.makho)
                            .HasColumnName("makho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.sl_dalayton)
                            .HasColumnName("sl_dalayton")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.sl_giamhanngach)
                            .HasColumnName("sl_giamhanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.sl_layton)
                            .HasColumnName("sl_layton")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.sl_tonkho)
                            .HasColumnName("sl_tonkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_kehoachmuavt_dklht>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
            #endregion End Code
        }
    }
}
