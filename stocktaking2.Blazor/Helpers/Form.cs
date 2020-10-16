using stocktaking2.Blazor.Data.DB;
using System;
using System.Collections.Generic;

namespace stocktaking2.Blazor.Helpers
{
    public class UnitForm
    {
        public int Id { get; set; }
        public int? UnitStatusId { get; set; }
        public int? CategoryId { get; set; }
        public int? ManufacturerId { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }
        public string InventId { get; set; }
        public string Serial { get; set; }
        public DateTime? BuyDate { get; set; }
        public DateTime? InstallDate { get; set; }
        public int? EmployerId { get; set; }
        public int? DepartamentId { get; set; }
        public int? WinNameId { get; set; }
        public string Processor { get; set; }
        public string Motherboard { get; set; }
        public string DDR { get; set; }
        public string Specs { get; set; }
        public string CartridgeModel { get; set; }
        public int? CartridgeCount { get; set; }
        public string NetName { get; set; }
        public string BiosPass { get; set; }
        public string Comment { get; set; }
        public Dictionary<string, bool> Soft { get; set; }
        public List<string> IPaddresses { get; set; }
        public Dictionary<string, string> WinAccounts { get; set; }
        public List<SWAdd> ServiceWorks { get; set; }
        public List<RDPAcc> RDPAccounts { get; set; }
    }

    public class SWAdd
    {
        public string WorkName { get; set; }
        public string WorkDescr { get; set; }
        public DateTime WorkDate { get; set; }
    }

    public class RDPAcc
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }
        public string Comment { get; set; }
    }

    public class ManForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, bool> categories { get; set; }
    }

    public class ActivityLog
    {
        public List<CategoryHistory> categoryHistories { get; set; }
        public List<DepartamentHistory> departamentHistories { get; set; }
        public List<EmailHistory> emailHistories { get; set; }
        public List<EmployerHistory> employerHistories { get; set; }
        public List<InstalledSoftHistory> installedSoftHistories { get; set; }
        public List<ManufacturerHistory> manufacturerHistories { get; set; }
        public List<UnitLog> UnitLogs { get; set; }
        public List<UnitStatusHistory> unitStatusHistories { get; set; }
        public List<UserHistory> userHistories { get; set; }
        public List<WinNameHistory> winNameHistories { get; set; }
    }

    public class UnitLog
    {
        public DateTime Time { get; set; }
        public string UnitModel { get; set; }
        public int UnitId { get; set; }
        public string Change { get; set; }
    }
}