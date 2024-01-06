namespace StoragewithComputerParts.Models
{
    public class Contractor
    {
        public int ContractorId { get; set; }
        public string ContractorName { get; set; } = string.Empty;
        public string ContractorAddress { get; set; } = string.Empty;
        public string ContractorCity { get; set; } = string.Empty;
        public string ContractorPostalCode { get; set; } = string.Empty;
        public string ContractorNIP { get; set; } = string.Empty;
        public string ContractorPhoneNumber { get; set; } = string.Empty;
        public string ContractorEmail { get; set; } = string.Empty;
        public string ContractorWebsite { get; set; } = string.Empty;

        //public Contractor() { }

        //Relationships
        public List<Delivery> Deliveries { get; set; }
    }
}
