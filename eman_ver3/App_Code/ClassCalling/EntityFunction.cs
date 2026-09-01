using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

public class EntityFunction
{
    public EntityFunction()
    {

    }

    public dynamic updateDataInEntity(dynamic objectDynamic, Type type, System.Web.HttpContext context)
    {
        var ma_module = context.Request.QueryString["ma_module"];
        var json = new ADmin_JSON();
        var columns = json.ad_columnJSON().Where(s=>s.ma_module == ma_module & (s.editable == "true")).ToList();
        var Keys = type.GetProperties().Select(s => s.Name).ToList();
        foreach (var key in Keys)
        {
            var valFromWeb = context.Request.Form[key].removeAllSpaceOrTrimText(true);
            var keyName = "";
            var column = columns.Where(s => s.ma_column.Trim() == key).FirstOrDefault();
            if (column == null)
                valFromWeb = "VNN_notpost";
            else
                keyName = column.label;

            var typeKey = type.GetProperty(key);
            var typeSav = typeKey.PropertyType.ToString();
            var isNull = typeSav.Contains("System.Nullable");
            var isString = typeSav.Contains("System.String");
            var isBool = typeSav.Contains("System.Boolean");
            var isDateTime = typeSav.Contains("System.DateTime");
            var isDecimal = typeSav.Contains("System.Decimal");
            var isDouble = typeSav.Contains("System.Double");
            var isInt32 = typeSav.Contains("System.Int32");
            var isInt64 = typeSav.Contains("System.Int64");

            if (!isNull & !isString) {
                if (valFromWeb == null)
                    throw new ArgumentNullException(key + " is not null");     
            }

            if (valFromWeb == "VNN_notpost")
            {
                if (!isNull & !isString)
                {
                    if(typeKey.GetValue(objectDynamic) == null)
                        throw new ArgumentNullException(key);
                }
            }
            else
            {
                if (isString)
                    typeKey.SetValue(objectDynamic, valFromWeb);
                else if (isDateTime)
                {
                    var date = VNN_Config.setDateTime(valFromWeb);
                    if (date.IsDate())
                        typeKey.SetValue(objectDynamic, date);
                    else
                        typeKey.SetValue(objectDynamic, null);
                }
                else if (isBool)
                {
                    try
                    {
                        typeKey.SetValue(objectDynamic, valFromWeb.ToNullableBool());
                    }
                    catch (Exception ex)
                    {
                        throw new FormatException(string.Format("<b>{0}</b>: {1}", keyName, ex.Message));
                    }
                }
                else if (isDecimal)
                {
                    try
                    {
                        typeKey.SetValue(objectDynamic, valFromWeb.ToNullableDecimal());
                    }
                    catch (Exception ex)
                    {
                        throw new FormatException(string.Format("<b>{0}</b>: {1}", keyName, ex.Message));
                    }
                }
                else if (isDouble)
                {
                    try
                    {
                        typeKey.SetValue(objectDynamic, valFromWeb.ToNullableDouble());
                    }
                    catch (Exception ex)
                    {
                        throw new FormatException(string.Format("<b>{0}</b>: {1}", keyName, ex.Message));
                    }
                }
                else if (isInt32)
                {
                    try
                    {
                        typeKey.SetValue(objectDynamic, valFromWeb.ToNullableInt());
                    }
                    catch (Exception ex)
                    {
                        throw new FormatException(string.Format("<b>{0}</b>: {1}", keyName, ex.Message));
                    }
                }
                else if (isInt64)
                {
                    try
                    {
                        typeKey.SetValue(objectDynamic, valFromWeb.ToNullableLong());
                    }
                    catch (Exception ex)
                    {
                        throw new FormatException(string.Format("<b>{0}</b>: {1}", keyName, ex.Message));
                    }
                }
            }
        }

        return objectDynamic;
    }
}
