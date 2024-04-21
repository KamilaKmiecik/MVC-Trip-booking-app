using UBB_Trips.Models;
using UBB_Trips.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.ViewModels;

namespace UBB_Trips.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClientViewModel>> GetAllAsync()
        {
            var clients = await _repository.GetAllAsync();
            return clients.Select(client => MapToViewModel(client));
        }

        public async Task<IEnumerable<ClientViewModel>> GetClientsPerPageAsync(int page, int pageSize)
        {
            var allClients = await _repository.GetAllAsync();
            return allClients.Skip((page - 1) * pageSize).Take(pageSize).Select(client => MapToViewModel(client));
        }

        public async Task<ClientViewModel?> GetByIdAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            return client != null ? MapToViewModel(client) : null;
        }

        public async Task<IEnumerable<ClientViewModel>> FindAsync(Func<ClientViewModel, bool> predicate)
        {
            var clients = await _repository.GetAllAsync();
            return clients.Select(client => MapToViewModel(client)).Where(predicate);
        }

        public async Task AddAsync(ClientViewModel entity)
        {
            await _repository.AddAsync(MapToModel(entity));
        }

        public async Task UpdateAsync(ClientViewModel entity)
        {
            await _repository.UpdateAsync(MapToModel(entity));
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

        private ClientViewModel MapToViewModel(Client client)
        {
            return new ClientViewModel
            {
                ID = client.ID,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                BookingID = client.Booking.ID
            };
        }

        private Client MapToModel(ClientViewModel viewModel)
        {
            return new Client
            {
                ID = viewModel.ID,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                //BookingID = viewModel.BookingID
            };
        }
    }
}
