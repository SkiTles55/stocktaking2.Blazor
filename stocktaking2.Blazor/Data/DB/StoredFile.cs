using System;

namespace stocktaking2.Blazor.Data.DB
{
    public class StoredFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public DateTime UploadData { get; set; }
        public StoredFile()
        {
            UploadData = DateTime.Now;
        }
    }
}
