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
using Microsoft.Extensions.Configuration;
using SIE.Dashboard;
using SIE.Helpers;

namespace SIE.Controllers
{
    [Route("api/[controller]")]
    [AuthorizeSIE(EProfile.Teacher)]
    public class TeacherController : Controller
    {
        private readonly BHistory _bHistory;
        private readonly BActivity _bActivity;
        private readonly BRoom _bRoom;
        private readonly BAnswer _bAnswer;
        private readonly BDocument _bDocument;
        private readonly BRelUploadActivity _bRelUploadActivity;

        private readonly UActivity _uActivity;
        private readonly URoom _uRoom;
        private readonly UAnswer _uAnswer;
        private readonly URelUploadActivity _uRelUploadActivity;
        private readonly URelUploadAnswer _uRelUploadAnswer;
        private readonly URelStudentRoom _uRelStudentRoom;

        private readonly IConfiguration _configuration;

        public TeacherController(SIEContext context, IConfiguration configuration)
        {
            _bHistory = new BHistory(context);
            _bActivity = new BActivity(context);
            _bRoom = new BRoom(context);
            _bAnswer = new BAnswer(context);
            _bDocument = new BDocument(context);
            _bRelUploadActivity = new BRelUploadActivity(context);

            _uActivity = new UActivity(context);
            _uRoom = new URoom(context);
            _uAnswer = new UAnswer(context);
            _uRelUploadActivity = new URelUploadActivity(context);
            _uRelUploadAnswer = new URelUploadAnswer(context);
            _uRelStudentRoom = new URelStudentRoom(context);

            _configuration = configuration;
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

            if (newRoom.ExpirationDate != null && newRoom.ExpirationDate < DateTime.Now)
                return BadRequest(ResponseContent.Create(errors, HttpStatusCode.BadRequest, $"A data do fim da sala deve ser maior que hoje, {DateTime.Now:dd/MM/yyyy}!"));

            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();
            _bRoom.Save(newRoom, authenticatedUserId);

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
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A sala com código \"{roomCode}\" não existe!"));

            if (room.Person.Id != authenticatedUserId)
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala!"));

            if (room.CurrentState == (int) ERoomState.Open)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A sala já está aberta!"));

            if (room.CurrentState == (int) ERoomState.Closed)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A não pode ser aberta depois de fechada!"));

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

            if (room.Person.Id != authenticatedUserId)
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala!"));

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

            if (room.Person.Id != authenticatedUserId)
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala!"));


            var activities = _uActivity.GetByRoom(room.Id);

            return Ok(ResponseContent.Create(new MRoomView(room, activities), HttpStatusCode.OK, null));
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

            if (activity.Person.Id != sessionPersonId || room.Person.Id != sessionPersonId)
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala/atividade!"));

            var answers = _uAnswer.GetByActivity(activity.Id);
            var uploads = _uRelUploadActivity.GetByActivity(activity.Id);
            var response = new MViewActivity(activity, null, answers, uploads)
            {
                Answers = answers?.Select(a => new MViewAnswer(a, _uRelUploadAnswer.GetByAnswer(a.Id))).ToList() ?? new List<MViewAnswer>()
            };

            return Ok(ResponseContent.Create(response, HttpStatusCode.OK, null));
        }

        [HttpPost]
        [Route("SaveActivity/{roomCode}")]
        public IActionResult SaveActivity([FromBody] MNewActivity activity, string roomCode)
        {
            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();
            var room = _uRoom.GetByCode(roomCode);
            if (room == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A sala com código \"{roomCode}\" não existe!"));

            if (room.Person.Id != authenticatedUserId)
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa sala!"));

            if (room.CurrentState == (int) ERoomState.Closed)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Essa sala está fechada!"));

            if (activity.ExpirationDate != null)
            {
                if (activity.ExpirationDate < DateTime.Now)
                    return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A data do fim da atividade deve ser maior que hoje, {DateTime.Now:dd/MM/yyyy}!"));

                if (room.ExpirationDate != null && activity.ExpirationDate > room.ExpirationDate)
                    return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A data de fim da atividade não pode ser maior que a data fim da sala!"));
            }

            if (activity.Weight <= 0.0)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "O peso da atividade deve ser um número maior que 0!"));

            var roomActivities = _uActivity.GetByRoom(room.Id);
            var sumRoomActivities = roomActivities.Sum(a => a.Weight);

            if (sumRoomActivities >= 100)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "Não é possível adicionar mais atividades pois já está no limite de 100 pontos (100%)!"));

            if (sumRoomActivities + activity.Weight > 100)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"O peso da atividade não pode ser maior que {100 - sumRoomActivities}, para não ultrapassar o limite da sala!"));

            var bdActivity = _bActivity.SaveOrUpdate(activity, room);

            if (activity.Files != null)
            {
                var filesName = FileExtensions.CopyFromTo(activity.Files, _configuration["Directory:TEMP"], _configuration["Directory:UPLOAD"]);
                var documents = _bDocument.Save(filesName, bdActivity.Person);
                _bRelUploadActivity.DeactivateByActivity(activity.Id);
                _bRelUploadActivity.Save(documents, bdActivity);
            }

            var msgTypePastTense = activity.Id > 0 ? "editou" : "criou";
            var msgType = activity.Id > 0 ? "editada" : "criada";

            _bHistory.SaveHistory(authenticatedUserId, $"Usuário {msgTypePastTense} atividade");

            return Ok(ResponseContent.Create(bdActivity.Id, HttpStatusCode.Created, $"Atividade {msgType} com sucesso!"));
        }


        [HttpGet]
        [Route("InitiateActivity/{activityId}")]
        public IActionResult InitiateActivity(int activityId)
        {
            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();
            var activity = _uActivity.GetById(activityId);

            if (activity == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A atividade não existe!"));

            if (activity.Person.Id != authenticatedUserId)
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa atividade!"));

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

            if (activity.Person.Id != authenticatedUserId)
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa atividade!"));

            if (activity.CurrentState != (int)EActivityState.InProgress)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A atividade não pode ser encerrada pois ela está {((EActivityState)activity.CurrentState).Description().ToLower()}!"));

            activity.CurrentState = (int)EActivityState.Done;
            activity.EndDate = DateTime.Now;
            _bActivity.SaveOrUpdate(activity);
            _bHistory.SaveHistory(authenticatedUserId, "Usuário finalizou uma atividade");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Atividade foi encerrada!"));
        }

        [HttpPost]
        [Route("Evaluate/{roomCode}")]
        public IActionResult Evaluate([FromBody] MTeacherAnswer mAnswer, string roomCode)
        {
            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();

            var answer = _uAnswer.GetById(mAnswer.Id);

            if (answer == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A resposta não existe!"));

            var activity = _uActivity.GetById(answer.Activity.Id);

            if (activity == null)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A atividade não existe!"));

            if (activity.Person.Id != authenticatedUserId)
                return StatusCode((int)HttpStatusCode.Unauthorized, ResponseContent.Create(null, HttpStatusCode.Unauthorized, "Você não tem acesso a essa atividade!"));

            if (activity.CurrentState != (int)EActivityState.InProgress)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A atividade não pode ser avaliada pois ela está {((EActivityState)activity.CurrentState).Description().ToLower()}!"));

            if (mAnswer.Grade <= 0.0)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, "A nota da atividade deve ser maior que 0!"));

            if (mAnswer.Grade > activity.Weight)
                return BadRequest(ResponseContent.Create(null, HttpStatusCode.BadRequest, $"A nota da atividade deve ser menor que o peso da atividade, {activity.Weight}!"));

            answer.EvaluatedDate = DateTime.Now;
            answer.Feedback = mAnswer.Feedback;
            answer.Grade = mAnswer.Grade;

            _bAnswer.Update(answer);
            _bHistory.SaveHistory(authenticatedUserId, "Usuário avaliou uma resposta");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Resposta foi avaliada!"));
        }

        [HttpGet]
        [Route("LoadDashboard")]
        public IActionResult LoadDashboard()
        {
            var authenticatedUserId = HttpContext.Session.GetSessionPersonId();

            var rooms = _uRoom.GetByOwner(authenticatedUserId);
            var activities = _uActivity.GetByUser(authenticatedUserId);
            var relStudentsRoom = _uRelStudentRoom.GetByRelByTeacher(authenticatedUserId);
            var dashboards = new
            {
                rooms = new DashboardRoomsTeacher().CreateGraph(rooms),
                activities = new DashboardActivitiesTeacher().CreateGraph(activities),
                studentsXRoom = new DashboardStudentsXRoom().CreateGraph(relStudentsRoom)
            };
            return Ok(ResponseContent.Create(dashboards, HttpStatusCode.OK, null));
        }
    }
}