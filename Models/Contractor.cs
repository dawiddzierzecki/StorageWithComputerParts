using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace StoragewithComputerParts.Models
{
    public class Contractor
    {
        public int ContractorId { get; set; }
        [Required]
        public string ContractorName { get; set; } 
        public string? ContractorAddress { get; set; }
        public string? ContractorCity { get; set; }
        public string? ContractorPostalCode { get; set; }

        [Required(ErrorMessage = "You must provide NIP")]
        [RegularExpression(@"([0-9]{10})$", ErrorMessage = "Not a NIP")]
        public string ContractorNIP { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)??([0-9]{3})?([0-9]{3})$", ErrorMessage = "Not a valid phone number")]
        public string ContractorPhoneNumber { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ContractorEmail { get; set; }
        public string? ContractorWebsite { get; set; }
        //public Contractor() { }

        //Relationships
        public List<Delivery>? Deliveries { get; set; }
        public List<Release>? Releases { get; set; }
    }
}
