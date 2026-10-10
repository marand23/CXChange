using CXChange.BuildingBlocks.Application.Exceptions;

namespace CXChange.Modules.UserAccess.Administrator.Exceptions
{
    internal class UserAccessCommandException : ExchangeCommandException
    {
        public UserAccessCommandException(Enum errorCode, string message) : base(errorCode, message)
        {
        }
    }
}
