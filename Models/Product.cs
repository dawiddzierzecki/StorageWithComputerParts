namespace StoragewithComputerParts.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public string ProductCategoryDescription { get; set;}

        //public Product() { }

        //Relationships
        public Stock? Stock { get; set; }
        public List<ReleaseProduct> ReleaseProducts { get; set; }
        public List<DeliveryProduct> DeliveryProducts { get; set; }

    }
}
