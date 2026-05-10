using Golbal.AppDbContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class CarData
    {
        public static async Task<Car?> Add(Car car)
        {
            if (car is null)
                return null;

            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    await context.Cars.AddAsync(car);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return null;
            }

            return car;
        }

        public static async Task<bool> Update(Car car)
        {
            if (car is null)
                return false;

            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    context.Cars.Update(car);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return false;
            }

            return true;
        }

        public static async Task<bool> Delete(short carID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    var car = await context.Cars.FindAsync(carID);
                    if (car == null) return false;

                    context.Cars.Remove(car);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent("لا يمكن حذف السيارة لارتباطها ببيانات أخرى: " + ex.Message);
                return false;
            }
        }

        public static async Task<Car?> Get(short carID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Cars
                        .Include(c => c.Area)
                        .FirstOrDefaultAsync(c => c.Id == carID);
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return null;
            }
        }

        public static async Task<List<Car>?> GetAll()
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Cars
                        .Include(c => c.Area)
                        .AsNoTracking()
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return null;
            }
        }
    }
}
