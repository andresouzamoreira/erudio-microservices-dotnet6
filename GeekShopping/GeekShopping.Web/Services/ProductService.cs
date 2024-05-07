using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IServiceProduct
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/product";

        public ProductService(HttpClient? client)
        {
            _client = client ?? throw new ArgumentException(nameof(_client));
        }

        public async Task<IEnumerable<ProductModel>> FindaAllProducts()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindbProductById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }
        public async Task<ProductModel> CreateProduct(ProductModel productModel)
        {
           var response = await _client.PostAsJsonAsync(BasePath, productModel);
            if (response.IsSuccessStatusCode)
                return await  response.ReadContentAs<ProductModel>();
            else
                throw new Exception("Algo de errado ocorreu na API");           
           
        }

        public async Task<ProductModel> UpdateProduct(ProductModel productModel)
        {
            var response = await _client.PutAsJsonAsync(BasePath, productModel);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();
            else
                throw new Exception("Algo de errado ocorreu na API");
        }
        public async Task<bool> DeleteProductById(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else
                throw new Exception("Algo de errado ocorreu na API");
        }

    }
}
