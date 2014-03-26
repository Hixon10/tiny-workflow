using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Bizagi.Controllers
{
    public class BaseController : Controller
    {
        public static List<string> GetErrorListFromModelState (ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
    }
}