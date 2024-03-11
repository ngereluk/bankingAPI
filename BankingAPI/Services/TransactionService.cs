using BankingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BankingApi.Services;

public class TransactionService
{
    private readonly IMongoCollection<Transaction> _transactionCollection;

    public TransactionService(IOptions<BankDatabaseSettings> bankDatabaseSettings)
    {
        var mongoClient = new MongoClient(bankDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(bankDatabaseSettings.Value.DatabaseName);

        _transactionCollection = mongoDatabase.GetCollection<Transaction>(
            bankDatabaseSettings.Value.TransactionCollectionName
        );
    }

    public async Task<List<Transaction>> GetAsync() =>
        await _transactionCollection.Find(_ => true).ToListAsync();

    public async Task<Transaction?> GetAsync(string id) =>
        await _transactionCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Transaction newTransaction) =>
        await _transactionCollection.InsertOneAsync(newTransaction);

    public async Task UpdateAsync(string id, Transaction updatedTransaction) =>
        await _transactionCollection.ReplaceOneAsync(x => x.Id == id, updatedTransaction);

    public async Task RemoveAsync(string id) =>
        await _transactionCollection.DeleteOneAsync(x => x.Id == id);
}
