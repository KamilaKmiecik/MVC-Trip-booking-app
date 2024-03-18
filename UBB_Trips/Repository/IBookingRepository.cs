using UBB_Trips.Models;

namespace UBB_Trips.Repository;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(int id);
    Task<IEnumerable<Booking>> FindAsync(Func<Booking, bool> predicate);
    Task AddAsync(Booking entity);
    Task UpdateAsync(Booking entity);
    Task DeleteAsync(int id);

    Task SaveAsync();
}
