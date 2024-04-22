using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UBB_Trips.Models;

namespace UBB_Trips.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<IEnumerable<Client>> GetClientsPerPageAsync(int page, int pageSize);
        Task<Client?> GetByIdAsync(int id);
        Task<IEnumerable<Client>> FindAsync(Func<Client, bool> predicate);
        Task AddAsync(Client entity);
        Task UpdateAsync(Client entity);
        Task DeleteAsync(int id);
        Task SaveAsync();

        Task<int> GetTotalNumberOfClients();
    }
}
