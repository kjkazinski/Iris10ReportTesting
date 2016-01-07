using System.Collections.Generic;
using IrisWeb.Code.Data.Models.Database;
using System.Web.Mvc;

namespace IrisWeb.Code.Data.Services
{
    [RoutePrefix("API/Activity")]
    public class ActivityService : DatabaseServiceBase<ActivityModel>
    {
        public IList<ActivityDropDown> GetDropDownList(string pageName)
        {
            using (SecurityUserService securityUserService = new SecurityUserService())
            {
                SqlGenerator sqlgen = new SqlGenerator(SqlGenerator.SqlTypes.Sproc, "ActivityListUSP");
                sqlgen.AddField("ReturnKey", "ActivityListUSP", 1);
                sqlgen.AddField("UserKey", "ActivityListUSP", securityUserService.CurrentSecurityUser());
                sqlgen.AddField("SecurityObjectTitle", "ActivityListUSP", pageName);
                sqlgen.AddField("Active", "ActivityListUSP", 1);

                return ModelBase.LoadModel<ActivityDropDown>(sqlgen) as IList<ActivityDropDown>;
            }
        }
    }
}
