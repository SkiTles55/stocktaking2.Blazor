using System;
using System.Collections.Generic;

namespace stocktaking2.Blazor.Data.DB
{
    public class Employer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }
        public int DepartamentId { get; set; }
        public Departament Departament { get; set; }
        public List<Email> Emails { get; set; }
        public List<Unit> Units { get; set; }
        public DateTime DateCreated { get; set; }
        public Employer()
        {
            Emails = new List<Email>();
            Units = new List<Unit>();
            DateCreated = DateTime.Now;
        }
    }
}
