using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.DB_Operations.DBObjects
{
    public class ConstDbCommands
    {
        public const string GET_USER_BY_EMAIL = "SELECT * " +
                                                "FROM " +
                                                "	USER_USER " +
                                                "WHERE " +
                                                "	STATUS=1 AND " +
                                                "	EMAIL = @P_EMAIL AND " +
                                                "   PASSWORD = @P_PASSWORD ";

        public enum DbCommandList
        {
            GET_USER_BY_EMAIL,
            cmd2,
            cmd3
        }


    }
}
