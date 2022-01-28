using AutoFixture;
using CarrinhoCompras.BLL.Contracts;
using CarrinhoCompras.BLL.Models;
using CarrinhoCompras.DAL.SQL.Interfaces;
using CarrinhoCompras.DAL.SQL.Models;
using Moq;

namespace CarrinhoCompras.BLL.Tests.Helpers
{
    public static class Moqs
    {
        public static Mock<IProductsService> CarsServiceMoq()
        {
            var fixture = new Fixture();

            var moq = new Mock<IProductsService>(MockBehavior.Strict);
            moq.Setup(s => s.CreateProductAsync(It.IsAny<Product>()))
              .ReturnsAsync(fixture.Build<Product>().Create());
            moq.Setup(s => s.GetProductAsync(It.IsAny<long>()))
             .ReturnsAsync(fixture.Build<Product>().Create());
            moq.Setup(s => s.UpdateProductAsync(It.IsAny<Product>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteProductAsync(It.IsAny<long>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetProductsListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<Product>().CreateMany(20));

            return moq;
        }

        public static Mock<IProductsRepository> CarsRepositoryMoq(ProductEntity carEntity)
        {
            var fixture = new Fixture();

            var moq = new Mock<IProductsRepository>(MockBehavior.Strict);
            moq.Setup(s => s.CreateProductAsync(It.IsAny<ProductEntity>()))
              .ReturnsAsync(carEntity);
            moq.Setup(s => s.GetProductAsync(It.IsAny<long>()))
             .ReturnsAsync(carEntity);
            moq.Setup(s => s.UpdateProductAsync(It.IsAny<ProductEntity>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteProductAsync(It.IsAny<long>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetProductsListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<ProductEntity>().CreateMany(20));

            return moq;
        }
    }
}