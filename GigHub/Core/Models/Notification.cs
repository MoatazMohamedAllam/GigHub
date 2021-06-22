using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {

        }

        private Notification(Gig gig,NotificationType type)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");
            
            Gig = gig;
            DateTime = DateTime.Now;
            Type = type;
        }


        //Factory Methods******************
        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);
        }

        public static Notification GigUpdated(Gig newGig,DateTime originalDateTime,string originalVenue)
        {
            var notification = new Notification(newGig, NotificationType.GigUpdated);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

    }
}