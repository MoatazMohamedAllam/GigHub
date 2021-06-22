using GigHub.Core.Repositories;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.GetUpCommingGigsForAudience();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                                    .Where(g => g.Artist.Name.Contains(query)
                                    || g.Genre.Name.Contains(query)
                                    || g.Venue.Contains(query));    
            }

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query
            };
            return View("Gigs",viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}