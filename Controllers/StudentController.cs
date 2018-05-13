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
        private readonly BRoom _bRoom;
        private readonly URoom _uRoom;
        private readonly URelStudentRoom _uRelStudentRoom;

        public StudentController(SIEContext context)
        {
            _bActivity = new BActivity(context);
            _bHistory = new BHistory(context);
            _uActivity = new UActivity(context);
            _bRoom = new BRoom(context);
            _uRoom = new URoom(context);
            _uRelStudentRoom = new URelStudentRoom(context);
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
            var roomId = _uRelStudentRoom.GetRoomIdByPersonId(authenticatedPersonId);

            var rooms = _uRoom
                .GetAll()
                .Where(r => r.CurrentState == (int) ERoomState.Open)
                .Select(r => new MAllRoomsView(r, !roomId.Contains(r.Id)));

            return Ok(ResponseContent.Create(rooms, HttpStatusCode.OK, null));
        }
    }
}