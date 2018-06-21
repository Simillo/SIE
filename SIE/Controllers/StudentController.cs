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
using Microsoft.Extensions.Configuration;
using SIE.Helpers;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    [AuthorizeSIE(EProfile.Student)]
    public class StudentController : Controller
    {
        private readonly BHistory _bHistory;
        private readonly BRelStudentRoom _bRelStudentRoom;
        private readonly BAnswer _bAnswer;
        private readonly BRoom _bRoom;
        private readonly BDocument _bDocument;
        private readonly BRelUploadAnswer _bRelUploadAnswer;

        private readonly UActivity _uActivity;
        private readonly URelStudentRoom _uRelStudentRoom;
        private readonly UAnswer _uAnswer;
        private readonly URoom _uRoom;
        private readonly URelUploadActivity _uRelUploadActivity;
        private readonly URelUploadAnswer _uRelUploadAnswer;

        private readonly IConfiguration _configuration;


        public StudentController(SIEContext context, IConfiguration configuration)
        {
            _bHistory = new BHistory(context);
            _bRelStudentRoom = new BRelStudentRoom(context);
            _bRoom = new BRoom(context);
            _bAnswer = new BAnswer(context);
            _bDocument = new BDocument(context);
            _bRelUploadAnswer = new BRelUploadAnswer(context);

            _uActivity = new UActivity(context);
            _uRelStudentRoom = new URelStudentRoom(context);
            _uRoom = new URoom(context);
            _uAnswer = new UAnswer(context);
            _uRelUploadActivity = new URelUploadActivity(context);
            _uRelUploadAnswer = new URelUploadAnswer(context);

            _configuration = configuration;
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
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Você já está nessa sala!"));

            _bRelStudentRoom.Save(authenticatedPersonId, room.Id);
            room.NumberOfStudents++;
            _bRoom.SaveOrUpdate(room);
            _bHistory.SaveHistory(authenticatedPersonId, "Usuário entrou em uma sala");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Você entrou na sala!"));
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
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não pode sair dessa sala pois não está cadastrado nelas!"));

            _bRelStudentRoom.Exit(authenticatedPersonId, room.Id);
            room.NumberOfStudents--;
            _bRoom.SaveOrUpdate(room);
            _bHistory.SaveHistory(authenticatedPersonId, "Usuário saiu de uma sala");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Você saiu da sala!"));
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
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala pois ela não está aberta!"));

            if (!roomsIds.Contains(room.Id))
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala pois não está cadastrado nela!"));

            var activities = _uActivity
                .GetByRoom(room.Id)
                .Where(r => r.CurrentState != (int)EActivityState.Building);

            var answers = _uAnswer.GetByActivity(activities.Select(a => a.Id).ToList(), authenticatedPersonId);
            return Ok(ResponseContent.Create(new MRoomView(room, activities, answers), HttpStatusCode.OK, null));
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
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala pois ela esta fechada!"));

            if (activity.CurrentState == (int)EActivityState.Building)
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa atividade!"));

            if (!roomsIds.Contains(room.Id))
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala/atividade!"));

            var answer = _uAnswer.GetByUser(activityId, authenticatedPersonId);
            var uploads = _uRelUploadActivity.GetByActivity(activity.Id);
            var attachments = _uRelUploadAnswer.GetByAnswer(answer.Id);
            var response = new MViewActivity(activity, answer, null, uploads)
            {
                Answer =
                {
                    Attachments = attachments?.Select(a => a.Document.FileName).ToList()
                }
            };
            return Ok(ResponseContent.Create(response, HttpStatusCode.OK, null));
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
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala pois ela esta fechada!"));

            if (activity.CurrentState == (int)EActivityState.Building)
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa atividade!"));

            if (!roomsIds.Contains(room.Id))
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala/atividade!"));

            if (string.IsNullOrEmpty(answer.Answer))
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "É obrigatório responder a atividade!"));

            if (_uAnswer.GetByUser(activity.Id, authenticatedPersonId) != null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Você já respondeu essa atividade!"));

            var answerDb = _bAnswer.Save(authenticatedPersonId, activity.Id, room.Id, answer.Answer);

            if (answer.Attachments != null)
            {
                var filesName = FileExtensions.CopyFromTo(answer.Attachments, _configuration["Directory:TEMP"], _configuration["Directory:UPLOAD"]);
                var documents = _bDocument.Save(filesName, answerDb.Person);
                _bRelUploadAnswer.Save(documents, answerDb);
            }

            _bHistory.SaveHistory(authenticatedPersonId, "Usuário respondeu a uma atividade");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Atividade respondida!"));
        }
    }
}