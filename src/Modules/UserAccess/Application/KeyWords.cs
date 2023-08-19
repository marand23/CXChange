namespace CXChange.Modules.UserAccess.Application
{
    internal class KeyWords
    {
        public static string ConfirmRegisterTotpModifier(string PhoneNumber) => $"register{PhoneNumber}";
        public static string LoginTotpModifier(string PhoneNumber) => $"login{PhoneNumber}";

    }
}
