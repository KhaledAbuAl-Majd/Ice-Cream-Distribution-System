using Golbal.AppDbContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class RepresentativeData
    {
        public static async Task<Representative?> Add(Representative representative)
        {
            if (representative is null)
                return null;

            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    await context.Representatives.AddAsync(representative);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return null;
            }

            return representative;
        }

        public static async Task<bool> Update(Representative representative)
        {
            if (representative is null)
                return false;

            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    context.Representatives.Update(representative);
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

        public static async Task<bool> Delete(int representativeID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    var representative = await context.Representatives.FindAsync(representativeID);
                    if (representative == null) return false;

                    context.Representatives.Remove(representative);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء حذف المندوب: {ex.Message}");
                return false;
            }
        }

        public static async Task<Representative?> Get(int representativeID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Representatives
                        .Include(r => r.User)
                            .ThenInclude(u => u.Person)
                        .Include(r => r.Car)
                        .Include(r => r.RepresentativesStocks)
                        .ThenInclude(rs => rs.Product)
                        .FirstOrDefaultAsync(r => r.Id == representativeID);
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return null;
            }
        }

        public static async Task<List<Representative>?> GetAll()
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Representatives
                        .Include(r => r.User)
                            .ThenInclude(u => u.Person)
                        .Include(r => r.Car)
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
