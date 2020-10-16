using System.Drawing;

namespace stocktaking2.Blazor.Helpers
{
    public class EmailReport
    {
        public bool Password { get; set; }
        public string HeaderColor { get; set; }
        public bool HeaderBold { get; set; }
        public string EmployerColor { get; set; }
        public string CellsColor { get; set; }
        public bool EmployerBold { get; set; }
        public bool EmailBold { get; set; }
    }

    public class DepReport
    {
        public string HeaderColor { get; set; }
        public bool HeaderBold { get; set; }
        public string DepColor { get; set; }
        public bool DepBold { get; set; }
        public string CellsColor { get; set; }
        public bool EmpCount { get; set; }
        public bool UnitsCount { get; set; }
    }

    public class EmpReport
    {
        public string HeaderColor { get; set; }
        public bool HeaderBold { get; set; }
        public string EmpColor { get; set; }
        public bool EmpBold { get; set; }
        public string CellsColor { get; set; }
        public bool Dep { get; set; }
        public bool Post { get; set; }
        public bool Email { get; set; }
        public bool UnitCount { get; set; }
    }
}
