using CarrinhoCompras.DAL.SQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarrinhoCompras.DAL.SQL.Context
{
    public class TableContext : DbContext
    {
        public TableContext(DbContextOptions<TableContext> options)
        : base(options)
        { }
        private readonly string _connectionString;

        public TableContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source =.; Initial Catalog = Chopee; Integrated Security = True");
        }

        public DbSet<ProductEntity> product { get; set; }

        
    }
}
