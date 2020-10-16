using System.Collections.Generic;

namespace stocktaking2.Blazor.Helpers
{
    public class ChartData
    {
        public Dictionary<string, double> depUnitsBar { get; set; }
        public Dictionary<string, double> catUnitsPie { get; set; }
    }
}
