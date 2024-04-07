using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.Data;
using UBB_Trips.Models;

namespace UBB_Trips.Repository;

public class ClientRepository : IClientRepository
{
    private readonly TripContext _context;

    public ClientRepository(TripContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<IEnumerable<Client>> FindAsync(Func<Client, bool> predicate)
    {
        return await Task.FromResult(_context.Clients.Where(predicate));
    }

    public async Task AddAsync(Client entity)
    {
        await _context.Clients.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Client entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var clientToDelete = await _context.Clients.FindAsync(id);
        if (clientToDelete != null)
        {
            _context.Clients.Remove(clientToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
