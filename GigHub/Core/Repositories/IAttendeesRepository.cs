using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHub.Core.Repositories
{
    public interface IAttendeesRepository
    {
        bool AnyAttendees(string userId, int gigId);
        void Add(Attendance attendance);
    }
}
