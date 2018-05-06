using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIE.Auxiliary;
using SIE.Business;
using SIE.Context;
using SIE.Enums;
using SIE.Models;
using SIE.Utils;
using SIE.Validations;
using System.Net;
using SIE.Helpers;

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
            var errors = new List<MModelError>();
            newRoom.ValidRoom(_uRoom, ref errors);
            if (errors.Any())
                return BadRequest(ResponseContent.Create(errors, HttpStatusCode.BadRequest, "Campo(s) inválido(s)!"));

            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();
            _bRoom.Save(newRoom, authenticatedUserId);

            return Ok(ResponseContent.Create(null, HttpStatusCode.Created, "Sala criada com sucesso!"));
        }

        [HttpGet]
        [Route("MyRooms")]
        public IActionResult MyRooms()
        {
            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();
            var rooms = _uRoom.GetByOwner(authenticatedUserId)
                .Select(r => new
                {
                    r.Name,
                    r.Code,
                    CurrentState = ((ERoomState)r.CurrentState).Description(),
                    NumberOfStudents = r.NumberOfStudents == 0 ? "-" : r.NumberOfStudents.ToString()
                });
            return Ok(ResponseContent.Create(rooms, HttpStatusCode.OK, null));
        }
    }
}