using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class CarService
    {
        public static async Task<Car?> Add(Car car) => await CarData.Add(car);

        public static async Task<bool> Update(Car car) => await CarData.Update(car);

        public static async Task<bool> Delete(short id) => await CarData.Delete(id);

        public static async Task<Car?> Get(short id) => await CarData.Get(id);

        public static async Task<List<Car>?> GetAll() => await CarData.GetAll();
    }
}
