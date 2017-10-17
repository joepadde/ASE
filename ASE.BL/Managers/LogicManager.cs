using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASE.BL;

namespace ASE.BL
{
    public static class LogicManager
    {
        private static readonly object padlock = new object();

        #region Lookup
        private static LookupLogic _objLookupLogic = null;
        internal static LookupLogic ObjLookupLogic
        {
            get
            {
                lock (padlock)
                {
                    if (HttpContext.Current != null)
                    {
                        if (HttpContext.Current.Cache["LookupLogic"] == null)
                        {
                            _objLookupLogic = new LookupLogic();
                            HttpContext.Current.Cache["LookupLogic"] = _objLookupLogic;
                        }
                        else
                            _objLookupLogic = HttpContext.Current.Cache["LookupLogic"] as LookupLogic;
                        return _objLookupLogic;
                    }
                    else
                    {
                        _objLookupLogic = new LookupLogic();
                        return _objLookupLogic;

                    }
                }
            }

        }
        #endregion

        #region Timeline
        private static TimelineLogic _objTimelineLogic = null;
        internal static TimelineLogic ObjTimelineLogic
        {
            get
            {
                lock (padlock)
                {
                    if (HttpContext.Current != null)
                    {
                        if (HttpContext.Current.Cache["TimelineLogic"] == null)
                        {
                            _objTimelineLogic = new TimelineLogic();
                            HttpContext.Current.Cache["TimelineLogic"] = _objTimelineLogic;
                        }
                        else
                            _objTimelineLogic = HttpContext.Current.Cache["TimelineLogic"] as TimelineLogic;
                        return _objTimelineLogic;
                    }
                    else
                    {
                        _objTimelineLogic = new TimelineLogic();
                        return _objTimelineLogic;
                    }
                }
            }
        }
		#endregion

		#region Stall
		private static StallLogic _objStallLogic = null;
		internal static StallLogic ObjStallLogic
		{
			get
			{
				lock (padlock)
				{
					if (HttpContext.Current != null)
					{
						if (HttpContext.Current.Cache["StallLogic"] == null)
						{
							_objStallLogic = new StallLogic();
							HttpContext.Current.Cache["StallLogic"] = _objStallLogic;
						}
						else
							_objStallLogic = HttpContext.Current.Cache["StallLogic"] as StallLogic;
						return _objStallLogic;
					}
					else
					{
						_objStallLogic = new StallLogic();
						return _objStallLogic;
					}
				}
			}
		}
		#endregion

		#region Dish
		private static DishLogic _objDishLogic = null;
		internal static DishLogic ObjDishLogic
		{
			get
			{
				lock (padlock)
				{
					if (HttpContext.Current != null)
					{
						if (HttpContext.Current.Cache["DishLogic"] == null)
						{
							_objDishLogic = new DishLogic();
							HttpContext.Current.Cache["DishLogic"] = _objDishLogic;
						}
						else
							_objDishLogic = HttpContext.Current.Cache["DishLogic"] as DishLogic;
						return _objDishLogic;
					}
					else
					{
						_objDishLogic = new DishLogic();
						return _objDishLogic;
					}
				}
			}
		}
		#endregion

		#region Staff
		private static StaffLogic _objStaffLogic = null;
		internal static StaffLogic ObjStaffLogic
		{
			get
			{
				lock (padlock)
				{
					if (HttpContext.Current != null)
					{
						if (HttpContext.Current.Cache["StaffLogic"] == null)
						{
							_objStaffLogic = new StaffLogic();
							HttpContext.Current.Cache["StaffLogic"] = _objStaffLogic;
						}
						else
							_objStaffLogic = HttpContext.Current.Cache["StaffLogic"] as StaffLogic;
						return _objStaffLogic;
					}
					else
					{
						_objStaffLogic = new StaffLogic();
						return _objStaffLogic;
					}
				}
			}
		}
		#endregion

		#region Order
		private static OrderLogic _objOrderLogic = null;
		internal static OrderLogic ObjOrderLogic
		{
			get
			{
				lock (padlock)
				{
					if (HttpContext.Current != null)
					{
						if (HttpContext.Current.Cache["OrderLogic"] == null)
						{
							_objOrderLogic = new OrderLogic();
							HttpContext.Current.Cache["OrderLogic"] = _objOrderLogic;
						}
						else
							_objOrderLogic = HttpContext.Current.Cache["OrderLogic"] as OrderLogic;
						return _objOrderLogic;
					}
					else
					{
						_objOrderLogic = new OrderLogic();
						return _objOrderLogic;
					}
				}
			}
		}
		#endregion


	}
}
