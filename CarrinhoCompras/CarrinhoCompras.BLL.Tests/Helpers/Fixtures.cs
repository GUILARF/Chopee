using AutoFixture;
using CarrinhoCompras.BLL.Models;

namespace CarrinhoCompras.BLL.Tests.Helpers
{
    public static class Fixtures
    {
        public static Product CarFixture(string modelName = null, CarType carType = 0)
        {
            var fixture = new Fixture();

            var car = fixture.Build<Product>();

            if (!string.IsNullOrEmpty(modelName))
            {
                car.With(s => s.Name, modelName);
            }

            if (carType > 0)
            {
                car.With(s => s.Type, carType);
            }

            return car.Create();
        }
    }
}
