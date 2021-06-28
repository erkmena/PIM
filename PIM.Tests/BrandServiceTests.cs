using Moq;
using NUnit.Framework;
using PIM.Model;
using PIM.Model.DTOs;
using PIM.Model.RequestModels;
using PIM.Repositories.Interfaces;
using PIM.Services;
using System.Threading.Tasks;

namespace PIM.Tests
{
    public class BrandServiceTests
    {
        private Mock<IBrandRepository> _brandRepositoryMock;
        private BrandService _brandService;
        [SetUp]
        public void Setup()
        {
            _brandRepositoryMock = new Mock<IBrandRepository>();
            _brandRepositoryMock.Setup(b => b.GetBrandAsync(It.IsAny<int>())).ReturnsAsync(MockModels.MockBrandDTOModel());
            _brandRepositoryMock.Setup(b => b.GetBrandAsync(2)).ReturnsAsync((BrandDTOModel)null);
            _brandService = new BrandService(_brandRepositoryMock.Object);  
        }

        [Test]
        public async Task Get_Brand_Successfull()
        {
            var result = await _brandService.GetBrandAsync(1);
            Assert.IsTrue(result.Data.BrandId == MockModels.MockBrandDTOModel().BrandId);
        }
        [Test]
        public async Task Get_Brand_Returns_Null()
        {
            var result = await _brandService.GetBrandAsync(2);
            Assert.IsTrue(result.Data == null);
        }
        [Test]
        public async Task Get_Brand_Brand_Id_Validation_Error()
        {
            var result = await _brandService.GetBrandAsync(0);
            Assert.IsTrue(result.Errors.Count > 0 && result.Errors[0] == "brandId parameter must be a valid value.");
        }
    }
}