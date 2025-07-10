using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Users.Commands.Update;
public class UpdateUserHandler(IRepository<User> repository, IValidator<UpdateUser> validator, IMapper mapper) : IRequestHandler<UpdateUser, UserDTO>
{
    public async Task<UserDTO> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new FormatException(validationResult.ToString());
        }
        var user = await repository.Query.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
        user.Username = request.NewUsername;
        user.PasswordHash = request.PasswordHash;
        user.Rol = request.Rol;
        await repository.SaveChangesAsync();
        return mapper.Map<UserDTO>(user);
    }
}
