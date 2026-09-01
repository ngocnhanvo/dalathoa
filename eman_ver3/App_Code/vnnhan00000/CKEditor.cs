using DataAcess;
using System;
using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// Summary description for Helper
/// </summary>
namespace vnnhan00000 {
    public class CKEditor
    {
        public HtmlAgilityPack.HtmlDocument doc = null;
        public CKEditor()
        {
            doc = new HtmlAgilityPack.HtmlDocument();
        }
        
        public List<Dictionary<string, string>> convertJsonStringToFile(string json, string folder, string name)
        {
            var files = new List<Dictionary<string, string>>();
            if (!(json.StartsWith("[{") & json.EndsWith("}]")))
            {
                return files;
            }

            var jsonFiles = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
            foreach (var jsonFile in jsonFiles)
            {
                string id = jsonFile["id"];
                string data = jsonFile["data"];

                var jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
                string mimeType = jsonData["name"];
                var indexMimeType = mimeType.LastIndexOf(".");
                mimeType = indexMimeType <= -1 ? "" : mimeType.Substring(indexMimeType).ToLower();
                var acceptMime = new string[] { ".png", ".jpg", ".doc", ".docx", ".xls", ".xlsx", ".pdf" };
                if (!acceptMime.Contains(mimeType))
                    throw new FormatException(string.Format(@"Chỉ chấp nhận hình ảnh có định dạng sau: {0}", string.Join(", ", acceptMime)));

                string fileBase64 = jsonData["data"];
                var indexBase64 = fileBase64.IndexOf(",");
                fileBase64 = indexBase64 <= -1 ? "" : fileBase64.Substring(indexBase64 + 1);

                string path = "";
                if (!string.IsNullOrWhiteSpace(mimeType))
                {
                    var bytes = Convert.FromBase64String(fileBase64);
                    var folderPath = ExcuteSignalRStatic.mapPathSignalR("~/" + folder);
                    if (!System.IO.Directory.Exists(folderPath))
                        System.IO.Directory.CreateDirectory(folderPath);

                    path = folderPath + "/" + id + mimeType;
                    using (var file = new System.IO.FileStream(path, System.IO.FileMode.Create))
                    {
                        file.Write(bytes, 0, bytes.Length);
                        file.Flush();
                        file.Close();
                        file.Dispose();
                        files.Add(new Dictionary<string, string>() {
                            { "id", id },
                            { "link", folder + "/" + id + mimeType }
                        });
                    }
                }
            }
            return files;
        }

        public List<string> removeFileNotInCkEditor(string folder, string escapeLink, string noidung)
        {
            var filesLost = new List<string>();
            var pathFolder = ExcuteSignalRStatic.mapPathSignalR("~/" + folder);
            if (!System.IO.Directory.Exists(pathFolder))
                return filesLost;

            doc.LoadHtml(noidung);
            filesLost = new System.IO.DirectoryInfo(pathFolder).GetFiles("*").Select(s => string.Format(@"{0}/{1}", escapeLink + folder, s.Name)).ToList();
            foreach (var link in doc.DocumentNode.Descendants("a").Where(s => !string.IsNullOrEmpty(s.Attributes["file"].Value)))
            {
                var attrFile = link.Attributes["file"].Value;
                if (filesLost.Contains(attrFile))
                    filesLost.Remove(attrFile);
            }

            foreach (var link in filesLost)
            {
                Helper.removeFileWithPath(ExcuteSignalRStatic.mapPathSignalR("~/" + link.Substring(3)));
            }

            return filesLost;
        }

        public string setContentAfterAttach(HttpContext context, List<Dictionary<string, string>> files)
        {
            string noidung = VNN_VariablePublic.DecodeHTML(context.Request.Form["noidung"]);
            foreach (var file in files)
            {
                noidung = noidung.Replace(file["id"], "../" + file["link"]);
            }
            return noidung;
        }
    }
}