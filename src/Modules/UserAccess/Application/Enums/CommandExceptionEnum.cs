namespace CXChange.Modules.UserAccess.Application.Enums
{
    public enum CommandExceptionEnum
    {
        //enum error code should be in this format -1XXX
        VerificationCodeInvalid = -1999,
        UserNotFound,
        UserRegisterTokenNotValid,
        UserLoginTokenNotValid,
        UserOrPasswordInvalid,
        EnterCodeFirst,
        DuplicateUserName,
        DefaultError,
        OldPasswordInvalid,
        IsNotAllowed,
        PasswordTooShort,
        IsLockedOut,
        InvalidToken,
        PhoneNumberDoesNotMatch,
        EmaiDoesNotMatch,
        PhoneNumberNotConfirmed,
        EmailNotConfirmed,
        EmailAlreadyConfirmed,
        EmailNotFound,
        PhoneNumberAlreadyConfirmed


    }
}
