using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.Models;
using UBB_Trips.Repository;
using UBB_Trips.Services;
using Xunit;

public class TripServiceTests
{
    private readonly Mock<ITripRepository> _mockRepository;
    private readonly TripService _tripService;

    public TripServiceTests()
    {
        _mockRepository = new Mock<ITripRepository>();
        _tripService = new TripService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetTotalNumberOfTripsAsync_ReturnsCorrectCount()
    {
        var trips = new List<Trip> { new Trip(), new Trip(), new Trip() };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(trips);

        var result = await _tripService.GetTotalNumberOfTripsAsync();

        Assert.Equal(3, result);
    }

    [Fact]
    public async Task GetTripsPerPageAsync_ReturnsCorrectPage()
    {
        var trips = new List<Trip>
        {
            new Trip { Title = "Trip1" },
            new Trip { Title = "Trip2" },
            new Trip { Title = "Trip3" },
            new Trip { Title = "Trip4" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(trips);

        var result = await _tripService.GetTripsPerPageAsync(2, 2, "");

        Assert.Equal(2, result.Count());
        Assert.Equal("Trip3", result.First().Title);
    }

    [Fact]
    public async Task GetTripsPerPageAsync_WithSearchQuery_ReturnsFilteredResults()
    {
        var trips = new List<Trip>
        {
            new Trip { Title = "Ania", Description = "Description1" },
            new Trip { Title = "ma", Description = "Description2" },
            new Trip { Title = "Kot", Description = "Description3" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(trips);

        var result = await _tripService.GetTripsPerPageAsync(1, 2, "a");

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetTripByIdAsync_ReturnsNull_WhenTripDoesNotExist()
    {
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Trip?)null);

        var result = await _tripService.GetTripByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_AddsTrip()
    {
        var trip = new Trip { Title = "Trip1" };
        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Trip>())).Returns(Task.CompletedTask);

        await _tripService.AddAsync(trip);

        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Trip>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesTrip()
    {
        var trip = new Trip { Title = "Trip1" };
        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Trip>())).Returns(Task.CompletedTask);

        await _tripService.UpdateAsync(trip);

        _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Trip>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_DeletesTrip()
    {
        var tripId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(tripId)).Returns(Task.CompletedTask);

        await _tripService.DeleteAsync(tripId);

        _mockRepository.Verify(repo => repo.DeleteAsync(tripId), Times.Once);
    }

    [Fact]
    public async Task FindAsync_ReturnsMatchingTrips()
    {
        var trips = new List<Trip>
        {
            new Trip { Title = "Ania" },
            new Trip { Title = "ma" },
            new Trip { Title = "Kot" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(trips);

        var result = await _tripService.FindAsync(t => t.Title.Contains("K"));

        Assert.Equal(1, result.Count());
        Assert.Contains(result, t => t.Title == "Kot");
    }

    [Fact]
    public async Task SaveAsync_CallsRepositorySave()
    {
        _mockRepository.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

        await _tripService.SaveAsync();

        _mockRepository.Verify(repo => repo.SaveAsync(), Times.Once);
    }
}
