using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIE.Auxiliary;
using SIE.Context;
using SIE.Helpers;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        public DashboardController(SIEContext context)
        {

        }
        [HttpGet]
        [Route("Load")]
        public IActionResult Load()
        {
            var id = HttpContext.Session.GetString("_id");
            if (!HttpContext.Session.IsAuth())
            {
                return Unauthorized();
            }

            return Ok(id);
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