using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.Framework
{
    public static class PublicKeys
    {
        public static string HOME_PAGE_PATH = "~/Public/Index.aspx";
        public static string SESSION_DATAMODEL_KEY = "DataModel";
        public static string SESSION_LOGED_IN_USER_KEY = "objLogedInUser";
        public static string SESSION_USER_ALLOWED_ACTIONS_KEY = "lstAllowedActions";
        public static string SESSION_ORDER_LINE_HISTORY_KEY = "ordlh";
        public static string USER_ID_KEY = "yt";
        public static string USER_USERNAME_KEY = "regun";
        public static string USER_ADDNEW_KEY = "AddNU";
        public static string USER_REMEMBER_USERNAME = "RememUserName";
        public static string USER_REMEMBER_PASSWORD = "RememPass";
        public static string EU_COOKIE_MESSAGE = "eucookiemsg";

        public static string PARTNER_ID_KEY = "Pt";
        public static string LOCATION_ID_KEY = "loc";
        public static string LOCATION_FROM_PARTNER_KEY = "fploc";
        public static string LOCATION_ID_KEY_RES_LOCATION = "rloc";
        public static string LOCATION_SELECTEDNAME_KEY_RES = "slocname";
        public static string LOCATION_LATITUDE_KEY_RES = "LocationLat";
        public static string LOCATION_LONGITUDE_KEY_RES = "LocationLong";


        public static string COUNTRY_ID_KEY = "cunt";
        public static string ZIPCITY_ID_KEY = "zcty";

        public static string RESERVATION_ID_KEY = "res";
        public static string RESERVATION_NO_KEY = "resno";
        public static string PAYMENT_RESERVATIONID__KEY = "orderid";

        public static string FROMDATE_ID_KEY = "from";
        public static string FROMTIME_ID_KEY = "fromt";
        public static string TODATE_ID_KEY = "to";
        public static string TOTIME_ID_KEY = "tot";


        public static string STARTDATE_FROM_INDEX = "IndexStartDate";

        public static string PRODUCT_ID_KEY = "Prod";
        public static string PRODUCT_UNIT_ID_KEY = "ud";
        public static string PRODUCT_CATEGORY_ID_KEY = "ctg";
        public static string PRODUCT_SELECTEDNAME_KEY = "sProdname";

        public static string ORDER_ID_KEY = "ord";

        public static string SEARCH_KEYWORD = "ky";
        public static string SEARCH_KEYWORD_RES_LOCATION = "kyresloc";


        public static string USER_DEFAULT_PASSWORD_KEY = "udpi";
        public static string DEFAULT_PASSWORD = "Ftbs";
        public static string VIEWSTAET_VALIDATION_GROUP_KEY = "vagk";
        public static string SESSION_MAINTAIN_PARTNER_CONTROL_CLIENT_ID = "UcMaintainPartnerClientID";
        public static string SESSION_MAINTAIN_PRODUCT_CONTROL_CLIENT_ID = "UcMaintainProductClientID";
        public static string SESSION_PROD_COLLECTION_KEY = "ProdCollAllProds";
        public static string SESSION_AUTHPAGEACTION_LST_KEY = "AllAuthPageActionLst";
        public static string SESSION_MENUITEMS_LST_KEY = "lstMenuItems";
        public static string SESSION_SELECTEDUSERID_KEY = "CurUserID";

        public static string IS_FROM_LOGIN_KEY = "IsFromLogin";

        public static string FLAG_RESERVATION_SEARCH = "ResSearchFlag";


        #region reservation

        #region Session Keys

        public static string CURRENT_ORDER = "curOrder";
        public static string BASKET_ORDER = "basOrder";
        public static string CURRENT_ORDER_LINE_ID = "curOrderLineID";

        public static string ORDER_LINE_NUMBER = "ordnum";
        public static string ORDER_LINE_ACTION_FLAG = "ordf";

        public static string ORDER_LINE_ACTIVE_FLAG = "a";
        public static string ORDER_LINE_RETURN_FLAG = "r";
        public static string ORDER_LINE_SAVE_FLAG = "s";
        public static string ORDER_DETAILS_PATH = "odp";
        public static string ORDER_LINE_TEMP_STATE = "OrderLineTempState";
        public static string PAYMENT_COMPLETE = "PaymentComplete";
        public static string PAYMENT_ORDER_LINE_LIST = "PaymentOrderLineList";

        public static string SESSION_ORDER_LINE_HISTORY_LIST = "OrderLineHistoryList";

        public static string ORDER_LINE_TYPE_FLAG = "OrderLineType";

        #endregion

        #endregion

        #region Roles Codes
        public static string ROLE_PARTNER_CODE = "prt";
        public static string ROLE_PARTNER_ADMIN_CODE = "prtadmn";
        public static string ROLE_PARTNER_USER_CODE = "prtusr";
        public static string ROLE_USER_CODE = "cus";
        #endregion


    }
}
