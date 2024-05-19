using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UBB_Trips.Models;

namespace UBB_Trips.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<IEnumerable<Booking>> FindAsync(Func<Booking, bool> predicate);
        Task AddAsync(Booking entity);
        Task UpdateAsync(Booking entity);
        Task DeleteAsync(int id);
        Task SaveAsync();

        Task<int> GetTotalNumberOfBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsPerPageAsync(int page, int pageSize, string searchQuery);
    }
}
