using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class AreaService
    {
        public static async Task<List<Area>?> GetAll() => await AreaData.GetAll();

        public static async Task<Area?> Get(int id) => await AreaData.Get(id);
    }
}
