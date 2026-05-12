using Golbal.AppDbContext;
using Ice_Cream_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;
using MoneyMindManagerGlobal;

namespace Data_Access
{
    public static class PaymentData
    {
        public static async Task<Payment?> Add(Payment payment)
        {
            if (payment is null) return null;
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    await context.Payments.AddAsync(payment);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء تسجيل عملية الدفع: {ex?.InnerException.Message}");
                return null;
            }
            return payment;
        }

        public static async Task<bool> Update(Payment payment)
        {
            if (payment is null) return false;
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    context.Payments.Update(payment);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء تحديث بيانات الدفع: {ex?.InnerException.Message}");
                return false;
            }
            return true;
        }

        public static async Task<bool> Delete(int paymentID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    var payment = await context.Payments.FindAsync(paymentID);
                    if (payment == null) return false;

                    context.Payments.Remove(payment);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent($"حدث خطأ أثناء حذف عملية الدفع: {ex?.InnerException.Message}");
                return false;
            }
        }

        public static async Task<Payment?> Get(int paymentID)
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Payments
                        .Include(p => p.Representative)
                            .ThenInclude(r => r.User)
                                .ThenInclude(u => u.Person)
                        .Include(p => p.Store)
                        .FirstOrDefaultAsync(p => p.Id == paymentID);
                }
            }
            catch (Exception ex)
            {
                clsGlobalEvents.RaiseErrorEvent(ex?.InnerException.Message);
                return null;
            }
        }

        public static async Task<List<Payment>?> GetAll()
        {
            try
            {
                using (var context = new IceCreamDistributionDbContext())
                {
                    return await context.Payments
                        .Include(p => p.Representative)
                            .ThenInclude(r => r.User)
                                .ThenInclude(u => u.Person)
                        .Include(p => p.Store)
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
