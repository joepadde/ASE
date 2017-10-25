using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASE.BL;
using ASE.Entities;

namespace ASE
{
	public class BrowseController : BaseController<object>
	{
		// GET: Browse
		public ActionResult Index()
		{
			return RedirectToAction("Stalls");
		}

		public ActionResult Stalls()
		{
			var _manager = new StallManager();
			var _dishmanager = new DishManager();
			List<StallEntity> stalls = _manager.GetAllStallEntities();
			var list = new List<StallEntity>();
			foreach (var stall in stalls)
			{
				var dishes = _dishmanager.GetDishByStallIDEntities(stall.ID.Value);
				if (dishes.Count != 0)
					list.Add(stall);
			}
			return View("BrowseStalls", list);
		}

		public ActionResult Dishes(Guid StallId)
		{
			var manager = new DishManager();
			List<DishEntity> dishes = manager.GetDishByStallIDEntities(StallId);
			return View("BrowseDishes", dishes);
		}

		public PartialViewResult OrderDish(Guid StallId, Guid DishId, Guid UserId)
		{
			Order order = new Order(StallId, DishId, UserId);
			order.Fields.OrderedTime = DateTime.Now;
			order.Fields.PickupTime = DateTime.Now;
			order.Save();
			return PartialView("~/Views/Browse/_ItemList.cshtml");
		}

		public PartialViewResult RefreshList()
		{
			return PartialView("~/Views/Browse/_ItemList.cshtml");
		}

		public PartialViewResult GetPickupTimePopup()
		{
			return PartialView("~/Views/Browse/_PickupTime.cshtml");
		}

		public PartialViewResult ClearBasket()
		{
			var _ordermanager = new OrderManager();
			_ordermanager.ClearBasket(UserID);
			return PartialView("~/Views/Browse/_ItemList.cshtml");
		}

		[HttpPost]
		public ActionResult CompleteOrder(int minutes = 5)
		{
			var _ordermanager = new OrderManager();
			List<Order> list = _ordermanager.GetOrderByUserIDObjects(UserID);
			foreach(var item in list)
			{
				if(item.Fields.Status == null)
				{
					item.Fields.OrderedTime = DateTime.Now;
					item.Fields.PickupTime = DateTime.Now.AddMinutes(minutes);
					item.Fields.Status = false;
					item.Save();
				}
			}
			return RedirectToAction("OrderSummary");
		}

		public ActionResult OrderSummary()
		{
			return View();
		}

	}
}