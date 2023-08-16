using CXChange.Modules.UserAccess.Application.Commands.UserRegisterConfirmation;
using CXChange.Modules.UserAccess.Application.Commands.UserRegistration;

namespace CXChange.API.Modules.UserManagment.SuccessMessages.Register
{
    internal static class RegisterMessages
    {
        public static string GetMessage(object obj)
        {
            if (obj.GetType() == typeof(UserRegistered))
            {
                return "با موفقیت ثبت نام شد ";
            }
            if (obj.GetType() == typeof(UserConfirmed))
            {
                return "ثبت نام موفق";
            }
            throw new NotImplementedException();
        }
    }
}
