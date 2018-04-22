using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SIE.Auxiliary;
using SIE.Context;

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
            if (!HttpContext.Session.IsAuth())
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}