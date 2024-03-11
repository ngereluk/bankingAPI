using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BankingApi.Models;

[BsonIgnoreExtraElements]
public class Transaction
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [Required]
    public long Amount { get; set; }

    [Required]
    public string SenderUserId { get; set; }

    [Required]
    public string SenderAccountId { get; set; }

    [Required]
    public string RecipientAccountId { get; set; }

    [Required]
    public DateTime TimeSent { get; set; }
}

public class TransactionDTO
{
    [Required]
    public long Amount { get; set; }

    [Required]
    public string SenderUserId { get; set; }

    [Required]
    public string SenderAccountId { get; set; }

    [Required]
    public string RecipientAccountId { get; set; }
}
