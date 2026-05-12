using Golbal.AppDbContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class InvoiceData
    {
        public static async Task<Invoice?> Add(Invoice invoice)
        {
            if (invoice is null)
                return null;

            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    await context.Invoices.AddAsync(invoice);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء حفظ الفاتورة: {ex?.InnerException.Message}");
                return null;
            }

            return invoice;
        }

        public static async Task<bool> Update(Invoice invoice)
        {
            if (invoice is null)
                return false;

            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    context.Invoices.Update(invoice);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء تحديث الفاتورة: {ex?.InnerException.Message}");
                return false;
            }

            return true;
        }

        public static async Task<bool> Delete(int invoiceID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    var invoice = await context.Invoices.FindAsync(invoiceID);
                    if (invoice == null) return false;

                    context.Invoices.Remove(invoice);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء حذف الفاتورة: {ex?.InnerException.Message}");
                return false;
            }
        }

        public static async Task<Invoice?> Get(int invoiceID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Invoices
                        .Include(i => i.Car)
                         .ThenInclude(c => c.Area)
                        .Include(i => i.Store)
                            .ThenInclude(s => s.Owner)
                        .Include(i => i.InvoiceRecords)
                            .ThenInclude(ir => ir.Product)
                        .FirstOrDefaultAsync(i => i.Id == invoiceID);
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex?.InnerException.Message);
                return null;
            }
        }

        public static async Task<List<Invoice>?> GetAll()
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Invoices
                        .Include(i => i.Car)
                            .ThenInclude(c => c.Area)
                        .Include(i => i.Store)
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
