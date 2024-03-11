using BankingApi.Models;
using BankingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService) => _userService = userService;

    [HttpGet]
    public async Task<List<User>> Get() => await _userService.GetAsync();

    [HttpGet("{Id:length(24)}")]
    public async Task<ActionResult<User>> Get(string Id)
    {
        var user = await _userService.GetAsync(Id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateUserDTO newUser)
    {
        var user = new User
        {
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            AddressLine1 = newUser.AddressLine1,
            AddressLine2 = newUser.AddressLine2,
            PostalCode = newUser.PostalCode,
            City = newUser.City,
            Province = newUser.Province,
            DOB = newUser.DOB,
            AccountIds = newUser.AccountIds,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        await _userService.CreateAsync(user);

        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, UpdateUserDTO updatedUser)
    {
        var existingUser = await _userService.GetAsync(id);

        if (existingUser is null)
        {
            return NotFound();
        }

        var newAccountIds = existingUser.AccountIds;

        if (updatedUser.AccountIds != null)
        {
            for (int i = 0; i < updatedUser.AccountIds.Length; i++)
            {
                //if account is not already linked to user, add it
                if (
                    !Array.Exists(
                        newAccountIds,
                        existingUser => existingUser == updatedUser.AccountIds[i]
                    )
                )
                {
                    Array.Resize(ref newAccountIds, newAccountIds.Length + 1);
                    newAccountIds.SetValue(updatedUser.AccountIds[i], newAccountIds.Length - 1);
                }
            }
        }

        var user = new User
        {
            Id = id,
            FirstName =
                updatedUser.FirstName != null ? updatedUser.FirstName : existingUser.FirstName,
            LastName = updatedUser.LastName != null ? updatedUser.LastName : existingUser.LastName,
            AddressLine1 =
                updatedUser.AddressLine1 != null
                    ? updatedUser.AddressLine1
                    : existingUser.AddressLine1,
            AddressLine2 =
                updatedUser.AddressLine2 != null
                    ? updatedUser.AddressLine2
                    : existingUser.AddressLine2,
            PostalCode =
                updatedUser.PostalCode != null ? updatedUser.PostalCode : existingUser.PostalCode,
            City = updatedUser.City != null ? updatedUser.City : existingUser.City,
            Province = updatedUser.Province != null ? updatedUser.Province : existingUser.Province,
            DOB = updatedUser.DOB != null ? updatedUser.DOB : existingUser.DOB,
            AccountIds = newAccountIds,
            CreatedAt = existingUser.CreatedAt,
            UpdatedAt = DateTime.Now
        };

        await _userService.UpdateAsync(id, user);

        return NoContent();
    }
}
