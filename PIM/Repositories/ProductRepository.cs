using Dapper;
using PIM.Model;
using PIM.Repositories.Interfaces;
using PIM.Settings;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PIM.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppSettings _appSettings;
        private const string InsertProductQuery = "INSERT INTO PRODUCTS (ProductName, BrandId, IsDeleted) VALUES (@ProductName, @BrandId, @IsDeleted)";
        private const string SelectProductQuery = "SELECT ProductId, ProductName, BrandId FROM PRODUCTS Where ProductId = @ProductId AND IsDeleted = 0";
        private const string UpdateProductQuery = "UPDATE PRODUCTS SET ProductName = @ProductName, BrandId = @BrandId Where ProductId = @ProductId AND IsDeleted = 0";
        private const string ArchiveProductQuery = "INSERT INTO PRODUCTARCHIVE SELECT ProductId, ProductName, BrandId FROM PRODUCTS Where ProductId = @ProductId AND IsDeleted = 0;" +
                                                   "UPDATE PRODUCTS SET IsDeleted = 1 Where ProductId = @ProductId AND IsDeleted = 0";

        public ProductRepository(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task AddProductAsync(ProductModel product)
        {
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                await connection.QueryAsync(InsertProductQuery, new { ProductName = product.ProductName, BrandId = product.BrandId, IsDeleted = 0 });
            }
        }

        public async Task<bool> ArchiveProductAsync(int productId)
        {
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                int effectedRowCount = await connection.ExecuteAsync(ArchiveProductQuery, new { ProductId = productId });
                if (effectedRowCount == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<ProductModel> GetProductAsync(int productId)
        {
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                ProductModel product = await connection.QueryFirstOrDefaultAsync<ProductModel>(SelectProductQuery, new { ProductId = productId });
                return product;
            }
        }

        public async Task<bool> UpdateProductAsync(ProductModel product)
        {
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                int effectedRowCount = await connection.ExecuteAsync(UpdateProductQuery, new { ProductId = product.ProductId, ProductName = product.ProductName, BrandId = product.BrandId });
                if(effectedRowCount == 0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
