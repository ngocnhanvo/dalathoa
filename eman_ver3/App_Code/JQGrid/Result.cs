using System;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Mbg.Web.JqGrid
{
    public class JqGResult
    {
        private int count, page, limit, totalPage;
        private DataTable dataSource;

        public static string rsDefault()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
            xml += "<rows><page>0</page><total>0</total><records>0</records></rows>";
            return xml;
        }

        public object WriteJson()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            int sl_columns = dataSource.Columns.Count;
            if (Count <= 0)
            {
                dataSource.Rows.Clear();
                dataSource.Rows.Add(0);
                for (int i = 1; i < sl_columns; i++)
                {
                    try
                    {
                        var type = dataSource.Columns[i].DataType;
                        var typeNumbers = new List<Type>() {
                            Type.GetType("System.Int"),
                            Type.GetType("System.Decimal"),
                            Type.GetType("System.Double")
                        };
                        if (type == Type.GetType("System.String"))
                            dataSource.Rows[0][i] = "err";
                        else if (typeNumbers.Contains(type))
                            dataSource.Rows[0][i] = -999999999;
                        else if (type == Type.GetType("System.DateTime"))
                            dataSource.Rows[0][i] = new DateTime(1000, 01, 01, 0, 0, 0);
                    }
                    catch { }
                }
            }

            /*for (int j = 0; j < Count; j++)
            {
                for (int i = 0; i < sl_columns; i++)
                {
                    try
                    {
                        dataSource.Rows[j][i] = context.Server.HtmlEncode(dataSource.Rows[j][i].ToString());
                    }
                    catch
                    {
                    }
                }
            }*/

            var result = new
            {
                    page = Page,
                    total = TotalPage,
                    records = Count,
                    rows = dataSource
            };
            return JsonConvert.SerializeObject(result);
        }

        public object WriteJson2()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            int sl_columns = dataSource.Columns.Count;
            if (Count <= 0)
            {
                dataSource.Rows.Clear();
                dataSource.Rows.Add(0);
                for (int i = 1; i < sl_columns; i++)
                {
                    try
                    {
                        var type = dataSource.Columns[i].DataType;
                        var typeNumbers = new List<Type>() {
                            Type.GetType("System.Int"),
                            Type.GetType("System.Decimal"),
                            Type.GetType("System.Double")
                        };
                        if (type == Type.GetType("System.String"))
                            dataSource.Rows[0][i] = "err";
                        else if (typeNumbers.Contains(type))
                            dataSource.Rows[0][i] = -999999999;
                        else if (type == Type.GetType("System.DateTime"))
                            dataSource.Rows[0][i] = new DateTime(1000, 01, 01, 0, 0, 0);
                    }
                    catch { }
                }
            }

            var result = new
            {
                    records = Count,
                    rows = dataSource
            };
            return JsonConvert.SerializeObject(result);
        }
        
        public String WriteXml2()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
            xml += "<rows>";
            xml += "<page>" + Page + "</page>";
            xml += "<total>" + TotalPage + "</total>";
            xml += "<records>" + Count + "</records>";
            if (Count <= 0)
            {
                xml += "<row>";
                foreach (System.Data.DataColumn col in dataSource.Columns)
                {
                    xml += "<cell><![CDATA[-]]></cell>";
                }
                xml += "</row>";
            }
            else
            {
                int count_col=0;
                foreach (System.Data.DataColumn col in dataSource.Columns)
                {
                    count_col++;
                }
                foreach (System.Data.DataRow row in dataSource.Rows)
                {
                    xml += "<row>";
                    for (int i = 0; i < count_col; i++ )
                        xml += "<cell><![CDATA[" + row[i] + "]]></cell>";
                    xml += "</row>";
                }
            }
            xml += "</rows>";
            return xml;
        }

        public String WriteXml()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
            xml += "<rows>";
            xml += "<page>" + Page + "</page>";
            xml += "<total>" + TotalPage + "</total>";
            xml += "<records>" + Count + "</records>";

            foreach (System.Data.DataRow row in dataSource.Rows)
            {
                xml += "<row>";
                foreach (System.Data.DataColumn col in dataSource.Columns)
                {
                    object obj = row[col];
                    xml += "<cell><![CDATA[" + obj + "]]></cell>";
                }
                xml += "</row>";
            }

            xml += "</rows>";
            return xml;
        }

        public JqGResult() { }

        public JqGResult(DataTable dataSource, int count, int page, int limit)
        {
            this.count = count;
            this.dataSource = dataSource;
            this.page = page;
            this.limit = limit;
        }

        public DataTable DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }

        public int Page
        {
            get
            {
                if (page > TotalPage)
                {
                    page = TotalPage;
                }
                return page;
            }
        }

        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public int TotalPage
        {
            get
            {
                if (Count > 0)
                {
                    totalPage = (int)Math.Ceiling(1.0 * Count / limit);
                }
                else
                {
                    totalPage = 0;
                }
                return totalPage;
            }
        }
    }
}