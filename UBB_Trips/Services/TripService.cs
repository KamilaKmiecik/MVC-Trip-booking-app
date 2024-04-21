using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.Models;
using UBB_Trips.Repository;
using UBB_Trips.ViewModels;

namespace UBB_Trips.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _repository;

        public TripService(ITripRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TripViewModel>> GetAllAsync()
        {
            var trips = await _repository.GetAllAsync();
            return trips.Select(trip => new TripViewModel
            {
                ID = trip.ID,
                TripSTART = trip.TripSTART,
                TripEND = trip.TripEND,
                Title = trip.Title,
                Description = trip.Description,
                Type = trip.Type,
                Food = trip.Food,
                Country = trip.Country,
                ImageURL = trip.ImageURL,
                Alt = trip.Alt
            });
        }

        public async Task<TripViewModel?> GetTripByIdAsync(int id)
        {
            var trip = await _repository.GetByIdAsync(id);
            return trip != null ? new TripViewModel
            {
                ID = trip.ID,
                TripSTART = trip.TripSTART,
                TripEND = trip.TripEND,
                Title = trip.Title,
                Description = trip.Description,
                Type = trip.Type,
                Food = trip.Food,
                Country = trip.Country,
                ImageURL = trip.ImageURL,
                Alt = trip.Alt
            } : null;
        }

        public async Task<IEnumerable<TripViewModel>> GetTripsPerPageAsync(int page, int pageSize)
        {
            var allTrips = await _repository.GetAllAsync();
            return allTrips.Skip((page - 1) * pageSize).Take(pageSize).Select(trip => new TripViewModel
            {
                ID = trip.ID,
                TripSTART = trip.TripSTART,
                TripEND = trip.TripEND,
                Title = trip.Title,
                Description = trip.Description,
                Type = trip.Type,
                Food = trip.Food,
                Country = trip.Country,
                ImageURL = trip.ImageURL,
                Alt = trip.Alt
            });
        }

        public async Task<IEnumerable<TripViewModel>> FindAsync(Func<TripViewModel, bool> predicate)
        {
            var allTrips = await GetAllAsync();
            return allTrips.Where(predicate);
        }

        public async Task AddAsync(TripViewModel entity)
        {
            var trip = new Trip
            {
                TripSTART = entity.TripSTART,
                TripEND = entity.TripEND,
                Title = entity.Title,
                Description = entity.Description,
                Type = entity.Type,
                Food = entity.Food,
                Country = entity.Country,
                ImageURL = entity.ImageURL,
                Alt = entity.Alt
            };
            await _repository.AddAsync(trip);
        }

        public async Task UpdateAsync(TripViewModel entity)
        {
            var trip = new Trip
            {
                ID = entity.ID,
                TripSTART = entity.TripSTART,
                TripEND = entity.TripEND,
                Title = entity.Title,
                Description = entity.Description,
                Type = entity.Type,
                Food = entity.Food,
                Country = entity.Country,
                ImageURL = entity.ImageURL,
                Alt = entity.Alt
            };
            await _repository.UpdateAsync(trip);
        }

        public Task DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public Task SaveAsync()
        {
            return _repository.SaveAsync();
        }

        public async Task<int> GetTotalNumberOfTripsAsync()
        {
            var trips = await _repository.GetAllAsync();
            return trips.Count();
        }
    }
}
