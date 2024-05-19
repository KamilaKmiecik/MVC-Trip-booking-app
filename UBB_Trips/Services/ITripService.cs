using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UBB_Trips.Models;

namespace UBB_Trips.Services
{
    public interface ITripService
    {
        Task<IEnumerable<Trip>> GetAllAsync();
        Task<Trip?> GetTripByIdAsync(int id);

        Task<IEnumerable<Trip>> GetTripsPerPageAsync(int page, int pageSize, string searchQuery);
        Task<IEnumerable<Trip>> FindAsync(Func<Trip, bool> predicate);
        Task AddAsync(Trip entity);
        Task UpdateAsync(Trip entity);
        Task DeleteAsync(int id);
        Task SaveAsync();

        Task<int> GetTotalNumberOfTripsAsync();
    }
}
