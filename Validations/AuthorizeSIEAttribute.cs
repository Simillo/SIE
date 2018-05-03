using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using SIE.Enums;
using SIE.Helpers;

namespace SIE.Validations
{
    public class AuthorizeSIEAttribute : ActionFilterAttribute
    {
        private readonly EProfile _profile;

        public AuthorizeSIEAttribute(EProfile profile)
        {
            _profile = profile;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                var idProfile = filterContext.HttpContext.Session.GetInt32("_profile");
                if (idProfile != (int) _profile)
                {
                    throw new UnauthorizedAccessException("Não autorizado!");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Não autorizado!");
            }
        }
    }
}
