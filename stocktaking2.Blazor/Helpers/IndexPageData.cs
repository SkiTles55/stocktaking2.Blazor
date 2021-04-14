using stocktaking2.Blazor.Data.DB;
using System.Collections.Generic;

namespace stocktaking2.Blazor.Helpers
{
    public class IndexPageData
    {
        public Dictionary<string, double> depUnitsBar { get; set; }
        public Dictionary<string, double> catUnitsPie { get; set; }

        public int UnitsCount { get; set; }
        public List<Unit> Last5Units { get; set; }
        public int ServiceWorksMounthCount { get; set; }
        public List<ServiceWork> Last5ServiceWorks { get; set; }
    }
}
