using System;
using System.Linq;
using System.Data.Linq;
using NetServ.Net.Json;

namespace jqGridHelper
{
    public class RuleFilter
    {
        String field, oper, data;

        public String Field
        {
            get { return field; }
        }

        public String Oper
        {
            get { return oper; }
        }

        public String Data
        {
            get { return data; }
            set { data = value; }
        }

        public RuleFilter()
        {

        }

        public static RuleFilter CreateRuleFilter(IJsonType jsType)
        {
            JsonObject json = (JsonObject)jsType;
            RuleFilter rule = new RuleFilter();
            foreach (System.Collections.Generic.KeyValuePair<string, IJsonType> type in json)
            {
                String key = type.Key;
                String value = type.Value.ToString();
                switch (key)
                {
                    case "op":
                        rule.oper = value;
                        break;
                    case "data":
                        rule.data = value;
                        break;
                    case "field":
                    default:
                        rule.field = value;
                        break;
                }
            }
            return rule;
        }
    }

    public class Filter
    {
        String groupOp;
        System.Collections.Generic.List<RuleFilter> rules;
        IJsonType groups;
        public static String ToScriptPL(System.Collections.Generic.List<RuleFilter> rules, String groupOp, IJsonType groups)
        {
            String str = "";
            foreach (RuleFilter item in rules)
            {
                if (item.Data.ToUpper().Contains("[ALL]"))
                {
                    str += String.Format("{0} {1} LIKE '%' ", groupOp, item.Field);
                }
                else
                {
                    switch (item.Oper)
                    {
                        case "eq":
                            str += String.Format("{0} {1} = N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "ne":
                            str += String.Format("{0} CONVERT(nvarchar, {1}, 103) != N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "lt":
                            str += String.Format("{0} CONVERT(nvarchar, {1}, 103) < N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "le":
                            str += String.Format("{0} CONVERT(nvarchar, {1}, 103) =< N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "gt":
                            str += String.Format("{0} CONVERT(nvarchar, {1}, 103) > N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "ge":
                            str += String.Format("{0} CONVERT(nvarchar, {1}, 103) >= N'{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "bw":
                            item.Data = item.Data.Contains("%") ? item.Data : "%" + item.Data + "%";
                            str += String.Format("{0} {1} LIKE N'{2}' ", groupOp, item.Field, item.Data.Replace("[", "[[]"));
                            break;
                        case "bn":
                            str += String.Format("{0} CONVERT(nvarchar, {1}, 103) LIKE N'%{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "in":
                            str += String.Format("{0} {1} IN (N'{2}') ", groupOp, item.Field, item.Data.Replace(",", "','"));
                            break;
                        case "ni":
                            str += String.Format("{0} CONVERT(nvarchar, {1}, 103) NOT IN N'%{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "ew":
                            str += String.Format("{0} CONVERT(nvarchar, {1}, 103) LIKE N'_{2}' ", groupOp, item.Field, item.Data);
                            break;
                        case "en":
                            int count_en = item.Data.Split('-').Count();
                            string[] en_search = new string[count_en];
                            en_search = item.Data.Split('-');
                            if (en_search[0] == null | en_search[0] == "") { en_search[0] = "-9999999999"; }
                            if (en_search[1] == null | en_search[1] == "") { en_search[1] = "9999999999"; }
                            str += String.Format("{0} ({1} >= {2} AND {1} <= {3}) ", groupOp, item.Field, en_search[0], en_search[1]);
                            break;
                        case "cn":
                            int count_cn = item.Data.Split('&').Count();
                            string[] cn_search = new string[count_cn];
                            cn_search = item.Data.Split('&');

                            DateTime mindate = DateTime.MinValue.AddYears(1752);
                            DateTime maxdate = DateTime.MaxValue;
                            if (cn_search[0] != null && cn_search[0] != "") { mindate = VNN_Config.setDateTime(cn_search[0]); }
                            if (cn_search[1] != null && cn_search[1] != "") { maxdate = VNN_Config.setDateTime(cn_search[1]); }
                            str += String.Format("{0} {1}>='{3}' and {1}<='{4}' ", groupOp, item.Field, item.Data, mindate.ToString("yyyy-MM-dd"), maxdate.ToString("yyyy-MM-dd"));
                            break;
                        case "abi":
                            int count_abi = item.Data.Split('#').Count();
                            string[] abi_search = new string[count_abi];
                            abi_search = item.Data.Split('#');
                            string data_search = "(select Data from [dbo].[Split](" + item.Field + ",N'♣')";
                            for (int i_s = 0; i_s < count_abi - 1; i_s++)
                            {
                                string[] abi_search_child = new string[5];
                                abi_search_child = abi_search[i_s].Split(',');
                                int i_s_ = i_s + 1;
                                string data_search_ = data_search + " where stt = " + i_s_ + ")";
                                //--
                                if (abi_search_child[0] == "" | abi_search_child[0] == null) { abi_search_child[0] = "%"; }
                                else { abi_search_child[0] = "Language: " + abi_search_child[0] + ""; }
                                //--
                                if (abi_search_child[1] == "" | abi_search_child[1] == null) { abi_search_child[1] = "%"; }
                                else { abi_search_child[1] = "Speaking: [[]" + abi_search_child[1] + "]"; }
                                //--
                                if (abi_search_child[2] == "" | abi_search_child[2] == null) { abi_search_child[2] = "%"; }
                                else { abi_search_child[2] = "Listening: [[]" + abi_search_child[2] + "]"; }
                                //--
                                if (abi_search_child[3] == "" | abi_search_child[3] == null) { abi_search_child[3] = "%"; }
                                else { abi_search_child[3] = "Writing: [[]" + abi_search_child[3] + "]"; }
                                //--
                                if (abi_search_child[4] == "" | abi_search_child[4] == null) { abi_search_child[4] = "%"; }
                                else { abi_search_child[4] = "Reading: [[]" + abi_search_child[4] + "]"; }
                                //--
                                str += string.Format("{0} {1} LIKE N'%{2}%' ", groupOp, data_search_, abi_search_child[0]);
                                str += string.Format("{0} {1} LIKE N'%{2}%' ", groupOp, data_search_, abi_search_child[1]);
                                str += string.Format("{0} {1} LIKE N'%{2}%' ", groupOp, data_search_, abi_search_child[2]);
                                str += string.Format("{0} {1} LIKE N'%{2}%' ", groupOp, data_search_, abi_search_child[3]);
                                str += string.Format("{0} {1} LIKE N'%{2}%' ", groupOp, data_search_, abi_search_child[4]);
                            }
                            break;
                        default:
                            str += " LIKE N'{0}%' ";
                            break;
                    }
                }
            }
            return str;
        }

        public String ToScript()
        {
            var rulesPL = rules.Where(s => !s.Field.StartsWith("P.")).ToList();
            return Filter.ToScriptPL(rulesPL, groupOp, groups);
        }

        public String ToScriptP()
        {
            var rulesPL = rules.Where(s => s.Field.StartsWith("P.")).ToList();
            return Filter.ToScriptPL(rulesPL, groupOp, groups);
        }

        public static System.Collections.Generic.List<RuleFilter> CreateRules(IJsonType jsRules)
        {
            JsonArray arr = (JsonArray)jsRules;
            System.Collections.Generic.List<RuleFilter> lst = new System.Collections.Generic.List<RuleFilter>();
            foreach (IJsonType i in arr)
            {
                RuleFilter r = RuleFilter.CreateRuleFilter(i);
                lst.Add(r);
            }
            return lst;
        }

        public static Filter CreateFilter(String _filters)
        {
            JsonParser parser = new JsonParser(new System.IO.StringReader(_filters), true);
            JsonObject json = parser.ParseObject();
            Filter f = new Filter();

            foreach (System.Collections.Generic.KeyValuePair<string, IJsonType> pair in json)
            {
                String key = pair.Key;
                IJsonType value = pair.Value;
                switch (key)
                {
                    case "groupOp":
                        f.groupOp = pair.Value.ToString();
                        break;
                    case "rules":
                        f.rules = Filter.CreateRules(value);
                        break;
                    case "groups":
                        f.groups = pair.Value;
                        break;
                    default:
                        break;
                }
            }
            return f;
        }
    }
}