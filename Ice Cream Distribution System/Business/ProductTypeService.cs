using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class ProductTypeService
    {
        public static async Task<ProductType?> Add(ProductType type) => await ProductTypeData.Add(type);

        public static async Task<bool> Update(ProductType type) => await ProductTypeData.Update(type);

        public static async Task<bool> Delete(short id) => await ProductTypeData.Delete(id);

        public static async Task<ProductType?> Get(short id) => await ProductTypeData.Get(id);

        public static async Task<List<ProductType>?> GetAll() => await ProductTypeData.GetAll();
    }
}
