using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASE.BL;
using ASE.Entities;
using ASE.UI;

namespace ASE.Controllers
{
    public class AdminController : BaseController<object>
    {
        void GetLists()
        {
            //var Languageslist = Lookup.Manager.GetAllLookupsEntities(UIHelper.GetCurrentThreadLanguage(), TableName.Lookup_Language.ToString());
            var Languageslist = Lookup.Manager.GetAllLookupsEntities(UIHelper.GetCurrentThreadLanguage(), "");
            ViewBag.Languages = Languageslist.Select(rr => new SelectListItem { Value = rr.ID.ToString(), Text = rr.Value }).ToList();
            ViewBag.Languages2 = Languageslist.Select(rr => new SelectListItem { Value = rr.Key.ToString(), Text = rr.Value }).ToList();
        }
    }
}
