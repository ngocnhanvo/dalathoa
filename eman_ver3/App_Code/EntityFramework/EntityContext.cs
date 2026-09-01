
            using System;
            using System.Data.Entity;
            using System.Data.Entity.ModelConfiguration.Conventions;
            namespace DataAcess
            {
                public partial class EntityContext : DbContext
                {
                    public EntityContext() : base("edoc2014ConnectionString")
                    {
                    }

                    public virtual DbSet<ad_autoload> ad_autoload { get; set; }
public virtual DbSet<ad_autoload_mmc> ad_autoload_mmc { get; set; }
public virtual DbSet<ad_avariableSQL> ad_avariableSQL { get; set; }
public virtual DbSet<ad_case> ad_case { get; set; }
public virtual DbSet<ad_clearcache> ad_clearcache { get; set; }
public virtual DbSet<ad_column> ad_column { get; set; }
public virtual DbSet<ad_department> ad_department { get; set; }
public virtual DbSet<ad_editstyle> ad_editstyle { get; set; }
public virtual DbSet<ad_formatter> ad_formatter { get; set; }
public virtual DbSet<ad_import> ad_import { get; set; }
public virtual DbSet<ad_import_ava> ad_import_ava { get; set; }
public virtual DbSet<ad_import_column> ad_import_column { get; set; }
public virtual DbSet<ad_import_ex> ad_import_ex { get; set; }
public virtual DbSet<ad_log> ad_log { get; set; }
public virtual DbSet<ad_logmssql> ad_logmssql { get; set; }
public virtual DbSet<ad_menu> ad_menu { get; set; }
public virtual DbSet<ad_mess> ad_mess { get; set; }
public virtual DbSet<ad_module> ad_module { get; set; }
public virtual DbSet<ad_remove> ad_remove { get; set; }
public virtual DbSet<ad_removeline> ad_removeline { get; set; }
public virtual DbSet<ad_role> ad_role { get; set; }
public virtual DbSet<ad_role_mmc> ad_role_mmc { get; set; }
public virtual DbSet<ad_role_mmcol> ad_role_mmcol { get; set; }
public virtual DbSet<ad_role_mmvalue> ad_role_mmvalue { get; set; }
public virtual DbSet<ad_role_where> ad_role_where { get; set; }
public virtual DbSet<ad_selectop_save> ad_selectop_save { get; set; }
public virtual DbSet<ad_selectoption> ad_selectoption { get; set; }
public virtual DbSet<ad_status> ad_status { get; set; }
public virtual DbSet<ad_systemconfig> ad_systemconfig { get; set; }
public virtual DbSet<ad_table_manager> ad_table_manager { get; set; }
public virtual DbSet<ad_user> ad_user { get; set; }
public virtual DbSet<ad_user_mmc> ad_user_mmc { get; set; }
public virtual DbSet<ad_user_role> ad_user_role { get; set; }
public virtual DbSet<c_danhsachdathang> c_danhsachdathang { get; set; }
public virtual DbSet<c_danhsachdathang_nangluc> c_danhsachdathang_nangluc { get; set; }
public virtual DbSet<c_danhsachdathang_thongtinnhanhang> c_danhsachdathang_thongtinnhanhang { get; set; }
public virtual DbSet<c_doichieuhangton> c_doichieuhangton { get; set; }
public virtual DbSet<c_doichieuhangton_cddh> c_doichieuhangton_cddh { get; set; }
public virtual DbSet<c_doichieuhangton_dkdh> c_doichieuhangton_dkdh { get; set; }
public virtual DbSet<c_doichieuhangton_dklht> c_doichieuhangton_dklht { get; set; }
public virtual DbSet<c_doichieuhangton_thdh> c_doichieuhangton_thdh { get; set; }
public virtual DbSet<c_doichieuhangton_tk> c_doichieuhangton_tk { get; set; }
public virtual DbSet<c_doichieuhangton_tkkd> c_doichieuhangton_tkkd { get; set; }
public virtual DbSet<c_dongdsdh> c_dongdsdh { get; set; }
public virtual DbSet<c_donmuahang> c_donmuahang { get; set; }
public virtual DbSet<c_donmuahang_cdmh> c_donmuahang_cdmh { get; set; }
public virtual DbSet<c_donmuahang_thue> c_donmuahang_thue { get; set; }
public virtual DbSet<c_hoadonbanhang> c_hoadonbanhang { get; set; }
public virtual DbSet<c_hoadonbanhang_cdmh> c_hoadonbanhang_cdmh { get; set; }
public virtual DbSet<c_kehoachdathang> c_kehoachdathang { get; set; }
public virtual DbSet<c_kehoachdathang_cdhcd> c_kehoachdathang_cdhcd { get; set; }
public virtual DbSet<c_kehoachdathang_dhcpx> c_kehoachdathang_dhcpx { get; set; }
public virtual DbSet<c_kehoachdathang_dhcpx_cdh> c_kehoachdathang_dhcpx_cdh { get; set; }
public virtual DbSet<c_kehoachdathang_dhcpx_vattu> c_kehoachdathang_dhcpx_vattu { get; set; }
public virtual DbSet<c_kehoachdathang_dhncc> c_kehoachdathang_dhncc { get; set; }
public virtual DbSet<c_kehoachdathang_dhncc_cdh> c_kehoachdathang_dhncc_cdh { get; set; }
public virtual DbSet<c_kehoachdathangtong> c_kehoachdathangtong { get; set; }
public virtual DbSet<c_kehoachmuavt> c_kehoachmuavt { get; set; }
public virtual DbSet<c_kehoachmuavt_cdh> c_kehoachmuavt_cdh { get; set; }
public virtual DbSet<c_kehoachmuavt_dklht> c_kehoachmuavt_dklht { get; set; }
public virtual DbSet<c_nhucauvattu> c_nhucauvattu { get; set; }
public virtual DbSet<c_nhucauvattu_ddhpx> c_nhucauvattu_ddhpx { get; set; }
public virtual DbSet<c_nhucauvattu_dhpx> c_nhucauvattu_dhpx { get; set; }
public virtual DbSet<c_nhucauvattu_ycmvt> c_nhucauvattu_ycmvt { get; set; }
public virtual DbSet<c_phidathang> c_phidathang { get; set; }
public virtual DbSet<c_yeucaumuavt> c_yeucaumuavt { get; set; }
public virtual DbSet<c_yeucaumuavt_cdh> c_yeucaumuavt_cdh { get; set; }
public virtual DbSet<md_banggia> md_banggia { get; set; }
public virtual DbSet<md_bo> md_bo { get; set; }
public virtual DbSet<md_bo_chitiet> md_bo_chitiet { get; set; }
public virtual DbSet<md_cangbien> md_cangbien { get; set; }
public virtual DbSet<md_chungloai> md_chungloai { get; set; }
public virtual DbSet<md_chungloai_ql> md_chungloai_ql { get; set; }
public virtual DbSet<md_dbbiendong> md_dbbiendong { get; set; }
public virtual DbSet<md_dieukienthanhtoan> md_dieukienthanhtoan { get; set; }
public virtual DbSet<md_doikhodoingay> md_doikhodoingay { get; set; }
public virtual DbSet<md_doikhodoingay_cdh> md_doikhodoingay_cdh { get; set; }
public virtual DbSet<md_doitackinhdoanh> md_doitackinhdoanh { get; set; }
public virtual DbSet<md_dondathangphanxuong> md_dondathangphanxuong { get; set; }
public virtual DbSet<md_dondathangphanxuong_cdh> md_dondathangphanxuong_cdh { get; set; }
public virtual DbSet<md_dondathangphanxuong_tinhhinh> md_dondathangphanxuong_tinhhinh { get; set; }
public virtual DbSet<md_dondathangphanxuong_vattu> md_dondathangphanxuong_vattu { get; set; }
public virtual DbSet<md_dongtien> md_dongtien { get; set; }
public virtual DbSet<md_donvitinh> md_donvitinh { get; set; }
public virtual DbSet<md_donvitinhsanpham> md_donvitinhsanpham { get; set; }
public virtual DbSet<md_donvitinhsanpham_cddv> md_donvitinhsanpham_cddv { get; set; }
public virtual DbSet<md_ghichuhdlh> md_ghichuhdlh { get; set; }
public virtual DbSet<md_giasanpham> md_giasanpham { get; set; }
public virtual DbSet<md_giasanpham_giaodich> md_giasanpham_giaodich { get; set; }
public virtual DbSet<md_hanngach> md_hanngach { get; set; }
public virtual DbSet<md_hanngach_chitiet> md_hanngach_chitiet { get; set; }
public virtual DbSet<md_hanngachPXTo> md_hanngachPXTo { get; set; }
public virtual DbSet<md_hanngachPXTo_chitiet> md_hanngachPXTo_chitiet { get; set; }
public virtual DbSet<md_hanngachPXTo_chitiet2> md_hanngachPXTo_chitiet2 { get; set; }
public virtual DbSet<md_hanngachPXTo_chuyenvekhoton> md_hanngachPXTo_chuyenvekhoton { get; set; }
public virtual DbSet<md_hinhthucthanhtoan> md_hinhthucthanhtoan { get; set; }
public virtual DbSet<md_hoadon> md_hoadon { get; set; }
public virtual DbSet<md_hoadon_chitiet> md_hoadon_chitiet { get; set; }
public virtual DbSet<md_kho> md_kho { get; set; }
public virtual DbSet<md_kho_dasudung> md_kho_dasudung { get; set; }
public virtual DbSet<md_kho_ghino> md_kho_ghino { get; set; }
public virtual DbSet<md_kho_giaodich> md_kho_giaodich { get; set; }
public virtual DbSet<md_kho_giucho> md_kho_giucho { get; set; }
public virtual DbSet<md_kho_sanpham> md_kho_sanpham { get; set; }
public virtual DbSet<md_kho_sanpham_hansudung> md_kho_sanpham_hansudung { get; set; }
public virtual DbSet<md_kho_the> md_kho_the { get; set; }
public virtual DbSet<md_khuvuc> md_khuvuc { get; set; }
public virtual DbSet<md_kiemke> md_kiemke { get; set; }
public virtual DbSet<md_kiemke_cdh> md_kiemke_cdh { get; set; }
public virtual DbSet<md_lenhdonggoi> md_lenhdonggoi { get; set; }
public virtual DbSet<md_lenhdonggoi_dh> md_lenhdonggoi_dh { get; set; }
public virtual DbSet<md_lenhdonggoi_vattu> md_lenhdonggoi_vattu { get; set; }
public virtual DbSet<md_lenhsanxuat> md_lenhsanxuat { get; set; }
public virtual DbSet<md_lenhsanxuat_ddsx> md_lenhsanxuat_ddsx { get; set; }
public virtual DbSet<md_lenhsanxuat_tosx> md_lenhsanxuat_tosx { get; set; }
public virtual DbSet<md_lenhsanxuat_tosx_cdh> md_lenhsanxuat_tosx_cdh { get; set; }
public virtual DbSet<md_lenhsanxuat_tosx_dklht> md_lenhsanxuat_tosx_dklht { get; set; }
public virtual DbSet<md_lenhsanxuat_tosx_dklhttp> md_lenhsanxuat_tosx_dklhttp { get; set; }
public virtual DbSet<md_lenhsanxuat_tosx_tonkho> md_lenhsanxuat_tosx_tonkho { get; set; }
public virtual DbSet<md_lenhsanxuat_tosx_vattu> md_lenhsanxuat_tosx_vattu { get; set; }
public virtual DbSet<md_lenhsanxuat_tosx_vattuBackup> md_lenhsanxuat_tosx_vattuBackup { get; set; }
public virtual DbSet<md_lenhsanxuat_vattu> md_lenhsanxuat_vattu { get; set; }
public virtual DbSet<md_lenhsanxuat2> md_lenhsanxuat2 { get; set; }
public virtual DbSet<md_loaicont> md_loaicont { get; set; }
public virtual DbSet<md_loaidtkd> md_loaidtkd { get; set; }
public virtual DbSet<md_loaihoadon> md_loaihoadon { get; set; }
public virtual DbSet<md_modongky> md_modongky { get; set; }
public virtual DbSet<md_namtaichinh> md_namtaichinh { get; set; }
public virtual DbSet<md_namtaichinh_ky> md_namtaichinh_ky { get; set; }
public virtual DbSet<md_nangluc> md_nangluc { get; set; }
public virtual DbSet<md_nhanphoisoi> md_nhanphoisoi { get; set; }
public virtual DbSet<md_nhapkho_ncc> md_nhapkho_ncc { get; set; }
public virtual DbSet<md_nhapkho_ncc_dh> md_nhapkho_ncc_dh { get; set; }
public virtual DbSet<md_nhapkho_px> md_nhapkho_px { get; set; }
public virtual DbSet<md_nhapkho_px_dh> md_nhapkho_px_dh { get; set; }
public virtual DbSet<md_nhapkhonb> md_nhapkhonb { get; set; }
public virtual DbSet<md_nhapkhonb_cdh> md_nhapkhonb_cdh { get; set; }
public virtual DbSet<md_nhapkhoton> md_nhapkhoton { get; set; }
public virtual DbSet<md_nhapkhoton_cdh> md_nhapkhoton_cdh { get; set; }
public virtual DbSet<md_nhomnangluc> md_nhomnangluc { get; set; }
public virtual DbSet<md_phannnhommx> md_phannnhommx { get; set; }
public virtual DbSet<md_phannnhommx_cdpk> md_phannnhommx_cdpk { get; set; }
public virtual DbSet<md_phannnhommx_qhd> md_phannnhommx_qhd { get; set; }
public virtual DbSet<md_phannnhommx_tlkthh> md_phannnhommx_tlkthh { get; set; }
public virtual DbSet<md_phanxuong> md_phanxuong { get; set; }
public virtual DbSet<md_phanxuong_to> md_phanxuong_to { get; set; }
public virtual DbSet<md_phanxuongMain> md_phanxuongMain { get; set; }
public virtual DbSet<md_phienbangia> md_phienbangia { get; set; }
public virtual DbSet<md_quocgia> md_quocgia { get; set; }
public virtual DbSet<md_sanpham> md_sanpham { get; set; }
public virtual DbSet<md_sanpham_bom> md_sanpham_bom { get; set; }
public virtual DbSet<md_sanpham_bom_vattu> md_sanpham_bom_vattu { get; set; }
public virtual DbSet<md_sanpham_giavon> md_sanpham_giavon { get; set; }
public virtual DbSet<md_sanphamhinhdang> md_sanphamhinhdang { get; set; }
public virtual DbSet<md_sanphammausac> md_sanphammausac { get; set; }
public virtual DbSet<md_sanphammausac_phatron> md_sanphammausac_phatron { get; set; }
public virtual DbSet<md_sochungtu> md_sochungtu { get; set; }
public virtual DbSet<md_sochungtudaxoa> md_sochungtudaxoa { get; set; }
public virtual DbSet<md_soquy> md_soquy { get; set; }
public virtual DbSet<md_taptin> md_taptin { get; set; }
public virtual DbSet<md_thongbao> md_thongbao { get; set; }
public virtual DbSet<md_thue_sanpham> md_thue_sanpham { get; set; }
public virtual DbSet<md_tonghopcongno> md_tonghopcongno { get; set; }
public virtual DbSet<md_tonghopkho> md_tonghopkho { get; set; }
public virtual DbSet<md_trangthai> md_trangthai { get; set; }
public virtual DbSet<md_travexacnhan> md_travexacnhan { get; set; }
public virtual DbSet<md_travexacnhan_cdh> md_travexacnhan_cdh { get; set; }
public virtual DbSet<md_tygia> md_tygia { get; set; }
public virtual DbSet<md_vanchuyennoibo> md_vanchuyennoibo { get; set; }
public virtual DbSet<md_vanchuyennoibo_cdvc> md_vanchuyennoibo_cdvc { get; set; }
public virtual DbSet<md_vanchuyennoibo_dalayton> md_vanchuyennoibo_dalayton { get; set; }
public virtual DbSet<md_vanchuyennoibo_rabo> md_vanchuyennoibo_rabo { get; set; }
public virtual DbSet<md_vattukhoan> md_vattukhoan { get; set; }
public virtual DbSet<md_xuatban> md_xuatban { get; set; }
public virtual DbSet<md_xuatban_cdh> md_xuatban_cdh { get; set; }
public virtual DbSet<md_xuatkhonb> md_xuatkhonb { get; set; }
public virtual DbSet<md_xuatkhonb_cdh> md_xuatkhonb_cdh { get; set; }
public virtual DbSet<md_xuatkhonb_sanpham> md_xuatkhonb_sanpham { get; set; }
                    internal object Set()
                    {
                        throw new NotImplementedException();
                    }
                
                
                    protected override void OnModelCreating(DbModelBuilder modelBuilder)
                    {
                        var lg00 = new EntityContext00table();
                        lg00.exec(modelBuilder);
                        var lg01 = new EntityContext01table();
                        lg01.exec(modelBuilder);
                        var lg02 = new EntityContext02table();
                        lg02.exec(modelBuilder);
                        var lg03 = new EntityContext03table();
                        lg03.exec(modelBuilder);
                        var lg04 = new EntityContext04table();
                        lg04.exec(modelBuilder);
                        var lg05 = new EntityContext05table();
                        lg05.exec(modelBuilder);
                        var lg06 = new EntityContext06table();
                        lg06.exec(modelBuilder);
                        var lg07 = new EntityContext07table();
                        lg07.exec(modelBuilder);
                        var lg08 = new EntityContext08table();
                        lg08.exec(modelBuilder);
                        var lg09 = new EntityContext09table();
                        lg09.exec(modelBuilder);
                        var lg10 = new EntityContext10table();
                        lg10.exec(modelBuilder);
                    }
                }
            }
            