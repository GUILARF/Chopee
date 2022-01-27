using AutoMapper;
using CarrinhoCompras.BLL.Mappings;

namespace CarrinhoCompras.BLL.Tests.Helpers
{
    public static class Mapper
    {
        public static IMapper GetAutoMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMapping());

            });
            var mapper = mockMapper.CreateMapper();
            return mapper;
        }
    }
}
