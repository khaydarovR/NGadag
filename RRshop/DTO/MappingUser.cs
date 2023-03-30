using AutoMapper;
using RRshop.Models;
using RRshop.Models.ViewModels;

namespace RRshop.DTO
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<RegisterViewModel, User>();
            CreateMap<LoginViewModel, User>();
        }
    }
}
