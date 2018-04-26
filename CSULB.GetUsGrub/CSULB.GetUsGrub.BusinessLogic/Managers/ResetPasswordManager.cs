using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>ResetPasswordManager</c> class.
    /// Contains business logic to manage the reseting of a user's password.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/25/2018
    /// </para>
    /// </summary>
    public class ResetPasswordManager
    {
        private readonly ResetPasswordDto _resetPasswordDto;

        public ResetPasswordManager(ResetPasswordDto resetPasswordDto)
        {
            _resetPasswordDto = resetPasswordDto;

        }

        /// <summary>
        /// The GetSecurityQuestions method.
        /// Gets security questions pertaining to a user which includes validations.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/25/2018
        /// </para>
        /// </summary>
        /// <returns>A ResponseDto which includes the ResetPasswordDto</returns>
        public ResponseDto<ResetPasswordDto> GetSecurityQuestions()
        {
            var resetPasswordPreLogicValidationStrategy = new ResetPasswordPreLogicValidationStrategy(_resetPasswordDto, ResetPasswordValidationTypes.GetSecurityQuestionsValidation);
            
            // Validate data transfer object
            var result = resetPasswordPreLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<ResetPasswordDto>
                {
                    Data = null,
                    Error = result.Error
                };
            }

            // Instantiatiate new Collection of SecurityQuestionDtos in ResetPasswordDto
            _resetPasswordDto.SecurityQuestionDtos = new Collection<SecurityQuestionDto>();

            // Get the security questions from the database
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.GetSecurityQuestions(_resetPasswordDto.Username);
                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<ResetPasswordDto>()
                    {
                        Data = null,
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }

                // Add SecurityQuestion to a list of SecurityQuestionDtos
                foreach (var securityQuestion in gatewayResult.Data)
                {
                    var securityQuestionDto = new SecurityQuestionDto(securityQuestion.Question);
                    _resetPasswordDto.SecurityQuestionDtos.Add(securityQuestionDto);
                }
            }

            return new ResponseDto<ResetPasswordDto>()
            {
                Data = _resetPasswordDto
            };
        }

        /// <summary>
        /// The ConfirmSecurityQuestionAnswers method.
        /// Confirms security answers pertaining to the security questions of a user.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/25/2018
        /// </para>
        /// </summary>
        /// <returns>A ResponseDto which includes the ResetPasswordDto</returns>
        public ResponseDto<ResetPasswordDto> ConfirmSecurityQuestionAnswers()
        {
            var resetPasswordPreLogicValidationStrategy = new ResetPasswordPreLogicValidationStrategy(_resetPasswordDto, ResetPasswordValidationTypes.ConfirmSecurityQuestionAnswersValidation);
            var payloadHasher = new PayloadHasher();

            // Validate data transfer object
            var result = resetPasswordPreLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<ResetPasswordDto>
                {
                    Data = null,
                    Error = result.Error
                };
            }

            ICollection<SecurityQuestionWithSaltDto> securityQuestionWithSalts;

            // Get security questions and security answer hashes
            using (var userGateway = new UserGateway())
            {
                // Get list of SecurityQuestionWithSalt
                var securityAnswerSaltsResult = userGateway.GetSecurityQuestionWithSalt(_resetPasswordDto.Username);
                if (securityAnswerSaltsResult.Error != null)
                {
                    return new ResponseDto<ResetPasswordDto>()
                    {
                        Data = null,
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
                
                securityQuestionWithSalts = securityAnswerSaltsResult.Data;
            }

            foreach (var securityQuestionWithSalt in securityQuestionWithSalts)
            {
                foreach (var securityQuestion in _resetPasswordDto.SecurityQuestionDtos)
                {
                    // Hash the security answers in data transfer object
                    if (securityQuestionWithSalt.Question == securityQuestion.Question && securityQuestionWithSalt.Answer != securityQuestion.Answer)
                    {
                        securityQuestion.Answer = payloadHasher.Sha256HashWithSalt(salt: securityQuestionWithSalt.Salt,
                            payload: securityQuestion.Answer);

                        // Confirm security answers match
                        if (securityQuestionWithSalt.Answer != securityQuestion.Answer)
                        {
                            return new ResponseDto<ResetPasswordDto>()
                            {
                                Data = null,
                                Error = ResetPasswordErrorMessages.SECURITY_QUESTIONS_AND_ANSWERS_NO_MATCH
                            };
                        }
                    }
                }
            }

            return new ResponseDto<ResetPasswordDto>()
            {
                Data = _resetPasswordDto
            };
        }

        /// <summary>
        /// The UpdatePassword method.
        /// Updates the password of a user.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/25/2018
        /// </para>
        /// </summary>
        /// <returns>A ResponseDto which includes the ResetPasswordDto</returns>
        public ResponseDto<ResetPasswordDto> UpdatePassword()
        {
            // Must validate the security question and answers again
            var confirmSecurityQuestionAnswersResult = ConfirmSecurityQuestionAnswers();
            if (confirmSecurityQuestionAnswersResult.Error != null)
            {
                return new ResponseDto<ResetPasswordDto>
                {
                    Data = null,
                    Error = confirmSecurityQuestionAnswersResult.Error
                };
            }

            var resetPasswordPreLogicValidationStrategy = new ResetPasswordPreLogicValidationStrategy(_resetPasswordDto, ResetPasswordValidationTypes.UpdatePasswordValidation);
            var saltGenerator = new SaltGenerator();
            var payloadHasher = new PayloadHasher();
            UserAccount userAccount;

            // Validate data transfer object
            var result = resetPasswordPreLogicValidationStrategy.ExecuteStrategy();
            if (result.Error != null)
            {
                return new ResponseDto<ResetPasswordDto>
                {
                    Data = null,
                    Error = result.Error
                };
            }

            // Get the existing UserAccount model associated with the username
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.GetUserByUsername(_resetPasswordDto.Username);
                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<ResetPasswordDto>()
                    {
                        Data = null,
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }

                userAccount = gatewayResult.Data;
            }

            // Set the new password to the UserAccount model
            userAccount.Password = _resetPasswordDto.Password;

            // Hash password
            var passwordSalt = new PasswordSalt(saltGenerator.GenerateSalt(128));
            userAccount.Password = payloadHasher.Sha256HashWithSalt(passwordSalt.Salt, userAccount.Password);

            // Update the password in the database
            using (var userGateway = new UserGateway())
            {
                var gatewayResult = userGateway.UpdatePassword(userAccount, passwordSalt);
                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<ResetPasswordDto>()
                    {
                        Data = null,
                        Error = GeneralErrorMessages.GENERAL_ERROR
                    };
                }
            }

            return new ResponseDto<ResetPasswordDto>()
            {
                Data = _resetPasswordDto
            };
        }
    }
}
