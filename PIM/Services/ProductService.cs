using PIM.Mappers;
using PIM.Model;
using PIM.Model.RequestModels;
using PIM.Model.Responses;
using PIM.Repositories.Interfaces;
using PIM.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PIM.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        public ProductService(IProductRepository productRepository, IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
        }
        public async Task<BaseResponse<AddProductResponse>> AddProductAsync(ProductAddRequestModel productRequest)
        {
            var response = new BaseResponse<AddProductResponse>();

            if (productRequest.BrandId <= 0 || string.IsNullOrEmpty(productRequest.ProductName))
            {
                string notValidAttribute = productRequest.BrandId <= 0 ? nameof(productRequest.BrandId) : nameof(productRequest.ProductName);
                response.Errors.Add($"{notValidAttribute} parameter must be a valid value.");
                return response;
            }

            ProductModel product = new ProductModel()
            {
                BrandId = productRequest.BrandId,
                ProductName = productRequest.ProductName
            };
            await _productRepository.AddProductAsync(product);

            response.Data = new AddProductResponse()
            {
                IsSuccess = true
            };

            return response;
        }

        public async Task<BaseResponse<GetProductResponse>> GetProductAsync(int productId)
        {
            var response = new BaseResponse<GetProductResponse>();

            if (productId <= 0)
            {
                response.Errors.Add($"{nameof(productId)} parameter must be a valid value.");
                return response;
            }

            var product = await _productRepository.GetProductAsync(productId);
            if (product != null)
            {
                var brand = await _brandRepository.GetBrandAsync(product.BrandId);
                response.Data = new GetProductResponse()
                {
                    ProductName = product.ProductName,
                    BrandId = product.BrandId,
                    BrandName = brand.BrandName
                };
            }
            return response;
        }

        public async Task<BaseResponse<UpdateProductResponse>> UpdateProductAsync(ProductUpdateRequestModel productRequest)
        {
            var response = new BaseResponse<UpdateProductResponse>();

            if (productRequest.BrandId <= 0)
            {
                response.Errors.Add($"{nameof(productRequest.BrandId)} parameter must be a valid value.");
            }
            if (productRequest.ProductId <= 0)
            {
                response.Errors.Add($"{nameof(productRequest.ProductId)} parameter must be a valid value.");
            }
            if (string.IsNullOrEmpty(productRequest.ProductName))
            {
                response.Errors.Add($"{nameof(productRequest.ProductName)} parameter must be a valid value.");
            }

            ProductModel product = new ProductModel()
            {
                ProductId = productRequest.ProductId,
                BrandId = productRequest.BrandId,
                ProductName = productRequest.ProductName
            };
            bool result = await _productRepository.UpdateProductAsync(product);

            response.Data = new UpdateProductResponse()
            {
                IsSuccess = result
            };

            return response;
        }

        public async Task<BaseResponse<ArchiveProductResponse>> ArchiveProductAsync(int productId)
        {
            var response = new BaseResponse<ArchiveProductResponse>();

            if (productId <= 0)
            {
                response.Errors.Add($"{nameof(productId)} parameter must be a valid value.");
                return response;
            }

            bool result = await _productRepository.ArchiveProductAsync(productId);

            response.Data = new ArchiveProductResponse()
            {
                IsSuccess = result
            };
            return response;
        }
    }
}
