using GitGrub.GetUsGrub.BusinessLogic.Managers.Interfaces;
using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    public class CreateRestaurantUserManager : ICreateNewUser<IRegisterRestaurantUserDto>
    {
        public ResponseDto<IRegisterRestaurantUserDto> CreateNewUser(IRegisterRestaurantUserDto registerRestaurantUserDto)
        {
            var responseDto = new ResponseDto<IRegisterRestaurantUserDto>();

            using (var gateway = new UserGateway())
            {
                var storeUserResult = gateway.StoreUserAccount(registerRestaurantUserDto);
                if (storeUserResult)
                {
                    responseDto.Data = registerRestaurantUserDto;
                    return responseDto;
                }
                else
                {
                    // TODO: Should this be the general error? Can I extend it so everyone can use it?
                    responseDto.Error = "Something went wrong. Please try again later.";
                    return responseDto;
                }
            }
        }
    }
}
