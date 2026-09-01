using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Objects
/// </summary>
public static class ExcuteSignalRStatic
{
	public static string mapPathSignalR(string path)
    {
        return System.Web.Hosting.HostingEnvironment.MapPath(path);
    }
}