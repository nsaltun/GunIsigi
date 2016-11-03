using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.Objects.ScreenObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DbCommands = KasifBusiness.DB_Operations.DBObjects.ConstDbCommands.DbCommandList;

namespace KasifBusiness.Business.KasifPageOperations
{
    public class PageOperations
    {
        public List<T> RunQueryForPage<T>(DbCommands dbCommands, string[] paramNames, object[] paramValues)
        {
            try
            {
                List<T> lstPageObj = new List<T>();
                DbOperations.RunDbQuery<T>(ref lstPageObj, dbCommands, null, null);
                if (lstPageObj != null && lstPageObj.Count > 0)
                {
                    foreach (object item in lstPageObj)
                    {
                        Type type = item.GetType();
                        foreach (PropertyInfo subItem in type.GetProperties())
                        {
                            //subItem.GetValue(item).GetType() == typeof(string)
                            if (subItem.GetValue(item) == null && subItem.PropertyType == typeof(string))
                            {
                                subItem.SetValue(item, "");
                            }
                        }
                    }

                    return lstPageObj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
