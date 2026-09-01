using System;
using System.Linq;
using DataAcess;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq.Expressions;

/// <summary>
/// Summary description for Extension
/// </summary>
public static class Extension
{
    public static string Text<T>(this T t) where T : struct, IConvertible
    {
        return Enum.GetName(typeof(T), t);
    }
    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
    public static string Description<T>(this T t) where T : struct, IConvertible
    {
        var type = t.GetType();
        if (!type.IsEnum)
        {
            throw new ArgumentException("Not Enum type");
        }
        var memberInfo = type.GetMember(t.ToString());
        if (memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }
        return t.ToString();
    }

    public static String nameof<T, TT>(this T obj, Expression<Func<T, TT>> propertyAccessor)
    {
        if (propertyAccessor.Body.NodeType == ExpressionType.MemberAccess)
        {
            var memberExpression = propertyAccessor.Body as MemberExpression;
            if (memberExpression == null)
                return null;
            return memberExpression.Member.Name;
        }
        return null;
    }

    public static bool isScientificNotation(this string str)
    {
        if (str == null)
            return false;
        else
        {
            double dummy;
            return (str.Contains("E") || str.Contains("e")) && double.TryParse(str, out dummy);
        }
    }

    public static string changeScientificNotation_Decimal(this string str)
    {
        if(str.isScientificNotation())
        {
            str = double.Parse(str).ToString("0.########");
        }

        return str;
    }

    public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
    {
        var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
        var destProps = typeof(TU).GetProperties()
                .Where(x => x.CanWrite)
                .ToList();

        foreach (var sourceProp in sourceProps)
        {
            if (destProps.Any(x => x.Name == sourceProp.Name))
            {
                var p = destProps.First(x => x.Name == sourceProp.Name);
                if (p.CanWrite)
                { // check if the property can be set or no.
                    p.SetValue(dest, sourceProp.GetValue(source, null), null);
                }
            }
        }
    }

    public static T Clone<T>(this T source)
    {
        var dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
        using (var ms = new System.IO.MemoryStream())
        {
            dcs.WriteObject(ms, source);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            return (T)dcs.ReadObject(ms);
        }
    }

    public static double Round(this double value, int precision)
    {
        if (precision < -4 && precision > 15)
            throw new ArgumentOutOfRangeException("precision", "Must be and integer between -4 and 15");

        if (precision >= 0) return Math.Round(value, precision);
        else
        {
            precision = (int)Math.Pow(10, Math.Abs(precision));
            value = value + (5 * precision / 10);
            return Math.Round(value - (value % precision), 0);
        }
    }

    public static string DropTrailingZeros(this decimal value)
    {
        return value.ToString("#,#0.#####");
    }

    public static bool isNullOrEmptyOrWhiteSpace(this string value)
    {
        return (string.IsNullOrEmpty(value) | string.IsNullOrWhiteSpace(value));
    }

    public static decimal? Set0WhenlessThan0(this decimal value)
    {
        if(value < 0)
        {
            return 0;
        }
        else
        {
            return value;
        }
    }

    public static decimal NgoaiTeToiVND(this decimal value, string dongtienId, DateTime? ngaylap, EntityContext db)
    {
        string vndId = db.md_dongtien.Where(s => s.ma_iso == "VND").Select(s => s.md_dongtien_id).FirstOrDefault();
        var tygia = db.md_tygia.
            Where(s => 
                s.tu_dongtien_id == dongtienId & 
                s.sang_dongtien_id == vndId &
                s.hieuluc_tungay <= ngaylap &
                s.hieuluc_denngay >= ngaylap).FirstOrDefault();
        if (tygia != null)
            value = value * tygia.chia_cho.GetValueOrDefault(0);
        return value;
    }

    public static decimal TyGiaVND(string dongtienId, DateTime? ngaylap, EntityContext db)
    {
        decimal value = 1;
        string vndId = db.md_dongtien.Where(s => s.ma_iso == "VND").Select(s => s.md_dongtien_id).FirstOrDefault();
        var tygia = db.md_tygia.
            Where(s =>
                s.tu_dongtien_id == dongtienId &
                s.sang_dongtien_id == vndId &
                s.hieuluc_tungay <= ngaylap &
                s.hieuluc_denngay >= ngaylap).OrderByDescending(s=>s.hieuluc_tungay).FirstOrDefault();
        if (tygia != null)
            value = tygia.chia_cho.GetValueOrDefault(0);
        return value;
    }

    public static DateTime? ToDateTime(this object timestamp)
    {
        string a = timestamp + "";
        if (string.IsNullOrEmpty(a))
        {
            return null;
        }
        else
        {
            a = a.Replace("/Date(", "");
            a = a.Split(' ')[0];
            double b = double.Parse(a);
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddMilliseconds(b).ToLocalTime();
        }
    }

    public static md_lenhsanxuat_tosx getLSXToId_NKNB(string id_parent, EntityContext db)
    {
        var nknb = db.md_nhapkhonb.Where(s => s.md_nhapkhonb_id == id_parent).FirstOrDefault();
        var lsx = db.md_lenhsanxuat.Where(s => s.md_lenhsanxuat_id == nknb.md_lenhsanxuat_id).FirstOrDefault();
        string[] arr = nknb.md_lenhsanxuat_tosx_id.Split(new[] { " --- " }, StringSplitOptions.None);
        string sctLSX = arr[0];
        string tentoLSX = arr[1];
        string toId = db.md_phanxuong_to.Where(s => s.md_phanxuong_id == lsx.md_phanxuong_id &
        s.ten_to == tentoLSX).Select(s => s.md_to_id).FirstOrDefault();
        var lsxTSX = db.md_lenhsanxuat_tosx.Where(s => s.md_lenhsanxuat_id == lsx.md_lenhsanxuat_id
                            & s.md_phanxuong_to_id == toId).FirstOrDefault();
        return lsxTSX;
    }

    public static md_lenhsanxuat_tosx getLSXToId_NKNB(md_nhapkhonb nknb, EntityContext db)
    {
        var lsx = db.md_lenhsanxuat.Where(s => s.md_lenhsanxuat_id == nknb.md_lenhsanxuat_id).FirstOrDefault();
        string[] arr = nknb.md_lenhsanxuat_tosx_id.Split(new[] { " --- " }, StringSplitOptions.None);
        string sctLSX = arr[0];
        string tentoLSX = arr[1];
        string toId = db.md_phanxuong_to.Where(s => s.md_phanxuong_id == lsx.md_phanxuong_id &
        s.ten_to == tentoLSX).Select(s => s.md_to_id).FirstOrDefault();
        var lsxTSX = db.md_lenhsanxuat_tosx.Where(s => s.md_lenhsanxuat_id == lsx.md_lenhsanxuat_id
                            & s.md_phanxuong_to_id == toId).FirstOrDefault();
        return lsxTSX;
    }

    public static md_lenhsanxuat_tosx getLSXToId(string md_lenhsanxuat_tosx_id, EntityContext db)
    {
        string[] arr = md_lenhsanxuat_tosx_id.Split(new[] { " --- " }, StringSplitOptions.None);
        string sctLSX = arr[0];
        string tentoLSX = arr[1];
        var lsx = db.md_lenhsanxuat.Where(s => s.sochungtu == sctLSX).FirstOrDefault();
        string toId = db.md_phanxuong_to.Where(s => s.md_phanxuong_id == lsx.md_phanxuong_id &
        s.ten_to == tentoLSX).Select(s => s.md_to_id).FirstOrDefault();
        var lsxTSX = db.md_lenhsanxuat_tosx.Where(s => s.md_lenhsanxuat_id == lsx.md_lenhsanxuat_id
                            & s.md_phanxuong_to_id == toId).FirstOrDefault();
        return lsxTSX;
    }

    public static LSX_TSX getLSXToId2(string md_lenhsanxuat_tosx_id, EntityContext db)
    {
        string[] arr = md_lenhsanxuat_tosx_id.Split(new[] { " --- " }, StringSplitOptions.None);
        string sctLSX = arr[0];
        string tentoLSX = arr[1];
        var lsx = db.md_lenhsanxuat.Where(s => s.sochungtu == sctLSX).FirstOrDefault();
        string toId = db.md_phanxuong_to.Where(s => 
            s.md_phanxuong_id == lsx.md_phanxuong_id 
            & s.ten_to == tentoLSX
            ).Select(s => s.md_to_id).FirstOrDefault();

        if(string.IsNullOrWhiteSpace(toId))
        {
            toId = db.md_phanxuong_to.Where(s =>
            s.md_phanxuong_id == lsx.md_phanxuong_id
            & s.mota == tentoLSX
            ).Select(s => s.md_to_id).FirstOrDefault();
        }

        var lsxTSX = db.md_lenhsanxuat_tosx.Where(s => s.md_lenhsanxuat_id == lsx.md_lenhsanxuat_id
                            & s.md_phanxuong_to_id == toId).FirstOrDefault();
        return new LSX_TSX {
            lsx = lsx,
            tsx = lsxTSX
        };
    }

    public static md_lenhsanxuat_tosx getLSXToId_Prev(md_lenhsanxuat_tosx lsxTo, EntityContext db)
    {
        var prev = db.md_lenhsanxuat_tosx.Where(s => 
            s.stt < lsxTo.stt 
            & s.md_lenhsanxuat_id == lsxTo.md_lenhsanxuat_id
            & s.hoatdong == true
            )
            .OrderByDescending(s=>s.stt).Take(1).FirstOrDefault();
        return prev;
    }

    public static decimal getGiaNhanCong(string spId, string dvt, string PBGNC, EntityContext db)
    {
        var gia = db.md_giasanpham.Where(s =>
            s.md_phienbangia_id == PBGNC
            & s.md_sanpham_id == spId
            & s.md_donvitinhsanpham_id == dvt).Select(s=>s.gia).FirstOrDefault();
        return gia.GetValueOrDefault(0);
    }

    public static bool IsDate(this DateTime date)
    {
        bool ok = true;
        if (date == DateTime.MinValue | date == DateTime.MinValue.AddDays(1))
            ok = false;
        return ok;
    }

    public static decimal DangNoCuaTo(md_xuatkhonb_cdh xnb_cdh, EntityContext db)
    {
        var xnb = db.md_xuatkhonb.Where(s => s.md_xuatkhonb_id == xnb_cdh.md_xuatkhonb_id).FirstOrDefault();
        //string tento = db.md_phanxuong_to.Where(s => s.md_to_id == xnb.md_to_id).Select(s => s.ten_to).FirstOrDefault();
        var notos = db.md_kho_ghino.Where(s => s.md_sanpham_id == xnb_cdh.md_sanpham_id);
        notos = notos.Where(s =>
                s.md_phanxuong_id == xnb.md_phanxuong_id
                & s.md_to_id == xnb.md_to_id
                );
        
        return notos.ToList().Sum(s => s.soluong_no.GetValueOrDefault(0));
    }

    ///###############################################################
    /// <summary>
    /// Convert a List to a DataTable.
    /// </summary>
    /// <remarks>
    /// Based on MIT-licensed code presented at http://www.chinhdo.com/20090402/convert-list-to-datatable/ as "ToDataTable"
    /// <para/>Code modifications made by Nick Campbell.
    /// <para/>Source code provided on this web site (chinhdo.com) is under the MIT license.
    /// <para/>Copyright © 2010 Chinh Do
    /// <para/>Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
    /// <para/>The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
    /// <para/>THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    /// <para/>(As per http://www.chinhdo.com/20080825/transactional-file-manager/)
    /// </remarks>
    /// <typeparam name="T">Type representing the type to convert.</typeparam>
    /// <param name="l_oItems">List of requested type representing the values to convert.</param>
    /// <returns></returns>
    ///###############################################################
    /// <LastUpdated>February 15, 2010</LastUpdated>
    public static DataTable ToDataTable<T>(this List<T> l_oItems)
    {
        DataTable oReturn = new DataTable(typeof(T).Name);
        object[] a_oValues;
        int i;

        //#### Collect the a_oProperties for the passed T
        PropertyInfo[] a_oProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //#### Traverse each oProperty, .Add'ing each .Name/.BaseType into our oReturn value
        //####     NOTE: The call to .BaseType is required as DataTables/DataSets do not support nullable types, so it's non-nullable counterpart Type is required in the .Column definition
        foreach (PropertyInfo oProperty in a_oProperties)
        {
            oReturn.Columns.Add(oProperty.Name, BaseType(oProperty.PropertyType));
        }

        //#### Traverse the l_oItems
        foreach (T oItem in l_oItems)
        {
            //#### Collect the a_oValues for this loop
            a_oValues = new object[a_oProperties.Length];

            //#### Traverse the a_oProperties, populating each a_oValues as we go
            for (i = 0; i < a_oProperties.Length; i++)
            {
                a_oValues[i] = a_oProperties[i].GetValue(oItem, null);
            }

            //#### .Add the .Row that represents the current a_oValues into our oReturn value
            oReturn.Rows.Add(a_oValues);
        }

        //#### Return the above determined oReturn value to the caller
        return oReturn;
    }

    ///###############################################################
    /// <summary>
    /// Returns the underlying/base type of nullable types.
    /// </summary>
    /// <remarks>
    /// Based on MIT-licensed code presented at http://www.chinhdo.com/20090402/convert-list-to-datatable/ as "GetCoreType"
    /// <para/>Code modifications made by Nick Campbell.
    /// <para/>Source code provided on this web site (chinhdo.com) is under the MIT license.
    /// <para/>Copyright © 2010 Chinh Do
    /// <para/>Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
    /// <para/>The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
    /// <para/>THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    /// <para/>(As per http://www.chinhdo.com/20080825/transactional-file-manager/)
    /// </remarks>
    /// <param name="oType">Type representing the type to query.</param>
    /// <returns>Type representing the underlying/base type.</returns>
    ///###############################################################
    /// <LastUpdated>February 15, 2010</LastUpdated>
    public static Type BaseType(Type oType)
    {
        //#### If the passed oType is valid, .IsValueType and is logicially nullable, .Get(its)UnderlyingType
        if (oType != null && oType.IsValueType &&
            oType.IsGenericType && oType.GetGenericTypeDefinition() == typeof(Nullable<>)
        )
        {
            return Nullable.GetUnderlyingType(oType);
        }
        //#### Else the passed oType was null or was not logicially nullable, so simply return the passed oType
        else
        {
            return oType;
        }
    }

    public static void delayTask(this System.Threading.Tasks.Task task, int time)
    {
        System.Threading.Timer timer = null;
        timer = new System.Threading.Timer((call) => {
            if (timer != null)
            {
                task.Start();
                timer.Dispose();
            }
        }, null, time, time);

        task.Wait();
    }

    public static int? ToNullableInt(this string s)
    {
        if (!string.IsNullOrWhiteSpace(s))
            return int.Parse(s);
        else
            return null;
    }

    public static long? ToNullableLong(this string s)
    {
        if (!string.IsNullOrWhiteSpace(s))
            return long.Parse(s);
        else
            return null;
    }

    public static decimal? ToNullableDecimal(this string s)
    {
        if (!string.IsNullOrWhiteSpace(s))
            return decimal.Parse(s);
        else
            return null;
    }

    public static double? ToNullableDouble(this string s)
    {
        if (!string.IsNullOrWhiteSpace(s))
            return double.Parse(s);
        else
            return null;
    }

    public static bool? ToNullableBool(this string s)
    {
        if (s != null)
            return s.ToLower() == "true";
        else
            return null;
    }

    public static DateTime? ToNullableDateTime(this string s, string fmt = "")
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return null;
        }
        else
        {
            var date = VNN_Config.setDateTime(s, fmt);
            if (date.IsDate())
                return date;
            else
                return null;
        }
    }

    public static string ToUnSign(this string input)
    {
        if (input == null)
        {
            input = "";
        }
        input = input.Trim();
        for (int i = 0x20; i < 0x30; i++)
        {
            input = input.Replace(((char)i).ToString(), " ");
        }
        Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
        string str = input.Normalize(NormalizationForm.FormD);
        string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
        while (str2.IndexOf("?") >= 0)
        {
            str2 = str2.Remove(str2.IndexOf("?"), 1);
        }
        return str2;
    }

    public static string removeAllSpaceOrTrimText(this string a, bool onlyTrim)
    {
        if (a == null)
            a = "";

        return onlyTrim ? a.Trim() : a.Replace(" ", "");
    }
}


public class LSX_TSX
{
    public md_lenhsanxuat lsx { get; set; }
    public md_lenhsanxuat_tosx tsx { get; set; }
}