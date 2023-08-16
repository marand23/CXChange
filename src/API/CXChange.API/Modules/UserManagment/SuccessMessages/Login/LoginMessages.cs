using CXChange.Modules.UserAccess.Application.Commands.ConfirmLogin;
using CXChange.Modules.UserAccess.Application.Commands.UserLogin;

namespace CXChange.API.Modules.UserManagment.SuccessMessages.Login
{
    public static class LoginMessages
    {
        public static string GetMessages(object obj)
        {
            if (obj.GetType() == typeof(UserLoginChecked))
            {
                return "ثبت نام موفق";
            }
            if (obj.GetType() == typeof(UserLoginConfirmed))
            {
                return "ورود موفق";
            }
            throw new NotImplementedException();
        }
    }
}
