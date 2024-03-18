using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.Data;
using UBB_Trips.Models;

namespace UBB_Trips.Repository
{
    public class TripRepository : ITripRepository
    {
        private readonly TripContext _context;

        public TripRepository(TripContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trip>> GetAllAsync()
        {
            return await _context.Trips.ToListAsync();
        }

        public async Task<Trip?> GetByIdAsync(int id)
        {
            return await _context.Trips.FindAsync(id);
        }

        public async Task<IEnumerable<Trip>> FindAsync(Func<Trip, bool> predicate)
        {
            return await Task.FromResult(_context.Trips.Where(predicate));
        }

        public async Task AddAsync(Trip entity)
        {
            await _context.Trips.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Trip entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tripToDelete = await _context.Trips.FindAsync(id);
            if (tripToDelete != null)
            {
                _context.Trips.Remove(tripToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
