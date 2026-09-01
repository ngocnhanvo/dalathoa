using DataAcess;
using System;
using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// Summary description for Helper
/// </summary>
namespace vnnhan00000
{
    public class TinNhanHeThong
    {
        public string ma_module { get; set; }
        public string sochungtu { get; set; }
        public string noidung { get; set; }
        public string tieude { get; set; }
        public EntityContext db { get; set; }
        public User_TK userTK { get; set; }

        public class ngn
        {
            public string user { get; set; }
            public string roleId { get; set; }
            public string partId { get; set; }
            public DateTime? viewTime { get; set; }
        }

        public class pbn
        {
            public string user { get; set; }
            public string partId { get; set; }
        }

        public TinNhanHeThong()
        {

        }

        public void taoTinNhan()
        {
            var nhanviens = db.ad_user.Where(s => s.hoatdong == true & s.ma_user != userTK.ma_user).ToList();
            var mod_ = db.ad_module.Where(s => s.ma_module == ma_module).FirstOrDefault();
            var colSCT = db.ad_column.Where(s => s.ad_module_id == mod_.ad_module_id & s.ma_column == "sochungtu").FirstOrDefault();

            if (colSCT != null)
            {
                var tinnhan = new ad_mess();
                tinnhan.ad_mess_id = Helper.getNewId();
                tinnhan.tieude = string.IsNullOrWhiteSpace(tieude) ? string.Format(@"Menu ""{0}"" có số chứng từ mới ""{1}"".", mod_.ten_module, sochungtu) : tieude;
                tinnhan.noidung = noidung + string.Format(@"<span class='viewMess' onclick=""viewMess(this, '{0}', '{1}', '{2}', '{3}', '{4}')"">""xem chi tiết""</span>", mod_.ma_menu, mod_.ma_module, sochungtu, colSCT.index_cot, tinnhan.ad_mess_id);
                tinnhan.ad_menu_id = mod_.ad_menu_id;
                tinnhan.ma_menu = mod_.ma_menu;
                tinnhan.ad_module_id = mod_.ad_module_id;
                tinnhan.ma_module = mod_.ma_module;
                tinnhan.ten_module = mod_.ten_module;
                tinnhan.indexColumn = colSCT.index_cot;
                tinnhan.sochungtu = sochungtu;
                tinnhan = Helper.setDefaultValueWhenInsertOrUpdate(tinnhan, userTK, false);

                var lstNGN = new List<ngn>();
                foreach (var nhanvien in nhanviens)
                {
                    var vaitros = db.ad_user_role.Where(s => s.ad_user_id == nhanvien.ad_user_id).ToList();
                    foreach (var vaitro in vaitros)
                    {
                        string where_vaitro = db.ad_role_where.Where(s => s.ad_role_id == vaitro.ad_role_id & s.ad_module_id == mod_.ad_module_id).Select(s => s.where_sql).FirstOrDefault();
                        string select_sql = VNN_Config.Select_sql(mod_.ma_module, db);
                        string where_chucnang = db.ad_role_mmc.Where(s => s.ad_role_id == vaitro.ad_role_id & s.ad_module_id == mod_.ad_module_id).Take(1).FirstOrDefault() == null ? "and 1=2" : "and 1=1";
                        string sql = string.Format(@"
                    sELeCt * fRoM (
                        sELeCt {0}
                        fRoM {1}
                        WHeRe sochungtu = N'{9}' {2} {3} {6} {7} {8} ) P WHeRe RowNum > 0 AND RowNum < 20
                    order by RowNum asc",
                        select_sql, mod_.from_sql, mod_.where_sql, where_chucnang, "", "", "", where_vaitro, "", sochungtu);

                        sql = ADmin_ConvertStringToCode.Avariable2(nhanvien.ad_user_id, vaitro.ad_role_id, vaitro.md_phongban_id, sql, "", "", db).Replace("'", "''");
                        string sqlcount = string.Format("exec [dbo].[{3}] N'{0}',N'{1}',{2}", sql, mod_.ma_module, 0, mod_.procedure_sql);
                        var dt_count = Mbg.Data.SqlClient.SqlHelper.GetData(sqlcount);
                        int count = int.Parse(dt_count.Rows[0][0].ToString());

                        if (count > 0)
                        {
                            var ngn = new ngn();
                            ngn.user = nhanvien.ma_user;
                            ngn.roleId = vaitro.ad_role_id;
                            ngn.partId = vaitro.md_phongban_id;
                            lstNGN.Add(ngn);
                        }
                    }
                }
                tinnhan.nguoinhan = JsonConvert.SerializeObject(lstNGN);
                db.ad_mess.Add(tinnhan);

                var hubExec = new MainHubExcute();
                hubExec.Exec("sendReportToClient", JsonConvert.SerializeObject(tinnhan));
            }
        }
    }
}