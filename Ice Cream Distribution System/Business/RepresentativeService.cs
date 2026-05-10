using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class RepresentativeService
    {
        public static async Task<Representative?> Add(Representative rep) => await RepresentativeData.Add(rep);

        public static async Task<bool> Update(Representative rep) => await RepresentativeData.Update(rep);

        public static async Task<bool> Delete(int id) => await RepresentativeData.Delete(id);

        public static async Task<Representative?> Get(int id) => await RepresentativeData.Get(id);

        public static async Task<List<Representative>?> GetAll() => await RepresentativeData.GetAll();
    }
}
