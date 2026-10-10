using CXChange.Modules.UserAccess.Domain.User;

namespace CXChange.Modules.UserAccess.Application.Services.Interfaces
{
    internal interface IVerificationCodeFunctions
    {
        Task Send(User user, string modifier);
        bool Verify(User user, int code, string modifier);
    }
}
