using UBB_Trips.Models;
using UBB_Trips.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.ViewModels;
using Mapster; // Import the Mapster namespace

namespace UBB_Trips.Services;

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
        return clients.Select(client => client.Adapt<ClientViewModel>());
    }

    public async Task<IEnumerable<ClientViewModel>> GetClientsPerPageAsync(int page, int pageSize)
    {
        var allClients = await _repository.GetAllAsync();
        return allClients.Skip((page - 1) * pageSize).Take(pageSize).Select(client => client.Adapt<ClientViewModel>());
    }

    public async Task<ClientViewModel?> GetByIdAsync(int id)
    {
        var client = await _repository.GetByIdAsync(id);
        return client != null ? client.Adapt<ClientViewModel>() : null;
    }

    public async Task<IEnumerable<ClientViewModel>> FindAsync(Func<ClientViewModel, bool> predicate)
    {
        var clients = await _repository.GetAllAsync();
        return clients.Select(client => client.Adapt<ClientViewModel>()).Where(predicate);
    }

    public async Task AddAsync(ClientViewModel entity)
    {
        await _repository.AddAsync(entity.Adapt<Client>());
    }

    public async Task UpdateAsync(ClientViewModel entity)
    {
        await _repository.UpdateAsync(entity.Adapt<Client>());
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
