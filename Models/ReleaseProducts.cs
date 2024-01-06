namespace StoragewithComputerParts.Models
{
    public class ReleaseProducts
    {
        public int ReleaseId { get; set; }
        public Release Release { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
