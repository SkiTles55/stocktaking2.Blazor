using System;
using System.Collections.Generic;

namespace stocktaking2.Blazor.Data.DB
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Unit> Units { get; set; }
        public IEnumerable<ManufacturerCategories> Categories { get; set; }
        public DateTime DateCreated { get; set; }
        public Manufacturer()
        {
            Units = new List<Unit>();
            Categories = new List<ManufacturerCategories>();
            DateCreated = DateTime.Now;
        }
    }
}
