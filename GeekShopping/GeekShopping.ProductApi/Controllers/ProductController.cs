using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));   
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FinnByID(long id)
        {
            var produtct  = await _repository.FinnByID(id);
            if (produtct.Id <= 0) return NotFound();
            return Ok(produtct);
            
        }

        [HttpGet]
        public async Task<ActionResult<ProductVO>> FinnAll()
        {
            var produtct = await  _repository.FinnAll();            
            return Ok(produtct);

        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO productVO)
        {
            if(productVO == null) return BadRequest();
            var product = await _repository.Create(productVO);  
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO productVO)
        {
            if (productVO == null) return BadRequest();
            var product = await _repository.Update(productVO);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var status = await _repository.Delete(id);
            if (!status) return BadRequest();             
            return Ok(status);
        }
    }
}
