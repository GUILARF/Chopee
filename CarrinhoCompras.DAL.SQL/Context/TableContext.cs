using CarrinhoCompras.DAL.SQL.Models;
using Microsoft.EntityFrameworkCore;

namespace CarrinhoCompras.DAL.SQL.Context
{
    internal class TableContext : DbContext
    {
        public TableContext() : base()
        {
        }

        public DbSet<ProductEntity> product { get; set; }
    }
}