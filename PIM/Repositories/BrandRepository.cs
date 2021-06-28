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
    public class BrandRepository : IBrandRepository
    {
        private readonly AppSettings _appSettings;
        private const string SelectBrandQuery = "SELECT B.BrandId, BrandName, ProductId, ProductName FROM Brands B INNER JOIN Products P ON P.BrandId = B.BrandId Where B.BrandId = @BrandId AND IsDeleted = 0";
        public BrandRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<BrandDTOModel> GetBrandAsync(int brandId)
        {
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                var brandDictionary = new Dictionary<int, BrandModel>();
                List<ProductModel> products = new List<ProductModel>();
                var brandList = await connection.QueryAsync<BrandModel, ProductModel, BrandModel>(SelectBrandQuery,
                    (brand,product) =>
                    {
                        BrandModel brandEntry;
                        if (!brandDictionary.TryGetValue(brand.BrandId, out brandEntry))
                        {
                            brandEntry = brand;
                            brandEntry.Products = new List<ProductModel>();
                            brandDictionary.Add(brandEntry.BrandId, brandEntry);
                        }
                        product.BrandId = brand.BrandId;
                        products.Add(product);
                        brandEntry.Products = products;
                        return brandEntry;
                    }
                    ,new { BrandId = brandId }
                    , splitOn: "ProductId");
                if(brandList.AsList().Count == 0)
                {
                    return null;
                }
                return BrandMapper.MapBrandModelToDTOModel(brandList.AsList()[0]);
            }
        }
    }
}
