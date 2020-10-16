using System;

namespace stocktaking2.Blazor.Data.DB
{
    public class UserHistory
    {
        public int Id { get; set; }
        public string Changes { get; set; }
        public string UserName { get; set; }
        public DateTime Time { get; set; }
        public UserHistory()
        {
            Time = DateTime.Now;
        }
    }
}
