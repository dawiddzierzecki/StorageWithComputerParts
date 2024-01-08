﻿namespace StoragewithComputerParts.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string? Comment { get; set; }

        //Relationships
        List<DeliveryProducts> DeliveryProducts { get; set; } = new List<DeliveryProducts>();
        
        public Contractor Contractor { get; set; } = new Contractor();
        public int ContractorId { get; set; }

        public Protocol Protocol { get; set; } = new Protocol();
        public int ProtocolId { get; set; }
    }
}
