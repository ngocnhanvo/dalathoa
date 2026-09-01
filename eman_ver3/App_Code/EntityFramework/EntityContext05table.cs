
using System.Data.Entity;
namespace DataAcess
{
    public class EntityContext05table
    {
        public void exec(DbModelBuilder modelBuilder)
        {
            #region Start Code
            modelBuilder.Entity<md_kho_dasudung>().ToTable("md_kho_dasudung");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.md_kho_dasudung_id)
                            .HasColumnName("md_kho_dasudung_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_kho_dasudung>().HasKey<string>(p => p.md_kho_dasudung_id);
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.lsx_to)
                            .HasColumnName("lsx_to")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.lydo)
                            .HasColumnName("lydo")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.md_to_id)
                            .HasColumnName("md_to_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.ngaydung)
                            .HasColumnName("ngaydung")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.sctlienquan)
                            .HasColumnName("sctlienquan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.soluong_dadung)
                            .HasColumnName("soluong_dadung")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.soluong_dadung2)
                            .HasColumnName("soluong_dadung2")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.tosx)
                            .HasColumnName("tosx")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_dasudung>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_kho_ghino>().ToTable("md_kho_ghino");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.md_kho_ghino_id)
                            .HasColumnName("md_kho_ghino_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_kho_ghino>().HasKey<string>(p => p.md_kho_ghino_id);
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.lsx_to)
                            .HasColumnName("lsx_to")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.lydo)
                            .HasColumnName("lydo")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.md_to_id)
                            .HasColumnName("md_to_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.ngayno)
                            .HasColumnName("ngayno")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.sctlienquan)
                            .HasColumnName("sctlienquan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.soluong_no)
                            .HasColumnName("soluong_no")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.tosx)
                            .HasColumnName("tosx")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_ghino>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_kho_giaodich>().ToTable("md_kho_giaodich");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.md_kho_giaodich_id)
                            .HasColumnName("md_kho_giaodich_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_kho_giaodich>().HasKey<string>(p => p.md_kho_giaodich_id);
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.dongkiemkho)
                            .HasColumnName("dongkiemkho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.dongnhapxuat)
                            .HasColumnName("dongnhapxuat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.dongsanxuat)
                            .HasColumnName("dongsanxuat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.dongvanchuyen)
                            .HasColumnName("dongvanchuyen")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.donhang)
                            .HasColumnName("donhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.gianhancong)
                            .HasColumnName("gianhancong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.giatriVND)
                            .HasColumnName("giatriVND")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.kieuchuyen)
                            .HasColumnName("kieuchuyen")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.lsx)
                            .HasColumnName("lsx")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.md_to_id)
                            .HasColumnName("md_to_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.md_to_id2)
                            .HasColumnName("md_to_id2")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.ngaychuyen)
                            .HasColumnName("ngaychuyen")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.pbgNC)
                            .HasColumnName("pbgNC")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.sanxuat)
                            .HasColumnName("sanxuat")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.soluong_dichchuyen)
                            .HasColumnName("soluong_dichchuyen")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.tosxId)
                            .HasColumnName("tosxId")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.tosxId2)
                            .HasColumnName("tosxId2")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giaodich>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_kho_giucho>().ToTable("md_kho_giucho");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.md_kho_giucho_id)
                            .HasColumnName("md_kho_giucho_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_kho_giucho>().HasKey<string>(p => p.md_kho_giucho_id);
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.lydo)
                            .HasColumnName("lydo")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.ngaygiu)
                            .HasColumnName("ngaygiu")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.sctlienquan)
                            .HasColumnName("sctlienquan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.soluong_giucho)
                            .HasColumnName("soluong_giucho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_giucho>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_kho_sanpham>().ToTable("md_kho_sanpham");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.md_kho_sanpham_id)
                            .HasColumnName("md_kho_sanpham_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_kho_sanpham>().HasKey<string>(p => p.md_kho_sanpham_id);
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.ngay_hethan)
                            .HasColumnName("ngay_hethan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.ngaytinh_tonkhocuoi)
                            .HasColumnName("ngaytinh_tonkhocuoi")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.soluong)
                            .HasColumnName("soluong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_kho_sanpham_hansudung>().ToTable("md_kho_sanpham_hansudung");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.md_kho_sanpham_hansudung_id)
                            .HasColumnName("md_kho_sanpham_hansudung_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_kho_sanpham_hansudung>().HasKey<string>(p => p.md_kho_sanpham_hansudung_id);
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.md_kho_sanpham_id)
                            .HasColumnName("md_kho_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.ngay_hethan)
                            .HasColumnName("ngay_hethan")
                            .HasColumnType("date");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.soluong)
                            .HasColumnName("soluong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_sanpham_hansudung>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_kho_the>().ToTable("md_kho_the");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.md_thekho_id)
                            .HasColumnName("md_thekho_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_kho_the>().HasKey<string>(p => p.md_thekho_id);
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.dongchuyen)
                            .HasColumnName("dongchuyen")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.dongkiem)
                            .HasColumnName("dongkiem")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.dongsx)
                            .HasColumnName("dongsx")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.dongxuat)
                            .HasColumnName("dongxuat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.dualq)
                            .HasColumnName("dualq")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.kieuxuatnhap)
                            .HasColumnName("kieuxuatnhap")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.md_donvidh_id)
                            .HasColumnName("md_donvidh_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.md_donvido_id)
                            .HasColumnName("md_donvido_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.ngaychuyen)
                            .HasColumnName("ngaychuyen")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.soluongdc)
                            .HasColumnName("soluongdc")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.soluongdh)
                            .HasColumnName("soluongdh")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.thongsobtt)
                            .HasColumnName("thongsobtt")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kho_the>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_khuvuc>().ToTable("md_khuvuc");
                            modelBuilder.Entity<md_khuvuc>()
                            .Property(p => p.md_khuvuc_id)
                            .HasColumnName("md_khuvuc_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_khuvuc>().HasKey<string>(p => p.md_khuvuc_id);
                            modelBuilder.Entity<md_khuvuc>()
                            .Property(p => p.anco_check)
                            .HasColumnName("anco_check")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_khuvuc>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_khuvuc>()
                            .Property(p => p.ma_khuvuc)
                            .HasColumnName("ma_khuvuc")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_khuvuc>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_khuvuc>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_khuvuc>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_khuvuc>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_khuvuc>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_khuvuc>()
                            .Property(p => p.ten_khuvuc)
                            .HasColumnName("ten_khuvuc")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_kiemke>().ToTable("md_kiemke");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.md_kiemke_id)
                            .HasColumnName("md_kiemke_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_kiemke>().HasKey<string>(p => p.md_kiemke_id);
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.ma_kiemke)
                            .HasColumnName("ma_kiemke")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.ngay_kiemke)
                            .HasColumnName("ngay_kiemke")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.ngaydenghi)
                            .HasColumnName("ngaydenghi")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.ngayhieuluc)
                            .HasColumnName("ngayhieuluc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.ngayxacnhan)
                            .HasColumnName("ngayxacnhan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.sochungtu)
                            .HasColumnName("sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.ten_kiemke)
                            .HasColumnName("ten_kiemke")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.tudong)
                            .HasColumnName("tudong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_kiemke_cdh>().ToTable("md_kiemke_cdh");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.md_kiemke_cdh_id)
                            .HasColumnName("md_kiemke_cdh_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_kiemke_cdh>().HasKey<string>(p => p.md_kiemke_cdh_id);
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.ma_sanpham)
                            .HasColumnName("ma_sanpham")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.md_kiemke_id)
                            .HasColumnName("md_kiemke_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.mota_tiengviet)
                            .HasColumnName("mota_tiengviet")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.sl_demduoc)
                            .HasColumnName("sl_demduoc")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.sl_sosach)
                            .HasColumnName("sl_sosach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_kiemke_cdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhdonggoi>().ToTable("md_lenhdonggoi");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.md_lenhdonggoi_id)
                            .HasColumnName("md_lenhdonggoi_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_lenhdonggoi>().HasKey<string>(p => p.md_lenhdonggoi_id);
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.sochungtu)
                            .HasColumnName("sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.c_danhsachdathang_id)
                            .HasColumnName("c_danhsachdathang_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.md_trangthai_id)
                            .HasColumnName("md_trangthai_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.ngayhoanthanh)
                            .HasColumnName("ngayhoanthanh")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.ngayketthuc)
                            .HasColumnName("ngayketthuc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.ngaylap)
                            .HasColumnName("ngaylap")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhdonggoi_dh>().ToTable("md_lenhdonggoi_dh");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.md_lenhdonggoi_dh_id)
                            .HasColumnName("md_lenhdonggoi_dh_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_lenhdonggoi_dh>().HasKey<string>(p => p.md_lenhdonggoi_dh_id);
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.bomdonggoi_id)
                            .HasColumnName("bomdonggoi_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.md_lenhdonggoi_id)
                            .HasColumnName("md_lenhdonggoi_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.ngayhoanthanh)
                            .HasColumnName("ngayhoanthanh")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.ngayketthuc)
                            .HasColumnName("ngayketthuc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.ngaylap)
                            .HasColumnName("ngaylap")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.sl_donggoi)
                            .HasColumnName("sl_donggoi")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.sl_donhang)
                            .HasColumnName("sl_donhang")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_dh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhdonggoi_vattu>().ToTable("md_lenhdonggoi_vattu");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.md_lenhdonggoi_vattu_id)
                            .HasColumnName("md_lenhdonggoi_vattu_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_lenhdonggoi_vattu>().HasKey<string>(p => p.md_lenhdonggoi_vattu_id);
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.md_lenhdonggoi_id)
                            .HasColumnName("md_lenhdonggoi_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.soluong)
                            .HasColumnName("soluong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.soluong_dagiao)
                            .HasColumnName("soluong_dagiao")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhdonggoi_vattu>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat>().ToTable("md_lenhsanxuat");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.md_lenhsanxuat_id)
                            .HasColumnName("md_lenhsanxuat_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat>().HasKey<string>(p => p.md_lenhsanxuat_id);
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.sochungtu)
                            .HasColumnName("sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.c_kehoachdathang_id)
                            .HasColumnName("c_kehoachdathang_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.check_dtvt)
                            .HasColumnName("check_dtvt")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.dhtron)
                            .HasColumnName("dhtron")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.donhang_thamchieu)
                            .HasColumnName("donhang_thamchieu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.lydogiaosom_tre)
                            .HasColumnName("lydogiaosom_tre")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.md_dondathangphanxuong_id)
                            .HasColumnName("md_dondathangphanxuong_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.md_trangthai_id)
                            .HasColumnName("md_trangthai_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.ngaydkgiaotp)
                            .HasColumnName("ngaydkgiaotp")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.ngayhoanthanh)
                            .HasColumnName("ngayhoanthanh")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.ngayketthuc)
                            .HasColumnName("ngayketthuc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.ngaylap)
                            .HasColumnName("ngaylap")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.ngayxlhangton)
                            .HasColumnName("ngayxlhangton")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.nhomKH)
                            .HasColumnName("nhomKH")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.nhomKHBTP)
                            .HasColumnName("nhomKHBTP")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.phieuXK)
                            .HasColumnName("phieuXK")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.sxton)
                            .HasColumnName("sxton")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat_ddsx>().ToTable("md_lenhsanxuat_ddsx");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.md_lenhsanxuat_ddsx_id)
                            .HasColumnName("md_lenhsanxuat_ddsx_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat_ddsx>().HasKey<string>(p => p.md_lenhsanxuat_ddsx_id);
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.check_hoanthanh)
                            .HasColumnName("check_hoanthanh")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.md_lenhsanxuat_id)
                            .HasColumnName("md_lenhsanxuat_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.sl_canlam)
                            .HasColumnName("sl_canlam")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.sl_dalam)
                            .HasColumnName("sl_dalam")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.sl_dk_khuon)
                            .HasColumnName("sl_dk_khuon")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.sl_dukien)
                            .HasColumnName("sl_dukien")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.sl_khuon)
                            .HasColumnName("sl_khuon")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.songay)
                            .HasColumnName("songay")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_ddsx>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat_tosx>().ToTable("md_lenhsanxuat_tosx");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.md_lenhsanxuat_tosx_id)
                            .HasColumnName("md_lenhsanxuat_tosx_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat_tosx>().HasKey<string>(p => p.md_lenhsanxuat_tosx_id);
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.check_hoanthanh)
                            .HasColumnName("check_hoanthanh")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.ma_to)
                            .HasColumnName("ma_to")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.md_lenhsanxuat_id)
                            .HasColumnName("md_lenhsanxuat_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.md_phanxuong_to_id)
                            .HasColumnName("md_phanxuong_to_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.ngaybatdausx)
                            .HasColumnName("ngaybatdausx")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.ngaydkht)
                            .HasColumnName("ngaydkht")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.ngaygiao)
                            .HasColumnName("ngaygiao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.ngayhoanthanh)
                            .HasColumnName("ngayhoanthanh")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.phieudoimau)
                            .HasColumnName("phieudoimau")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.phieulayht)
                            .HasColumnName("phieulayht")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.phieulayhttp)
                            .HasColumnName("phieulayhttp")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.phongbanId)
                            .HasColumnName("phongbanId")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.stt)
                            .HasColumnName("stt")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.stt_sapxep)
                            .HasColumnName("stt_sapxep")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.tinhhangton)
                            .HasColumnName("tinhhangton")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.tinhhangtontp)
                            .HasColumnName("tinhhangtontp")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.tolayton)
                            .HasColumnName("tolayton")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.tolaytonId)
                            .HasColumnName("tolaytonId")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.tolaytontpId)
                            .HasColumnName("tolaytontpId")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.xuongChinh)
                            .HasColumnName("xuongChinh")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx>()
                            .Property(p => p.xuongPhu)
                            .HasColumnName("xuongPhu")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>().ToTable("md_lenhsanxuat_tosx_cdh");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.md_lenhsanxuat_tosx_cdh_id)
                            .HasColumnName("md_lenhsanxuat_tosx_cdh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>().HasKey<string>(p => p.md_lenhsanxuat_tosx_cdh_id);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.bomId)
                            .HasColumnName("bomId")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.gianhancong)
                            .HasColumnName("gianhancong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.lsxCT)
                            .HasColumnName("lsxCT")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.lsxTen)
                            .HasColumnName("lsxTen")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.mabo)
                            .HasColumnName("mabo")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.macuoi)
                            .HasColumnName("macuoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.mathaydoi)
                            .HasColumnName("mathaydoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.md_lenhsanxuat_id)
                            .HasColumnName("md_lenhsanxuat_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.md_lenhsanxuat_tosx_id)
                            .HasColumnName("md_lenhsanxuat_tosx_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.ncc)
                            .HasColumnName("ncc")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.ngayhoanthanh)
                            .HasColumnName("ngayhoanthanh")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.nhanphoisoi)
                            .HasColumnName("nhanphoisoi")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.nhomnangluc)
                            .HasColumnName("nhomnangluc")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.noigiaohang)
                            .HasColumnName("noigiaohang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.pbgId)
                            .HasColumnName("pbgId")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_chiato)
                            .HasColumnName("sl_chiato")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_chiato2)
                            .HasColumnName("sl_chiato2")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_chophepnkton)
                            .HasColumnName("sl_chophepnkton")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_dagiao)
                            .HasColumnName("sl_dagiao")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_dahoanthanh)
                            .HasColumnName("sl_dahoanthanh")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_danhapkho)
                            .HasColumnName("sl_danhapkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_dat)
                            .HasColumnName("sl_dat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_dat2)
                            .HasColumnName("sl_dat2")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_datncc)
                            .HasColumnName("sl_datncc")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_datncc2)
                            .HasColumnName("sl_datncc2")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_giamhanngach)
                            .HasColumnName("sl_giamhanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_hoanthanh)
                            .HasColumnName("sl_hoanthanh")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_layton)
                            .HasColumnName("sl_layton")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_laytondu)
                            .HasColumnName("sl_laytondu")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_nhapkho)
                            .HasColumnName("sl_nhapkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_nkton)
                            .HasColumnName("sl_nkton")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_tamgiao)
                            .HasColumnName("sl_tamgiao")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sl_tinhvattu)
                            .HasColumnName("sl_tinhvattu")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.sp1)
                            .HasColumnName("sp1")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.stt)
                            .HasColumnName("stt")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.stt_sapxep)
                            .HasColumnName("stt_sapxep")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.xuongChinh)
                            .HasColumnName("xuongChinh")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_cdh>()
                            .Property(p => p.xuongPhu)
                            .HasColumnName("xuongPhu")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>().ToTable("md_lenhsanxuat_tosx_dklht");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.md_lenhsanxuat_tosx_dklht_id)
                            .HasColumnName("md_lenhsanxuat_tosx_dklht_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>().HasKey<string>(p => p.md_lenhsanxuat_tosx_dklht_id);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.macuoi)
                            .HasColumnName("macuoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.mathaydoi)
                            .HasColumnName("mathaydoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.md_lenhsanxuat_id)
                            .HasColumnName("md_lenhsanxuat_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.md_lenhsanxuat_tosx_id)
                            .HasColumnName("md_lenhsanxuat_tosx_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.sl_layton)
                            .HasColumnName("sl_layton")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.sl_layton_dachot)
                            .HasColumnName("sl_layton_dachot")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.sl_layton1)
                            .HasColumnName("sl_layton1")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.sl_lsx)
                            .HasColumnName("sl_lsx")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.sl_tonkho)
                            .HasColumnName("sl_tonkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklht>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>().ToTable("md_lenhsanxuat_tosx_dklhttp");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.md_lenhsanxuat_tosx_dklhttp_id)
                            .HasColumnName("md_lenhsanxuat_tosx_dklhttp_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>().HasKey<string>(p => p.md_lenhsanxuat_tosx_dklhttp_id);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.macuoi)
                            .HasColumnName("macuoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.md_lenhsanxuat_id)
                            .HasColumnName("md_lenhsanxuat_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.md_lenhsanxuat_tosx_dklht_id)
                            .HasColumnName("md_lenhsanxuat_tosx_dklht_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.md_lenhsanxuat_tosx_id)
                            .HasColumnName("md_lenhsanxuat_tosx_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.sl_layton)
                            .HasColumnName("sl_layton")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.sl_layton_dachot)
                            .HasColumnName("sl_layton_dachot")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.sl_layton1)
                            .HasColumnName("sl_layton1")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.sl_lsx)
                            .HasColumnName("sl_lsx")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.sl_tonkho)
                            .HasColumnName("sl_tonkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_dklhttp>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>().ToTable("md_lenhsanxuat_tosx_tonkho");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.md_lenhsanxuat_tosx_tonkho_id)
                            .HasColumnName("md_lenhsanxuat_tosx_tonkho_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>().HasKey<string>(p => p.md_lenhsanxuat_tosx_tonkho_id);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.macungkieudang)
                            .HasColumnName("macungkieudang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.md_lenhsanxuat_id)
                            .HasColumnName("md_lenhsanxuat_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.md_lenhsanxuat_tosx_id)
                            .HasColumnName("md_lenhsanxuat_tosx_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.sl_tonkho)
                            .HasColumnName("sl_tonkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_lenhsanxuat_tosx_tonkho>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                    
                    
            #endregion End Code
        }
    }
}
