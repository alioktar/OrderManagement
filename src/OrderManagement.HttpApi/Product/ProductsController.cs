using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Utilities.Response;
using OrderManagement.DTOs;
using OrderManagement.HttpApi.Base;
using OrderManagement.Service.Services.Base;

namespace OrderManagement.HttpApi.Product
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IDataResponse<ProductDto>> Create(ProductAddDto product) => await _productService.CreateAsync(product);

        [HttpDelete("{productId}")]
        public async Task<IResponse> Delete(int productId) => await _productService.DeleteAsync(productId);

        [HttpGet]
        public async Task<IDataResponse<IEnumerable<ProductDto>>> GetAll() => await _productService.GetAllAsync();

        [HttpGet("{productId}")]
        public async Task<IDataResponse<ProductDto>> Get(int productId) => await _productService.GetAsync(productId);

        [HttpPut]
        public async Task<IDataResponse<ProductDto>> Update(ProductUpdateDto product) => await _productService.UpdateAsync(product);
    }
}
