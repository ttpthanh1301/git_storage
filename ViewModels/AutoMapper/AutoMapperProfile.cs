using AutoMapper;
using TourWebsite.Data.Entities;

namespace TourWebsite.ViewModels.AutoMapper
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tours, ToursViewModel>();
            CreateMap<ToursViewModel, Tours>();
            CreateMap<ToursRequest, Tours>();
            CreateMap<Category, CategoriesViewModel>();
            CreateMap<CategoriesViewModel, Category>();
            CreateMap<CategoriesRequest, Category>();
        }
    }
}