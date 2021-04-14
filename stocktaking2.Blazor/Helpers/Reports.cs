using System.Drawing;

namespace stocktaking2.Blazor.Helpers
{
    public class EmailReport
    {
        public bool Password { get; set; }
        public string HeaderColor { get; set; }
        public bool HeaderBold { get; set; }
        public string EmployerColor { get; set; }
        public string CellsColor { get; set; }
        public bool EmployerBold { get; set; }
        public bool EmailBold { get; set; }
    }

    public class DepReport
    {
        public string HeaderColor { get; set; }
        public bool HeaderBold { get; set; }
        public string DepColor { get; set; }
        public bool DepBold { get; set; }
        public string CellsColor { get; set; }
        public bool EmpCount { get; set; }
        public bool UnitsCount { get; set; }
    }

    public class EmpReport
    {
        public string HeaderColor { get; set; }
        public bool HeaderBold { get; set; }
        public string EmpColor { get; set; }
        public bool EmpBold { get; set; }
        public string CellsColor { get; set; }
        public bool Dep { get; set; }
        public bool Post { get; set; }
        public bool Email { get; set; }
        public bool UnitCount { get; set; }
    }

    public class UnitReport
    {
        public string HeaderColor { get; set; }
        public bool HeaderBold { get; set; }
        public string CellsColor { get; set; }
        public bool Filter { get; set; }
        public bool UnitStatus { get; set; }
        public bool Category { get; set; }
        public bool Manufacturer { get; set; }
        public bool Model { get; set; }
        public bool Location { get; set; }
        public bool InventId { get; set; }
        public bool Serial { get; set; }
        public bool BuyDate { get; set; }
        public bool InstallDate { get; set; }
        public bool Employer { get; set; }
        public bool Departament { get; set; }
        public bool WinName { get; set; }
        public bool Processor { get; set; }
        public bool Motherboard { get; set; }
        public bool DDR { get; set; }
        public bool Specs { get; set; }
        public bool CartridgeModel { get; set; }
        public bool CartridgeCount { get; set; }
        public bool ServiceWorks { get; set; }
        public bool IPAdresses { get; set; }
        public bool NetName { get; set; }
        public bool BiosPass { get; set; }
        public bool WinAccounts { get; set; }
        public bool RdpConnects { get; set; }
        public bool Comment { get; set; }
        public bool UnitInstalledSofts { get; set; }
        public int catFilter { get; set; } = 0;
        public int depFilter { get; set; } = 0;
        public int empFilter { get; set; } = 0;
        public int manFilter { get; set; } = 0;
        public int statFilter { get; set; } = 0;
        public int winFilter { get; set; } = 0;
        public int softFilter { get; set; } = 0;
    }
}
