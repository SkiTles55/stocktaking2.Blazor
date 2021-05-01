using stocktaking2.Blazor.Data;
using stocktaking2.Blazor.Data.DB;
using System;
using System.Linq;

namespace stocktaking2.Blazor.Helpers
{
    public class SampleData
    {
        private static readonly Random getrandom = new Random();
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.WinNames.Any())
            {
                context.WinNames.AddRange(
                    new WinName { Name = "Windows XP" },
                    new WinName { Name = "Windows XP Professional" },
                    new WinName { Name = "Windows 7 Начальная x32" },
                    new WinName { Name = "Windows 7 Начальная x64" },
                    new WinName { Name = "Windows 7 Домашняя базовая x32" },
                    new WinName { Name = "Windows 7 Домашняя базовая x64" },
                    new WinName { Name = "Windows 7 Домашняя расширенная x32" },
                    new WinName { Name = "Windows 7 Домашняя расширенная x64" },
                    new WinName { Name = "Windows 7 Профессиональная x32" },
                    new WinName { Name = "Windows 7 Профессиональная x64" },
                    new WinName { Name = "Windows 7 Максимальная x32" },
                    new WinName { Name = "Windows 7 Максимальная x64" },
                    new WinName { Name = "Windows 10 Pro x64" },
                    new WinName { Name = "Windows Server 2008 R2 Standart x64" }
                    );
                context.SaveChanges();
            }
            if (!context.UnitStatuses.Any())
            {
                context.UnitStatuses.AddRange(
                    new UnitStatus { Name = "На списании" },
                    new UnitStatus { Name = "Эксплуатация" },
                    new UnitStatus { Name = "В ремонте" },
                    new UnitStatus { Name = "Не используется" }
                    );
                context.SaveChanges();
            }
            if (!context.Manufacturers.Any())
            {
                context.Manufacturers.AddRange(
                    new Manufacturer { Name = "Brother" },
                    new Manufacturer { Name = "Kyocera" },
                    new Manufacturer { Name = "HP" },
                    new Manufacturer { Name = "Benq" },
                    new Manufacturer { Name = "Canon" },
                    new Manufacturer { Name = "Ippon" },
                    new Manufacturer { Name = "APC" },
                    new Manufacturer { Name = "Xerox" },
                    new Manufacturer { Name = "LG" },
                    new Manufacturer { Name = "Samsung" },
                    new Manufacturer { Name = "ViewSonic" },
                    new Manufacturer { Name = "D-Link" },
                    new Manufacturer { Name = "Unknown" },
                    new Manufacturer { Name = "FSP" },
                    new Manufacturer { Name = "Crown" },
                    new Manufacturer { Name = "PowerCom" },
                    new Manufacturer { Name = "Powerman" },
                    new Manufacturer { Name = "Sven" },
                    new Manufacturer { Name = "AOC" },
                    new Manufacturer { Name = "Asus" },
                    new Manufacturer { Name = "Acer" },
                    new Manufacturer { Name = "Dexp" },
                    new Manufacturer { Name = "Dell" },
                    new Manufacturer { Name = "Lenovo" },
                    new Manufacturer { Name = "NEC" },
                    new Manufacturer { Name = "Philips" },
                    new Manufacturer { Name = "TONK" }
                    );
                context.SaveChanges();
            }
            if (!context.InstalledSofts.Any())
            {
                context.InstalledSofts.AddRange(
                    new InstalledSoft { Name = "Крипто-ПРО" },
                    new InstalledSoft { Name = "Kaspersky Endpoint Security" },
                    new InstalledSoft { Name = "ТМ МИС" }
                    );
                context.SaveChanges();
            }/*
            if (!context.Units.Any())
            {
                for (int i = 1; i < 121; i++)
                {
                    context.Units.Add(new Unit { Model = "TempUnit" + i, UnitStatusId = getrandom.Next(1, 4), CategoryId = getrandom.Next(1, 10), EmployerId = getrandom.Next(1, 12) });
                }
                context.SaveChanges();
            }*/
        }
    }
}