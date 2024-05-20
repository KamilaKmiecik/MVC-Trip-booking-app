using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Net.WebSockets;
using UBB_Trips.Models;
using UBB_Trips.Services;
using UBB_Trips.ViewModels;

namespace UBB_Trips.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IBookingService _bookingService;
        private readonly IClientService _clientService;
        private readonly ITripService _tripService;

        public HomeController(ILogger<HomeController> logger, ITripService tripService, IBookingService bookingService, IClientService clientService)
        {
            _logger = logger;
            _tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Detail(int id)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DashBoard()
        {
            var trips = await _tripService.GetAllAsync();
            var bookings = await _bookingService.GetAllAsync();
            var clients = await _clientService.GetAllAsync();

            ViewBag.Bookings = bookings.Adapt<IEnumerable<BookingViewModel>>();
            ViewBag.Clients = clients.Adapt<IEnumerable<ClientViewModel>>();
            ViewBag.Trips = trips.ToList().Adapt<IEnumerable<TripViewModel>>(); 
            return View();
           // return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}