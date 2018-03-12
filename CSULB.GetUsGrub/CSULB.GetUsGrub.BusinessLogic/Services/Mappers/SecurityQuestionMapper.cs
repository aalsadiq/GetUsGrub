using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic.Services.Mappers
{
    /// <summary>
    /// The <c>SecurityQuestionMapper</c> class.
    /// Maps data transfer objects to and from SecurityQuestion model.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class SecurityQuestionMapper
    {
        public SecurityQuestionDto ModelToDto(SecurityQuestion securityQuestion)
        {
            var securityQuestionDto = new SecurityQuestionDto
            {
                Question = securityQuestion.Question,
                Answer = securityQuestion.Answer
            };
            return securityQuestionDto;
        }

        public SecurityQuestion DtoToModel(SecurityQuestionDto securityQuestionDto)
        {
            var securityQuestion = new SecurityQuestion
            {
                Question = securityQuestionDto.Question,
                Answer = securityQuestionDto.Answer
            };
            return securityQuestion;
        }
    }
}
