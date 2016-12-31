using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects.CodeMgmt;
using KasifBusiness.Objects.ScreenObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;

namespace KasifBusiness.Business.User
{
    public class OUser
    {
        USER_USER UserObj;
        public OUser(USER_USER userObj)
        {
            this.UserObj = userObj;
        }

        public ResultObject OUserOperation()
        {
            try
            {
                ResultObject resultObj = new ResultObject();
                resultObj.isOk = true;
                List<USER_USER> lstUser = new List<USER_USER>();
                DbOperations.RunDbQuery<USER_USER>(ref lstUser, DbCommandList.GET_USER_USER, new string[] { "P_EMAIL" }, new object[] { UserObj.EMAIL.Trim() });

                if (lstUser.Count > 0)
                {
                    resultObj.errorMsg = "Bu email ile kayıtlı bir kullanıcı mevcut.";
                    resultObj.errPrefix = "Hata! ";
                    resultObj.isOk = false;
                    return resultObj;
                }

                DbOperations.Insert(UserObj);

                return resultObj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
