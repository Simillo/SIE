using System;
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
        private readonly BHistory _bHistory;
        private readonly BRoom _bRoom;
        private readonly URoom _uRoom;
        public TeacherController(SIEContext context)
        {
            _bHistory = new BHistory(context);
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

            var history = new History
            {
                PersonId = authenticatedUserId,
                Action = "Usuário criou uma nova sala",
                DateAction = DateTime.Now
            };

            _bHistory.SaveHistory(history);

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

        [HttpGet]
        [Route("LoadRoom/{roomCode}")]
        public IActionResult LoadRoom(string roomCode)
        {
            var room = _uRoom.SearchByCode(roomCode);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"Sala com código \"{roomCode}\" não existe!"));
            
            if (room.PersonId != HttpContext.Session.GetSessionPersonId())
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, $"Você não tem acesso a essa sala!"));

            return Ok(ResponseContent.Create(room, HttpStatusCode.OK, null));
        }
    }
}