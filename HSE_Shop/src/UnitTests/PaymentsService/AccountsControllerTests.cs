using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentsService.Controllers;
using PaymentsService.Persistence;
using PaymentsService.Persistence.Entities;
using Xunit;

namespace PaymentsService.UnitTests;

public class AccountsControllerTests
{
    private readonly DbContextOptions<PaymentsDbContext> _dbOptions;
    private readonly Mock<ILogger<AccountsController>> _loggerMock;

    public AccountsControllerTests()
    {
        _dbOptions = new DbContextOptionsBuilder<PaymentsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _loggerMock = new Mock<ILogger<AccountsController>>();
    }

    [Fact]
    public async Task CreateAccount_WhenUserAlreadyExists_ReturnsConflict()
    {
        var userId = Guid.NewGuid();
        await using var dbContext = new PaymentsDbContext(_dbOptions);
        await dbContext.Accounts.AddAsync(new Account { UserId = userId, Balance = 0 });
        await dbContext.SaveChangesAsync();

        var controller = new AccountsController(dbContext, _loggerMock.Object);
        var createRequest = new AccountsController.CreateAccountRequest(userId);
        
        var result = await controller.CreateAccount(createRequest);
        
        Assert.IsType<ConflictObjectResult>(result);
    }

    [Fact]
    public async Task Deposit_WhenAccountNotFound_ReturnsNotFound()
    {
        var userId = Guid.NewGuid();
        await using var dbContext = new PaymentsDbContext(_dbOptions);
        var controller = new AccountsController(dbContext, _loggerMock.Object);
        var depositRequest = new AccountsController.DepositRequest(userId, 100);
        
        var result = await controller.Deposit(depositRequest);
        
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetBalance_WhenAccountExists_ReturnsOkWithCorrectBalance()
    {
        var userId = Guid.NewGuid();
        var expectedBalance = 123.45m;
        await using var dbContext = new PaymentsDbContext(_dbOptions);
        await dbContext.Accounts.AddAsync(new Account { UserId = userId, Balance = expectedBalance });
        await dbContext.SaveChangesAsync();

        var controller = new AccountsController(dbContext, _loggerMock.Object);
        
        var result = await controller.GetBalance(userId);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = okResult.Value;
        Assert.NotNull(value);
        
        var balanceProperty = value.GetType().GetProperty("Balance");
        Assert.NotNull(balanceProperty);
        
        var actualBalance = Assert.IsType<decimal>(balanceProperty.GetValue(value));
        Assert.Equal(expectedBalance, actualBalance);
    }
}