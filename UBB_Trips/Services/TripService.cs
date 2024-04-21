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

    public async Task<IEnumerable<TripViewModel>> GetAllAsync()
    {
        var trips = await _repository.GetAllAsync();
        return trips.Adapt<IEnumerable<TripViewModel>>();
    }

    public async Task<TripViewModel?> GetTripByIdAsync(int id)
    {
        var trip = await _repository.GetByIdAsync(id);
        return trip != null ? trip.Adapt<TripViewModel>() : null;
    }

    public async Task<IEnumerable<TripViewModel>> GetTripsPerPageAsync(int page, int pageSize)
    {
        var allTrips = await _repository.GetAllAsync();
        return allTrips.Skip((page - 1) * pageSize).Take(pageSize).Select(trip => trip.Adapt<TripViewModel>());
    }

    public async Task<IEnumerable<TripViewModel>> FindAsync(Func<TripViewModel, bool> predicate)
    {
        var allTrips = await GetAllAsync();
        return allTrips.Where(predicate);
    }

    public async Task AddAsync(TripViewModel entity)
    {
        await _repository.AddAsync(entity.Adapt<Trip>());
    }

    public async Task UpdateAsync(TripViewModel entity)
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
