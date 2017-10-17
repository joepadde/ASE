using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using ASE.BL;
using ASE.Common;
using ASE.Entities;

namespace ASE
{
    public class BaseController<T> : Controller where T : new()
    {
        private T _dataModel;
		private UserManager _usermanager = new UserManager();

        //private ApplicationUserManager _userManager;
        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //     set
        //    {
        //        _userManager = value;
        //    }
        //}

        public string UserName
        {
            get
            {
                return User.Identity.Name;
            }

        }

		public Guid UserID
		{
			get
			{
				return _usermanager.GetUserIdByEmail(UserName);
			}
		}

		//}
		//public Guid UserExtendedID
		//{
		//    get
		//    {
		//        if (!string.IsNullOrEmpty(UserASPID))
		//            return BL.User.Manager.GetUserByUserASPIDObject(UserASPID).Fields.ID.Value;

			//        return Guid.Empty;
			//    }

			//}

		public string CurrentLanguage
        {
            get
            {
                return Common.Common.GetCurrentThreadLanguage();
            }

        }
        public T DataModel
        {
            get
            {
                return _dataModel;
            }
            set
            {
                _dataModel = value;
            }
        }


        //protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        //{
        //    if (Request.IsAuthenticated)
        //    {
        //        string currentURL = "";
        //        bool flag = false;
        //        if (!User.IsInRole("SuperAdmin"))
        //        {
        //            currentURL = UIHelper.Controller() + "/" + UIHelper.Action();// System.Web.HttpContext.Current.Request.Url.AbsolutePath;//HttpContext.Request.Url.AbsolutePath;
        //            List<URLConfigurationEntity> URLConfigList = BL.BetterCruitURLConfiguration.Manager.GetURLConfigurationPagedListEntities(CurrentLanguage).Where(x => x.URL.ToUpper() == currentURL.ToUpper()).ToList();
        //            if (URLConfigList.Count > 0)
        //            {
        //                foreach (URLConfigurationEntity URLConfig in URLConfigList)
        //                {

        //                    if (UserExtendedID == URLConfig.FK_UserID || User.IsInRole(URLConfig.RoleName) || Subscription.Manager.IsURLAccessedByUser(UserExtendedID, currentURL).FirstOrDefault() != null)
        //                    {
        //                        flag = true;
        //                        break;
        //                    }
        //                }
        //                if (!flag)
        //                {
        //                    ViewBag.errorMessage = UIHelper.Localize("NotAuthorized");
        //                    Response.Redirect("/Error", true);
        //                    // RedirectToAction("Index","Error");
        //                }
        //            }
        //            //else no special URL config so URL is public "can be accessed"
        //        }
        //        //else access any URL
        //    }
        //    string cultureName = null;

        //    // Attempt to read the culture cookie from Request
        //    HttpCookie cultureCookie = Request.Cookies["_culture"];
        //    if (cultureCookie != null)
        //        cultureName = cultureCookie.Value;
        //    //else
        //    //{
        //    //    if (Session["Culture"] != null)
        //    //    {
        //    //        cultureName = (string)Session["Culture"];
        //    //    }
        //    //}

        //    if (string.IsNullOrEmpty(cultureName))
        //        cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

        //    // Validate culture name
        //    cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


        //    // Modify current thread's cultures            
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
        //    Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

        //    MapCommonFieldsFormPOST(System.Web.HttpContext.Current);

        //    return base.BeginExecuteCore(callback, state);
        //}

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    Log.LogError(filterContext.Exception.Message, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name, System.Reflection.MethodBase.GetCurrentMethod().Name, filterContext.Exception);
        //    //if (filterContext.HttpContext.IsCustomErrorEnabled)
        //    //{
        //    //    filterContext.ExceptionHandled = true;
        //    //    //ViewBag.errorMessage = filterContext.Exception.ToString();
        //    //    //this.View("Error").ExecuteResult(this.ControllerContext);
        //    //}
        //    base.OnException(filterContext);
        //}

        //ApplicationDbContext context = new ApplicationDbContext();
        //static bool IsVisible(string RoleName)
        //{
        //    var account = new AccountController();
        //    ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

        //    if (account.UserManager.IsInRole(user.Id, RoleName))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        #region Helper Functions

        private void MapCommonFieldsFormPOST(HttpContext ObjHttpContext)
        {

            if (ObjHttpContext.Request.Form != null && ObjHttpContext.Request.HttpMethod == "POST")
            {

                string[] AllKeys = ObjHttpContext.Request.Form.AllKeys;
                //Set ID From current logined user
                if (Array.IndexOf(AllKeys, "ID") > -1)
                {
                    NameValueCollection ObjForm = ObjHttpContext.Request.Form;
                    ObjForm = (NameValueCollection)ObjHttpContext.Request.GetType().GetField("_form", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(ObjHttpContext.Request);
                    PropertyInfo ObjIsReadOnly = ObjForm.GetType().GetProperty("IsReadOnly", BindingFlags.NonPublic | BindingFlags.Instance);
                    ObjIsReadOnly.SetValue(ObjForm, false, null);
                    Guid? ID = (!string.IsNullOrEmpty(ObjForm["ID"])) ? (Guid?)Guid.Parse(ObjForm["ID"]) : null;
                    if (ID == null || ID == default(Guid))
                    {
                        if (Array.IndexOf(AllKeys, "CreatedBy") > -1)
                        {
                            ObjForm["CreatedBy"] = User.Identity.Name;
                            //ObjForm["CreatedBy"] = UserExtendedID.ToString();
                        }
                    }
                    ObjForm["ModifiedBy"] = User.Identity.Name;
                    //ObjForm["ModifiedBy"] = UserExtendedID.ToString();
                    ObjForm["ModifiedDate"] = DateTime.Now.ToString();

                    // maping for related objects like "ObjectName.ModifiedBy"

                    string RelatedObjectID = ObjForm.AllKeys.FirstOrDefault(key => key.Contains(".ID"));
                    if (!string.IsNullOrEmpty(RelatedObjectID) && RelatedObjectID.Contains("."))
                    {
                        string RelatedObjectName = RelatedObjectID.Substring(0, RelatedObjectID.IndexOf("."));
                        ID = (!string.IsNullOrEmpty(ObjForm[RelatedObjectName + "." + "ID"])) ? (Guid?)Guid.Parse(ObjForm[RelatedObjectName + "." + "ID"]) : null;
                        if (ID == null || ID == default(Guid))
                        {
                            if (Array.IndexOf(AllKeys, RelatedObjectName + "." + "CreatedBy") > -1)
                            {
                                //ObjForm[RelatedObjectName + "." + "CreatedBy"] = UserExtendedID.ToString();
                                ObjForm[RelatedObjectName + "." + "CreatedBy"] = User.Identity.Name;
                            }
                        }
                        ObjForm[RelatedObjectName + "." + "ModifiedBy"] = User.Identity.Name;
                        //ObjForm[RelatedObjectName + "." + "ModifiedBy"] = UserExtendedID.ToString();
                        ObjForm[RelatedObjectName + "." + "ModifiedDate"] = DateTime.Now.ToString();
                    }
                    ObjIsReadOnly.SetValue(ObjForm, true, null);
                }
            }
        }
        public void MapCommonFields(BaseEntity ObjBaseEntity)
        {
        }
        #endregion
    }
}
