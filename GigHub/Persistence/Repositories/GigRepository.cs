using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;
        
        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                               .Where(a => a.AttendeeId == userId)
                               .Select(a => a.Gig)
                               .Include(g => g.Artist)
                               .Include(g => g.Genre)
                               .ToList();
        }

        public IEnumerable<Gig> GetUpCommingGigs(string userId)
        {
            return _context.Gigs
                    .Where(g => g.ArtistID == userId && g.DateTime > DateTime.Now && !g.IsCanceled)
                    .Include(g => g.Genre)
                    .ToList();
        }

        public IEnumerable<Gig> GetUpCommingGigsForAudience()
        {
           return _context.Gigs
                   .Include(g => g.Artist)
                   .Include(g => g.Genre)
                   .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
        }

        public Gig GetGigById(int id)
        {
            return _context.Gigs
                    .SingleOrDefault(g => g.Id == id);
        }

        public Gig GetGigWithAttendees(int id)
        {
            return _context.Gigs
                   .Include(g => g.Attendances)
                   .SingleOrDefault(g => g.Id == id);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}