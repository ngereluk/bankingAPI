using BankingApi.Models;
using BankingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService) => _accountService = accountService;

    [HttpGet]
    public async Task<List<Account>> Get()
    {
        var data = await _accountService.GetAsync();

        return data;
    }

    [HttpGet("{Id:length(24)}")]
    public async Task<ActionResult<Account>> Get(string Id)
    {
        var account = await _accountService.GetAsync(Id);

        if (account is null)
        {
            return NotFound();
        }

        return account;
    }

    [HttpPost]
    public async Task<IActionResult> Post(AccountDTO newAccount)
    {
        var account = new Account
        {
            AccountBalance = newAccount.AccountBalance,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        await _accountService.CreateAsync(account);

        return CreatedAtAction(nameof(Get), new { id = account.Id }, account);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, AccountDTO updatedAccount)
    {
        var existingAccount = await _accountService.GetAsync(id);

        if (existingAccount is null)
        {
            return NotFound();
        }

        var account = new Account
        {
            Id = id,
            AccountBalance =
                updatedAccount.AccountBalance != null
                    ? updatedAccount.AccountBalance
                    : existingAccount.AccountBalance
        };

        await _accountService.UpdateAsync(id, account);

        return NoContent();
    }
}
