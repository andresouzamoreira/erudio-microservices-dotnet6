using AutoMapper;
using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Model;
using GeekShopping.ProductApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlServerContext _sqlServerContext;
        private readonly IMapper _mapper;

        public ProductRepository(SqlServerContext sqlServerContext, IMapper mapper)
        {
            _sqlServerContext = sqlServerContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FinnAll()
        {
            List<Product> products = await _sqlServerContext.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FinnByID(long id)
        {
            Product products = await _sqlServerContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new Product();
            return _mapper.Map<ProductVO>(products);
        }

        public async Task<ProductVO> Create(ProductVO productvo)
        {
            Product product = _mapper.Map<Product>(productvo);
            _sqlServerContext.Add(product);
            await _sqlServerContext.SaveChangesAsync();
            return  _mapper.Map<ProductVO>(product);
        }
        public async Task<ProductVO> Update(ProductVO productvo)
        {
            Product product = _mapper.Map<Product>(productvo);
            _sqlServerContext.Update(product);
            await _sqlServerContext.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }
        public async Task<bool> Delete(long id)
        {
            Product products = await _sqlServerContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new Product();
            if (products.Id <=0) return false;
            _sqlServerContext.Remove(products);
            await _sqlServerContext.SaveChangesAsync();
            return true;
        }

     
    }
}
