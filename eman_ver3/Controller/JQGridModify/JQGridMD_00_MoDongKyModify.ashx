<%@ WebHandler Language="C#" Class="JQGridMD_00_MoDongKyModify" %>
using System;
using System.Web;
using System.Linq;
using System.Data.Linq;
using DataAcess;
using System.Data;
public class JQGridMD_00_MoDongKyModify : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public EntityContext db = new EntityContext();
    public EntityFunction entityFunc = new EntityFunction();
    User_TK userTK = null;

    public string oper = "vnn";
    public void ProcessRequest(HttpContext context)
    {
        if (Security.id_taikhoan(context) != "")
        {
            oper = context.Request.QueryString["oper"] == null ? context.Request.Form["oper"] : context.Request.QueryString["oper"];
            userTK = VNN_Function.get_user(Security.id_taikhoan(context), Security.id_vaitro(context), Security.id_phongban(context), db);
        }

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
            case "CA_01_MoDongKy":
                this.CA_01_MoDongKy(context);
                break;
            default:
                break;
        }
    }

    public void CA_01_MoDongKy(HttpContext context)
    {
        string msg = "", msg_success = "";
        string id = context.Request.Form["id"];
        var mdk = db.md_modongky.Where(s => s.md_modongky_id == id).FirstOrDefault();
        //using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                if (mdk == null)
                {
                    msg = "<div style='color:red'>Lỗi: Không tìm thấy đối tượng đã chọn.</div>";
                }
                else if (mdk.hieuluc == true)
                {
                    msg = string.Format(@"<div style='color:red'>Lỗi: Đối tượng đã thực hiện ""{0}"".</div>", mdk.ky_hoatdong);
                }
                else
                {
                    string type = mdk.loai_baocao;
                    if (mdk.ky_hoatdong == "Mở kỳ")
                    {
                        var kmo = db.md_namtaichinh_ky.Where(s => s.md_namtaichinh_ky_id == mdk.md_namtaichinh_ky_id).FirstOrDefault();
                        var nbd_kt = kmo.ngaybatdau.Value.AddMinutes(-1);
                        var kdong = db.md_namtaichinh_ky.Where(p => p.ngayketthuc.Value == nbd_kt).FirstOrDefault();
                        if (type == "KHO" | type == "")
                        {
                            moky_Kho(db, userTK, mdk, kmo, kdong);
                        }

                        //if (type == "CNKH" | type == "")
                        //{
                        //    moky_congnoKH(db, us, mdk, kmo, kdong);
                        //}

                        //if (type == "CNNCC" | type == "")
                        //{
                        //    moky_congnoNCC(db, us, mdk, kmo, kdong);
                        //}
                        msg_success = string.Format(@"<div style=""color:blue"">Mở kỳ thành công</div>");
                    }
                    else
                    {
                        var kdong = db.md_namtaichinh_ky.Where(p => p.md_namtaichinh_ky_id == mdk.md_namtaichinh_ky_id).FirstOrDefault();
                        if (type == "KHO" | type == "")
                        {
                            dongky_Kho(db, userTK, mdk, kdong);
                        }

                        //if (type == "CNKH" | type == "")
                        //{
                        //    msg = dongky_CNKH(db, us, mdk, kdong);
                        //}

                        //if (type == "CNNCC" | type == "")
                        //{
                        //    msg = dongky_CNNCC(db, us, mdk, kdong);
                        //}
                        msg_success = string.Format(@"<div style=""color:blue"">Đóng kỳ thành công</div>");
                    }
                    mdk.hieuluc = true;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                msg = string.Format(@"<div style='color:red'>Lỗi: {0}</div>", ex.Message);
            }

            if (msg.Length <= 0)
            {
                //transaction.Commit();
                msg = msg_success;
            }
            else
            {
                //transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }
    //Mo ky
    public void moky_Kho(EntityContext db, User_TK us, md_modongky mdk, md_namtaichinh_ky kmo, md_namtaichinh_ky kdong)
    {
        string md_namtaichinh_id = kdong != null ? kdong.md_namtaichinh_ky_id : "";
        string sql = string.Format(@"
			declare @ntcKy nvarchar(32) = N'{1}'
            declare @ngayMoKy datetime = convert(datetime,N'{0} 23:59',103)
            select
	            A.md_kho_id as md_kho_id,
	            A.md_sanpham_id as md_sanpham_id,
	            A.md_donvitinhsanpham_id as md_donvitinhsanpham_id, 
	            A.ma_sanpham as ma_sanpham,
	            A.sl_cuoiky as sl_dauky,
	            0 as sl_nhaptrongky,
	            0 as sl_xuattrongky,
	            A.sl_cuoiky as sl_cuoiky
	            from (
		            select 
		            a.md_sanpham_id, a.md_kho_id, c.md_donvitinhsanpham_id, c.ma_sanpham,
		            isnull((
			            select top 1 sl_cuoiky as sl_cuoiky 
                        from md_tonghopkho with (NOLOCK)
			            where md_kho_id = a.md_kho_id 
			            and	md_sanpham_id = a.md_sanpham_id
			            and	md_namtaichinh_ky_id = @ntcKy
			            order by ngaytao desc 
		            ), 0) as sl_cuoiky
		            from md_kho_giaodich a with (NOLOCK)
		            inner join md_kho_sanpham b with (NOLOCK) on a.md_sanpham_id = b.md_sanpham_id
		            inner join md_sanpham c with (NOLOCK) on a.md_sanpham_id = c.md_sanpham_id
		            where 
		            a.ngaychuyen <= @ngayMoKy
		            and a.md_kho_id = b.md_kho_id
		            group by a.md_kho_id, a.md_sanpham_id, c.md_donvitinhsanpham_id, c.ma_sanpham
	            )A
		", kmo.ngayketthuc.Value.ToString("dd/MM/yyyy"), md_namtaichinh_id);
        var dt_dtkd = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
        foreach (DataRow row in dt_dtkd.Rows)
        {
            var thk = new md_tonghopkho();
            thk.md_tonghopkho_id = Helper.getNewId();
            thk.md_namtaichinh_ky_id = kmo.md_namtaichinh_ky_id;
            thk.md_namtaichinh_id = kmo.md_namtaichinh_id;
            thk.md_kho_id = row["md_kho_id"] + "";
            thk.md_sanpham_id = row["md_sanpham_id"] + "";
            thk.md_donvitinhsanpham_id = row["md_donvitinhsanpham_id"] + "";
            thk.ma_sanpham = row["ma_sanpham"] + "";
            thk.nam = mdk.nam;
            thk.soky = mdk.ky;
            thk.sl_dauky = row["sl_dauky"] as decimal?;
            thk.sl_nhaptrongky = row["sl_nhaptrongky"] as decimal?;
            thk.sl_xuattrongky = row["sl_xuattrongky"] as decimal?;
            thk.sl_cuoiky = row["sl_cuoiky"] as decimal?;
            thk.nguoitao = us.ad_user_id;
            thk.vaitrotao = us.ad_role_id;
            thk.bophantao = us.md_phongban_id;
            thk.value_nguoitao = us.ma_user;
            thk.value_vaitrotao = us.ten_role;
            thk.value_bophantao = us.ten_phongban;
            thk.nguoicapnhat = us.ad_user_id;
            thk.vaitrocapnhat = us.ad_role_id;
            thk.bophancapnhat = us.md_phongban_id;
            thk.value_nguoicapnhat = us.ma_user;
            thk.value_vaitrocapnhat = us.ten_role;
            thk.value_bophancapnhat = us.ten_phongban;
            thk.ngaytao = DateTime.Now;
            thk.ngaycapnhat = DateTime.Now;
            thk.mota = "";
            thk.hoatdong = true;
            db.md_tonghopkho.Add(thk);
        }
        db.SaveChanges();
    }

    public void moky_congnoKH(EntityContext db, User_TK us, md_modongky mdk, md_namtaichinh_ky kmo, md_namtaichinh_ky kdong)
    {
        string sql =string.Format(@"
            select distinct dtkd.ma_dtkd, dtkd.ten_dtkd
					from md_xuatban xb
					inner join c_danhsachdathang dh on xb.sctdathang = dh.sochungtu
					inner join md_doitackinhdoanh dtkd on dh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
					where xb.ngaychuyen <= convert(datetime,N'{0} 23:59',103) and 
					dtkd.hoatdong = 1 and xb.trangthai = 'HIEULUC' 
					", kmo.ngayketthuc.Value.ToString("dd/MM/yyyy"));

        var dt_dtkd = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
        var dt_tygia = Mbg.Data.SqlClient.SqlHelper.GetData("select [dbo].[GET_TyGia](N'"+ DateTime.Now.ToString("dd/MM/yyyy") +"')");
        decimal tygia = decimal.Parse(dt_tygia.Rows[0][0].ToString());
        foreach (DataRow row in dt_dtkd.Rows)
        {
            decimal nodauky = 0, codauky = 0, notrongky = 0, cotrongky = 0, nocuoiky = 0, cocuoiky = 0;
            decimal nodauky_usd = 0, codauky_usd = 0, notrongky_usd = 0, cotrongky_usd = 0, nocuoiky_usd = 0, cocuoiky_usd = 0;
            if(kdong != null) {
                var thk = db.md_tonghopcongno.Where(s => s.ma_dtkd == row[0].ToString() &
                s.md_namtaichinh_ky_id == kdong.md_namtaichinh_ky_id & s.iskh == true).FirstOrDefault();
                if (thk != null) {
                    //Nếu kỳ trước có khách hàng này
                    nodauky = thk.nocuoiky.Value;
                    codauky = thk.cocuoiky.Value;
                    notrongky = 0;
                    cotrongky = 0;
                    nocuoiky = thk.nocuoiky.Value;
                    cocuoiky = thk.cocuoiky.Value;

                    nodauky_usd = thk.nocuoiky_usd.Value;
                    codauky_usd = thk.cocuoiky_usd.Value;
                    notrongky_usd = 0;
                    cotrongky_usd = 0;
                    nocuoiky_usd = thk.nocuoiky_usd.Value;
                    cocuoiky_usd = thk.cocuoiky_usd.Value;
                }
            }

            var thk_new = new md_tonghopcongno {
                md_tonghopcongno_id = Helper.getNewId(),
                md_namtaichinh_ky_id = kmo.md_namtaichinh_ky_id,
                md_namtaichinh_id = kmo.md_namtaichinh_id,
                ma_dtkd = row[0].ToString(),
                ten_dtkd = row[1].ToString(),
                nam = mdk.nam,
                soky = mdk.ky,
                nodauky = nodauky,
                codauky = codauky,
                notrongky = notrongky,
                cotrongky = cotrongky,
                nocuoiky = nocuoiky,
                cocuoiky = cocuoiky,

                nodauky_usd = nodauky_usd,
                codauky_usd = codauky_usd,
                notrongky_usd = notrongky_usd,
                cotrongky_usd = cotrongky_usd,
                nocuoiky_usd = nocuoiky_usd,
                cocuoiky_usd = cocuoiky_usd,

                iskh = true,
                tygia = tygia,
                ngaytao = DateTime.Now
            };
            db.md_tonghopcongno.Add(thk_new);
        }
        db.SaveChanges();
    }

    public void moky_congnoNCC(EntityContext db, User_TK us, md_modongky mdk, md_namtaichinh_ky kmo, md_namtaichinh_ky kdong)
    {
        string sql = string.Format(@"select distinct dtkd.ma_dtkd, dtkd.ten_dtkd
					from md_nhapkho_ncc nkncc
					inner join c_donmuahang dmh on nkncc.c_donmuahang_id = dmh.c_donmuahang_id
					inner join md_doitackinhdoanh dtkd on dmh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
					where nkncc.ngaychuyen <= convert(datetime,N'{0} 23:59',103) and 
					dtkd.hoatdong = 1 and nkncc.trangthai = 'HIEULUC' 
					", kmo.ngayketthuc.Value.ToString("dd/MM/yyyy"));

        var dt_dtkd = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
        decimal tygia = 0;
        foreach (DataRow row in dt_dtkd.Rows)
        {
            decimal nodauky = 0, codauky = 0, notrongky = 0, cotrongky = 0, nocuoiky = 0, cocuoiky = 0;
            decimal nodauky_usd = 0, codauky_usd = 0, notrongky_usd = 0, cotrongky_usd = 0, nocuoiky_usd = 0, cocuoiky_usd = 0;
            if(kdong != null) {
                string ma_dtkd = row[0].ToString();
                var thk = db.md_tonghopcongno.Where(s => s.ma_dtkd == ma_dtkd &
                s.md_namtaichinh_ky_id == kdong.md_namtaichinh_ky_id & s.isncc == true).FirstOrDefault();
                if (thk != null) {
                    //Nếu kỳ trước có khách hàng này
                    nodauky = thk.nocuoiky.Value;
                    codauky = thk.cocuoiky.Value;
                    notrongky = 0;
                    cotrongky = 0;
                    nocuoiky = thk.nocuoiky.Value;
                    cocuoiky = thk.cocuoiky.Value;

                    nodauky_usd = thk.nocuoiky_usd.Value;
                    codauky_usd = thk.cocuoiky_usd.Value;
                    notrongky_usd = 0;
                    cotrongky_usd = 0;
                    nocuoiky_usd = thk.nocuoiky_usd.Value;
                    cocuoiky_usd = thk.cocuoiky_usd.Value;
                    tygia = thk.tygia.Value;
                }
            }

            var thk_new = new md_tonghopcongno {
                md_tonghopcongno_id = Helper.getNewId(),
                md_namtaichinh_ky_id = kmo.md_namtaichinh_ky_id,
                md_namtaichinh_id = kmo.md_namtaichinh_id,
                ma_dtkd = row[0] + "",
                ten_dtkd = row[1] + "",
                nam = mdk.nam,
                soky = mdk.ky,
                nodauky = nodauky,
                codauky = codauky,
                notrongky = notrongky,
                cotrongky = cotrongky,
                nocuoiky = nocuoiky,
                cocuoiky = cocuoiky,

                nodauky_usd = nodauky_usd,
                codauky_usd = codauky_usd,
                notrongky_usd = notrongky_usd,
                cotrongky_usd = cotrongky_usd,
                nocuoiky_usd = nocuoiky_usd,
                cocuoiky_usd = cocuoiky_usd,

                isncc = true,
                tygia = tygia,
                ngaytao = DateTime.Now
            };
            db.md_tonghopcongno.Add(thk_new);
        }
        db.SaveChanges();
    }
    /*-----------------------------------------------------------------------------------------------------*/
    //Dong ky
    public string dongky_Kho(EntityContext db, User_TK us, md_modongky mdk, md_namtaichinh_ky kdong)
    {
        string msg = "<div style='color:blue'>Đóng kỳ tháng "+ mdk.ky +" năm "+ mdk.nam +" thành công.</div>";
        md_tonghopkho tk = db.md_tonghopkho.Where(s => s.md_namtaichinh_ky_id == mdk.md_namtaichinh_ky_id & s.nam == mdk.nam & s.soky == mdk.ky).FirstOrDefault();
        if(tk == null) {
            msg = "<div style='color:red'>Đóng kỳ thất bại, bạn chưa mở kỳ tháng "+ mdk.ky +" năm "+ mdk.nam +" loại \"Kho\".</div>";
        }

        string sql = string.Format(@"
		select 
		isnull(SUM(A.sln), 0) as sln, isnull(SUM(A.slx),0) as slx,
		A.md_kho_id, A.md_sanpham_id, A.md_donvitinhsanpham_id, A.ma_sanpham
		from (
			select 
			isnull((select SUM(a.soluong_dichchuyen) where kieuchuyen = N'Nhập kho'),0) as sln,
			isnull((select SUM(a.soluong_dichchuyen) where kieuchuyen = N'Xuất kho'),0) as slx,
			a.md_kho_id, a.md_sanpham_id, c.md_donvitinhsanpham_id, c.ma_sanpham
			from md_kho_giaodich a with (NOLOCK)
			inner join md_kho_sanpham b with (NOLOCK) on a.md_sanpham_id = b.md_sanpham_id and a.md_kho_id = b.md_kho_id
			inner join md_sanpham c with (NOLOCK) on a.md_sanpham_id = c.md_sanpham_id
			where 
			a.ngaychuyen >= convert(datetime,N'{0} 00:00',103) and
			a.ngaychuyen <= convert(datetime,N'{1} 23:59',103)
			group by kieuchuyen, a.md_kho_id, a.md_sanpham_id, c.md_donvitinhsanpham_id, c.ma_sanpham
		) A
		group by A.md_kho_id, A.md_sanpham_id, A.md_donvitinhsanpham_id, A.ma_sanpham
		order by A.md_kho_id, A.ma_sanpham", kdong.ngaybatdau.Value.ToString("dd/MM/yyyy"), kdong.ngayketthuc.Value.ToString("dd/MM/yyyy"));

        DataTable dt_slnslx = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
        foreach (DataRow row in dt_slnslx.Rows)
        {
            decimal soluongnhap = 0, soluongxuat = 0;
            soluongnhap = decimal.Parse(row[0].ToString());
            soluongxuat = decimal.Parse(row[1].ToString());
            string md_sanpham_id = row[3].ToString(), md_kho_id = row[2].ToString(), md_donvitinhsanpham_id = row[4].ToString(), ma_sanpham = row[5].ToString();
            var thk = db.md_tonghopkho.Where(s => s.md_kho_id == md_kho_id & s.md_sanpham_id == md_sanpham_id
                & s.md_namtaichinh_ky_id == kdong.md_namtaichinh_ky_id).FirstOrDefault();
            if (thk != null)
            {
                thk.sl_nhaptrongky = soluongnhap;
                thk.sl_xuattrongky = soluongxuat;
                thk.sl_cuoiky = soluongnhap - soluongxuat + thk.sl_dauky;
            }
            else
            {
                var thk_new = new md_tonghopkho();
                thk_new.md_tonghopkho_id = Helper.getNewId();
                thk_new.md_namtaichinh_ky_id = kdong.md_namtaichinh_ky_id;
                thk_new.md_namtaichinh_id = kdong.md_namtaichinh_id;
                thk_new.md_kho_id = md_kho_id;
                thk_new.md_sanpham_id = md_sanpham_id;
                thk_new.md_donvitinhsanpham_id = md_donvitinhsanpham_id;
                thk_new.ma_sanpham = ma_sanpham;
                thk_new.nam = mdk.nam;
                thk_new.soky = mdk.ky;
                //Nếu kỳ trước có sản phẩm này
                thk_new.sl_dauky = 0;
                thk_new.sl_nhaptrongky = soluongnhap;
                thk_new.sl_xuattrongky = soluongxuat;
                thk_new.sl_cuoiky = soluongnhap - soluongxuat;
                //--
                thk_new.nguoitao = us.ad_user_id;
                thk_new.vaitrotao = us.ad_role_id;
                thk_new.bophantao = us.md_phongban_id;
                thk_new.value_nguoitao = us.ma_user;
                thk_new.value_vaitrotao = us.ten_role;
                thk_new.value_bophantao = us.ten_phongban;
                thk_new.nguoicapnhat = us.ad_user_id;
                thk_new.vaitrocapnhat = us.ad_role_id;
                thk_new.bophancapnhat = us.md_phongban_id;
                thk_new.value_nguoicapnhat = us.ma_user;
                thk_new.value_vaitrocapnhat = us.ten_role;
                thk_new.value_bophancapnhat = us.ten_phongban;
                thk_new.ngaytao = DateTime.Now;
                thk_new.ngaycapnhat = DateTime.Now;
                thk_new.mota = "";
                db.md_tonghopkho.Add(thk_new);
            }
        }
        db.SaveChanges();
        return msg;
    }

    public string dongky_CNKH(EntityContext db, User_TK us, md_modongky mdk, md_namtaichinh_ky kdong)
    {
        string msg = "";
        var tk = db.md_tonghopcongno.Where(s => s.md_namtaichinh_ky_id == mdk.md_namtaichinh_ky_id & s.nam == mdk.nam & s.soky == mdk.ky
        & s.iskh == true).FirstOrDefault();
        if(tk == null) {
            msg = "<div style='color:red'>Đóng kỳ thất bại, bạn chưa mở kỳ tháng "+ mdk.ky +" năm "+ mdk.nam +" loại \"Công nợ khách hàng\".</div>";
        }
        string sql = string.Format(@"
		select sum(A.tt) as tt, A.ma_dtkd, A.ten_dtkd
		from (
			select (ddsdh.gianhap * xb_cdh.sl_daxuat) as tt, dtkd.ma_dtkd, dtkd.ten_dtkd
			from md_xuatban_cdh xb_cdh
			inner join md_xuatban xb on xb.md_xuatban_id = xb_cdh.md_xuatban_id
			inner join c_danhsachdathang dsdh on dsdh.c_danhsachdathang_id = xb.c_danhsachdathang_id
			inner join c_dongdsdh ddsdh on xb.c_danhsachdathang_id = ddsdh.c_danhsachdathang_id
			inner join md_doitackinhdoanh dtkd on xb.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
			where xb.trangthai = 'HIEULUC'
			and ddsdh.md_sanpham_id = xb_cdh.md_sanpham_id
			and xb.ngaychuyen >= CONVERT(datetime,N'{0} 00:00',103)
			and xb.ngaychuyen <= CONVERT(datetime,N'{1} 23:59',103)
		) A
		group by A.ma_dtkd, A.ten_dtkd
		", kdong.ngaybatdau.Value.ToString("dd/MM/yyyy"), kdong.ngayketthuc.Value.ToString("dd/MM/yyyy"));

        var dt_slnslx = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
        foreach (DataRow row in dt_slnslx.Rows)
        {
            string ma_dtkd = row[1].ToString();
            decimal notrongky = 0, cotrongky = decimal.Parse(row[0].ToString());
            var thk = db.md_tonghopcongno.Where(s =>
                s.ma_dtkd == ma_dtkd
                & s.iskh == true
                & s.md_namtaichinh_ky_id == kdong.md_namtaichinh_ky_id).FirstOrDefault();

            if(thk != null) {
                thk.notrongky = notrongky;
                thk.cotrongky = cotrongky;
                thk.nocuoiky = notrongky + thk.nodauky;
                thk.cocuoiky = cotrongky + thk.codauky;
            }
            else {
                var thk_new = new md_tonghopcongno();
                thk_new.md_tonghopcongno_id = Helper.getNewId();
                thk_new.md_namtaichinh_ky_id = kdong.md_namtaichinh_ky_id;
                thk_new.md_namtaichinh_id = kdong.md_namtaichinh_id;
                thk_new.ma_dtkd = row[1].ToString();
                thk_new.iskh = false;
                thk_new.nam = mdk.nam;
                thk_new.soky = mdk.ky;
                //Nếu kỳ trước có sản phẩm này
                thk_new.nodauky = 0;
                thk_new.codauky = 0;
                thk_new.notrongky = notrongky;
                thk_new.cotrongky = cotrongky;
                thk_new.nocuoiky = notrongky;
                thk_new.cocuoiky = cotrongky;

                thk_new.nodauky_usd = 0;
                thk_new.codauky_usd = 0;
                thk_new.notrongky_usd = 0;
                thk_new.cotrongky_usd = 0;
                thk_new.nocuoiky_usd = 0;
                thk_new.cocuoiky_usd = 0;
                //--
                thk_new.ngaytao = DateTime.Now;
                db.md_tonghopcongno.Add(thk_new);
            }
        }
        db.SaveChanges();
        return msg;
    }

    public string dongky_CNNCC(EntityContext db, User_TK us, md_modongky mdk, md_namtaichinh_ky kdong)
    {
        string msg = "";
        var tk = db.md_tonghopcongno.Where(s =>
            s.md_namtaichinh_ky_id == mdk.md_namtaichinh_ky_id &
            s.nam == mdk.nam &
            s.soky == mdk.ky &
            s.isncc == true).FirstOrDefault();
        if(tk == null) {
            msg = "<div style='color:red'>Đóng kỳ thất bại, bạn chưa mở kỳ tháng "+ mdk.ky +" năm "+ mdk.nam +" loại \"Công nợ khách hàng\".</div>";
        }
        string sql = string.Format(@"
		select sum(isnull(A.ttusd,0)) as ttusd, sum(isnull(A.ttvnd,0)) as ttvnd, 
		A.tygia, A.ma_dtkd, A.ten_dtkd
		from (
			select 
			(case when bg.md_dongtien_id = '385ec93024915838c98ef66e58b02e9b' then 
			dmh_cdh.dongiamua * nkncc_dh.sl_danhap else 0 end) as ttusd,
			(case when bg.md_dongtien_id = '385ec93024915838c98ef66e58b02e9b' then 
			0 else dmh_cdh.dongiamua * nkncc_dh.sl_danhap end) as ttvnd,
			[dbo].[GET_TyGia](convert(nvarchar,dmh.ngaydonhang, 103)) as tygia, 
			dtkd.ma_dtkd,
			dtkd.ten_dtkd
			from md_nhapkho_ncc_dh nkncc_dh
			inner join md_nhapkho_ncc nkncc on nkncc_dh.md_nhapkho_ncc_id = nkncc.md_nhapkho_ncc_id
			inner join c_donmuahang_cdmh dmh_cdh on nkncc.c_donmuahang_id = dmh_cdh.c_donmuahang_id
			inner join c_donmuahang dmh on dmh.c_donmuahang_id = dmh_cdh.c_donmuahang_id
			inner join md_doitackinhdoanh dtkd on dmh.md_doitackinhdoanh_id = dtkd.md_doitackinhdoanh_id
			inner join md_phienbangia pbg on pbg.md_phienbangia_id = dmh.md_phienbangia_id
			inner join md_banggia bg on bg.md_banggia_id = pbg.md_banggia_id
			where nkncc.trangthai = 'HIEULUC'
			and nkncc_dh.sl_danhap > 0 
			and dmh_cdh.md_sanpham_id = nkncc_dh.md_sanpham_id
			and nkncc.ngaychuyen >= CONVERT(datetime,N'{0} 00:00',103)
			and nkncc.ngaychuyen <= CONVERT(datetime,N'{1} 23:59',103)
		)A
		group by A.ma_dtkd, A.ten_dtkd, A.tygia
		", kdong.ngaybatdau.Value.ToString("dd/MM/yyyy"), kdong.ngayketthuc.Value.ToString("dd/MM/yyyy"));

        var dt_slnslx = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
        foreach (DataRow row in dt_slnslx.Rows)
        {
            string ma_dtkd = row[3].ToString();
            decimal notrongky_usd = decimal.Parse(row[0].ToString()), notrongky = decimal.Parse(row[1].ToString()), tygia = decimal.Parse(row[2].ToString());
            decimal cotrongky = 0, cotrongky_usd = 0;
            var thk = db.md_tonghopcongno.Where(s => s.ma_dtkd == ma_dtkd & s.isncc == true & s.md_namtaichinh_ky_id == kdong.md_namtaichinh_ky_id).FirstOrDefault();

            if(thk != null) {
                thk.notrongky = notrongky;
                thk.cotrongky = cotrongky;
                thk.nocuoiky = notrongky + thk.nodauky;
                thk.cocuoiky = cotrongky + thk.codauky;

                thk.notrongky_usd = notrongky_usd;
                thk.cotrongky_usd = cotrongky_usd;
                thk.nocuoiky_usd = notrongky_usd + thk.nodauky_usd;
                thk.cocuoiky_usd = cotrongky_usd + thk.codauky_usd;
                thk.tygia = tygia;
            }
            else {
                var thk_new = new md_tonghopcongno();
                thk_new.md_tonghopcongno_id = Helper.getNewId();
                thk_new.md_namtaichinh_ky_id = kdong.md_namtaichinh_ky_id;
                thk_new.md_namtaichinh_id = kdong.md_namtaichinh_id;
                thk_new.ma_dtkd = row[3].ToString();
                thk_new.ten_dtkd = row[4].ToString();
                thk_new.isncc = true;
                thk_new.nam = mdk.nam;
                thk_new.soky = mdk.ky;
                //Nếu kỳ trước có sản phẩm này
                thk_new.nodauky = 0;
                thk_new.codauky = 0;
                thk_new.notrongky = notrongky;
                thk_new.cotrongky = cotrongky;
                thk_new.nocuoiky = notrongky;
                thk_new.cocuoiky = cotrongky;

                thk_new.nodauky_usd = 0;
                thk_new.codauky_usd = 0;
                thk_new.notrongky_usd = notrongky_usd;
                thk_new.cotrongky_usd = cotrongky_usd;
                thk_new.nocuoiky_usd = notrongky_usd;
                thk_new.cocuoiky_usd = cotrongky_usd;
                //--
                thk_new.ngaytao = DateTime.Now;
                db.md_tonghopcongno.Add(thk_new);
            }
        }
        db.SaveChanges();
        return msg;
    }
    /*-----------------------------------------------------------------------------------------------------*/
    public void add(HttpContext context)
    {
        string msg = "", id_new = Helper.getNewId();
        string ma_module = context.Request.QueryString["ma_module"], md_namtaichinh_id = context.Request.Form["md_namtaichinh_id"];

        string ky_hoatdong = context.Request.Form["ky_hoatdong"];

        string loai_baocao = context.Request.Form["loai_baocao"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                int soky = int.Parse(context.Request.Form["ky"]);
                var ntc = db.md_namtaichinh.FirstOrDefault(s => s.md_namtaichinh_id == md_namtaichinh_id);
                md_namtaichinh_ky ntc_ky = null;
                string id = context.Request.Form["id"];
                if (ntc == null)
                {
                    msg = "Không tìm thấy năm đã chọn.";
                }
                else
                {
                    ntc_ky = db.md_namtaichinh_ky.Where(s => s.soky == soky & s.md_namtaichinh_id == md_namtaichinh_id).FirstOrDefault();
                    if (ntc_ky == null)
                    {
                        msg = "Không tìm thấy kỳ \"" + soky + "\" trong năm \"" + ntc.giatri + "\".";
                    }
                    else if (loai_baocao == "")
                    {
                        int mdk2 = db.md_modongky.Where(s => s.md_namtaichinh_ky_id == ntc_ky.md_namtaichinh_ky_id &
                        s.ky_hoatdong == ky_hoatdong).Count();
                        if (mdk2 > 0)
                        {
                            msg = "Không thể tạo kỳ tổng của tháng " + soky + " năm " + ntc.giatri + " khi đã có kỳ tương ứng được tạo.";
                        }
                    }
                    else
                    {
                        int mdk2 = db.md_modongky.Where(s => s.md_namtaichinh_ky_id == ntc_ky.md_namtaichinh_ky_id &
                        (s.loai_baocao == loai_baocao) & s.ky_hoatdong == ky_hoatdong).Count();
                        if (mdk2 > 0)
                        {
                            msg = "Không thể " + ky_hoatdong.ToLower() + " của tháng " + soky + " năm " + ntc.giatri + " khi đã có kỳ tương ứng được " + ky_hoatdong.ToLower().Replace(" kỳ", "") + ".";
                        }
                        else if (db.md_modongky.Where(s => s.md_namtaichinh_ky_id == ntc_ky.md_namtaichinh_ky_id & s.loai_baocao == "").Count() > 0)
                        {
                            msg = "Không thể " + ky_hoatdong.ToLower() + " của tháng " + soky + " năm " + ntc.giatri + " khi đã có kỳ tổng được tạo.";
                        }
                    }
                }

                if (msg.Length <= 0)
                {
                    var object_ = new md_modongky();
                    object_.md_modongky_id = id_new;
                    object_.md_namtaichinh_ky_id = ntc_ky.md_namtaichinh_ky_id;
                    object_.nam = ntc.giatri;
                    VNN_Function.SetFormValue(object_.nameof(s => s.ky), soky.ToString());
                    object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, false);
                    object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                    db.md_modongky.Add(object_);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"true#Thêm mới thành công#{0}", id_new);
                transaction.Commit();
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = string.Format(@"false#{0}", msg);
                transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }

    public void edit(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"], md_namtaichinh_id = context.Request.Form["md_namtaichinh_id"];

        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                int soky = int.Parse(context.Request.Form["ky"]);
                var ntc = db.md_namtaichinh.FirstOrDefault(s => s.md_namtaichinh_id == md_namtaichinh_id);
                md_namtaichinh_ky ntc_ky = null;
                string id = context.Request.Form["id"];
                var object_ = db.md_modongky.Where(p => p.md_modongky_id == id).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg = "Lỗi:Không tìm thấy đối tượng cần sửa ";
                }
                else if (object_.hieuluc == true)
                {
                    msg = "Lỗi: Đã hiệu lực, không thể chỉnh sửa ";
                }

                if (ntc == null)
                {
                    msg = "Không tìm thấy năm đã chọn.";
                }
                else
                {
                    ntc_ky = db.md_namtaichinh_ky.FirstOrDefault(s => s.soky == soky & s.md_namtaichinh_id == md_namtaichinh_id);
                    if (ntc_ky == null)
                    {
                        msg = "Không tìm thấy kỳ \"" + soky + "\" trong năm \"" + ntc.giatri + "\".";
                    }
                }

                if (msg.Length <= 0)
                {
                    object_.md_namtaichinh_ky_id = ntc_ky.md_namtaichinh_ky_id;
                    object_.nam = ntc.giatri;
                    VNN_Function.SetFormValue(object_.nameof(s => s.ky), soky.ToString());
                    object_ = entityFunc.updateDataInEntity(object_, object_.GetType(), context);
                    object_ = Helper.setDefaultValueWhenInsertOrUpdate(object_, userTK, true);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg.Length <= 0)
            {
                msg = string.Format(@"true#Cập nhật thành công");
                transaction.Commit();
                VNN_Function.loaddulieu_Auto(db, ma_module);
            }
            else
            {
                msg = string.Format(@"false#{0}", msg);
                transaction.Rollback();
            }
        }
        context.Response.Write(msg);
    }

    public void del(HttpContext context)
    {
        string msg = "";
        string ma_module = context.Request.QueryString["ma_module"];

        try
        {
            var ids = context.Request.Form["id"].Split(',').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            foreach (var id_del_ in ids)
            {
                var object_ = db.md_modongky.Where(p => p.md_modongky_id == id_del_).Take(1).FirstOrDefault();
                if (object_ == null)
                {
                    msg += string.Format(@"<br><b>{0}</b>: Không tìm thấy đối tượng cần xóa.", id_del_);
                }
                else
                {
                    if (object_.hieuluc.GetValueOrDefault(false) == true)
                    {
                        var mdks = db.md_modongky.Where(s => s.ky == object_.ky & s.nam == object_.nam & s.loai_baocao == object_.loai_baocao).ToList();
                        db.md_modongky.RemoveRange(mdks);

                        if(object_.loai_baocao == "")
                        {
                            var thks = db.md_tonghopkho.Where(s => s.md_namtaichinh_ky_id == object_.md_namtaichinh_ky_id).ToList();
                            db.md_tonghopkho.RemoveRange(thks);

                            var thcns = db.md_tonghopcongno.Where(s => s.md_namtaichinh_ky_id == object_.md_namtaichinh_ky_id).ToList();
                            db.md_tonghopcongno.RemoveRange(thcns);
                        }
                        else if (object_.loai_baocao == "KHO")
                        {
                            var thks = db.md_tonghopkho.Where(s => s.md_namtaichinh_ky_id == object_.md_namtaichinh_ky_id).ToList();
                            db.md_tonghopkho.RemoveRange(thks);
                        }
                        else if (object_.loai_baocao == "CNKH")
                        {
                            var thcns = db.md_tonghopcongno.Where(s => s.md_namtaichinh_ky_id == object_.md_namtaichinh_ky_id & s.iskh == true).ToList();
                            db.md_tonghopcongno.RemoveRange(thcns);
                        }
                        else if (object_.loai_baocao == "CNNCC")
                        {
                            var thcns = db.md_tonghopcongno.Where(s => s.md_namtaichinh_ky_id == object_.md_namtaichinh_ky_id & s.isncc == true).ToList();
                            db.md_tonghopcongno.RemoveRange(thcns);
                        }
                    }
                    else 
                        db.md_modongky.Remove(object_);
                }
            }

            db.SaveChanges();
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }

        if (msg.Length <= 0)
        {
            msg = @"true#Xóa thành công.";
            //VNN_Function.loaddulieu_Auto(db, ma_module);
        }
        else
        {
            msg = string.Format(@"false#{0}", msg.Substring(4));
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
