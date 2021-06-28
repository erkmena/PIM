using PIM.Model.Responses;
using PIM.Repositories.Interfaces;
using PIM.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PIM.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<BaseResponse<GetBrandResponse>> GetBrandAsync(int brandId)
        {
            var response = new BaseResponse<GetBrandResponse>();

            if (brandId <= 0)
            {
                response.Errors.Add($"{nameof(brandId)} parameter must be a valid value.");
                return response;
            }

            var brand = await _brandRepository.GetBrandAsync(brandId);
            if (brand != null)
            {
                response.Data = new GetBrandResponse()
                {
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    Products = brand.Products
                };
            }
            return response;
        }
    }
}
