using StoragewithComputerParts.Models;

namespace StoragewithComputerParts.ViewModels
{
    public class AddDeliveryViewModel
    {

        public DateTime DeliveryTime { get; set; }
        public string Comment { get; set; }
        public int ContractorId { get; set; }

        public List<AddDeliveryProductViewModel> Products { get; set; }
    }
}
