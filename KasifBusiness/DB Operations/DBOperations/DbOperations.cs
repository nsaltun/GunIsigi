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
        private static void DbAction(Action<EntityObject.GN_KASIFEntities> action)
        {
            using (var context = new EntityObject.GN_KASIFEntities())
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
                    prop.SetValue(model, 0);
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

        public static bool RunQuery<T>(ref List<T> entityObj, ConstDbCommands.DbCommandList queryName, string[] parameterNames, object[] parameterValues)
        {
            string queryContent = GetQuery(queryName);
            PrepareAndExecuteQuery(ref entityObj, queryContent, parameterNames, parameterValues);

            return true;
        }
        private static void PrepareAndExecuteQuery<T>(ref List<T> entityObj, string queryContent, string[] parameterNames, object[] parameterValues)
        {
            GN_KASIFEntities EntityObj = new GN_KASIFEntities();
            SqlParameter[] SqlParameters = new SqlParameter[parameterNames.Length];

            for (int i = 0; i < parameterNames.Length; i++)
            {
                SqlParameters[i] = new SqlParameter(parameterNames[i], parameterValues[i]);
            }

            entityObj = EntityObj.Database.SqlQuery<T>(queryContent, SqlParameters).ToList<T>();

        }

        private static string GetQuery(ConstDbCommands.DbCommandList queryName)
        {
            switch (queryName)
            {
                case ConstDbCommands.DbCommandList.GET_USER_BY_EMAIL: return ConstDbCommands.GET_USER_BY_EMAIL;
                case ConstDbCommands.DbCommandList.cmd2:
                    break;
                case ConstDbCommands.DbCommandList.cmd3:
                    break;
                default:
                    break;
            }

            return "";
        }


    }
}
