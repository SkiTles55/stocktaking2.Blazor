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

            var man1 = new Manufacturer { Name = "Brother" };
            var man2 = new Manufacturer { Name = "Kyocera" };
            var man3 = new Manufacturer { Name = "HP" };
            var man4 = new Manufacturer { Name = "Benq" };
            var man5 = new Manufacturer { Name = "Canon" };
            var man6 = new Manufacturer { Name = "Ippon" };
            var man7 = new Manufacturer { Name = "APC" };
            var man8 = new Manufacturer { Name = "Xerox" };
            var man9 = new Manufacturer { Name = "LG" };
            var man10 = new Manufacturer { Name = "Samsung" };
            var man11 = new Manufacturer { Name = "ViewSonic" };
            var man12 = new Manufacturer { Name = "D-Link" };
            var man13 = new Manufacturer { Name = "Unknown" };
            var man14 = new Manufacturer { Name = "FSP" };
            var man15 = new Manufacturer { Name = "Crown" };
            var man16 = new Manufacturer { Name = "PowerCom" };
            var man17 = new Manufacturer { Name = "Powerman" };
            var man18 = new Manufacturer { Name = "Sven" };
            var man19 = new Manufacturer { Name = "AOC" };
            var man20 = new Manufacturer { Name = "Asus" };
            var man21 = new Manufacturer { Name = "Acer" };
            var man22 = new Manufacturer { Name = "Dexp" };
            var man23 = new Manufacturer { Name = "Dell" };
            var man24 = new Manufacturer { Name = "Lenovo" };
            var man25 = new Manufacturer { Name = "NEC" };
            var man26 = new Manufacturer { Name = "Philips" };
            var man27 = new Manufacturer { Name = "TONK" };
            if (!context.Manufacturers.Any())
            {
                context.Manufacturers.AddRange(man1, man2, man3, man4, man5, man6, man7, man8, man9, man10, man11, man12, man13, man14,man15,man16,man17,man18,man19,man20,man21,man22,man23,man24,man25,man26,man27);
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