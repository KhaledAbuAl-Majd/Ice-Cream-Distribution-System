using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class InvoiceService
    {
        public static async Task<Invoice?> Add(Invoice invoice)
        {
            if (invoice is null) return null;

            return await InvoiceData.Add(invoice);
        }
        public static async Task<bool> Update(Invoice invoice)
        {
            if (invoice is null) return false;
            return await InvoiceData.Update(invoice);
        }

        public static async Task<bool> Delete(int id)
        {
            if (id <= 0) return false;
            return await InvoiceData.Delete(id);
        }
        public static async Task<Invoice?> Get(int id)
        {
            if (id <= 0) return null;
            return await InvoiceData.Get(id);
        }
        public static async Task<List<Invoice>?> GetAll()
        {
            return await InvoiceData.GetAll();
        }
    }
}
