using System;
using System.Collections.Generic;

namespace stocktaking2.Blazor.Data.DB
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Location { get; set; }
        public bool InventId { get; set; }
        public bool Serial { get; set; }
        public bool BuyDate { get; set; }
        public bool InstallDate { get; set; }
        public bool Employer { get; set; }
        public bool Departament { get; set; }
        public bool Processor { get; set; }
        public bool Motherboard { get; set; }
        public bool DDR { get; set; }
        public bool NetName { get; set; }
        public bool IPAdresses { get; set; }
        public bool BiosPass { get; set; }
        public bool WinAccounts { get; set; }
        public bool ServiceWorks { get; set; }
        public bool WinName { get; set; }
        public bool RdpConnects { get; set; }
        public bool Specs { get; set; }
        public bool Manufacturer { get; set; }
        public bool Model { get; set; }
        public bool Comments { get; set; }
        public bool CartridgeModel { get; set; }
        public bool CartridgeCount { get; set; }
        public bool StoredFiles { get; set; }
        public bool InstaledSofts { get; set; }
        public List<Unit> Units { get; set; }
        public IEnumerable<ManufacturerCategories> Manufacturers { get; set; }
        public DateTime DateCreated { get; set; }
        public Category()
        {
            Units = new List<Unit>();
            Manufacturers = new List<ManufacturerCategories>();
            DateCreated = DateTime.Now;
        }
    }
}
