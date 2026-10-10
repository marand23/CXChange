using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Enums;
using Microsoft.AspNetCore.Identity;

namespace CXChange.Modules.UserAccess.Application.Exceptions
{
    public class UserAccessCommandException : ExchangeCommandException
    {

        public UserAccessCommandException(CommandExceptionEnum errorCodes, string message = "an error occured in the UserAccessCommand") : base(errorCodes, message)
        {


        }

        public static implicit operator UserAccessCommandException(IdentityResult result)
        {
            var error = result.Errors.FirstOrDefault()?.Code;
            var errorenum = Enum.TryParse(error, out CommandExceptionEnum res);
            if (!errorenum) return new UserAccessCommandException(CommandExceptionEnum.DefaultError, error);
            return new UserAccessCommandException(res);
        }
        public static implicit operator UserAccessCommandException(SignInResult result)
        {
            CommandExceptionEnum res;
            if (result.IsNotAllowed)
            {

                if (!Enum.TryParse(nameof(result.IsNotAllowed), out res)) return new UserAccessCommandException(CommandExceptionEnum.DefaultError);
                return new UserAccessCommandException(res);
            }
            if (result.IsLockedOut)
            {

                if (!Enum.TryParse(nameof(result.IsLockedOut), out res)) return new UserAccessCommandException(CommandExceptionEnum.DefaultError);
                return new UserAccessCommandException(res);
            }
            return new UserAccessCommandException(CommandExceptionEnum.DefaultError);



        }
    }

}
