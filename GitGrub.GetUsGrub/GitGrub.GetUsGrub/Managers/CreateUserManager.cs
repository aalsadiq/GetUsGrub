using GitGrub.GetUsGrub.Helpers;
using GitGrub.GetUsGrub.Interfaces;
using GitGrub.GetUsGrub.Models.DTOs;
using GitGrub.GetUsGrub.Models.Interfaces;
using GitGrub.GetUsGrub.Models.Models;

namespace GitGrub.GetUsGrub
{
    public class CreateUserManager : ICreateUserManager
    {
        private readonly ISalt _salt = new PasswordSalt();
        private readonly IValidateGateway _validateGateway;

        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="validateGateway">Validation gateway to bind to the manager upon instantiation.</param>
        public CreateUserManager(IValidateGateway validateGateway)
        {
            _validateGateway = validateGateway;
        }

        /// <summary>
        /// Checks if the username of the registering user already exists in the data store.
        /// </summary>
        /// <param name="registerUserWithSecurityDto">DTO of user.</param>
        /// <returns>True if user doesn't exist. Throws exception otherwise.</returns>
        public bool CheckIfUserExists(RegisterUserWithSecurityDto registerUserWithSecurityDto)
        {
            try
            {
                var result = _validateGateway.CheckIfUserExists(registerUserWithSecurityDto);

                return result;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool HashPassword(RegisterUserWithSecurityDto registerUserWithSecurityDto)
        {
            var randomSalt = PayloadHasher.CreateRandomSalt(128);
            _salt.Salt = randomSalt;
            var passwordHash = PayloadHasher.HashPayload(_salt.Salt, registerUserWithSecurityDto.Password);
            registerUserWithSecurityDto.Password = passwordHash;
            return true;
        }

        public ResponseDto<RegisterUserDto> CreateNewUser(ResponseDto<RegisterUserDto> registerUserResponseDto)
        {
            try
            {
                using (var gateway = new UserGateway())
                {
                    var storeUserResult = gateway.StoreUser(registerUserResponseDto.Data);
                    if (storeUserResult)
                    {
                        return registerUserResponseDto;
                    }
                    else
                    {
                        //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                        var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CreateNewUser");
                        return responseDtoError;
                    }
                }
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CreateNewUser - Catch");
                return responseDtoError;
            }
        }
    }
}
﻿using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.UserAccessControl;
        public ResponseDto<RegisterUserDto> CheckUserDoesNotExist(ResponseDto<RegisterUserDto> registerUserResponseDto)
        {
            try
            {
                using (var gateway = new UserGateway())
                {
                    var getUserResult = gateway.GetUserByUsername(registerUserResponseDto.Data.Username);
                    // getUserResult.Username
                    if (registerUserResponseDto.Data.Username != getUserResult.Username)
                    {
                        return registerUserResponseDto;
                    }
                    else
                    {
                        //var responseDtoError = ErrorHandler<RegisterUserDto>.SetUserExistsError(registerUserResponseDto);
                        var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CheckUserDoesNotExist");
                        return responseDtoError;
                    }
                }
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CheckUserDoesNotExist - Catch");
                return responseDtoError;
            }
        }
        public ResponseDto<RegisterUserDto> HashPassword(ResponseDto<RegisterUserDto> registerUserResponseDto)
            try
            {
                registerUserResponseDto.Data.Salt = PayloadHasher.CreateRandomSalt(128);
                var passwordHash = PayloadHasher.HashWithSalt(registerUserResponseDto.Data.Salt, registerUserResponseDto.Data.Password);
                if (registerUserResponseDto.Data.Password != null &&
                    registerUserResponseDto.Data.Password != passwordHash)
                {
                    registerUserResponseDto.Data.Password = passwordHash;
                    return registerUserResponseDto;
                }
                else
                {
                    //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                    var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - HashPassword");
                    return responseDtoError;
                }
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - HashPassword - Catch");
                return responseDtoError;
            }
        public ResponseDto<RegisterUserDto> CreateClaims(ResponseDto<RegisterUserDto> registerUserResponseDto)
            try
            {
                var claimsFactory = new ClaimsFactory();

                registerUserResponseDto.Data.Claims = claimsFactory.CreateIndividualClaims();
                if (registerUserResponseDto.Data.Claims != null)
                {
                    return registerUserResponseDto;
                }
                else
                {
                    //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                    var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CreateClaims");
                    return responseDtoError;
                }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - CreateClaims - Catch");
                return responseDtoError;
        public ResponseDto<RegisterUserDto> SetAccountIsActive(ResponseDto<RegisterUserDto> registerUserResponseDto)
            try
            {
                registerUserResponseDto.Data.IsActive = true;
                if (registerUserResponseDto.Data.IsActive)
                {
                    return registerUserResponseDto;
                }
                else
                {
                    //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                    var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto, "CreateUserManager - SetAccountIsActive");
                    return responseDtoError;
                }
            }
            catch
            {
                //var responseDtoError = ErrorHandler<RegisterUserDto>.SetGeneralError(registerUserResponseDto);
                var responseDtoError = ErrorHandler<RegisterUserDto>.SetCustomError(registerUserResponseDto,
                    "CreateUserManager - SetAccountIsActive - Catch");
                return responseDtoError;
            }