using System;

namespace CarrinhoCompras.BLL.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CarType Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
