using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Collections;
using System.Web.Routing;
using ASE.BL;

namespace ASE.UI
{
    public static class UIHelper
    {

		public static Guid GetUserIDByEmail(string Email)
		{
			var _usermanager = new UserManager();
			return _usermanager.GetUserIdByEmail(Email);
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

        public static string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
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

        public static string Controller()
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        public static string Action()
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }
    }
}