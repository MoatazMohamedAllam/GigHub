using GigHub.Core.Repositories;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Principal;

namespace GigHub.Controllers.APIs
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);
                
                //_context.Gigs
                //        .Include(g => g.Attendances.Select(a => a.Attendee))
                //        .Single(g => g.Id == id && g.ArtistID == userId);

            if (gig.IsCanceled)
                return NotFound();

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }

         
    }
}
