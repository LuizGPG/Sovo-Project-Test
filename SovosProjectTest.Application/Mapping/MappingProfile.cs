using AutoMapper;
using SovosProjectTest.Application.Filters;
using SovosProjectTest.Application.Model;
using SovosProjectTest.Domain.Entities;
using SovosProjectTest.Domain.Filters;

namespace SovosProjectTest.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductModel, Product>().ReverseMap();

            CreateMap<ProductFilterModel, ProductFilterDto>();
            CreateMap<BaseFilterModel, BaseFilterDto>();
        }
    }
}
