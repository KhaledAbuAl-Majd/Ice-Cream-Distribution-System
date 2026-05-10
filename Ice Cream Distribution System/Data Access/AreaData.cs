using Golbal.DBContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class AreaData
    {
        public static async Task<List<Area>?> GetAll()
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Areas
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

        public static async Task<Area?> Get(int areaID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Areas
                        .AsNoTracking()
                        .FirstOrDefaultAsync(a => a.Id == areaID);
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
