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
    [AuthorizeSIE(EProfile.Student)]
    public class StudentController : Controller
    {
        private readonly BHistory _bHistory;

        private readonly BActivity _bActivity;
        private readonly BRelStudentRoom _bRelStudentRoom;
        private readonly BAnswer _bAnswer;
        private readonly BRoom _bRoom;

        private readonly UActivity _uActivity;
        private readonly URelStudentRoom _uRelStudentRoom;
        private readonly UAnswer _uAnswer;
        private readonly URoom _uRoom;

        public StudentController(SIEContext context)
        {
            _bActivity = new BActivity(context);
            _bHistory = new BHistory(context);
            _uActivity = new UActivity(context);
            _uRelStudentRoom = new URelStudentRoom(context);
            _uAnswer = new UAnswer(context);
            _bRoom = new BRoom(context);
            _uRoom = new URoom(context);
            _bRelStudentRoom = new BRelStudentRoom(context);
            _bAnswer = new BAnswer(context);
        }

        [HttpGet]
        [Route("Load")]
        public IActionResult Load()
        {
            return Ok(ResponseContent.Create(HttpContext.Session.GetCurrentPerson(), HttpStatusCode.OK, null));
        }

        [HttpGet]
        [Route("LoadRooms")]
        public IActionResult LoadRooms()
        {
            var authenticatedPersonId = HttpContext.Session.GetSessionPersonId();
            var roomsIds = _uRelStudentRoom.GetRoomIdByPersonId(authenticatedPersonId);

            var rooms = _uRoom
                .GetAll()
                .Where(r => r.CurrentState == (int)ERoomState.Open)
                .Select(r => new MAllRoomsView(r, !roomsIds.Contains(r.Id)));

            return Ok(ResponseContent.Create(rooms, HttpStatusCode.OK, null));
        }

        [HttpGet]
        [Route("LoadMyRooms")]
        public IActionResult LoadMyRooms()
        {
            var authenticatedPersonId = HttpContext.Session.GetSessionPersonId();
            var roomsIds = _uRelStudentRoom.GetRoomIdByPersonId(authenticatedPersonId);

            var rooms = _uRoom
                .GetAll()
                .Where(r => roomsIds.Contains(r.Id))
                .Select(r => new MMyRoomsView(r));

            return Ok(ResponseContent.Create(rooms, HttpStatusCode.OK, null));
        }

        [HttpGet]
        [Route("Join/{roomCode}")]
        public IActionResult Join(string roomCode)
        {
            var authenticatedPersonId = HttpContext.Session.GetSessionPersonId();
            var roomsIds = _uRelStudentRoom.GetRoomIdByPersonId(authenticatedPersonId);
            var room = _uRoom.GetByCode(roomCode);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A sala com código \"{roomCode}\" não existe!"));

            if (room.CurrentState != (int)ERoomState.Open)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Você não pode entrar nessa sala pois ela não está aberta!"));

            if (roomsIds.Contains(room.Id))
            {
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Você já está nessa sala!"));
            }

            _bRelStudentRoom.Save(authenticatedPersonId, room.Id);
            room.NumberOfStudents++;
            _bRoom.SaveOrUpdate(room);
            _bHistory.SaveHistory(authenticatedPersonId, "Usuário entrou em uma sala");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Você entrou na sala!"));
        }

        [HttpGet]
        [Route("LoadRoom/{roomCode}")]
        public IActionResult LoadRoom(string roomCode)
        {
            var authenticatedPersonId = HttpContext.Session.GetSessionPersonId();
            var room = _uRoom.GetByCode(roomCode);
            var roomsIds = _uRelStudentRoom.GetRoomIdByPersonId(authenticatedPersonId);

            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A sala com código \"{roomCode}\" não existe!"));
            
            if (room.CurrentState != (int)ERoomState.Open)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala pois ela não está aberta!"));

            if (!roomsIds.Contains(room.Id))
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala pois não está cadastrado nela!"));

            var activities = _uActivity
                .GetByRoom(room.Id)
                .Where(r => r.CurrentState != (int)EActivityState.Building);

            return Ok(ResponseContent.Create(new MRoomView(room, activities), HttpStatusCode.OK, null));
        }

        [HttpGet]
        [Route("ExitRoom/{roomCode}")]
        public IActionResult ExitRoom(string roomCode)
        {
            var authenticatedPersonId = HttpContext.Session.GetSessionPersonId();
            var room = _uRoom.GetByCode(roomCode);
            var roomsIds = _uRelStudentRoom.GetRoomIdByPersonId(authenticatedPersonId);

            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A sala com código \"{roomCode}\" não existe!"));

            if (!roomsIds.Contains(room.Id))
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não pode sair dessa sala pois não está cadastrado nelas!"));

            _bRelStudentRoom.Exit(authenticatedPersonId, room.Id);
            room.NumberOfStudents--;
            _bRoom.SaveOrUpdate(room);
            _bHistory.SaveHistory(authenticatedPersonId, "Usuário saiu de uma sala");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Você saiu da sala!"));
        }

        [HttpGet]
        [Route("LoadActivity/{roomCode}/{activityId}")]
        public IActionResult LoadActivity(string roomCode, int activityId)
        {
            var authenticatedPersonId = HttpContext.Session.GetSessionPersonId();
            var activity = _uActivity.GetById(activityId);
            var room = _uRoom.GetByCode(roomCode);
            var roomsIds = _uRelStudentRoom.GetRoomIdByPersonId(authenticatedPersonId);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A sala com código \"{roomCode}\" não existe!"));

            if (activity == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A atividade não existe!"));

            if (room.CurrentState != (int)ERoomState.Open)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala pois ela esta fechada!"));

            if (activity.CurrentState == (int)EActivityState.Building)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa atividade!"));

            if (!roomsIds.Contains(room.Id))
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala/atividade!"));

            return Ok(ResponseContent.Create(new MViewActivity(activity), HttpStatusCode.OK, null));
        }

        [HttpPost]
        [Route("Answer/{roomCode}/{activityId}")]
        public IActionResult Answer([FromBody] MAnswer answer, string roomCode, int activityId)
        {
            var authenticatedPersonId = HttpContext.Session.GetSessionPersonId();
            var activity = _uActivity.GetById(activityId);
            var room = _uRoom.GetByCode(roomCode);
            var roomsIds = _uRelStudentRoom.GetRoomIdByPersonId(authenticatedPersonId);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A sala com código \"{roomCode}\" não existe!"));

            if (activity == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A atividade não existe!"));

            if (room.CurrentState != (int)ERoomState.Open)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala pois ela esta fechada!"));

            if (activity.CurrentState == (int)EActivityState.Building)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa atividade!"));

            if (!roomsIds.Contains(room.Id))
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala/atividade!"));

            if (string.IsNullOrEmpty(answer.Answer))
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "É obrigatório responder a atividade!"));

            if (_uAnswer.GetByActivity(activity.Id) != null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Você já respondeu essa atividade!"));

            _bAnswer.Save(authenticatedPersonId, activity.Id, room.Id, answer.Answer);
            _bHistory.SaveHistory(authenticatedPersonId, "Usuário respondeu a uma atividade");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Atividade respondida!"));
        }
    }
}