using UBB_Trips.Models;
using UBB_Trips.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UBB_Trips.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsPerPageAsync(int page, int pageSize, string searchQuery)
        {
            var allClients = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                allClients = allClients.Where(c => c.FirstName.ToUpper().Contains(searchQuery.ToUpper()) || c.LastName.ToUpper().Contains(searchQuery.ToUpper()));
            }

            return allClients.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Client>> FindAsync(Func<Client, bool> predicate)
        {
            var clients = await _repository.GetAllAsync();
            return clients.Where(predicate);
        }

        public async Task AddAsync(Client entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(Client entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task SaveAsync()
        {
            await _repository.SaveAsync();
        }

        public async Task<int> GetTotalNumberOfClients()
        {
            var clients = await _repository.GetAllAsync();
            return clients.Count();
        }
    }
}
