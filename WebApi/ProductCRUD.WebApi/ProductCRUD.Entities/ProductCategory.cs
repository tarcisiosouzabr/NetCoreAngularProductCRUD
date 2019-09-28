using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCRUD.Entities
{
    public class ProductCategory
    {
        [Key]
        [Column(Order = 1)]
        public int CategoryId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ProductId { get; set; }
    }
}
