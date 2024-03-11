namespace BankingApi.Models;

public class BankDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string UserCollectionName { get; set; } = null!;

    public string AccountCollectionName { get; set; } = null!;
    public string TransactionCollectionName { get; set; } = null!;
}
