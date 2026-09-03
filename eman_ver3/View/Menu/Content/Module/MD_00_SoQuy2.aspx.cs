using System;
using System.Linq;
using System.Web;
using DataAcess;

public partial class Views_Menu_Content_Module_MD_00_SoQuy2 : System.Web.UI.Page
{
    public string nhanviens = "", nhanviensSelect = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        using (var db = new EntityContext())
        {
            var nvs = db.ad_user.Select(s => new { s.ma_user, s.hoten }).OrderBy(s => s.hoten).ToList();
            nhanviens = Newtonsoft.Json.JsonConvert.SerializeObject(nvs);

            // Tạo phần thân của chuỗi: ma_user:hoten nối với nhau bằng dấu ';'
            string items = string.Join(";", nvs.Select(x => $"{x.ma_user}:{x.hoten}"));

            // Ghép tiền tố ':;' ở đầu
            nhanviensSelect = ":;" + items;
        }
    }
}

