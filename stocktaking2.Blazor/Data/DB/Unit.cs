using System;
using System.Collections.Generic;

namespace stocktaking2.Blazor.Data.DB
{
    public class Unit
    {
        public int Id { get; set; }
        public int UnitStatusId { get; set; }
        public UnitStatus UnitStatus { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }
        public string InventId { get; set; }
        public string Serial { get; set; }
        public DateTime? BuyDate { get; set; }
        public DateTime? InstallDate { get; set; }
        public int? EmployerId { get; set; }
        public Employer Employer { get; set; }
        public int? DepartamentId { get; set; }
        public Departament Departament { get; set; }
        public int? WinNameId { get; set; }
        public WinName WinName { get; set; }
        public string Processor { get; set; }
        public string Motherboard { get; set; }
        public string DDR { get; set; }
        public string Specs { get; set; }
        public string CartridgeModel { get; set; }
        public int? CartridgeCount { get; set; }
        public List<StoredFile> StoredFiles { get; set; }
        public List<ServiceWork> ServiceWorks { get; set; }
        public List<IPs> IPAdresses { get; set; }
        public string NetName { get; set; }
        public string BiosPass { get; set; }
        public List<WinAccount> WinAccounts { get; set; }
        public List<RdpConnect> RdpConnects { get; set; }
        public string Comment { get; set; }
        public List<UnitHistory> UnitHistories { get; set; }
        public IEnumerable<UnitInstalledSofts> UnitInstalledSofts { get; set; }
        public DateTime DateCreated { get; set; }
        public Unit()
        {
            StoredFiles = new List<StoredFile>();
            ServiceWorks = new List<ServiceWork>();
            IPAdresses = new List<IPs>();
            WinAccounts = new List<WinAccount>();
            RdpConnects = new List<RdpConnect>();
            UnitHistories = new List<UnitHistory>();
            UnitInstalledSofts = new List<UnitInstalledSofts>();
            DateCreated = DateTime.Now;
        }
    }
}
