using KasifBusiness.DB_Operations.DBObjects;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace KasifBusiness.DB_Operations.DBOperations
{
    public class DbOperations
    {
        private static void DbAction(Action<GN_KASIFEntities> action)
        {
            using (var context = new GN_KASIFEntities())
            {
                action(context);
            }
        }

        public static void Insert(object model)
        {
            //Type type = model.GetType();
            //string propName = type.Name;
            //var property = type.GetProperty(propName);
            //var propertyValue = property.GetValue(model);

            Type type = model.GetType();

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.Name == "GUID")
                {
                    Int64 guid = KasifHelper.GuidFactory();
                    prop.SetValue(model, guid);
                }
                else if (prop.Name == "STATUS")
                {
                    prop.SetValue(model, Convert.ToInt16(1));
                }
                else if (prop.Name == "LASTUPDATED")
                {
                    prop.SetValue(model, KasifHelper.GetCurrentDate());
                }
            }

            DbAction(context =>
            {
                context.Entry(model).State = EntityState.Added;
                context.SaveChanges();
            });

        }

        public static void Update(object model)
        {
            Type type = model.GetType();

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.Name == "STATUS")
                {
                    prop.SetValue(model, Convert.ToInt16(1));
                }
                else if (prop.Name == "LASTUPDATED")
                {
                    prop.SetValue(model, KasifHelper.GetCurrentDate());
                }
            }

            DbAction(context =>
            {
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
            });
        }

        public static void Delete(object model)
        {
            Type type = model.GetType();

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.Name == "STATUS")
                {
                    prop.SetValue(model, Convert.ToInt16(0));
                }
                else if (prop.Name == "LASTUPDATED")
                {
                    prop.SetValue(model, KasifHelper.GetCurrentDate());
                }
            }

            DbAction(context =>
            {
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
            });
        }

        public static void FindBy<T>(ref List<T> entityObj, object model, Expression<Func<T, bool>> predicate)
        {
            List<T> lstObj = new List<T>();
            DbAction(context =>
            {
                //_entities.Set<T>().Where(predicate);
                lstObj = context.Set(model.GetType()).OfType<T>().Where(predicate).ToList();

            });
            entityObj = lstObj;
        }

        private static bool RunQueryForQueryContent(string queryName, ref string queryText)
        {
            List<QUERY_TABLE> lstQueryTable = new List<QUERY_TABLE>();
            PrepareAndExecuteQuery(ref lstQueryTable, ConstDbCommands.GET_QUERY_CONTENT, new string[] { "P_QUERY_NAME" }, new object[] { queryName });
            if (lstQueryTable != null && lstQueryTable.Count > 0)
            {
                queryText = lstQueryTable[0].QUERY_TEXT;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RunDbQuery<T>(ref List<T> entityObj, ConstDbCommands.DbCommandList queryName, string[] parameterNames, object[] parameterValues)
        {
            try
            {
                string queryText = "";
                if (RunQueryForQueryContent(queryName.ToString(), ref queryText))
                {
                    queryText = CheckParametersInQuery(queryText, parameterNames);
                    PrepareAndExecuteQuery(ref entityObj, queryText, parameterNames, parameterValues);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static void PrepareAndExecuteQuery<T>(ref List<T> entityObj, string queryContent, string[] parameterNames, object[] parameterValues)
        {
            try
            {
                GN_KASIFEntities EntityObj = new GN_KASIFEntities();

                if (parameterNames != null && parameterValues != null)
                {
                    SqlParameter[] SqlParameters = new SqlParameter[parameterNames.Length];
                    for (int i = 0; i < parameterNames.Length; i++)
                    {
                        SqlParameters[i] = new SqlParameter(parameterNames[i], parameterValues[i]);
                    }
                    entityObj = EntityObj.Database.SqlQuery<T>(queryContent, SqlParameters).ToList<T>();
                }
                else
                {
                    entityObj = EntityObj.Database.SqlQuery<T>(queryContent).ToList<T>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string CheckParametersInQuery(string queryText, string[] parameterNames)
        {
            /* -- JOB DESCRIPTION --
             search queryText by rows for parameter. get parameters in query and keep them in a variable with row id.
                if sent parameter not exist in this variable : delete related row.
             */

            List<string[]> lstPrmInQuery = new List<string[]>();
            List<int> lstDeletedRows = new List<int>();
            List<string> lst = queryText.Split(new char[] { '\r' }).ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].Contains('@'))
                {
                    int startIndex = lst[i].IndexOf('@');
                    int endIndex = lst[i].IndexOf(' ', startIndex);
                    if (endIndex == -1)
                    {
                        endIndex = lst[i].Length;
                    }
                    lstPrmInQuery.Add(new string[] { i.ToString(), lst[i].Substring(startIndex + 1, endIndex - startIndex - 1) });
                    lstDeletedRows.Add(i);
                }
            }
            if (parameterNames != null && parameterNames.Length > 0)
            {
                foreach (string item1 in parameterNames)
                {
                    foreach (string[] item2 in lstPrmInQuery)
                    {
                        if (item1 == item2[1])
                        {
                            lstDeletedRows.Remove(Convert.ToInt32(item2[0]));
                            break;
                        }
                    }
                }
            }
            int j = 0;
            foreach (int index in lstDeletedRows)
            {
                lst.RemoveAt(index - j);
                j++;
            }

            return string.Join("", lst);
        }

        #region commented - not used
        private static string GetQuery(ConstDbCommands.DbCommandList queryName)
        {
            switch (queryName)
            {
                case ConstDbCommands.DbCommandList.GET_USER_BY_EMAIL: return ConstDbCommands.GET_QUERY_CONTENT;
                default:
                    break;
            }

            return "";
        }
        #endregion

    }
}
