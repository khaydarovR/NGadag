using NGadag.Models;
using NGadag.ViewModels;
using AutoMapper;

namespace NGadag.DTO
{
    public class AdMapping: Profile
    {
        public AdMapping()
        {
            CreateMap<CreateAdViewModel, Ad>();
        }
    }
}
