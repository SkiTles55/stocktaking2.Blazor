namespace stocktaking2.Blazor.Data.DB
{
    public class UserSettings
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string CollumnSettings { get; set; }
        public int SortOrder { get; set; }
        public int RowsCount { get; set; }
    }
}
