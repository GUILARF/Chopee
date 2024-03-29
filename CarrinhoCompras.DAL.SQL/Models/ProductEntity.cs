﻿using System;

namespace CarrinhoCompras.DAL.SQL.Models
{
    public class ProductEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}