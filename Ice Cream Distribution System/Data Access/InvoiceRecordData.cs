using Golbal.AppDbContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class InvoiceRecordData
    {
        public static async Task<InvoiceRecord?> Add(InvoiceRecord record)
        {
            if (record is null) return null;
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    await context.InvoiceRecords.AddAsync(record);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء إضافة صنف للفاتورة: {ex?.InnerException.Message}");
                return null;
            }
            return record;
        }

        public static async Task<bool> Update(InvoiceRecord record)
        {
            if (record is null) return false;
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    context.InvoiceRecords.Update(record);
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

        public static async Task<bool> Delete(int recordID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    var record = await context.InvoiceRecords.FindAsync(recordID);
                    if (record == null) return false;

                    context.InvoiceRecords.Remove(record);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء حذف الصنف: {ex?.InnerException.Message}");
                return false;
            }
        }

        public static async Task<List<InvoiceRecord>?> GetAllByInvoiceId(int invoiceID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.InvoiceRecords
                        .Where(ir => ir.InvoiceId == invoiceID)
                        .Include(ir => ir.Product)
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

        public static async Task<InvoiceRecord?> Get(int recordID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.InvoiceRecords
                        .Include(ir => ir.Product)
                        .Include(ir => ir.Invoice)
                        .FirstOrDefaultAsync(ir => ir.Id == recordID);
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
