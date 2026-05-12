using Golbal.AppDbContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class ProductData
    {
        public static async Task<Product?> Add(Product product)
        {
            if (product is null) return null;
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    await context.Products.AddAsync(product);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء إضافة الصنف: {ex?.InnerException.Message}");
                return null;
            }
            return product;
        }

        public static async Task<bool> Update(Product product)
        {
            if (product is null) return false;
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    context.Products.Update(product);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء تحديث الصنف: {ex?.InnerException.Message}");
                return false;
            }
            return true;
        }

        public static async Task<bool> Delete(int productID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    var product = await context.Products.FindAsync(productID);
                    if (product == null) return false;

                    context.Products.Remove(product);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء حذف الصنف: {ex?.InnerException.Message}. تأكد أنه غير مرتبط بفواتير أو عهدة.");
                return false;
            }
        }

        public static async Task<Product?> Get(int productID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Products
                        .Include(p => p.ProductType)
                        .FirstOrDefaultAsync(p => p.Id == productID);
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex?.InnerException.Message);
                return null;
            }
        }

        public static async Task<List<Product>?> GetAll()
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Products
                        .Include(p => p.ProductType)
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
