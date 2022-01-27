using CarrinhoCompras.API.Models;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace CarrinhoCompras.API.Swagger
{
    public class ProductModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var dnow = DateTime.UtcNow;
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = "Toyota",
                Type = ProductType.Toy,
                CreatedOn = dnow,
                ModifiedOn = dnow
            };
        }
    }
}
