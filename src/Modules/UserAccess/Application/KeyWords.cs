namespace CXChange.Modules.UserAccess.Application
{
    internal class KeyWords
    {
        public static string ConfirmRegisterTotpModifier(string PhoneNumber) => $"register{PhoneNumber}";
        public static string LoginTotpModifier(string PhoneNumber) => $"login{PhoneNumber}";
        public static string RegisterTokenProvider() => "RegisterTokenProvider";
        public static string LoginTokenProvider() => "LoginTokenProvider";
        public static string RegisterTokenProviderPurpose(string PhoneNumber) => $"register{PhoneNumber}";
        public static string LoginTokenProviderPurpose(string PhoneNumber) => $"login{PhoneNumber}";
        public static string ResetPasswordTotpModifier(string PhoneNumber) => $"resetpassword{PhoneNumber}";


    }
}
