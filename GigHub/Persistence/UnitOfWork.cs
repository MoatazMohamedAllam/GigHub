using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IAttendeesRepository Attendees { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Followings = new FollowingRepository(_context);
            Genres = new GenreRepository(_context);
            Attendees = new AttendeesRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}