using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASE.Entities;
using ASE.BL;
using System.IO;

namespace CareerPanorama
{
    public class StaffController : ASE.BaseController<object>
	{
        // GET: Staff
        public ActionResult Index()
        {
			Guid id = Guid.Parse("97c11d5a-9ec0-4b91-9dbe-dc8520d307c7");
			var staffmanager = new StaffManager();
			List<StaffEntity> staff = staffmanager.GetStaffByUserIDEntities(id);
			if (staff.Count == 0)
				return View("Error");
					
			var stallmanager = new StallManager();
			List<StallEntity> list = stallmanager.GetStallByUserIDEntities(id);
			return View("StallSelection", list);
        }

		public ActionResult Stall(Guid StallId)
		{
			return View("StallOrders", StallId);
		}

		public ActionResult RemoveOrder(Guid ID)
		{	
			var _manager = new OrderManager();
			var stallid = _manager.GetOrderByIDObject(ID).Fields.StallID;
			_manager.DeleteOrderByID(ID);
			return RedirectToAction("Stall", new { StallID = stallid });
		}

		public PartialViewResult RefreshOrders(Guid StallID)
		{
			return PartialView("~/Views/Staff/_OrderList.cshtml", StallID);
		}

		public ActionResult CreateStall()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreateStall(string Name, string Description, HttpPostedFileBase Logo)
		{
			Stall obj = new Stall();
			obj.Fields.Name = Name;
			obj.Fields.Description = Description;
			obj.Fields.UserID = UserID;
			//using (var binaryReader = new BinaryReader(Logo.InputStream))
			//	obj.Fields.Logo = binaryReader.ReadBytes(Logo.ContentLength);
			obj.Save();
			return RedirectToAction("Index");
		}

		public ActionResult Dishes(Guid StallId)
		{
			var _manager = new DishManager();
			var list = _manager.GetDishByStallIDEntities(StallId);
			ViewData["StallId"] = StallId;
			return View("DishSelection", list);
		}

		public ActionResult CreateDish(Guid StallId)
		{
			ViewData["StallId"] = StallId;
			return View("CreateDish");
		}

		//string Name, string Description, int Price, bool OutOfOrder, HttpPostedFile Photo, Guid StallId

		[HttpPost]
		public ActionResult SubmitDish(Dish dish)
		{
			//using (var binaryReader = new BinaryReader(Logo.InputStream))
			//	obj.Fields.Logo = binaryReader.ReadBytes(Logo.ContentLength);
			dish.Save();
			return RedirectToAction("Dishes", new { StallId = dish.Fields.StallID });
		}

		public ActionResult RemoveDish(Guid DishId)
		{
			var _manager = new DishManager();
			_manager.DeleteDishByID(DishId);
			return RedirectToAction("Index");
		}

		public ActionResult Users()
		{
			var _manager = new StaffManager();
			var staff = _manager.GetAllStaffEntities();
			return View("Users", staff);
		}

		public ActionResult AddStaffByEmail(string Email)
		{
			var _manager = new UserManager();
			if (_manager.GetUserIdByEmail(Email) != null)
			{
				var obj = new Staff(Email);
				obj.Save();
			}
			return RedirectToAction("Users");
		}

		public ActionResult RemoveStaff(Guid StaffID)
		{
			var _manager = new StaffManager();
			_manager.DeleteStaffByID(StaffID);
			return RedirectToAction("Users");
		}

    }
}