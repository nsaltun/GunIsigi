using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.DB_Operations.DBObjects
{
    public class ConstDbCommands
    {
        public const string GET_QUERY_CONTENT = "SELECT * " +
                                                "FROM " +
                                                    "QUERY_TABLE " +
                                                "WHERE " +
                                                    "STATUS = 1 AND " +
                                                    "QUERY_NAME = @P_QUERY_NAME";

        //public const string GET_USER_BY_EMAIL = "SELECT * " +
        //                                        "FROM " +
        //                                        "	USER_USER " +
        //                                        "WHERE " +
        //                                        "	STATUS=1 AND " +
        //                                        "	EMAIL = @P_EMAIL AND " +
        //                                        "   PASSWORD = @P_PASSWORD ";

        public enum DbCommandList
        {
            GET_QUERY_CONTENT,
            GET_USER_BY_EMAIL,
            GET_PAGE_OGR_BILGI,
            GET_HOCA_BILGI,
            GET_BOLGE_INFO,
            GET_DERS_BILGI,
            GET_DERS_KONU_BILGI,
            GET_MUFREDAT_BILGI
        }


    }
}
