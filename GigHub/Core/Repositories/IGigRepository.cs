using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGigById(int id);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int id);
        IEnumerable<Gig> GetUpCommingGigs(string userId);
        IEnumerable<Gig> GetUpCommingGigsForAudience();
    }
}