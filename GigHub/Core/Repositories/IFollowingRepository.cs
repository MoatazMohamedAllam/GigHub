using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        IEnumerable<ApplicationUser> GetAllArtistFollowers(string userId);
        bool AnyFollowing(string userId, string followeeId);
        void Add(Following following);
    }
}