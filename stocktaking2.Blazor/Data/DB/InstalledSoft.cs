using System;
using System.Collections.Generic;

namespace stocktaking2.Blazor.Data.DB
{
    public class InstalledSoft
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UnitInstalledSofts> UnitInstalledSofts { get; set; }
        public DateTime DateCreated { get; set; }
        public InstalledSoft()
        {
            UnitInstalledSofts = new List<UnitInstalledSofts>();
            DateCreated = DateTime.Now;
        }
    }
}
