namespace CXChange.Modules.UserAccess.Application.Utils
{
    internal class KeyWords
    {
        public static readonly string CustomTokenProvider = "CusotmTokenProvider";
        public static readonly string LoginUser = "Login-User";
        public static readonly string ResetPassword = "Reset-Password";
        public static readonly string ChangeTwoFactorType = "Change-Two-Factor-Type";
        public static readonly string ConfirmRegister = "Confirm-User-Registration";
        public static readonly string ConfirmAuthenticator = "Confirm-Authenticator-Code";
        public static readonly string ConfirmDefault2FaForAuthenticator = "confirm-default-code-authenticator";


    }
    public class CodeType
    {
        public string Name { get; }
        public bool RequiresTokenVerification { get; }
        public CodeProvider Provider { get; }
        private static List<CodeType> CodeTypes = new List<CodeType>
        {
            new CodeType(KeyWords.LoginUser, requiresTokenVerification: true),
            new CodeType(KeyWords.ResetPassword, requiresTokenVerification: true),
            new CodeType(KeyWords.ChangeTwoFactorType,false),
            new CodeType(KeyWords.ConfirmAuthenticator,false,CodeProvider.Authenticator),
            new CodeType(KeyWords.ConfirmDefault2FaForAuthenticator,false)

        };
        private CodeType(string name, bool requiresTokenVerification, CodeProvider provider = CodeProvider.Default)
        {
            Name = name;
            RequiresTokenVerification = requiresTokenVerification;
        }
        public static CodeType GetCodeTypeByName(string name)
        {
            return CodeTypes.Find(codeType => codeType.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }


    }
    public enum CodeProvider
    {
        Default,
        Authenticator
    }



}
