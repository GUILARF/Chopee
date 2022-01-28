using AutoMapper;
using CarrinhoCompras.BLL.Contracts;
using CarrinhoCompras.BLL.Models;
using CarrinhoCompras.DAL.SQL.Interfaces;
using CarrinhoCompras.DAL.SQL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarrinhoCompras.BLL
{
    public class ProductService : IProductsService
    {
        private readonly IMapper _mapper;

        public IProductsRepository _productsRepo { get; }

        public ProductService(IMapper mapper, IProductsRepository productsRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productsRepo = productsRepo ?? throw new ArgumentNullException(nameof(productsRepo));
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var newProduct = await _productsRepo.CreateProductAsync(_mapper.Map<ProductEntity>(product));
            return _mapper.Map<Product>(newProduct);
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            var result = await _productsRepo.DeleteProductAsync(id);
            return result;
        }

        public async Task<Product> GetProductAsync(long id)
        {
            var product = await _productsRepo.GetProductAsync(id);
            return _mapper.Map<Product>(product);
        }

        public async Task<IEnumerable<Product>> GetProductsListAsync(int pageNumber, int pageSize)
        {
            var products = await _productsRepo.GetProductsListAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<Product>>(products);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var result = await _productsRepo.UpdateProductAsync(_mapper.Map<ProductEntity>(product));
            return result;
        }
    }
}