namespace StoragewithComputerParts.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public Data.Enums.ProductCategory.Category ProductCategory { get; set; } = Data.Enums.ProductCategory.Category.Other;
       // public string ProductCategoryDescription { get; set;}

        //public Product() { }

        //Relationships
        public Stock? Stock { get; set; }
        public List<ReleaseProducts> ReleaseProducts { get; } = new List<ReleaseProducts>();
        public List<DeliveryProducts> DeliveryProducts { get; set; } = new List<DeliveryProducts>();

    }
}
