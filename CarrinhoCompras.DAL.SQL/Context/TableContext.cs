using CarrinhoCompras.DAL.SQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarrinhoCompras.DAL.SQL.Context
{
    class TableContext : DbContext
    {
        public TableContext() : base() { }
        public DbSet<ProductEntity> product { get; set; }
    }
}
