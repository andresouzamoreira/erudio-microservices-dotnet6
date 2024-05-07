using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Model;

namespace GeekShopping.ProductApi.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FinnAll();
        Task<ProductVO> FinnByID(long id);
        Task<ProductVO> Create(ProductVO productvo);
        Task<ProductVO> Update(ProductVO productvo);
        Task<bool> Delete(long id);
    }
}
