namespace stocktaking2.Blazor.Data.DB
{
    public class UnitInstalledSofts
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public int InstalledSoftId { get; set; }
        public InstalledSoft InstalledSoft { get; set; }
    }
}