using System;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UBB_Trips.Data;
using UBB_Trips.Models;
using UBB_Trips.Services;
using UBB_Trips.ViewModels;

namespace UBB_Trips.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IBookingService _bookingService; 

        public ClientsController(IClientService clientService, IBookingService bookingService) 
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        }

        // GET: Clients
        [Authorize(Roles ="Admin, Booking Agent")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var clients = await _clientService.GetClientsPerPageAsync(page, pageSize);
            var totalClients = await _clientService.GetTotalNumberOfClients();

            var clientList = clients.ToList();

            ViewBag.TotalClients = totalClients;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;
            return View(clientList.Adapt<IEnumerable<ClientViewModel>>());
        }


        // GET: Clients/Details/5
        [Authorize(Roles = "Admin, Booking Agent")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientService.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client.Adapt<ClientViewModel>());
        }

        // GET: Clients/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Email")] ClientViewModel client)
        {
            if (ModelState.IsValid)
            {
                await _clientService.AddAsync(client.Adapt<Client>());
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "Admin, Booking Agent")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientService.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            ViewBag.Bookings = (await _bookingService.GetAllAsync()).Adapt<IEnumerable<BookingViewModel>>(); // Set the list of bookings in ViewBag

            return View(client.Adapt<ClientViewModel>());
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin, Booking Agent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Email")] ClientViewModel client)
        {
            if (id != client.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _clientService.UpdateAsync(client.Adapt<Client>());
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle exception
                    return View(client);
                }
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientService.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client.Adapt<ClientViewModel>());
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clientService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
