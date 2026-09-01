
using System.Data.Entity;
namespace DataAcess
{
    public class EntityContext01table
    {
        public void exec(DbModelBuilder modelBuilder)
        {
            #region Start Code
            modelBuilder.Entity<ad_role>().ToTable("ad_role");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.ad_role_id)
                            .HasColumnName("ad_role_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_role>().HasKey<string>(p => p.ad_role_id);
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.ma_role)
                            .HasColumnName("ma_role")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.md_phongban_id)
                            .HasColumnName("md_phongban_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.role_thuake)
                            .HasColumnName("role_thuake")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.ten_role)
                            .HasColumnName("ten_role")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_role_mmc>().ToTable("ad_role_mmc");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.ad_role_mmc_id)
                            .HasColumnName("ad_role_mmc_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_role_mmc>().HasKey<string>(p => p.ad_role_mmc_id);
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.ad_role_id)
                            .HasColumnName("ad_role_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.ad_case_id)
                            .HasColumnName("ad_case_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.ad_menu_id)
                            .HasColumnName("ad_menu_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.chophep)
                            .HasColumnName("chophep")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.ten_case)
                            .HasColumnName("ten_case")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmc>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_role_mmcol>().ToTable("ad_role_mmcol");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.ad_role_mmcol_id)
                            .HasColumnName("ad_role_mmcol_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_role_mmcol>().HasKey<string>(p => p.ad_role_mmcol_id);
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.ad_column_id)
                            .HasColumnName("ad_column_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.ad_menu_id)
                            .HasColumnName("ad_menu_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.ad_role_id)
                            .HasColumnName("ad_role_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.chophep)
                            .HasColumnName("chophep")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.disableEdit)
                            .HasColumnName("disableEdit")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.disableView)
                            .HasColumnName("disableView")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.ten_column)
                            .HasColumnName("ten_column")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmcol>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_role_mmvalue>().ToTable("ad_role_mmvalue");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.ad_role_mmvalue_id)
                            .HasColumnName("ad_role_mmvalue_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_role_mmvalue>().HasKey<string>(p => p.ad_role_mmvalue_id);
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.ad_column_id)
                            .HasColumnName("ad_column_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.ad_menu_id)
                            .HasColumnName("ad_menu_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.ad_role_id)
                            .HasColumnName("ad_role_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.ten_column)
                            .HasColumnName("ten_column")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_mmvalue>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_role_where>().ToTable("ad_role_where");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.ad_role_where_id)
                            .HasColumnName("ad_role_where_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_role_where>().HasKey<string>(p => p.ad_role_where_id);
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.ad_menu_id)
                            .HasColumnName("ad_menu_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.ad_role_id)
                            .HasColumnName("ad_role_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.ma_module)
                            .HasColumnName("ma_module")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_role_where>()
                            .Property(p => p.where_sql)
                            .HasColumnName("where_sql")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_selectop_save>().ToTable("ad_selectop_save");
                            modelBuilder.Entity<ad_selectop_save>()
                            .Property(p => p.ad_selectop_save_id)
                            .HasColumnName("ad_selectop_save_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_selectop_save>().HasKey<string>(p => p.ad_selectop_save_id);
                            modelBuilder.Entity<ad_selectop_save>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_selectop_save>()
                            .Property(p => p.ten_table)
                            .HasColumnName("ten_table")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_selectoption>().ToTable("ad_selectoption");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.ad_selectoption_id)
                            .HasColumnName("ad_selectoption_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_selectoption>().HasKey<string>(p => p.ad_selectoption_id);
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.display_member)
                            .HasColumnName("display_member")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.from_sql)
                            .HasColumnName("from_sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.ma_selectoption)
                            .HasColumnName("ma_selectoption")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.orderby_sql)
                            .HasColumnName("orderby_sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.select_sql)
                            .HasColumnName("select_sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.ten_selectoption)
                            .HasColumnName("ten_selectoption")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.value_selectoption)
                            .HasColumnName("value_selectoption")
                            .HasColumnType("ntext");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_selectoption>()
                            .Property(p => p.where_sql)
                            .HasColumnName("where_sql")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_status>().ToTable("ad_status");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.ad_status_id)
                            .HasColumnName("ad_status_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_status>().HasKey<string>(p => p.ad_status_id);
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.ma_status)
                            .HasColumnName("ma_status")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.ten_status)
                            .HasColumnName("ten_status")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_status>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_systemconfig>().ToTable("ad_systemconfig");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.ad_systemconfig_id)
                            .HasColumnName("ad_systemconfig_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_systemconfig>().HasKey<string>(p => p.ad_systemconfig_id);
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.connectstring_anco)
                            .HasColumnName("connectstring_anco")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.date_import)
                            .HasColumnName("date_import")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.diachi)
                            .HasColumnName("diachi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.domain)
                            .HasColumnName("domain")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.email_hotro)
                            .HasColumnName("email_hotro")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.emailserver)
                            .HasColumnName("emailserver")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.fax)
                            .HasColumnName("fax")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.format_ngay)
                            .HasColumnName("format_ngay")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.format_so)
                            .HasColumnName("format_so")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.logo)
                            .HasColumnName("logo")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.logo_trangchu)
                            .HasColumnName("logo_trangchu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.mausac)
                            .HasColumnName("mausac")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.passemail)
                            .HasColumnName("passemail")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.phone)
                            .HasColumnName("phone")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.port)
                            .HasColumnName("port")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.soluong_grid)
                            .HasColumnName("soluong_grid")
                            .HasColumnType("int");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.soluong_grid_2)
                            .HasColumnName("soluong_grid_2")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.ssl)
                            .HasColumnName("ssl")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.taikhoanemail)
                            .HasColumnName("taikhoanemail")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.ten_canhbao)
                            .HasColumnName("ten_canhbao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.ten_connectstring)
                            .HasColumnName("ten_connectstring")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.ten_db)
                            .HasColumnName("ten_db")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.ten_linq)
                            .HasColumnName("ten_linq")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.tencongty)
                            .HasColumnName("tencongty")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.url_lichtuan)
                            .HasColumnName("url_lichtuan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.url_linq)
                            .HasColumnName("url_linq")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_systemconfig>()
                            .Property(p => p.website)
                            .HasColumnName("website")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_table_manager>().ToTable("ad_table_manager");
                            modelBuilder.Entity<ad_table_manager>()
                            .Property(p => p.ad_table_manager_id)
                            .HasColumnName("ad_table_manager_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_table_manager>().HasKey<string>(p => p.ad_table_manager_id);
                            modelBuilder.Entity<ad_table_manager>()
                            .Property(p => p.data_core)
                            .HasColumnName("data_core")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_table_manager>()
                            .Property(p => p.delete_data)
                            .HasColumnName("delete_data")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_table_manager>()
                            .Property(p => p.system)
                            .HasColumnName("system")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_table_manager>()
                            .Property(p => p.table_name)
                            .HasColumnName("table_name")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_user>().ToTable("ad_user");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.ad_user_id)
                            .HasColumnName("ad_user_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_user>().HasKey<string>(p => p.ad_user_id);
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.ad_role_id)
                            .HasColumnName("ad_role_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.btnDongMenuConTuDong)
                            .HasColumnName("btnDongMenuConTuDong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.btnDongMenuTuDong)
                            .HasColumnName("btnDongMenuTuDong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.chuyenCachInBTSangPDF)
                            .HasColumnName("chuyenCachInBTSangPDF")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.diachi)
                            .HasColumnName("diachi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.duyet_sms)
                            .HasColumnName("duyet_sms")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.email)
                            .HasColumnName("email")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.email_pass)
                            .HasColumnName("email_pass")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.fax)
                            .HasColumnName("fax")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.googleAuthenticator)
                            .HasColumnName("googleAuthenticator")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.googlePass)
                            .HasColumnName("googlePass")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.hoten)
                            .HasColumnName("hoten")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.ma_nhanvien)
                            .HasColumnName("ma_nhanvien")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.ma_user)
                            .HasColumnName("ma_user")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.matkhau)
                            .HasColumnName("matkhau")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.mauBackground)
                            .HasColumnName("mauBackground")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.md_phongban_id)
                            .HasColumnName("md_phongban_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.phone)
                            .HasColumnName("phone")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.tuDongNhanDienCachIn)
                            .HasColumnName("tuDongNhanDienCachIn")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_user_mmc>().ToTable("ad_user_mmc");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.ad_user_mmc_id)
                            .HasColumnName("ad_user_mmc_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_user_mmc>().HasKey<string>(p => p.ad_user_mmc_id);
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.ad_case_id)
                            .HasColumnName("ad_case_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.ad_menu_id)
                            .HasColumnName("ad_menu_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.ad_role_id)
                            .HasColumnName("ad_role_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.ad_user_id)
                            .HasColumnName("ad_user_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.md_phongban_id)
                            .HasColumnName("md_phongban_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_mmc>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_user_role>().ToTable("ad_user_role");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.ad_user_role_id)
                            .HasColumnName("ad_user_role_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_user_role>().HasKey<string>(p => p.ad_user_role_id);
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.ad_role_id)
                            .HasColumnName("ad_role_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.ad_user_id)
                            .HasColumnName("ad_user_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.macdinh)
                            .HasColumnName("macdinh")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.md_phongban_id)
                            .HasColumnName("md_phongban_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_user_role>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_danhsachdathang>().ToTable("c_danhsachdathang");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.c_danhsachdathang_id)
                            .HasColumnName("c_danhsachdathang_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_danhsachdathang>().HasKey<string>(p => p.c_danhsachdathang_id);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.sochungtu)
                            .HasColumnName("sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.anco_check)
                            .HasColumnName("anco_check")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.anhDungDK)
                            .HasColumnName("anhDungDK")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.anhDungTT)
                            .HasColumnName("anhDungTT")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.banHHVT)
                            .HasColumnName("banHHVT")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.c_donhang_id)
                            .HasColumnName("c_donhang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.cbm)
                            .HasColumnName("cbm")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.cbmTN)
                            .HasColumnName("cbmTN")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.check_nangluc)
                            .HasColumnName("check_nangluc")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.chiHaDK)
                            .HasColumnName("chiHaDK")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.chiHaTT)
                            .HasColumnName("chiHaTT")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.chu_tong_tatca)
                            .HasColumnName("chu_tong_tatca")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.chu_tong_tienhang)
                            .HasColumnName("chu_tong_tienhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.chungloai)
                            .HasColumnName("chungloai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.cont20)
                            .HasColumnName("cont20")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.cont40)
                            .HasColumnName("cont40")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.cont40hc)
                            .HasColumnName("cont40hc")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.cont45hc)
                            .HasColumnName("cont45hc")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.contle)
                            .HasColumnName("contle")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.dg_nangluc)
                            .HasColumnName("dg_nangluc")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.diachigiaohang)
                            .HasColumnName("diachigiaohang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.discount)
                            .HasColumnName("discount")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.donhangtron)
                            .HasColumnName("donhangtron")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.formKYC)
                            .HasColumnName("formKYC")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ghichuLXH)
                            .HasColumnName("ghichuLXH")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ghichuLXH2)
                            .HasColumnName("ghichuLXH2")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ghichuLXH3)
                            .HasColumnName("ghichuLXH3")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.giahangngaygiao)
                            .HasColumnName("giahangngaygiao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.giamgia)
                            .HasColumnName("giamgia")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.grandtotal)
                            .HasColumnName("grandtotal")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.hangiaohang_po)
                            .HasColumnName("hangiaohang_po")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.hanHDTBB)
                            .HasColumnName("hanHDTBB")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.hanXNTSDG)
                            .HasColumnName("hanXNTSDG")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.hinhthucthanhtoan)
                            .HasColumnName("hinhthucthanhtoan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.huongdanlamhang)
                            .HasColumnName("huongdanlamhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.huongdanlamhang2)
                            .HasColumnName("huongdanlamhang2")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.huongdanlamhangchung)
                            .HasColumnName("huongdanlamhangchung")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.huongdanlamhangchung2)
                            .HasColumnName("huongdanlamhangchung2")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.isgui_hdlh)
                            .HasColumnName("isgui_hdlh")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.khachhang)
                            .HasColumnName("khachhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.khachkiem)
                            .HasColumnName("khachkiem")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.khoDG)
                            .HasColumnName("khoDG")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.loai)
                            .HasColumnName("loai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.loaiVTDGK)
                            .HasColumnName("loaiVTDGK")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.md_dieukienthanhtoan_id)
                            .HasColumnName("md_dieukienthanhtoan_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.md_doitackinhdoanh_id)
                            .HasColumnName("md_doitackinhdoanh_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.md_trangthai_id)
                            .HasColumnName("md_trangthai_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngay_tigia)
                            .HasColumnName("ngay_tigia")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaybatdau)
                            .HasColumnName("ngaybatdau")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaybdDG)
                            .HasColumnName("ngaybdDG")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaybookCont)
                            .HasColumnName("ngaybookCont")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaycoBaoBi)
                            .HasColumnName("ngaycoBaoBi")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaycoHDDG)
                            .HasColumnName("ngaycoHDDG")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaycoPallet)
                            .HasColumnName("ngaycoPallet")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaycoTem)
                            .HasColumnName("ngaycoTem")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaycoTSDG)
                            .HasColumnName("ngaycoTSDG")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaycoVTK)
                            .HasColumnName("ngaycoVTK")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayDKcoHDTemBB)
                            .HasColumnName("ngayDKcoHDTemBB")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayDKduTho)
                            .HasColumnName("ngayDKduTho")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayDKduTP)
                            .HasColumnName("ngayDKduTP")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayDKKCS)
                            .HasColumnName("ngayDKKCS")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayHDTBBTT)
                            .HasColumnName("ngayHDTBBTT")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayhieuluc)
                            .HasColumnName("ngayhieuluc")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayhoanthanh)
                            .HasColumnName("ngayhoanthanh")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaykhachkiemDK)
                            .HasColumnName("ngaykhachkiemDK")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaykhachkiemTT)
                            .HasColumnName("ngaykhachkiemTT")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayktDG)
                            .HasColumnName("ngayktDG")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaylap)
                            .HasColumnName("ngaylap")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaynhan)
                            .HasColumnName("ngaynhan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayNKTHOcuoicung)
                            .HasColumnName("ngayNKTHOcuoicung")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayNKTPcuoicung)
                            .HasColumnName("ngayNKTPcuoicung")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaytauchayETD)
                            .HasColumnName("ngaytauchayETD")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaythanhtoan)
                            .HasColumnName("ngaythanhtoan")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngaythucteCont)
                            .HasColumnName("ngaythucteCont")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayTTcoHDTemBB)
                            .HasColumnName("ngayTTcoHDTemBB")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayTTDDH)
                            .HasColumnName("ngayTTDDH")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayTTKCS)
                            .HasColumnName("ngayTTKCS")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ngayXNTSDGTT)
                            .HasColumnName("ngayXNTSDGTT")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.nguoi_dathang)
                            .HasColumnName("nguoi_dathang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.nguoi_phutrach)
                            .HasColumnName("nguoi_phutrach")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.nguoinhan)
                            .HasColumnName("nguoinhan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.nhanVienLH)
                            .HasColumnName("nhanVienLH")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.phuthu)
                            .HasColumnName("phuthu")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.reportKCS)
                            .HasColumnName("reportKCS")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.sanxuatton)
                            .HasColumnName("sanxuatton")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.slcont)
                            .HasColumnName("slcont")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.so_po)
                            .HasColumnName("so_po")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.tau1)
                            .HasColumnName("tau1")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.tau2)
                            .HasColumnName("tau2")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.ten_nguoi_dathang)
                            .HasColumnName("ten_nguoi_dathang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.thoigiansx)
                            .HasColumnName("thoigiansx")
                            .HasColumnType("int");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.thongtinnhanhang)
                            .HasColumnName("thongtinnhanhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.thongtinsanpham)
                            .HasColumnName("thongtinsanpham")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.thongtinthanhtoan)
                            .HasColumnName("thongtinthanhtoan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.thongtinxuathoadon)
                            .HasColumnName("thongtinxuathoadon")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.tiendo)
                            .HasColumnName("tiendo")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.tong_tatca)
                            .HasColumnName("tong_tatca")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.tong_tienhang)
                            .HasColumnName("tong_tienhang")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.tongVTDG)
                            .HasColumnName("tongVTDG")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.total)
                            .HasColumnName("total")
                            .HasColumnType("numeric").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.trangthai)
                            .HasColumnName("trangthai")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.trangthaicam)
                            .HasColumnName("trangthaicam")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.trangthaigiaohang)
                            .HasColumnName("trangthaigiaohang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.trangthaiHDLH)
                            .HasColumnName("trangthaiHDLH")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.trangthaihoadon)
                            .HasColumnName("trangthaihoadon")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.trangthaithanhtoan)
                            .HasColumnName("trangthaithanhtoan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_danhsachdathang_nangluc>().ToTable("c_danhsachdathang_nangluc");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.c_danhsachdathang_nangluc_id)
                            .HasColumnName("c_danhsachdathang_nangluc_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_danhsachdathang_nangluc>().HasKey<string>(p => p.c_danhsachdathang_nangluc_id);
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.c_danhsachdathang_id)
                            .HasColumnName("c_danhsachdathang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.CBMdatduoc)
                            .HasColumnName("CBMdatduoc")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.danhgia)
                            .HasColumnName("danhgia")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.dauma)
                            .HasColumnName("dauma")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.thoigiandukien)
                            .HasColumnName("thoigiandukien")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.thoigianyeucau)
                            .HasColumnName("thoigianyeucau")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.tongCBM)
                            .HasColumnName("tongCBM")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_danhsachdathang_nangluc>()
                            .Property(p => p.tuanthu)
                            .HasColumnName("tuanthu")
                            .HasColumnType("int");
modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>().ToTable("c_danhsachdathang_thongtinnhanhang");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.c_danhsachdathang_thongtinnhanhang_id)
                            .HasColumnName("c_danhsachdathang_thongtinnhanhang_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>().HasKey<string>(p => p.c_danhsachdathang_thongtinnhanhang_id);
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.c_danhsachdathang_id)
                            .HasColumnName("c_danhsachdathang_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.diachi)
                            .HasColumnName("diachi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.hoten)
                            .HasColumnName("hoten")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.macdinh)
                            .HasColumnName("macdinh")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.sdt)
                            .HasColumnName("sdt")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_danhsachdathang_thongtinnhanhang>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton>().ToTable("c_doichieuhangton");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.c_doichieuhangton_id)
                            .HasColumnName("c_doichieuhangton_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton>().HasKey<string>(p => p.c_doichieuhangton_id);
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.c_danhsachdathang_id)
                            .HasColumnName("c_danhsachdathang_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.doichieuht)
                            .HasColumnName("doichieuht")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.donhang_thamchieu)
                            .HasColumnName("donhang_thamchieu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.hangiaohangPO)
                            .HasColumnName("hangiaohangPO")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.kehoach)
                            .HasColumnName("kehoach")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.ngayhoanthanh)
                            .HasColumnName("ngayhoanthanh")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.ngaykehoach)
                            .HasColumnName("ngaykehoach")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.phieuhangton)
                            .HasColumnName("phieuhangton")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.phieutonkho)
                            .HasColumnName("phieutonkho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.so_donhang)
                            .HasColumnName("so_donhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.ten_donhang)
                            .HasColumnName("ten_donhang")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton_cddh>().ToTable("c_doichieuhangton_cddh");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.c_doichieuhangton_cddh_id)
                            .HasColumnName("c_doichieuhangton_cddh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton_cddh>().HasKey<string>(p => p.c_doichieuhangton_cddh_id);
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.c_doichieuhangton_id)
                            .HasColumnName("c_doichieuhangton_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.mota_tienganh)
                            .HasColumnName("mota_tienganh")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.mota_tiengviet)
                            .HasColumnName("mota_tiengviet")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.sl_dathang)
                            .HasColumnName("sl_dathang")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.sl_giamhanngach)
                            .HasColumnName("sl_giamhanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_cddh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton_dkdh>().ToTable("c_doichieuhangton_dkdh");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.c_doichieuhangton_dkdh_id)
                            .HasColumnName("c_doichieuhangton_dkdh_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton_dkdh>().HasKey<string>(p => p.c_doichieuhangton_dkdh_id);
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.c_doichieuhangton_id)
                            .HasColumnName("c_doichieuhangton_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.issanpham)
                            .HasColumnName("issanpham")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.macuoi)
                            .HasColumnName("macuoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.macuoi_id)
                            .HasColumnName("macuoi_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.phukien)
                            .HasColumnName("phukien")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.sl_conlai)
                            .HasColumnName("sl_conlai")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.sl_dat)
                            .HasColumnName("sl_dat")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.sl_dathang)
                            .HasColumnName("sl_dathang")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.sl_dathang_cp)
                            .HasColumnName("sl_dathang_cp")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.sl_giamhanngach)
                            .HasColumnName("sl_giamhanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dkdh>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton_dklht>().ToTable("c_doichieuhangton_dklht");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.c_doichieuhangton_dklht_id)
                            .HasColumnName("c_doichieuhangton_dklht_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<c_doichieuhangton_dklht>().HasKey<string>(p => p.c_doichieuhangton_dklht_id);
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.c_doichieuhangton_id)
                            .HasColumnName("c_doichieuhangton_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.issanpham)
                            .HasColumnName("issanpham")
                            .HasColumnType("bit");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.macuoi)
                            .HasColumnName("macuoi")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.macuoi_id)
                            .HasColumnName("macuoi_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.makho)
                            .HasColumnName("makho")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.md_donvitinhsanpham_id)
                            .HasColumnName("md_donvitinhsanpham_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.md_sanpham_id)
                            .HasColumnName("md_sanpham_id")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.mota_tienganh)
                            .HasColumnName("mota_tienganh")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.mota_tiengviet)
                            .HasColumnName("mota_tiengviet")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.sl_conlai)
                            .HasColumnName("sl_conlai")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.sl_dalay)
                            .HasColumnName("sl_dalay")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.sl_giamhanngach)
                            .HasColumnName("sl_giamhanngach")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.sl_lay)
                            .HasColumnName("sl_lay")
                            .HasColumnType("decimal").HasPrecision(18, 8);
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<c_doichieuhangton_dklht>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
            #endregion End Code
        }
    }
}
