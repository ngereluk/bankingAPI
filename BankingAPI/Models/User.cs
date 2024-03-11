using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BankingApi.Models;

[BsonIgnoreExtraElements]
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }

    [Required]
    public string DOB { get; set; }

    public string[]? AccountIds { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool? Deleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}

public class CreateUserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string DOB { get; set; }
    public string[]? AccountIds { get; set; }
}

public class UpdateUserDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? DOB { get; set; }
    public string[]? AccountIds { get; set; }
}
