using PIM.Model.Responses;
using PIM.Repositories.Interfaces;
using PIM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIM.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<BaseResponse<GetUserResponse>> GetUserAsync(int userId)
        {
            var response = new BaseResponse<GetUserResponse>();

            if (userId <= 0)
            {
                response.Errors.Add($"{nameof(userId)} parameter must be a valid value.");
                return response;
            }

            var user = await _userRepository.GetUserAsync(userId);
            if (user != null)
            {
                response.Data = new GetUserResponse()
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Products = user.Products
                };
            }
            return response;
        }
    }
}
