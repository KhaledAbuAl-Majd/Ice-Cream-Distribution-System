using Golbal.AppDbContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class StoreData
    {
        public static async Task<Store?> Add(Store store)
        {
            if (store is null) return null;
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    await context.Stores.AddAsync(store);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء إضافة المحل: {ex?.InnerException.Message}");
                return null;
            }
            return store;
        }

        public static async Task<bool> Update(Store store)
        {
            if (store is null) return false;
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    context.Stores.Update(store);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء تحديث بيانات المحل: {ex?.InnerException.Message}");
                return false;
            }
            return true;
        }

        public static async Task<bool> Delete(int storeID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    var store = await context.Stores.FindAsync(storeID);
                    if (store == null) return false;

                    context.Stores.Remove(store);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء حذف المحل: {ex?.InnerException.Message}. تأكد من عدم وجود فواتير مرتبطة به.");
                return false;
            }
        }

        public static async Task<Store?> Get(int storeID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Stores
                        .Include(s => s.Area)
                        .Include(s => s.Owner)
                        .FirstOrDefaultAsync(s => s.Id == storeID);
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex?.InnerException.Message);
                return null;
            }
        }

        public static async Task<List<Store>?> GetAll()
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Stores
                        .Include(s => s.Area)
                        .Include(s => s.Owner)
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
