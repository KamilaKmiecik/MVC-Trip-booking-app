using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UBB_Trips.Models;
using UBB_Trips.ViewModel;

namespace UBB_Trips.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private List<TripDetailViewModel> _trips = new List<TripDetailViewModel>
        {
            new TripDetailViewModel
            {
                ID = 1,
                Title = "Family Adventure in the Mountains",
                Description = "Plan a wonderful adventure in the mountains for the whole family.",
                TripSTART = new DateTime(2024,3,15),
                TripEND = new DateTime(2024, 3, 20),
                Type = TripTypeModel.Family,
                Food = FoodAcoomodationModel.FullBoard,
                Country = CountryModel.Poland,
                ImageURL = "https://dag08uxs564ub.cloudfront.net/images/mountain_ranges_in_poland.max-1280x768.jpg",
                Alt = "Mountains"
            },
            new TripDetailViewModel
            {
                ID = 2,
                Title = "Journey through Romantic Cities",
                Description = "Perfect trip for newlyweds, explore romantic places.",
                TripEND = new DateTime(2024,5,1),
                TripSTART = new DateTime(2024,4, 20), 
                Type = TripTypeModel.Honeymoon,
                Food = FoodAcoomodationModel.RoomOnly,
                Country = CountryModel.France,
                ImageURL = "https://worldwidehoneymoon.com/wp-content/uploads/2022/03/Petite-Venise-in-Colmar-1024x768.jpg",
                Alt = "romantic hotel image",
            },
            new TripDetailViewModel
            {
                ID = 3,
                Title = "Luxurious Experience on a Paradise Island",
                Description = "Relax and indulge in luxury on a paradise island.",
                TripSTART = new DateTime(2024,8, 15),
                TripEND = new DateTime(2024, 8, 29),
                Type = TripTypeModel.Luxury,
                Food = FoodAcoomodationModel.FullBoard,
                Country = CountryModel.Maldives,
                ImageURL = "https://i0.wp.com/theluxurytravelexpert.com/wp-content/uploads/2023/06/best-hotels-resorts-maldives.jpg?fit=1280%2C720&ssl=1",
                Alt = "luxury maledives resort"
            },

        };


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_trips);
        }

        public IActionResult Detail(int id)
        {
            return View(_trips.FirstOrDefault(x => x.ID == id));
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