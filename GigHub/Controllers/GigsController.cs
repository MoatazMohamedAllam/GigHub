using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Persistence;
using GigHub.Core.Repositories;
using GigHub.Core.ViewModels;
using GigHub.Core.Models;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetUpCommingGigs(userId);

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetGigsUserAttending(userId);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Who I'm Attending"
            };
            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var followees = _unitOfWork.Followings.GetAllArtistFollowers(userId);

            return View(followees);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genres.GetAllGenres(),
                Heading = "Add Gig"
            };
            return View("GigForm", viewModel);
        }


        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetAllGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistID = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigById(id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genres.GetAllGenres(),
                Id = gig.Id,
                Date = gig.DateTime.ToString("dd MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetAllGenres();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistID != userId)
                return new HttpUnauthorizedResult();


            gig.Modify(viewModel.GetDateTime(),viewModel.Venue,viewModel.Genre);

            //gig.Venue = viewModel.Venue;
            //gig.DateTime = viewModel.GetDateTime();
            //gig.GenreId = viewModel.Genre;

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }
    }
}
