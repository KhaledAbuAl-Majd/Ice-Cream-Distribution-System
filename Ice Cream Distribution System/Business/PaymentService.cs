using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class PaymentService
    {
        public static async Task<Payment?> Add(Payment payment) => await PaymentData.Add(payment);

        public static async Task<bool> Update(Payment payment) => await PaymentData.Update(payment);

        public static async Task<bool> Delete(int id) => await PaymentData.Delete(id);

        public static async Task<Payment?> Get(int id) => await PaymentData.Get(id);

        public static async Task<List<Payment>?> GetAll() => await PaymentData.GetAll();
    }
}
