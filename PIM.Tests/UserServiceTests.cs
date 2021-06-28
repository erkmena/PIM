using Moq;
using NUnit.Framework;
using PIM.Model.DTOs;
using PIM.Repositories.Interfaces;
using PIM.Services;
using System.Threading.Tasks;

namespace PIM.Tests
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _userService;
        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(u => u.GetUserAsync(It.IsAny<int>())).ReturnsAsync(MockModels.MockUserDTOModel());
            _userRepositoryMock.Setup(u => u.GetUserAsync(2)).ReturnsAsync((UserDTOModel)null);
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Test]
        public async Task Get_User_Successfull()
        {
            var result = await _userService.GetUserAsync(1);
            Assert.IsTrue(result.Data.UserId == MockModels.MockUserDTOModel().UserId);
        }
        [Test]
        public async Task Get_User_Returns_Null()
        {
            var result = await _userService.GetUserAsync(2);
            Assert.IsTrue(result.Data == null);
        }
        [Test]
        public async Task Get_User_User_Id_Validation_Error()
        {
            var result = await _userService.GetUserAsync(0);
            Assert.IsTrue(result.Errors.Count > 0 && result.Errors[0] == "userId parameter must be a valid value.");
        }
    }
}
