using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ASE.Common
{
	public static class Common
    {
        public static string GetWebConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        public static string GetCurrentThreadLanguage()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["_culture"];
            if (cookie == null)
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = Thread.CurrentThread.CurrentCulture.Name;
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
			if (cookie.Value != "en-US" && cookie.Value != "da-DK")
				cookie.Value = "en-US";
			return cookie.Value;


            //if (HttpContext.Current.Session["Culture"] != null)
            //{
            //    return (string)HttpContext.Current.Session["Culture"];
            //}
            //else
            //{
            //    HttpContext.Current.Session["Culture"] = Thread.CurrentThread.CurrentCulture.Name;
            //     return (string)HttpContext.Current.Session["Culture"];
            //}
            //return "";

        }

        public static string Id(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("id"))
                return (string)routeValues["id"];
            else if (HttpContext.Current.Request.QueryString.AllKeys.Contains("id"))
                return HttpContext.Current.Request.QueryString["id"];

            return string.Empty;
        }

        public static bool ValidAllowFileType(string FileName)
        {

            string FileTypes = GetWebConfigValue("AllowFileTypes").ToLower();
            string Extension = Path.GetExtension(FileName.ToLower()).Replace(".", "");
            if (string.IsNullOrEmpty(FileTypes))
            {
                return false;
            }
            if (FileTypes.Contains(Extension))
            {
                return true;
            }

            return false;
        }
        public static bool ValidateTableName(string entityName)
        {
            string AllowedNotificationEntities = GetWebConfigValue("AllowedNotificationEntities").ToLower();
            if (string.IsNullOrEmpty(AllowedNotificationEntities))
            {
                return false;
            }
            if (AllowedNotificationEntities.ToLower().Contains(entityName.ToLower()))
            {
                return true;
            }
            return false;
        }
        public static int GetMaximumFileSize()
        {
            return int.Parse(GetWebConfigValue("MaximumFileSize"));
        }

        public static int MaximumUserLogoFileSize()
        {
            return int.Parse(GetWebConfigValue("MaximumUserLogoFileSize"));
        }

        public static string CreateCSVText<T>(List<T> data, List<string> columnsToInclude, string seperator)
        {
            var result = new StringBuilder();
            var properties = typeof(T).GetProperties();
            string line;
            List<string> colNames = new List<string>();
            foreach (var prop in properties)
            {
                foreach (var item in columnsToInclude)
                {
                    if (item.ToUpper() == prop.Name.ToUpper())
                        colNames.Add(prop.Name);
                }
            }
            if (colNames.Count > 0)
            {
                line = string.Join(seperator, colNames);
                result.AppendLine(line);

                foreach (var row in data)
                {
                    //var values = properties.Select(p => p.GetValue(row, null))
                    //                       .Select(v => StringToCSVCell(Convert.ToString(v)));

                    List<string> values2 = new List<string>();
                    foreach (var col in colNames)
                    {
                        values2.Add(StringToCSVCell(string.IsNullOrEmpty(properties.Where(p => p.Name == col).FirstOrDefault().GetValue(row).ToString()) ? " " : properties.Where(p => p.Name == col).FirstOrDefault().GetValue(row).ToString()));
                    }

                    line = string.Join(seperator, values2);
                    result.AppendLine(line);
                }
            }
            return result.ToString();
        }
        private static string StringToCSVCell(string str)
        {
            bool mustQuote = (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"));
            if (mustQuote)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in str)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }
            return str;
        }
    }
}
