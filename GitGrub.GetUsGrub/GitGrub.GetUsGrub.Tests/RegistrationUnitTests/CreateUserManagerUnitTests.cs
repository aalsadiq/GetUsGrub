using GitGrub.GetUsGrub.Models;
using Moq;
using Xunit;

namespace GitGrub.GetUsGrub.Tests.RegistrationUnitTests
{
    public class CreateUserManagerUnitTests
    {
        [Fact]
        public void CheckIfUserExists_UserExistsInDb_Fail()
        {
            var mockRegisterUserDto = new Mock<IRegisterUserDto>();
            mockRegisterUserDto.Object.UserAccount.Username = "null";
        }
    }
}
