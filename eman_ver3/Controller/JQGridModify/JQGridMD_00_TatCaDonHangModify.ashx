<%@ WebHandler Language="C#" Class="JQGridMD_00_DSDHJQGSModify" %>
using System;
using System.Data;
using System.Web;
using System.Linq;
using System.Data.SqlClient;
using DataAcess;
public class JQGridMD_00_DSDHJQGSModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string oper = "vnn";
        if (Security.id_taikhoan(context) != "")
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
        switch (oper)
        {
            case "add":
                this.add(context);
                break;
            case "edit":
                this.edit(context);
                break;
            case "del":
                this.del(context);
                break;
            case "CA01DSAncotradingMD00DSDHJQGS_MD00TatCaDonHang":
                this.CA01DSAncotradingMD00DSDHJQGS_MD00TatCaDonHang(context);
                break;
            case "CA01UpdateDSX_MD00TatCaDonHang":
                this.CA01UpdateDSX_MD00TatCaDonHang(context);
                break;
            case "CA01DCHT_MD00TatCaDonHang":
                this.CA01DCHT_MD00TatCaDonHang(context);
                break;
            case "CA_01_HuyDonHang":
                this.CA_01_HuyDonHang(context);
                break;
            case "CA_01_CapNhatHDLH":
                this.CA_01_CapNhatHDLH(context);
                break;
            default:
                break;
        }
    }
    public void CA_01_CapNhatHDLH(HttpContext context)
    {
        EntityContext db = new EntityContext();
        User_TK us = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context));
        string msg = "", msg_success = "";
        string id = context.Request.Form["id"];
        string check_1 = context.Request.Form["check"];
        string[] vnn = id.Split(',');
        string str1 = VNN_VariablePublic.connectString_Anco(db);
        SqlConnection cnn1 = new SqlConnection(str1);
        
        SqlCommand cmd1 = new SqlCommand(@"select dh.c_danhsachdathang_id,  dh.huongdanlamhangchung, dh.huongdanlamhang  from c_danhsachdathang dh where '" + id + "' like N'%'+ dh.c_danhsachdathang_id + '%'", cnn1);
        DataTable dt1 = new DataTable();
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        da1.Fill(dt1);
        try
        {
            foreach (DataRow row in dt1.Rows)
            {
                foreach (c_danhsachdathang dsdh in db.c_danhsachdathang.Where(s => s.c_danhsachdathang_id == row["c_danhsachdathang_id"].ToString()))
                {
                    //dsdh.huongdanlamhang = row["huongdanlamhang"].ToString();
                    dsdh.huongdanlamhangchung +=  "\n\n" + DateTime.Now.ToString("dd/MM/yyyy hh:mm") + "\n" + row["huongdanlamhangchung"].ToString();
                }
            }
            msg = "<div style='color:blue'>Cập nhật hướng dẩn làm hàng cho đơn hàng thành công.</div>";
            db.SaveChanges();
        }
        catch (Exception ex)
        {

            msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void CA_01_HuyDonHang(HttpContext context)
    {
		EntityContext db = new EntityContext();
        User_TK us = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context));
        string msg = "", msg_success = "";
        string id = context.Request.Form["id"];
		string check_1 = context.Request.Form["check"];
        string[] vnn = id.Split(',');
		try
		{
			foreach (c_danhsachdathang dsdh in db.c_danhsachdathang.Where(s => vnn.Contains(s.c_danhsachdathang_id)))
			{
				if(dsdh.trangthai != "DANHAN" & dsdh.trangthai != "SOANTHAO") 
				{ 	
					if(check_1 == "1")
					{
						dsdh.trangthai = "HUYBO";
						msg_success = "<div style='color:blue'> Hủy bỏ đơn hàng thành công.</div>";
					}
					else if(check_1 == "2")
					{
						dsdh.trangthai = "KETTHUC";
						msg_success = "<div style='color:blue'> Kết thúc đơn hàng thành công.</div>";
					}
					else if(check_1 == "3" & dsdh.anco_check == false)
					{
						dsdh.trangthai = "HIEULUC";
						msg_success = "<div style='color:blue'>Khởi động lại đơn hàng thành công.</div>";
					}
					else if(check_1 == "3"  & dsdh.anco_check == true)
					{
						dsdh.trangthai = "HIEULUC";
						msg_success = "<div style='color:blue'>Khởi động lại đơn hàng thành công.</div>";
					}
					else if(check_1 == "4"  & dsdh.anco_check == false)
					{
						dsdh.trangthai = "HIEULUC";
						msg_success = "<div style='color:blue'>Kích hoạt đơn hàng thành công.</div>";
					}
					else if(check_1 == "4"  & dsdh.anco_check == true)
					{
						dsdh.trangthai = "HIEULUC";
						msg_success = "<div style='color:blue'>Kích hoạt đơn hàng thành công.</div>";
					}	
				}
				else
				{
					msg = "<div style='color:red'>Đơn hàng chưa hiệu lực.</div>";
				}					
			}	
				if (msg.Length <= 0)
				{
					db.SaveChanges();
					msg = msg_success;
				}	
		}
		catch(Exception ex)
		{
			msg = "false#" + ex.Message;
		}
		context.Response.Write(msg);
	}

    public void CA01DCHT_MD00TatCaDonHang(HttpContext context)
    {
        EntityContext db = new EntityContext();
        User_TK us = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context));
        string msg = "", msg_success = "";
        string id = context.Request.Form["id"];
        string[] vnn = id.Split(',');
        foreach (c_danhsachdathang dsdh in db.c_danhsachdathang.Where(s => vnn.Contains(s.c_danhsachdathang_id)).OrderByDescending(s => s.ngaylap))
        {
            if ((dsdh.trangthai == "DAGUI" | dsdh.trangthai == "") & dsdh.anco_check == true)
            {
                msg += "<div style='color:red'>Lỗi:Đơn hàng " + dsdh.sochungtu + " chưa được xác nhận .</div>";
            }
			else if (dsdh.trangthai == "HIEULUC")
			{
				msg += "<div style='color:red'>Lỗi:Đơn hàng " + dsdh.sochungtu + " đã hiệu lực.</div>";
			}
			else if (dsdh.trangthai == "HUYBO")
			{
				msg += "<div style='color:red'>Lỗi:Đơn hàng " + dsdh.sochungtu + " đã hủy.</div>";
			}
			else if (dsdh.trangthai == "KETTHUC")
			{
				msg += "<div style='color:red'>Lỗi:Đơn hàng " + dsdh.sochungtu + " đã kết thúc.</div>";
			}
            else if (dsdh.md_trangthai_id != "HIEULUC")
            {
                msg += "<div style='color:red'>Lỗi:Đơn hàng " + dsdh.sochungtu + " đã được đối chiếu hàng tồn.</div>";
            }
			else {
				string id_dc = Helper.getNewId();
				c_doichieuhangton dc = new c_doichieuhangton
				{
					c_doichieuhangton_id = id_dc,
					c_danhsachdathang_id = dsdh.c_danhsachdathang_id,
					so_donhang = dsdh.sochungtu,
					ten_donhang = "KHDH-" + dsdh.so_po,
					donhang_thamchieu = dsdh.so_po,
					kehoach = " ",
					doichieuht = false,
					phieuhangton = " ",
					ngaykehoach = DateTime.Now,
					ngayhoanthanh = dsdh.ngayhoanthanh,

					nguoitao = us.ad_user_id,
					vaitrotao = us.ad_role_id,
					bophantao = us.md_phongban_id,


					value_nguoitao = us.ma_user,
					value_vaitrotao = us.ten_role,
					value_bophantao = us.ten_phongban,

					nguoicapnhat = us.ad_user_id,
					vaitrocapnhat = us.ad_role_id,
					bophancapnhat = us.md_phongban_id,
					value_nguoicapnhat = us.ma_user,
					value_vaitrocapnhat = us.ten_role,
					value_bophancapnhat = us.ten_phongban,

					ngaytao = DateTime.Now,
					ngaycapnhat = DateTime.Now,
					hoatdong = true
				};
				db.c_doichieuhangton.Add(dc);

				//tao cac dong don hang
				foreach (c_dongdsdh ddsdh in db.c_dongdsdh.Where(s => s.c_danhsachdathang_id == dsdh.c_danhsachdathang_id))
				{
					md_sanpham sp = db.md_sanpham.Where(s => s.md_sanpham_id == ddsdh.md_sanpham_id).FirstOrDefault();
					if (sp == null)
					{
						 msg += "<div style='color:red'>Lỗi:Đơn hàng " + dsdh.sochungtu + " chưa có dữ liệu của hàng hóa.</div>";
					}

					c_doichieuhangton_cddh cddh = new c_doichieuhangton_cddh
					{
						c_doichieuhangton_cddh_id = Helper.getNewId(),
						c_doichieuhangton_id = id_dc,
						md_sanpham_id = ddsdh.md_sanpham_id,
						md_donvitinhsanpham_id = sp.md_donvitinhsanpham_id,
						mota_tiengviet = sp.mota_tiengviet,
						mota_tienganh = sp.mota_tienganh,
						sl_dathang = ddsdh.sl_dathang,
						mota = dsdh.mota,

						nguoitao = us.ad_user_id,
						vaitrotao = us.ad_role_id,
						bophantao = us.md_phongban_id,

						value_nguoitao = us.ma_user,
						value_vaitrotao = us.ten_role,
						value_bophantao = us.ten_phongban,

						nguoicapnhat = us.ad_user_id,
						vaitrocapnhat = us.ad_role_id,
						bophancapnhat = us.md_phongban_id,
						value_nguoicapnhat = us.ma_user,
						value_vaitrocapnhat = us.ten_role,
						value_bophancapnhat = us.ten_phongban,

						ngaytao = DateTime.Now,
						ngaycapnhat = DateTime.Now,
						hoatdong = true
					};
					db.c_doichieuhangton_cddh.Add(cddh);
				}
				if(dsdh.anco_check == true) {
					string str = VNN_VariablePublic.connectString_Anco(db);
					SqlConnection cnn = new SqlConnection(str);
					
					//disable 2 procedure updateGoiHDLH va updateSLDaDatTrenPO
					SqlCommand cmd = new SqlCommand(@"
					ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateGoiHDLH
					ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateSLDaDatTrenPO
					", cnn);
					cmd.Connection.Open();
					cmd.ExecuteNonQuery();
					cmd.Connection.Close();
					//update c_danhsachdathang
					cmd = new SqlCommand(@"
					update c_danhsachdathang set trangthai = 'DAHIEULUC' where c_danhsachdathang_id = '" + dsdh.c_danhsachdathang_id + "'"
					, cnn);
					cmd.Connection.Open();
					cmd.ExecuteNonQuery();
					cmd.Connection.Close();
					//enable 2 procedure updateGoiHDLH va updateSLDaDatTrenPO
					cmd = new SqlCommand(@"
					ALTER TABLE c_danhsachdathang ENABLE TRIGGER updateGoiHDLH
					ALTER TABLE c_danhsachdathang ENABLE TRIGGER updateSLDaDatTrenPO
					", cnn);
					cmd.Connection.Open();
					cmd.ExecuteNonQuery();
					cmd.Connection.Close();
				}
				//dsdh.md_trangthai_id = "DATAO";
				dsdh.trangthai = "HIEULUC";
				msg_success += "<div style='color:blue'>Đơn hàng " + dsdh.sochungtu + " được hiệu lực thành công.</div>";
			}
        }
        if (msg.Length <= 0)
        {
            db.SaveChanges();
            msg = msg_success;
        }
        context.Response.Write(msg);
    }

    public void CA01UpdateDSX_MD00TatCaDonHang(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        string[] vnn = context.Request.Form["id"].Split(',');
        string thoigiansx = context.Request.Form["thoigiansx"];
        string thoigianht = context.Request.Form["thoigianht"];
        foreach (c_danhsachdathang dsdh in db.c_danhsachdathang.Where(s => vnn.Contains(s.c_danhsachdathang_id)))
        {
            if (dsdh.md_trangthai_id == "DOICHIEUHANGTON")
            {
                msg += "<div style='color:red'>Lỗi: Dòng " + dsdh.sochungtu + " đã đối chiếu hàng tồn.</div>";
            }
            else if (dsdh.trangthai != "DANHAN")
            {
                msg += "<div style='color:red'>Lỗi: Dòng " + dsdh.sochungtu + " chưa được xác nhận.</div>";
            }
            else {
                dsdh.thoigiansx = int.Parse(thoigiansx);
                dsdh.ngaybatdau = dsdh.hangiaohang_po - TimeSpan.Parse(thoigiansx) - TimeSpan.Parse(thoigianht);
                dsdh.ngayhoanthanh = dsdh.hangiaohang_po - TimeSpan.Parse(thoigianht);
                db.SaveChanges();
                msg += "<div style='color:blue'>Dòng " + dsdh.sochungtu + " đã cập nhật thời gian sản xuất thành công.</div>";
            }
        }
        context.Response.Write(msg);
    }

    public void CA01DSAncotradingMD00DSDHJQGS_MD00TatCaDonHang(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        string id = context.Request.Form["id"];
        string thoigiansx = context.Request.Form["thoigiansx"];
        string thoigianht = context.Request.Form["thoigianht"];
        string[] vnn = id.Split(',');
        foreach (c_danhsachdathang dsdh1 in db.c_danhsachdathang.Where(s => vnn.Contains(s.c_danhsachdathang_id) & (s.trangthai != "DANHAN")).OrderByDescending(s => s.ngaylap))
        {
            //lay du lieu dong danh sach dat hang
            string str = VNN_VariablePublic.connectString_Anco(db);
            SqlConnection cnn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand(@"select 
			dddsh.c_dongdsdh_id,
			dddsh.c_danhsachdathang_id,
			dddsh.c_dongdonhang_id,
			(select ma_sanpham from md_sanpham where dddsh.md_sanpham_id = md_sanpham_id) as md_sanpham_id,
			dddsh.mota_tiengviet,
			dddsh.mota_tienganh,
			dddsh.ma_sanpham_khach,
			dddsh.md_doitackinhdoanh_id,
			dddsh.huongdan_dathang,
			dddsh.han_giaohang,
			isnull(dddsh.sl_conlai, 0) as sl_dathang,
			0 as sl_dagiao,
			dddsh.sl_conlai,
			dddsh.gianhap,
			dddsh.sothutu,
			dddsh.ngaytao,
			dddsh.nguoitao,
			dddsh.ngaycapnhat,
			dddsh.nguoicapnhat,
			dddsh.mota,
			dddsh.hoatdong,
			dddsh.md_donggoi_id,
			dddsh.sl_inner,
			dddsh.l1,
			dddsh.w1,
			dddsh.h1,
			dddsh.sl_outer,
			dddsh.l2,
			dddsh.w2,
			dddsh.h2,
			dddsh.v2,
			dddsh.sl_cont,
			dddsh.vd,
			dddsh.vn,
			dddsh.vl,
			dddsh.ghichu_vachngan,
			dddsh.sl_huy,
			dvt_i.ten_dvt as dvt_inner,
			dvt_o.ten_dvt as dvt_outer
			from c_dongdsdh dddsh 
			left join c_dongdonhang ddh on dddsh.c_dongdonhang_id = ddh.c_dongdonhang_id
			left join md_donggoi dg on dddsh.md_donggoi_id = dg.md_donggoi_id
			left join md_donvitinh dvt_i on dg.dvt_inner = dvt_i.md_donvitinh_id
			left join md_donvitinh dvt_o on dg.dvt_outer = dvt_o.md_donvitinh_id
			where dddsh.sl_conlai > 0 and dddsh.c_danhsachdathang_id = '" + dsdh1.c_danhsachdathang_id + "'", cnn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            int count_col = 0;
            string sql_column = "(";
            string sql_values = "(";
            foreach (System.Data.DataColumn col in dt.Columns)
            {
                count_col++;
                sql_column += col.ColumnName + ",";
            }

            if (sql_column != "(") { sql_column += "anco_check, sl_hanngach" + ")"; }

            foreach (System.Data.DataRow row in dt.Rows)
            {
                string c_dongdsdh_id = row[0].ToString();
                c_dongdsdh dsdh = db.c_dongdsdh.Where(s => s.c_dongdsdh_id == c_dongdsdh_id).FirstOrDefault();
                if (dsdh == null)
                {
                    string sql_insert = "insert into c_dongdsdh" + sql_column + " values(";
                    for (int i = 0; i < count_col; i++)
                    {
                        string cell_value = row[i].ToString();
                        if (cell_value == null | cell_value == "")
                        {
                            sql_insert += "NULL,";
                        }
                        else
                        {
                            if (i == 3)
                            {
                                sql_insert += "(" + "select md_sanpham_id from md_sanpham where ma_sanpham = N'" + cell_value.Replace("'", "''") + "'),";
                            }
                            else
                            {
                                sql_insert += "N'" + cell_value.Replace("'", "''") + "',";
                            }
                        }
                    }
                    sql_insert += "1,0)";
                    Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(sql_insert);
                }
            }

            //lay du lieu tu Cac khoan phi
            string str1 = VNN_VariablePublic.connectString_Anco(db);
            SqlConnection cnn1 = new SqlConnection(str1);
            SqlCommand cmd1 = new SqlCommand(@"select * from c_phidathang where c_danhsachdathang_id = '" + dsdh1.c_danhsachdathang_id + "'", cnn1);
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);

            int count_col1 = 0;
            string sql_column1 = "(";
            string sql_values1 = "(";
            foreach (System.Data.DataColumn col in dt1.Columns)
            {
                count_col1++;
                sql_column1 += col.ColumnName + ",";
            }

            if (sql_column1 != "(") { sql_column1 += "anco_check" + ")"; }

            foreach (System.Data.DataRow row in dt1.Rows)
            {
                string c_phidathang_id = row[0].ToString();
                c_phidathang dsdh2 = db.c_phidathang.Where(s => s.c_phidathang_id == c_phidathang_id).FirstOrDefault();
                if (dsdh2 == null)
                {
                    string sql_insert = "insert into c_phidathang" + sql_column1 + " values(";
                    for (int i = 0; i < count_col1; i++)
                    {
                        string cell_value = row[i].ToString();
                        if (cell_value == null | cell_value == "")
                        {
                            sql_insert += "NULL,";
                        }
                        else
                        {
                            sql_insert += "N'" + cell_value.Replace("'", "''") + "',";
                        }
                    }
                    sql_insert += "1)";
                    Mbg.Data.SqlClient.SqlHelper.ExcuteNonQuery(sql_insert);
                }
            }

            //disable 2 procedure updateGoiHDLH va updateSLDaDatTrenPO
            cmd = new SqlCommand(@"
            ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateGoiHDLH
            ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateSLDaDatTrenPO
            ", cnn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            //update c_danhsachdathang
            cmd = new SqlCommand(@"
            update c_danhsachdathang set trangthai = 'DANHAN' where c_danhsachdathang_id = '" + dsdh1.c_danhsachdathang_id + "'"
            , cnn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            //enable 2 procedure updateGoiHDLH va updateSLDaDatTrenPO
            cmd = new SqlCommand(@"
            ALTER TABLE c_danhsachdathang ENABLE TRIGGER updateGoiHDLH
            ALTER TABLE c_danhsachdathang ENABLE TRIGGER updateSLDaDatTrenPO
            ", cnn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            dsdh1.trangthai = "DANHAN";
            dsdh1.thoigiansx = int.Parse(thoigiansx);
            dsdh1.ngaybatdau = dsdh1.hangiaohang_po - TimeSpan.Parse(thoigiansx) - TimeSpan.Parse(thoigianht);
            dsdh1.ngayhoanthanh = dsdh1.hangiaohang_po - TimeSpan.Parse(thoigianht);
            db.SaveChanges();
            msg += "\"" + dsdh1.sochungtu + "\" đã nhận\n";
        }
    }

    public void add(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"];
        string md_dtk_id = context.Request.Form["md_doitackinhdoanh_id"];
        string sochungtu = context.Request.Form["sochungtu"];
        DateTime ngaybatdau = VNN_Config.setDateTime(context.Request.Form["ngaybatdau"]);
        DateTime ngaylap = VNN_Config.setDateTime(context.Request.Form["ngaylap"]);
        int thoigiansx = int.Parse(context.Request.Form["thoigiansx"]);
        DateTime hangiaohang_po = VNN_Config.setDateTime(context.Request.Form["hangiaohang_po"]);
        try
        {
            ngaybatdau = hangiaohang_po - TimeSpan.Parse(thoigiansx.ToString());
            md_dtk_id = db.md_doitackinhdoanh.Where(s => s.ma_dtkd == md_dtk_id).Select(s => s.md_doitackinhdoanh_id).FirstOrDefault();
            string id = context.Request.QueryString["id"];
            if (msg.Length <= 0)
            {
                if (ngaybatdau == DateTime.MinValue)
                {
                    VNN_Function.SetFormValue("ngaybatdau", null);
                }
                if (ngaylap == DateTime.MinValue)
                {
                    VNN_Function.SetFormValue("ngaylap", null);
                }
                if (hangiaohang_po == DateTime.MinValue)
                {
                    VNN_Function.SetFormValue("hangiaohang_po", null);
                }
                if (sochungtu == null)
                {
                    sochungtu = VNN_VariablePublic.sochungtu(db, "DSDH", 1);
                }
                if (msg.Length <= 0)
                {
                    string action = "add";
                    string[] column_ex = { "md_trangthai_id" };
                    VNN_Function.SetFormValue("md_doitackinhdoanh_id", md_dtk_id);
                    VNN_Function.SetFormValue("sochungtu", sochungtu);
                    VNN_Function.SetFormValue("ngaybatdau", ngaybatdau.ToString(VNN_Config.get_FormatDate()));
                    string ten_table = "c_danhsachdathang";
                    VNN_Function.Set_DefaultvalueColumn(context, action);
                    VNN_Function.Modify_Function(context, ma_module, id_new, ten_table, action, column_ex, db);
                    VNN_Function.loaddulieu_Auto(db, ma_module);
                    msg = "true#Thêm thành công." + "#" + id_new;
                }
            }
        }
        catch (Exception ex)
        {
            msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string id = context.Request.QueryString["id"];
            string object_ = db.c_danhsachdathang.Where(p => p.c_danhsachdathang_id == id).Select(s => s.c_danhsachdathang_id).Take(1).FirstOrDefault();
            if (object_ == null)
            {
                msg = "false#Lỗi:Không tìm thấy đối tượng cần sửa ";
            }

            if (msg.Length <= 0)
            {
                string action = "edit";
                string[] column_ex = { };
                string ten_table = "c_danhsachdathang";
                VNN_Function.Set_DefaultvalueColumn(context, action);
                VNN_Function.Modify_Function(context, ma_module, null, ten_table, action, column_ex, db);
                VNN_Function.loaddulieu_Auto(db, ma_module);
                msg = "true#Cập nhật thành công.";
            }
        }
        catch (Exception ex)
        {
            msg = "false#" + ex.Message;
        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        EntityContext db = new EntityContext();
        string msg = "",  msg_del = "";
        string ma_module = context.Request.QueryString["ma_module"];
        try
        {
            string ten_table = "c_danhsachdathang";
            int count = context.Request.Form["id"].Split(',').Count();
            string[] id_del = new string[count];
            id_del = context.Request.Form["id"].Split(',');
            for (int i = 0; i < count; i++)
            {
                 msg_del = ""; var id_del_ = id_del[i];
                c_danhsachdathang object_ = db.c_danhsachdathang.Where(p => p.c_danhsachdathang_id == id_del_).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg_del = "Lỗi dòng " + i + ": Không tìm thấy đối tượng cần xóa.";
                    msg += msg_del + "\n";
                }
                else  if (object_.trangthai == "HUYBO")
				{
					msg_del = "<div style='color:red'>Lỗi:Đơn hàng " + object_.sochungtu + " đã hủy.</div>";
					msg += msg_del + "\n";
				}
				else if (object_.trangthai == "KETTHUC")
				{
					msg_del = "<div style='color:red'>Lỗi:Đơn hàng " + object_.sochungtu + " đã kết thúc.</div>";
					msg += msg_del + "\n";
				}
				else if (object_.trangthai == "HIEULUC")
				{
					msg_del = "<div style='color:red'>Lỗi:Đơn hàng " + object_.sochungtu + " đã hiệu lực.</div>";
					msg += msg_del + "\n";
				}
				else if (object_.md_trangthai_id != "HIEULUC")
                {
                    msg_del = "Lỗi dòng: " + i + ":Đã hiệu lực không thể xóa.";
                    msg += msg_del + "\n";
                }
				else {
                    string action = "del";
                    string[] column_ex = { };
                    VNN_Function.SetFormValue("id", object_.c_danhsachdathang_id);
                    VNN_Function.Set_DefaultvalueColumn(context, action);
                    VNN_Function.Modify_Function(context, ma_module, null, ten_table, action, column_ex, db);
					if(object_.anco_check == true) {
						string str = VNN_VariablePublic.connectString_Anco(db);
						SqlConnection cnn = new SqlConnection(str);
						SqlCommand cmd = new SqlCommand(@"select * from c_dongdsdh where c_danhsachdathang_id = '" + object_.c_danhsachdathang_id + "'", cnn);
						DataTable dt = new DataTable();
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						//disable 2 procedure updateGoiHDLH va updateSLDaDatTrenPO
						cmd = new SqlCommand(@"
							ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateGoiHDLH
							ALTER TABLE c_danhsachdathang DISABLE TRIGGER updateSLDaDatTrenPO
							", cnn);
						cmd.Connection.Open();
						cmd.ExecuteNonQuery();
						cmd.Connection.Close();
						//update c_danhsachdathang
						cmd = new SqlCommand(@"
							update c_danhsachdathang set trangthai = 'CHUAGUI' where c_danhsachdathang_id = '" + object_.c_danhsachdathang_id + "'"
						, cnn);
						cmd.Connection.Open();
						cmd.ExecuteNonQuery();
						cmd.Connection.Close();
						//enable 2 procedure updateGoiHDLH va updateSLDaDatTrenPO
						cmd = new SqlCommand(@"
							ALTER TABLE c_danhsachdathang ENABLE TRIGGER updateGoiHDLH
							ALTER TABLE c_danhsachdathang ENABLE TRIGGER updateSLDaDatTrenPO
							", cnn);
						cmd.Connection.Open();
						cmd.ExecuteNonQuery();
						cmd.Connection.Close();
					}
                }
            }
            VNN_Function.loaddulieu_Auto(db, ma_module);
            if (msg.Length <= 0)
            {
                msg = "true#Xóa thành công.";
            }
            else
            {
                msg = "false#" + msg;
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToUpper().Contains("REFERENCE"))
            {
                msg = "false#Lỗi: Đang được sử dụng, không thể xóa";
            }
            else
            {
                msg = "false#Lỗi: " + ex.Message;
            }
        }
        context.Response.Write(msg);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}