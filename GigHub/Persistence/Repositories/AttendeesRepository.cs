using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.Repositories
{
    public class AttendeesRepository : IAttendeesRepository
    {
        private readonly ApplicationDbContext _context;
        public AttendeesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public bool AnyAttendees(string userId, int gigId)
        {
            return _context.Attendances
                    .Any(a => a.AttendeeId == userId && a.GigId == gigId);
        }
    }
}