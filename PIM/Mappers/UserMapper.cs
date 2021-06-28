using PIM.Model;
using PIM.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIM.Mappers
{
    public static class UserMapper
    {
        internal static UserDTOModel MapUserModelToDTOModel(UserModel user)
        {
            return new UserDTOModel()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Products = ProductMapper.MapProductListToDTOList(user.Products)
            };
        }
    }
}
