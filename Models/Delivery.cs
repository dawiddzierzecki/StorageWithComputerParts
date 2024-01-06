namespace StoragewithComputerParts.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Comment { get; set; }

        //Relationships
        List<DeliveryProducts> DeliveryProducts { get; set; }
        public int ContractorId { get; set; }

        public Protocol Protocol { get; set; }
        public int ProtocolId { get; set; }
    }
}
