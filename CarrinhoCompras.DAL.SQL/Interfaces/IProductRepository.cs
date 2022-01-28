using CarrinhoCompras.DAL.SQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarrinhoCompras.DAL.SQL.Interfaces
{
    public interface IProductsRepository
    {
        Task<ProductEntity> CreateProductAsync(ProductEntity car);

        Task<ProductEntity> GetProductAsync(long id);

        Task<bool> UpdateProductAsync(ProductEntity car);

        Task<bool> DeleteProductAsync(long id);

        Task<IEnumerable<ProductEntity>> GetProductsListAsync(int pageNumber, int pageSize);
    }
}