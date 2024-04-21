using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UBB_Trips.ViewModels;

namespace UBB_Trips.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingViewModel>> GetAllAsync();
        Task<BookingViewModel?> GetByIdAsync(int id);
        Task<IEnumerable<BookingViewModel>> FindAsync(Func<BookingViewModel, bool> predicate);
        Task AddAsync(BookingViewModel entity);
        Task UpdateAsync(BookingViewModel entity);
        Task DeleteAsync(int id);
        Task SaveAsync();

        Task<int> GetTotalNumberOfBookingsAsync();
        Task<IEnumerable<BookingViewModel>> GetBookingsPerPageAsync(int page, int pageSize);
    }
}
