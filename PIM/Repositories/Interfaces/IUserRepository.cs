using PIM.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIM.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDTOModel> GetUserAsync(int userId);
    }
}
