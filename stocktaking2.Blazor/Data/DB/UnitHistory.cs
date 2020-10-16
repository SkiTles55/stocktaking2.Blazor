using System;

namespace stocktaking2.Blazor.Data.DB
{
    public class UnitHistory
    {
        public int Id { get; set; }
        public string Change { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public string UserName { get; set; }
        public bool Secure { get; set; }
        public DateTime Time { get; set; }
        public UnitHistory()
        {
            Time = DateTime.Now;
        }
    }
}
