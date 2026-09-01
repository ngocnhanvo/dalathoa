using System;
using System.Linq;
/// <summary>
/// Summary description for VNN_Validate
/// </summary>
public class VNN_Validate
{
    public static bool check_number(string value, string type)
    {
        bool kq = false;
        if (type == "int")
        {
            try { int.Parse(value); kq = true; } 
            catch { }
        }
        else if (type == "Int16")
        {
            try { Int16.Parse(value); kq = true; }
            catch { }
        }
        else if (type == "Int32")
        {
            try { Int32.Parse(value); kq = true; }
            catch { }
        }
        else if (type == "Int64")
        {
            try { Int64.Parse(value); kq = true; }
            catch { }
        }
        else if (type == "double")
        {
            try { double.Parse(value); kq = true; }
            catch { }
        }
        else if (type == "Double")
        {
            try { Double.Parse(value); kq = true; }
            catch { }
        }
        else if (type == "float" | type == "Float")
        {
            try { float.Parse(value); kq = true; }
            catch { }
        }
        else if (type == "long" | type == "Long")
        {
            try { long.Parse(value); kq = true; }
            catch { }
        }
        else if (type == "short" | type == "Short")
        {
            try { short.Parse(value); kq = true; }
            catch { }
        }
        else if (type == "decimal")
        {
            try { decimal.Parse(value); kq = true; }
            catch { }
        }
        else if (type == "Decimal")
        {
            try { Decimal.Parse(value); kq = true; }
            catch { }
        }
        return kq;
    }

    public static bool check_bool(string value, string type)
    {
        bool kq = false;
        if (type == "bool")
        {
            try { bool.Parse(value); kq = true; }
            catch { }
        }
        else if (type == "Boolean")
        {
            try { Boolean.Parse(value); kq = true; }
            catch { }
        }
        return kq;
    }

    public static bool check_column_default(string column, string action, string[] column_ex)
    {
        bool kq = false;
        string[] ma_column_df = { "nguoitao", "vaitrotao", "bophantao", "nguoicapnhat", "vaitrocapnhat", "bophancapnhat", "value_nguoitao", "value_vaitrotao", "value_bophantao", "value_nguoicapnhat", "value_vaitrocapnhat", "value_bophancapnhat", "ngaytao","ngaycapnhat", "hoatdong" };

        if (column_ex != null)
        {
            for (int i = 0; i < column_ex.Length; i++)
            {
                if (column == column_ex[i]) { kq = true; }
            }
        }

        if (kq == false)
        {
            for (int i = 0; i < ma_column_df.Length; i++)
            {
                if (action == "edit")
                {
                    if (column == ma_column_df[i] & i > 2 & i != 6) { kq = true; }
                }
                else
                {
                    if (column == ma_column_df[i]) { kq = true; }
                }
            }
        }
        return kq;
    }
	
	public static bool check_column_default_(string column)
    {
        bool kq = false;
        string[] ma_column_df = { "value_nguoitao", "value_vaitrotao", "value_bophantao", "value_nguoicapnhat", "value_vaitrocapnhat", "value_bophancapnhat","ngaytao","ngaycapnhat", "hoatdong" };
		for (int i = 0; i < ma_column_df.Length; i++)
		{
            if (column == ma_column_df[i]) { kq = true; }
        }
		return kq;
	}
	
    public static string check_ValueForm( string column, string action, string value, string DBtypes)
    {
        string kq = "";
        if (value == null) { 
            if(action == "add")
                kq+= "null";
            else
                kq += column; 
        }
        else if (value == "null")
        {
            kq += "null";
        }
        else
        {
            if (DBtypes.Equals("text") | DBtypes.Equals("ntext") |
                DBtypes.Equals("nvarchar") | DBtypes.Equals("varchar") |
                DBtypes.Equals("char") | DBtypes.Equals("nchar"))
            {
                kq += "N'" + value.Replace("'","''") + "'";
            }
            else if (DBtypes.Equals("datetime") | DBtypes.Equals("date") |
                DBtypes.Equals("datetime2") | DBtypes.Equals("smalldatetime") |
                DBtypes.Equals("datetimeoffset") | DBtypes.Equals("time"))
            {
                kq += "'" + VNN_Config.setDateTime(value).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            }
            else if (DBtypes.Equals("binary") | DBtypes.Equals("varbinary"))
            {
                kq += value;
            }
            else if (DBtypes.Equals("bit"))
            {
                kq += "'" + bool.Parse(value) + "'";
            }
            else if (DBtypes.Equals("int"))
            {
				value = convert_number(value, null, null);
                kq += "'" + int.Parse(value) + "'";
            }
            else if (DBtypes.Equals("bigint"))
            {
				value = convert_number(value, null, null);
                kq += "'" + long.Parse(value) + "'";
            }
            else if (DBtypes.Equals("smallint"))
            {
				value = convert_number(value, null, null);
                kq += "'" + short.Parse(value) + "'";
            }
            else if (DBtypes.Equals("decimal") | DBtypes.Equals("numeric") | DBtypes.Equals("money"))
            {
				value = convert_number(value, null, null);
                kq += "'" + decimal.Parse(value) + "'";
            }
            else if (DBtypes.Equals("float") | DBtypes.Equals("double"))
            {
				value = convert_number(value, null,null);
                kq += "'" + double.Parse(value) + "'";
            }
            else if (DBtypes.Equals("real"))
            {
				value = convert_number(value, null,null);
                kq += "'" + float.Parse(value) + "'";
            }
            else if (DBtypes.Equals("tinyint"))
            {
                kq += "'" + byte.Parse(value) + "'";
            }
            else if (DBtypes.Equals("timestamp"))
            {
                kq += "null";
            }
            else if (DBtypes.Equals("uniqueidentifier"))
            {
                kq += "NEWID()";
            }
            else
            {
                kq += "N'" + value.Replace("'","''") + "'";
            }
        }
        return kq;
    }

    public static string[] check_NameColumn_default(string column_name, string index_cot, string mota, string bien_table)
    {
        //0 ten_column, label
        //1 hidden
        //2 editable
        //3 index_cot
        //4 mota
        //5 width
        string[] kq = new string[20];

        if (column_name == "nguoitao")
        {
            kq[0] = "Người tạo HT";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "vaitrotao")
        {
            kq[0] = "Vai trò tạo HT";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "bophantao")
        {
            kq[0] = "Bộ phận tạo HT";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "nguoicapnhat")
        {
            kq[0] = "Người cập nhật HT";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "vaitrocapnhat")
        {
            kq[0] = "Vai trò cập nhật HT";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "bophancapnhat")
        {
            kq[0] = "Bộ phận cập nhật HT";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "95";
            kq[6] = "center";
        }
		else if (column_name == "value_nguoitao")
        {
            kq[0] = "Người tạo";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "value_vaitrotao")
        {
            kq[0] = "Vai trò tạo";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "value_bophantao")
        {
            kq[0] = "Bộ phận tạo";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "value_nguoicapnhat")
        {
            kq[0] = "Người cập nhật";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "value_vaitrocapnhat")
        {
            kq[0] = "Vai trò cập nhật";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "value_bophancapnhat")
        {
            kq[0] = "Bộ phận cập nhật";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "95";
            kq[6] = "center";
        }
        else if (column_name == "ngaytao")
        {
            kq[0] = "Ngày tạo";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "ngaycapnhat")
        {
            kq[0] = "Ngày cập nhật";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "90";
            kq[6] = "center";
        }
        else if (column_name == "mota")
        {
            kq[0] = "Mô tả";
            kq[1] = "false";
            kq[2] = "true";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "150";
            kq[6] = "left";
        }
        else if (column_name == "hoatdong")
        {
            kq[0] = "Hoạt động";
            kq[1] = "true";
            kq[2] = "false";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "75";
            kq[6] = "center";
        }
        else
        {
            kq[0] = column_name;
            kq[1] = "false";
            kq[2] = "true";
            kq[3] = index_cot;
            kq[4] = mota;
            kq[5] = "120";
            kq[6] = "left";
        }
        return kq;
    }

    public static string[] check_FormatColumn(string column_name, string table_name, string ma_module)
    {
        string[] kq = new string[10];
        //0 ma_edittype
        //1 edittype
        //2 formatter
        //3 formatoptions
        //4 editoptions
        //5 searchoptions
        //6 stype
        //7 sopt
        string sql = VNN_Function.ADGetColumn_SQL(table_name, "", "and vnn_col.COLUMN_NAME = '" + column_name + "'");
        System.Data.DataTable dt = Mbg.Data.SqlClient.SqlHelper.GetData(sql, "@start", 0, "@end", 100000000);
        string DBtypes = "nvarchar", maxlength = "-1";
        if(dt.Rows.Count > 0)
        {
            DBtypes = dt.Rows[0]["DATA_TYPE"].ToString();
            maxlength = dt.Rows[0]["CHARACTER_MAXIMUM_LENGTH"].ToString();
        }

        if (DBtypes.Equals("text") | DBtypes.Equals("ntext") |
        DBtypes.Equals("nvarchar") | DBtypes.Equals("varchar") |
        DBtypes.Equals("char") | DBtypes.Equals("nchar"))
        {
            if (maxlength == "-1")
            {
                kq[0] = "textarea";
                kq[1] = "textarea";
                kq[2] = "";
                kq[3] = "";
                kq[4] = "";
                kq[5] = "";
                kq[6] = "";
                kq[7] = "bw";
            }
			else if(maxlength == "33")
			{
				kq[0] = "";
                kq[1] = "";
                kq[2] = "'password'";
                kq[3] = "";
                kq[4] = "dataInit: function (elem) { format_password(elem); }";
                kq[5] = "";
                kq[6] = "";
                kq[7] = "bw";
			}
            else
            {
                kq[0] = "";
                kq[1] = "";
                kq[2] = "";
                kq[3] = "";
                kq[4] = "maxLength:" + maxlength;
                kq[5] = "";
                kq[6] = "";
                kq[7] = "bw";
            }
        }
        else if (DBtypes.Equals("datetime") | DBtypes.Equals("date") |
            DBtypes.Equals("datetime2") | DBtypes.Equals("smalldatetime") | 
            DBtypes.Equals("time"))
        {
            kq[0] = "datetime";
            kq[1] = "";
            kq[2] = "esc_date";
            kq[3] = "srcformat: 'm/d/Y', newformat:format_srcdatetime()";
            kq[4] = "dataInit: function (elem) { format_datetime(elem); }";
            kq[5] = "dataInit: function (elem) { search_datetime(elem); }";
            kq[6] = "";
            kq[7] = "cn";
        }
        else if (DBtypes.Equals("bit"))
        {
            kq[0] = "checkbox";
            kq[1] = "checkbox";
            kq[2] = "'checkbox'";
            kq[3] = "";
            kq[4] = "value: 'True:False', defaultValue: 'False'";
            kq[5] = "value: ':Tất cả;1:Có;0:Không'";
            kq[6] = "select";
            kq[7] = "bw";
        }
        else if (DBtypes.Equals("int"))
        {
            kq[0] = "";
            kq[1] = "";
			kq[2] = "vnn_number";
            kq[3] = "decimalSeparator:vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: vnn_formatnumber()[2], suffix:''";
            kq[4] = "dataInit: function (elem) { format_number(elem,1); }";
            kq[5] = "dataInit: function (elem) { search_number(elem); }";
            kq[6] = "";
            kq[7] = "en";
        }
        else if (DBtypes.Equals("bigint"))
        {
            kq[0] = "";
            kq[1] = "";
			kq[2] = "vnn_number";
            kq[3] = "decimalSeparator:vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: vnn_formatnumber()[2], suffix:''";
            kq[4] = "dataInit: function (elem) { format_number(elem,1); }";
            kq[5] = "dataInit: function (elem) { search_number(elem); }";
            kq[6] = "";
            kq[7] = "en";
        }
        else if (DBtypes.Equals("smallint"))
        {
            kq[0] = "";
            kq[1] = "";
			kq[2] = "vnn_number";
            kq[3] = "decimalSeparator:vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: vnn_formatnumber()[2], suffix:''";
            kq[4] = "dataInit: function (elem) { format_number(elem,1); }";
            kq[5] = "dataInit: function (elem) { search_number(elem); }";
            kq[6] = "";
            kq[7] = "en";
        }
        else if (DBtypes.Equals("decimal") | DBtypes.Equals("numeric") | DBtypes.Equals("money"))
        {
            kq[0] = "";
            kq[1] = "";
			kq[2] = "vnn_number";
            kq[3] = "decimalSeparator:vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: vnn_formatnumber()[2], suffix:''";
            kq[4] = "dataInit: function (elem) { format_number(elem, 0); }";
            kq[5] = "dataInit: function (elem) { search_number(elem); }";
            kq[6] = "";
            kq[7] = "en";
        }
        else if (DBtypes.Equals("float") | DBtypes.Equals("double"))
        {
            kq[0] = "";
            kq[1] = "";
			kq[2] = "vnn_number";
            kq[3] = "decimalSeparator:vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: vnn_formatnumber()[2], suffix:''";
            kq[4] = "dataInit: function (elem) { format_number(elem, 0); }";
            kq[5] = "dataInit: function (elem) { search_number(elem); }";
            kq[6] = "";
            kq[7] = "en";
        }
        else if (DBtypes.Equals("real"))
        {
            kq[0] = "";
            kq[1] = "";
			kq[2] = "vnn_number";
            kq[3] = "decimalSeparator:vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: vnn_formatnumber()[2], suffix:''";
            kq[4] = "dataInit: function (elem) { format_number(elem, 0); }";
            kq[5] = "dataInit: function (elem) { search_number(elem); }";
            kq[6] = "";
            kq[7] = "en";
        }
        else if (DBtypes.Equals("tinyint"))
        {
            kq[0] = "";
            kq[1] = "";
			kq[2] = "vnn_number";
            kq[3] = "decimalSeparator:vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: vnn_formatnumber()[2], suffix:''";
            kq[4] = "dataInit: function (elem) { format_number(elem,1); }";
            kq[5] = "dataInit: function (elem) { search_number(elem); }";
            kq[6] = "";
            kq[7] = "en";
        }
        else
        {
            kq[0] = "";
            kq[1] = "";
            kq[2] = "";
            kq[3] = "";
            kq[4] = "";
            kq[5] = "";
            kq[6] = "";
            kq[7] = "bw";
        }
        return kq;
    }
	
	public static string convert_number(string value,string thous, string dec){
		/*if(thous == null & dec == null){
			string format_so = VNN_VariablePublic.format_so;
			thous = format_so.Substring(1,1);
			if("[~`!#$%^&*+=-\\';,./{}|\":<>?]".Contains(thous)){
				value = value.Replace(thous,"");
				if(format_so.Length > 5){
					dec = format_so.Substring(5,1);
					if("[~`!#$%^&*+=-\\';,.//{}|\":<>?]".Contains(dec))
						value = value.Replace(dec,".");
				}
			}
			else{
				if(format_so.Length > 4){
					dec = format_so.Substring(4,1);
					if("[~`!#$%^&*+=-\\';,./{}|\":<>?]".Contains(dec))
						value.Replace(dec,".");
				}
			} 			
		}
		else {
			value = value.Replace(thous,"").Replace(dec,".");
		}*/
		return value;
	}
}