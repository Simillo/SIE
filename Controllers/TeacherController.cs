using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIE.Auxiliary;
using SIE.Business;
using SIE.Context;
using SIE.Enums;
using SIE.Models;
using SIE.Utils;
using SIE.Validations;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    [AuthorizeSIE(EProfile.Teacher)]
    public class TeacherController : Controller
    {
        private readonly BRoom _bRoom;
        private readonly URoom _uRoom;
        public TeacherController(SIEContext context)
        {
            _bRoom = new BRoom(context);
            _uRoom = new URoom(context);
        }
        [HttpGet]
        [Route("Load")]
        public IActionResult Load()
        {
            return Ok();
        }

        [HttpPost]
        [Route("CreateRoom")]
        public IActionResult CreateRoom([FromBody] MNewRoom newRoom)
        {
            var isValidRoom = newRoom.ValidRoom(_uRoom);
            if (!isValidRoom)
                return BadRequest();

            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();
            _bRoom.Save(newRoom, authenticatedUserId);


            return Ok(true);
        }
    }
}