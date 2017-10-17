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
    public class ErrorController : BaseController<object>
    {
        public ActionResult Index()
        {
            return View("Error");
        }
    }
}