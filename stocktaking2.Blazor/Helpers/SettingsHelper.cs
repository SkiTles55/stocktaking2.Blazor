using System.Collections.Generic;

namespace stocktaking2.Blazor.Helpers
{
    public class SettingsHelper
    {
        public Dictionary<string,int> Collumns { get; set; }
        public int SortOrder { get; set; }
    }
}
