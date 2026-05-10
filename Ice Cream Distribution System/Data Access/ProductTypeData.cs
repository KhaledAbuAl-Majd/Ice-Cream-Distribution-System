using Golbal.AppDbContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class ProductTypeData
    {
        public static async Task<ProductType?> Add(ProductType type)
        {
            if (type is null) return null;
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    await context.ProductTypes.AddAsync(type);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء إضافة نوع المنتج: {ex.Message}");
                return null;
            }
            return type;
        }

        public static async Task<bool> Update(ProductType type)
        {
            if (type is null) return false;
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    context.ProductTypes.Update(type);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء تحديث نوع المنتج: {ex.Message}");
                return false;
            }
            return true;
        }

        public static async Task<bool> Delete(short typeID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    var type = await context.ProductTypes.FindAsync(typeID);
                    if (type == null) return false;

                    context.ProductTypes.Remove(type);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء الحذف: {ex.Message}. تأكد أنه لا توجد أصناف مرتبطة بهذا النوع.");
                return false;
            }
        }

        public static async Task<ProductType?> Get(short typeID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.ProductTypes
                        .Include(t => t.Products)
                        .FirstOrDefaultAsync(t => t.Id == typeID);
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex.Message);
                return null;
            }
        }

        public static async Task<List<ProductType>?> GetAll()
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.ProductTypes
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
