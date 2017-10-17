using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASE.BL;
using ASE.UI;
using ASE.Entities;

namespace ASE
{
    [RequireHttps]
    public class HomeController : BaseController<object>
	{
        public ActionResult Index()
        {
			if (Request.IsAuthenticated)
			{
				var _manager = new StaffManager();
				var staff = _manager.GetStaffByUserIDEntities(UserID);
				if (staff.Count == 0)
					return RedirectToAction("Index", "Browse");
				return RedirectToAction("Index", "Staff");
			}
            return View("Landing");
        }

    }
}
