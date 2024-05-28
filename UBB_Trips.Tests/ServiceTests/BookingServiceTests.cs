using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.Models;
using UBB_Trips.Repository;
using UBB_Trips.Services;
using Xunit;

public class BookingServiceTests
{
    private readonly Mock<IBookingRepository> _mockRepository;
    private readonly BookingService _bookingService;

    public BookingServiceTests()
    {
        _mockRepository = new Mock<IBookingRepository>();
        _bookingService = new BookingService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetTotalNumberOfBookingsAsync_ReturnsCorrectCount()
    {
        var bookings = new List<Booking> { new Booking(), new Booking(), new Booking() };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bookings);

        var result = await _bookingService.GetTotalNumberOfBookingsAsync();

        Assert.Equal(3, result);
    }

    [Fact]
    public async Task GetBookingsPerPageAsync_ReturnsCorrectPage()
    {
        var bookings = new List<Booking>
        {
            new Booking { Name = "Booking1" },
            new Booking { Name = "Booking2" },
            new Booking { Name = "Booking3" },
            new Booking { Name = "Booking4" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bookings);

        var result = await _bookingService.GetBookingsPerPageAsync(2, 2, "");

        Assert.Equal(2, result.Count());
        Assert.Equal("Booking3", result.First().Name);
    }

    [Fact]
    public async Task GetBookingsPerPageAsync_WithSearchQuery_ReturnsFilteredResults()
    {
        var bookings = new List<Booking>
        {
            new Booking { Name = "Ania" },
            new Booking { Name = "ma" },
            new Booking { Name = "Kot" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bookings);

        var result = await _bookingService.GetBookingsPerPageAsync(1, 2, "a");

        Assert.Equal(2, result.Count());
        Assert.Contains(result, b => b.Name == "Ania");
        Assert.Contains(result, b => b.Name == "ma");
    }


    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenBookingDoesNotExist()
    {
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Booking?)null);

        var result = await _bookingService.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_AddsBooking()
    {
        var booking = new Booking {  Name = "Booking1" };
        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Booking>())).Returns(Task.CompletedTask);

        await _bookingService.AddAsync(booking);

        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Booking>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesBooking()
    {
        var booking = new Booking {  Name = "Booking1" };
        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Booking>())).Returns(Task.CompletedTask);

        await _bookingService.UpdateAsync(booking);

        _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Booking>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_DeletesBooking()
    {
        var bookingId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(bookingId)).Returns(Task.CompletedTask);

        await _bookingService.DeleteAsync(bookingId);

        _mockRepository.Verify(repo => repo.DeleteAsync(bookingId), Times.Once);
    }

    [Fact]
    public async Task FindAsync_ReturnsMatchingBookings()
    {
        var bookings = new List<Booking>
        {
            new Booking { Name = "Ania" },
            new Booking { Name = "ma" },
            new Booking { Name = "Kot" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bookings);

        var result = await _bookingService.FindAsync(b => b.Name.Contains("a"));

        Assert.Equal(2, result.Count());
        Assert.Contains(result, b => b.Name == "Ania");
        Assert.Contains(result, b => b.Name == "ma");
    }

    [Fact]
    public async Task SaveAsync_CallsRepositorySave()
    {
        _mockRepository.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

        await _bookingService.SaveAsync();

        _mockRepository.Verify(repo => repo.SaveAsync(), Times.Once);
    }
}
