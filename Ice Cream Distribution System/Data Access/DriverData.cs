using Golbal.AppDbContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class DriverData
    {
        public static async Task<Driver?> Add(Driver driver)
        {
            if (driver is null)
                return null;

            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    await context.Drivers.AddAsync(driver);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex?.InnerException.Message);
                return null;
            }

            return driver;
        }

        public static async Task<bool> Update(Driver driver)
        {
            if (driver is null)
                return false;

            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    context.Drivers.Update(driver);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex?.InnerException.Message);
                return false;
            }

            return true;
        }

        public static async Task<bool> Delete(int driverID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    var driver = await context.Drivers.FindAsync(driverID);
                    if (driver == null) return false;

                    context.Drivers.Remove(driver);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent("لا يمكن حذف السائق لارتباطه بورديات سابقة: " + ex?.InnerException.Message);
                return false;
            }
        }

        public static async Task<Driver?> Get(int driverID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Drivers
                        .Include(d => d.User)
                            .ThenInclude(u => u.Person)
                        .Include(d => d.Car)
                        .FirstOrDefaultAsync(d => d.Id == driverID);
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex?.InnerException.Message);
                return null;
            }
        }

        public static async Task<List<Driver>?> GetAll()
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Drivers
                        .Include(d => d.User)
                            .ThenInclude(u => u.Person)
                        .Include(d => d.Car)
                        .AsNoTracking()
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex?.InnerException.Message);
                return null;
            }
        }
    }
}
