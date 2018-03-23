//using CSULB.GetUsGrub.Models;

//namespace CSULB.GetUsGrub.BusinessLogic
//{
//    /// <summary>
//    /// The <c>UserAccountMapper</c> class.
//    /// Maps data transfer objects to and from UserAccount model.
//    /// <para>
//    /// @author: Jennifer Nguyen
//    /// @updated: 03/11/2018
//    /// </para>
//    /// </summary>
//    public class UserAccountMapper
//    {
//        public UserAccountDto MapModelToDto(UserAccount userAccount)
//        {
//            var userAccountDto = new UserAccountDto
//            {
//                Username = userAccount.Username,
//                Password = userAccount.Password,
//                IsActive = userAccount.IsActive,
//                IsFirstTimeUser = userAccount.IsFirstTimeUser
//            };
//            return userAccountDto;
//        }

//        public UserAccount MapDtoToModel(UserAccountDto userAccountDto)
//        {
//            var userAccount = new UserAccount
//            {
//                Username = userAccountDto.Username,
//                Password = userAccountDto.Password,
//                IsActive = userAccountDto.IsActive,
//                IsFirstTimeUser = userAccountDto.IsFirstTimeUser
//            };
//            return userAccount;
//        }
//    }
//}
