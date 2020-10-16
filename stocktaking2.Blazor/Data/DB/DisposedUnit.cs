using System;

namespace stocktaking2.Blazor.Data.DB
{
    public class DisposedUnit
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }
        public string InventId { get; set; }
        public string Serial { get; set; }
        public DateTime? BuyDate { get; set; }
        public DateTime? InstallDate { get; set; }
        public DateTime DisposeDate { get; set; }
        public string EmployerName { get; set; }
        public string DepartamentName { get; set; }
        public string WinName { get; set; }
        public string Processor { get; set; }
        public string Motherboard { get; set; }
        public string DDR { get; set; }
        public string Specs { get; set; }
        public string CartridgeModel { get; set; }
        public int? CartridgeCount { get; set; }
        public string ServiceWorksSummary { get; set; }
        public string InstalledSoftsSummary { get; set; }
        public string IPAdressesSummary { get; set; }
        public string NetName { get; set; }
        public string BiosPass { get; set; }
        public string WinAccountsSummary { get; set; }
        public string RdpConnectsSummary { get; set; }
        public string Comment { get; set; }
        public DateTime DateFirstCreated { get; set; }
        public DateTime AddedToDisposedUnits { get; set; }
        public DisposedUnit()
        {
            AddedToDisposedUnits = DateTime.Now;
        }
    }
}
