using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UBB_Trips.ViewModels;

namespace UBB_Trips.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientViewModel>> GetAllAsync();
        Task<IEnumerable<ClientViewModel>> GetClientsPerPageAsync(int page, int pageSize);
        Task<ClientViewModel?> GetByIdAsync(int id);
        Task<IEnumerable<ClientViewModel>> FindAsync(Func<ClientViewModel, bool> predicate);
        Task AddAsync(ClientViewModel entity);
        Task UpdateAsync(ClientViewModel entity);
        Task DeleteAsync(int id);
        Task SaveAsync();

        Task<int> GetTotalNumberOfClients();
    }
}
