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
    public class CarsRepository: ICarsRepository, IHealthCheck
    {
        private readonly IOptionsMonitor<CarsSqlRepositoryOptions> _options;

        public CarsRepository(IOptionsMonitor<CarsSqlRepositoryOptions> options)
        {
            _options = options;
        }

        public async Task<CarEntity> CreateCarAsync(CarEntity newCar)
        {
            //if (newCar.Id == Guid.Empty.ToString())
            //{
            //    newCar.Id = Guid.NewGuid().ToString();
            //}
            var dnow = DateTime.UtcNow;
            newCar.CreatedOn = dnow;
            newCar.ModifiedOn = dnow;
            newCar.CarType = 1;

            const string sqlQuery = @"INSERT INTO cars (
                    
                    modelname,
                    cartype,
                    createdon,
                    modifiedon
                )
                VALUES (
                    
                    @modelname,
                    @cartype,
                    @createdon,
                    @modifiedon);";

            using (var con = new SqlConnection(_options.CurrentValue.CarsDbConnectionString))
            {
                //await db.(sqlQuery, newCar, commandType: CommandType.Text);
                //return newCar;

                try
                {
                    con.Open();
                    await con.ExecuteAsync(sqlQuery, newCar, commandType: CommandType.Text);
                    return newCar;
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

        public async Task<CarEntity> GetCarAsync(Guid id)
        {
            using (var con = new SqlConnection(_options.CurrentValue.CarsDbConnectionString))
            {
                const string sqlQuery = @"SELECT 
                    id,
                    modelname,                    
                    cartype,
                    createdon,
                    modifiedon
                FROM cars
                WHERE id=@id;";
                return await con.QueryFirstAsync<CarEntity>(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);
            }
        }

        public async Task<bool> UpdateCarAsync(CarEntity car)
        {
            car.ModifiedOn = DateTime.UtcNow;

            const string sqlQuery = @"UPDATE cars SET                
                modelname = @modelname,                
                cartype = @cartype,
                createdon = @createdon,
                modifiedon = @modifiedon
            WHERE id = @id;";

            using (var con = new SqlConnection(_options.CurrentValue.CarsDbConnectionString))
            {
                await con.ExecuteAsync(sqlQuery, car, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<bool> DeleteCarAsync(Guid id)
        {
            const string sqlQuery = @"DELETE FROM cars WHERE id = @id;";
            using (var con = new SqlConnection(_options.CurrentValue.CarsDbConnectionString))
            {
                await con.ExecuteAsync(sqlQuery, new { id = id.ToString() }, commandType: CommandType.Text);
                return true;
            }
        }

        public async Task<IEnumerable<CarEntity>> GetCarsListAsync(int pageNumber, int pageSize)
        {
            using (var con = new SqlConnection(_options.CurrentValue.CarsDbConnectionString))
            {
                var offset = pageNumber <= 1 ? 0 : (pageNumber - 1) * pageSize;
                const string sqlQuery = @"SELECT 
                    id,
                    modelname,                    
                    cartype,
                    createdon,
                    modifiedon
                FROM cars
                LIMIT @pageSize OFFSET @offset;";
                return await con.QueryAsync<CarEntity>(sqlQuery, new { pageSize, offset }, commandType: CommandType.Text);
            }
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var con = new SqlConnection(_options.CurrentValue.CarsDbConnectionString))
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