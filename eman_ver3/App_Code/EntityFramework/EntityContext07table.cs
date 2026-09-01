
using System.Data.Entity;
namespace DataAcess
{
    public class EntityContext07table
    {
        public void exec(DbModelBuilder modelBuilder)
        {
            #region Start Code
            modelBuilder.Entity<md_nhapkhoton_cdh>().ToTable("md_nhapkhoton_cdh");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.md_nhapkhoton_cdh_id)
                            .HasColumnName("md_nhapkhoton_cdh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_nhapkhoton_cdh>().HasKey<string>(p => p.md_nhapkhoton_cdh_id);
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.check_kho)
                            .HasColumnName("check_kho")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.ghichu_donvi2)
                            .HasColumnName("ghichu_donvi2")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.khoden)
                            .HasColumnName("khoden")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.md_nhapkhoton_id)
                            .HasColumnName("md_nhapkhoton_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.sl_danhap)
                            .HasColumnName("sl_danhap")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.sl_nhap)
                            .HasColumnName("sl_nhap")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.sl_tonkho)
                            .HasColumnName("sl_tonkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.tong_sl_dat)
                            .HasColumnName("tong_sl_dat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhapkhoton_cdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_nhomnangluc>().ToTable("md_nhomnangluc");
                            modelBuilder.Entity<md_nhomnangluc>()
                            .Property(p => p.md_nhomnangluc_id)
                            .HasColumnName("md_nhomnangluc_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_nhomnangluc>().HasKey<string>(p => p.md_nhomnangluc_id);
                            modelBuilder.Entity<md_nhomnangluc>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_nhomnangluc>()
                            .Property(p => p.ma_nhom)
                            .HasColumnName("ma_nhom")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhomnangluc>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhomnangluc>()
                            .Property(p => p.mota_tiengviet)
                            .HasColumnName("mota_tiengviet")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhomnangluc>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_nhomnangluc>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_nhomnangluc>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhomnangluc>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhomnangluc>()
                            .Property(p => p.ten_nhom)
                            .HasColumnName("ten_nhom")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_nhomnangluc>()
                            .Property(p => p.thoigianlamhang)
                            .HasColumnName("thoigianlamhang")
                            .HasColumnType("int");
modelBuilder.Entity<md_phannnhommx>().ToTable("md_phannnhommx");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.md_phannnhommx_id)
                            .HasColumnName("md_phannnhommx_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phannnhommx>().HasKey<string>(p => p.md_phannnhommx_id);
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.dientich_lonnhat)
                            .HasColumnName("dientich_lonnhat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.dientich_nhonhat)
                            .HasColumnName("dientich_nhonhat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.dongia_mauA)
                            .HasColumnName("dongia_mauA")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.dongia_mauB)
                            .HasColumnName("dongia_mauB")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.hinhdang)
                            .HasColumnName("hinhdang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.ketcausoi)
                            .HasColumnName("ketcausoi")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.ma_mx)
                            .HasColumnName("ma_mx")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.titrong)
                            .HasColumnName("titrong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phannnhommx_cdpk>().ToTable("md_phannnhommx_cdpk");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.md_phannnhommx_cdpk_id)
                            .HasColumnName("md_phannnhommx_cdpk_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phannnhommx_cdpk>().HasKey<string>(p => p.md_phannnhommx_cdpk_id);
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.md_phannnhommx_id)
                            .HasColumnName("md_phannnhommx_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.md_sanphammausac_id)
                            .HasColumnName("md_sanphammausac_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.tile)
                            .HasColumnName("tile")
                            .HasColumnType("float");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.tile_cdct)
                            .HasColumnName("tile_cdct")
                            .HasColumnType("float");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.tile_v52)
                            .HasColumnName("tile_v52")
                            .HasColumnType("float");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_cdpk>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phannnhommx_qhd>().ToTable("md_phannnhommx_qhd");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.md_phannnhommx_qhd_id)
                            .HasColumnName("md_phannnhommx_qhd_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phannnhommx_qhd>().HasKey<string>(p => p.md_phannnhommx_qhd_id);
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.md_phannnhommx_id)
                            .HasColumnName("md_phannnhommx_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.tile)
                            .HasColumnName("tile")
                            .HasColumnType("float");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_qhd>()
                            .Property(p => p.vattu_value)
                            .HasColumnName("vattu_value")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phannnhommx_tlkthh>().ToTable("md_phannnhommx_tlkthh");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.md_phannnhommx_tlkthh_id)
                            .HasColumnName("md_phannnhommx_tlkthh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phannnhommx_tlkthh>().HasKey<string>(p => p.md_phannnhommx_tlkthh_id);
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.md_phannnhommx_id)
                            .HasColumnName("md_phannnhommx_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.md_sanphammausac_id)
                            .HasColumnName("md_sanphammausac_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.tile)
                            .HasColumnName("tile")
                            .HasColumnType("float");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phannnhommx_tlkthh>()
                            .Property(p => p.vattu_value)
                            .HasColumnName("vattu_value")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phanxuong>().ToTable("md_phanxuong");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_phanxuong>().HasKey<string>(p => p.md_phanxuong_id);
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.ma_phanxuong)
                            .HasColumnName("ma_phanxuong")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.ten_phanxuong)
                            .HasColumnName("ten_phanxuong")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.thuocPX)
                            .HasColumnName("thuocPX")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phanxuong_to>().ToTable("md_phanxuong_to");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.md_to_id)
                            .HasColumnName("md_to_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_phanxuong_to>().HasKey<string>(p => p.md_to_id);
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.congdoan)
                            .HasColumnName("congdoan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.ma_to)
                            .HasColumnName("ma_to")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.phongbanId)
                            .HasColumnName("phongbanId")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.phongbanId2)
                            .HasColumnName("phongbanId2")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.ten_to)
                            .HasColumnName("ten_to")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.ten_to_cu)
                            .HasColumnName("ten_to_cu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuong_to>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phanxuongMain>().ToTable("md_phanxuongMain");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.md_phanxuongMain_id)
                            .HasColumnName("md_phanxuongMain_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_phanxuongMain>().HasKey<string>(p => p.md_phanxuongMain_id);
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.ma_phanxuongMain)
                            .HasColumnName("ma_phanxuongMain")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.ten_phanxuongMain)
                            .HasColumnName("ten_phanxuongMain")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.thuocPX)
                            .HasColumnName("thuocPX")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phanxuongMain>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phienbangia>().ToTable("md_phienbangia");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.md_phienbangia_id)
                            .HasColumnName("md_phienbangia_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_phienbangia>().HasKey<string>(p => p.md_phienbangia_id);
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.md_banggia_id)
                            .HasColumnName("md_banggia_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.md_trangthai_id)
                            .HasColumnName("md_trangthai_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.ngay_hieuluc)
                            .HasColumnName("ngay_hieuluc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.ngayHL)
                            .HasColumnName("ngayHL")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.ten_phienbangia)
                            .HasColumnName("ten_phienbangia")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.trangthai)
                            .HasColumnName("trangthai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_phienbangia>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_quocgia>().ToTable("md_quocgia");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.md_quocgia_id)
                            .HasColumnName("md_quocgia_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_quocgia>().HasKey<string>(p => p.md_quocgia_id);
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.anco_check)
                            .HasColumnName("anco_check")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.ma_quocgia)
                            .HasColumnName("ma_quocgia")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.ten_quocgia)
                            .HasColumnName("ten_quocgia")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quocgia>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_quy>().ToTable("md_quy");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.md_quy_id)
                            .HasColumnName("md_quy_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_quy>().HasKey<string>(p => p.md_quy_id);
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.chu_taikhoan)
                            .HasColumnName("chu_taikhoan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.loai_quy)
                            .HasColumnName("loai_quy")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.ma_quy)
                            .HasColumnName("ma_quy")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.ngay_sodu_ban_dau)
                            .HasColumnName("ngay_sodu_ban_dau")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.so_taikhoan)
                            .HasColumnName("so_taikhoan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.sodu_ban_dau)
                            .HasColumnName("sodu_ban_dau")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.ten_nganhang)
                            .HasColumnName("ten_nganhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.ten_quy)
                            .HasColumnName("ten_quy")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_quy>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanpham>().ToTable("md_sanpham");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanpham>().HasKey<string>(p => p.md_sanpham_id);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.anco_check)
                            .HasColumnName("anco_check")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.ban_thanhpham)
                            .HasColumnName("ban_thanhpham")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.co_ngayhethan)
                            .HasColumnName("co_ngayhethan")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.dathang)
                            .HasColumnName("dathang")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.dientich)
                            .HasColumnName("dientich")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.dinhkhoan)
                            .HasColumnName("dinhkhoan")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.dokho)
                            .HasColumnName("dokho")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.ghichu)
                            .HasColumnName("ghichu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.giaban)
                            .HasColumnName("giaban")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.giabinhquan)
                            .HasColumnName("giabinhquan")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.h_cm)
                            .HasColumnName("h_cm")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.h_inch)
                            .HasColumnName("h_inch")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.heso)
                            .HasColumnName("heso")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.khomacdinh)
                            .HasColumnName("khomacdinh")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.khoton)
                            .HasColumnName("khoton")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.khuon)
                            .HasColumnName("khuon")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.l_cm)
                            .HasColumnName("l_cm")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.l_inch)
                            .HasColumnName("l_inch")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.ma_sanpham)
                            .HasColumnName("ma_sanpham")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.ma_sanphamcu)
                            .HasColumnName("ma_sanphamcu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.ma_vach)
                            .HasColumnName("ma_vach")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_cangbien_id)
                            .HasColumnName("md_cangbien_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_chucnang_id)
                            .HasColumnName("md_chucnang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_chungloai_id)
                            .HasColumnName("md_chungloai_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_hscode_id)
                            .HasColumnName("md_hscode_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_kieudang_id)
                            .HasColumnName("md_kieudang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_nhomnangluc_id)
                            .HasColumnName("md_nhomnangluc_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_phanloaihh_id)
                            .HasColumnName("md_phanloaihh_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_thue_sanpham_id)
                            .HasColumnName("md_thue_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.md_to_id)
                            .HasColumnName("md_to_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.mota_tienganh)
                            .HasColumnName("mota_tienganh")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.mota_tiengviet)
                            .HasColumnName("mota_tiengviet")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.muangoai)
                            .HasColumnName("muangoai")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.nhacungung)
                            .HasColumnName("nhacungung")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.noigiaohang)
                            .HasColumnName("noigiaohang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.phongbanId)
                            .HasColumnName("phongbanId")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.quycachdonggoi)
                            .HasColumnName("quycachdonggoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.sanpham)
                            .HasColumnName("sanpham")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.sl_tonkho_toithieu)
                            .HasColumnName("sl_tonkho_toithieu")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.son)
                            .HasColumnName("son")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.tonkho)
                            .HasColumnName("tonkho")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.trangthai)
                            .HasColumnName("trangthai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.trongluong)
                            .HasColumnName("trongluong")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.updated)
                            .HasColumnName("updated")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.v2)
                            .HasColumnName("v2")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.vattu)
                            .HasColumnName("vattu")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.w_cm)
                            .HasColumnName("w_cm")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham>()
                            .Property(p => p.w_inch)
                            .HasColumnName("w_inch")
                            .HasColumnType("numeric").HasPrecision(18, 8);
modelBuilder.Entity<md_sanpham_bom>().ToTable("md_sanpham_bom");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.md_sanpham_bom_id)
                            .HasColumnName("md_sanpham_bom_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanpham_bom>().HasKey<string>(p => p.md_sanpham_bom_id);
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.bom_donggoi)
                            .HasColumnName("bom_donggoi")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.bom_tam)
                            .HasColumnName("bom_tam")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.md_phanxuong_id)
                            .HasColumnName("md_phanxuong_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.md_to_id)
                            .HasColumnName("md_to_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.ngay_hieuluc)
                            .HasColumnName("ngay_hieuluc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.phongbanId)
                            .HasColumnName("phongbanId")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.soluong)
                            .HasColumnName("soluong")
                            .HasColumnType("float");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.ten_phienban)
                            .HasColumnName("ten_phienban")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.trangthai)
                            .HasColumnName("trangthai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanpham_bom_vattu>().ToTable("md_sanpham_bom_vattu");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.md_sanpham_bom_vattu_id)
                            .HasColumnName("md_sanpham_bom_vattu_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanpham_bom_vattu>().HasKey<string>(p => p.md_sanpham_bom_vattu_id);
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.lavt)
                            .HasColumnName("lavt")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.md_sanpham_bom_id)
                            .HasColumnName("md_sanpham_bom_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.sanphamId)
                            .HasColumnName("sanphamId")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.soluong)
                            .HasColumnName("soluong")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.tam)
                            .HasColumnName("tam")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_bom_vattu>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanpham_giavon>().ToTable("md_sanpham_giavon");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.md_sanpham_giavon_id)
                            .HasColumnName("md_sanpham_giavon_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanpham_giavon>().HasKey<string>(p => p.md_sanpham_giavon_id);
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.gianhapmoinhat)
                            .HasColumnName("gianhapmoinhat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.giavon)
                            .HasColumnName("giavon")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanpham_giavon>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanphamhinhdang>().ToTable("md_sanphamhinhdang");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.md_sanphamhinhdang_id)
                            .HasColumnName("md_sanphamhinhdang_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanphamhinhdang>().HasKey<string>(p => p.md_sanphamhinhdang_id);
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.ma_hinhdang)
                            .HasColumnName("ma_hinhdang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.ten_hinhdang)
                            .HasColumnName("ten_hinhdang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphamhinhdang>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanphammausac>().ToTable("md_sanphammausac");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.md_sanphammausac_id)
                            .HasColumnName("md_sanphammausac_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanphammausac>().HasKey<string>(p => p.md_sanphammausac_id);
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.ma_mausac)
                            .HasColumnName("ma_mausac")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.ten_mausac)
                            .HasColumnName("ten_mausac")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanphammausac_phatron>().ToTable("md_sanphammausac_phatron");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.md_sanphammausac_phatron_id)
                            .HasColumnName("md_sanphammausac_phatron_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sanphammausac_phatron>().HasKey<string>(p => p.md_sanphammausac_phatron_id);
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.md_sanphammausac_id)
                            .HasColumnName("md_sanphammausac_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.tile)
                            .HasColumnName("tile")
                            .HasColumnType("float");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sanphammausac_phatron>()
                            .Property(p => p.vattu_value)
                            .HasColumnName("vattu_value")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<md_sochungtu>().ToTable("md_sochungtu");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.md_sochungtu_id)
                            .HasColumnName("md_sochungtu_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<md_sochungtu>().HasKey<string>(p => p.md_sochungtu_id);
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.buocnhay)
                            .HasColumnName("buocnhay")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.giatri_namtruoc)
                            .HasColumnName("giatri_namtruoc")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.giatri_thaydoi)
                            .HasColumnName("giatri_thaydoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.khuonmau)
                            .HasColumnName("khuonmau")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.ma_sochungtu)
                            .HasColumnName("ma_sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.mau_hienthi)
                            .HasColumnName("mau_hienthi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.md_trangthai_id)
                            .HasColumnName("md_trangthai_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.namnay)
                            .HasColumnName("namnay")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.namtruoc)
                            .HasColumnName("namtruoc")
                            .HasColumnType("int");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.object_id)
                            .HasColumnName("object_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.ten_sochungtu)
                            .HasColumnName("ten_sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<md_sochungtu>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                    
                    
            #endregion End Code
        }
    }
}
