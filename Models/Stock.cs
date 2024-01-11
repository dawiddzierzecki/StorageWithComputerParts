using System.ComponentModel.DataAnnotations.Schema;

namespace StoragewithComputerParts.Models
{
    public class Stock
    {
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
