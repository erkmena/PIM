using Dapper;
using PIM.Mappers;
using PIM.Model;
using PIM.Model.DTOs;
using PIM.Repositories.Interfaces;
using PIM.Settings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PIM.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;
        private const string SelectuserQuery = "SELECT U.UserId, UserName, P.ProductId, ProductName, BrandId FROM USERS U " +
            "INNER JOIN UserProduct UP ON UP.UserId = U.UserId " +
            "INNER JOIN Products P ON P.ProductId = UP.ProductId Where U.UserId = @UserId";
        public UserRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<UserDTOModel> GetUserAsync(int userId)
        {
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                var userDictionary = new Dictionary<int, UserModel>();
                List<ProductModel> products = new List<ProductModel>();
                var userList = await connection.QueryAsync<UserModel, ProductModel, UserModel>(SelectuserQuery,
                    (user, product) =>
                    {
                        UserModel userEntry;
                        if (!userDictionary.TryGetValue(user.UserId, out userEntry))
                        {
                            userEntry = user;
                            userEntry.Products = new List<ProductModel>();
                            userDictionary.Add(userEntry.UserId, userEntry);
                        }
                        products.Add(product);
                        userEntry.Products = products;
                        return userEntry;
                    }
                    , new { UserId = userId }
                    , splitOn: "ProductId");
                if (userList.AsList().Count > 0)
                {
                    return UserMapper.MapUserModelToDTOModel(userList.AsList()[0]);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
