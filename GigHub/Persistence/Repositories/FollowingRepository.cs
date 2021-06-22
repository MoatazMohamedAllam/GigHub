using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public bool AnyFollowing(string userId, string followeeId)
        {
            return _context.Followings
                                 .Any(f => f.FolloweeId == userId
                                    && f.FolloweeId == followeeId);
        }

        public IEnumerable<ApplicationUser> GetAllArtistFollowers(string userId)
        {
            return  _context.Followings
                    .Where(f => f.FollowerId == userId)
                    .Select(f => f.Followee)
                    .ToList();
        }
    }
}