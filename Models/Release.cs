namespace StoragewithComputerParts.Models
{
    public class Release
    {
        public int ReleaseId { get; set; }
        public int Quantity { get; set; }
        public string? Comment { get; set; }
        public DateTime ReleaseDate { get; set; }

        //Relationships
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
        List<ReleaseProducts> ReleaseProducts { get; set; }
        public Protocol Protocol { get; set; }
        public int ProtocolId { get; set; }
    }
}
