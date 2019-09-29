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

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
