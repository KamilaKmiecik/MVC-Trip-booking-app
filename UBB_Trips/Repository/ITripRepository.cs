using UBB_Trips.Models;

namespace UBB_Trips.Repository;

public interface ITripRepository
{
    Task<IEnumerable<Trip>> GetAllAsync();
    Task<Trip?> GetByIdAsync(int id);
    Task<IEnumerable<Trip>> FindAsync(Func<Trip, bool> predicate);
    Task AddAsync(Trip entity);
    Task UpdateAsync(Trip entity);
    Task DeleteAsync(int id);

    Task SaveAsync(); 

}
