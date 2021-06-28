using PIM.Model.Responses;
using System.Threading.Tasks;

namespace PIM.Services.Interfaces
{
    public interface IBrandService
    {
        public Task<BaseResponse<GetBrandResponse>> GetBrandAsync(int brandId);
    }
}
