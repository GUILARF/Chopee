using CarrinhoCompras.DAL.SQL.Interfaces;
using CarrinhoCompras.DAL.SQL.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
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
    public class ProductsRepository : IProductsRepository, IHealthCheck
    {
        private readonly IOptionsMonitor<ProductsSqlRepositoryOptions> _options;
        private IConfiguration configuration;

        public ProductsRepository(IOptionsMonitor<ProductsSqlRepositoryOptions> options, IConfiguration Iconfig)
        {
            _options = options;
            configuration = Iconfig;
        }

        public async Task<ProductEntity> CreateProductAsync(ProductEntity newProduct)
        {
            var dnow = DateTime.UtcNow;
            newProduct.CreatedOn = dnow;
            newProduct.ModifiedOn = dnow;

            const string sqlQuery = @"INSERT INTO product (

                    Name,
                    Type,
                    createdon,
                    modifiedon
                )
                VALUES (

                    @Name,
                    @Type,
                    @Createdon,
                    @Modifiedon);";

            using (var con = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionString").Value))
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

        public async Task<ProductEntity> GetProductAsync(long id)
        {
            using (var con = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionString").Value))
            {
                const string sqlQuery = @"SELECT
                    id,
                    Name,
                    Type,
                    Createdon,
                    Modifiedon
                FROM product
                WHERE id=@id;";
                return await con.QueryFirstAsync<ProductEntity>(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);
            }
        }

        public async Task<bool> UpdateProductAsync(ProductEntity product)
        {
            product.ModifiedOn = DateTime.UtcNow;

            const string sqlQuery = @"UPDATE product SET
                Name = @Name,
                Type = @Type,
                Createdon = @Createdon,
                Modifiedon = @Modifiedon
            WHERE id = @id;";

            using (var con = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionString").Value))
            {
                await con.ExecuteAsync(sqlQuery, product, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            const string sqlQuery = @"DELETE FROM product WHERE id = @id;";
            using (var con = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionString").Value))
            {
                await con.ExecuteAsync(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<IEnumerable<ProductEntity>> GetProductsListAsync(int pageNumber, int pageSize)
        {
            using (var con = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionString").Value))
            {
                var offset = pageNumber <= 1 ? 0 : (pageNumber - 1) * pageSize;
                const string sqlQuery = @"SELECT
                    id,
                    Name,
                    Type,
                    Createdon,
                    Modifiedon
                FROM product
                LIMIT @pageSize OFFSET @offset;";
                return await con.QueryAsync<ProductEntity>(sqlQuery, new { pageSize, offset }, commandType: CommandType.Text);
            }
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var con = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnectionString").Value))
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