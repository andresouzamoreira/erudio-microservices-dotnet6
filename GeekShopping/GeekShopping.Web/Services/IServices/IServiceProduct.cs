using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IServiceProduct
    {
        Task<IEnumerable<ProductModel>> FindaAllProducts(); 
        Task <ProductModel> FindbProductById(long id);
        Task<ProductModel> CreateProduct(ProductModel productModel);
        Task<ProductModel> UpdateProduct(ProductModel productModel);
        Task <bool> DeleteProductById(long id);
    }
}
