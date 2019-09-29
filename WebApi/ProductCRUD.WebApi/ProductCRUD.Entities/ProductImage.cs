using System;
using System.ComponentModel.DataAnnotations;

namespace ProductCRUD.Entities
{
    public class ProductImage
    {
        [Key]
        public Guid Id { get; set; }
        public int ProductId { get; set; }
    }
}
