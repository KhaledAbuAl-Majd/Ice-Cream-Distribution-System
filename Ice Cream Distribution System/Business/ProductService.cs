using Data_Access;
using Ice_Cream_Distribution_System.Models;

namespace Business
{
    public static class ProductService
    {
        public static async Task<Product?> Add(Product product) => await ProductData.Add(product);

        public static async Task<bool> Update(Product product) => await ProductData.Update(product);

        public static async Task<bool> Delete(int id) => await ProductData.Delete(id);

        public static async Task<Product?> Get(int id) => await ProductData.Get(id);

        public static async Task<List<Product>?> GetAll() => await ProductData.GetAll();
    }
}
