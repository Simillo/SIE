using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIE.Auxiliary;
using SIE.Context;
using SIE.Enums;
using SIE.Helpers;

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
        public IActionResult Load()
        {
            var profileId = HttpContext.Session.GetInt32("_profile");
            if (!HttpContext.Session.IsAuth() || profileId != (int)EProfile.Teacher)
            {
                return Unauthorized();
            }

            return Ok(profileId);
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