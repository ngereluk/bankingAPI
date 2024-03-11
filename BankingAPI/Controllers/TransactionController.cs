using BankingApi.Models;
using BankingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;
    private readonly UserService _userService;
    private readonly AccountService _accountService;

    public TransactionController(
        TransactionService transactionService,
        UserService userService,
        AccountService accountService
    )
    {
        _transactionService = transactionService;
        _userService = userService;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<List<Transaction>> Get() => await _transactionService.GetAsync();

    [HttpGet("{Id:length(24)}")]
    public async Task<ActionResult<Transaction>> Get(string Id)
    {
        var transaction = await _transactionService.GetAsync(Id);

        if (transaction is null)
        {
            return NotFound();
        }

        return transaction;
    }

    [HttpPost]
    public async Task<IActionResult> Post(TransactionDTO newTransaction)
    {
        //check that the sending account and reciever account are not the same account
        if (newTransaction.SenderAccountId == newTransaction.RecipientAccountId)
        {
            return Problem(
                "The sending account and recipient account must be different accounts",
                statusCode: 400
            );
        }

        //check sender account is linked to sender user
        var sendingUser = await _userService.GetAsync(newTransaction.SenderUserId);

        var accountIsLinked = Array.Exists(
            sendingUser.AccountIds,
            accountId => accountId == newTransaction.SenderAccountId
        );

        if (accountIsLinked == false)
        {
            return Problem(
                "The sending user does not have access to the sending account",
                statusCode: 400
            );
        }

        //check there is enough balance in sender account to complete transaction
        var sendingAccountObj = await _accountService.GetAsync(newTransaction.SenderAccountId);
        var sendingAccountBalance = sendingAccountObj.AccountBalance;
        if (sendingAccountBalance < newTransaction.Amount)
        {
            return Problem("Insufficient funds in sender account", statusCode: 403);
        }

        //create new transaction object
        var transaction = new Transaction
        {
            Amount = newTransaction.Amount,
            SenderUserId = newTransaction.SenderUserId,
            SenderAccountId = newTransaction.SenderAccountId,
            RecipientAccountId = newTransaction.RecipientAccountId,
            TimeSent = DateTime.Now
        };
        //create transaction
        await _transactionService.CreateAsync(transaction);

        // Once transaction is created, increment and decrement sender/recipient accounts accordingly
        var recipientAccountObj = await _accountService.GetAsync(newTransaction.RecipientAccountId);
        var preTransactionRecipientAccountBalance = recipientAccountObj.AccountBalance;
        var incrementRecipientAccount = new Account
        {
            Id = recipientAccountObj.Id,
            AccountBalance = preTransactionRecipientAccountBalance + newTransaction.Amount
        };
        var decrementSenderAccount = new Account
        {
            Id = sendingAccountObj.Id,
            AccountBalance = sendingAccountBalance - newTransaction.Amount
        };
        await _accountService.UpdateAsync(
            newTransaction.RecipientAccountId,
            incrementRecipientAccount
        );
        await _accountService.UpdateAsync(newTransaction.SenderAccountId, decrementSenderAccount);

        return CreatedAtAction(nameof(Get), new { id = transaction.Id }, transaction);
    }
}
