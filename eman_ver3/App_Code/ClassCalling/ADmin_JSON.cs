using System;
using System.Linq;
using System.Collections.Generic;
using DataAcess;
using Newtonsoft.Json;
using System.Web;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Text;

public class ADmin_JSON
{
    public string urlData { get; set; }
    private string FormsCookieName { get; set; }
    public List<string> modules = new List<string>();

    public ADmin_JSON()
    {
        FormsCookieName = System.Web.Security.FormsAuthentication.FormsCookieName;
    }

    private string getCacheName(string moduleName)
    {
        moduleName = moduleName == null ? urlData : moduleName;
        return FormsCookieName + "_" + moduleName;
    }

    public void ClearCache(HttpContext context)
    {
        if (modules.Count <= 0)
        {
            modules.Add("ad_menu");
            modules.Add("ad_module");
            modules.Add("ad_column");
            modules.Add("ad_case");
            modules.Add("ad_role_mmc");
            modules.Add("ad_systemconfig");
            modules.Add("ad_user_mmc");
        }

        foreach (var module in modules)
        {
            context.Cache.Remove(getCacheName(module));
        }
    }

    public void ClearCache(HttpContext context, string name)
    {
        context.Cache.Remove(name);
    }

    public List<Dictionary<string, object>> ReadJSON()
    {
        HttpContext context = HttpContext.Current;
        string url = context.Server.MapPath(Security.UrlBase() + "App_Data/JsonData/" + urlData + ".json");
        string json = File.ReadAllText(url);
        List<Dictionary<string, object>> items = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
        return items;
    }

    public List<ad_column> ad_columnJSON()
    {
        urlData = typeof(ad_column).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<ad_column>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<ad_column>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<ad_column>;
        }
        return jsonData;
    }

    public List<ad_systemconfig> ad_systemconfigJSON()
    {
        urlData = typeof(ad_systemconfig).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<ad_systemconfig>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<ad_systemconfig>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<ad_systemconfig>;
        }
        return jsonData;
    }

    public List<ad_module> ad_moduleJSON()
    {
        urlData = typeof(ad_module).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<ad_module>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<ad_module>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<ad_module>;
        }
        return jsonData;
    }

    public List<ad_selectoption> ad_selectoptionJSON()
    {
        urlData = typeof(ad_selectoption).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<ad_selectoption>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<ad_selectoption>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<ad_selectoption>;
        }
        return jsonData;
    }

    public List<ad_case> ad_caseJSON()
    {
        urlData = typeof(ad_case).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<ad_case>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<ad_case>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<ad_case>;
        }
        return jsonData;
    }

    public List<ad_role_mmc> ad_role_mmcJSON()
    {
        urlData = typeof(ad_role_mmc).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<ad_role_mmc>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<ad_role_mmc>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<ad_role_mmc>;
        }
        return jsonData;
    }

    public List<ad_role_mmcol> ad_role_mmcolJSON()
    {
        urlData = typeof(ad_role_mmcol).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<ad_role_mmcol>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<ad_role_mmcol>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<ad_role_mmcol>;
        }
        return jsonData;
    }

    public List<ad_role_mmvalue> ad_role_mmvalueJSON()
    {
        urlData = typeof(ad_role_mmvalue).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<ad_role_mmvalue>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<ad_role_mmvalue>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<ad_role_mmvalue>;
        }
        return jsonData;
    }

    public List<ad_role_where> ad_role_whereJSON()
    {
        urlData = typeof(ad_role_where).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<ad_role_where>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<ad_role_where>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<ad_role_where>;
        }
        return jsonData;
    }

    public List<ad_menu> ad_menuJSON()
    {
        urlData = typeof(ad_menu).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<ad_menu>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<ad_menu>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<ad_menu>;
        }
        return jsonData;
    }

    public List<md_ghinotruno> md_ghinotrunoJSON()
    {
        urlData = typeof(md_ghinotruno).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<md_ghinotruno>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<md_ghinotruno>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<md_ghinotruno>;
        }
        return jsonData;
    }

    public List<ad_user_mmc> ad_user_mmcJSON()
    {
        urlData = typeof(ad_user_mmc).Name;
        object cache = HttpContext.Current.Cache.Get(getCacheName(urlData));
        var jsonData = new List<ad_user_mmc>();

        if (cache == null)
        {
            jsonData = JsonConvert.DeserializeObject<List<ad_user_mmc>>(ReadStringJSON());
            HttpContext.Current.Cache.Insert(getCacheName(urlData), jsonData);
        }
        else
        {
            jsonData = cache as List<ad_user_mmc>;
        }
        return jsonData;
    }

    public string ReadStringJSON()
    {
        HttpContext context = HttpContext.Current;
        string url = ExcuteSignalRStatic.mapPathSignalR("~/App_Data/JsonData/" + urlData + ".json");
        string json = File.ReadAllText(url);
        return json;
    }

    #region Modify
    public void WriteJson()
    {
        string select = string.Format("select * from {0} (nolock)", urlData);
        DataTable tables = Mbg.Data.SqlClient.SqlHelper.GetData(select);
        List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
        foreach (DataRow row_ in tables.Rows)
        {
            Dictionary<string, object> item = new Dictionary<string, object>();
            foreach (DataColumn column_ in tables.Columns)
            {
                item[column_.ColumnName] = row_[column_.ColumnName].ToString();
            }
            lst.Add(item);
        }

        HttpContext context = HttpContext.Current;
        string filepath = context.Server.MapPath(Security.UrlBase() + "App_Data/JsonData/" + urlData + ".json");
        if (!File.Exists(filepath))
        {
            StreamWriter w = new StreamWriter(filepath, false, Encoding.UTF8);
            w.Flush();
            w.Close();
        }
        string Content = JsonConvert.SerializeObject(lst, Formatting.Indented);
        File.WriteAllText(filepath, Content);
        context.Cache.Remove(getCacheName(urlData));
    }

    public void WriteJson(string jsonData)
    {
        HttpContext context = HttpContext.Current;
        string filepath = context.Server.MapPath(Security.UrlBase() + "App_Data/JsonData/" + urlData + ".json");
        if (!File.Exists(filepath))
        {
            StreamWriter w = new StreamWriter(filepath, false, Encoding.UTF8);
            w.Flush();
            w.Close();
        }
        File.WriteAllText(filepath, jsonData);
        context.Cache.Remove(getCacheName(urlData));
    }

    public void WriteJson(List<dynamic> jsonData)
    {
        var data = JsonConvert.SerializeObject(jsonData);
        HttpContext context = HttpContext.Current;
        string filepath = context.Server.MapPath(Security.UrlBase() + "App_Data/JsonData/" + urlData + ".json");
        if (!File.Exists(filepath))
        {
            StreamWriter w = new StreamWriter(filepath, false, Encoding.UTF8);
            w.Flush();
            w.Close();
        }
        File.WriteAllText(filepath, data);
        context.Cache.Remove(getCacheName(urlData));
    }
    #endregion
}