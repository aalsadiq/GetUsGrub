using Xunit;
using FluentAssertions;
using CSULB.GetUsGrub.UserAccessControl;

namespace CSULB.GetUsGrub.UnitTests
{
    /// <summary>
    /// Unit testing for the Claims Factory.
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/07/2018
    /// </summary>
    public class ClaimsFactoryUnitTests
    {
        // Arrange
        ClaimsFactory factory = new ClaimsFactory();

        [Fact]
        public void Should_ReturnIndividualClaims_Given_IndividualAccountType()
        {
            // Arrange
            var accountType = AccountType.INDIVIDUAL;

            // Act
            var result = factory.Create(accountType);
            var expected = new IndividualClaims().Claims;

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Should_ReturnRestaurantClaims_Given_RestaurantAccountType()
        {
            // Arrange
            var accountType = AccountType.RESTAURANT;

            // Act
            var result = factory.Create(accountType);
            var expected = new RestaurantClaims().Claims;

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Should_ReturnAdminClaims_Given_AdminAccountType()
        {
            // Arrange
            var accountType = AccountType.ADMIN;

            // Act
            var result = factory.Create(accountType);
            var expected = new AdminClaims().Claims;

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Should_ReturnEmptyClaims_Given_InvalidAccountType()
        {
            // Arrange
            var accountType = "InvalidAccountType";

            // Act
            var result = factory.Create(accountType);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Should_ReturnEmptyClaims_Given_Null()
        {
            // Act
            var result = factory.Create(null);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void Should_ReturnEmptyClaims_Given_EmptyType()
        {
            // Arrange
            var accountType = "";

            // Act
            var result = factory.Create(accountType);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
