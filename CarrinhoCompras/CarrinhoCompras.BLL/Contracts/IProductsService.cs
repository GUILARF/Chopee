using CarrinhoCompras.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarrinhoCompras.BLL.Contracts
{
    public interface IProductsService
    {
        /// <summary>
        /// Create a new car
        /// </summary>
        /// <param name="Product"></param>
        /// <returns></returns>
        Task<Product> CreateProductAsync(Product Product);

        /// <summary>
        /// Get Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Product> GetProductAsync(long id);

        /// <summary>
        /// Update Product parameters
        /// </summary>
        /// <param name="Product"></param>
        /// <returns></returns>
        Task<bool> UpdateProductAsync(Product Product);

        /// <summary>
        /// Delete Product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteProductAsync(long id);

        /// <summary>
        /// Get Products list
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetProductsListAsync(int pageNumber, int pageSize);
    }
}