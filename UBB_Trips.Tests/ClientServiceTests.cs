using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_Trips.Models;
using UBB_Trips.Repository;
using UBB_Trips.Services;
using Xunit;

public class ClientServiceTests
{
    private readonly Mock<IClientRepository> _mockRepository;
    private readonly ClientService _clientService;

    public ClientServiceTests()
    {
        _mockRepository = new Mock<IClientRepository>();
        _clientService = new ClientService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetTotalNumberOfClients_ReturnsCorrectCount()
    {
        var clients = new List<Client> { new Client(), new Client(), new Client() };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(clients);

        var result = await _clientService.GetTotalNumberOfClients();

        Assert.Equal(3, result);
    }

    [Fact]
    public async Task GetClientsPerPageAsync_ReturnsCorrectPage()
    {
        var clients = new List<Client>
        {
            new Client { FirstName = "Client1" },
            new Client { FirstName = "Client2" },
            new Client { FirstName = "Client3" },
            new Client { FirstName = "Client4" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(clients);

        var result = await _clientService.GetClientsPerPageAsync(2, 2, "");

        Assert.Equal(2, result.Count());
        Assert.Equal("Client3", result.First().FirstName);
    }

    [Fact]
    public async Task GetClientsPerPageAsync_WithSearchQuery_ReturnsFilteredResults()
    {
        var clients = new List<Client>
        {
            new Client { FirstName = "Ania", LastName = "Kowalczyk" },
            new Client { FirstName = "ma", LastName = "Waluszek" },
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(clients);

        var result = await _clientService.GetClientsPerPageAsync(1, 2, "a");

        Assert.Equal(2, result.Count());
        Assert.Contains(result, c => c.FirstName == "Ania");
        Assert.Contains(result, c => c.FirstName == "ma");
    }

    [Fact]
    public async Task GetClientsPerPageAsync_WithSearchQuery_ReturnsCorrectCount()
    {
        var clients = new List<Client>
        {
            new Client { FirstName = "Kamila", LastName = "Kmiecik" },
            new Client { FirstName = "Patrycja", LastName = "Kubica" },
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(clients);

        var result = await _clientService.GetClientsPerPageAsync(1, 2, "z");

        Assert.Equal(0, result.Count());
    }



    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenClientDoesNotExist()
    {
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Client?)null);

        var result = await _clientService.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_AddsClient()
    {
        var client = new Client { FirstName = "Client1" };
        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Client>())).Returns(Task.CompletedTask);

        await _clientService.AddAsync(client);

        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Client>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesClient()
    {
        var client = new Client { FirstName = "Client1" };
        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Client>())).Returns(Task.CompletedTask);

        await _clientService.UpdateAsync(client);

        _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Client>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_DeletesClient()
    {
        var clientId = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(clientId)).Returns(Task.CompletedTask);

        await _clientService.DeleteAsync(clientId);

        _mockRepository.Verify(repo => repo.DeleteAsync(clientId), Times.Once);
    }


    [Fact]
    public async Task SaveAsync_CallsRepositorySave()
    {
        _mockRepository.Setup(repo => repo.SaveAsync()).Returns(Task.CompletedTask);

        await _clientService.SaveAsync();

        _mockRepository.Verify(repo => repo.SaveAsync(), Times.Once);
    }
}
