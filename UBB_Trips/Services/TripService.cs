using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.Models;
using UBB_Trips.Repository;
using UBB_Trips.ViewModels;
using Mapster; 

namespace UBB_Trips.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _repository;

    public TripService(ITripRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Trip>> GetAllAsync()
    {
        var trips = await _repository.GetAllAsync();
        return trips.Adapt<IEnumerable<Trip>>();
    }

    public async Task<Trip?> GetTripByIdAsync(int id)
    {
        var trip = await _repository.GetByIdAsync(id);
        return trip != null ? trip : null;
    }

    public async Task<IEnumerable<Trip>> GetTripsPerPageAsync(int page, int pageSize, string searchQuery)
    {
        var allTrips = await _repository.GetAllAsync();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            allTrips = allTrips.Where(c => c.Title.ToUpper().Contains(searchQuery.ToUpper()) || c.Description.ToUpper().Contains(searchQuery.ToUpper()));
        }
        return allTrips.Skip((page - 1) * pageSize).Take(pageSize).Select(trip => trip);
    }

    public async Task<IEnumerable<Trip>> FindAsync(Func<Trip, bool> predicate)
    {
        var allTrips = await GetAllAsync();
        return allTrips.Where(predicate);
    }

    public async Task AddAsync(Trip entity)
    {
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(Trip entity)
    {
        await _repository.UpdateAsync(entity.Adapt<Trip>());
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
