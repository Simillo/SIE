using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIE.Auxiliary;
using SIE.Context;
using SIE.Enums;
using SIE.Validations;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    public class TeacherController : Controller
    {
        public TeacherController(SIEContext context)
        {

        }
        [HttpGet]
        [Route("Load")]
        [AuthorizeSIE(EProfile.Teacher)]
        public IActionResult Load()
        {
            //var profileId = HttpContext.Session.GetInt32("_profile");
            //if (!HttpContext.Session.IsAuth() || profileId != (int)EProfile.Teacher)
            //{
            //    return Unauthorized();
            //}

            return Ok(1);
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok();
        }
    }
}