using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASE.Framework;

namespace ASE.DAL
{
    public static class DalBase<T>
    {
        #region Properties
        public static SqlParameter ObjSqlParameter = null;
        #endregion

        #region Helper Functions


        #region Insert Functions

        private static string GetExtFieldsInsStmnt(T entity)
        {
            string strFelds = "";
            string strValues = "";
            string sqlStat = "";

            PropertyInfo propExtraFilelds = entity.GetType().GetProperty("ExtraFields");
            Dictionary<string, string> dicExtraFields = (Dictionary<string, string>)propExtraFilelds.GetValue(entity, null);

            if (dicExtraFields.Count > 0)
            {
                strFelds = "INSERT INTO @TableName(";
                strValues = " VALUES(";

                foreach (KeyValuePair<string, string> pair in dicExtraFields)
                {
                    if (pair.Value != null)
                    {
                        strFelds += pair.Key + ",";
                        strValues += FormatProperty(pair.Value) + ",";
                    }

                }

                strFelds = strFelds.Substring(0, strFelds.Length - 1) + ")";
                strValues = strValues.Substring(0, strValues.Length - 1) + ")";

            }

            sqlStat = strFelds + strValues;

            return (!string.IsNullOrEmpty(sqlStat)) ? sqlStat + " SELECT Top 1 @ID = ID FROM @TableName ORDER BY CreatedDate DESC" : "";
        }

        //This function for getting the sql insert satatment for the fields related by the passed table name
        private static string GetetStandTableInsStmnt(T entity, string tableName, bool isParent)
        {
            string strFelds = "";
            string strValues = "";
            object val;
            DbField dbField;
            ParentIdentity attParentIdentity;

            var lstMappedProperties =
                entity.GetType().GetProperties().Where(
                    prop => prop.GetCustomAttributes(typeof(DbField), true).Count() > 0).ToList();


            if (lstMappedProperties.Count > 0)
            {
                strFelds = "INSERT INTO " + tableName + "(";
                strValues = " VALUES(";


                foreach (PropertyInfo prop in lstMappedProperties)
                {
                    val = GetFieldValue(entity, prop);
                    dbField = (DbField)prop.GetCustomAttributes(typeof(DbField), true).Where(fld => ((DbField)fld).TableName == tableName || (isParent && ((DbField)fld).TableName == null)).SingleOrDefault();

                    //Get the parent identity attribute to check if this field is parent identity then take the paren identity value
                    attParentIdentity = (ParentIdentity)prop.GetCustomAttributes(typeof(ParentIdentity), true).SingleOrDefault();

                    if (dbField == null)
                    {
                        continue;
                    }
                    if (val != null && dbField.PostToDB)
                    {
                        strFelds += dbField.FieldName + ",";

                        //Here Chek to get @identity of parent value instead of Prop value
                        if (attParentIdentity != null)
                        {
                            strValues += "@ID,";
                        }
                        else
                        {
                            double dblVal;
                            if (val.GetType() == typeof(float))
                            {
                                dblVal = Convert.ToDouble(val);
                                strValues += "'" + dblVal + "',";
                            }
                            else
                            {
                                strValues += "'" + val + "',";
                            }

                        }
                    }
                }

                strFelds = strFelds.Substring(0, strFelds.Length - 1) + ")";
                strValues = strValues.Substring(0, strValues.Length - 1) + ")";

                if (isParent)
                {
                    //Set the ID output parameter with the master table ID after insertion
                    strValues += " SELECT Top 1  @ID = ID FROM " + tableName + " ORDER BY CreatedDate DESC"; ;
                }


            }

            return strFelds + strValues;

        }

        //This function get all object properies table and get the insert statement for each table
        private static string GetStandFieldsInsStmnt(T entity)
        {
            List<string> lstTableNames = GetTableNames(entity);

            //Craete the sql return statment and initilaize it with the parent table inser stat
            string sqlStat = GetetStandTableInsStmnt(entity, GetStandTblName(entity), true);

            if (lstTableNames.Count > 1)
            {
                for (int i = 1; i < lstTableNames.Count; i++)
                {
                    bool updInv = (bool)GetFieldValue(entity, "UpdateInvFlag");
                    if (lstTableNames[i].ToLower() != "ibals" || (lstTableNames[i].ToLower() == "ibals" && updInv))
                    {
                        sqlStat += " " + GetetStandTableInsStmnt(entity, lstTableNames[i], false);
                    }
                }
            }

            return sqlStat;
        }

        private static string GetetExtStandTableInsStmnt(T entity, string tableName, bool isParent)
        {
            string strFelds = "";
            string strValues = "";
            object val;
            DbField dbField;
            Dictionary<string, string> dicExtraFields = new Dictionary<string, string>();
            ParentIdentity attParentIdentity;

            var lstMappedProperties =
                entity.GetType().GetProperties().Where(
                    prop => prop.GetCustomAttributes(typeof(DbField), true).Count() > 0).ToList();


            if (lstMappedProperties.Count > 0)
            {
                strFelds = "INSERT INTO " + tableName + " (";
                strValues = " VALUES(";


                foreach (PropertyInfo prop in lstMappedProperties)
                {
                    val = GetFieldValue(entity, prop);
                    dbField = (DbField)prop.GetCustomAttributes(typeof(DbField), true).Where(fld => ((DbField)fld).TableName == tableName || (isParent && ((DbField)fld).TableName == null)).SingleOrDefault();

                    //Get the parent identity attribute to check if this field is parent identity then take the paren identity value
                    attParentIdentity = (ParentIdentity)prop.GetCustomAttributes(typeof(ParentIdentity), true).SingleOrDefault();

                    if (dbField == null)
                    {
                        continue;
                    }

                    if (val != null && dbField.PostToDB)
                    {
                        strFelds += dbField.FieldName + ",";

                        //Here Chek to get @identity of parent value instead of Prop value
                        if (attParentIdentity != null)
                        {
                            strValues += "@ID,";
                        }
                        else
                        {
                            strValues += FormatProperty(val) + ",";
                        }
                    }
                }
            }

			if (isParent)
			{
				PropertyInfo propExtraFilelds = entity.GetType().GetProperty("ExtraFields");
				if (propExtraFilelds != null)
				{
					dicExtraFields = (Dictionary<string, string>)propExtraFilelds.GetValue(entity, null);
				}

				if (dicExtraFields.Count > 0)
				{
					foreach (KeyValuePair<string, string> pair in dicExtraFields)
					{
						if (pair.Value != null)
						{
							strFelds += pair.Key + ",";
							strValues += "'" + pair.Value + "',";
						}

					}
				}

			}

			strFelds = strFelds.Substring(0, strFelds.Length - 1) + ")";
            strValues = strValues.Substring(0, strValues.Length - 1) + ")";

            //if (isParent)
            //{
            //    //Set the ID output parameter with the master table ID after insertion
            //    strValues += " SELECT Top 1 @ID = ID FROM " + tableName + " ORDER BY CreatedDate DESC";
            //}

            return strFelds + strValues;
        }

        //This function get all object properies table and get the insert statement for each table
        private static string GetExtStandInsStmnt(T entity)
        {
            List<string> lstTableNames = GetTableNames(entity);

            //Craete the sql return statment and initilaize it with the parent table inser stat
            string sqlStat = GetetExtStandTableInsStmnt(entity, GetStandTblName(entity), true);

            if (lstTableNames.Count > 1)
            {
                for (int i = 1; i < lstTableNames.Count; i++)
                {
                    sqlStat += " " + GetetExtStandTableInsStmnt(entity, lstTableNames[i], false);
                }
            }

            return sqlStat;
        }

        #endregion

        #region Update Functions

        private static string GetExtUpdStmnt(T entity)
        {
            string strFelds = "";
            string whereStat = "";

            PropertyInfo propExtraFilelds = entity.GetType().GetProperty("ExtraFields");
            Dictionary<string, string> dicExtraFields = (Dictionary<string, string>)propExtraFilelds.GetValue(entity, null);

            if (dicExtraFields.Count > 0)
            {
                strFelds = "UPDATE @TableName SET ";

                foreach (KeyValuePair<string, string> pair in dicExtraFields)
                {
                    if (pair.Value != null)
                    {
                        strFelds += pair.Key + "=" + FormatProperty(pair.Value) + ",";
                    }

                }

                whereStat = GetWhereStatForExt(entity);
                strFelds = strFelds.Substring(0, strFelds.Length - 1) + whereStat;


            }

            return strFelds;
        }

        //This function for getting the sql update satatment for the fields related by the passed table name
        private static string GetStandTableUpdStmnt(T entity, string tableName, bool isParent)
        {
            string strFelds = "";
            string whereStat = "";
            object val;
            DbField dbField;

            var lstMappedProperties =
                entity.GetType().GetProperties().Where(
                    prop => prop.GetCustomAttributes(typeof(DbField), true).Count() > 0).ToList();


            if (lstMappedProperties.Count > 0)
            {
                strFelds = "UPDATE " + tableName + "  SET ";


                foreach (PropertyInfo prop in lstMappedProperties)
                {
                    val = GetFieldValue(entity, prop);
                    dbField = (DbField)prop.GetCustomAttributes(typeof(DbField), true).Where(fld => ((DbField)fld).TableName == tableName || (isParent && ((DbField)fld).TableName == null)).SingleOrDefault();

                    if (dbField == null)
                    {
                        continue;
                    }


                    if (dbField.PostToDB)
                    {
                        strFelds += (val != null) ? dbField.FieldName + "=" + "'" + val + "'," : dbField.FieldName + "= NULL ,";
                    }
                }

                whereStat = GetWhereStat(entity);
                strFelds = strFelds.Substring(0, strFelds.Length - 1) + whereStat;

            }

            return strFelds;
        }

        //This function get all object properies table and get the insert statement for each table
        private static string GetStandUpdStmnt(T entity)
        {
            List<string> lstTableNames = GetTableNames(entity);

            //Craete the sql return statment and initilaize it with the parent table inser stat
            string sqlStat = GetStandTableUpdStmnt(entity, GetStandTblName(entity), true);

            if (lstTableNames.Count > 1)
            {
                for (int i = 1; i < lstTableNames.Count; i++)
                {
                    bool updInv = (bool)GetFieldValue(entity, "UpdateInvFlag");
                    if (lstTableNames[i] != "ibals" || (lstTableNames[i] == "ibals" && updInv))
                    {
                        sqlStat += " " + GetStandTableUpdStmnt(entity, lstTableNames[i], false);
                    }
                }
            }

            return sqlStat;
        }

        //This function for getting the sql update satatment for the fields related by the passed table name standared and extra
        private static string GetExtStandTableUpdStmnt(T entity, string tableName, bool isParent)
        {
            string strFelds = "";
            string whereStat = "";
            object val;
            DbField dbField;
            Dictionary<string, string> dicExtraFields = new Dictionary<string, string>();

            var lstMappedProperties =
                entity.GetType().GetProperties().Where(
                    prop => prop.GetCustomAttributes(typeof(DbField), true).Count() > 0).ToList();


            if (lstMappedProperties.Count > 0)
            {
                strFelds = "UPDATE " + tableName + " SET ";


                foreach (PropertyInfo prop in lstMappedProperties)
                {
                    val = GetFieldValue(entity, prop);
                    dbField = (DbField)prop.GetCustomAttributes(typeof(DbField), true).Where(fld => ((DbField)fld).TableName == tableName || (isParent && ((DbField)fld).TableName == null)).SingleOrDefault();

                    if (dbField == null)
                    {
                        continue;
                    }

                    //if (val != null && dbField.PostToDB) 
                    if (dbField.PostToDB) // to Save NULL Values
                    {
                        //strFelds += dbField.FieldName + "=" +  FormatProperty(val) + ",";
                        strFelds += dbField.FieldName + "=" + (string.IsNullOrEmpty(FormatProperty(val)) ? "NULL" : FormatProperty(val)) + ","; // to Set NULL Values
                    }
                }

                //Only contnate the extra fields in case of parent table
                if (isParent)
                {
                    PropertyInfo propExtraFilelds = entity.GetType().GetProperty("ExtraFields");
                    if (propExtraFilelds != null)
                    {
                        dicExtraFields = (Dictionary<string, string>)propExtraFilelds.GetValue(entity, null);
                    }

                    if (dicExtraFields.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> pair in dicExtraFields)
                        {
                            if (pair.Value != null)
                            {
                                strFelds += pair.Key + "=" + "'" + pair.Value + "',";
                            }

                        }
                    }
                }

                whereStat = GetWhereStat(entity);
                strFelds = strFelds.Substring(0, strFelds.Length - 1) + whereStat;

            }

            return strFelds;
        }

        //This function get all object properies table and get the insert statement for each table
        private static string GetExtStandUpdStmnt(T entity)
        {
            List<string> lstTableNames = GetTableNames(entity);

            //Craete the sql return statment and initilaize it with the parent table inser stat
            string sqlStat = GetExtStandTableUpdStmnt(entity, GetStandTblName(entity), true);

            if (lstTableNames.Count > 1)
            {
                for (int i = 1; i < lstTableNames.Count; i++)
                {
                    sqlStat += " " + GetExtStandTableUpdStmnt(entity, lstTableNames[i], false);
                }
            }

            return sqlStat;
        }


        #endregion

        #region General Functions

        private static string GetStandTblName(T entity)
        {
            DbTable dbTable;
            dbTable = (DbTable)entity.GetType().GetCustomAttributes(typeof(DbTable), true).SingleOrDefault();

            return (dbTable != null) ? dbTable.TableName : "";
        }

        public static void SetFieldValue(T entity, PropertyInfo propertyInfo, object Val)
        {
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(entity, Convert.ChangeType(Val, propertyInfo.PropertyType),
                                      null);
            }
        }

        //This function check if this field is formual or not and get it's value
        public static object GetFieldValue(T entity, string prop)
        {
            PropertyInfo propertyInfo = entity.GetType().GetProperty(prop);
            return GetFieldValue(entity, propertyInfo);
        }

        //This function check if this field is formual or not and get it's value
        public static object GetFieldValue(T entity, PropertyInfo propertyInfo)
        {
            FormulaField attFormual;
            object val = null;

            if (propertyInfo != null)
            {
                attFormual = (FormulaField)propertyInfo.GetCustomAttributes(typeof(FormulaField), true).SingleOrDefault();
                if (attFormual == null)
                {
                    val = propertyInfo.GetValue(entity, null);
                }
                else
                {
                    val = GetFormulaFieldValue(propertyInfo, entity, attFormual);
                }

                //This special handling for date formating to put the date on the server format
                if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                {
                    if (val != null)
                        val = FormatProperty(val).Replace("'", "");
                }
                if (val is Enum)
                {
                    val = (byte)val;
                }
            }
            return val;
        }

        public static string FormatProperty(object _str)
        {
            if (_str == null)
            {
                return null;
            }
            DateTime? dateVal = IsDate(_str);
            if (dateVal != null)
            {
                return "'" + ((DateTime)dateVal).ToString("yyyy/MM/dd HH:mm:ss.fff") + "'";
            }
            else
            {
                if (Helper.IsNumberProp(_str))
                {
                    CultureInfo us = new CultureInfo("en-US");
                    return "'" + Convert.ToString(_str, us.NumberFormat) + "'";
                }
                else if (Helper.IsByteArrayProp(_str))
                {
                    //return "CAST('" + ByteArrayToString(_str as byte[]) + "' AS VARBINARY(MAX))";
                    return "0x" + ByteArrayToString(_str as byte[]);
                }

                return "'" + _str.ToString() + "'";
            }
        }


        public static DateTime? IsDate(object obj)
        {

            if (obj == null)
            {
                return null;
            }

            if (obj.GetType() == typeof(DateTime))
            {
                return DateTime.Parse(obj.ToString());
            }

            string strDate = obj.ToString();
            var list1 = strDate.Where(x => x == '.').ToList();
            var list2 = strDate.Where(x => x == '-').ToList();
            var list3 = strDate.Where(x => x == '/').ToList();

            DateTime _date;

            if (DateTime.TryParse(strDate, out _date) && (list1.Count == 2 || list2.Count == 2 || list3.Count == 2))
                return _date;

            return null;
        }

        //This function responsable for resolving formual and getting the value
        private static object GetFormulaFieldValue(PropertyInfo propertyInfo, T entity, FormulaField attFormual)
        {

            var formualFields = attFormual.Formula.Split(']');

            string fieldName = "";
            object fieldVal = "";
            string formula = "";
            PropertyInfo innerPropertyInfo;
            object formulaValue = null;
            formula = attFormual.Formula;

            for (int i = 0; i < formualFields.Length - 1; i++)
            {
                fieldName = formualFields[i].Split('[')[1];
                innerPropertyInfo = entity.GetType().GetProperty(fieldName);
                fieldVal = innerPropertyInfo.GetValue(entity, null);

                if (fieldVal != null)
                {
                    formula = formula.Replace("[" + fieldName + "]", fieldVal.ToString().Trim());
                }
            }

            if (propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(float))
            {
                formulaValue = MathEvaluator.Evaluate(formula);
                SetFieldValue(entity, propertyInfo, formulaValue);

                return formulaValue;
            }

            //This only will be for default values on not numbers fields
            else
            {
                return formula;
            }


        }

        private static string GetWhereStat(T entity)
        {
            string whereStat;
            PropertyInfo prop = entity.GetType().GetProperty("ID");
            object ID = prop.GetValue(entity, null);
            DbField dbField = (DbField)prop.GetCustomAttributes(typeof(DbField), true).SingleOrDefault();
            whereStat = " WHERE " + dbField.FieldName + "= '" + ID + "'";
            return whereStat;
        }

        private static string GetWhereStatForExt(T entity)
        {
            string whereStat;
            PropertyInfo prop = entity.GetType().GetProperty("ExtraOf");
            object ExtraOf = prop.GetValue(entity, null);
            DbField dbField = (DbField)prop.GetCustomAttributes(typeof(DbField), true).SingleOrDefault();
            whereStat = " WHERE ID = '" + ExtraOf + "'";
            return whereStat;
        }

        private static List<string> GetTableNames(T entity)
        {
            //Craete list for table names and initiliza it with the standard table name
            List<string> lstTableNames = new List<string>();
            lstTableNames.Insert(0, GetStandTblName(entity).ToLower());


            var lstMappedProperties =
                entity.GetType().GetProperties().Where(
                    prop => prop.GetCustomAttributes(typeof(DbField), true).Count() > 0).ToList();

            foreach (PropertyInfo prop in lstMappedProperties)
            {
                lstTableNames.AddRange(prop.GetCustomAttributes(typeof(DbField), true).Where(fld => ((DbField)fld).TableName != null).Select(fld => ((DbField)fld).TableName));
            }

            lstTableNames = lstTableNames.Distinct().ToList();
            return lstTableNames;
        }

        public static bool GetFlagByIndex(string flags, int index)
        {
            if (!string.IsNullOrEmpty(flags) && index >= 0 && index < flags.Length)
            {
                bool flag = Convert.ToBoolean(int.Parse(flags[index].ToString()));
                return flag;
            }

            return false;
        }

        public static string SetFlagByIndex(string flags, int index, int val)
        {
            string newFlag = "";
            if (!string.IsNullOrEmpty(flags) && index >= 0 && index < flags.Length)
            {
                newFlag = flags.Remove(index, 1);
                newFlag = newFlag.Insert(index, val.ToString());
                //flags[index] = val.ToString()[0];
                //newFlag.Remove(.Insert( = flags.Replace(flags[index], val.ToString()[0]);
            }

            return newFlag;
        }

        //This function to delete the product collection which is not the list of the collection products
        public static void DeleteObjectUnWantedRelatedItems(string ListIDs, string TableName, string FKIDName, string objectIDName, Guid ID, SqlTransaction sqlTrans)
        {

            string sqlStat = "";

            if (string.IsNullOrEmpty(ListIDs))
            {
                sqlStat = "DELETE FROM " + TableName + " WHERE " + objectIDName + " = '" + ID.ToString() + "'";
            }
            else
            {
                sqlStat = "DELETE FROM " + TableName + " WHERE " + FKIDName + "  Not IN(" + ListIDs + ") AND " + objectIDName + " = '" + ID.ToString() + "'";
            }

            SqlParameter[] Params = new SqlParameter[1];

            Params[0] = new SqlParameter("@StrQuery", sqlStat);
            SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
                                      "ExecuteSqlString",
                                      Params);
        }
        //public static void DeleteAgencyUnWantedRelatedItems(string ListIDs,string TableName,string FKIDName, Guid AgencyID, SqlTransaction sqlTrans)
        //{

        //    string sqlStat = "";

        //    if (string.IsNullOrEmpty(ListIDs))
        //    {
        //        sqlStat = "DELETE FROM " + TableName + " WHERE FK_AgencyID = '" + AgencyID.ToString() + "'";
        //    }
        //    else
        //    {
        //        sqlStat = "DELETE FROM " + TableName + " WHERE " + FKIDName + "  Not IN(" + ListIDs + ") AND FK_AgencyID = '" + AgencyID.ToString() + "'";
        //    }

        //    SqlParameter[] Params = new SqlParameter[1];

        //    Params[0] = new SqlParameter("@StrQuery", sqlStat);
        //    SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
        //                              "ExecuteSqlString",
        //                              Params);
        //}

        public static void SaveListOfURLEntity(string ListIDs, string TableName, string FKIDName, Guid AgencyID, SqlTransaction sqlTrans)
        {

            string sqlStat = "";

            if (string.IsNullOrEmpty(ListIDs))
            {
                sqlStat = "DELETE FROM " + TableName + " WHERE FK_AgencyID = '" + AgencyID.ToString() + "'";
            }
            else
            {
                sqlStat = "DELETE FROM " + TableName + " WHERE " + FKIDName + "  Not IN(" + ListIDs + ") AND FK_AgencyID = '" + AgencyID.ToString() + "'";
            }

            SqlParameter[] Params = new SqlParameter[1];

            Params[0] = new SqlParameter("@StrQuery", sqlStat);
            SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
                                      "ExecuteSqlString",
                                      Params);
        }

        //This function to delete the PageAction which is not in the list of the PageAction List
        public static void DeletePageActionUnWantedActions(string PageActionsIDs, Guid AuthPageID, SqlTransaction sqlTrans)
        {

            string sqlStat = "";

            if (string.IsNullOrEmpty(PageActionsIDs))
            {
                sqlStat = "DELETE FROM Auth_PageAction WHERE FK_PageID = '" + AuthPageID.ToString() + "'";
            }
            else
            {
                sqlStat = "DELETE FROM Auth_PageAction WHERE ID Not IN(" + PageActionsIDs + ") AND FK_PageID = '" + AuthPageID.ToString() + "'";
            }

            SqlParameter[] Params = new SqlParameter[1];

            Params[0] = new SqlParameter("@StrQuery", sqlStat);
            SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
                                      "ExecuteSqlString",
                                      Params);


        }

        //This function to delete the Order Line Details which is not the list of the OrderLine Details
        public static void DeleteNotExsistOrderLineDetails(string OrderLineDetailsIDs, Guid OrderLineID, SqlTransaction sqlTrans)
        {

            string sqlStat = "";

            if (string.IsNullOrEmpty(OrderLineDetailsIDs))
            {
                sqlStat = "DELETE FROM OrderLine_Details WHERE FK_OrderLineID = '" + OrderLineID.ToString() + "'";
            }
            else
            {
                sqlStat = "DELETE FROM OrderLine_Details WHERE ID Not IN(" + OrderLineDetailsIDs + ") AND FK_OrderLineID = '" + OrderLineID.ToString() + "'";
            }

            SqlParameter[] Params = new SqlParameter[1];

            Params[0] = new SqlParameter("@StrQuery", sqlStat);
            SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
                                      "ExecuteSqlString",
                                      Params);


        }

        //This Function Convert Binary array to string
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        #endregion


        #endregion

        #region Save Functions

        public static SqlTransaction GetTransactionObject()
        {
            SqlConnection myConnection = new SqlConnection(ConnectionStringManager.ConnectionString);
            myConnection.Open();
            return myConnection.BeginTransaction();
        }

        private static object Insert(T entity, string extraTableName, SqlTransaction sqlTrans)
        {
            try
            {

                SqlParameter[] Params = new SqlParameter[2];

                Guid ID = Guid.NewGuid();
                Params[1] = new SqlParameter("@ID", ID);
                //Params[1].DbType = DbType.Guid;
                Params[1].Size = 50;
                Params[1].Direction = ParameterDirection.Output;




                string sqlStat = "";


                #region Footer

                if (extraTableName == "-") // It Means Footer Entity
                {
                    sqlStat = GetExtStandInsStmnt(entity);

                    if (!string.IsNullOrEmpty(sqlStat)) // It Means Footer Entity
                    {
                        Params[0] = new SqlParameter("@InsertStatement", sqlStat);
                        //test
                        //if (ObjSqlParameter != null)
                        //{
                        //    Params[2] = ObjSqlParameter;
                        //}
                        SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
                                                  "ExecuteInsertStatement",
                                                  Params);

                        if (Params[1].Value != DBNull.Value)
                        {
                            ID = (Guid)Params[1].Value;

                            //2-Set Object ExtraOf Field
                            Helper.SetFieldValue(entity, "ID", ID);

                        }
                    }
                }
                #endregion

                #region Header Body

                else //Header Or Body 
                {
                    //1-Insert Extra Fields Values
                    sqlStat = GetExtFieldsInsStmnt(entity);
                    sqlStat = sqlStat.Replace("@TableName", extraTableName);

                    if (!string.IsNullOrEmpty(sqlStat))
                    {
                        Params[0] = new SqlParameter("@InsertStatement", sqlStat);
                        SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
                                                  "ExecuteInsertStatement",
                                                  Params);

                        if (Params[1].Value != DBNull.Value && Params[1].Value != null)
                        {
                            ID = (Guid)Params[1].Value;

                            //2-Set Object ExtraOf Field
                            Helper.SetFieldValue(entity, "ExtraOf", ID);
                        }
                    }



                    //3-Insert Standard
                    sqlStat = GetStandFieldsInsStmnt(entity);

                    if (!string.IsNullOrEmpty(sqlStat))
                    {
                        Params[0] = new SqlParameter("@InsertStatement", sqlStat);
                        SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
                                                  "ExecuteInsertStatement",
                                                  Params);

                        ID = (Guid)Params[1].Value;

                        //Set Object ID after Insert
                        Helper.SetFieldValue(entity, "ID", ID);

                    }



                }
                #endregion



                return ID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private static object Update(T entity, string extraTableName, SqlTransaction sqlTrans)
        {
            try
            {
                SqlParameter[] Params = new SqlParameter[1];

                string sqlStat = "";


                #region Footer

                if (extraTableName == "-") // It Means Footer Entity
                {
                    sqlStat = GetExtStandUpdStmnt(entity);

                    if (!string.IsNullOrEmpty(sqlStat))
                    {
                        Params[0] = new SqlParameter("@StrQuery", sqlStat);
                        SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
                                                  "ExecuteSqlString",
                                                  Params);

                    }
                }
                #endregion

                #region Header Body

                else //Header Or Body 
                {
                    if (GetFieldValue(entity, "ExtraOf").Equals(-1))
                    {
                        SqlParameter[] InsParams = new SqlParameter[2];

                        Guid ID = new Guid();
                        InsParams[1] = new SqlParameter("@ID", ID);
                        InsParams[1].Direction = ParameterDirection.Output;

                        //1-Insert Extra Fields Values
                        sqlStat = GetExtFieldsInsStmnt(entity);
                        sqlStat = sqlStat.Replace("@TableName", extraTableName);

                        if (!string.IsNullOrEmpty(sqlStat))
                        {
                            InsParams[0] = new SqlParameter("@InsertStatement", sqlStat);
                            SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
                                                      "ExecuteInsertStatement",
                                                      InsParams);

                            ID = (Guid)InsParams[1].Value;

                            //2-Set Object ExtraOf Field
                            Helper.SetFieldValue(entity, "ExtraOf", ID);
                        }

                    }
                    else
                    {

                        //1-Update Extra Fields Values
                        sqlStat = GetExtUpdStmnt(entity);
                        sqlStat = sqlStat.Replace("@TableName", extraTableName);

                        if (!string.IsNullOrEmpty(sqlStat))
                        {
                            Params[0] = new SqlParameter("@StrQuery", sqlStat);
                            SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
                                                      "ExecuteSqlString",
                                                      Params);
                        }

                    }


                    //3-Update Standard
                    sqlStat = GetStandUpdStmnt(entity);

                    if (!string.IsNullOrEmpty(sqlStat))
                    {
                        Params[0] = new SqlParameter("@StrQuery", sqlStat);
                        SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure,
                                                  "ExecuteSqlString",
                                                  Params);
                    }

                }
                #endregion

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static object Save(T entity, SqlTransaction sqlTrans, bool? isNew)
        {
            if (isNew == null || (bool)isNew)
            {
                return Insert(entity, "-", sqlTrans);
            }
            else
            {
                return Update(entity, "-", sqlTrans);
            }
        }

        public static object Save(T entity, bool? isNew)
        {
            SqlTransaction sqlTrans = GetTransactionObject();
            try
            {
                object result = Save(entity, sqlTrans, isNew);
                sqlTrans.Commit();
                return result;
            }
            catch (Exception ex)
            {
                sqlTrans.Rollback();
                return null;
            }
            finally
            {
                sqlTrans.Dispose();
            }
        }

        // Dashboard Preferences (special case)

        //public static object Save(T entity, SqlTransaction sqlTrans, string extraTableName,  bool? isNew)
        //{
        //    if (isNew == null || (bool)isNew)
        //    {
        //        return Insert(entity, extraTableName, sqlTrans);
        //    }
        //    else
        //    {
        //        return Update(entity, extraTableName, sqlTrans);
        //    }
        //}
        #endregion
    }
}