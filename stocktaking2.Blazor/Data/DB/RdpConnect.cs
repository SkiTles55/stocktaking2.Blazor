using System;

namespace stocktaking2.Blazor.Data.DB
{
    public class RdpConnect
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }
        public string Comment { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public DateTime DateCreated { get; set; }
        public RdpConnect()
        {
            DateCreated = DateTime.Now;
        }
    }
}
