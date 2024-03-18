using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.Data;
using UBB_Trips.Models;

namespace UBB_Trips.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly TripContext _context;

        public BookingRepository(TripContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<IEnumerable<Booking>> FindAsync(Func<Booking, bool> predicate)
        {
            return await Task.FromResult(_context.Bookings.Where(predicate));
        }

        public async Task AddAsync(Booking entity)
        {
            await _context.Bookings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Booking entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bookingToDelete = await _context.Bookings.FindAsync(id);
            if (bookingToDelete != null)
            {
                _context.Bookings.Remove(bookingToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
