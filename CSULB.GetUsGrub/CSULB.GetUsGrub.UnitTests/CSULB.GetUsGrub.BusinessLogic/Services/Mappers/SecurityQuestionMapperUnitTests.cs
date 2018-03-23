//using CSULB.GetUsGrub.BusinessLogic;
//using CSULB.GetUsGrub.Models;
//using FluentAssertions;
//using Xunit;

//namespace CSULB.GetUsGrub.UnitTests
//{
//    /// <summary>
//    /// The <c>SecurityQuestionMapperUnitTests</c> class.
//    /// Contains unit tests for SecurityQuestionMapper.
//    /// <para>
//    /// @author: Jennifer Nguyen
//    /// @updated: 03/11/2018
//    /// </para>
//    /// </summary>
//    public class SecurityQuestionMapperUnitTests
//    {
//        [Fact]
//        public void Should_MapModelToDto_When_AllFieldsFromModelAreMappedToDto()
//        {
//            // Arrange
//            var securityQuestion = new SecurityQuestion()
//            {
//                Id = 0,
//                UserId = 0,
//                Question = 1,
//                Answer = "answer"
//            };
//            var securityQuestionMapper = new SecurityQuestionMapper();

//            // Act
//            var securityQuestionDto = securityQuestionMapper.MapModelToDto(securityQuestion);

//            // Assert
//            securityQuestionDto.Question.Should().Be(1);
//            securityQuestionDto.Answer.Should().Be("answer");
//        }

//        [Fact]
//        public void Should_MapDtoToModel_When_AllFieldsFromDtoAreMappedToModel()
//        {
//            // Arrange
//            var securityQuestionDto = new SecurityQuestionDto()
//            {
//                Question = 1,
//                Answer = "answer"
//            };
//            var securityQuestionMapper = new SecurityQuestionMapper();

//            // Act
//            var securityQuestion = securityQuestionMapper.MapDtoToModel(securityQuestionDto);

//            // Assert
//            securityQuestion.Id.Should().Be(null);
//            securityQuestion.UserId.Should().Be(null);
//            securityQuestion.Question.Should().Be(1);
//            securityQuestion.Answer.Should().Be("answer");
//        }
//    }
//}
