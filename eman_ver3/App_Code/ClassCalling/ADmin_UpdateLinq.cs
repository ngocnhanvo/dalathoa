using System;
using System.Linq;
using System.IO;

/// <summary>
/// Summary description for VNN_UpdateLinq
/// </summary>
public class ADmin_UpdateLinq
{
    public static string UpdateLinq(string ten_db, string ten_linq, string ten_connectstring)
    {
        //get table name
        string sql_table = "USE " + ten_db + " SELECT * FROM INFORMATION_SCHEMA.tables";
        string kq = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Database Name=\""+ ten_db +"\" Class=\""+ ten_linq +"DataContext\" xmlns=\"http://schemas.microsoft.com/linqtosql/dbml/2007\">";
        kq += "<Connection Mode=\"WebSettings\" ConnectionString=\"" + Helper.getConnectStrings(ten_connectstring) + "\" SettingsObjectName=\"System.Configuration.ConfigurationManager.ConnectionStrings\" SettingsPropertyName=\"" + ten_connectstring + "\" Provider=\"System.Data.SqlClient\" />";
        System.Data.DataTable dt_table = Mbg.Data.SqlClient.SqlHelper.GetData(sql_table, "@start", 0, "@end", 10000);
        foreach (System.Data.DataRow row_table in dt_table.Rows)
        {
            kq += "\n<Table Name=\"" + row_table[1] + "." + row_table[2] + "\" Member=\"" + getMember(row_table[2].ToString()) + "\">";
            kq += "\n<Type Name=\"" + row_table[2] + "\">";

            //get colum in table
            string sql_column = String.Format(@"SELECT vnn_col.TABLE_NAME, vnn_col.COLUMN_NAME, vnn_col.IS_NULLABLE,
            vnn_col.DATA_TYPE, vnn_col.CHARACTER_MAXIMUM_LENGTH, 
            OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName,
            type_key.CONSTRAINT_TYPE
            FROM INFORMATION_SCHEMA.COLUMNS vnn_col
            left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE vnn_key
            on vnn_col.TABLE_NAME = vnn_key.TABLE_NAME and vnn_col.COLUMN_NAME = vnn_key.COLUMN_NAME
            left join sys.foreign_keys f
            on f.name = vnn_key.CONSTRAINT_NAME
            left join INFORMATION_SCHEMA.TABLE_CONSTRAINTS type_key
			on type_key.CONSTRAINT_NAME = vnn_key.CONSTRAINT_NAME
            WHERE vnn_col.TABLE_NAME = '{0}'", row_table[2]);
            //get foreign
            string foreign = "";
            //get table depend on sql_colum
            System.Data.DataTable dt_column = Mbg.Data.SqlClient.SqlHelper.GetData(sql_column, "@start", 0, "@end", 10000);

            foreach (System.Data.DataRow row_column in dt_column.Rows)
            {
                string datatype = Fix_type(row_column[3].ToString(), row_column[4].ToString()), canbebull = "CanBeNull=\"true\"", isprimary = "";
                if(row_column[2].ToString() == "NO") { datatype += " NOT NULL"; canbebull = "CanBeNull=\"false\""; }
                if(row_column[6].ToString() == "PRIMARY KEY") 
                { 
                    isprimary = "IsPrimaryKey=\"true\""; 
                }
                else if(row_column[6].ToString() == "FOREIGN KEY")
                {
                    foreign += String.Format("\n<Association Name=\"{0}_{1}\" Member=\"{0}\" ThisKey=\"{2}\""+
                    " OtherKey=\"{2}\" Type=\"{0}\" IsForeignKey=\"true\" />", row_column[5], row_column[0], row_column[1]);
                }
                kq += "\n<Column Name=\""+ row_column[1] +"\" Type=\""+ getSystemTypes(row_column[3].ToString(),0) +"\" DbType=\""+ datatype +"\" " + isprimary +  " "+ canbebull +" />";
            }
            kq += foreign;
            kq += "\n</Type>";
            kq += "\n</Table>";
        }
        kq += "\n</Database>";
        return kq;
    }

    public static string UpdateDesigner (string ten_db, string ten_linq, string ten_connectstring)
    {
        //get table name
        string kq = "";
        kq += "#pragma warning disable 1591";
        kq += "\nusing System;";
        kq += "\nusing System.Collections.Generic;";
        kq += "\nusing System.ComponentModel;";
        kq += "\nusing System.Data;";
        kq += "\nusing System.Data.Linq;";
        kq += "\nusing System.Data.Linq.Mapping;";
        kq += "\nusing System.Linq;";
        kq += "\nusing System.Linq.Expressions;";
        kq += "\nusing System.Reflection;";

        kq += "\n\n\n[global::System.Data.Linq.Mapping.DatabaseAttribute(Name=\"" + ten_db + "\")]";
        kq += "\npublic partial class "+ ten_linq +"DataContext : System.Data.Linq.DataContext";
        kq += "\n{";
        kq += "\nprivate static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();";
        
        //--
        string detail_Extensibility = "";
        detail_Extensibility += "\n#region Extensibility Method Definitions";
        detail_Extensibility += "\npartial void OnCreated();";
        //--
        string detail_PublicFunction = "";
        detail_PublicFunction += "\n\n\npublic "+ ten_linq +"DataContext() :";
		detail_PublicFunction += "\nbase(global::System.Configuration.ConfigurationManager.ConnectionStrings[\""+ ten_connectstring +"\"].ConnectionString, mappingSource)";
	    detail_PublicFunction += "\n{";
		detail_PublicFunction +="\nOnCreated();";
	    detail_PublicFunction += "\n}";
        
        detail_PublicFunction += "\n\n\npublic "+ ten_linq +"DataContext(string connection) :"; 
		detail_PublicFunction += "\nbase(connection, mappingSource)";
	    detail_PublicFunction += "\n{";
		detail_PublicFunction += "\nOnCreated();";
	    detail_PublicFunction += "\n}";
	
	    detail_PublicFunction += "\n\n\npublic "+ ten_linq +"DataContext(System.Data.IDbConnection connection) : ";
		detail_PublicFunction += "\nbase(connection, mappingSource)";
	    detail_PublicFunction += "\n{";
		detail_PublicFunction += "\nOnCreated();";
	    detail_PublicFunction += "\n}";
	
	    detail_PublicFunction += "\n\n\npublic "+ ten_linq +"DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : ";
		detail_PublicFunction += "\nbase(connection, mappingSource)";
	    detail_PublicFunction += "\n{";
		detail_PublicFunction += "\nOnCreated();";
	    detail_PublicFunction += "\n}";
	
	    detail_PublicFunction += "\n\n\npublic "+ ten_linq +"DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :";
        detail_PublicFunction += "\nbase(connection, mappingSource)";
	    detail_PublicFunction += "\n{";
		detail_PublicFunction += "\nOnCreated();";
        detail_PublicFunction += "\n}";
        //--
        string detail_TableAttribute = "";
        //--
        string sql_table = "USE " + ten_db + " SELECT * FROM INFORMATION_SCHEMA.tables";
        System.Data.DataTable dt_table = Mbg.Data.SqlClient.SqlHelper.GetData(sql_table, "@start", 0, "@end", 10000);
        foreach (System.Data.DataRow row_table in dt_table.Rows)
        {
            string sql_colum = String.Format(@"SELECT vnn_col.TABLE_NAME, vnn_col.COLUMN_NAME, vnn_col.IS_NULLABLE,
            vnn_col.DATA_TYPE, vnn_col.CHARACTER_MAXIMUM_LENGTH, 
            OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName,
            type_key.CONSTRAINT_TYPE
            FROM INFORMATION_SCHEMA.COLUMNS vnn_col
            left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE vnn_key
            on vnn_col.TABLE_NAME = vnn_key.TABLE_NAME and vnn_col.COLUMN_NAME = vnn_key.COLUMN_NAME
            left join sys.foreign_keys f
            on f.name = vnn_key.CONSTRAINT_NAME
            left join INFORMATION_SCHEMA.TABLE_CONSTRAINTS type_key
            on type_key.CONSTRAINT_NAME = vnn_key.CONSTRAINT_NAME
            WHERE vnn_col.TABLE_NAME = '{0}'", row_table[2]);
            //--
            detail_Extensibility += String.Format("\npartial void Inser{0}({0} instance);",row_table[2]);
            detail_Extensibility += String.Format("\npartial void Update{0}({0} instance);",row_table[2]);
            detail_Extensibility += String.Format("\npartial void Delete{0}({0} instance);",row_table[2]);
            //--
            detail_PublicFunction += "\npublic System.Data.Linq.Table<" + row_table[2] + "> " + getMember(row_table[2].ToString());
            detail_PublicFunction += "\n{";
            detail_PublicFunction += "\nget";
            detail_PublicFunction += "\n{";
            detail_PublicFunction += "\nreturn this.GetTable<" + row_table[2] + ">();";
            detail_PublicFunction += "\n}";
            detail_PublicFunction += "\n}";
            //--
            detail_TableAttribute += String.Format("\n\n\n[global::System.Data.Linq.Mapping.TableAttribute(Name=\"{0}.{1}\")]", row_table[1], row_table[2]);
            detail_TableAttribute += "\npublic partial class "+ row_table[2] +" : INotifyPropertyChanging, INotifyPropertyChanged";
            detail_TableAttribute += "\n{";
            detail_TableAttribute += "\nprivate static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);";
            //-- --
            string detail_TableAttribute_Extensibility ="";
            detail_TableAttribute_Extensibility += "\n#region Extensibility Method Definitions";
            detail_TableAttribute_Extensibility += "\npartial void OnLoaded();";
            detail_TableAttribute_Extensibility += "\npartial void OnValidate(System.Data.Linq.ChangeAction action);";
            detail_TableAttribute_Extensibility += "\npartial void OnCreated();";
            //-- --
            string detail_TableAttribute_ColumnAttribute = "";
            //-- --
            string detail_TableAttribute_Foreignkey = "";
            System.Data.DataTable dt_column = Mbg.Data.SqlClient.SqlHelper.GetData(sql_colum, "@start", 0, "@end", 10000);
            foreach (System.Data.DataRow row_column in dt_column.Rows)
            {
                string code_foreignkey="", row1 = row_column[1].ToString().Replace(" ","");
                //--
                detail_TableAttribute += "\nprivate " + getSystemTypes(row_column[3].ToString(), 1) + " _" + row_column[1] + ";";
                //--
                string datatype = Fix_type(row_column[3].ToString(), row_column[4].ToString()), canbebull = ", CanBeNull=true", isprimary = "";
                if (row_column[2].ToString() == "NO") { datatype += " NOT NULL"; canbebull = ", CanBeNull=false"; }
                if (row_column[6].ToString() == "PRIMARY KEY")
                {
                    isprimary = ", IsPrimaryKey=true";
                }
                else if (row_column[6].ToString() == "FOREIGN KEY")
                {
                    detail_TableAttribute_Foreignkey += "\nthis._" + row_column[5] + " = default(EntityRef<" + row_column[5] + ">);";
                    code_foreignkey += "\nif (this._"+ row_column[5] +".HasLoadedOrAssignedValue)";
				    code_foreignkey += "\n{";
					code_foreignkey += "\nthrow new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();";
				    code_foreignkey += "\n}";
                    detail_TableAttribute += "\nprivate EntityRef<" + row_column[5] + "> _" + row_column[5] + ";";
                }
                //--
                detail_TableAttribute_ColumnAttribute += "\n[global::System.Data.Linq.Mapping.ColumnAttribute(" +
                "Storage=\"_" + row1 + "\", DbType=\"" + datatype + "\"" + canbebull + isprimary + " , UpdateCheck=UpdateCheck.Never)]";
                detail_TableAttribute_ColumnAttribute += "\npublic " + getSystemTypes(row_column[3].ToString(), 1) + " " + Fix_name(row1.ToString());
                detail_TableAttribute_ColumnAttribute += "\n{";
                detail_TableAttribute_ColumnAttribute += "\nget";
                detail_TableAttribute_ColumnAttribute += "\n{";
                detail_TableAttribute_ColumnAttribute += "\nreturn this._" + row1 + ";";
                detail_TableAttribute_ColumnAttribute += "\n}";
                detail_TableAttribute_ColumnAttribute += "\nset";
                detail_TableAttribute_ColumnAttribute += "\n{";
                detail_TableAttribute_ColumnAttribute += "\nif ((this._" + row1 + " != value))";
                detail_TableAttribute_ColumnAttribute += "\n{";
                detail_TableAttribute_ColumnAttribute += code_foreignkey;
                detail_TableAttribute_ColumnAttribute += "\nthis.On" + row1 + "Changing(value);";
                detail_TableAttribute_ColumnAttribute += "\nthis.SendPropertyChanging();";
                detail_TableAttribute_ColumnAttribute += "\nthis._" + row1 + " = value;";
                detail_TableAttribute_ColumnAttribute += "\nthis.SendPropertyChanged(\"" + row1 + "\");";
                detail_TableAttribute_ColumnAttribute += "\nthis.On" + row1 + "Changed();";
                detail_TableAttribute_ColumnAttribute += "\n}";
                detail_TableAttribute_ColumnAttribute += "\n}";
                detail_TableAttribute_ColumnAttribute += "\n}";
                //--
                detail_TableAttribute_Extensibility += "\npartial void On" + row1 + "Changing(" + getSystemTypes(row_column[3].ToString(), 1) + " value);";
                detail_TableAttribute_Extensibility += "\npartial void On" + row1 + "Changed();";
            
            }
            detail_TableAttribute_Extensibility += "\n#endregion";
            //--
            detail_TableAttribute += detail_TableAttribute_Extensibility;
            detail_TableAttribute += "\npublic "+ row_table[2] +"()";
	        detail_TableAttribute += "\n{";
            detail_TableAttribute += detail_TableAttribute_Foreignkey;
		    detail_TableAttribute += "\nOnCreated();";
            detail_TableAttribute += "\n}";
            detail_TableAttribute += detail_TableAttribute_ColumnAttribute;

            //--
            detail_TableAttribute += "\npublic event PropertyChangingEventHandler PropertyChanging;";
            detail_TableAttribute += "\npublic event PropertyChangedEventHandler PropertyChanged;";
            detail_TableAttribute += "\nprotected virtual void SendPropertyChanging()";
            detail_TableAttribute += "\n{";
            detail_TableAttribute += "\nif ((this.PropertyChanging != null))";
            detail_TableAttribute += "\n{";
            detail_TableAttribute += "\nthis.PropertyChanging(this, emptyChangingEventArgs);";
            detail_TableAttribute += "\n}";
            detail_TableAttribute += "\n}";

            detail_TableAttribute += "\nprotected virtual void SendPropertyChanged(String propertyName)";
            detail_TableAttribute += "\n{";
            detail_TableAttribute += "\nif ((this.PropertyChanged != null))";
            detail_TableAttribute += "\n{";
            detail_TableAttribute += "\nthis.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));";
            detail_TableAttribute += "\n}";
            detail_TableAttribute += "\n}";
            //--
            detail_TableAttribute += "\n}";
        }
        detail_Extensibility += "\n#endregion";

        kq = kq + detail_Extensibility + detail_PublicFunction + "\n}";
        kq += detail_TableAttribute;
        kq += "\n#pragma warning restore 1591";
        return kq;
    }

    public static string getSystemTypes(string DBtypes, int type)
    {
        string kq="";
        if (DBtypes.Equals("text") | DBtypes.Equals("ntext") |
            DBtypes.Equals("nvarchar") | DBtypes.Equals("varchar") |
            DBtypes.Equals("char") | DBtypes.Equals("nchar") 
            )
        {
            if (type == 0)
                kq = "System.String";
            else
                kq = "string";
        }
        else if (DBtypes.Equals("datetime") | DBtypes.Equals("date") |
            DBtypes.Equals("datetime2") | DBtypes.Equals("smalldatetime")
            )
        {
            if (type == 0)
                kq = "System.DateTime";
            else
                kq = "System.Nullable<System.DateTime>";
        }
        else if (DBtypes.Equals("datetimeoffset"))
        {
            if (type == 0)
                kq = "System.DateTimeOffset";
            else
                kq = "System.Nullable<System.DateTimeOffset>";
        }
        else if (DBtypes.Equals("bit"))
        {
            if (type == 0)
                kq = "System.Boolean";
            else
                kq = "System.Nullable<bool>";
        }
        else if (DBtypes.Equals("int"))
        {
            if (type == 0)
                kq = "System.Int32";
            else
                kq = "System.Nullable<int>";
        }
        else if (DBtypes.Equals("bigint"))
        {
            if (type == 0)
                kq = "System.Int64";
            else
                kq = "System.Nullable<long>";
        }
        else if (DBtypes.Equals("smallint"))
        {
            if (type == 0)
                kq = "System.Int16";
            else
                kq = "System.Nullable<short>";
        }
        else if (DBtypes.Equals("decimal"))
        {
            if (type == 0)
                kq = "System.Decimal";
            else
                kq = "System.Nullable<decimal>";
        }
        else if (DBtypes.Equals("float") | DBtypes.Equals("double"))
        {
            if (type == 0)
                kq = "System.Double";
            else
                kq = "System.Nullable<double>";
        }
        else if (DBtypes.Equals("numeric") | DBtypes.Equals("money"))
        {
            if (type == 0)
                kq = "System.Decimal";
            else
                kq = "System.Nullable<decimal>";
        }
        else if (DBtypes.Equals("real"))
        {
            if (type == 0)
                kq = "System.Single";
            else
                kq = "System.Nullable<float>";
        }
        else if (DBtypes.Equals("time"))
        {
            if (type == 0)
                kq = "System.TimeSpan";
            else
                kq = "System.Nullable<System.TimeSpan>";
        }
        else if (DBtypes.Equals("timestamp") | DBtypes.Equals("varbinary") |
            DBtypes.Equals("binary")
            )
        {
            if (type == 0)
                kq = "System.Data.Linq.Binary";
            else
                kq = "System.Data.Linq.Binary";
        }
        else if (DBtypes.Equals("tinyint"))
        {
            if (type == 0)
                kq = "System.Byte";
            else
                kq = "System.Nullable<byte>";
        }
        else if (DBtypes.Equals("uniqueidentifier"))
        {
            if (type == 0)
                kq = "System.Guid";
            else
                kq = "System.Nullable<System.Guid>";
        }
        else if (DBtypes.Equals("xml"))
        {
            if (type == 0)
                kq = "System.Xml.Linq.XElement";
            else
                kq = "System.Xml.Linq.XElement";
        }
        else
        {
            if (type == 0)
                kq = "System.String";
            else
                kq = "string";
        }
        return kq;
    }

    public static string getMember(string member)
    {
        string kq = "";
        
        //if (member.Length > 1)
        //{
        //    if (member.ElementAt(member.Length - 2) == 's' & member.ElementAt(member.Length - 1) == 'h')
        //    {
        //        member = member + "";
        //    }
        //    else if (member.ElementAt(member.Length - 2) == 'c' & member.ElementAt(member.Length - 1) == 'h')
        //    {
        //        member = member + "";
        //    }
        //    else if (member.ElementAt(member.Length - 1) == 'y')
        //    {
        //        member = member.Remove(member.Length - 1) + "";
        //    }
        //    else if (member.ElementAt(member.Length - 1) == 'x' | 
        //        member.ElementAt(member.Length - 1) == 's' |
        //        member.ElementAt(member.Length - 1) == 'z'
        //        )
        //    {
        //        member = member + "";
        //    }
        //    else
        //    {
        //        member = member + "";
        //    }
        //}
        //else
        //{
        //    if (member.ElementAt(member.Length - 1) == 'y')
        //    {
        //        member = member.Remove(member.Length - 1) + "ies";
        //    }
        //    else if (member.ElementAt(member.Length - 1) == 'x' | 
        //        member.ElementAt(member.Length - 1) == 's' |
        //        member.ElementAt(member.Length - 1) == 'z'
        //        )
        //    {
        //        member = member + "";
        //    }
        //    else
        //    {
        //        member = member + "";
        //    }
        //}
        kq = member;
        return kq;
    }

    //public static void Exec_UpdateLinq(System.Web.HttpContext context)
    //{
    //    LinqDataContext db = new LinqDataContext();
    //    ad_systemconfig ttc = db.ad_systemconfigs.FirstOrDefault();
    //    string filepath_dbml = ttc.url_linq + "/" + ttc.ten_linq + ".dbml";
    //    string filepath_cs = ttc.url_linq + "/" + ttc.ten_linq + ".designer.cs";
    //    //string kq_dbml = UpdateLinq(ttc.ten_db, ttc.ten_linq, ttc.ten_connectstring);
    //    string kq_cs = UpdateDesigner(ttc.ten_db, ttc.ten_linq, ttc.ten_connectstring);
    //    filepath_dbml = context.Server.MapPath(filepath_dbml).Replace("\\Controller","").Replace("\\JQGridModify","");
    //    filepath_cs = context.Server.MapPath(filepath_cs).Replace("\\Controller","").Replace("\\JQGridModify","");
    //    if (File.Exists(filepath_dbml) & File.Exists(filepath_cs))
    //    {
    //        //string w_dbml = File.ReadAllText(filepath_dbml);
    //        //w_dbml = w_dbml.Replace(w_dbml, kq_dbml);
    //        //File.WriteAllText(filepath_dbml, w_dbml);

    //        string w_cs = File.ReadAllText(filepath_cs);
    //        w_cs = w_cs.Replace(w_cs, kq_cs).Replace("$", "_");
    //        File.WriteAllText(filepath_cs, w_cs);
    //    }
    //    else
    //    {
    //        if (!File.Exists(filepath_dbml))
    //        {
    //            //StreamWriter w = File.CreateText(filepath_dbml);
    //            //w.WriteLine(kq_dbml);
    //            //w.Flush();
    //            //w.Close();
    //        }

    //        if (!File.Exists(filepath_cs))
    //        {
    //             StreamWriter w = File.CreateText(filepath_cs);
    //            w.WriteLine(kq_cs);
    //            w.Flush();
    //            w.Close();
    //        }
    //    }
    //}

    public static string Fix_name(string name)
    {
        string kq = "@" + name;
        return kq;
    }

    public static string Fix_type(string type, string length)
    {
        string kq = type;
        if (kq.Equals("ntext") | kq.Equals("text") | 
            kq.Equals("image") | kq.Equals("xml")
            ) 
        { length = ""; }
        else if (kq.Equals("decimal"))
        { length = "18,0"; }
        if (length != null & length != "")
        {
            if (length == "-1")
                length = "MAX";
            kq = type.Substring(0, 1).ToUpper() + type.Substring(1) + "(" + length + ")";
        }
        else
        {
            if (type == "timestamp")
                kq = "rowversion";
            else
                kq = type.Substring(0, 1).ToUpper() + type.Substring(1);
        }
        return kq;
    }

    public static string get_KeyOfTable(string ten_table)
    {
        string sql = VNN_Function.ADGetColumn_SQL(ten_table, "", " and type_key.CONSTRAINT_TYPE = 'PRIMARY KEY'");
        System.Data.DataTable dt_column = Mbg.Data.SqlClient.SqlHelper.GetData(sql, "@start", 0, "@end", 10);
        if (dt_column.Rows.Count > 0)
            return dt_column.Rows[0][0].ToString();
        else
            return null;
    }
}