using System.Diagnostics;
using UBB_Trips.Models;

namespace UBB_Trips.Data;

public static class DbInitializer
{
    public static void Initialize(TripContext context)
    {
        context.Database.EnsureCreated();

        if (context.Trips.Any())
        {
            return;   // DB has been seeded
        }

        var trips = new Trip[]
        {
            new Trip
            {
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
            new Trip
            {
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
            new Trip
            {
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

        context.Trips.AddRange(trips);
        context.SaveChanges();

        var clients = new Client[]
        {
                new Client
                {
                    FirstName = "Tom",
                    LastName = "Kowalczyk",
                    Email = "Tom.Kowalczyk@onet.pl",
                    Booking = new Booking
                    {
                        Name = "Family Vacation Booking",
                        Trip = trips.First(), 
                        NumberOfBookings = 2 
                    }
                },
                new Client
                {
                    FirstName = "Jerry",
                    LastName = "Krawczyk",
                    Email = "Jerry98@gmail.com",
                    Booking = new Booking
                    {
                        Name = "Family Vacation Booking",
                        Trip = trips.FirstOrDefault(x => x.ID == 2),
                        NumberOfBookings = 2
                    }
                },

        };

        context.Clients.AddRange(clients);
        context.SaveChanges();
    }
}



