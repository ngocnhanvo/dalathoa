<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CacheAction.aspx.cs" Inherits="Controller_PublicFunction_CacheAction" EnableSessionState="ReadOnly"%>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="DataAcess" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Text" %>

<%
    EntityContext db = new EntityContext();
    var us = db.ad_user.Where(s => s.ma_user == "admin").FirstOrDefault();

    var userJSon = new Dictionary<string, object>();
    userJSon["ad_user_id"] = us.ad_user_id;
    userJSon["ma_user"] = us.ma_user;
    userJSon["mauBackground"] = us.mauBackground;
    userJSon["chuyenCachInBTSangPDF"] = us.chuyenCachInBTSangPDF;
    userJSon["tuDongNhanDienCachIn"] = us.tuDongNhanDienCachIn;
    foreach (ad_user_role tk_vtr in db.ad_user_role.Where(s => s.ad_user_id == us.ad_user_id & s.macdinh == true).ToList())
    {
        userJSon["user_role"] = tk_vtr.ad_role_id;
        userJSon["user_part"] = tk_vtr.md_phongban_id;
    }
    string token = Newtonsoft.Json.JsonConvert.SerializeObject(userJSon);
    FormsAuthentication.SetAuthCookie(token, true);

    string viewPath = Security.UrlBase() + "View/Menu/Content/Module/";
    DirectoryInfo d = new DirectoryInfo(Server.MapPath(viewPath));
    FileInfo[] Files = d.GetFiles("*.aspx");
    List<string> names = Files.Select(x => viewPath + x.Name).ToList();
    names.Add("View/Menu/Menu.html");
    names.Add("View/Menu/Content/Content.aspx");
%>
<div align="center"><h1 id="KQ">Vui lòng chờ đến khi trang này tự đóng.</h1></div>
<script type="text/javascript">
    let count = <%=Files.Length%>, dem = 0;
    let doc = document.getElementById('KQ');

    setTimeout(function () {
    <% foreach (FileInfo file in Files) { 
        string action = viewPath + file.Name;
        %>

        var request = new XMLHttpRequest();
        request.open('GET', '<%=action %>', true);
        request.send(null);

        request.onreadystatechange = function () {
            if (this.readyState === XMLHttpRequest.DONE) {
                dem++;
                if (dem >= count)
                    window.close();

                doc.innerHTML = (count - dem) + ' more files left';
            }
        }
    <%}%>
    }, 3000);
</script>