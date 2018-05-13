using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        [Route("Load")]
        public IActionResult Load()
        {
            return Ok(ResponseContent.Create(HttpContext.Session.GetCurrentPerson(), HttpStatusCode.OK, null));
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
            _bRoom.SaveOrUpdate(newRoom, authenticatedUserId);

            _bHistory.SaveHistory(authenticatedUserId, "Usuário criou uma nova sala");

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
        [Route("OpenRoom/{roomCode}")]
        public IActionResult OpenRoom(string roomCode)
        {
            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();
            var room = _uRoom.GetByCode(roomCode);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest,
                    $"A sala com código \"{roomCode}\" não existe!"));

            if (room.PersonId != authenticatedUserId)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized,
                    "Você não tem acesso a essa sala!"));

            if (room.CurrentState == (int) ERoomState.Open)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A sala já está aberta!"));

            if (room.CurrentState == (int) ERoomState.Closed)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest,
                    "A não pode ser aberta depois de fechada!"));

            room.CurrentState = (int) ERoomState.Open;
            room.StartDate = DateTime.Now;
            _bRoom.SaveOrUpdate(room);

            _bHistory.SaveHistory(authenticatedUserId, "Usuário abriu uma sala");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "A sala foi aberta com sucesso!"));
        }


        [HttpGet]
        [Route("CloseRoom/{roomCode}")]
        public IActionResult CloseRoom(string roomCode)
        {
            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();
            var room = _uRoom.GetByCode(roomCode);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A sala com código \"{roomCode}\" não existe!"));

            if (room.PersonId != authenticatedUserId)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala!"));

            if (room.CurrentState == (int)ERoomState.Closed)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A sala já está fechada!"));

            room.CurrentState = (int)ERoomState.Closed;
            room.EndDate = DateTime.Now;
            _bRoom.SaveOrUpdate(room);

            var activities = _uActivity.GetByRoom(room.Id);
            _bActivity.CloseAll(activities);
            _bHistory.SaveHistory(authenticatedUserId, "Usuário fechou uma sala");


            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "A sala foi fechada com sucesso!"));
        }


        [HttpGet]
        [Route("LoadRoom/{roomCode}")]
        public IActionResult LoadRoom(string roomCode)
        {
            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();

            var room = _uRoom.GetByCode(roomCode);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A sala com código \"{roomCode}\" não existe!"));

            if (room.PersonId != authenticatedUserId)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala!"));


            var activities = _uActivity.GetByRoom(room.Id);

            return Ok(ResponseContent.Create(new MRoomView(room, activities), HttpStatusCode.OK, null));
        }

        [HttpPost]
        [Route("SaveActivity/{roomCode}")]
        public IActionResult SaveActivity([FromBody] MNewActivity activity, string roomCode)
        {
            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();
            var room = _uRoom.GetByCode(roomCode);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest,
                    $"A sala com código \"{roomCode}\" não existe!"));

            if (room.PersonId != authenticatedUserId)
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

            _bHistory.SaveHistory(authenticatedUserId, "Usuário criou uma nova atividade");

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
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A atividade não existe!"));

            if (activity.PersonId != sessionPersonId || room.PersonId != sessionPersonId)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala/atividade!"));

            return Ok(ResponseContent.Create(new MViewActivity(activity), HttpStatusCode.OK, null));
        }

        [HttpGet]
        [Route("InitiateActivity/{activityId}")]
        public IActionResult InitiateActivity(int activityId)
        {
            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();
            var activity = _uActivity.GetById(activityId);

            if (activity == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A atividade não existe!"));

            if (activity.PersonId != authenticatedUserId)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa atividade!"));

            if (activity.CurrentState != (int)EActivityState.Building)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A atividade não pode ser iniciada pois ela está {((EActivityState)activity.CurrentState).Description().ToLower()}!"));

            activity.CurrentState = (int) EActivityState.InProgress;
            activity.StartDate = DateTime.Now;
            _bActivity.SaveOrUpdate(activity);

            _bHistory.SaveHistory(authenticatedUserId, "Usuário inciou uma atividade");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Atividade foi iniciada!"));
        }

        [HttpGet]
        [Route("FinalizeActivity/{activityId}")]
        public IActionResult FinalizeActivity(int activityId)
        {
            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();
            var activity = _uActivity.GetById(activityId);

            if (activity == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A atividade não existe!"));

            if (activity.PersonId != authenticatedUserId)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa atividade!"));

            if (activity.CurrentState != (int)EActivityState.InProgress)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A atividade não pode ser encerrada pois ela está {((EActivityState)activity.CurrentState).Description().ToLower()}!"));

            activity.CurrentState = (int)EActivityState.Done;
            activity.EndDate = DateTime.Now;
            _bActivity.SaveOrUpdate(activity);
            _bHistory.SaveHistory(authenticatedUserId, "Usuário finalizou uma atividade");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Atividade foi encerrada!"));
        }
    }
}