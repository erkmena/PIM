using PIM.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIM.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<GetUserResponse>> GetUserAsync(int userId);
    }
}
