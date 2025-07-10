using DRAFTME_BUSINESS.Services.Users.Commands.Create;
using DRAFTME_BUSINESS.Services.Users.Commands.Delete;
using DRAFTME_BUSINESS.Services.Users.Commands.Update;
using DRAFTME_BUSINESS.Services.Users.Queries.GetAll;
using DRAFTME_BUSINESS.Services.Users.Queries.GetUserByUsername;
using DRAFTME_CORE.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DRAFTME_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseApiController
{
    [HttpGet("All")]
    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        return await Mediator.Send(new GetAllUsers());
    }

    [HttpGet("{id}")]
    public async Task<UserDTO> GetUserByIdAsync(string id)
    {
        return await Mediator.Send(new GetUserByUsername() { Username = id });
    }

    [HttpPost]
    public async Task<UserDTO> Create(CreateUser create)
    {
        return await Mediator.Send(create);
    }

    [HttpDelete("{id}")]
    public async Task Delete(string id)
    {
        await Mediator.Send(new DeleteUser() { Username = id });
    }

    [HttpPut("{id}")]
    public async Task Update(int id, UpdateUser update)
    {
        await Mediator.Send(update);
    }
}
