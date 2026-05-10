using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class InvoiceRecordService
    {
        public static async Task<InvoiceRecord?> Add(InvoiceRecord record) => await InvoiceRecordData.Add(record);

        public static async Task<bool> Update(InvoiceRecord record) => await InvoiceRecordData.Update(record);

        public static async Task<bool> Delete(int id) => await InvoiceRecordData.Delete(id);

        public static async Task<List<InvoiceRecord>?> GetAllByInvoiceId(int invoiceId) => await InvoiceRecordData.GetAllByInvoiceId(invoiceId);

        public static async Task<InvoiceRecord?> Get(int id)
        {
            if (id <= 0) return null;
            return await InvoiceRecordData.Get(id);
        }
    }
}
