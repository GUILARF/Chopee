using System;
using System.ComponentModel.DataAnnotations;

namespace CarrinhoCompras.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(45, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        public ProductType Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
