using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAcess;

public static class Security 
{
    public static string base64Decode(string data)
    {
        if (data == null) return "";
        try
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();

            byte[] todecode_byte = Convert.FromBase64String(data);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception("Error in base64Decode" + e.Message);
        }
    }

    public static string base64Encode(string data)
    {
        if (data == null) return "";
        try
        {
            byte[] encData_byte = new byte[data.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception e)
        {
            throw new Exception("Error in base64Encode" + e.Message);
        }
    }

    // Hash an input string and return the hash as
    // a 32 character hexadecimal string.
    public static string EncodeMd5Hash(string input)
    {
        // Create a new instance of the MD5CryptoServiceProvider object.
        System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hasher.ComputeHash(System.Text.Encoding.Default.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

    // Verify a hash against a string.
    public static bool verifyMd5Hash(string input, string hash)
    {
        // Hash the input.
        string hashOfInput = EncodeMd5Hash(input);

        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        if (0 == comparer.Compare(hashOfInput, hash))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
	
	public static string UrlPic()
    {
        return "/anco/images/products/fullsize/";
    }
	
    public static string UrlBase()
    {
        return System.Web.Configuration.WebConfigurationManager.AppSettings["appPool"];
    }

    public static Dictionary<string, object> all_taikhoan(HttpContext context)
    {
        try
        {
            string token = context.User.Identity.Name;
            Dictionary<string, object> tokenJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(token);
            string ad_user_id = tokenJson["ad_user_id"].ToString();
            if (ad_user_id != null & ad_user_id != "")
            {
                if (!tokenJson.ContainsKey("chuyenCachInBTSangPDF"))
                    tokenJson["chuyenCachInBTSangPDF"] = false;
                if (!tokenJson.ContainsKey("tuDongNhanDienCachIn"))
                    tokenJson["tuDongNhanDienCachIn"] = false;
                if (!tokenJson.ContainsKey("mauBackground"))
                    tokenJson["mauBackground"] = "mau_01";
                return tokenJson;
            }
            else
            {
                return new Dictionary<string, object>();
            }
        }
        catch
        {
            return new Dictionary<string, object>();
        }
    }

    public static bool googleAuthenticator_taikhoan(HttpContext context)
    {
        if (context.User == null)
            return false;

        string token = context.User.Identity.Name;
        if (!string.IsNullOrWhiteSpace(token))
        {
            var tokenJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(token);
            string googleAuthenticator = tokenJson["googleAuthenticator"].ToString();
            return googleAuthenticator == "1";
        }
        else
        {
            return false;
        }
    }

    public static string id_taikhoan(HttpContext context)
    {
        try
        {
            string token = context.User.Identity.Name;
            Dictionary<string, object> tokenJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(token);
            string ad_user_id = tokenJson["ad_user_id"].ToString();
            if (ad_user_id != null & ad_user_id != "")
            {
                return ad_user_id;
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }
    }

    public static string id_vaitro(HttpContext context)
    {
        try
        {
            string token = context.User.Identity.Name;
            Dictionary<string, object> tokenJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(token);
            string user_role = tokenJson["user_role"].ToString();
            if (!string.IsNullOrEmpty(user_role))
                return user_role;
            else
                return "";
        }
        catch
        {
            return "";
        }
    }

    public static string id_phongban(HttpContext context)
    {
        try
        {
            string token = context.User.Identity.Name;
            Dictionary<string, object> tokenJson = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(token);
            string user_part = tokenJson["user_part"].ToString();
            if (!string.IsNullOrEmpty(user_part))
                return user_part;
            else
                return "";
        }
        catch
        {
            return "";
        }
    }
    public static void thongbao(HttpContext context, int maso, string tieude)
    {
        string cauthongbao = "";
        switch (maso)
        {
            //lỗi
            case 101: cauthongbao = String.Format("{0} đã tồn tại!", tieude); break;
            case 102: cauthongbao = String.Format("{0} đang được sử dụng!", tieude); break;
            //thành công
            case 1: cauthongbao = String.Format("{0} Thêm thành công!", tieude); break;
            case 2: cauthongbao = String.Format("{0} Cập nhật thành công!", tieude); break;
            case 3: cauthongbao = String.Format("{0} Xóa thành công!", tieude); break;
            //exit function
            default: break;

        }

        context.Response.Write(cauthongbao);
    }

    public static bool PhanQuyen_VaiTro(HttpContext context, string tenquyen)
    {
        bool quyen_xacnhan = false;
        string ad_user_id = Security.id_taikhoan(context);
        string ad_role_id = Security.id_vaitro(context);
        string sql = string.Format("select [dbo].[PhanQuyen_VaiTro]('{0}', '{1}', '{2}')", ad_user_id, ad_role_id, tenquyen);
        System.Data.DataTable pq_vaitro = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
        if (pq_vaitro.Rows[0][0].Equals(true))
            quyen_xacnhan = true;
        return quyen_xacnhan;
    }

    //public static bool PhanQuyen_VaiTro(string ad_user_id,string ad_role_id, string tenquyen)
    //{
    //    LinqDataContext db = new LinqDataContext();
    //    bool quyen_xacnhan = false;
    //    string sql = string.Format("select [dbo].[PhanQuyen_VaiTro]('{0}', '{1}', '{2}')", ad_user_id, ad_role_id, tenquyen);
    //    System.Data.DataTable pq_vaitro = Mbg.Data.SqlClient.SqlHelper.GetData(sql);
    //    if (pq_vaitro.Rows[0][0].Equals(true))
    //        quyen_xacnhan = true;
    //    return quyen_xacnhan;
    //}

    //Phân quyền dựa trên id của chức năng
    public static bool PhanQuyen_ChucNang(HttpContext context, string ma_module, string ad_case_id)
    {
        bool quyen = false;

        string ad_role_id = id_vaitro(context);
        string ad_user_id = id_taikhoan(context);

        ADmin_JSON json = new ADmin_JSON();
        var caseM = json.ad_caseJSON().Where(s => s.ad_case_id == ad_case_id | s.ma_case == ad_case_id).FirstOrDefault();
        if (caseM != null)
        {
            var user_mmcs = json.ad_user_mmcJSON();
            var cnMMC = user_mmcs.Where(s => s.ad_case_id == caseM.ad_case_id & s.ad_user_id == ad_user_id).FirstOrDefault();
            if (cnMMC == null)
            {
                var modules = json.ad_moduleJSON();

                var role_mmcs = json.ad_role_mmcJSON();

                string ad_module_id = modules.Where(s => s.ma_module.Equals(ma_module)).Select(s => s.ad_module_id).Take(1).FirstOrDefault();
                if (ad_role_id != null & ad_role_id != "")
                {
                    if (ad_module_id != null & ad_module_id != "")
                    {
                        string pq_chucnang = role_mmcs.Where(s => s.ad_role_id.Equals(ad_role_id) & s.ad_module_id.Equals(ad_module_id) & s.ad_case_id.Equals(caseM.ad_case_id)).Select(s => s.ad_role_mmc_id).Take(1).FirstOrDefault();
                        if (pq_chucnang != null & pq_chucnang != "")
                            quyen = true;
                    }
                }
            }
            else
            {
                quyen = string.IsNullOrEmpty(cnMMC.ad_role_id) ? true : (cnMMC.ad_role_id == ad_role_id ? true : false);
            }
        }
        return quyen;
    }

    public static bool PhanQuyen_ChucNang(HttpContext context,
        List<ad_case> cases,
        List<ad_module> modules,
        List<ad_role_mmc> role_mmcs,
        List<ad_user_mmc> user_mmcs,
        string ma_module,
        string ad_case_id)
    {
        bool quyen = false;

        string ad_role_id = id_vaitro(context);
        string ad_user_id = id_taikhoan(context);

        var cnMMC = user_mmcs.Where(s => s.ad_case_id == ad_case_id & s.ad_user_id == ad_user_id).FirstOrDefault();
        if (cnMMC == null)
        {
            string ad_module_id = modules.Where(s => s.ma_module.Equals(ma_module)).Select(s => s.ad_module_id).Take(1).FirstOrDefault();
            if (ad_role_id != null & ad_role_id != "")
            {
                if (ad_module_id != null & ad_module_id != "")
                {
                    string ad_case_id_ = cases.Where(s => s.ma_case.Equals(ad_case_id)).Select(s => s.ad_case_id).Take(1).FirstOrDefault();
                    if (ad_case_id_ == "" | ad_case_id_ == null)
                    {
                        ad_case_id_ = cases.Where(s => s.ad_case_id.Equals(ad_case_id)).Select(s => s.ad_case_id).Take(1).FirstOrDefault();
                    }

                    if (ad_case_id_ != "" & ad_case_id_ != null)
                    {
                        string pq_chucnang = role_mmcs.Where(s => s.ad_role_id.Equals(ad_role_id) & s.ad_module_id.Equals(ad_module_id) & s.ad_case_id.Equals(ad_case_id_)).Select(s => s.ad_role_mmc_id).Take(1).FirstOrDefault();
                        if (pq_chucnang != null & pq_chucnang != "")
                            quyen = true;
                    }
                }
            }
        }
        else
        {
            quyen = string.IsNullOrEmpty(cnMMC.ad_role_id) ? true : (cnMMC.ad_role_id == ad_role_id ? true : false);
        }
        return quyen;
    }

    public static bool PhanQuyen_Module(HttpContext context, string ad_module_id)
    {
        bool ok = false;
        string ad_role_id = id_vaitro(context);
        string ad_user_id = id_taikhoan(context);
        ADmin_JSON json = new ADmin_JSON();
        var role_mmcs = json.ad_role_mmcJSON();
        var user_mmcs = json.ad_user_mmcJSON();

        var cnMMC = user_mmcs.Where(s => s.ad_module_id == ad_module_id & s.ad_user_id == ad_user_id & (string.IsNullOrEmpty(s.ad_role_id) | s.ad_role_id == ad_role_id)).FirstOrDefault();
        if (cnMMC == null)
        {
            string pq_module = role_mmcs.Where(s => s.ad_role_id.Equals(ad_role_id) & s.ad_module_id.Equals(ad_module_id)).Select(p => p.ad_role_mmc_id).Take(1).FirstOrDefault();
            if (pq_module != null & pq_module != "")
            {
                ok = true;
            }
        }
        else
        {
            ok = true;
        }
        return ok;
    }

    public static bool PhanQuyen_Module(
        HttpContext context,
        List<ad_role_mmc> role_mmcs,
        List<ad_user_mmc> user_mmcs,
        string ad_module_id)
    {
        bool ok = false;
        string ad_role_id = id_vaitro(context);
        string ad_user_id = id_taikhoan(context);
        var cnMMC = user_mmcs.Where(s => s.ad_module_id == ad_module_id & s.ad_user_id == ad_user_id & (string.IsNullOrEmpty(s.ad_role_id) | s.ad_role_id == ad_role_id)).FirstOrDefault();
        if (cnMMC == null)
        {
            string pq_module = role_mmcs.Where(s => s.ad_role_id.Equals(ad_role_id) & s.ad_module_id.Equals(ad_module_id)).Select(p => p.ad_role_mmc_id).Take(1).FirstOrDefault();
            if (pq_module != null & pq_module != "")
            {
                ok = true;
            }
        }
        else
        {
            ok = true;
        }
        return ok;
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            var mailAddress = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static bool IsValidParentheses(string input)
    {
        if (input.removeAllSpaceOrTrimText(true).Length <= 0)
            return true;

        int roundBracketCount = 0;
        bool inString = false;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            // 1. Xử lý chuỗi để tránh bắt nhầm ngoặc trong '...'
            if (c == '\'')
            {
                // Xử lý escape nháy đơn '' trong SQL
                if (inString && i + 1 < input.Length && input[i + 1] == '\'')
                {
                    i++;
                    continue;
                }
                inString = !inString;
                continue;
            }

            // Nếu đang trong chuỗi thì không đếm ngoặc
            if (inString) continue;

            // 2. Chỉ đếm ngoặc tròn ngoài chuỗi
            if (c == '(')
            {
                roundBracketCount++;
            }
            else if (c == ')')
            {
                roundBracketCount--;

                // Nếu đóng nhiều hơn mở -> Phá cấu trúc -> Bắt lỗi ngay
                if (roundBracketCount < 0) return false;
            }
        }

        // Trả về true nếu các cặp ngoặc cân bằng và chuỗi nháy đơn đã đóng
        return roundBracketCount == 0 && !inString;
    }

    public static string test_InjectionSQL(string filter, string where_ex, string where_module_select)
    {
        if (!IsValidParentheses(filter))
            return "and 1=2";
        if (!IsValidParentheses(where_ex))
            return "and 1=2";
        if(filter.removeAllSpaceOrTrimText(false).StartsWith("OR"))
            return $"and (((1=2 {filter} {where_ex})))";
        else
            return $"and (((1=1 {filter} {where_ex})))";
    }
} 
