using CarrinhoCompras.DAL.SQL.Interfaces;
using CarrinhoCompras.DAL.SQL.Models;
using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace CarrinhoCompras.DAL.SQL
{
    public class ProductsRepository: IProductsRepository, IHealthCheck
    {
        private readonly IOptionsMonitor<ProductsSqlRepositoryOptions> _options;

        public ProductsRepository(IOptionsMonitor<ProductsSqlRepositoryOptions> options)
        {
            _options = options;
        }

        public async Task<ProductEntity> CreateProductAsync(ProductEntity newProduct)
        {
            //if (newProduct.Id == Guid.Empty.ToString())
            //{
            //    newProduct.Id = Guid.NewGuid().ToString();
            //}
            var dnow = DateTime.UtcNow;
            newProduct.CreatedOn = dnow;
            newProduct.ModifiedOn = dnow;
            newProduct.Type = 1;

            const string sqlQuery = @"INSERT INTO products (
                    
                    modelname,
                    type,
                    createdon,
                    modifiedon
                )
                VALUES (
                    
                    @modelname,
                    @type,
                    @createdon,
                    @modifiedon);";

            using (var con = new SqlConnection(_options.CurrentValue.ProductsDbConnectionString))
            {
                //await db.(sqlQuery, newProduct, commandType: CommandType.Text);
                //return newProduct;

                try
                {
                    con.Open();
                    await con.ExecuteAsync(sqlQuery, newProduct, commandType: CommandType.Text);
                    return newProduct;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }

        }

        public async Task<ProductEntity> GetProductAsync(Guid id)
        {
            using (var con = new SqlConnection(_options.CurrentValue.ProductsDbConnectionString))
            {
                const string sqlQuery = @"SELECT 
                    id,
                    modelname,                    
                    Producttype,
                    createdon,
                    modifiedon
                FROM Products
                WHERE id=@id;";
                return await con.QueryFirstAsync<ProductEntity>(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);
            }
        }

        public async Task<bool> UpdateProductAsync(ProductEntity product)
        {
            product.ModifiedOn = DateTime.UtcNow;

            const string sqlQuery = @"UPDATE products SET                
                modelname = @modelname,                
                type = @type,
                createdon = @createdon,
                modifiedon = @modifiedon
            WHERE id = @id;";

            using (var con = new SqlConnection(_options.CurrentValue.ProductsDbConnectionString))
            {
                await con.ExecuteAsync(sqlQuery, product, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            const string sqlQuery = @"DELETE FROM products WHERE id = @id;";
            using (var con = new SqlConnection(_options.CurrentValue.ProductsDbConnectionString))
            {
                await con.ExecuteAsync(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<IEnumerable<ProductEntity>> GetProductsListAsync(int pageNumber, int pageSize)
        {
            using (var con = new SqlConnection(_options.CurrentValue.ProductsDbConnectionString))
            {
                var offset = pageNumber <= 1 ? 0 : (pageNumber - 1) * pageSize;
                const string sqlQuery = @"SELECT 
                    id,
                    modelname,                    
                    type,
                    createdon,
                    modifiedon
                FROM products
                LIMIT @pageSize OFFSET @offset;";
                return await con.QueryAsync<ProductEntity>(sqlQuery, new { pageSize, offset }, commandType: CommandType.Text);
            }
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var con = new SqlConnection(_options.CurrentValue.ProductsDbConnectionString))
            {
                try
                {
                    con.Open();
                    con.Close();
                    return Task.FromResult(HealthCheckResult.Healthy());
                }
                catch
                {
                    return Task.FromResult(HealthCheckResult.Unhealthy());
                }
            }
        }
    }
}