using System;
using System.Collections.Generic;

namespace stocktaking2.Blazor.Data.DB
{
    public class Departament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employer> Employers { get; set; }
        public List<Unit> Units { get; set; }
        public DateTime DateCreated { get; set; }
        public Departament()
        {
            Employers = new List<Employer>();
            Units = new List<Unit>();
            DateCreated = DateTime.Now;
        }
    }
}
