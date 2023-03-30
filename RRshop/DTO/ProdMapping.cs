using AutoMapper;
using RRshop.Models;
using RRshop.ViewModels;

namespace RRshop.DTO
{
    public class ProdrMapping : Profile
    {
        public ProdrMapping()
        {
            CreateMap<CreateProdViewModel, Prod>();
            CreateMap<EditProdViewModel, Prod>().ReverseMap();
        }
    }
}
