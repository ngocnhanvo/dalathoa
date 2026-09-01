
using System.Data.Entity;
namespace DataAcess
{
    public class EntityContext00table
    {
        public void exec(DbModelBuilder modelBuilder)
        {
            #region Start Code
            modelBuilder.Entity<ad_autoload>().ToTable("ad_autoload");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.ad_autoload_id)
                            .HasColumnName("ad_autoload_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_autoload>().HasKey<string>(p => p.ad_autoload_id);
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.ma_autoload)
                            .HasColumnName("ma_autoload")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.tanso)
                            .HasColumnName("tanso")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.ten_autoload)
                            .HasColumnName("ten_autoload")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_autoload_mmc>().ToTable("ad_autoload_mmc");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.ad_autoload_mmc_id)
                            .HasColumnName("ad_autoload_mmc_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_autoload_mmc>().HasKey<string>(p => p.ad_autoload_mmc_id);
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.ad_autoload_id)
                            .HasColumnName("ad_autoload_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.ma_menu)
                            .HasColumnName("ma_menu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.ma_module)
                            .HasColumnName("ma_module")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_autoload_mmc>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_avariableSQL>().ToTable("ad_avariableSQL");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.ad_avariableSQL_id)
                            .HasColumnName("ad_avariableSQL_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_avariableSQL>().HasKey<string>(p => p.ad_avariableSQL_id);
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.iscode)
                            .HasColumnName("iscode")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.value)
                            .HasColumnName("value")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.value_replace)
                            .HasColumnName("value_replace")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_avariableSQL>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_case>().ToTable("ad_case");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.ad_case_id)
                            .HasColumnName("ad_case_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_case>().HasKey<string>(p => p.ad_case_id);
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.canhgiua)
                            .HasColumnName("canhgiua")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.docaoForm)
                            .HasColumnName("docaoForm")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.dodaiForm)
                            .HasColumnName("dodaiForm")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.hamxuly)
                            .HasColumnName("hamxuly")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.hidden_modify)
                            .HasColumnName("hidden_modify")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.id_parent)
                            .HasColumnName("id_parent")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.isview)
                            .HasColumnName("isview")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.logo)
                            .HasColumnName("logo")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.ma_case)
                            .HasColumnName("ma_case")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.ma_menu)
                            .HasColumnName("ma_menu")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.ma_module)
                            .HasColumnName("ma_module")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.ten_case)
                            .HasColumnName("ten_case")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.thuake)
                            .HasColumnName("thuake")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.tieude)
                            .HasColumnName("tieude")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_case>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_clearcache>().ToTable("ad_clearcache");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.ad_clearcache_id)
                            .HasColumnName("ad_clearcache_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_clearcache>().HasKey<string>(p => p.ad_clearcache_id);
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.url_cache)
                            .HasColumnName("url_cache")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_clearcache>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_column>().ToTable("ad_column");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.ad_column_id)
                            .HasColumnName("ad_column_id")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_column>().HasKey<string>(p => p.ad_column_id);
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.align)
                            .HasColumnName("align")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.colspan)
                            .HasColumnName("colspan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.disable_modify)
                            .HasColumnName("disable_modify")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.editable)
                            .HasColumnName("editable")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.editoptions)
                            .HasColumnName("editoptions")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.editrules)
                            .HasColumnName("editrules")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.edittype)
                            .HasColumnName("edittype")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.@fixed)
                            .HasColumnName("fixed")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.focus)
                            .HasColumnName("focus")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.formatoptions)
                            .HasColumnName("formatoptions")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.formatter)
                            .HasColumnName("formatter")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.formoptions)
                            .HasColumnName("formoptions")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.frozen)
                            .HasColumnName("frozen")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.hidden)
                            .HasColumnName("hidden")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.important)
                            .HasColumnName("important")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.index_cot)
                            .HasColumnName("index_cot")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.key_cot)
                            .HasColumnName("key_cot")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.label)
                            .HasColumnName("label")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.ma_column)
                            .HasColumnName("ma_column")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.ma_edittype)
                            .HasColumnName("ma_edittype")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.ma_menu)
                            .HasColumnName("ma_menu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.ma_module)
                            .HasColumnName("ma_module")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.not_order)
                            .HasColumnName("not_order")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.reset_modify)
                            .HasColumnName("reset_modify")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.searchoptions)
                            .HasColumnName("searchoptions")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.sopt)
                            .HasColumnName("sopt")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.stype)
                            .HasColumnName("stype")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.ten_column)
                            .HasColumnName("ten_column")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.unformat)
                            .HasColumnName("unformat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_column>()
                            .Property(p => p.width)
                            .HasColumnName("width")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_department>().ToTable("ad_department");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.md_phongban_id)
                            .HasColumnName("md_phongban_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_department>().HasKey<string>(p => p.md_phongban_id);
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.dtkdValue)
                            .HasColumnName("dtkdValue")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.ma_phongban)
                            .HasColumnName("ma_phongban")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.ma_phongban2)
                            .HasColumnName("ma_phongban2")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.md_doitackinhdoanh_id)
                            .HasColumnName("md_doitackinhdoanh_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.md_kho_id)
                            .HasColumnName("md_kho_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.phongbanChaId)
                            .HasColumnName("phongbanChaId")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.ten_phongban)
                            .HasColumnName("ten_phongban")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_department>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_editstyle>().ToTable("ad_editstyle");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.ad_editstyle_id)
                            .HasColumnName("ad_editstyle_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_editstyle>().HasKey<string>(p => p.ad_editstyle_id);
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.ma_editstyle)
                            .HasColumnName("ma_editstyle")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.ma_module)
                            .HasColumnName("ma_module")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("ntext");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.ten_editstyle)
                            .HasColumnName("ten_editstyle")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.value_editoption)
                            .HasColumnName("value_editoption")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.value_editstyle)
                            .HasColumnName("value_editstyle")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.value_formatoptions)
                            .HasColumnName("value_formatoptions")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.value_formatter)
                            .HasColumnName("value_formatter")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.value_searchoptions)
                            .HasColumnName("value_searchoptions")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_editstyle>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_formatter>().ToTable("ad_formatter");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.ad_formatter_id)
                            .HasColumnName("ad_formatter_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_formatter>().HasKey<string>(p => p.ad_formatter_id);
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.ma_formatter)
                            .HasColumnName("ma_formatter")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_formatter>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_import>().ToTable("ad_import");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.ad_import_id)
                            .HasColumnName("ad_import_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_import>().HasKey<string>(p => p.ad_import_id);
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.column_del)
                            .HasColumnName("column_del")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.ma_import)
                            .HasColumnName("ma_import")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.mau_import)
                            .HasColumnName("mau_import")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.table_del)
                            .HasColumnName("table_del")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.ten_import)
                            .HasColumnName("ten_import")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.ten_table)
                            .HasColumnName("ten_table")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.value_header)
                            .HasColumnName("value_header")
                            .HasColumnType("int");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.value_row)
                            .HasColumnName("value_row")
                            .HasColumnType("int");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_import_ava>().ToTable("ad_import_ava");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.ad_import_ava_id)
                            .HasColumnName("ad_import_ava_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_import_ava>().HasKey<string>(p => p.ad_import_ava_id);
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.ad_import_id)
                            .HasColumnName("ad_import_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.ava_name)
                            .HasColumnName("ava_name")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.select_sql)
                            .HasColumnName("select_sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ava>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_import_column>().ToTable("ad_import_column");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.ad_import_column_id)
                            .HasColumnName("ad_import_column_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_import_column>().HasKey<string>(p => p.ad_import_column_id);
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.ad_import_id)
                            .HasColumnName("ad_import_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.function_sql)
                            .HasColumnName("function_sql")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.imported)
                            .HasColumnName("imported")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.ma_import_column)
                            .HasColumnName("ma_import_column")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.primary_key)
                            .HasColumnName("primary_key")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.select_sql)
                            .HasColumnName("select_sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.select_sql_cp)
                            .HasColumnName("select_sql_cp")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.ten_import_column)
                            .HasColumnName("ten_import_column")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_column>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_import_ex>().ToTable("ad_import_ex");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.ad_import_ex_id)
                            .HasColumnName("ad_import_ex_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_import_ex>().HasKey<string>(p => p.ad_import_ex_id);
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.ad_import_id)
                            .HasColumnName("ad_import_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.row_ex)
                            .HasColumnName("row_ex")
                            .HasColumnType("int");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.value_replace)
                            .HasColumnName("value_replace")
                            .HasColumnType("int");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_import_ex>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_log>().ToTable("ad_log");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.ad_log_id)
                            .HasColumnName("ad_log_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_log>().HasKey<string>(p => p.ad_log_id);
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.ad_case_id)
                            .HasColumnName("ad_case_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_log>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_logmssql>().ToTable("ad_logmssql");
                            modelBuilder.Entity<ad_logmssql>()
                            .Property(p => p.ad_logmssql_id)
                            .HasColumnName("ad_logmssql_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_logmssql>().HasKey<string>(p => p.ad_logmssql_id);
                            modelBuilder.Entity<ad_logmssql>()
                            .Property(p => p.clientip)
                            .HasColumnName("clientip")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_logmssql>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_logmssql>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_logmssql>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_logmssql>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_logmssql>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_logmssql>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_logmssql>()
                            .Property(p => p.sql)
                            .HasColumnName("sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_logmssql>()
                            .Property(p => p.user)
                            .HasColumnName("user")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_menu>().ToTable("ad_menu");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.ad_menu_id)
                            .HasColumnName("ad_menu_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_menu>().HasKey<string>(p => p.ad_menu_id);
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.capmenu)
                            .HasColumnName("capmenu")
                            .HasColumnType("int");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.loai_menu)
                            .HasColumnName("loai_menu")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.logo)
                            .HasColumnName("logo")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.ma_menu)
                            .HasColumnName("ma_menu")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.ma_menucha)
                            .HasColumnName("ma_menucha")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.ma_module_count)
                            .HasColumnName("ma_module_count")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.taomodule)
                            .HasColumnName("taomodule")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.ten_menu)
                            .HasColumnName("ten_menu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.url)
                            .HasColumnName("url")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_menu>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_mess>().ToTable("ad_mess");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.ad_mess_id)
                            .HasColumnName("ad_mess_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_mess>().HasKey<string>(p => p.ad_mess_id);
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.ad_menu_id)
                            .HasColumnName("ad_menu_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.danhan)
                            .HasColumnName("danhan")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.indexColumn)
                            .HasColumnName("indexColumn")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.ma_menu)
                            .HasColumnName("ma_menu")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.ma_module)
                            .HasColumnName("ma_module")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.nguoinhan)
                            .HasColumnName("nguoinhan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.noidung)
                            .HasColumnName("noidung")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.sochungtu)
                            .HasColumnName("sochungtu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.songaybaotruoc)
                            .HasColumnName("songaybaotruoc")
                            .HasColumnType("int");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.ten_menu)
                            .HasColumnName("ten_menu")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.ten_module)
                            .HasColumnName("ten_module")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.thangThongBao)
                            .HasColumnName("thangThongBao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.tieude)
                            .HasColumnName("tieude")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.value_nguoinhan)
                            .HasColumnName("value_nguoinhan")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_mess>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_module>().ToTable("ad_module");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.ad_module_id)
                            .HasColumnName("ad_module_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_module>().HasKey<string>(p => p.ad_module_id);
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.ad_menu_id)
                            .HasColumnName("ad_menu_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.capmodule)
                            .HasColumnName("capmodule")
                            .HasColumnType("int");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.double_click)
                            .HasColumnName("double_click")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.from_sql)
                            .HasColumnName("from_sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.groupby_sql)
                            .HasColumnName("groupby_sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.header_grid)
                            .HasColumnName("header_grid")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.Join_sql)
                            .HasColumnName("Join_sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.loai_module)
                            .HasColumnName("loai_module")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.loaithuake)
                            .HasColumnName("loaithuake")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.ma_menu)
                            .HasColumnName("ma_menu")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.ma_module)
                            .HasColumnName("ma_module")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.ma_modulecha)
                            .HasColumnName("ma_modulecha")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.mutil_select)
                            .HasColumnName("mutil_select")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.orderby_sql)
                            .HasColumnName("orderby_sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.procedure_sql)
                            .HasColumnName("procedure_sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.row_count)
                            .HasColumnName("row_count")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.select_sql)
                            .HasColumnName("select_sql")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.soluong_mod)
                            .HasColumnName("soluong_mod")
                            .HasColumnType("int");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.ten_module)
                            .HasColumnName("ten_module")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.thuake)
                            .HasColumnName("thuake")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.update_linq)
                            .HasColumnName("update_linq")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.url)
                            .HasColumnName("url")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_module>()
                            .Property(p => p.where_sql)
                            .HasColumnName("where_sql")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_remove>().ToTable("ad_remove");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.ad_remove_id)
                            .HasColumnName("ad_remove_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_remove>().HasKey<string>(p => p.ad_remove_id);
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.ten_key)
                            .HasColumnName("ten_key")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.ten_table)
                            .HasColumnName("ten_table")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_remove>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
modelBuilder.Entity<ad_removeline>().ToTable("ad_removeline");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.ad_removeline_id)
                            .HasColumnName("ad_removeline_id")
                            .HasColumnType("varchar");
modelBuilder.Entity<ad_removeline>().HasKey<string>(p => p.ad_removeline_id);
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.ad_remove_id)
                            .HasColumnName("ad_remove_id")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.bophancapnhat)
                            .HasColumnName("bophancapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.bophantao)
                            .HasColumnName("bophantao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.hoatdong)
                            .HasColumnName("hoatdong")
                            .HasColumnType("bit");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.mota)
                            .HasColumnName("mota")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.ngaycapnhat)
                            .HasColumnName("ngaycapnhat")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.ngaytao)
                            .HasColumnName("ngaytao")
                            .HasColumnType("datetime");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.nguoicapnhat)
                            .HasColumnName("nguoicapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.nguoitao)
                            .HasColumnName("nguoitao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.sapxep)
                            .HasColumnName("sapxep")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.ten_key)
                            .HasColumnName("ten_key")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.ten_table)
                            .HasColumnName("ten_table")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.vaitrocapnhat)
                            .HasColumnName("vaitrocapnhat")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.vaitrotao)
                            .HasColumnName("vaitrotao")
                            .HasColumnType("varchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.value_bophancapnhat)
                            .HasColumnName("value_bophancapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.value_bophantao)
                            .HasColumnName("value_bophantao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.value_nguoicapnhat)
                            .HasColumnName("value_nguoicapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.value_nguoitao)
                            .HasColumnName("value_nguoitao")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.value_vaitrocapnhat)
                            .HasColumnName("value_vaitrocapnhat")
                            .HasColumnType("nvarchar");
                            modelBuilder.Entity<ad_removeline>()
                            .Property(p => p.value_vaitrotao)
                            .HasColumnName("value_vaitrotao")
                            .HasColumnType("nvarchar");
            #endregion End Code
        }
    }
}
