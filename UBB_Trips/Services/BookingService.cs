using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.Models;
using UBB_Trips.Repository;
using UBB_Trips.ViewModels;
using Mapster;

namespace UBB_Trips.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<int> GetTotalNumberOfBookingsAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return bookings.Count();
    }

    public async Task<IEnumerable<Booking>> GetBookingsPerPageAsync(int page, int pageSize, string searchQuery)
    {
        var allBookings = await _bookingRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            allBookings = allBookings.Where(c => c.Name.ToUpper().Contains(searchQuery.ToUpper()));
        }

        return allBookings.Skip((page - 1) * pageSize).Take(pageSize).Adapt<List<Booking>>();
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        var allBookings = await _bookingRepository.GetAllAsync();
        return allBookings.Adapt<List<Booking>>();
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        return booking?.Adapt<Booking>(); 
    }

    public async Task<IEnumerable<Booking>> FindAsync(Func<Booking, bool> predicate)
    {
        var allBookings = await GetAllAsync();
        return allBookings.Where(predicate);
    }

    public async Task AddAsync(Booking entity)
    {
        var booking = entity.Adapt<Models.Booking>(); 
        await _bookingRepository.AddAsync(booking);
    }

    public async Task UpdateAsync(Booking entity)
    {
        var booking = entity.Adapt<Models.Booking>(); 
        await _bookingRepository.UpdateAsync(booking);
    }

    public Task DeleteAsync(int id)
    {
        return _bookingRepository.DeleteAsync(id);
    }

    public Task SaveAsync()
    {
        return _bookingRepository.SaveAsync();
    }
}
