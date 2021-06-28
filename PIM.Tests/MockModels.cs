using PIM.Model;
using PIM.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Tests
{
    static class MockModels
    {
        public static ProductModel MockProductModel()
        {
            return new ProductModel()
            {
                BrandId = 1,
                ProductId = 1,
                ProductName = "MockProduct"
            };
        }
        public static List<ProductModel> MockProductModelList()
        {
            List<ProductModel> productList = new List<ProductModel>();
            ProductModel product1 = new ProductModel()
            {
                BrandId = 1,
                ProductId = 1,
                ProductName = "MockProduct1",
            };
            ProductModel product2 = new ProductModel()
            {
                BrandId = 2,
                ProductId = 2,
                ProductName = "MockProduct2"
            };
            productList.Add(product1);
            productList.Add(product2);
            return productList;
        }
        public static List<ProductDTOModel> MockProductDTOModelList()
        {
            List<ProductDTOModel> productList = new List<ProductDTOModel>();
            ProductDTOModel product1 = new ProductDTOModel()
            {
                BrandId = 1,
                ProductId = 1,
                ProductName = "MockProduct1",
            };
            ProductDTOModel product2 = new ProductDTOModel()
            {
                BrandId = 2,
                ProductId = 2,
                ProductName = "MockProduct2"
            };
            productList.Add(product1);
            productList.Add(product2);
            return productList;
        }
        public static BrandModel MockBrandModel()
        {
            return new BrandModel()
            {
                BrandId = 1,
                BrandName = "MockBrand",
                Products = MockProductModelList()
            };
        }
        public static UserModel MockUserModel()
        {
            return new UserModel()
            {
                UserId = 1,
                UserName = "MockUser",
                Products = MockProductModelList()
            };
        }
        public static UserDTOModel MockUserDTOModel()
        {
            return new UserDTOModel()
            {
                UserId = 1,
                UserName = "MockUser",
                Products = MockProductDTOModelList()
            };
        }
        public static BrandDTOModel MockBrandDTOModel()
        {
            return new BrandDTOModel()
            {
                BrandId = 1,
                BrandName = "MockBrand",
                Products = MockProductDTOModelList()
            };
        }
    }
}
