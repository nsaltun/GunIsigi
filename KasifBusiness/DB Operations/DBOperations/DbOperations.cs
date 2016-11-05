using KasifBusiness.DB_Operations.DBObjects;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                    prop.SetValue(model, 1);
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
            string queryText = "";
            if (RunQueryForQueryContent(queryName.ToString(), ref queryText))
            {
                PrepareAndExecuteQuery(ref entityObj, queryText, parameterNames, parameterValues);
                return true;
            }
            else
            {
                return false;
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
