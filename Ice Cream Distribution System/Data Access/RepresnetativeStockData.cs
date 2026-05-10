using Golbal.AppDbContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class RepresentativesStockData
    {
        //public static async Task<RepresentativesStock?> Add(RepresentativesStock stock)
        //{
        //    if (stock is null)
        //        return null;

        //    try
        //    {
        //        using (var context = new IceCreamDistributionDbContext())
        //        {
        //            await context.RepresentativesStocks.AddAsync(stock);
        //            await context.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobalEvents.RaiseErrorEvent("حدث خطأ أثناء إضافة عهدة المندوب: " + ex.Message);
        //        return null;
        //    }

        //    return stock;
        //}

        //public static async Task<bool> Update(RepresentativesStock stock)
        //{
        //    if (stock is null)
        //        return false;

        //    try
        //    {
        //        using (var context = new IceCreamDistributionDbContext())
        //        {
        //            context.RepresentativesStocks.Update(stock);
        //            await context.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobalEvents.RaiseErrorEvent("حدث خطأ أثناء تحديث بيانات العهدة: " + ex.Message);
        //        return false;
        //    }

        //    return true;
        //}

        //public static async Task<bool> Delete(int id)
        //{
        //    try
        //    {
        //        using (var context = new IceCreamDistributionDbContext())
        //        {
        //            var stock = await context.RepresentativesStocks.FindAsync(id);
        //            if (stock == null) return false;

        //            context.RepresentativesStocks.Remove(stock);
        //            await context.SaveChangesAsync();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء حذف سجل العهدة: {ex.Message}");
        //        return false;
        //    }
        //}

        public static async Task<RepresentativesStock?> Get(int id)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.RepresentativesStocks
                        .Include(s => s.Product)
                        .Include(s => s.Representative)
                            .ThenInclude(r => r.User)
                                .ThenInclude(u => u.Person)
                        .FirstOrDefaultAsync(s => s.Id == id);
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return null;
            }
        }

        public static async Task<List<RepresentativesStock>?> GetAll(int representativeID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.RepresentativesStocks
                        .Where(s => s.RepresentativeId == representativeID)
                        .Include(s => s.Product)
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
