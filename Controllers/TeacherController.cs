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
        private readonly SIEContext _context;
        public TeacherController(SIEContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Load")]
        public IActionResult Load()
        {
            return Ok();
        }
    }
}