namespace CXChange.API.Modules.UserManagment.ErrorMessages
{
    public class UserAccessError : IError
    {



        public ApiErrorModel GetError(string innerMessage)
        {
            if (innerMessage == "User already exist")
            {
                return new ApiErrorModel { code = (int)UserAccessErrorCodes.UserAlreadyRegistered, message = innerMessage };
            }
            throw new NotImplementedException();
        }

    }

    public enum UserAccessErrorCodes
    {
        UserAlreadyRegistered = -100,
        VerificationCodeWrong = -101,
        UserRestricted = -102,
        UserBanned = -103,
        PhoneNumberOrPasswordtWrong = -104
    }


}
