using AutoMapper;
using DRAFTME_CORE.DTOs;
using DRAFTME_CORE.Models;

namespace DRAFTME_BUSINESS.Mappers;
public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<User,UserDTO>().ReverseMap();
    }
}
