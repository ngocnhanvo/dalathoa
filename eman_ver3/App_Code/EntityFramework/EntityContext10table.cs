
using System.Data.Entity;
namespace DataAcess
{
    public class EntityContext10table
    {
        public void exec(DbModelBuilder modelBuilder)
        {
            #region Start Code
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
