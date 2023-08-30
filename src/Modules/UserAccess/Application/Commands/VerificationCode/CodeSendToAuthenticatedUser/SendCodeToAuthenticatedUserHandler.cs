using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeSendToAuthenticatedUser
{
    internal class SendCodeToAuthenticatedUserHandler : IConsumer<SendCodeToAuthenticatedUser>
    {
        IUserRepository _repo;
        IVerificationCodeFunctions _sender;
        public SendCodeToAuthenticatedUserHandler(IUserRepository repo, IVerificationCodeFunctions sender)
        {
            _repo = repo;
            _sender = sender;
        }
        public async Task Consume(ConsumeContext<SendCodeToAuthenticatedUser> context)
        {
            var user = await _repo.GetUserById(context.Message.UserId);
            if (user == null)
            {
                throw new ExchangeCommandException("user is not exits", CommandExceptionEnum.UserDoesNotExist);
            }
            if (CodeType.GetCodeTypeByName(context.Message.CodeType).RequiresTokenVerification)
            {
                var tokenRes = await _repo.ValidateUserToken(user, context.Message.Token, "", context.Message.CodeType);
                if (!tokenRes)
                {
                    throw new NotImplementedException();
                }
            }

            await _sender.Send(user, context.Message.CodeType);
            await context.RespondAsync(new CodeSendedToAuthenticatedUser { });
        }
    }
}
