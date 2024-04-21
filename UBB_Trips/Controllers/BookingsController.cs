using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using UBB_Trips.Models;
using UBB_Trips.Services;
using UBB_Trips.ViewModels;

namespace UBB_Trips.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        }

        // GET: Bookings
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var bookings = await _bookingService.GetBookingsPerPageAsync(page, pageSize);
            var totalBookings = await _bookingService.GetTotalNumberOfBookingsAsync();

            ViewBag.TotalBookings = totalBookings;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;
            return View(bookings);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetByIdAsync(id.Value);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public async Task<IActionResult> Create()
        {
            var clients = await _bookingService.GetAllAsync();
            ViewBag.ClientIds = new SelectList(clients, "Id", "Name");
           // ViewBag.Trips = await _bookingService.GetTripsAsync();
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,NumberOfBookings")] BookingViewModel booking)
        {
            if (ModelState.IsValid)
            {
                await _bookingService.AddAsync(booking);
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetByIdAsync(id.Value);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,NumberOfBookings")] BookingViewModel booking)
        {
            if (id != booking.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bookingService.UpdateAsync(booking);
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetByIdAsync(id.Value);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookingService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
