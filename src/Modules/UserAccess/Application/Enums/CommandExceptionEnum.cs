namespace CXChange.Modules.UserAccess.Application.Enums
{
    internal enum CommandExceptionEnum
    {
        //enum error code should be in this format -1XXX
        VerificationCodeInvalid = -1999,
        UserDoesNotExist,
        UserRegisterTokenNotValid,
        UserLoginTokenNotValid,

    }
}
