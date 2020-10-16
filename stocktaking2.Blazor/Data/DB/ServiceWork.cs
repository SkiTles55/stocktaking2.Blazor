using System;

namespace stocktaking2.Blazor.Data.DB
{
    public class ServiceWork
    {
        public int Id { get; set; }
        public string WorkName { get; set; }
        public string WorkDescr { get; set; }
        public DateTime WorkDate { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public DateTime DateCreated { get; set; }
        public ServiceWork()
        {
            DateCreated = DateTime.Now;
        }
    }
}
