using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }

        public bool ShowActions { get; set; }

        public string  Heading { get; set; }

        public string  SearchTerm { get; set; }
    }
}