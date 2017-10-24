using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASE.Entities;
using ASE.BL;
using ASE.UI;
using System.IO;

namespace CareerPanorama
{
	[Authorize]
    public class StaffController : ASE.BaseController<object>
	{
        // GET: Staff
        public ActionResult Index()
        {
			Guid id = UIHelper.GetUserIDByEmail(User.Identity.Name);
			if (!IsAuthorized())
				return View("Error");			
			var stallmanager = new StallManager();
			List<StallEntity> list = stallmanager.GetStallByUserIDEntities(id);
			return View("StallSelection", list);
        }

		public ActionResult Stall(Guid StallId)
		{
			if (!IsAuthorized())
				return View("Error");
			return View("StallOrders", StallId);
		}

		public ActionResult RemoveOrder(Guid ID)
		{
			if (!IsAuthorized())
				return View("Error");
			var _manager = new OrderManager();
			var stall = _manager.GetOrderByIDObject(ID);
			if (stall != null)
			{
				var stallid = _manager.GetOrderByIDObject(ID).Fields.StallID;
				_manager.DeleteOrderByID(ID);
				return RedirectToAction("Stall", new { StallID = stallid });
			}
			return RedirectToAction("Index");
		}

		public PartialViewResult RefreshOrders(Guid StallID)
		{
			return PartialView("~/Views/Staff/_OrderList.cshtml", StallID);
		}

		public ActionResult CreateStall()
		{
			if (!IsAuthorized())
				return View("Error");
			return View();
		}

		[HttpPost]
		public ActionResult CreateStall(string Name, string Description, HttpPostedFileBase ImageData)
		{
			if (!IsAuthorized())
				return View("Error");
			Stall obj = new Stall();
			obj.Fields.Name = Name;
			obj.Fields.Description = Description;
			obj.Fields.UserID = UserID;
			var type = ImageData.ContentType;
			obj.Fields.ContentType = type;
			MemoryStream target = new MemoryStream();
			ImageData.InputStream.CopyTo(target);
			obj.Fields.Logo = target.ToArray();

			if (obj.Fields.Logo.Length > 1000000 || (type != "image/x-png" && type != "image/gif" && type != "image/jpeg"))
			{
				return RedirectToAction("CreateStall");
			}
			obj.Save();
			return RedirectToAction("Index");
		}

		public ActionResult Dishes(Guid StallId)
		{
			if (!IsAuthorized())
				return View("Error");
			var _manager = new DishManager();
			var list = _manager.GetDishByStallIDEntities(StallId);
			ViewData["StallId"] = StallId;
			return View("DishSelection", list);
		}

		public ActionResult CreateDish(Guid StallId)
		{
			if (!IsAuthorized())
				return View("Error");
			ViewData["StallId"] = StallId;
			return View("CreateDish");
		}

		[HttpPost]
		public ActionResult SubmitDish(Dish dish, HttpPostedFileBase ImageData)
		{
			if (!IsAuthorized())
				return View("Error");
			var type = ImageData.ContentType;
			dish.Fields.ContentType = type;
			MemoryStream target = new MemoryStream();
			ImageData.InputStream.CopyTo(target);
			dish.Fields.Photo = target.ToArray();

			if (dish.Fields.Photo.Length > 1000000 || (type != "image/x-png" && type != "image/gif" && type != "image/jpeg"))
			{
				return RedirectToAction("CreateDish", new { StallId = dish.Fields.StallID });
			}
			dish.Save();
			return RedirectToAction("Dishes", new { StallId = dish.Fields.StallID });
		}

		public ActionResult RemoveDish(Guid DishId)
		{
			if (!IsAuthorized())
				return View("Error");
			var _manager = new DishManager();
			var dish = _manager.GetDishByIDObject(DishId);
			if (dish != null)
			{
				_manager.DeleteDishByID(DishId);
				var _ordermanager = new OrderManager();
				var orders = _ordermanager.GetOrderByStallIDObjects(dish.Fields.StallID);
				foreach (var item in orders)
				{
					if (item.Fields.DishID == DishId)
						_ordermanager.DeleteOrderByID(item.Fields.ID.Value);
				}
			}
			return RedirectToAction("Index");
		}

		public ActionResult Users()
		{
			if (!IsAuthorized())
				return View("Error");
			var _manager = new StaffManager();
			var staff = _manager.GetAllStaffEntities();
			return View("Users", staff);
		}

		public ActionResult AddStaffByEmail(string Email)
		{
			if (!IsAuthorized())
				return View("Error");
			var _manager = new UserManager();
			var userid = _manager.GetUserIdByEmail(Email);
			if (userid != null && userid != Guid.Empty)
			{
				var obj = new Staff(Email);
				obj.Save();
			}
			return RedirectToAction("Users");
		}

		public ActionResult RemoveStaff(Guid StaffID)
		{
			if (!IsAuthorized())
				return View("Error");
			var _manager = new StaffManager();
			_manager.DeleteStaffByID(StaffID);
			return RedirectToAction("Users");
		}

		#region Helper Functions

		public bool IsAuthorized()
		{
			Guid id = UIHelper.GetUserIDByEmail(User.Identity.Name);
			var staffmanager = new StaffManager();
			List<StaffEntity> staff = staffmanager.GetStaffByUserIDEntities(id);
			if (staff.Count == 0)
				return false;
			return true;
		}

		#endregion

    }
}