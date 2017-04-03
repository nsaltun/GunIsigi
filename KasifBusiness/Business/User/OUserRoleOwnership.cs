using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Business.User
{
    public class OUserRoleOwnership : GenericRepository<GN_KASIFEntities,USER_ROLE_OWNERSHIP>
    {
        public USER_ROLE_OWNERSHIP FindByKey(System.Linq.Expressions.Expression<Func<USER_ROLE_OWNERSHIP, bool>> predicate)
        {
            return FindBy(predicate).FirstOrDefault<USER_ROLE_OWNERSHIP>();
        }
        
    }
}
