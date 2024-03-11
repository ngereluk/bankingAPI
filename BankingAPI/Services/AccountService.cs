using BankingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BankingApi.Services;

public class AccountService
{
    private readonly IMongoCollection<Account> _accountCollection;

    public AccountService(IOptions<BankDatabaseSettings> bankDatabaseSettings)
    {
        var mongoClient = new MongoClient(bankDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(bankDatabaseSettings.Value.DatabaseName);

        _accountCollection = mongoDatabase.GetCollection<Account>(
            bankDatabaseSettings.Value.AccountCollectionName
        );
    }

    public async Task<List<Account>> GetAsync() =>
        await _accountCollection.Find(_ => true).ToListAsync();

    public async Task<Account?> GetAsync(string id) =>
        await _accountCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Account newAccount) =>
        await _accountCollection.InsertOneAsync(newAccount);

    public async Task UpdateAsync(string id, Account updatedAccount) =>
        await _accountCollection.ReplaceOneAsync(x => x.Id == id, updatedAccount);

    public async Task RemoveAsync(string id) =>
        await _accountCollection.DeleteOneAsync(x => x.Id == id);
}
