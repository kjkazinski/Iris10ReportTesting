using System;
using System.Collections.Generic;

using IrisWeb.Code.Data.Models.Database;

namespace IrisWeb.Code.Data.Services
{
    public class SecurityUserService : DatabaseServiceBase<SecurityUserModel>
    {
        public IList<SecurityUserDropDown> GetDropDownList()
        {
            return ModelBase.LoadModel<SecurityUserDropDown>() as IList<SecurityUserDropDown>;
        }

        public string CurrentSecurityUser()
        {
            IrisWeb.SqlGenerator sqlgen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "SecurityUser");
            sqlgen.AddField("SecurityUser_Key");
            sqlgen.AddField("UserName");
            sqlgen.SelectStatementLimit = 1;

            IList<SecurityUserDropDown> currentSecurityUser = (ModelBase.LoadModel<SecurityUserDropDown>(sqlgen) as IList<SecurityUserDropDown>);

            if (currentSecurityUser == null || currentSecurityUser.Count == 0) return "32000EDD73";  // Ginger
            else return currentSecurityUser[0].SecurityUser_Key;

        }
    }
}
