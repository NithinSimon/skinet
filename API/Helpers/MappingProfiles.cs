using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>().ForMember(m => m.ProductType, o => o.MapFrom(p => p.ProductType.Name))
            .ForMember(m => m.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name))
            .ForMember(m => m.PictureUrl, o=> o.MapFrom<ProductUrlResolver>());
        }
    }
}