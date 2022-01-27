using CarrinhoCompras.DAL.SQL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarrinhoCompras.DAL.SQL.Interfaces
{
    public interface IProductsRepository
    {
        Task<ProductEntity> CreateProductAsync(ProductEntity car);
        Task<ProductEntity> GetProductAsync(Guid id);
        Task<bool> UpdateProductAsync(ProductEntity car);
        Task<bool> DeleteProductAsync(Guid id);

        Task<IEnumerable<ProductEntity>> GetProductsListAsync(int pageNumber, int pageSize);
    }
}
