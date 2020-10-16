using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using stocktaking2.Blazor.Data.DB;

namespace stocktaking2.Blazor.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryHistory> CategoryHistories { get; set; }
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<DepartamentHistory> DepartamentHistories { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailHistory> EmailHistories { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<EmployerHistory> EmployerHistories { get; set; }
        public DbSet<InstalledSoft> InstalledSofts { get; set; }
        public DbSet<InstalledSoftHistory> InstalledSoftHistories { get; set; }
        public DbSet<IPs> IPadresses { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ManufacturerHistory> ManufacturerHistories { get; set; }
        public DbSet<RdpConnect> RdpConnects { get; set; }
        public DbSet<ServiceWork> ServiceWorks { get; set; }
        public DbSet<StoredFile> StoredFiles { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UnitHistory> UnitHistories { get; set; }
        public DbSet<UnitStatus> UnitStatuses { get; set; }
        public DbSet<UnitStatusHistory> UnitStatusHistories { get; set; }
        public DbSet<UserHistory> UserHistories { get; set; }
        public DbSet<WinAccount> WinAccounts { get; set; }
        public DbSet<WinName> WinNames { get; set; }
        public DbSet<WinNameHistory> WinNameHistories { get; set; }
        public DbSet<DisposedUnit> DisposedUnits { get; set; }
        public DbSet<ManufacturerCategories> ManufacturerCategories { get; set; }
        public DbSet<UnitInstalledSofts> UnitInstalledSofts { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
