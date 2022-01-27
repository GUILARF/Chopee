using AutoMapper;
using CarrinhoCompras.BLL.Models;
using CarrinhoCompras.DAL.SQL.Models;
using System;

namespace CarrinhoCompras.BLL.Mappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductEntity>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(d => d.ModifiedOn, opt => opt.MapFrom(src => src.ModifiedOn));

            CreateMap<ProductEntity, Product>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(d => d.ModifiedOn, opt => opt.MapFrom(src => src.ModifiedOn));
        }
    }
}
