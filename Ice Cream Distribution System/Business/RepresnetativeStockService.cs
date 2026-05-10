using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class RepresentativesStockService
    {
        //public static async Task<RepresentativesStock?> Add(RepresentativesStock stock) => await RepresentativesStockData.Add(stock);

        //public static async Task<bool> Update(RepresentativesStock stock) => await RepresentativesStockData.Update(stock);

        //public static async Task<bool> Delete(int id) => await RepresentativesStockData.Delete(id);

        public static async Task<RepresentativesStock?> Get(int id) => await RepresentativesStockData.Get(id);

        public static async Task<List<RepresentativesStock>?> GetAllByRepresentative(int repID) => await RepresentativesStockData.GetAll(repID);
    }
}
