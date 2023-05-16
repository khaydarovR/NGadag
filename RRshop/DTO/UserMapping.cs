using AutoMapper;
using NGadag.Models;
using RRshop.ViewModels;

namespace RRshop.DTO
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<RegisterViewModel, User>();
            CreateMap<LoginViewModel, User>();
        }
    }
}
