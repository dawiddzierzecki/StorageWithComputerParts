namespace StoragewithComputerParts.Models
{
    public class Stock
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
