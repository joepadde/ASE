using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Web.UI;
using System.Web.Security;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;

namespace ASE.Framework
{
    public class Helper {

        public static void InvokeMethod(object instance, string methodName, object[] _Params)
        {
            //Getting the method information using the method info class            
            MethodInfo mi = instance.GetType().GetMethod(methodName);
            //invoing the method            
            //null- no parameter for the function [or] we can pass the array of parameters            
            mi.Invoke(instance, _Params);
        }

        public static string NumberToCurrencyText(decimal number, MidpointRounding midpointRounding)
        {
            // Round the value just in case the decimal value is longer than two digits
            number = decimal.Round(number, 2, midpointRounding);

            string wordNumber = string.Empty;

            // Divide the number into the whole and fractional part strings
            string[] arrNumber = number.ToString().Split('.');

            // Get the whole number text
            long wholePart = long.Parse(arrNumber[0]);
            string strWholePart = NumberToText(wholePart);

            // For amounts of zero dollars show 'No Dollars...' instead of 'Zero Dollars...'
            wordNumber = (wholePart == 0 ? "zero" : strWholePart) + (wholePart == 1 ? " Dollar and " : " Dollars and ");

            // If the array has more than one element then there is a fractional part otherwise there isn't
            // just add 'No Cents' to the end
            if (arrNumber.Length > 1)
            {
                // If the length of the fractional element is only 1, add a 0 so that the text returned isn't,
                // 'One', 'Two', etc but 'Ten', 'Twenty', etc.
                long fractionPart = long.Parse((arrNumber[1].Length == 1 ? arrNumber[1] + "0" : arrNumber[1]));
                string strFarctionPart = NumberToText(fractionPart);

                wordNumber += (fractionPart == 0 ? " zero" : strFarctionPart) + (fractionPart == 1 ? " Cent" : " Cents");
            }
            else
                wordNumber += "zero Cents";

            return wordNumber;
        }

        public static string NumberToCurrencyTextCheque(decimal number, MidpointRounding midpointRounding)
        {
            // Round the value just in case the decimal value is longer than two digits
            number = decimal.Round(number, 2, midpointRounding);

            string wordNumber = string.Empty;

            // Divide the number into the whole and fractional part strings
            string[] arrNumber = number.ToString().Split('.');

            // Get the whole number text
            long wholePart = long.Parse(arrNumber[0]);
            string strWholePart = NumberToText(wholePart);

            // For amounts of zero dollars show 'No Dollars...' instead of 'Zero Dollars...'
            wordNumber = (wholePart == 0 ? "zero" : strWholePart) + (wholePart == 1 ? "" : " and ");

            // If the array has more than one element then there is a fractional part otherwise there isn't
            // just add 'No Cents' to the end
            if (arrNumber.Length > 1)
            {
                // If the length of the fractional element is only 1, add a 0 so that the text returned isn't,
                // 'One', 'Two', etc but 'Ten', 'Twenty', etc.
                long fractionPart = long.Parse((arrNumber[1].Length == 1 ? arrNumber[1] + "0" : arrNumber[1]));

                wordNumber += "0." + fractionPart.ToString();
            }
            else
                wordNumber += "0";

            return wordNumber + " Only";
        }

        public static string NumberToText(long number)
        {
            StringBuilder wordNumber = new StringBuilder();

            string[] powers = new string[] { "Thousand ", "Million ", "Billion " };
            string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                                   "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            if (number == 0) { return "Zero"; }
            if (number < 0)
            {
                wordNumber.Append("Negative ");
                number = -number;
            }

            long[] groupedNumber = new long[] { 0, 0, 0, 0, 0 };
            int groupIndex = 0;

            while (number > 0)
            {
                groupedNumber[groupIndex++] = number % 1000;
                number /= 1000;
            }

            for (int i = 3; i >= 0; i--)
            {
                long group = groupedNumber[i];

                if (group >= 100)
                {
                    wordNumber.Append(ones[group / 100 - 1] + " Hundred ");
                    group %= 100;

                    if (group == 0 && i > 0)
                        wordNumber.Append(powers[i - 1]);
                }

                if (group >= 20)
                {
                    if ((group % 10) != 0)
                        wordNumber.Append(tens[group / 10 - 2] + " " + ones[group % 10 - 1] + " ");
                    else
                        wordNumber.Append(tens[group / 10 - 2] + " ");
                }
                else if (group > 0)
                    wordNumber.Append(ones[group - 1] + " ");

                if (group != 0 && i > 0)
                    wordNumber.Append(powers[i - 1]);
            }

            return wordNumber.ToString().Trim();
        }

        public static object GetFieldValue(object entity, string prop)
        {
            object val = null;
            PropertyInfo propertyInfo = entity.GetType().GetProperty(prop);
            if (propertyInfo != null)
            {
                val = propertyInfo.GetValue(entity, null);
            }

            return val;
        }

        public static void SetFieldValue(object entity, string prop, object value)
        {
            try
            {
                PropertyInfo property = entity.GetType().GetProperty(prop);
                if (property != null)
                {
                    Type t = Nullable.GetUnderlyingType(property.PropertyType)
                 ?? property.PropertyType;

                    object safeValue = null;

                    if (t == typeof(Guid) && value.GetType() == typeof(string))
                    {
                        safeValue = (property == null) ? null

                                                           : Convert.ChangeType(new Guid(value.ToString()), t);
                    }

                    else
                    {
                        safeValue = (property == null) ? null

                                                           : Convert.ChangeType(value, t);
                    }

                    property.SetValue(entity, safeValue, null);
                }
            }
            catch (Exception)
            {

            }
        }

        public static bool HasArabicChars(string str)
        {
            if (str == null)
            {
                return false;
            }

            foreach (char character in str.ToCharArray())
            {
                if (character >= 0x600 && character <= 0x6ff)
                    return true;
                if (character >= 0x750 && character <= 0x77f)
                    return true;
                if (character >= 0xfb50 && character <= 0xfc3f)
                    return true;
                if (character >= 0xfe70 && character <= 0xfefc)
                    return true;
            }
            return false;

        }

        //public static void GetControlList(Control Parent, List<Control> resultCollection)
        //{
        //    foreach (Control control in Parent.Controls)
        //    {
        //        if (IsMappingControl(control))
        //            resultCollection.Add(control);

        //        if (control.HasControls())
        //            GetControlList(control, resultCollection);
        //    }
        //}
        //public static void GetAllControlList(Control Parent, List<Control> resultCollection)
        //{
        //    foreach (Control control in Parent.Controls)
        //    {
        //        if (IsExtendControl(control))
        //            resultCollection.Add(control);                
        //        if (control.HasControls())
        //            GetAllControlList(control, resultCollection);
        //    }
        //}

        //public static bool IsMappingControl(Control Cont)
        //{
        //    if (Cont is ExtTextBox || Cont is ExtCheckBox || Cont is ExtDropDownList || Cont is ExtCalendar)
        //    {
        //        return true;
        //    }

        //    return false;
        //}
        //public static bool IsExtendControl(Control Cont)
        //{
        //    if (Cont is ExtTextBox || Cont is ExtCheckBox || Cont is ExtDropDownList || Cont is ExtCalendar
        //     || Cont is ExtCalendarExtender || Cont is ExtFileUploadExtender || Cont is ExtCheckBoxList || Cont is ExtDropDownList
        //     || Cont is ExtGridView || Cont is ExtLabel || Cont is ExtListBox || Cont is ExtRadioButtonList || Cont is ExtButton)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public static Guid? GetLogedInUserID()
        {
            object objUser = HttpContext.Current.Session[PublicKeys.SESSION_LOGED_IN_USER_KEY];
            if (objUser == null)
            {
                return null;
            }

            PropertyInfo _fields = objUser.GetType().GetProperty("Fields");
            if (_fields == null)
            {
                return null;
            }

            Guid ID = new Guid(Helper.GetFieldValue(_fields.GetValue(objUser, null), "ID").ToString());
            if (ID == default(Guid))
            {
                return null;
            }

            return ID;
        }

        public static void PopulateListControlFromEnum(ListControl drp, Type enm)
        {
            try
            {
                var names = Enum.GetNames(enm);
                foreach (string str in names)
                {
                    drp.Items.Add(str);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int GetPagingPageSize()
        {
            string strPageSize = ConfigurationSettings.AppSettings["GridPageSize"];
            int intPageSize = -1;

            if (string.IsNullOrEmpty(strPageSize))
            {
                return 0;
            }
            if (!int.TryParse(strPageSize, out intPageSize))
            {
                return 0;
            }

            return intPageSize;
        }

        public static int GetOrderChangeHours()
        {
            string strOrderChangeHours = ConfigurationSettings.AppSettings["OrderChangeHours"];
            int intOrderChangeHours = -1;

            if (string.IsNullOrEmpty(strOrderChangeHours))
            {
                return 0;
            }
            if (!int.TryParse(strOrderChangeHours, out intOrderChangeHours))
            {
                return 0;
            }

            return intOrderChangeHours;
        }

        public static DateTime ToDate(string strDateTime)
        {
            //CultureInfo provider = new CultureInfo("fr-FR");
            //return DateTime.ParseExact(strDateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //return DateTime.ParseExact(strDateTime, "s", CultureInfo.InvariantCulture, DateTimeStyles.None);
            return Convert.ToDateTime(strDateTime.Trim());
        }

        public static void ClearReservationKeys()
        {
            //Clear Session Keys
            HttpContext.Current.Session[PublicKeys.CURRENT_ORDER_LINE_ID] = null;

            //Clear Cookies Keys
            HttpCookie CountryCookies = new HttpCookie(PublicKeys.COUNTRY_ID_KEY);
            CountryCookies.Value = null;
            HttpContext.Current.Response.Cookies.Add(CountryCookies);

            HttpCookie CityCookies = new HttpCookie(PublicKeys.ZIPCITY_ID_KEY);
            CityCookies.Value = null;
            HttpContext.Current.Response.Cookies.Add(CityCookies);

            HttpCookie FromDateCookies = new HttpCookie(PublicKeys.FROMDATE_ID_KEY);
            FromDateCookies.Value = null;
            HttpContext.Current.Response.Cookies.Add(FromDateCookies);

            HttpCookie FromTimeCookies = new HttpCookie(PublicKeys.FROMTIME_ID_KEY);
            FromTimeCookies.Value = null;
            HttpContext.Current.Response.Cookies.Add(FromTimeCookies);

            HttpCookie ToDateCookies = new HttpCookie(PublicKeys.TODATE_ID_KEY);
            ToDateCookies.Value = null;
            HttpContext.Current.Response.Cookies.Add(ToDateCookies);

            HttpCookie ToTimeCookies = new HttpCookie(PublicKeys.TOTIME_ID_KEY);
            ToTimeCookies.Value = null;
            HttpContext.Current.Response.Cookies.Add(ToTimeCookies);

            HttpCookie KeyWordLocCookies = new HttpCookie(PublicKeys.SEARCH_KEYWORD_RES_LOCATION);
            KeyWordLocCookies.Value = null;
            HttpContext.Current.Response.Cookies.Add(KeyWordLocCookies);

            HttpCookie ProductCategoryCookies = new HttpCookie(PublicKeys.PRODUCT_CATEGORY_ID_KEY);
            ProductCategoryCookies.Value = null;
            HttpContext.Current.Response.Cookies.Add(ProductCategoryCookies);

            HttpCookie KeyWordProdCookies = new HttpCookie(PublicKeys.SEARCH_KEYWORD);
            KeyWordProdCookies.Value = null;
            HttpContext.Current.Response.Cookies.Add(KeyWordProdCookies);

            HttpCookie ReservationLocationCookies = new HttpCookie(PublicKeys.LOCATION_ID_KEY_RES_LOCATION);
            ReservationLocationCookies.Value = null;
            HttpContext.Current.Response.Cookies.Add(ReservationLocationCookies);

            //HttpCookie SelectedLocationNameCookies = new HttpCookie(PublicKeys.LOCATION_SELECTEDNAME_KEY_RES);
            //SelectedLocationNameCookies.Value = null;
            //HttpContext.Current.Response.Cookies.Add(SelectedLocationNameCookies);

            HttpCookie SelectedProductNameCookies = new HttpCookie(PublicKeys.PRODUCT_SELECTEDNAME_KEY);
            SelectedProductNameCookies.Value = null;
            HttpContext.Current.Response.Cookies.Add(SelectedProductNameCookies);
        }

        public static bool IsNumber(string val)
        {
            int intVal;

            if (Int32.TryParse(val.Trim(), out intVal))
            {
                return true;
            }

            return false;
        }

        public static bool IsNumberProp(object val)
        {
            if (val.GetType() == typeof(int) || val.GetType() == typeof(float) || val.GetType() == typeof(decimal) || val.GetType() == typeof(double) || val.GetType() == typeof(long))
            {
                return true;
            }

            return false;
        }

        public static bool IsByteArrayProp(object val)
        {
            if (val.GetType() == typeof(byte[]))
            {
                return true;
            }

            return false;
        }

        public static string GetCurrentDateFormat() 
        {
           return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
        }

        public static void GetWeekStartAndEndDate(DateTime now, CultureInfo cultureInfo, out DateTime begining, out DateTime end)
        {
            if (now == null)
                throw new ArgumentNullException("now");
            if (cultureInfo == null)
                throw new ArgumentNullException("cultureInfo");

            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

            int offset = firstDayOfWeek - now.DayOfWeek;
            if (offset != 1)
            {
                DateTime weekStart = now.AddDays(offset);
                DateTime endOfWeek = weekStart.AddDays(6);
                begining = weekStart;
                end = endOfWeek;
            }
            else
            {
                //begining = now.AddDays(-6);
                //end = now;

                begining = now;
                end = now.AddDays(6);
            }
        }
    }
}
