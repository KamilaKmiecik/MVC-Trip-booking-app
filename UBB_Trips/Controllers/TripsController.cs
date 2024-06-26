﻿using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.Models;
using UBB_Trips.Services;
using UBB_Trips.ViewModels;

namespace UBB_Trips.Controllers
{
    [Authorize]
    public class TripsController : Controller
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
        }

        // GET: Trips
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var trips = await _tripService.GetTripsPerPageAsync(page, pageSize, searchQuery);
            var totalTrips = await _tripService.GetTotalNumberOfTripsAsync();

            ViewBag.TotalTrips = totalTrips;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;
            return View(trips.ToList().Adapt<IEnumerable<TripViewModel>>());
        }

        // GET: Trips/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _tripService.GetTripByIdAsync(id.Value);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip.Adapt<TripViewModel>());
        }

        // GET: Trips/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,TripSTART,TripEND,Title,Description,Type,Food,Country,ImageURL,Alt")] TripViewModel tripViewModel)
        {
            if (ModelState.IsValid)
            {
                await _tripService.AddAsync(tripViewModel.Adapt<Trip>());
                return RedirectToAction(nameof(Index));
            }
            return View(tripViewModel);
        }

        // GET: Trips/Edit/5
        [Authorize(Roles = "Admin, Booking Agent")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _tripService.GetTripByIdAsync(id.Value);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip.Adapt<TripViewModel>());
        }

        // POST: Trips/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Booking Agent")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TripSTART,TripEND,Title,Description,Type,Food,Country,ImageURL,Alt")] TripViewModel tripViewModel)
        {
            if (id != tripViewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _tripService.UpdateAsync(tripViewModel.Adapt<Trip>());
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException e)
                {
                    // Handle exception
                    return View(tripViewModel);
                }
            }
            return View(tripViewModel);
        }

        // GET: Trips/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _tripService.GetTripByIdAsync(id.Value);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip.Adapt<TripViewModel>());
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tripService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
