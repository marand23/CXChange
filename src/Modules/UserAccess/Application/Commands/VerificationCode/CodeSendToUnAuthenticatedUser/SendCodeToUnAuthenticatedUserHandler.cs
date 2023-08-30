using CXChange.BuildingBlocks.Application.Exceptions;
using CXChange.Modules.UserAccess.Application.Enums;
using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Application.Utils;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeSendToUnAuthenticatedUser
{
    internal class SendCodeToUnAuthenticatedUserHandler : IConsumer<SendCodeToUnAuthenticatedUser>
    {
        IUserRepository _repo;
        IVerificationCodeFunctions _sender;
        public SendCodeToUnAuthenticatedUserHandler(IUserRepository repo, IVerificationCodeFunctions sender)
        {
            _repo = repo;
            _sender = sender;
        }
        public async Task Consume(ConsumeContext<SendCodeToUnAuthenticatedUser> context)
        {
            var user = await _repo.GetUserByPhoneNumber(context.Message.PhoneNumber);
            if (user == null)
            {
                throw new ExchangeCommandException("user is not exits", CommandExceptionEnum.UserDoesNotExist);
            }
            if (CodeType.GetCodeTypeByName(context.Message.CodeType).RequiresTokenVerification)
            {
                var tokenRes = await _repo.ValidateUserToken(user, context.Message.Token, KeyWords.CustomTokenProvider, context.Message.CodeType);
                if (!tokenRes)
                {

                }
            }

            await _sender.Send(user, string.Concat(context.Message.CodeType, user.GetSnapShot().Id));
            await context.RespondAsync(new CodeToUnAuthenticatedUserSent { });
        }
    }
}
