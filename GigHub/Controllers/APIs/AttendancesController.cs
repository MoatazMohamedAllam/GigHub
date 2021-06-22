using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("api/attendances")]
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _unitOfWork.Attendees.AnyAttendees(userId, dto.GigId);

            if (exists)
                return BadRequest("The attendance already exists.");

            var attendances = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = User.Identity.GetUserId()
            };

            _unitOfWork.Attendees.Add(attendances);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
