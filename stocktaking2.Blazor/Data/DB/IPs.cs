using System.Net;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace stocktaking2.Blazor.Data.DB
{
    public class IPs
    {
        public int Id { get; set; }
        [Required, MinLength(4), MaxLength(16)]
        public byte[] IPAddressBytes { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public DateTime DateCreated { get; set; }
        
        [NotMapped]
        public IPAddress IPAddress
        {
            get { return new IPAddress(IPAddressBytes); }
            set { IPAddressBytes = value.GetAddressBytes(); }
        }
        public IPs()
        {
            DateCreated = DateTime.Now;
        }
    }
}
