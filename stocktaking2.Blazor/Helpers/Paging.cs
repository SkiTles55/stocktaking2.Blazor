using stocktaking2.Blazor.Data.DB;
using System.Collections.Generic;

namespace stocktaking2.Blazor.Helpers
{
    public class EmpPaging
    {
        public List<Employer> Employers { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

    public class UnitsPaging
    {
        public List<Unit> Units { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

    public class DepPaging
    {
        public List<Departament> Departaments { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

    public class EmailPaging
    {
        public List<Email> Emails { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

    public class CatPaging
    {
        public List<Category> Categories { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

    public class InstSoftPaging
    {
        public List<InstalledSoft> InstalledSofts { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

    public class ManPaging
    {
        public List<Manufacturer> Manufacturers { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

    public class StatusPaging
    {
        public List<UnitStatus> UnitStatuses { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }

    public class WinNamePaging
    {
        public List<WinName> WinNames { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
