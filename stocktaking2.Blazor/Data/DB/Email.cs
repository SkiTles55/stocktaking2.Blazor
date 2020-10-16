using System;

namespace stocktaking2.Blazor.Data.DB
{
    public class Email
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? EmployerId { get; set; }
        public Employer Employer { get; set; }
        public DateTime DateCreated { get; set; }
        public Email()
        {
            DateCreated = DateTime.Now;
        }
    }
}
