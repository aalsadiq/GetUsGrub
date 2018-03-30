using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using FluentAssertions;
using Xunit;

namespace CSULB.GetUsGrub.UnitTests
{
    public class PwnedPasswordIntegrationTest
    {
        [Fact]
        public async void Should_Fail_When_Validating_Insecure_Password()
        {
            var service = new PwnedPasswordValidationService();
            var password = "password123";

            var result = await service.IsPasswordValidAsync(password);

            result.Should().Be(false);
        }

        [Fact]
        public async void Should_Pass_When_Validating_Secure_Password()
        {
            var service = new PwnedPasswordValidationService();
            var password = "I Am A Secure Password";

            var result = await service.IsPasswordValidAsync(password);

            result.Should().Be(true);
        }
    }
}