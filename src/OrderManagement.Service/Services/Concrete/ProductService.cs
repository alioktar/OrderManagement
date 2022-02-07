using AutoMapper;
using OrderManagement.Core.CrossCuttingConcerns.Caching;
using OrderManagement.Core.Utilities.Exceptions;
using OrderManagement.Core.Utilities.Response;
using OrderManagement.DTOs;
using OrderManagement.Entities;
using OrderManagement.Repository.Base;
using OrderManagement.Service.Core;
using OrderManagement.Service.Services.Base;

namespace OrderManagement.Service.Services.Concrete
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ICacheManager cacheManager) : base(unitOfWork, mapper, cacheManager) { }

        public async Task<IDataResponse<ProductDto>> GetAsync(int productId)
        {
            var product = await UnitOfWork.ProductRepository.GetAsync(p => p.Id == productId && !p.IsDeleted) ?? throw new NotFoundException($"Product not found with id : {productId}");
            

            return new SuccessDataResponse<ProductDto>(Mapper.Map<ProductDto>(product));
        }

        public async Task<IDataResponse<IEnumerable<ProductDto>>> GetAllAsync()
        {
            var products = await UnitOfWork.ProductRepository.GetListAsync(p => !p.IsDeleted);

            return new SuccessDataResponse<IEnumerable<ProductDto>>(Mapper.Map<IEnumerable<ProductDto>>(products));
        }

        public async Task<IDataResponse<ProductDto>> CreateAsync(ProductAddDto product)
        {
            var added = await UnitOfWork.ProductRepository.AddAsync(Mapper.Map<Product>(product));
            await UnitOfWork.SaveChangesAsync();

            return new SuccessDataResponse<ProductDto>(Mapper.Map<ProductDto>(added));
        }

        public async Task<IDataResponse<ProductDto>> UpdateAsync(ProductUpdateDto product)
        {
            var existing = await UnitOfWork.ProductRepository.GetAsync(x => x.Id == product.Id) ?? throw new NotFoundException($"Product not found with id : {product.Id}");

            var updated = UnitOfWork.ProductRepository.Update(Mapper.Map(product, existing));
            await UnitOfWork.SaveChangesAsync();

            return new SuccessDataResponse<ProductDto>(Mapper.Map<ProductDto>(updated));
        }

        public async Task<IResponse> DeleteAsync(int productId)
        {
            var existing = await UnitOfWork.ProductRepository.GetAsync(x => x.Id == productId) ?? throw new NotFoundException($"Product not found with id : {productId}");

            UnitOfWork.ProductRepository.Delete(existing);
            await UnitOfWork.SaveChangesAsync();

            return new SuccessResponse();
        }
    }
}
