using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub
{
    public class ErrorHandler<T>
    {
        public static ResponseDto<T> SetGeneralError(ResponseDto<T> responseDto)
        {
            responseDto.Error = new CustomException("Something went wrong. Please try again later.");
            return responseDto;
        }

        public static ResponseDto<T> SetCustomError(ResponseDto<T> responseDto, string errorMessage)
        {
            responseDto.Error = new CustomException(errorMessage);
            return responseDto;
        }

        public static ResponseDto<T> SetUserExistsError(ResponseDto<T> responseDto)
        {
            responseDto.Error = new CustomException("Username is already used.");
            return responseDto;
        }
    }
}