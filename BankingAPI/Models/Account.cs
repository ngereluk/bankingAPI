using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BankingApi.Models;

[BsonIgnoreExtraElements]
public class Account
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public long? AccountBalance { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool? Deleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}

public class AccountDTO
{
    public long? AccountBalance { get; set; } = 0;
}
