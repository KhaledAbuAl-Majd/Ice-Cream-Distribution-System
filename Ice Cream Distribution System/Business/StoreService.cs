using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class StoreService
    {
        public static async Task<Store?> Add(Store store) => await StoreData.Add(store);

        public static async Task<bool> Update(Store store) => await StoreData.Update(store);

        public static async Task<bool> Delete(int id) => await StoreData.Delete(id);

        public static async Task<Store?> Get(int id) => await StoreData.Get(id);

        public static async Task<List<Store>?> GetAll() => await StoreData.GetAll();
    }
}
