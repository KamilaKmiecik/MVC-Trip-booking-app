using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UBB_Trips.ViewModels;

namespace UBB_Trips.Services
{
    public interface ITripService
    {
        Task<IEnumerable<TripViewModel>> GetAllAsync();
        Task<TripViewModel?> GetTripByIdAsync(int id);

        Task<IEnumerable<TripViewModel>> GetTripsPerPageAsync(int page, int pageSize);
        Task<IEnumerable<TripViewModel>> FindAsync(Func<TripViewModel, bool> predicate);
        Task AddAsync(TripViewModel entity);
        Task UpdateAsync(TripViewModel entity);
        Task DeleteAsync(int id);
        Task SaveAsync();

        Task<int> GetTotalNumberOfTripsAsync();
    }
}
