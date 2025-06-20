using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentsService.Persistence;
using PaymentsService.Persistence.Entities;

namespace PaymentsService.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController(PaymentsDbContext dbContext, ILogger<AccountsController> logger) : ControllerBase
{
    public record CreateAccountRequest(Guid UserId);
    public record DepositRequest(Guid UserId, decimal Amount);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
    {
        var accountExists = await dbContext.Accounts.AnyAsync(a => a.UserId == request.UserId);
        if (accountExists)
        {
            return Conflict($"Аккаунт с id {request.UserId} уже существует.");
        }

        var account = new Account
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Balance = 0
        };

        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("Создан аккаунт с id {UserId}", request.UserId);

        return CreatedAtAction(nameof(GetBalance), new { userId = request.UserId }, account);
    }
    
    [HttpPost("deposit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deposit([FromBody] DepositRequest request)
    {
        var account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.UserId == request.UserId);
        if (account == null)
        {
            return NotFound($"Аккаунт с id {request.UserId} не найден.");
        }

        account.Balance += request.Amount;
        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("Вложено {Amount} денег в аккаунт с id {UserId}. Новый баланс: {Balance}", 
            request.Amount, request.UserId, account.Balance);
            
        return Ok(account);
    }

    [HttpGet("balance/{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBalance(Guid userId)
    {
        var account = await dbContext.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.UserId == userId);
            
        if (account == null)
        {
            return NotFound($"Аккаунт с id {userId} не найден.");
        }

        return Ok(new { account.UserId, account.Balance });
    }
}