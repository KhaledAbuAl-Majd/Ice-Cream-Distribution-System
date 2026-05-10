using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class DriverService
    {
        public static async Task<Driver?> Add(Driver driver) => await DriverData.Add(driver);

        public static async Task<bool> Update(Driver driver) => await DriverData.Update(driver);

        public static async Task<bool> Delete(int id) => await DriverData.Delete(id);

        public static async Task<Driver?> Get(int id) => await DriverData.Get(id);

        public static async Task<List<Driver>?> GetAll() => await DriverData.GetAll();
    }
}
