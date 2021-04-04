using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using stocktaking2.Blazor.Data.DB;
using stocktaking2.Blazor.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace stocktaking2.Blazor.Data
{
    public class DbService
    {
        private ApplicationDbContext _context;
        const string quote = "\"";

        public DbService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region OrdersDict
        private Dictionary<int, Expression<Func<Unit, object>>> UnitOrderDict = new Dictionary<int, Expression<Func<Unit, object>>>()
        {
            [1] = x => x.DateCreated,
            [2] = x => x.DateCreated,
            [3] = x => x.Category.Name,
            [4] = x => x.Category.Name,
            [5] = x => x.Departament.Name,
            [6] = x => x.Departament.Name,
            [7] = x => x.Employer.Name,
            [8] = x => x.Employer.Name,
            [9] = x => x.InstallDate,
            [10] = x => x.InstallDate,
            [11] = x => x.InventId,
            [12] = x => x.InventId,
            [13] = x => x.Location,
            [14] = x => x.Location,
            [15] = x => x.Manufacturer.Name,
            [16] = x => x.Manufacturer.Name,
            [17] = x => x.Model,
            [18] = x => x.Model,
            [19] = x => x.NetName,
            [20] = x => x.NetName,
            [21] = x => x.Serial,
            [22] = x => x.Serial,
            [23] = x => x.Specs,
            [24] = x => x.Specs,
            [25] = x => x.UnitStatus.Name,
            [26] = x => x.UnitStatus.Name,
            [27] = x => x.IPAdresses.OrderBy(a => a.IPAddressBytes).Select(a => a.IPAddressBytes).FirstOrDefault(),
            [28] = x => x.IPAdresses.OrderBy(a => a.IPAddressBytes).Select(a => a.IPAddressBytes).FirstOrDefault()

        };

        private Dictionary<int, Expression<Func<Employer, object>>> EmpOrderDict = new Dictionary<int, Expression<Func<Employer, object>>>()
        {
            [1] = x => x.LastName,
            [2] = x => x.LastName,
            [3] = x => x.FirstName,
            [4] = x => x.FirstName,
            [5] = x => x.FatherName,
            [6] = x => x.FatherName,
            [7] = x => x.Departament.Name,
            [8] = x => x.Departament.Name,
            [9] = x => x.Post,
            [10] = x => x.Post,
            [11] = x => x.Units.Count,
            [12] = x => x.Units.Count,
            [13] = x => x.Emails.OrderBy(a => a.Login).Select(a => a.Login).FirstOrDefault(),
            [14] = x => x.Emails.OrderBy(a => a.Login).Select(a => a.Login).FirstOrDefault()

        };

        private Dictionary<int, Expression<Func<Departament, object>>> DepOrderDict = new Dictionary<int, Expression<Func<Departament, object>>>()
        {
            [1] = x => x.Name,
            [2] = x => x.Name,
            [3] = x => x.Employers.Count,
            [4] = x => x.Employers.Count,
            [5] = x => x.Units.Count,
            [6] = x => x.Units.Count
        };

        private Dictionary<int, Expression<Func<Email, object>>> EmOrderDict = new Dictionary<int, Expression<Func<Email, object>>>()
        {
            [1] = x => x.Login,
            [2] = x => x.Login,
            [3] = x => x.Password,
            [4] = x => x.Password,
            [5] = x => x.Employer.Name,
            [6] = x => x.Employer.Name
        };

        private Dictionary<int, Expression<Func<Category, object>>> CatOrderDict = new Dictionary<int, Expression<Func<Category, object>>>()
        {
            [1] = x => x.Name,
            [2] = x => x.Name,
            [3] = x => x.Units.Count,
            [4] = x => x.Units.Count
        };

        private Dictionary<int, Expression<Func<UnitStatus, object>>> StatusOrderDict = new Dictionary<int, Expression<Func<UnitStatus, object>>>()
        {
            [1] = x => x.Name,
            [2] = x => x.Name,
            [3] = x => x.Units.Count,
            [4] = x => x.Units.Count
        };

        private Dictionary<int, Expression<Func<WinName, object>>> WinNameOrderDict = new Dictionary<int, Expression<Func<WinName, object>>>()
        {
            [1] = x => x.Name,
            [2] = x => x.Name,
            [3] = x => x.Units.Count,
            [4] = x => x.Units.Count
        };

        private Dictionary<int, Expression<Func<InstalledSoft, object>>> SoftOrderDict = new Dictionary<int, Expression<Func<InstalledSoft, object>>>()
        {
            [1] = x => x.Name,
            [2] = x => x.Name,
            [3] = x => x.UnitInstalledSofts.Count(),
            [4] = x => x.UnitInstalledSofts.Count()
        };

        private Dictionary<int, Expression<Func<Manufacturer, object>>> ManOrderDict = new Dictionary<int, Expression<Func<Manufacturer, object>>>()
        {
            [1] = x => x.Name,
            [2] = x => x.Name,
            [3] = x => x.Units.Count,
            [4] = x => x.Units.Count,
            [5] = x => x.Categories.OrderBy(a => a.Category.Name).Select(a => a.Category.Name).FirstOrDefault(),
            [6] = x => x.Categories.OrderBy(a => a.Category.Name).Select(a => a.Category.Name).FirstOrDefault()
        };
        #endregion

        #region AddUnit
        public Task<List<Category>> CategoriesList()
        {
            List<Category> categories = new List<Category>();
            categories = _context.Categories.OrderBy(x => x.Name).ToList();
            return Task.FromResult(categories);
        }

        public List<Manufacturer> Manufacturers(int categoryId)
        {
            var used = _context.ManufacturerCategories.Where(x => x.CategoryId == categoryId);
            return used.Select(x => x.Manufacturer).OrderBy(x => x.Name).ToList();
        }

        public bool CheckMan(int man, int cat)
        {
            return _context.ManufacturerCategories.Where(x => x.ManufacturerId == man && x.CategoryId == cat).Count() > 0;
        }

        public Task<List<UnitStatus>> GetStatusesForAdd()
        {
            List<UnitStatus> statuses = new List<UnitStatus>();
            statuses = _context.UnitStatuses.OrderBy(x => x.Name).ToList();
            return Task.FromResult(statuses);
        }

        public Task<List<Employer>> GetEmployersAdd()
        {
            List<Employer> employers = new List<Employer>();
            employers = _context.Employers.OrderBy(x => x.Name).ToList();
            return Task.FromResult(employers);
        }

        public Task<List<Departament>> GetDepartamentsAdd()
        {
            List<Departament> departaments = new List<Departament>();
            departaments = _context.Departaments.OrderBy(x => x.Name).ToList();
            return Task.FromResult(departaments);
        }

        public Task<List<WinName>> GetWinNamesAdd()
        {
            List<WinName> winnames = new List<WinName>();
            winnames = _context.WinNames.OrderBy(x => x.Name).ToList();
            return Task.FromResult(winnames);
        }
        public Task<Dictionary<string, bool>> GetSoftDict()
        {
            Dictionary<string, bool> softs = new Dictionary<string, bool>();
            var temp = _context.InstalledSofts.OrderBy(x => x.Name).ToList();
            foreach (var s in temp)
            {
                softs.Add(s.Name, false);
            }
            return Task.FromResult(softs);
        }

        public Task<int> CreateUnit(UnitForm unit, string UserName)
        {
            var category = _context.Categories.Where(x => x.Id == unit.CategoryId).FirstOrDefault();
            if (category != null)
            {
                try
                {
                    Unit newUnit = new Unit { UnitStatusId = unit.UnitStatusId.Value, CategoryId = unit.CategoryId.Value };
                    if (category.Manufacturer && unit.ManufacturerId != null && _context.Manufacturers.Where(x => x.Id == unit.ManufacturerId).Count() > 0)
                        newUnit.ManufacturerId = unit.ManufacturerId;
                    if (category.Model)
                        newUnit.Model = unit.Model;
                    if (category.Location)
                        newUnit.Location = unit.Location;
                    if (category.InventId)
                        newUnit.InventId = unit.InventId;
                    if (category.Serial)
                        newUnit.Serial = unit.Serial;
                    if (category.BuyDate && unit.BuyDate != null)
                        newUnit.BuyDate = unit.BuyDate;
                    if (category.InstallDate && unit.InstallDate != null)
                        newUnit.InstallDate = unit.InstallDate;
                    if (category.Employer && unit.EmployerId != null)
                    {
                        var emp = _context.Employers.Where(x => x.Id == unit.EmployerId).FirstOrDefault();
                        if (emp != null)
                        {
                            newUnit.EmployerId = emp.Id;
                            if (category.Departament)
                                newUnit.DepartamentId = emp.DepartamentId;
                        }
                    }
                    if (category.Departament && newUnit.DepartamentId == null)
                    {
                        if (unit.DepartamentId != null && _context.Departaments.Where(x => x.Id == unit.DepartamentId).Count() > 0)
                            newUnit.DepartamentId = unit.DepartamentId;
                    }
                    if (category.WinName && unit.WinNameId != null && _context.WinNames.Where(x => x.Id == unit.WinNameId).Count() > 0)
                        newUnit.WinNameId = unit.WinNameId;
                    if (category.Processor)
                        newUnit.Processor = unit.Processor;
                    if (category.Motherboard)
                        newUnit.Motherboard = unit.Motherboard;
                    if (category.DDR)
                        newUnit.DDR = unit.DDR;
                    if (category.Specs)
                        newUnit.Specs = unit.Specs;
                    if (category.CartridgeModel)
                        newUnit.CartridgeModel = unit.CartridgeModel;
                    if (category.CartridgeCount && unit.CartridgeCount != null)
                        newUnit.CartridgeCount = unit.CartridgeCount;
                    if (category.NetName)
                        newUnit.NetName = unit.NetName;
                    if (category.BiosPass)
                        newUnit.BiosPass = unit.BiosPass;
                    if (category.Comments)
                        newUnit.Comment = unit.Comment;
                    _context.Units.Add(newUnit);
                    _context.SaveChanges();
                    if (category.ServiceWorks && unit.ServiceWorks.Count > 0)
                    {
                        foreach (var work in unit.ServiceWorks)
                            _context.ServiceWorks.Add(new ServiceWork { WorkName = work.WorkName, WorkDescr = work.WorkDescr, WorkDate = work.WorkDate, UnitId = newUnit.Id });
                    }
                    if (category.IPAdresses && unit.IPaddresses.Count > 0)
                    {
                        foreach (var ip in unit.IPaddresses)
                        {
                            try
                            {
                                IPAddress address = IPAddress.Parse(ip);
                                _context.IPadresses.Add(new IPs { IPAddress = address, UnitId = newUnit.Id });
                            }
                            catch { }
                        }
                    }
                    if (category.WinAccounts && unit.WinAccounts.Count > 0)
                    {
                        foreach (var wa in unit.WinAccounts)
                            _context.WinAccounts.Add(new WinAccount { Login = wa.Key, Password = wa.Value, UnitId = newUnit.Id });
                    }
                    if (category.RdpConnects && unit.RDPAccounts.Count > 0)
                    {
                        foreach (var rdp in unit.RDPAccounts)
                            _context.RdpConnects.Add(new RdpConnect { Comment = rdp.Comment, IPAddress = rdp.IPAddress, Login = rdp.Login, Password = rdp.Password, UnitId = newUnit.Id });
                    }
                    if (category.InstaledSofts && unit.Soft.Count > 0)
                    {
                        foreach (var soft in unit.Soft.Where(x => x.Value == true))
                        {
                            var InSoft = _context.InstalledSofts.Where(x => x.Name == soft.Key).FirstOrDefault();
                            if (InSoft != null)
                            {
                                _context.UnitInstalledSofts.Add(new UnitInstalledSofts { InstalledSoftId = InSoft.Id, UnitId = newUnit.Id });
                            }
                        }
                    }
                    string Changes = $"Добавлена новая техника {quote}{category.Name}{quote}";
                    if (!string.IsNullOrEmpty(newUnit.Model)) Changes += " " + newUnit.Model;
                    Changes += ".";
                    _context.UnitHistories.Add(new UnitHistory { Change = Changes, Secure = false, UnitId = newUnit.Id, UserName = UserName });
                    _context.SaveChanges();
                    return Task.FromResult(newUnit.Id);
                }
                catch
                {
                    return Task.FromResult(0);
                }
            }
            else
            {
                return Task.FromResult(0);
            }
            
        }
        #endregion

        #region Search
        public UnitsPaging userSearch(int sort, int page, int pageSize, int cat, int dep, int emp, int man, int stat, string text)
        {
            text = text.ToLower();
            UnitsPaging unitsPaging = new UnitsPaging();
            unitsPaging.Units = new List<Unit>();
            unitsPaging.TotalCount = _context.Units
                .Include(x => x.Manufacturer)
                .Include(x => x.Employer)
                .Include(x => x.Departament)
                .Include(x => x.Category)
                .Include(x => x.UnitStatus)
                .Where(x => cat == 0 ? true : x.CategoryId == cat)
                .Where(x => dep == 0 ? true : x.DepartamentId == dep)
                .Where(x => emp == 0 ? true : x.EmployerId == emp)
                .Where(x => man == 0 ? true : x.ManufacturerId == man)
                .Where(x => stat == 0 ? true : x.UnitStatusId == stat)
                .Where(x => x.CartridgeModel.ToLower().Contains(text)
                || x.UnitStatus.Name.ToLower().Contains(text) || x.Category.Name.ToLower().Contains(text)
                || x.Manufacturer.Name.ToLower().Contains(text) || x.Model.ToLower().Contains(text)
                || x.Location.ToLower().Contains(text) || x.InventId.ToLower().Contains(text)
                || x.Serial.ToLower().Contains(text) || x.Employer.Name.ToLower().Contains(text)
                || x.Departament.Name.ToLower().Contains(text) || x.WinName.Name.ToLower().Contains(text)
                || x.Processor.ToLower().Contains(text) || x.Motherboard.ToLower().Contains(text)
                || x.DDR.ToLower().Contains(text) || x.Specs.ToLower().Contains(text)).Count();
            unitsPaging.TotalPages = (int)Math.Ceiling(unitsPaging.TotalCount / (double)pageSize);
            var skip = page == 1 ? 0 : (page - 1) * pageSize;
            if (IsEven(sort))
            {
                unitsPaging.Units = _context.Units
                .Include(x => x.Manufacturer)
                .Include(x => x.Employer)
                .Include(x => x.Departament)
                .Include(x => x.Category)
                .Include(x => x.UnitStatus)
                .Where(x => cat == 0 ? true : x.CategoryId == cat)
                .Where(x => dep == 0 ? true : x.DepartamentId == dep)
                .Where(x => emp == 0 ? true : x.EmployerId == emp)
                .Where(x => man == 0 ? true : x.ManufacturerId == man)
                .Where(x => stat == 0 ? true : x.UnitStatusId == stat)
                .Where(x => x.CartridgeModel.ToLower().Contains(text)
                || x.UnitStatus.Name.ToLower().Contains(text) || x.Category.Name.ToLower().Contains(text)
                || x.Manufacturer.Name.ToLower().Contains(text) || x.Model.ToLower().Contains(text)
                || x.Location.ToLower().Contains(text) || x.InventId.ToLower().Contains(text)
                || x.Serial.ToLower().Contains(text) || x.Employer.Name.ToLower().Contains(text)
                || x.Departament.Name.ToLower().Contains(text) || x.WinName.Name.ToLower().Contains(text)
                || x.Processor.ToLower().Contains(text) || x.Motherboard.ToLower().Contains(text)
                || x.DDR.ToLower().Contains(text) || x.Specs.ToLower().Contains(text))
                .OrderByDescending(UnitOrderDict[sort])
                .Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                unitsPaging.Units = _context.Units
                .Include(x => x.Manufacturer)
                .Include(x => x.Employer)
                .Include(x => x.Departament)
                .Include(x => x.Category)
                .Include(x => x.UnitStatus)
                .Where(x => cat == 0 ? true : x.CategoryId == cat)
                .Where(x => dep == 0 ? true : x.DepartamentId == dep)
                .Where(x => emp == 0 ? true : x.EmployerId == emp)
                .Where(x => man == 0 ? true : x.ManufacturerId == man)
                .Where(x => stat == 0 ? true : x.UnitStatusId == stat)
                .Where(x => x.CartridgeModel.ToLower().Contains(text)
                || x.UnitStatus.Name.ToLower().Contains(text) || x.Category.Name.ToLower().Contains(text)
                || x.Manufacturer.Name.ToLower().Contains(text) || x.Model.ToLower().Contains(text)
                || x.Location.ToLower().Contains(text) || x.InventId.ToLower().Contains(text)
                || x.Serial.ToLower().Contains(text) || x.Employer.Name.ToLower().Contains(text)
                || x.Departament.Name.ToLower().Contains(text) || x.WinName.Name.ToLower().Contains(text)
                || x.Processor.ToLower().Contains(text) || x.Motherboard.ToLower().Contains(text)
                || x.DDR.ToLower().Contains(text) || x.Specs.ToLower().Contains(text))
                .OrderBy(UnitOrderDict[sort])
                .Skip(skip).Take(pageSize).ToList();
            }            
            return unitsPaging;
        }
        public UnitsPaging adminSearch(int sort, int page, int pageSize, int cat, int dep, int emp, int man, int stat, string text)
        {
            text = text.ToLower();
            UnitsPaging unitsPaging = new UnitsPaging();
            unitsPaging.Units = new List<Unit>();
            unitsPaging.TotalCount = _context.Units
                .Include(x => x.Manufacturer)
                .Include(x => x.Employer)
                .Include(x => x.Departament)
                .Include(x => x.Category)
                .Include(x => x.UnitStatus)
                .Where(x => cat == 0 ? true : x.CategoryId == cat)
                .Where(x => dep == 0 ? true : x.DepartamentId == dep)
                .Where(x => emp == 0 ? true : x.EmployerId == emp)
                .Where(x => man == 0 ? true : x.ManufacturerId == man)
                .Where(x => stat == 0 ? true : x.UnitStatusId == stat)
                .Where(x => x.CartridgeModel.ToLower().Contains(text)
                || x.UnitStatus.Name.ToLower().Contains(text) || x.Category.Name.ToLower().Contains(text)
                || x.Manufacturer.Name.ToLower().Contains(text) || x.Model.ToLower().Contains(text)
                || x.Location.ToLower().Contains(text) || x.InventId.ToLower().Contains(text)
                || x.Serial.ToLower().Contains(text) || x.Employer.Name.ToLower().Contains(text)
                || x.Departament.Name.ToLower().Contains(text) || x.WinName.Name.ToLower().Contains(text)
                || x.Processor.ToLower().Contains(text) || x.Motherboard.ToLower().Contains(text)
                || x.DDR.ToLower().Contains(text) || x.Specs.ToLower().Contains(text)
                || x.NetName.ToLower().Contains(text) || x.BiosPass.ToLower().Contains(text)
                || x.Comment.ToLower().Contains(text)).Count();
            unitsPaging.TotalPages = (int)Math.Ceiling(unitsPaging.TotalCount / (double)pageSize);
            var skip = page == 1 ? 0 : (page - 1) * pageSize;
            if (IsEven(sort))
            {
                unitsPaging.Units = _context.Units
                .Include(x => x.Manufacturer)
                .Include(x => x.Employer)
                .Include(x => x.Departament)
                .Include(x => x.Category)
                .Include(a => a.IPAdresses)
                .Include(x => x.UnitStatus)
                .Where(x => cat == 0 ? true : x.CategoryId == cat)
                .Where(x => dep == 0 ? true : x.DepartamentId == dep)
                .Where(x => emp == 0 ? true : x.EmployerId == emp)
                .Where(x => man == 0 ? true : x.ManufacturerId == man)
                .Where(x => stat == 0 ? true : x.UnitStatusId == stat)
                .Where(x => x.CartridgeModel.ToLower().Contains(text)
                || x.UnitStatus.Name.ToLower().Contains(text) || x.Category.Name.ToLower().Contains(text)
                || x.Manufacturer.Name.ToLower().Contains(text) || x.Model.ToLower().Contains(text)
                || x.Location.ToLower().Contains(text) || x.InventId.ToLower().Contains(text)
                || x.Serial.ToLower().Contains(text) || x.Employer.Name.ToLower().Contains(text)
                || x.Departament.Name.ToLower().Contains(text) || x.WinName.Name.ToLower().Contains(text)
                || x.Processor.ToLower().Contains(text) || x.Motherboard.ToLower().Contains(text)
                || x.DDR.ToLower().Contains(text) || x.Specs.ToLower().Contains(text)
                || x.NetName.ToLower().Contains(text) || x.BiosPass.ToLower().Contains(text)
                || x.Comment.ToLower().Contains(text))
                .OrderByDescending(UnitOrderDict[sort])
                .Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                unitsPaging.Units = _context.Units
                .Include(x => x.Manufacturer)
                .Include(x => x.Employer)
                .Include(x => x.Departament)
                .Include(x => x.Category)
                .Include(a => a.IPAdresses)
                .Include(x => x.UnitStatus)
                .Where(x => cat == 0 ? true : x.CategoryId == cat)
                .Where(x => dep == 0 ? true : x.DepartamentId == dep)
                .Where(x => emp == 0 ? true : x.EmployerId == emp)
                .Where(x => man == 0 ? true : x.ManufacturerId == man)
                .Where(x => stat == 0 ? true : x.UnitStatusId == stat)
                .Where(x => x.CartridgeModel.ToLower().Contains(text)
                || x.UnitStatus.Name.ToLower().Contains(text) || x.Category.Name.ToLower().Contains(text)
                || x.Manufacturer.Name.ToLower().Contains(text) || x.Model.ToLower().Contains(text)
                || x.Location.ToLower().Contains(text) || x.InventId.ToLower().Contains(text)
                || x.Serial.ToLower().Contains(text) || x.Employer.Name.ToLower().Contains(text)
                || x.Departament.Name.ToLower().Contains(text) || x.WinName.Name.ToLower().Contains(text)
                || x.Processor.ToLower().Contains(text) || x.Motherboard.ToLower().Contains(text)
                || x.DDR.ToLower().Contains(text) || x.Specs.ToLower().Contains(text)
                || x.NetName.ToLower().Contains(text) || x.BiosPass.ToLower().Contains(text)
                || x.Comment.ToLower().Contains(text))
                .OrderBy(UnitOrderDict[sort])
                .Skip(skip).Take(pageSize).ToList();
            }
            
            return unitsPaging;
        }

        public Task<int> FindIP(string ip)
        {
            IPAddress address = IPAddress.Parse(ip);
            var founded = _context.IPadresses.Where(x => x.IPAddressBytes == address.GetAddressBytes()).FirstOrDefault();
            if (founded != null)
                return Task.FromResult(founded.UnitId);
            else
                return Task.FromResult(0);
        }
        #endregion

        #region Units
        public void AddFile(byte[] data, string name, int unitid, string username)
        {
            _context.StoredFiles.Add(new StoredFile
            {
                Data = data,
                FileName = name,
                UnitId = unitid
            });
            _context.UnitHistories.Add(new UnitHistory { Secure = false, UnitId = unitid, UserName = username, Change = $"Добавлен новый файл {name}" });
            _context.SaveChanges();
        }
        public UnitsPaging unitsPaging(int sort, int page, int pageSize, int cat, int dep, int emp, int man, int stat)
        {
            UnitsPaging unitsPaging = new UnitsPaging();
            unitsPaging.Units = new List<Unit>();
            unitsPaging.TotalCount = _context.Units
                .Where(x => cat == 0 ? true : x.CategoryId == cat)
                .Where(x => dep == 0 ? true : x.DepartamentId == dep)
                .Where(x => emp == 0 ? true : x.EmployerId == emp)
                .Where(x => man == 0 ? true : x.ManufacturerId == man)
                .Where(x => stat == 0 ? true : x.UnitStatusId == stat)
                .Count();
            unitsPaging.TotalPages = (int)Math.Ceiling(unitsPaging.TotalCount / (double)pageSize);
            var skip = page == 1 ? 0 : (page - 1) * pageSize;
			if (IsEven(sort))
			{
				unitsPaging.Units = _context.Units
                .Include(x => x.Manufacturer)
                .Include(x => x.Employer)
                .Include(x => x.Departament)
                .Include(x => x.Category)
                .Include(a => a.IPAdresses)
                .Include(x => x.UnitStatus)
                .Where(x => cat == 0 ? true : x.CategoryId == cat)
                .Where(x => dep == 0 ? true : x.DepartamentId == dep)
                .Where(x => emp == 0 ? true : x.EmployerId == emp)
                .Where(x => man == 0 ? true : x.ManufacturerId == man)
                .Where(x => stat == 0 ? true : x.UnitStatusId == stat)
                .OrderByDescending(UnitOrderDict[sort])
                .Skip(skip).Take(pageSize).ToList();
			}
            else
            {
                unitsPaging.Units = _context.Units
                .Include(x => x.Manufacturer)
                .Include(x => x.Employer)
                .Include(x => x.Departament)
                .Include(x => x.Category)
                .Include(a => a.IPAdresses)
                .Include(x => x.UnitStatus)
                .Where(x => cat == 0 ? true : x.CategoryId == cat)
                .Where(x => dep == 0 ? true : x.DepartamentId == dep)
                .Where(x => emp == 0 ? true : x.EmployerId == emp)
                .Where(x => man == 0 ? true : x.ManufacturerId == man)
                .Where(x => stat == 0 ? true : x.UnitStatusId == stat)
                .OrderBy(UnitOrderDict[sort])
                .Skip(skip).Take(pageSize).ToList();
            }
            return unitsPaging;
        }

        private bool IsEven(int a)
        {
            return (a % 2) == 0;
        }

        public Task<Unit> GetUnit(int id)
        {
            Unit unit = _context.Units.Where(x => x.Id == id)
                .Include(a => a.Category)
                    .ThenInclude(r => r.Manufacturers)
                        .ThenInclude(t => t.Manufacturer)
                .Include(s => s.Manufacturer)
                .Include(d => d.UnitStatus)
                .Include(f => f.Employer)
                .Include(g => g.WinName)
                .Include(h => h.Departament)
                .Include(j => j.UnitInstalledSofts)
                    .ThenInclude(x => x.InstalledSoft)
                .Include(a => a.IPAdresses)
                .Include(s => s.WinAccounts)
                .Include(x => x.RdpConnects)
                .Include(x => x.ServiceWorks)
                .Include(x => x.StoredFiles)
                .Include(s => s.UnitHistories).FirstOrDefault();
            return Task.FromResult(unit);
        }

        public Task<bool> DeleteFromUnitView(int what, int id, string UserName)
        {
            if (what == 1)
            {
                var sw = _context.ServiceWorks.Where(x => x.Id == id).FirstOrDefault();
                if (sw != null)
                {
                    _context.UnitHistories.Add(new UnitHistory
                    {
                        Change = $"Ремонтная работа {quote}{sw.WorkName}. {sw.WorkDescr}.{quote} удалена",
                        UnitId = sw.UnitId,
                        UserName = UserName,
                        Secure = false
                    });
                    _context.ServiceWorks.Remove(sw);
                    _context.SaveChanges();
                    return Task.FromResult(true);
                }                
            }
            else if (what == 2)
            {
                var ip = _context.IPadresses.Where(x => x.Id == id).FirstOrDefault();
                if (ip != null)
                {
                    _context.UnitHistories.Add(new UnitHistory
                    {
                        Change = $"IP адрес {quote}{ip.IPAddress}{quote} удален",
                        UnitId = ip.UnitId,
                        UserName = UserName,
                        Secure = true
                    });
                    _context.IPadresses.Remove(ip);
                    _context.SaveChanges();
                    return Task.FromResult(true);
                }
            }
            else if (what == 3)
            {
                var acc = _context.WinAccounts.Where(x => x.Id == id).FirstOrDefault();
                if (acc != null)
                {
                    _context.UnitHistories.Add(new UnitHistory
                    {
                        Change = $"Аккаунт Windows (Логин: {acc.Login}. Пароль: {acc.Password}.) удален",
                        UnitId = acc.UnitId,
                        UserName = UserName,
                        Secure = true
                    });
                    _context.WinAccounts.Remove(acc);
                    _context.SaveChanges();
                    return Task.FromResult(true);
                }
            }
            else if (what == 4)
            {
                var acc = _context.RdpConnects.Where(x => x.Id == id).FirstOrDefault();
                if (acc != null)
                {
                    _context.UnitHistories.Add(new UnitHistory
                    {
                        Change = $"RDP Аккаунт (IP адрес: {acc.IPAddress}. Логин: {acc.Login}. Пароль: {acc.Password}. Комментарий: {acc.Comment}.) удален",
                        UnitId = acc.UnitId,
                        UserName = UserName,
                        Secure = true
                    });
                    _context.RdpConnects.Remove(acc);
                    _context.SaveChanges();
                    return Task.FromResult(true);
                }
            }
            else if (what == 5)
            {
                var file = _context.StoredFiles.Where(x => x.Id == id).FirstOrDefault();
                if (file != null)
                {
                    _context.UnitHistories.Add(new UnitHistory
                    {
                        Change = $"Файл {file.FileName} удален",
                        UnitId = file.UnitId,
                        UserName = UserName,
                        Secure = true
                    });
                    _context.StoredFiles.Remove(file);
                    _context.SaveChanges();
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task<bool> EditUnitSelectList(int what, int selected, int unitId, string UserName)
        {
            Unit unit = _context.Units.Where(x => x.Id == unitId)
                .Include(c => c.Manufacturer)
                .Include(v => v.Employer)
                .Include(x => x.Departament)
                .Include(z => z.UnitStatus)
                .Include(x => x.WinName).FirstOrDefault();
            if (unit != null)
            {
                UnitHistory unitHistory = new UnitHistory
                {
                    UserName = UserName,
                    UnitId = unitId,
                    Secure = false
                };
                if (what == 1)
                {
                    Manufacturer NewMan = null;
                    if (selected != 0) NewMan = _context.Manufacturers.Where(x => x.Id == selected).FirstOrDefault();
                    unitHistory.Change = $"Производитель {quote}{(unit.ManufacturerId == null ? "Не указан" : unit.Manufacturer.Name)}{quote} изменен на {quote}{(NewMan == null ? "Не указан" : NewMan.Name)}{quote}.";
                    unit.ManufacturerId = NewMan?.Id;
                }
                else if (what == 2)
                {
                    Employer NewEmp = null;
                    if (selected != 0) NewEmp = _context.Employers.Where(x => x.Id == selected).FirstOrDefault();
                    unitHistory.Change = $"Сотрудник {quote}{(unit.EmployerId == null ? "Не указан" : unit.Employer.Name)}{quote} изменен на {quote}{(NewEmp == null ? "Не указан" : NewEmp.Name)}{quote}.";
                    unit.EmployerId = NewEmp?.Id;
                    if (NewEmp != null)
                    {
                        var dep = _context.Departaments.Where(x => x.Id == NewEmp.DepartamentId).FirstOrDefault();
                        if (dep != null && dep.Id != unit.DepartamentId)
                        {
                            unitHistory.Change += $" Отдел {quote}{(unit.DepartamentId == null ? "Не указан" : unit.Departament.Name)}{quote} изменен на {quote}{dep.Name}{quote}.";
                            unit.DepartamentId = dep.Id;
                        }
                    }
                }
                else if (what == 3)
                {
                    UnitStatus NewStatus = _context.UnitStatuses.Where(x => x.Id == selected).FirstOrDefault();
                    if (NewStatus != null)
                    {
                        unitHistory.Change = $"Статус {quote}{unit.UnitStatus.Name}{quote} изменен на {quote}{NewStatus.Name}{quote}.";
                        unit.UnitStatusId = NewStatus.Id;
                    }
                }
                else if (what == 4)
                {
                    Departament NewDep = null;
                    if (selected != 0) NewDep = _context.Departaments.Where(x => x.Id == selected).FirstOrDefault();
                    unitHistory.Change = $"Отдел {quote}{(unit.DepartamentId == null ? "Не указан" : unit.Departament.Name)}{quote} изменен на {quote}{(NewDep == null ? "Не указан" : NewDep.Name)}{quote}.";
                    unit.DepartamentId = NewDep?.Id;
                }
                else if (what == 5)
                {
                    WinName NewWin = null;
                    if (selected != 0) NewWin = _context.WinNames.Where(x => x.Id == selected).FirstOrDefault();
                    unitHistory.Change = $"Операционная система {quote}{(unit.WinNameId == null ? "Не указана" : unit.WinName.Name)}{quote} изменена на {quote}{(NewWin == null ? "Не указана" : NewWin.Name)}{quote}.";
                    unit.WinNameId = NewWin?.Id;
                }
                _context.UnitHistories.Add(unitHistory);
                _context.Entry(unit).State = EntityState.Modified;
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> EditUnitIntInput(int count, int unitId, string UserName)
        {
            Unit unit = _context.Units.Where(x => x.Id == unitId).FirstOrDefault();
            if (unit != null)
            {
                _context.UnitHistories.Add(new UnitHistory
                {
                    Change = $"Количество картриджей {quote}{(unit.CartridgeCount == null ? "Не указано" : unit.CartridgeCount.Value.ToString())}{quote} изменено на {quote}{(count == 0 ? "Не указано" : count.ToString())}{quote}.",
                    Secure = false,
                    UnitId = unit.Id,
                    UserName = UserName
                });
                if (count == 0) 
                    unit.CartridgeCount = null;
                else
                    unit.CartridgeCount = count;
                _context.Entry(unit).State = EntityState.Modified;
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> EditUnitDateInput(int what, DateTime? newDate, int unitId, string UserName)
        {
            Unit unit = _context.Units.Where(x => x.Id == unitId).FirstOrDefault();
            if (unit != null)
            {
                UnitHistory unitHistory = new UnitHistory
                {
                    UserName = UserName,
                    UnitId = unitId,
                    Secure = false
                };
                if (what == 1)
                {
                    unitHistory.Change = $"Дата приобретения {quote}{(unit.BuyDate == null ? "Не указана" : unit.BuyDate.Value.ToString("d"))}{quote} изменена на {quote}{(newDate == null ? "Не указана" : newDate.Value.ToString("d"))}{quote}.";
                    unit.BuyDate = newDate;
                }
                else if (what == 2)
                {
                    unitHistory.Change = $"Дата установки {quote}{(unit.InstallDate == null ? "Не указана" : unit.InstallDate.Value.ToString("d"))}{quote} изменена на {quote}{(newDate == null ? "Не указана" : newDate.Value.ToString("d"))}{quote}.";
                    unit.InstallDate = newDate;
                }
                _context.UnitHistories.Add(unitHistory);
                _context.Entry(unit).State = EntityState.Modified;
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool>EditUnitTextarea(int what, string newName, int unitId, string UserName)
        {
            Unit unit = _context.Units.Where(x => x.Id == unitId).FirstOrDefault();
            if (unit != null)
            {
                UnitHistory unitHistory = new UnitHistory
                {
                    UserName = UserName,
                    UnitId = unitId,
                    Secure = false
                };
                if (what == 1)
                {
                    unitHistory.Change = $"Характеристики {quote}{unit.Specs}{quote} изменены на {quote}{newName}{quote}.";
                    unit.Specs = newName;
                }
                else if (what == 2)
                {
                    unitHistory.Change = $"Комментарий {quote}{unit.Comment}{quote} изменен на {quote}{newName}{quote}.";
                    unitHistory.Secure = true;
                    unit.Comment = newName;
                }
                _context.UnitHistories.Add(unitHistory);
                _context.Entry(unit).State = EntityState.Modified;
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> EditUnitOneInput(int what, string newName, int unitId, string UserName)
        {
            Unit unit = _context.Units.Where(x => x.Id == unitId).FirstOrDefault();
            if (unit != null)
            {
                UnitHistory unitHistory = new UnitHistory
                {
                    UserName = UserName,
                    UnitId = unitId,
                    Secure = false
                };
                if (what == 1)
                {
                    unitHistory.Change = $"Модель {quote}{unit.Model}{quote} изменена на {quote}{newName}{quote}.";
                    unit.Model = newName;
                }
                else if (what == 2)
                {
                    unitHistory.Change = $"Местоположение {quote}{unit.Location}{quote} изменено на {quote}{newName}{quote}.";
                    unit.Location = newName;
                }
                else if (what == 3)
                {
                    unitHistory.Change = $"Инвентарный номер {quote}{unit.InventId}{quote} изменен на {quote}{newName}{quote}.";
                    unit.InventId = newName;
                }
                else if (what == 4)
                {
                    unitHistory.Change = $"Серийный номер {quote}{unit.Serial}{quote} изменен на {quote}{newName}{quote}.";
                    unit.Serial = newName;
                }
                else if (what == 5)
                {
                    unitHistory.Change = $"Процессор {quote}{unit.Processor}{quote} изменен на {quote}{newName}{quote}.";
                    unit.Processor = newName;
                }
                else if (what == 6)
                {
                    unitHistory.Change = $"Материнская плата {quote}{unit.Motherboard}{quote} изменена на {quote}{newName}{quote}.";
                    unit.Motherboard = newName;
                }
                else if (what == 7)
                {
                    unitHistory.Change = $"Оперативная память {quote}{unit.DDR}{quote} изменена на {quote}{newName}{quote}.";
                    unit.DDR = newName;
                }
                else if (what == 8)
                {
                    unitHistory.Change = $"Модель картриджа {quote}{unit.CartridgeModel}{quote} изменена на {quote}{newName}{quote}.";
                    unit.CartridgeModel = newName;
                }
                else if (what == 9)
                {
                    unitHistory.Change = $"Сетевое имя {quote}{unit.NetName}{quote} изменено на {quote}{newName}{quote}.";
                    unitHistory.Secure = true;
                    unit.NetName = newName;
                }
                else if (what == 10)
                {
                    unitHistory.Change = $"Пароль BIOS {quote}{unit.BiosPass}{quote} изменен на {quote}{newName}{quote}.";
                    unitHistory.Secure = true;
                    unit.BiosPass = newName;
                }
                _context.UnitHistories.Add(unitHistory);
                _context.Entry(unit).State = EntityState.Modified;
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> CheckIP(string ip)
        {
            IPAddress address = IPAddress.Parse(ip);
            if (_context.IPadresses.Where(x => x.IPAddressBytes == address.GetAddressBytes()).Count() > 0)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        public Task<bool> AddServiceWork(ServiceWork work, string UserName)
        {
            _context.ServiceWorks.Add(work);
            _context.UnitHistories.Add(new UnitHistory { Change = $"Добавлен новый ремонт: {quote}{work.WorkName}. {work.WorkDescr}.{quote}", Secure = false, UnitId = work.UnitId, UserName = UserName });
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> AddWinAcc(WinAccount acc, string UserName)
        {
            _context.WinAccounts.Add(acc);
            _context.UnitHistories.Add(new UnitHistory { Change = $"Добавлен новый аккаунт Windows: Логин: {acc.Login}. Пароль: {acc.Password}.", Secure = true, UnitId = acc.UnitId, UserName = UserName });
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> AddRDPAcc(RdpConnect rdp, string UserName)
        {
            _context.RdpConnects.Add(rdp);
            _context.UnitHistories.Add(new UnitHistory { Change = $"Добавлен новый RDP аккаунт: IP адрес: {rdp.IPAddress}. Логин: {rdp.Login}. Пароль: {rdp.Password}. Комментарий: {rdp.Comment}.", Secure = true, UnitId = rdp.UnitId, UserName = UserName });
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> EditSofts(Dictionary<string, bool> softs, int id, string UserName)
        {
            Unit edit = _context.Units.Where(x => x.Id == id)
                .Include(x => x.UnitInstalledSofts)
                    .ThenInclude(x => x.InstalledSoft).FirstOrDefault();            
            if (edit != null)
            {
                int changes = 0;
                List<string> Add = new List<string>();
                List<string> Remove = new List<string>();
                foreach (var soft in softs)
                {
                    var check = _context.InstalledSofts.Where(x => x.Name == soft.Key).FirstOrDefault();
                    if (check != null)
                    {
                        var inBase = edit.UnitInstalledSofts.Where(x => x.InstalledSoftId == check.Id).FirstOrDefault();
                        if (soft.Value != (inBase != null))
                        {
                            if (soft.Value)
                            {
                                //add
                                changes++;
                                Add.Add(check.Name);
                                _context.UnitInstalledSofts.Add(new UnitInstalledSofts { InstalledSoftId = check.Id, UnitId = edit.Id });
                            }
                            else
                            {
                                //remove
                                var forDel = _context.UnitInstalledSofts.Where(x => x.InstalledSoftId == check.Id && x.UnitId == edit.Id).FirstOrDefault();
                                if (forDel != null)
                                {
                                    changes++;
                                    Remove.Add(check.Name);
                                    _context.UnitInstalledSofts.Remove(forDel);
                                }                                
                            }
                        }
                    }
                }
                if (changes > 0)
                {
                    string Changes = "Установленное ПО отредактировано.";
                    if (Add.Count > 0) Changes += " Добавлено: " + string.Join(", ", Add.ToArray()) + ".";
                    if (Remove.Count > 0) Changes += " Удалено: " + string.Join(", ", Remove.ToArray()) + ".";
                    _context.UnitHistories.Add(new UnitHistory { Change = Changes, Secure = false, UnitId = edit.Id, UserName = UserName });
                    _context.SaveChanges();
                }
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> AddUnitIP(string ip, int id, string UserName)
        {
            var unit = _context.Units.Where(x => x.Id == id).FirstOrDefault();
            if (unit != null)
            {
                try
                {
                    IPAddress address = IPAddress.Parse(ip);
                    _context.IPadresses.Add(new IPs { IPAddress = address, UnitId = id });
                    _context.UnitHistories.Add(new UnitHistory { Change = $"Добавлен новый IP адрес: {address}.", Secure = true, UnitId = id, UserName = UserName });
                    _context.SaveChanges();
                    return Task.FromResult(true);
                }
                catch { return Task.FromResult(false); }
            }
            else
            {
                return Task.FromResult(false);
            }
        }
        #endregion

        #region InstalledSoft
        public InstSoftPaging softPaging(int sort, int page, int pageSize)
        {
            InstSoftPaging softPaging = new InstSoftPaging();
            softPaging.InstalledSofts = new List<InstalledSoft>();
            softPaging.TotalCount = _context.InstalledSofts.Count();
            softPaging.TotalPages = (int)Math.Ceiling(softPaging.TotalCount / (double)pageSize);
            var skip = page == 1 ? 0 : (page - 1) * pageSize;
            if (IsEven(sort))
            {
                softPaging.InstalledSofts = _context.InstalledSofts
                    .Include(x => x.UnitInstalledSofts)
                    .OrderByDescending(SoftOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                softPaging.InstalledSofts = _context.InstalledSofts
                    .Include(x => x.UnitInstalledSofts)
                    .OrderBy(SoftOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            return softPaging;
        }

        public Task<bool> CreateInstalledSoft(InstalledSoft cat, string UserName)
        {
            _context.InstalledSofts.Add(cat);
            _context.InstalledSoftHistories.Add(new InstalledSoftHistory { Changes = $"Добавлено новое программное обеспечение. Название: {quote}{cat.Name}{quote}.", UserName = UserName });
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> CheckSoftName(string name, int id)
        {
            if (_context.InstalledSofts.Where(x => x.Name == name).Count() > 0 && _context.InstalledSofts.Where(x => x.Name == name).FirstOrDefault().Id != id)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        public Task<bool> EditInstalledSoft(InstalledSoft cat, string UserName)
        {
            var Existing = _context.InstalledSofts.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                int changes = 0;
                string Changes = $"Программное обеспечение {quote}{Existing.Name}{quote} отредактировано. ";
                if (cat.Name != Existing.Name)
                {
                    Changes += $" Название изменено на {quote}{cat.Name}{quote}. ";
                    Existing.Name = cat.Name;
                    changes++;
                }
                if (changes > 0)
                {
                    _context.Entry(Existing).State = EntityState.Modified;
                    _context.InstalledSoftHistories.Add(new InstalledSoftHistory { Changes = Changes, UserName = UserName });
                    _context.SaveChanges();
                }
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteInstalledSoft(InstalledSoft cat, string UserName)
        {
            var Existing = _context.InstalledSofts.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                _context.InstalledSoftHistories.Add(new InstalledSoftHistory { Changes = $"Программное обеспечение {quote}{cat.Name}{quote} удалено.", UserName = UserName });
                _context.InstalledSofts.Remove(Existing);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
        #endregion

        #region Manufacturers
        public ManPaging manPaging(int sort, int page, int pageSize)
        {
            ManPaging manPaging = new ManPaging();
            manPaging.Manufacturers = new List<Manufacturer>();
            manPaging.TotalCount = _context.Manufacturers.Count();
            manPaging.TotalPages = (int)Math.Ceiling(manPaging.TotalCount / (double)pageSize);
            var skip = page == 1 ? 0 : (page - 1) * pageSize;
            if (IsEven(sort))
            {
                manPaging.Manufacturers = _context.Manufacturers
                    .Include(x => x.Units)
                    .Include(x => x.Categories)
                    .OrderByDescending(ManOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                manPaging.Manufacturers = _context.Manufacturers
                    .Include(x => x.Units)
                    .Include(x => x.Categories)
                    .OrderBy(ManOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            return manPaging;
        }        

        public Task<bool> CheckManName(string name, int id)
        {
            if (_context.Manufacturers.Where(x => x.Name == name).Count() > 0 && _context.Manufacturers.Where(x => x.Name == name).FirstOrDefault().Id != id)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        public Task<List<Manufacturer>> GetManufacturersAdd()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            manufacturers = _context.Manufacturers.OrderBy(x => x.Name).ToList();
            return Task.FromResult(manufacturers);
        }

        public Task<bool> CreateManufacturer(ManForm cat, string UserName)
        {
            Manufacturer NewM = new Manufacturer
            {
                Name = cat.Name
            };
            _context.Manufacturers.Add(NewM);
            _context.SaveChanges();
            string Changes = $"Добавлен новый производитель: {quote}{NewM.Name}{quote}.";
            List<string> catlist = new List<string>();
            foreach (var c in cat.categories)
            {
                if (c.Value)
                {
                    var category = _context.Categories.Where(x => x.Name == c.Key).FirstOrDefault();
                    if (category != null)
                    {
                        _context.ManufacturerCategories.Add(new ManufacturerCategories { CategoryId = category.Id, ManufacturerId = NewM.Id });
                        catlist.Add(category.Name);
                    }
                }
            }
            if (catlist.Count > 0)
                Changes += " Типы техники: " + string.Join(", ", catlist.ToArray()) + ".";
            _context.ManufacturerHistories.Add(new ManufacturerHistory { Changes = Changes, UserName = UserName });
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> EditManufacturer(ManForm cat, string UserName)
        {
            var Existing = _context.Manufacturers.Where(x => x.Id == cat.Id).Include(x => x.Categories).FirstOrDefault();
            if (Existing != null)
            {
                int changes = 0;
                string Changes = $"Производитель {quote}{Existing.Name}{quote} отредактирован. ";
                if (cat.Name != Existing.Name)
                {
                    Changes += $" Название изменено на {quote}{cat.Name}{quote}. ";
                    Existing.Name = cat.Name;
                    changes++;
                }
                List<string> ExistCats = Existing.Categories.Select(x => x.Category.Name).ToList();
                List<string> Added = new List<string>();
                List<string> Removed = new List<string>();
                foreach (var c in cat.categories)
                {
                    if (ExistCats.Contains(c.Key) != c.Value)
                    {
                        var category = _context.Categories.Where(x => x.Name == c.Key).FirstOrDefault();
                        if (category != null)
                        {
                            if (c.Value)
                            {
                                Added.Add(c.Key);
                                _context.ManufacturerCategories.Add(new ManufacturerCategories { ManufacturerId = Existing.Id, CategoryId = category.Id });
                                changes++;
                            }
                            else
                            {                               
                                var ManCat = _context.ManufacturerCategories.Where(x => x.CategoryId == category.Id && x.ManufacturerId == Existing.Id).FirstOrDefault();
                                if (ManCat != null)
                                {
                                    Removed.Add(c.Key);
                                    _context.ManufacturerCategories.Remove(ManCat);
                                    changes++;
                                }
                            }
                        }                        
                    }
                }
                if (changes > 0)
                {
                    _context.Entry(Existing).State = EntityState.Modified;
                    if (Added.Count > 0) Changes += "Добавлены типы техники: " + string.Join(", ", Added.ToArray()) + ". ";
                    if (Removed.Count > 0) Changes += "Удалены типы типы техники: " + string.Join(", ", Removed.ToArray()) + ". ";
                    _context.ManufacturerHistories.Add(new ManufacturerHistory { Changes = Changes, UserName = UserName });
                    _context.SaveChanges();
                }
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteManufacturer(Manufacturer cat, string UserName)
        {
            var Existing = _context.Manufacturers.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                _context.ManufacturerHistories.Add(new ManufacturerHistory { Changes = $"Производитель {quote}{cat.Name}{quote} удален.", UserName = UserName });
                _context.Manufacturers.Remove(Existing);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
        #endregion

        #region WinNames
        public WinNamePaging winNamePaging(int sort, int page, int pageSize)
        {
            WinNamePaging winNamePaging = new WinNamePaging();
            winNamePaging.WinNames = new List<WinName>();
            winNamePaging.TotalCount = _context.WinNames.Count();
            winNamePaging.TotalPages = (int)Math.Ceiling(winNamePaging.TotalCount / (double)pageSize);
            var skip = page == 1 ? 0 : (page - 1) * pageSize;
            if (IsEven(sort))
            {
                winNamePaging.WinNames = _context.WinNames
                    .Include(x => x.Units)
                    .OrderByDescending(WinNameOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                winNamePaging.WinNames = _context.WinNames
                    .Include(x => x.Units)
                    .OrderBy(WinNameOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            return winNamePaging;
        }

        public Task<bool> CheckWinName(string name, int id)
        {
            if (_context.WinNames.Where(x => x.Name == name).Count() > 0 && _context.WinNames.Where(x => x.Name == name).FirstOrDefault().Id != id)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        public Task<bool> CreateWinName(WinName cat, string UserName)
        {
            _context.WinNames.Add(cat);
            _context.WinNameHistories.Add(new WinNameHistory { Changes = $"Добавлена новая операционная система. Название: {quote}{cat.Name}{quote}.", UserName = UserName });
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> EditWinName(WinName cat, string UserName)
        {
            var Existing = _context.WinNames.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                int changes = 0;
                string Changes = $"Операционная система {quote}{Existing.Name}{quote} отредактирована. ";
                if (cat.Name != Existing.Name)
                {
                    Changes += $" Название изменено на {quote}{cat.Name}{quote}. ";
                    Existing.Name = cat.Name;
                    changes++;
                }
                if (changes > 0)
                {
                    _context.Entry(Existing).State = EntityState.Modified;
                    _context.WinNameHistories.Add(new WinNameHistory { Changes = Changes, UserName = UserName });
                    _context.SaveChanges();
                }
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteWinName(WinName cat, string UserName)
        {
            var Existing = _context.WinNames.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                _context.WinNameHistories.Add(new WinNameHistory { Changes = $"Операционная система {quote}{cat.Name}{quote} удалена.", UserName = UserName });
                _context.WinNames.Remove(Existing);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
        #endregion

        #region Departaments        
        public DepPaging depPaging(int sort, int page, int pageSize)
        {
            DepPaging depPaging = new DepPaging();
            depPaging.Departaments = new List<Departament>();
            depPaging.TotalCount = _context.Departaments.Count();
            depPaging.TotalPages = (int)Math.Ceiling(depPaging.TotalCount / (double)pageSize);
            var skip = page == 1 ? 0 : (page - 1) * pageSize;
            if (IsEven(sort))
            {
                depPaging.Departaments = _context.Departaments
                    .Include(x => x.Employers)
                    .Include(x => x.Units)
                    .OrderByDescending(DepOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                depPaging.Departaments = _context.Departaments
                    .Include(x => x.Employers)
                    .Include(x => x.Units)
                    .OrderBy(DepOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            return depPaging;
        }        

        public Task<bool> CheckDepName(string name, int id)
        {
            if (_context.Departaments.Where(x => x.Name == name).Count() > 0 && _context.Departaments.Where(x => x.Name == name).FirstOrDefault().Id != id)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        public Task<bool> CreateDepartament(Departament cat, string UserName)
        {
            _context.Departaments.Add(cat);
            _context.DepartamentHistories.Add(new DepartamentHistory { Changes = $"Добавлен новый отдел. Название: {quote}{cat.Name}{quote}.", UserName = UserName });
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> EditDepartament(Departament cat, string UserName)
        {
            var Existing = _context.Departaments.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                int changes = 0;
                string Changes = $"Отдел {quote}{Existing.Name}{quote} отредактирован. ";
                if (cat.Name != Existing.Name)
                {
                    Changes += $" Название изменено на {quote}{cat.Name}{quote}. ";
                    Existing.Name = cat.Name;
                    changes++;
                }
                if (changes > 0)
                {
                    _context.Entry(Existing).State = EntityState.Modified;
                    _context.DepartamentHistories.Add(new DepartamentHistory { Changes = Changes, UserName = UserName });
                    _context.SaveChanges();
                }
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteDepartament(Departament cat, string UserName, int newcat)
        {
            var Existing = _context.Departaments.Where(x => x.Id == cat.Id).Include(x => x.Employers).FirstOrDefault();
            if (Existing != null)
            {
                if (Existing.Employers.Count > 0 && newcat != 0)
                {
                    var NewDep = _context.Departaments.Where(x => x.Id == newcat).FirstOrDefault();
                    if (NewDep != null)
                    {
                        foreach (var emp in Existing.Employers.ToList())
                        {
                            emp.DepartamentId = NewDep.Id;
                            _context.Entry(emp).State = EntityState.Modified;
                            _context.EmployerHistories.Add(new EmployerHistory { UserName = UserName, Changes = $"Сотрудник {quote}{emp.FirstName} {emp.FatherName} {emp.LastName}{quote} переведен в отдел {quote}{NewDep.Name}{quote} в результате удаления отдела {quote}{Existing.Name}{quote}" });
                        }
                    }
                }
                _context.DepartamentHistories.Add(new DepartamentHistory { Changes = $"Отдел {quote}{cat.Name}{quote} удален.", UserName = UserName });
                _context.Departaments.Remove(Existing);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
        #endregion

        #region Employers
        public EmpPaging empPaging(int sort, int page, int pageSize, int dep)
        {
            EmpPaging empPagaing = new EmpPaging();
            empPagaing.Employers = new List<Employer>();
            empPagaing.TotalCount = _context.Employers.Where(x => dep == 0 ? true : x.DepartamentId == dep).Count();
            empPagaing.TotalPages = (int)Math.Ceiling(empPagaing.TotalCount / (double)pageSize);
            var skip = page == 1 ? 0 : (page - 1) * pageSize;
            if (IsEven(sort))
            {
                empPagaing.Employers = _context.Employers
                    .Include(x => x.Units)
                    .Include(x => x.Departament)
                    .Include(x => x.Emails)
                    .Where(x => dep == 0 ? true : x.DepartamentId == dep)
                    .OrderByDescending(EmpOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                empPagaing.Employers = _context.Employers
                    .Include(x => x.Units)
                    .Include(x => x.Departament)
                    .Include(x => x.Emails)
                    .Where(x => dep == 0 ? true : x.DepartamentId == dep)
                    .OrderBy(EmpOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            return empPagaing;
        }

        public Task<bool> RenameEmployers()
        {
            foreach (var emp in _context.Employers.ToList())
            {
                emp.Name = emp.LastName + " " + emp.FirstName + " " + emp.FatherName;
            }
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> CreateEmployer(Employer cat, string UserName)
        {
            var Departament = _context.Departaments.Where(x => x.Id == cat.DepartamentId).FirstOrDefault();
            if (Departament != null)
            {
                cat.Name = cat.LastName + " " + cat.FirstName + " " + cat.FatherName;
                _context.Employers.Add(cat);
                _context.EmployerHistories.Add(new EmployerHistory { Changes = $"Добавлен новый сотрудник: {quote}{cat.Name}{quote}. Отдел: {quote}{Departament.Name}{quote}. Должность: {quote}{cat.Post}{quote}.", UserName = UserName });
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> EditEmployer(Employer cat, string UserName)
        {
            var Existing = _context.Employers.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                int changes = 0;
                string Changes = $"Сотрудник {quote}{Existing.Name}{quote} отредактирован. ";
                if (cat.FirstName != Existing.FirstName)
                {
                    Changes += $" Имя изменено на {quote}{cat.FirstName}{quote}. ";
                    Existing.FirstName = cat.FirstName;
                    changes++;
                }
                if (cat.FatherName != Existing.FatherName)
                {
                    Changes += $" Отчество изменено на {quote}{cat.FatherName}{quote}. ";
                    Existing.FatherName = cat.FatherName;
                    changes++;
                }
                if (cat.LastName != Existing.LastName)
                {
                    Changes += $" Фамилия изменена на {quote}{cat.LastName}{quote}. ";
                    Existing.LastName = cat.LastName;
                    changes++;
                }
                if (cat.Post != Existing.Post)
                {
                    Changes += $" Должность изменена на {quote}{cat.Post}{quote}. ";
                    Existing.Post = cat.Post;
                    changes++;
                }
                if (cat.DepartamentId != Existing.DepartamentId)
                {
                    var newdep = _context.Departaments.Where(x => x.Id == cat.DepartamentId).FirstOrDefault();
                    if (newdep != null)
                    {
                        Changes += $" Отдел изменена на {quote}{newdep.Name}{quote}. ";
                        Existing.DepartamentId = cat.DepartamentId;
                        changes++;
                    }
                }
                if (changes > 0)
                {
                    Existing.Name = Existing.LastName + " " + Existing.FirstName + " " + Existing.FatherName;
                    _context.Entry(Existing).State = EntityState.Modified;
                    _context.EmployerHistories.Add(new EmployerHistory { Changes = Changes, UserName = UserName });
                    _context.SaveChanges();
                }
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteEmployer(Employer cat, string UserName)
        {
            var Existing = _context.Employers.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                _context.EmployerHistories.Add(new EmployerHistory { Changes = $"Сотрудник {quote}{cat.Name}{quote} удален.", UserName = UserName });
                _context.Employers.Remove(Existing);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
        #endregion

        #region UnitStatuses
        public StatusPaging statusPaging(int sort, int page, int pageSize)
        {
            StatusPaging statusPaging = new StatusPaging();
            statusPaging.UnitStatuses = new List<UnitStatus>();
            statusPaging.TotalCount = _context.UnitStatuses.Count();
            statusPaging.TotalPages = (int)Math.Ceiling(statusPaging.TotalCount / (double)pageSize);
            var skip = page == 1 ? 0 : (page - 1) * pageSize;
            if (IsEven(sort))
            {
                statusPaging.UnitStatuses = _context.UnitStatuses
                    .Include(x => x.Units)
                    .OrderByDescending(StatusOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                statusPaging.UnitStatuses = _context.UnitStatuses
                    .Include(x => x.Units)
                    .OrderBy(StatusOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            return statusPaging;
        }

        public Task<bool> CreateStatus(UnitStatus cat, string UserName)
        {
            _context.UnitStatuses.Add(cat);
            _context.UnitStatusHistories.Add(new UnitStatusHistory { Changes = $"Добавлен новый статус техники. Название: {quote}{cat.Name}{quote}.", UserName = UserName });
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> EditStatus(UnitStatus cat, string UserName)
        {
            var Existing = _context.UnitStatuses.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                int changes = 0;
                string Changes = $"Статус техники {quote}{Existing.Name}{quote} отредактирован. ";
                if (cat.Name != Existing.Name)
                {
                    Changes += $" Название изменено на {quote}{cat.Name}{quote}. ";
                    Existing.Name = cat.Name;
                    changes++;
                }
                if (changes > 0)
                {
                    _context.Entry(Existing).State = EntityState.Modified;
                    _context.UnitStatusHistories.Add(new UnitStatusHistory { Changes = Changes, UserName = UserName });
                    _context.SaveChanges();
                }
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteStatus(UnitStatus cat, string UserName, int newcat)
        {
            var Existing = _context.UnitStatuses.Where(x => x.Id == cat.Id).Include(x => x.Units).FirstOrDefault();
            if (Existing != null)
            {
                if (Existing.Units.Count > 0 && newcat != 0)
                {
                    var NewStatus = _context.UnitStatuses.Where(x => x.Id == newcat).FirstOrDefault();
                    if (NewStatus != null)
                    {
                        foreach (var unit in Existing.Units.ToList())
                        {
                            unit.UnitStatusId = NewStatus.Id;
                            _context.Entry(unit).State = EntityState.Modified;
                            _context.UnitHistories.Add(new UnitHistory { UnitId = unit.Id, Secure = false, UserName = UserName, Change = $"Изменен статус техники на {quote}{NewStatus.Name}{quote} в результате удаления статуса {quote}{Existing.Name}{quote}" });
                        }
                    }
                }
                _context.UnitStatusHistories.Add(new UnitStatusHistory { Changes = $"Статус техники {quote}{cat.Name}{quote} удален.", UserName = UserName });
                _context.UnitStatuses.Remove(Existing);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
        #endregion

        #region Categories
        public CatPaging catPaging(int sort, int page, int pageSize)
        {
            CatPaging catPaging = new CatPaging();
            catPaging.Categories = new List<Category>();
            catPaging.TotalCount = _context.Categories.Count();
            catPaging.TotalPages = (int)Math.Ceiling(catPaging.TotalCount / (double)pageSize);
            var skip = page == 1 ? 0 : (page - 1) * pageSize;
            if (IsEven(sort))
            {
                catPaging.Categories = _context.Categories
                    .Include(x => x.Units)
                    .OrderByDescending(CatOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                catPaging.Categories = _context.Categories
                    .Include(x => x.Units)
                    .OrderBy(CatOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            return catPaging;
        }        

        public Task<bool> CheckCatName(string name, int id)
        {
            if (_context.Categories.Where(x => x.Name == name).Count() > 0 && _context.Categories.Where(x => x.Name == name).FirstOrDefault().Id != id)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        public Task<List<Category>> GetCategoriesAdd()
        {
            List<Category> categories = new List<Category>();
            categories = _context.Categories.OrderBy(x => x.Name).ToList();
            return Task.FromResult(categories);
        }

        public Task<bool> CreateCategory(Category cat, string UserName)
        {
            _context.Categories.Add(cat);
            string Changes = $"Добавлен новый тип техники {quote}{cat.Name}{quote}. Типы данных: ";
            List<string> types = new List<string>();
            if (cat.Manufacturer) types.Add("Произвоитель");
            if (cat.Model) types.Add("Модель");
            if (cat.Location) types.Add("Местоположение");
            if (cat.InventId) types.Add("Инвентарный номер");
            if (cat.Serial) types.Add("Серийный номер");
            if (cat.BuyDate) types.Add("Дата приобретения");
            if (cat.InstallDate) types.Add("Дата установки");
            if (cat.Employer) types.Add("Сотрудник");
            if (cat.Departament) types.Add("Отдел");
            if (cat.WinName) types.Add("Операционная система");
            if (cat.Processor) types.Add("Процессор");
            if (cat.Motherboard) types.Add("Материнская плата");
            if (cat.DDR) types.Add("Оперативная память");
            if (cat.Specs) types.Add("Характеристики");
            if (cat.CartridgeModel) types.Add("Модель картриджа");
            if (cat.CartridgeCount) types.Add("Количество картриджей");
            if (cat.StoredFiles) types.Add("Файлы");
            if (cat.ServiceWorks) types.Add("Ремонтные работы");
            if (cat.InstaledSofts) types.Add("Установленное ПО");
            if (cat.IPAdresses) types.Add("IP адреса");
            if (cat.NetName) types.Add("Сетевое имя");
            if (cat.BiosPass) types.Add("Пароль BIOS");
            if (cat.WinAccounts) types.Add("Аккаунты Windows");
            if (cat.RdpConnects) types.Add("RDP аккаунты");
            if (cat.Comments) types.Add("Комментарий");
            Changes += string.Join(", ", types.ToArray()) + ".";
            _context.CategoryHistories.Add(new CategoryHistory { Changes = Changes, UserName = UserName });
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> EditCategory(Category cat, string UserName)
        {
            var Existing = _context.Categories.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                int changes = 0;
                string Changes = $"Тип техники {quote}{Existing.Name}{quote} отредактирован. ";
                List<string> Added = new List<string>();
                List<string> Removed = new List<string>();
                if (Existing.Name != cat.Name)
                {
                    Existing.Name = cat.Name;
                    Changes += $"Новое название: {quote}{cat.Name}{quote}. ";
                    changes++;
                }
                if (Existing.Manufacturer != cat.Manufacturer)
                {
                    Existing.Manufacturer = cat.Manufacturer;
                    if (cat.Manufacturer)
                        Added.Add("Производитель");
                    else
                        Removed.Add("Производитель");
                    changes++;
                }
                if (Existing.Model != cat.Model)
                {
                    Existing.Model = cat.Model;
                    if (cat.Model)
                        Added.Add("Модель");
                    else
                        Removed.Add("Модель");
                    changes++;
                }
                if (Existing.Location != cat.Location)
                {
                    Existing.Location = cat.Location;
                    if (cat.Location)
                        Added.Add("Местоположение");
                    else
                        Removed.Add("Местоположение");
                    changes++;
                }
                if (Existing.InventId != cat.InventId)
                {
                    Existing.InventId = cat.InventId;
                    if (cat.InventId)
                        Added.Add("Инвентарный номер");
                    else
                        Removed.Add("Инвентарный номер");
                    changes++;
                }
                if (Existing.Serial != cat.Serial)
                {
                    Existing.Serial = cat.Serial;
                    if (cat.Serial)
                        Added.Add("Серийный номер");
                    else
                        Removed.Add("Серийный номер");
                    changes++;
                }
                if (Existing.BuyDate != cat.BuyDate)
                {
                    Existing.BuyDate = cat.BuyDate;
                    if (cat.BuyDate)
                        Added.Add("Дата приобретения");
                    else
                        Removed.Add("Дата приобретения");
                    changes++;
                }
                if (Existing.InstallDate != cat.InstallDate)
                {
                    Existing.InstallDate = cat.InstallDate;
                    if (cat.InstallDate)
                        Added.Add("Дата установки");
                    else
                        Removed.Add("Дата установки");
                    changes++;
                }
                if (Existing.Employer != cat.Employer)
                {
                    Existing.Employer = cat.Employer;
                    if (cat.Employer)
                        Added.Add("Сотрудник");
                    else
                        Removed.Add("Сотрудник");
                    changes++;
                }
                if (Existing.Departament != cat.Departament)
                {
                    Existing.Departament = cat.Departament;
                    if (cat.Departament)
                        Added.Add("Отдел");
                    else
                        Removed.Add("Отдел");
                    changes++;
                }
                if (Existing.WinName != cat.WinName)
                {
                    Existing.WinName = cat.WinName;
                    if (cat.WinName)
                        Added.Add("Операционная система");
                    else
                        Removed.Add("Операционная система");
                    changes++;
                }
                if (Existing.Processor != cat.Processor)
                {
                    Existing.Processor = cat.Processor;
                    if (cat.Processor)
                        Added.Add("Процессор");
                    else
                        Removed.Add("Процессор");
                    changes++;
                }
                if (Existing.Motherboard != cat.Motherboard)
                {
                    Existing.Motherboard = cat.Motherboard;
                    if (cat.Motherboard)
                        Added.Add("Материнская плата");
                    else
                        Removed.Add("Материнская плата");
                    changes++;
                }
                if (Existing.DDR != cat.DDR)
                {
                    Existing.DDR = cat.DDR;
                    if (cat.DDR)
                        Added.Add("Оперативная память");
                    else
                        Removed.Add("Оперативная память");
                    changes++;
                }
                if (Existing.Specs != cat.Specs)
                {
                    Existing.Specs = cat.Specs;
                    if (cat.Specs)
                        Added.Add("Характеристики");
                    else
                        Removed.Add("Характеристики");
                    changes++;
                }
                if (Existing.CartridgeModel != cat.CartridgeModel)
                {
                    Existing.CartridgeModel = cat.CartridgeModel;
                    if (cat.CartridgeModel)
                        Added.Add("Модель картриджа");
                    else
                        Removed.Add("Модель картриджа");
                    changes++;
                }
                if (Existing.CartridgeCount != cat.CartridgeCount)
                {
                    Existing.CartridgeCount = cat.CartridgeCount;
                    if (cat.CartridgeCount)
                        Added.Add("Количество картриджей");
                    else
                        Removed.Add("Количество картриджей");
                    changes++;
                }
                if (Existing.StoredFiles != cat.StoredFiles)
                {
                    Existing.StoredFiles = cat.StoredFiles;
                    if (cat.StoredFiles)
                        Added.Add("Файлы");
                    else
                        Removed.Add("Файлы");
                    changes++;
                }
                if (Existing.ServiceWorks != cat.ServiceWorks)
                {
                    Existing.ServiceWorks = cat.ServiceWorks;
                    if (cat.ServiceWorks)
                        Added.Add("Ремонтные работы");
                    else
                        Removed.Add("Ремонтные работы");
                    changes++;
                }
                if (Existing.InstaledSofts != cat.InstaledSofts)
                {
                    Existing.InstaledSofts = cat.InstaledSofts;
                    if (cat.InstaledSofts)
                        Added.Add("Установленное ПО");
                    else
                        Removed.Add("Установленное ПО");
                    changes++;
                }
                if (Existing.IPAdresses != cat.IPAdresses)
                {
                    Existing.IPAdresses = cat.IPAdresses;
                    if (cat.IPAdresses)
                        Added.Add("IP адреса");
                    else
                        Removed.Add("IP адреса");
                    changes++;
                }
                if (Existing.NetName != cat.NetName)
                {
                    Existing.NetName = cat.NetName;
                    if (cat.NetName)
                        Added.Add("Сетевое имя");
                    else
                        Removed.Add("Сетевое имя");
                    changes++;
                }
                if (Existing.BiosPass != cat.BiosPass)
                {
                    Existing.BiosPass = cat.BiosPass;
                    if (cat.BiosPass)
                        Added.Add("Пароль BIOS");
                    else
                        Removed.Add("Пароль BIOS");
                    changes++;
                }
                if (Existing.WinAccounts != cat.WinAccounts)
                {
                    Existing.WinAccounts = cat.WinAccounts;
                    if (cat.WinAccounts)
                        Added.Add("Аккаунты Windows");
                    else
                        Removed.Add("Аккаунты Windows");
                    changes++;
                }
                if (Existing.RdpConnects != cat.RdpConnects)
                {
                    Existing.RdpConnects = cat.RdpConnects;
                    if (cat.RdpConnects)
                        Added.Add("RDP аккаунты");
                    else
                        Removed.Add("RDP аккаунты");
                    changes++;
                }
                if (Existing.Comments != cat.Comments)
                {
                    Existing.Comments = cat.Comments;
                    if (cat.Comments)
                        Added.Add("Комментарий");
                    else
                        Removed.Add("Комментарий");
                    changes++;
                }
                if (changes > 0)
                {
                    _context.Entry(Existing).State = EntityState.Modified;
                    if (Added.Count > 0) Changes += "Добавлены типы данных: " + string.Join(", ", Added.ToArray()) + ". ";
                    if (Removed.Count > 0) Changes += "Удалены типы данных: " + string.Join(", ", Removed.ToArray()) + ". ";
                    _context.CategoryHistories.Add(new CategoryHistory { Changes = Changes, UserName = UserName });
                    _context.SaveChanges();
                }
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteCat(Category cat, string UserName, int newcat)
        {
            var Existing = _context.Categories.Where(x => x.Id == cat.Id).Include(x => x.Units).FirstOrDefault();
            if (Existing != null)
            {
                if (Existing.Units.Count > 0 && newcat != 0)
                {
                    var NewCategory = _context.Categories.Where(x => x.Id == newcat).FirstOrDefault();
                    if (NewCategory != null)
                    {
                        foreach (var unit in Existing.Units.ToList())
                        {
                            unit.CategoryId = NewCategory.Id;
                            _context.Entry(unit).State = EntityState.Modified;
                            _context.UnitHistories.Add(new UnitHistory { UnitId = unit.Id, Secure = false, UserName = UserName, Change = $"Изменен тип техники на {quote}{NewCategory.Name}{quote} в результате удаления типа {quote}{Existing.Name}{quote}" });
                        }
                    }
                }
                _context.CategoryHistories.Add(new CategoryHistory { Changes = $"Тип техники {quote}{cat.Name}{quote} удален.", UserName = UserName });
                _context.Categories.Remove(Existing);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
        #endregion

        #region Histories
        public Task<List<CategoryHistory>> GetCategoryHistories()
        {
            List<CategoryHistory> categoryHistories = new List<CategoryHistory>();
            categoryHistories = _context.CategoryHistories.OrderByDescending(x => x.Time).ToList();
            return Task.FromResult(categoryHistories);
        }

        public Task<List<EmailHistory>> GetEmailHistories()
        {
            List<EmailHistory> EmailHistories = new List<EmailHistory>();
            EmailHistories = _context.EmailHistories.OrderByDescending(x => x.Time).ToList();
            return Task.FromResult(EmailHistories);
        }

        public Task<List<InstalledSoftHistory>> GetInstalledSoftHistories()
        {
            List<InstalledSoftHistory> installedSoftHistories = new List<InstalledSoftHistory>();
            installedSoftHistories = _context.InstalledSoftHistories.OrderByDescending(x => x.Time).ToList();
            return Task.FromResult(installedSoftHistories);
        }

        public Task<List<ManufacturerHistory>> GetManufacturerHistories()
        {
            List<ManufacturerHistory> manufacturerHistories = new List<ManufacturerHistory>();
            manufacturerHistories = _context.ManufacturerHistories.OrderByDescending(x => x.Time).ToList();
            return Task.FromResult(manufacturerHistories);
        }

        public Task<List<UnitStatusHistory>> GetStatusHistories()
        {
            List<UnitStatusHistory> statusHistories = new List<UnitStatusHistory>();
            statusHistories = _context.UnitStatusHistories.OrderByDescending(x => x.Time).ToList();
            return Task.FromResult(statusHistories);
        }

        public Task<List<EmployerHistory>> GetEmployersHistories()
        {
            List<EmployerHistory> employersHistories = new List<EmployerHistory>();
            employersHistories = _context.EmployerHistories.OrderByDescending(x => x.Time).ToList();
            return Task.FromResult(employersHistories);
        }

        public Task<List<DepartamentHistory>> GetDepartamentsHistories()
        {
            List<DepartamentHistory> departamentsHistories = new List<DepartamentHistory>();
            departamentsHistories = _context.DepartamentHistories.OrderByDescending(x => x.Time).ToList();
            return Task.FromResult(departamentsHistories);
        }

        public Task<List<WinNameHistory>> GetWinNameHistories()
        {
            List<WinNameHistory> winnameHistories = new List<WinNameHistory>();
            winnameHistories = _context.WinNameHistories.OrderByDescending(x => x.Time).ToList();
            return Task.FromResult(winnameHistories);
        }

        public Task<List<UserHistory>> GetUserHistoriesAsync()
        {
            List<UserHistory> histories = new List<UserHistory>();
            histories = _context.UserHistories.OrderByDescending(x => x.Time).ToList();
            return Task.FromResult(histories);
        }

        public Task<UserHistory> AddUserHistoryAsync(string text, string UserName)
        {
            UserHistory objHs = new UserHistory { Changes = text, UserName = UserName };
            _context.UserHistories.Add(objHs);
            _context.SaveChanges();
            return Task.FromResult(objHs);
        }
        #endregion

        #region UserSettings
        public Task<UserSettings> GetSettings(string id, bool admin)
        {
            UserSettings userSettings = _context.UserSettings.Where(x => x.UserID == id).FirstOrDefault();
            if (userSettings == null)
            {
                userSettings = new UserSettings 
                { 
                    Collumn1 = "departament", 
                    Collumn2 = "model", 
                    Collumn3 = "employer", 
                    Collumn4 = "category", 
                    Collumn5 = admin ? "netname" : "invent", 
                    Collumn6 = admin ? "ip" : "serial", 
                    Collumn7 = "specs", 
                    Collumn8 = "installdate", 
                    Collumn9 = "Не выбран",
                    Collumn10 = "Не выбран",
                    Collumn11 = "Не выбран",
                    Collumn12 = "Не выбран",
                    Collumn13 = "Не выбран",
                    SortOrder = 1,
                    UserID = id,
                    RowsCount = 10
                };
                _context.UserSettings.Add(userSettings);
                _context.SaveChanges();
            }
            return Task.FromResult(userSettings);
        }

        public Task<bool> SaveSettings(int id)
        {
            UserSettings userSettings = _context.UserSettings.Where(x => x.Id == id).FirstOrDefault();
            if (userSettings != null)
            {
                _context.Entry(userSettings).State = EntityState.Modified;
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        #endregion

        #region Emails
        public EmailPaging EmailPaging(int sort, int page, int pageSize, int emp)
        {
            EmailPaging EmailPaging = new EmailPaging();
            EmailPaging.Emails = new List<Email>();
            EmailPaging.TotalCount = _context.Emails.Where(x => emp == 0 ? true : x.EmployerId == emp).Count();
            EmailPaging.TotalPages = (int)Math.Ceiling(EmailPaging.TotalCount / (double)pageSize);
            var skip = page == 1 ? 0 : (page - 1) * pageSize;
            if (IsEven(sort))
            {
                EmailPaging.Emails = _context.Emails
                    .Include(x => x.Employer)
                    .Where(x => emp == 0 ? true : x.EmployerId == emp)
                    .OrderByDescending(EmOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            else
            {
                EmailPaging.Emails = _context.Emails
                    .Include(x => x.Employer)
                    .Where(x => emp == 0 ? true : x.EmployerId == emp)
                    .OrderBy(EmOrderDict[sort])
                    .Skip(skip).Take(pageSize).ToList();
            }
            return EmailPaging;
        }

        public Task<bool> CheckEmailAddress(string email, int id)
        {
            if (_context.Emails.Where(x => x.Login == email).Count() > 0 && _context.Emails.Where(x => x.Login == email).FirstOrDefault().Id != id)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        public Task<bool> CreateEmail(Email cat, string UserName)
        {
            _context.Emails.Add(cat);
            _context.EmailHistories.Add(new EmailHistory { Changes = $"Добавлен новый аккаунт email: {quote}Адрес: {cat.Login}. Пароль: {cat.Password}.{quote}.", UserName = UserName });
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<bool> EditEmail(Email cat, string UserName)
        {
            var Existing = _context.Emails.Include(x => x.Employer).Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                int changes = 0;
                string Changes = $"Аккаунт Email {quote}{Existing.Login}{quote} отредактирован. ";
                if (cat.Login != Existing.Login)
                {
                    Changes += $" Адрес {quote}{Existing.Login}{quote} изменен на {quote}{cat.Login}{quote}. ";
                    Existing.Login = cat.Login;
                    changes++;
                }
                if (cat.Password != Existing.Password)
                {
                    Changes += $" Пароль {quote}{Existing.Password}{quote} изменен на {quote}{cat.Password}{quote}. ";
                    Existing.Password = cat.Password;
                    changes++;
                }
                if (cat.EmployerId != Existing.EmployerId)
                {
                    if (cat.EmployerId != 0)
                    {
                        var emp = _context.Employers.Where(x => x.Id == cat.EmployerId).FirstOrDefault();
                        if (emp != null)
                        {
                            Changes += $" Сотрудник {quote}{Existing.Employer?.Name}{quote} изменен на {quote}{emp.Name}{quote}. ";
                            Existing.EmployerId = cat.EmployerId;
                            changes++;
                        }
                    }
                    else
                    {
                        Changes += $" Сотрудник {quote}{Existing.Employer.Name}{quote} изменен на {quote}Не указан{quote}. ";
                        Existing.EmployerId = null;
                        changes++;
                    }
                }
                if (changes > 0)
                {
                    _context.Entry(Existing).State = EntityState.Modified;
                    _context.EmailHistories.Add(new EmailHistory { Changes = Changes, UserName = UserName });
                    _context.SaveChanges();
                }
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteEmail(Email cat, string UserName)
        {
            var Existing = _context.Emails.Where(x => x.Id == cat.Id).FirstOrDefault();
            if (Existing != null)
            {
                _context.EmailHistories.Add(new EmailHistory { Changes = $"Аккаунт email {quote}{cat.Login}{quote} удален.", UserName = UserName });
                _context.Emails.Remove(Existing);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
        #endregion

        #region Index
        public Task<ChartData> GetCharts()
        {
            ChartData chartData = new ChartData();
            chartData.depUnitsBar = new Dictionary<string, double>();
            chartData.catUnitsPie = new Dictionary<string, double>();
            foreach (var dep in _context.Departaments.Include(x => x.Units).Where(x => x.Units.Count > 0).OrderBy(x => x.Name))
                chartData.depUnitsBar.Add(dep.Name, dep.Units.Count);
            foreach (var cat in _context.Categories.Include(x => x.Units).Where(x => x.Units.Count > 0).OrderBy(x => x.Name))
                chartData.catUnitsPie.Add(cat.Name, cat.Units.Count);
            return Task.FromResult(chartData);
        }
        #endregion

        #region Reports
        public byte[] EmpsReport(EmpReport report)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Сотрудники");
                int count = 1;
                int row = 1;
                worksheet.Cells[1, count].Value = "Сотрудник";
                if (report.Dep)
                {
                    count++;
                    worksheet.Cells[1, count].Value = "Отдел";
                }
                if (report.Post)
                {
                    count++;
                    worksheet.Cells[1, count].Value = "Должность";
                }
                if (report.Email)
                {
                    count++;
                    worksheet.Cells[1, count].Value = "Email";
                }
                if (report.UnitCount)
                {
                    count++;
                    worksheet.Cells[1, count].Value = "Количество техники";
                }
                using (var range = worksheet.Cells[1, 1, 1, count])
                {
                    if (report.HeaderBold) range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.HeaderColor));
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                }
                foreach (var emp in _context.Employers.Include(x => x.Emails).Include(x => x.Units).Include(x => x.Departament))
                {
                    row++;
                    int celc = 1;
                    worksheet.Cells[row, celc].Value = emp.Name;
                    if (report.Dep)
                    {
                        celc++;
                        worksheet.Cells[row, celc].Value = emp.Departament.Name;
                    }
                    if (report.Post)
                    {
                        celc++;
                        worksheet.Cells[row, celc].Value = emp.Post;
                    }
                    if (report.Email)
                    {
                        celc++;
                        worksheet.Cells[row, celc].Value = string.Join(", ", emp.Emails.OrderBy(a => a.Login).Select(x => x.Login).ToArray());
                    }
                    if (report.UnitCount)
                    {
                        celc++;
                        worksheet.Cells[row, celc].Value = emp.Units.Count();
                    }
                }
                using (var range = worksheet.Cells[2, 1, row, 1])
                {
                    if (report.EmpBold) range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.EmpColor));
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                int celc2 = 1;
                worksheet.Column(celc2).AutoFit();
                if (report.Dep)
                {
                    celc2++;
                    worksheet.Column(celc2).AutoFit();
                    using (var range = worksheet.Cells[2, celc2, row, celc2])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.CellsColor));
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                }
                if (report.Post)
                {
                    celc2++;
                    worksheet.Column(celc2).AutoFit();
                    using (var range = worksheet.Cells[2, celc2, row, celc2])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.CellsColor));
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                }
                if (report.Email)
                {
                    celc2++;
                    worksheet.Column(celc2).AutoFit();
                    using (var range = worksheet.Cells[2, celc2, row, celc2])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.CellsColor));
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                }
                if (report.UnitCount)
                {
                    celc2++;
                    worksheet.Column(celc2).AutoFit();
                    using (var range = worksheet.Cells[2, celc2, row, celc2])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.CellsColor));
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                }
                worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.FitToWidth = 1;
                worksheet.PrinterSettings.FitToHeight = 0;
                worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");
                worksheet.View.FreezePanes(2, 1);
                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, row, celc2];
                return package.GetAsByteArray();
            }
        }
        public byte[] DepsReport(DepReport report)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Отделы");
                int count = 1;
                int row = 1;
                worksheet.Cells[1,count].Value = "Отдел";
                if (report.EmpCount)
                {
                    count++;
                    worksheet.Cells[1,count].Value = "Количество сотрудников";
                }
                if (report.UnitsCount)
                {
                    count++;
                    worksheet.Cells[1, count].Value = "Количество техники";
                }
                using (var range = worksheet.Cells[1, 1, 1, count])
                {
                    if (report.HeaderBold) range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.HeaderColor));
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                }
                foreach (var dep in _context.Departaments.Include(x => x.Employers).Include(x => x.Units).OrderBy(x => x.Name))
                {
                    row++;
                    int celc = 1;
                    worksheet.Cells[row, celc].Value = dep.Name;
                    if (report.EmpCount)
                    {
                        celc++;
                        worksheet.Cells[row, celc].Value = dep.Employers.Count();
                    }
                    if (report.UnitsCount)
                    {
                        celc++;
                        worksheet.Cells[row, celc].Value = dep.Units.Count();
                    }
                }
                using (var range = worksheet.Cells[2, 1, row, 1])
                {
                    if (report.DepBold) range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.DepColor));
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                int celc2 = 1;
                worksheet.Column(celc2).AutoFit();
                if (report.EmpCount)
                {
                    celc2++;
                    worksheet.Column(celc2).AutoFit();
                    using (var range = worksheet.Cells[2, celc2, row, celc2])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.CellsColor));
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                }
                if (report.UnitsCount)
                {
                    celc2++;
                    worksheet.Column(celc2).AutoFit();
                    using (var range = worksheet.Cells[2, celc2, row, celc2])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.CellsColor));
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                }
                worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.FitToWidth = 1;
                worksheet.PrinterSettings.FitToHeight = 0;
                worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");
                worksheet.View.FreezePanes(2, 1);
                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, row, celc2];
                return package.GetAsByteArray();
            }
        }
        public byte[] EmailsReport(EmailReport report)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Аккаунты Email");
                worksheet.Cells["A1"].Value = "Сотрудник";
                worksheet.Cells["B1"].Value = "Email";
                if (report.Password) worksheet.Cells["C1"].Value = "Пароль";
                using (var range = worksheet.Cells[report.Password ? "A1:C1" : "A1:B1"])
                {
                    if (report.HeaderBold) range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.HeaderColor));
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                }
                int count = 1;
                foreach (var email in _context.Emails.Include(x => x.Employer).OrderBy(x => x.Employer.Name))
                {
                    count++;
                    worksheet.Cells["A" + count].Value = email.Employer.Name;
                    worksheet.Cells["B" + count].Value = email.Login;
                    if (report.Password) worksheet.Cells["C" + count].Value = email.Password;
                }
                using (var range = worksheet.Cells[$"A2:A{count}"])
                {
                    if (report.EmployerBold) range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.EmployerColor));
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                using (var range = worksheet.Cells[$"B2:B{count}"])
                {
                    if (report.EmailBold) range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.CellsColor));
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                if (report.Password)
                {
                    using (var range = worksheet.Cells[$"C2:C{count}"])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(report.CellsColor));
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                }
                worksheet.Column(1).AutoFit();
                worksheet.Column(2).AutoFit();
                if (report.Password) worksheet.Column(3).AutoFit();
                worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.FitToWidth = 1;
                worksheet.PrinterSettings.FitToHeight = 0;
                worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");
                worksheet.View.FreezePanes(2, 1);
                worksheet.PrinterSettings.PrintArea = worksheet.Cells[report.Password ? $"A1:C{count}" : $"A1:B{count}"];
                return package.GetAsByteArray();
            }
        }
        #endregion

        #region Activity
        public Task<ActivityLog> GetUserActivityRange(string user, DateTime start, DateTime end)
        {
            ActivityLog activityLog = new ActivityLog();
            activityLog.UnitLogs = new List<UnitLog>();
            foreach (var h in _context.UnitHistories.Include(x => x.Unit).Where(x => x.UserName == user).Where(x => x.Time >= start && x.Time <= end).OrderByDescending(x => x.Time))
                activityLog.UnitLogs.Add(new UnitLog { Change = h.Change, Time = h.Time, UnitId = h.UnitId, UnitModel = h.Unit.Model });
            activityLog.unitStatusHistories = _context.UnitStatusHistories.Where(x => x.UserName == user).Where(x => x.Time >= start && x.Time <= end).OrderByDescending(x => x.Time).ToList();
            activityLog.userHistories = _context.UserHistories.Where(x => x.UserName == user).Where(x => x.Time >= start && x.Time <= end).OrderByDescending(x => x.Time).ToList();
            activityLog.employerHistories = _context.EmployerHistories.Where(x => x.UserName == user).Where(x => x.Time >= start && x.Time <= end).OrderByDescending(x => x.Time).ToList();
            activityLog.departamentHistories = _context.DepartamentHistories.Where(x => x.UserName == user).Where(x => x.Time >= start && x.Time <= end).OrderByDescending(x => x.Time).ToList();
            activityLog.categoryHistories = _context.CategoryHistories.Where(x => x.UserName == user).Where(x => x.Time >= start && x.Time <= end).OrderByDescending(x => x.Time).ToList();
            activityLog.emailHistories = _context.EmailHistories.Where(x => x.UserName == user).Where(x => x.Time >= start && x.Time <= end).OrderByDescending(x => x.Time).ToList();
            activityLog.installedSoftHistories = _context.InstalledSoftHistories.Where(x => x.UserName == user).Where(x => x.Time >= start && x.Time <= end).OrderByDescending(x => x.Time).ToList();
            activityLog.manufacturerHistories = _context.ManufacturerHistories.Where(x => x.UserName == user).Where(x => x.Time >= start && x.Time <= end).OrderByDescending(x => x.Time).ToList();
            activityLog.winNameHistories = _context.WinNameHistories.Where(x => x.UserName == user).Where(x => x.Time >= start && x.Time <= end).OrderByDescending(x => x.Time).ToList();
            return Task.FromResult(activityLog);
        }
        #endregion
    }
}