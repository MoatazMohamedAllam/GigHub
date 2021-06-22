using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GigHub.Persistence;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub.Controllers.APIs
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotification()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                                    .Where(nu => nu.UserId == userId && !nu.IsRead)
                                    .Select(nu => nu.Notification)
                                    .Include(n => n.Gig.Artist)
                                    .ToList();

            //with using AutoMapper

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);

            //without using AutoMapper
            
            //return notifications.Select(n => new NotificationDto()
            //{
            //    DateTime = n.DateTime,
            //    Gig = new GigDto
            //    {
            //        Artist = new UserDto
            //        {
            //            Id=n.Gig.Artist.Id,
            //            Name = n.Gig.Artist.Name
            //        },
            //        DateTime= n.Gig.DateTime,
            //        IsCanceled = n.Gig.IsCanceled,
            //        Venue = n.Gig.Venue
            //    },
            //    OriginalDateTime = n.OriginalDateTime,
            //    OriginalVenue = n.OriginalVenue,
            //    Type = n.Type
            //});
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                                        .Where(un => un.UserId == userId && !un.IsRead)
                                        .ToList();

            notifications.ForEach(n => n.Read());

            //to acheive same task

            //foreach (var notif in notifications)
            //{
            //    notif.IsRead = true;
            //}

            _context.SaveChanges();

            return Ok();

        }
    }
}
