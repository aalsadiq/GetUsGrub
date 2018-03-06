using GitGrub.GetUsGrub.DataAccess;
using GitGrub.GetUsGrub.Models;

// TODO: !!!!!!!!!!!!!!!!!!!!!! Can reduce the number of methods used if Brian can delete user by just giving a username
namespace GitGrub.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>CreateRestaurantUserManager</c> class.
    /// Contains CreateNewUser method which creates a new restaurant user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public class CreateRestaurantUserManager : ICreateNewUser<IRegisterRestaurantUserDto>
    {
        /// <summary>
        /// A CreateNewUser method.
        /// Creates a new Restaurant user in user data store.
        /// Sequentially stores the following models to the user store: 
        /// RestaurantAccount and RestaurantProfile.
        /// If failures occur, then any previously added user data will be deleted and an error will return in the ResponseDto.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2017
        /// </para>
        /// </summary>
        // TODO: Confirm with Brian his Gateways and methods. Also how to perform User Delete if failure?
        public ResponseDto<IRegisterRestaurantUserDto> CreateNewUser(IRegisterRestaurantUserDto registerRestaurantUserDto)
        {
            using (var gateway = new UserGateway())
            { 
                ResponseDto<IRegisterRestaurantUserDto> responseDto = new ResponseDto<IRegisterRestaurantUserDto>();

                // Store RestaurantAccount model
                var result = gateway.StoreRestaurantAccount(responseDto.Data.UserAccount.Username, responseDto.Data.RestaurantAccount);
                if (!result)
                {
                    responseDto.Error = ErrorHandler.GetGeneralError();
                    return responseDto;
                }

                // Store RestaurantProfile model
                result = gateway.StoreRestaurantProfile(responseDto.Data.UserAccount.Username);
                if (!result)
                {
                    responseDto.Error = ErrorHandler.GetGeneralError();
                    return responseDto;
                }

                return responseDto;
            }
        }
    }
}
