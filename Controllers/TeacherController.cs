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
using Activity = System.Diagnostics.Activity;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    [AuthorizeSIE(EProfile.Teacher)]
    public class TeacherController : Controller
    {
        private readonly BHistory _bHistory;
        private readonly BActivity _bActivity;
        private readonly UActivity _uActivity;
        private readonly BRoom _bRoom;
        private readonly URoom _uRoom;

        public TeacherController(SIEContext context)
        {
            _bActivity = new BActivity(context);
            _bHistory = new BHistory(context);
            _uActivity = new UActivity(context);
            _bRoom = new BRoom(context);
            _uRoom = new URoom(context);
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
            var rooms = _uRoom
                .GetByOwner(authenticatedUserId)
                .Select(r => new MMyRoomsView(r));
            return Ok(ResponseContent.Create(rooms, HttpStatusCode.OK, null));
        }

        [HttpGet]
        [Route("LoadRoom/{roomCode}")]
        public IActionResult LoadRoom(string roomCode)
        {
            var room = _uRoom.GetByCode(roomCode);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A sala com código \"{roomCode}\" não existe!"));

            if (room.PersonId != HttpContext.Session.GetSessionPersonId())
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized,
                    "Você não tem acesso a essa sala!"));

            return Ok(ResponseContent.Create(new MRoomView(room), HttpStatusCode.OK, null));
        }

        [HttpPost]
        [Route("SaveActivity/{roomCode}")]
        public IActionResult SaveActivity([FromBody] MNewActivity activity, string roomCode)
        {
            var sessionPersonId = HttpContext.Session.GetSessionPersonId();
            var room = _uRoom.GetByCode(roomCode);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest,
                    $"A sala com código \"{roomCode}\" não existe!"));

            if (room.PersonId != sessionPersonId)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized,
                    "Você não tem acesso a essa sala!"));

            if (room.CurrentState == (int) ERoomState.Closed)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Essa sala está fechada!"));

            if (activity.ExpirationDate != null)
            {
                if (activity.ExpirationDate < DateTime.Now)
                    return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest,
                        "A data de fim da atividade não pode ser menor que a data atual!"));

                if (room.ExpirationDate != null && activity.ExpirationDate > room.ExpirationDate)
                    return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest,
                        "A data de fim da atividade não pode ser maior que a data fim da sala!"));
            }

            if (activity.Weight <= 0.0)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest,
                    "O peso da atividade deve ser um número maior que 0!"));

            _bActivity.SaveOrUpdate(activity, room);
            var msgType = activity.Id > 0 ? "editada" : "criada";

            return Ok(ResponseContent.Create(null, HttpStatusCode.Created, $"Atividade {msgType} com sucesso!"));
        }

        [HttpGet]
        [Route("LoadActivity/{roomCode}/{activityId}")]
        public IActionResult LoadActivity(string roomCode, int activityId)
        {
            var sessionPersonId = HttpContext.Session.GetSessionPersonId();
            var activity = _uActivity.GetById(activityId);
            var room = _uRoom.GetByCode(roomCode);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A sala com código \"{roomCode}\" não existe!"));

            if (activity == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A atividade com código não existe!"));

            if (activity.PersonId != sessionPersonId || room.PersonId != sessionPersonId)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala/atividade!"));

            return Ok(ResponseContent.Create(new MViewActivity(activity), HttpStatusCode.OK, null));
        }
    }
}