namespace StoragewithComputerParts.Models
{
    public class DeliveryProducts
    {
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
