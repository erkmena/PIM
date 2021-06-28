using PIM.Model;
using PIM.Model.RequestModels;
using PIM.Model.Responses;
using System.Threading.Tasks;

namespace PIM.Services.Interfaces
{
    public interface IProductService
    {
        public Task<BaseResponse<GetProductResponse>> GetProductAsync(int productId);
        public Task<BaseResponse<AddProductResponse>> AddProductAsync(ProductAddRequestModel productRequest);
        public Task<BaseResponse<UpdateProductResponse>> UpdateProductAsync(ProductUpdateRequestModel productRequest);
        public Task<BaseResponse<ArchiveProductResponse>> ArchiveProductAsync(int productId);

    }
}
