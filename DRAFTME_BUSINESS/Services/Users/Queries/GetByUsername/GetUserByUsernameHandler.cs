using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Users.Queries.GetUserByUsername;
public class GetUserByUsernameHandler(IRepository<User> repository, IMapper mapper) : IRequestHandler<GetUserByUsername, UserDTO>
{
    public async Task<UserDTO> Handle(GetUserByUsername request, CancellationToken cancellationToken)
    {
        var user = await repository.Query.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException($"No se ha encontrado el usuario con username: {request.Username}");
        }
        else
        {
            return mapper.Map<UserDTO>(user);
        }
    }
}
