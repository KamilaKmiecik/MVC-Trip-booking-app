using UBB_Trips.Models;
using UBB_Trips.Repository;
using System.Collections.Generic;
using System.Linq; 

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
        return await _repository.GetAllAsync();
    }

    public Task<Trip?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<IEnumerable<Trip>> FindAsync(Func<Trip, bool> predicate)
    {
        return _repository.FindAsync(predicate);
    }

    public Task AddAsync(Trip entity)
    {
        return _repository.AddAsync(entity);
    }

    public Task UpdateAsync(Trip entity)
    {
        return _repository.UpdateAsync(entity);
    }

    public Task DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }

    public Task SaveAsync()
    {
        return _repository.SaveAsync();
    }

    public async Task<Trip> GetTripByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<int> GetTotalNumberOfTripsAsync()
    {
        var trips = await _repository.GetAllAsync();
        return trips.Count();
    }

    public async Task<IEnumerable<Trip>> GetTripsPerPageAsync(int page, int pageSize)
    {
        var allTrips = await _repository.GetAllAsync();
        return allTrips.Skip((page - 1) * pageSize).Take(pageSize);
    }
}
