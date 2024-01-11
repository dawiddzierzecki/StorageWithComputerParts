using StoragewithComputerParts.Data.Enums;

namespace StoragewithComputerParts.Models
{
    public class Protocol
    {
        public int ProtocolId { get; set; }
        public DateTime ProtocolDate { get; set; }
        public string? Comment { get; set; }
        public ProtocolType ProtocolType { get; set; }
        public string ProtocolFilePath { get; set; } = string.Empty;


        //Relationships
        public Release? Release { get; set; }
        public Delivery? Delivery { get; set; }
    }
}
