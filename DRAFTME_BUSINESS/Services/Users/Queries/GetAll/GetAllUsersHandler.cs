using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Interfaces;
using DRAFTME_CORE.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DRAFTME_BUSINESS.Services.Users.Queries.GetAll;
public class GetAllUsersHandler(IRepository<User> repository,Mapper mapper) : IRequestHandler<GetAllUsers, List<UserDTO>>
{
    public async Task<List<UserDTO>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        return await repository.Query.Select(x => mapper.Map<UserDTO>(x)).ToListAsync(cancellationToken);
    }
}
