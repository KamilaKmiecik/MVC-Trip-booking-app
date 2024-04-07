using UBB_Trips.Models;
using UBB_Trips.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UBB_Trips.Services;

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

    public async Task<IEnumerable<Client>> GetClientsPerPageAsync(int page, int pageSize)
    {
        var allClients = await _repository.GetAllAsync();
        return allClients.Skip((page - 1) * pageSize).Take(pageSize);
    }

    public Task<Client?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<IEnumerable<Client>> FindAsync(Func<Client, bool> predicate)
    {
        return _repository.FindAsync(predicate);
    }

    public Task AddAsync(Client entity)
    {
        return _repository.AddAsync(entity);
    }

    public Task UpdateAsync(Client entity)
    {
        return _repository.UpdateAsync(entity);
    }

    public Task DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }

    public Task SaveAsync()
    {
        return _repository.SaveAsync();
    }

    public async Task<int> GetTotalNumberOfClients()
    {
        var clients = await _repository.GetAllAsync();
        return clients.Count();
    }
}
