using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.Models;
using UBB_Trips.Repository;

namespace UBB_Trips.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _bookingRepository.GetAllAsync();
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await _bookingRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Booking>> FindAsync(Func<Booking, bool> predicate)
    {
        return await _bookingRepository.FindAsync(predicate);
    }

    public async Task AddAsync(Booking entity)
    {
        await _bookingRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Booking entity)
    {
        await _bookingRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _bookingRepository.DeleteAsync(id);
    }

    public async Task SaveAsync()
    {
        await _bookingRepository.SaveAsync();
    }

    public async Task<int> GetTotalNumberOfBookingsAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return bookings.Count();
    }

    public async Task<IEnumerable<Booking>> GetBookingsPerPageAsync(int page, int pageSize)
    {
        var allBookings = await _bookingRepository.GetAllAsync();
        return allBookings.Skip((page - 1) * pageSize).Take(pageSize);
    }
}
