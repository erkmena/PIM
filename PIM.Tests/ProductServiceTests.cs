using Moq;
using NUnit.Framework;
using PIM.Model;
using PIM.Model.RequestModels;
using PIM.Repositories.Interfaces;
using PIM.Services;
using System.Threading.Tasks;

namespace PIM.Tests
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IBrandRepository> _brandRepositoryMock;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _brandRepositoryMock = new Mock<IBrandRepository>();

            _productRepositoryMock.Setup(p => p.AddProductAsync(It.IsAny<ProductModel>()));
            _productRepositoryMock.Setup(p => p.ArchiveProductAsync(It.IsAny<int>())).ReturnsAsync(true);
            _productRepositoryMock.Setup(p => p.ArchiveProductAsync(2)).ReturnsAsync(false);
            _productRepositoryMock.Setup(p => p.GetProductAsync(It.IsAny<int>())).ReturnsAsync(MockModels.MockProductModel());
            _productRepositoryMock.Setup(p => p.GetProductAsync(2)).ReturnsAsync((ProductModel)null);
            _productRepositoryMock.Setup(p => p.UpdateProductAsync(It.IsAny<ProductModel>())).ReturnsAsync(true);
            _productRepositoryMock.Setup(p => p.UpdateProductAsync(new ProductModel() { BrandId = 1, ProductName = "TestProduct", ProductId = 2 })).ReturnsAsync(false);

            _brandRepositoryMock.Setup(b => b.GetBrandAsync(It.IsAny<int>())).ReturnsAsync(MockModels.MockBrandDTOModel());

            _productService = new ProductService(_productRepositoryMock.Object, _brandRepositoryMock.Object);
        }

        [Test]
        public async Task Add_Product_Successfull()
        {
            ProductAddRequestModel addRequestModel = new ProductAddRequestModel()
            {
                BrandId = 1,
                ProductName = "TestProduct"
            };
            var result = await _productService.AddProductAsync(addRequestModel);
            Assert.IsTrue(result.Data.IsSuccess);
        }
        [Test]
        public async Task Add_Product_BrandId_Validation_Error()
        {
            ProductAddRequestModel addRequestModel = new ProductAddRequestModel()
            {
                BrandId = 0,
                ProductName = "TestProduct"
            };

            var result = await _productService.AddProductAsync(addRequestModel);

            Assert.IsTrue(result.Errors.Count > 0 && result.Errors[0] == "BrandId parameter must be a valid value.");
        }
        [Test]
        public async Task Add_Product_ProductName_Validation_Error()
        {
            ProductAddRequestModel addRequestModel = new ProductAddRequestModel()
            {
                BrandId = 1,
                ProductName = ""
            };

            var result = await _productService.AddProductAsync(addRequestModel);

            Assert.IsTrue(result.Errors.Count > 0 && result.Errors[0] == "ProductName parameter must be a valid value.");
        }

        [Test]
        public async Task Archive_Product_Successfull()
        {
            var result = await _productService.ArchiveProductAsync(1);

            Assert.IsTrue(result.Data.IsSuccess);
        }

        [Test]
        public async Task Archive_Product_Returns_False()
        {
            var result = await _productService.ArchiveProductAsync(2);
            Assert.IsFalse(result.Data.IsSuccess);
        }

        [Test]
        public async Task Archive_Product_Product_Id_Validation_Error()
        {
            var result = await _productService.ArchiveProductAsync(0);
            Assert.IsTrue(result.Errors.Count > 0 && result.Errors[0] == "productId parameter must be a valid value.");
        }

        [Test]
        public async Task Get_Product_Successfull()
        {
            var result = await _productService.GetProductAsync(1);

            Assert.IsTrue(result.Data.BrandId == MockModels.MockProductModel().BrandId);
        }
        [Test]
        public async Task Get_Product_Returns_Null()
        {
            var result = await _productService.GetProductAsync(2);

            Assert.IsTrue(result.Data == null);
        }
        [Test]
        public async Task Get_Product_Product_Id_Validation_Error()
        {
            var result = await _productService.GetProductAsync(0);
            Assert.IsTrue(result.Errors.Count > 0 && result.Errors[0] == "productId parameter must be a valid value.");
        }

        [Test]
        public async Task Update_Product_Successfull()
        {
            ProductUpdateRequestModel updateRequestModel = new ProductUpdateRequestModel()
            {
                BrandId = 1,
                ProductId = 1,
                ProductName = "TestProduct"
            };

            var result = await _productService.UpdateProductAsync(updateRequestModel);

            Assert.IsTrue(result.Data.IsSuccess);
        }

        [Test]
        public async Task Update_ProducT_Brand_Id_Validation_Error()
        {
            ProductUpdateRequestModel updateRequestModel = new ProductUpdateRequestModel()
            {
                BrandId = 0,
                ProductId = 1,
                ProductName = "TestProduct"
            };

            var result = await _productService.UpdateProductAsync(updateRequestModel);

            Assert.IsTrue(result.Errors.Count > 0 && result.Errors[0] == "BrandId parameter must be a valid value.");
        }
        [Test]
        public async Task Update_Product_Product_Id_Validation_Error()
        {
            ProductUpdateRequestModel updateRequestModel = new ProductUpdateRequestModel()
            {
                BrandId = 1,
                ProductId = 0,
                ProductName = "TestProduct"
            };

            var result = await _productService.UpdateProductAsync(updateRequestModel);

            Assert.IsTrue(result.Errors.Count > 0 && result.Errors[0] == "ProductId parameter must be a valid value.");
        }
        [Test]
        public async Task Update_ProducT_Product_Name_Validation_Error()
        {
            ProductUpdateRequestModel updateRequestModel = new ProductUpdateRequestModel()
            {
                BrandId = 1,
                ProductId = 1,
                ProductName = ""
            };

            var result = await _productService.UpdateProductAsync(updateRequestModel);

            Assert.IsTrue(result.Errors.Count > 0 && result.Errors[0] == "ProductName parameter must be a valid value.");
        }
    }
}
