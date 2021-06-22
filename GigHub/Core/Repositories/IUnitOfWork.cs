
namespace GigHub.Core.Repositories
{
    public interface IUnitOfWork
    {
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        IAttendeesRepository Attendees { get; }

        void Complete();
    }
}