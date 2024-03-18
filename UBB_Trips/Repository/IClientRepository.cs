using UBB_Trips.Models;

namespace UBB_Trips.Repository;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<IEnumerable<Client>> FindAsync(Func<Client, bool> predicate);
    Task AddAsync(Client entity);
    Task UpdateAsync(Client entity);
    Task DeleteAsync(int id);

    Task SaveAsync();
}
