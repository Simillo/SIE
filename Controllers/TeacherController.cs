using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIE.Auxiliary;
using SIE.Context;
using SIE.Enums;
using SIE.Validations;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    [AuthorizeSIE(EProfile.Teacher)]
    public class TeacherController : Controller
    {
        public TeacherController(SIEContext context)
        {

        }
        [HttpGet]
        [Route("Load")]
        public IActionResult Load()
        {
            return Ok(1);
        }
    }
}