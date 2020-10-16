namespace stocktaking2.Blazor.Data.DB
{
    public class ManufacturerCategories
    {
        public int ID { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
