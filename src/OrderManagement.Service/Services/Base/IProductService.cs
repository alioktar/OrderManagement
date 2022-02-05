using OrderManagement.Core.Utilities.Response;
using OrderManagement.DTOs;

namespace OrderManagement.Service.Services.Base
{
    public interface IProductService
    {
        Task<IDataResponse<ProductDto>> GetAsync(int productId);
        Task<IDataResponse<IEnumerable<ProductDto>>> GetAllAsync();

        Task<IDataResponse<ProductDto>> CreateAsync(ProductAddDto product);
        Task<IDataResponse<ProductDto>> UpdateAsync(ProductUpdateDto product);
        Task<IResponse> DeleteAsync(int productId);
    }
}
