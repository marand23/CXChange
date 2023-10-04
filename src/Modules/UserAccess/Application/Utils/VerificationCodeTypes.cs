using CXChange.Modules.UserAccess.Domain.User;

namespace CXChange.Modules.UserAccess.Application.Utils
{
    internal class KeyWords
    {
        public static readonly string CustomTokenProvider = "CusotmTokenProvider";
        public static readonly string LoginUser = "user_login";
        public static readonly string ResetPassword = "reset_password";
        public static readonly string ChangeTwoFactorType = "change_two_factor_type";
        public static readonly string VerifyPhoneNumberOnSet = "verify_phonenumber";
        public static readonly string VerifyOldPhoneNumberOnChange = "verify_old_phonenumber";
        public static readonly string VerifyNewPhoneNumberOnChange = "verify_new_phonenumber";
        public static readonly string ConfirmAuthenticatorActivation = "confirm_authenticator_activation";
        public static readonly string ConfirmDefault2FaForAuthenticator = "confirm_default_code_authenticator";
        public static readonly string ChangePassword = "change_user_password";
        public static readonly string DisableEmail = "disable_user_email";
        public static readonly string DeactivateAuthenticator = "deactivate_user_authenticator";
        public static readonly string VerifyOldEmailOnEmailChange = "verify_old_email";
        public static readonly string VerifyNewEmailOnEmailChange = "verify_new_email";
        public static readonly string VerifyEmailOnEmailSet = "verify_email";
        public static readonly string ConfirmRegister = "confirm_user_registration";
        public static readonly string DisableAuthenticator = "disable_authenticator";

    }
    public class CodeType
    {
        public string Name { get; }
        public bool RequiresTokenVerification { get; }
        public bool RequiresEmailOrPhoneNumberConfirmation { get; }
        //if this is true ,the email or phone nubmer in request should
        //match the email or phone number in database for user 
        //otherwise there is no need to impose 
        public bool RequiresEmailOrPhoneNumberMatching { get; }


        public bool RequiresAuthentication { get; }

        private static List<CodeType> CodeTypes = new List<CodeType>
        {
            new CodeType(KeyWords.LoginUser, requiresTokenVerification: true,false),
            new CodeType(KeyWords.ResetPassword, requiresAuthentication:false ),
            new CodeType(KeyWords.ChangeTwoFactorType),
            new CodeType(KeyWords.ConfirmAuthenticatorActivation),
            new CodeType(KeyWords.ConfirmDefault2FaForAuthenticator),
            new CodeType(KeyWords.ChangePassword),
            new CodeType(KeyWords.DisableEmail),
            new CodeType(KeyWords.DeactivateAuthenticator),
            new CodeType(KeyWords.VerifyNewEmailOnEmailChange,requiresEmailOrPhoneNumberMatching:false),
            new CodeType(KeyWords.VerifyOldEmailOnEmailChange),
            new CodeType(KeyWords.VerifyEmailOnEmailSet,requiresEmailOrPhoneNumberMatching:false,requiresEmailOrPhoneNumberConfirmation:false),
            new CodeType(KeyWords.ConfirmRegister,requiresEmailOrPhoneNumberConfirmation:false,requiresEmailOrPhoneNumberMatching:false),
            new CodeType(KeyWords.DisableAuthenticator),
            new CodeType(KeyWords.VerifyPhoneNumberOnSet),
            new CodeType(KeyWords.VerifyOldPhoneNumberOnChange),
            new CodeType(KeyWords.VerifyNewPhoneNumberOnChange)



        };
        private CodeType(string name, bool requiresTokenVerification = false, bool requiresAuthentication = true, bool requiresEmailOrPhoneNumberConfirmation = true, bool requiresEmailOrPhoneNumberMatching = true)
        {
            Name = name;
            RequiresTokenVerification = requiresTokenVerification;
            RequiresAuthentication = requiresAuthentication;
            RequiresEmailOrPhoneNumberConfirmation = requiresEmailOrPhoneNumberConfirmation;
            RequiresEmailOrPhoneNumberMatching = requiresEmailOrPhoneNumberMatching;

        }
        public static CodeType GetCodeTypeByName(string name)
        {
            return CodeTypes.Find(codeType => codeType.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        //generate purpose for user token and user code
        internal static string GeneratePurposeForDefaultTwoFactor(SnapShot snapShot, string codeType)
        {

            switch (snapShot.ActiveTwoFactor)
            {
                case (TwoFactorEnum.SMS):
                    return string.Concat(codeType, snapShot.Id, snapShot.PhoneNumber);
                case (TwoFactorEnum.EMAIL):
                    return string.Concat(codeType, snapShot.Id, snapShot.Email);
                case (TwoFactorEnum.AUTHENTICATOR):
                    return string.Concat(codeType, snapShot.Id, TwoFactorEnum.AUTHENTICATOR);


            }
            throw new NotImplementedException();


        }
        internal static string GeneratePurposeForSpecificTwoFactor(string UserId, string codeType, string destination)
        {
            return string.Concat(codeType, UserId, destination);

        }



    }



}
