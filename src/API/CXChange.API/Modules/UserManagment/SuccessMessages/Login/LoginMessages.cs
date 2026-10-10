using CXChange.Modules.UserAccess.Application.Commands.Login.ConfirmLogin;
using CXChange.Modules.UserAccess.Application.Commands.Login.UserLogin;

namespace CXChange.API.Modules.UserManagment.SuccessMessages.Login
{
    public static class LoginMessages
    {
        public static string GetMessages(object obj)
        {
            if (obj.GetType() == typeof(UserLoginChecked))
            {
                return "Successfull login ";
            }
            if (obj.GetType() == typeof(UserLoginConfirmed))
            {
                return "login confirmed";
            }
            throw new NotImplementedException();
        }
    }
}
