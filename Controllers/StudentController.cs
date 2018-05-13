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
    [AuthorizeSIE(EProfile.Student)]
    public class StudentController : Controller
    {
        private readonly BHistory _bHistory;
        private readonly BActivity _bActivity;
        private readonly UActivity _uActivity;
        private readonly URelStudentRoom _uRelStudentRoom;
        private readonly BRoom _bRoom;
        private readonly URoom _uRoom;
        private readonly BRelStudentRoom _bRelStudentRoom;

        public StudentController(SIEContext context)
        {
            _bActivity = new BActivity(context);
            _bHistory = new BHistory(context);
            _uActivity = new UActivity(context);
            _uRelStudentRoom = new URelStudentRoom(context);
            _bRoom = new BRoom(context);
            _uRoom = new URoom(context);
            _bRelStudentRoom = new BRelStudentRoom(context);
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
                .Where(r => r.CurrentState == (int) ERoomState.Open)
                .Select(r => new MAllRoomsView(r, !roomsIds.Contains(r.Id)));

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
            _bHistory.SaveHistory(authenticatedPersonId, "Usuário entrou em uma sala");

            return Ok(ResponseContent.Create(null, HttpStatusCode.OK, "Você entrou na sala!"));

        }
    }
}