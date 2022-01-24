using AutoFixture;
using Moq;
using CarrinhoCompras.BLL.Contracts;
using CarrinhoCompras.BLL.Models;
using CarrinhoCompras.DAL.SQL.Interfaces;
using CarrinhoCompras.DAL.SQL.Models;
using System;
using CarrinhoCompras.DAL.SQL.Interfaces;

namespace CarrinhoCompras.BLL.Tests.Helpers
{
    public static class Moqs
    {
        public static Mock<ICarsService> CarsServiceMoq()
        {
            var fixture = new Fixture();

            var moq = new Mock<ICarsService>(MockBehavior.Strict);
            moq.Setup(s => s.CreateCarAsync(It.IsAny<Car>()))
              .ReturnsAsync(fixture.Build<Car>().Create());
            moq.Setup(s => s.GetCarAsync(It.IsAny<Guid>()))
             .ReturnsAsync(fixture.Build<Car>().Create());
            moq.Setup(s => s.UpdateCarAsync(It.IsAny<Car>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteCarAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetCarsListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<Car>().CreateMany(20));

            return moq;
        }

        public static Mock<ICarsRepository> CarsReposirotyMoq(CarEntity carEntity)
        {
            var fixture = new Fixture();

            var moq = new Mock<ICarsRepository>(MockBehavior.Strict);
            moq.Setup(s => s.CreateCarAsync(It.IsAny<CarEntity>()))
              .ReturnsAsync(carEntity);
            moq.Setup(s => s.GetCarAsync(It.IsAny<Guid>()))
             .ReturnsAsync(carEntity);
            moq.Setup(s => s.UpdateCarAsync(It.IsAny<CarEntity>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteCarAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetCarsListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<CarEntity>().CreateMany(20));

            return moq;
        }
    }
}
