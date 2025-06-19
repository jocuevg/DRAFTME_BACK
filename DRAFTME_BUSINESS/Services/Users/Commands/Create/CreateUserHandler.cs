using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;

namespace DRAFTME_BUSINESS.Services.Users.Commands.Create;
public class CreateUserHandler(IRepository<User> repository, IValidator<CreateUser> validator, Mapper mapper) : IRequestHandler<CreateUser, UserDTO>
{
    public async Task<UserDTO> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }
        var user = new User
        {
            Username = request.Username,
            PasswordHash = request.PasswordHash,
            Rol = request.Rol,
        };
        repository.Add(user);
        await repository.SaveChangesAsync();
        return mapper.Map<UserDTO>(user);
    }
}
